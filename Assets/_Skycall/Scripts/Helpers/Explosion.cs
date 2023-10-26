using System;
using UnityEngine;

namespace _Skycall.Scripts.Helpers
{
    public class Explosion : MonoBehaviour
    {
        public static event Action OnExplosion;

        [SerializeField] ParticleSystem explosionParticleSystem;

        float _startTime;


        public void OnEnable()
        {
            explosionParticleSystem.Clear();
            explosionParticleSystem.Play();
            OnExplosion?.Invoke();
            _startTime = Time.realtimeSinceStartup;
        }
    }
}