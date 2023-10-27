using System.Collections.Generic;
using _Skycall.Scripts.GameStateMachine;
using _Skycall.Scripts.Level.Collectibles;
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
        [SerializeField] private TMP_Text finalScoreText;


        private List<GameObject> _guiElements;
        private float _score;

        private GameStates _currentState;


        private void Start()
        {
            _guiElements = new List<GameObject>() { waitingToStartGui, playingGui, gameOverGui };
            GameManager.OnGameStateUpdate += OnUpdateGui;
            ScorerBase.OnScore += UpdateScore;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateUpdate -= OnUpdateGui;
            ScorerBase.OnScore -= UpdateScore;
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
                    ResetScore();

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
            ResetScore();
            waitingToStartGui.SetActive(true);
        }


        private void PlayingGui()
        {
            HideGui();
            playingGui.SetActive(true);
        }

        private void GameOverGui()
        {
            //HideGui();
            finalScoreText.SetText(_score.ToString());
            gameOverGui.SetActive(true);
        }

        void UpdateScore(int value)
        {
            _score += value;
            scoreText.SetText(_score.ToString());
        }

        void ResetScore()
        {
            _score = 0;
            scoreText.SetText(_score.ToString());
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