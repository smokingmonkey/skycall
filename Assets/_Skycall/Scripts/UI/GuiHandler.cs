using System.Collections.Generic;
using _Skycall.Scripts.GameStateMachine;
using _Skycall.Scripts.Models;
using TMPro;
using UnityEngine;

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
        private float _elapsedTime;

        private GameStates _currentState;


        private void Start()
        {
            _guiElements = new List<GameObject>() { waitingToStartGui, playingGui, gameOverGui };
            GameManager.OnGameStateUpdate += OnUpdateGui;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateUpdate -= OnUpdateGui;
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
                UpdateElapsedTimeText(_elapsedTime);
            }
        }


        private void StartGui()
        {
            HideGui();
            _elapsedTime = 0f;
            waitingToStartGui.SetActive(true);
        }


        private void PlayingGui()
        {
            HideGui();
            UpdateElapsedTimeText(0f);
            playingGui.SetActive(true);
        }

        private void GameOverGui()
        {
            //HideGui();

            gameOverGui.SetActive(true);
        }

        void UpdateScore()
        {
        }

        void UpdateElapsedTimeText(float elapsedTime)
        {
            elapsedTimeText.SetText(elapsedTime.ToString("F1"));
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