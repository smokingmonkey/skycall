using System;
using _Skycall.Scripts.Helpers;
using ModestTree;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Skycall.Scripts.Level.Collectibles
{
    public class CoinSpawner : MonoBehaviour
    {
        private LevelHelper _level;
        private Settings _settings;
        private Coin.Factory _coinFactory;
        private float _timeToNextSpawn;

        bool _started;

        [Inject]
        public void Construct(Settings settings, Coin.Factory coinFactory, LevelHelper level)
        {
            _settings = settings;
            _coinFactory = coinFactory;
            _level = level;
            _timeToNextSpawn = settings.maxSpawnTime;
        }

        [InjectOptional] bool _autoSpawn = true;


        public void StartGame()
        {
            if (_started) return;

            _started = true;
        }


        public void Stop()
        {
            if (!_started) return;

            _started = false;
        }


        public void Update()
        {
            if (_started || _autoSpawn)
            {
                _timeToNextSpawn -= Time.deltaTime;

                if (_timeToNextSpawn < 0)
                {
                    _timeToNextSpawn = _settings.maxSpawnTime;
                    SpawnNext();
                }
            }
        }

        public void SpawnNext()
        {
            var coin = _coinFactory.Create();
            coin.Position = GetRandomStartPosition(coin.Scale);
        }

        Vector3 GetRandomStartPosition(float scale)
        {
            var randW = Random.Range(0.0f, 1.0f);
            var randH = Random.Range(0.0f, 1.0f);
            return new Vector3(_level.Left + randW * _level.Width + scale, _level.Top - randH * _level.Height + scale,
                0);
        }


        [Serializable]
        public class Settings
        {
            public float collectibleValue;

            public int startingSpawns;
            public int maxSpawns;

            public float maxSpawnTime;
        }
    }
}