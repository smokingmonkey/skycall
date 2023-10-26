using System.Collections.Generic;
using _Skycall.Scripts.GameStateMachine;
using _Skycall.Scripts.Models;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Skycall.Scripts.UI
{
    public class GuiHandler : MonoBehaviour
    {
        [SerializeField] private GameObject waitingToStartGui;
        [SerializeField] private GameObject playingGui;
        [SerializeField] private GameObject gameOverGui;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text elapsedTimeText;

        private List<GameObject> _guiElements;
        private GameManager _gameManager;
        private float _elapsedTime;

        private GameStates _currentState;

        [Inject]
        public void Construct(
            GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Start()
        {
            _guiElements = new List<GameObject>() { waitingToStartGui, playingGui, gameOverGui };
            _gameManager.OnGameStateUpdate += OnUpdateGui;
        }

        private void OnDisable()
        {
            _gameManager.OnGameStateUpdate -= OnUpdateGui;
        }

        private void OnUpdateGui(GameStates state)
        {
            _currentState = state;
            switch (_currentState)
            {
                case GameStates.WaitingToStart:
                {
                    StartGui();
                    break;
                }
                case GameStates.Playing:
                {
                    PlayingGui();
                    break;
                }
                case GameStates.GameOver:
                {
                    PlayingGui();
                    GameOverGui();
                    break;
                }
            }
        }

        private void Update()
        {
            if (_currentState == GameStates.Playing)
            {
                _elapsedTime += Time.deltaTime;
                elapsedTimeText.SetText(_elapsedTime.ToString());
            }
        }


        private void StartGui()
        {
            HideGui();
            _elapsedTime = 0f;
            elapsedTimeText.SetText(_elapsedTime.ToString());

            waitingToStartGui.SetActive(true);
        }


        private void PlayingGui()
        {
            HideGui();
            playingGui.SetActive(true);
        }

        private void GameOverGui()
        {
            HideGui();

            gameOverGui.SetActive(true);
        }

        void HideGui()
        {
            foreach (var element in _guiElements)
            {
                element.SetActive(false);
            }
        }
    }
}