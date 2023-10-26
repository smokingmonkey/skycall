using System;
using _Skycall.Scripts.Enemies.Asteroid;
using _Skycall.Scripts.Enemies.EnemyShip;
using _Skycall.Scripts.Level.Collectibles;
using _Skycall.Scripts.Models;
using _Skycall.Scripts.Player.Ship;
using ModestTree;
using UnityEngine;
using Zenject;

namespace _Skycall.Scripts.GameStateMachine
{
    public class GameManager : MonoBehaviour
    {
        public static event Action<GameStates> OnGameStateUpdate;

        private Ship _ship;
        private EnemyShipManager _enemyShipsManager;

        private AsteroidManager _asteroidManager;
        private CoinSpawner _coinSpawner;

        GameStates _state = GameStates.WaitingToStart;
        float _elapsedTime;

        [Inject]
        public void Construct(Ship ship, AsteroidManager asteroidManager,
            EnemyShipManager enemyShipManager, CoinSpawner coinSpawner)
        {
            _enemyShipsManager = enemyShipManager;

            _ship = ship;

            _asteroidManager = asteroidManager;

            _coinSpawner = coinSpawner;
        }

        public float ElapsedTime
        {
            get { return _elapsedTime; }
        }

        public GameStates State
        {
            get { return _state; }
        }

        public void Start()
        {
            Physics.gravity = Vector3.zero;

            Cursor.visible = false;

            _ship.OnCrashed += OnShipCrashed;
            OnGameStateUpdate?.Invoke(GameStates.WaitingToStart);
        }

        public void OnDisable()
        {
            _ship.OnCrashed -= OnShipCrashed;
        }

        public void Update()
        {
            switch (_state)
            {
                case GameStates.WaitingToStart:
                {
                    UpdateStarting();
                    break;
                }
                case GameStates.Playing:
                {
                    UpdatePlaying();
                    break;
                }
                case GameStates.GameOver:
                {
                    UpdateGameOver();
                    break;
                }
                default:
                {
                    Assert.That(false);
                    break;
                }
            }
        }

        void UpdateGameOver()
        {
            if (_state != GameStates.GameOver) return;

            if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
            {
                StartGame();
            }
        }

        void OnShipCrashed()
        {
            if (_state != GameStates.Playing) return;

            _state = GameStates.GameOver;
            OnGameStateUpdate?.Invoke(_state);

            _asteroidManager.Stop();
            _enemyShipsManager.Stop();
            _coinSpawner.Stop();
        }

        void UpdatePlaying()
        {
            if (_state != GameStates.Playing) return;
            _elapsedTime += Time.deltaTime;
        }

        void UpdateStarting()
        {
            if (_state != GameStates.WaitingToStart) return;

            if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
            {
                StartGame();
            }
        }

        void StartGame()
        {
            if (_state != GameStates.WaitingToStart && _state != GameStates.GameOver) return;

            _ship.Position = Vector3.zero;
            _elapsedTime = 0;
            _asteroidManager.StartGame();
            _enemyShipsManager.StartGame();
            _coinSpawner.StartGame();
            _ship.ChangeState(ShipStates.Moving);
            _state = GameStates.Playing;
            OnGameStateUpdate?.Invoke(_state);
        }
    }
}