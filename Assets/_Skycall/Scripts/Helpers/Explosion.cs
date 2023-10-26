using UnityEngine;

namespace _Skycall.Scripts.Helpers
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] float lifeTime;

        [SerializeField] ParticleSystem explosionParticleSystem;

        float _startTime;


        public void OnEnable()
        {
            explosionParticleSystem.Clear();
            explosionParticleSystem.Play();

            _startTime = Time.realtimeSinceStartup;
        }
    }
}