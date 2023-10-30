using UnityEngine;

namespace _Skycall.Scripts.Helpers.Shooting
{
    public class AutoShootingBehaviour : MonoBehaviour, IShootBehaviour
    {
        [SerializeField] private BulletPool pool;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireRate;

        private bool _started;


        private float _nextFireTime = 0f;

        private bool CanShoot => _started && Time.time > _nextFireTime;

        void Update()
        {
            //Gives randomness to the shooting decision
            var num = Random.Range(0, 10);

            if (CanShoot && num % 2 == 0)
            {
                Shoot();
                _nextFireTime = Time.time + 1f / fireRate;
            }
        }

        public void Shoot()
        {
            pool.GetBullet(firePoint);
        }

        public void Init()
        {
            _started = true;
        }

        public void Stop()
        {
            _started = false;
        }
    }
}