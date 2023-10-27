using System;
using UnityEngine;

namespace _Skycall.Scripts.Helpers.Shooting
{
    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnRecycleThis;
        [SerializeField] private float speed;

        void Update()
        {
            transform.Translate(transform.forward * (speed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(this.tag))
            {
                return;
            }

            if (other.CompareTag(Tags.T_asteroid) || other.CompareTag(Tags.T_ship)
                                                  || other.CompareTag(Tags.T_enemyShip))
            {
                OnRecycleThis?.Invoke(this);
            }
        }
    }
}