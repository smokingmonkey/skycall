using _Skycall.Scripts.Enemies.Asteroid;
using _Skycall.Scripts.Enemies.EnemyShip;
using _Skycall.Scripts.Models;
using _Skycall.Scripts.Player.Ship;
using ModestTree;
using UnityEngine;
using Zenject;

namespace _Skycall.Scripts.GameStateMachine
{
    public class GameManager : MonoBehaviour
    {
        private Ship _ship;
        private EnemyShipManager _enemyShipsManager;

        private AsteroidManager _asteroidManager;

        GameStates _state = GameStates.WaitingToStart;
        float _elapsedTime;

        [Inject]
        public void Construct(Ship ship, AsteroidManager asteroidManager, EnemyShipManager enemyShipManager)
        {
            _enemyShipsManager = enemyShipManager;

            _ship = ship;

            _asteroidManager = asteroidManager;
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
            _asteroidManager.Stop();
            _enemyShipsManager.Stop();
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
            _asteroidManager.Start();
            _enemyShipsManager.Start();
            _ship.ChangeState(ShipStates.Moving);
            _state = GameStates.Playing;
        }
    }
}