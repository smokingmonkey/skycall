using UnityEngine;

namespace _Skycall.Scripts.Helpers.Shooting
{
    public class ClickShootingBehaviour : MonoBehaviour, IShootBehaviour
    {
        [SerializeField] private BulletPool pool;
        [SerializeField] private Transform firePoint;

        private bool _started;

        private bool CanShoot => _started && (Input.GetMouseButtonDown(0)
                                              || Input.GetKeyDown(KeyCode.Space));

        void Update()
        {
            if (CanShoot)
            {
                Shoot();
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