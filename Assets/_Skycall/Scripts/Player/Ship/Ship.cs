using System;
using _Skycall.Scripts.Helpers;
using _Skycall.Scripts.Models;
using _Skycall.Scripts.Player.Ship.States;
using _Skycall.Scripts.Util;
using ModestTree;
using UnityEngine;
using Zenject;

namespace _Skycall.Scripts.Player.Ship
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        [SerializeField] private ParticleSystem _particleSystem;

        [SerializeField] private GameObject _explosion;

        private ShipStateFactory _stateFactory;
        private ShipState _state;
        private LevelHelper _level;


        public event Action OnCrashed;

        [Inject]
        public void Construct(ShipStateFactory stateFactory, LevelHelper level)
        {
            _stateFactory = stateFactory;
            _level = level;
        }

        public MeshRenderer MeshRenderer
        {
            get { return _meshRenderer; }
        }

        public ParticleSystem ParticleEmitter
        {
            get { return _particleSystem; }
        }


        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public float Scale
        {
            get
            {
                var scale = transform.localScale;

                Assert.That(scale[0] == scale[1] && scale[1] == scale[2]);

                return scale[0];
            }
            set { transform.localScale = new Vector3(value, value, value); }
        }

        public Quaternion Rotation
        {
            get { return transform.rotation; }
            set { transform.rotation = value; }
        }

        public void Start()
        {
            ChangeState(ShipStates.WaitingToStart);
            _explosion.SetActive(false);
        }

        public void Update()
        {
            _state.Update();
        }

        private void FixedUpdate()
        {
            CheckForTeleport();
        }

        public void OnTriggerEnter(Collider other)
        {
            _state.OnTriggerEnter(other);
        }

        public void ChangeState(ShipStates state)
        {
            if (_state != null)
            {
                _state.Dispose();
                _state = null;
            }

            _state = _stateFactory.CreateState(state);
            _state.Start();
        }

        public void OnShipCrashed()
        {
            _explosion.SetActive(true);
            OnCrashed?.Invoke();
        }

        public void OnShipMoving()
        {
            _explosion.SetActive(false);
        }

        //If the ship is about to get out of the camera applies teleport effect
        private void CheckForTeleport()
        {
            if (Position.x > _level.Right + Scale)
            {
                transform.SetX(_level.Left - Scale);
            }
            else if (Position.x < _level.Left - Scale)
            {
                transform.SetX(_level.Right + Scale);
            }
            else if (Position.y < _level.Bottom - Scale)
            {
                transform.SetY(_level.Top + Scale);
            }
            else if (Position.y > _level.Top + Scale)
            {
                transform.SetY(_level.Bottom - Scale);
            }
        }
    }
}