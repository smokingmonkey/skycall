using System;
using System.Collections.Generic;
using System.Linq;
using _Skycall.Scripts.Helpers;
using ModestTree;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Skycall.Scripts.Enemies.EnemyShip
{
    public class EnemyShipManager : MonoBehaviour
    {
        readonly List<EnemyShip> _enemyShips = new List<EnemyShip>();
        readonly Queue<EnemyShipAttributes> _cachedAttributes = new Queue<EnemyShipAttributes>();
        Settings _settings;
        EnemyShip.Factory _enemyShipFactory;
        LevelHelper _level;
   
        float _timeToNextSpawn;
        float _timeIntervalBetweenSpawns;
        bool _started;
   
        [InjectOptional]
        bool _autoSpawn = true;
   
        [Inject]
        public void Construct(
            Settings settings, EnemyShip.Factory enemyShipFactory, LevelHelper level)
        {
            _settings = settings;
            _timeIntervalBetweenSpawns = _settings.maxSpawnTime / (_settings.maxSpawns - _settings.startingSpawns);
            _timeToNextSpawn = _timeIntervalBetweenSpawns;
            _enemyShipFactory = enemyShipFactory;
            _level = level;
        }
   
        public IEnumerable<EnemyShip> EnemyShips
        {
            get { return _enemyShips; }
        }
   
        public void Start()
        {
            Assert.That(!_started);
            _started = true;
   
            ResetAll();
            GenerateRandomAttributes();
   
            for (int i = 0; i < _settings.startingSpawns; i++)
            {
                SpawnNext();
            }
        }
   
        // Generate the full list of size and speeds so that we can maintain an approximate average
        // this way we don't get wildly different difficulties each time the game is run
        // For example, if we just chose speed randomly each time we spawned an enemy, in some
        // cases that might result in the first set of enemy all going at max speed, or min speed
        void GenerateRandomAttributes()
        {
            Assert.That(_cachedAttributes.Count == 0);
   
            var speedTotal = 0.0f;
            var sizeTotal = 0.0f;
   
            for (int i = 0; i < _settings.maxSpawns; i++)
            {
                var sizePx = Random.Range(0.0f, 1.0f);
                var speed = Random.Range(_settings.minSpeed, _settings.maxSpeed);
   
                _cachedAttributes.Enqueue(new EnemyShipAttributes {
                    SizePx = sizePx,
                    InitialSpeed = speed
                });
   
                speedTotal += speed;
                sizeTotal += sizePx;
            }
   
            var desiredAverageSpeed = (_settings.minSpeed + _settings.maxSpeed) * 0.5f;
            var desiredAverageSize = 0.5f;
   
            var averageSize = sizeTotal / _settings.maxSpawns;
            var averageSpeed = speedTotal / _settings.maxSpawns;
   
            var speedScaleFactor = desiredAverageSpeed / averageSpeed;
            var sizeScaleFactor = desiredAverageSize / averageSize;
   
            foreach (var attributes in _cachedAttributes)
            {
                attributes.SizePx *= sizeScaleFactor;
                attributes.InitialSpeed *= speedScaleFactor;
            }
   
            Assert.That(Mathf.Approximately(_cachedAttributes.Average(x => x.InitialSpeed), desiredAverageSpeed));
            Assert.That(Mathf.Approximately(_cachedAttributes.Average(x => x.SizePx), desiredAverageSize));
        }
   
        void ResetAll()
        {
            foreach (var enemyShip in _enemyShips)
            {
                GameObject.Destroy(enemyShip.gameObject);
            }
   
            _enemyShips.Clear();
            _cachedAttributes.Clear();
        }
   
        public void Stop()
        {
            Assert.That(_started);
            _started = false;
        }
   
        public void FixedUpdate()
        {
            for (int i = 0; i < _enemyShips.Count; i++)
            {
                _enemyShips[i].FixedUpdateEnemy();
            }
        }
   
        public void Update()
        {
            for (int i = 0; i < _enemyShips.Count; i++)
            {
                _enemyShips[i].UpdateEnemy();
            }
   
            if (_started && _autoSpawn)
            {
                _timeToNextSpawn -= Time.deltaTime;
   
                if (_timeToNextSpawn < 0 && _enemyShips.Count < _settings.maxSpawns)
                {
                    _timeToNextSpawn = _timeIntervalBetweenSpawns;
                    SpawnNext();
                }
            }
        }
   
        public void SpawnNext()
        {
            var EnemyShip = _enemyShipFactory.Create();
   
            var attributes = _cachedAttributes.Dequeue();
   
            EnemyShip.Scale = Mathf.Lerp(_settings.minScale, _settings.maxScale, attributes.SizePx);
            EnemyShip.Mass = Mathf.Lerp(_settings.minMass, _settings.maxMass, attributes.SizePx);
            EnemyShip.Position = GetRandomStartPosition(EnemyShip.Scale);
            EnemyShip.Velocity = GetRandomDirection() * attributes.InitialSpeed;
   
            _enemyShips.Add(EnemyShip);
        }
   
        Vector3 GetRandomDirection()
        {
            var theta = Random.Range(0, Mathf.PI * 2.0f);
            return new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);
        }
   
        Vector3 GetRandomStartPosition(float scale)
        {
            var side = (Side)Random.Range(0, (int)Side.Count);
            var rand = Random.Range(0.0f, 1.0f);
   
            switch (side)
            {
                case Side.Top:
                {
                    return new Vector3(_level.Left + rand * _level.Width, _level.Top + scale, 0);
                }
                case Side.Bottom:
                {
                    return new Vector3(_level.Left + rand * _level.Width, _level.Bottom - scale, 0);
                }
                case Side.Right:
                {
                    return new Vector3(_level.Right + scale, _level.Bottom + rand * _level.Height, 0);
                }
                case Side.Left:
                {
                    return new Vector3(_level.Left - scale, _level.Bottom + rand * _level.Height, 0);
                }
            }
   
            throw Assert.CreateException();
        }
   
        enum Side
        {
            Top,
            Bottom,
            Left,
            Right,
            Count
        }
   
        [Serializable]
        public class Settings
        {
            public float minSpeed;
            public float maxSpeed;
   
            public float minScale;
            public float maxScale;
   
            public int startingSpawns;
            public int maxSpawns;
   
            public float maxSpawnTime;
   
            public float maxMass;
            public float minMass;
        }
   
        class EnemyShipAttributes
        {
            public float SizePx;
            public float InitialSpeed;
        }
    }
}
