using System;
using ModestTree;
using UnityEngine;
using Zenject;

namespace _Skycall.Scripts.Level.Collectibles
{
    public class Coin : MonoBehaviour, ICollectible
    {
        public static event Action<float> OnCollected;

        private Settings _settings;

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
        }

        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public float Scale
        {
            get
            {
                var scale = transform.localScale;
                // We assume scale is uniform
                Assert.That(scale[0] == scale[1] && scale[1] == scale[2]);

                return scale[0];
            }
            set { transform.localScale = new Vector3(value, value, value); }
        }

        public void Collect()
        {
            GameObject.Destroy(this);
            OnCollected?.Invoke(_settings.collectibleValue);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.T_ship))
            {
                Collect();
            }
        }

        [Serializable]
        public class Settings
        {
            public int collectibleValue;
        }

        public class Factory : PlaceholderFactory<Coin>
        {
        }
    }
}