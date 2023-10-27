using UnityEngine;

namespace _Skycall.Scripts.Helpers.Shooting
{
    public class AutoShootingBehaviour : MonoBehaviour, IShootBehaviour
    {
        [SerializeField] private BulletPool pool;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireRate = 0.5f;

        private bool _started;


        private float _nextFireTime = 0f;

        private bool CanShoot => _started && Time.time > _nextFireTime;

        void Update()
        {
            if (CanShoot)
            {
                Debug.Log("autoshot");

                Shoot();
                _nextFireTime = Time.time + 1f / fireRate;
            }
        }

        public void Shoot()
        {
            Debug.Log("Onshooooooo");

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