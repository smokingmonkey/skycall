using System.Collections.Generic;
using _Skycall.Scripts.GameStateMachine;
using _Skycall.Scripts.Models;
using TMPro;
using UnityEngine;

namespace _Skycall.Scripts.UI
{
    public class SceneGuiHandler : MonoBehaviour
    {
        [SerializeField] private GameObject waitingToStartGui;
        [SerializeField] private GameObject playingGui;
        [SerializeField] private GameObject gameOverGui;

        private List<GameObject> _guiElements;

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

        private void StartGui()
        {
            HideGui();
            if (waitingToStartGui != null) waitingToStartGui.SetActive(true);
        }

        private void PlayingGui()
        {
            HideGui();
            if (playingGui != null) playingGui.SetActive(true);
        }

        private void GameOverGui()
        {
            HideGui();

            if (gameOverGui != null) gameOverGui.SetActive(true);
        }

        void HideGui()
        {
            if (_guiElements != null)
                foreach (var element in _guiElements)
                {
                    element.SetActive(false);
                }
        }
    }
}