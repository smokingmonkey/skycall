using System;
using System.Collections;
using UnityEngine;

namespace _Skycall.Scripts.Helpers.Shooting
{
    public class Bullet : MonoBehaviour
    {
        public BulletPool owner;
        [SerializeField] private float speed;
        [SerializeField] private bool isEnemy;
        [SerializeField] private Explosion explosion;

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
            transform.Translate(transform.forward * (speed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isEnemy && other.CompareTag(Tags.T_ship) || other.CompareTag(Tags.T_asteroid))
            {
                other.gameObject.SetActive(false);

                Recycle();
            }
            else if (other.CompareTag(Tags.T_enemyShip) || other.CompareTag(Tags.T_asteroid))
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