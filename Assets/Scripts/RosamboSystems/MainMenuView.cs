using Rosambo.Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rosambo.GestureSystem.View
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private TextMeshProUGUI _highScore;
        private GameSystem _gameSystem;

        private void Start()
        {
            _gameSystem = RosamboApplication.Instance.GameSystem;
            _playButton.onClick.AddListener(StartNewGame);
            _gameSystem.GameStarted += OnGameStarted;
            _gameSystem.GameOver += OnGameOver;
            SetHighScore(_gameSystem.HighScore);
        }

        private void SetHighScore(int highScore)
        {
            _highScore.SetText("High Score:{0}", highScore);
        }

        private void OnGameOver()
        {
            gameObject.SetActive(true);
            SetHighScore(_gameSystem.HighScore);
        }

        private void OnGameStarted()
        {
            gameObject.SetActive(false);
        }

        private void StartNewGame()
        {
            _gameSystem.StartNewGame();
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(StartNewGame);
            _gameSystem.GameStarted -= OnGameStarted;
            _gameSystem.GameOver -= OnGameOver;
        }
    }
}