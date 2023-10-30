using System;
using System.Collections;
using UnityEngine;

namespace _Skycall.Scripts.Helpers.Shooting
{
    public class Bullet : MonoBehaviour
    {
        public BulletPool owner;
        [SerializeField] private float speed;
        [SerializeField] public bool isEnemy;

        private float _recyclingTime = 4f;

        private void OnEnable()
        {
            StartCoroutine(RecycleCoroutine(_recyclingTime));
        }

        IEnumerator RecycleCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            Recycle();
        }

        void Update()
        {
            transform.Translate(transform.up * (speed * Time.deltaTime), Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isEnemy && other.CompareTag(Tags.T_ship))
            {
                Recycle();
            }

            if (!isEnemy && other.CompareTag(Tags.T_enemyShip))
            {
                other.gameObject.SetActive(false);
                Recycle();
            }

            if (other.CompareTag(Tags.T_asteroid))
            {
                other.gameObject.SetActive(false);
                Recycle();
            }
        }

        void Recycle()
        {
            owner.OnRecycleThis(this);
        }
    }
}