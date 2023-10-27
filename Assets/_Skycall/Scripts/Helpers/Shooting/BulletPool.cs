using System.Collections.Generic;
using UnityEngine;

namespace _Skycall.Scripts.Helpers.Shooting
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;

        private Queue<Bullet> _pool = new Queue<Bullet>();
        [SerializeField] private int poolMaxSize;

        private List<GameObject> _bullets = new List<GameObject>();

        private void Start()
        {
            for (int i = 0; i < poolMaxSize; i++)
            {
                GameObject go = Instantiate(bulletPrefab);
                go.SetActive(false);
                _bullets.Add(go);

                Bullet bullet = go.GetComponent<Bullet>();
                bullet.owner = this;

                _pool.Enqueue(bullet);
            }
        }


        public void GetBullet(Transform cannon)
        {
            if (_pool.Count > 0)
            {
                var bullet = _pool.Dequeue();

                bullet.transform.position = cannon.position;
                bullet.transform.rotation = cannon.rotation;
                bullet.gameObject.SetActive(true);
            }
        }

        public void OnRecycleThis(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            _pool.Enqueue(bullet);
        }

        private void OnDisable()
        {
            foreach (var bullet in _bullets)
            {
                Destroy(bullet.gameObject);
            }
        }
    }
}