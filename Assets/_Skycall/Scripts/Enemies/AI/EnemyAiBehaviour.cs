using System;
using UnityEngine;

namespace _Skycall.Scripts.Enemies.AI
{
    public class EnemyAiBehaviour : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.T_ship))
            {
                throw new NotImplementedException();
            }
        }
    }
}