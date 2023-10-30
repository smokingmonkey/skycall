using System;
using _Skycall.Scripts.Helpers;
using _Skycall.Scripts.Helpers.Shooting;
using _Skycall.Scripts.Util;
using ModestTree;
using UnityEngine;
using Zenject;

namespace _Skycall.Scripts.Enemies.EnemyShip
{
    public class EnemyShip : EnemyBase
    {
        LevelHelper _level;
        Rigidbody _rigidBody;
        Settings _settings;

        private Transform _enemyTarget;

        [SerializeField] private RadarSphere radar;

        [SerializeField] private AutoShootingBehaviour weapon;
        [SerializeField] private float rotationSpeed;

        private IShootBehaviour Weapon => weapon;


        [Inject]
        public void Construct(LevelHelper level, Settings settings)
        {
            _level = level;
            _settings = settings;
            _rigidBody = GetComponent<Rigidbody>();
        }


        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public float Mass
        {
            get { return _rigidBody.mass; }
            set { _rigidBody.mass = value; }
        }

        public float Scale
        {
            get
            {
                var scale = transform.localScale;
                // We assume scale is uniform
                Assert.That(scale[0] == scale[1] && scale[1] == scale[2]);

                return scale[0];
            }
            set
            {
                transform.localScale = new Vector3(value, value, value);
                _rigidBody.mass = value;
            }
        }

        public Vector3 Velocity
        {
            get { return _rigidBody.velocity; }
            set { _rigidBody.velocity = value; }
        }

        public override void UpdateEnemy()
        {
            CheckForTeleport();

            _enemyTarget = radar.FoundObject(Tags.T_ship);
            //When Player ship is near, Enemy Ships Follows it and shot directly to it

            if (_enemyTarget)
            {
                Vector3 targetPosition =
                    new Vector3(_enemyTarget.position.x, _enemyTarget.position.y, transform.position.z);

                transform.LookAt(targetPosition);

                //Enemy ship starts shooting
                StarShooting();
                return;
            }

            //When player is lost Enemy ship stops shooting
            StopShooting();
        }


        public override void FixedUpdateEnemy()
        {
            //When Player's Ship is near, Enemy Ships Follows it and shot directly to it
            if (_enemyTarget) return;

            AlignTransformToMovement();

            // Limit speed to a maximum
            LimitSpeed();
        }

        void AlignTransformToMovement()
        {
            var velocity = _rigidBody.velocity;
            Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                Time.deltaTime * rotationSpeed);
        }

        void LimitSpeed()
        {
            var velocity = _rigidBody.velocity;
            var speed = velocity.magnitude;

            if (speed > _settings.maxSpeed)
            {
                var dir = _rigidBody.velocity / speed;
                _rigidBody.velocity = dir * _settings.maxSpeed;
            }
        }

        void StarShooting()
        {
            Weapon.Init();
        }

        void StopShooting()
        {
            Weapon.Stop();
        }


        protected override void CheckForTeleport()
        {
            if (Position.x > _level.Right + Scale && IsMovingInDirection(Vector3.right))
            {
                transform.SetX(_level.Left - Scale);
            }
            else if (Position.x < _level.Left - Scale && IsMovingInDirection(-Vector3.right))
            {
                transform.SetX(_level.Right + Scale);
            }
            else if (Position.y < _level.Bottom - Scale && IsMovingInDirection(-Vector3.up))
            {
                transform.SetY(_level.Top + Scale);
            }
            else if (Position.y > _level.Top + Scale && IsMovingInDirection(Vector3.up))
            {
                transform.SetY(_level.Bottom - Scale);
            }
        }

        bool IsMovingInDirection(Vector3 dir)
        {
            return Vector3.Dot(dir, _rigidBody.velocity) > 0;
        }


        [Serializable]
        public class Settings
        {
            public float massScaleFactor;
            public float maxSpeed;
        }

        public new class Factory : PlaceholderFactory<EnemyShip>
        {
        }
    }
}