using System;
using ModestTree;
using UnityEngine;
using Zenject;

namespace _Skycall.Scripts.Level.Collectibles
{
    public class Coin : ScorerBase, ICollectible
    {
        private Settings _settings;

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
            GameObject.Destroy(this.gameObject, settings.duration);
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
            ScorerBase.OnScoreHandler(_settings.collectibleValue);
            GameObject.Destroy(this.gameObject);
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
            public int duration;
        }

        public class Factory : PlaceholderFactory<Coin>
        {
        }
    }
}