using System.Collections.Generic;
using UnityEngine;

namespace _Skycall.Scripts.Helpers.Shooting
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;

        private Queue<Bullet> _pool;
        [SerializeField] private int poolMaxSize;

        private void Start()
        {
            for (int i = 0; i < poolMaxSize; i++)
            {
                Bullet bullet = Instantiate(bulletPrefab);
            }
        }

        public Bullet GetBullet()
        {
            Bullet bullet;

            if (_pool.Count > 0)
            {
                bullet = _pool.Dequeue();
            }
            else
            {
                bullet = Instantiate(bulletPrefab);
            }

            bullet.OnRecycleThis += OnRecycleElement;


            bullet.gameObject.SetActive(true);


            return SetBullet(bullet);
        }
        
        public Bullet GetBullet(Vector3 firePointPosition, Quaternion firePointRotation)
        {
            Bullet bullet;

            if (_pool.Count > 0)
            {
                bullet = _pool.Dequeue();
            }
            else
            {
                bullet = Instantiate(bulletPrefab);
            }

            bullet.OnRecycleThis += OnRecycleElement;
            
            var transform1 = bullet.transform;
            transform1.position = firePointPosition;
            transform1.rotation = firePointRotation;
            bullet.gameObject.SetActive(true);

            return bullet;
        }
        
        

        Bullet SetBullet(Bullet bullet)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            return bullet;
        }

        private void OnDisable()
        {
            foreach (var bullet in _pool)
            {
                bullet.OnRecycleThis -= OnRecycleElement;
            }
        }

        void OnRecycleElement(Bullet bullet)
        {
            bullet.OnRecycleThis -= OnRecycleElement;
            bullet.gameObject.SetActive(false);
            _pool.Enqueue(bullet);
        }

        
    }
}