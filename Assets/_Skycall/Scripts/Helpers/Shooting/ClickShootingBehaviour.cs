using UnityEngine;

namespace _Skycall.Scripts.Helpers.Shooting
{
    public class ClickShootingBehaviour : MonoBehaviour, IShootBehaviour
    {
        [SerializeField] private BulletPool pool;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireRate = 0.5f;

        private float _nextFireTime = 0f;
        private bool _started;

        private bool CanShoot => _started && Input.GetMouseButtonDown(0) && Time.time > _nextFireTime;

        void Update()
        {
            if (CanShoot)
            {
                Shoot();
                _nextFireTime = Time.time + 1f / fireRate;
            }
        }

        public void Shoot()
        {
            pool.GetBullet(firePoint.position, firePoint.rotation);
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