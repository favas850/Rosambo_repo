using System.Collections;
using Rosambo.Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rosambo.GestureSystem.View
{
    public class GameHUDView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timer;

        [SerializeField] private GameOverPopUpView _gameOverPopUpView;
        [SerializeField] private GameInfoView _gameInfoView;
        [SerializeField] private GameResultView _gameResultView;
        [SerializeField] private Button _nextRound;
        [SerializeField] private Button _startGame;

        private GameSystem _gameSystem;
        private Coroutine _timerCoroutine;

        private void Start()
        {
            _gameSystem = RosamboApplication.Instance.GameSystem;
            _gameSystem.ResultReady += OnResultReady;
            _gameSystem.RoundStarted += OnRoundStarted;
            _gameSystem.GameOver += OnGameOver;
            _gameSystem.RoundEnded += OnRoundEnded;
            _gameSystem.GameStarted += OnGameStarted;
            _nextRound.onClick.AddListener(StartNewRound);
            _startGame.onClick.AddListener(StartNewRound);
        }

        private void OnRoundEnded()
        {
            StopCoroutine(_timerCoroutine);
            _timer.gameObject.SetActive(false);
        }

        private void StartNewRound()
        {
            _gameSystem.StartNextRound();
        }

        private void OnGameStarted()
        {
            _gameOverPopUpView.gameObject.SetActive(false);
            _gameInfoView.SetScore(_gameSystem.Score);
            _gameInfoView.SetLife(_gameSystem.LivesLeft);
            _startGame.gameObject.SetActive(true);
            _gameInfoView.gameObject.SetActive(true);
        }

        private void OnGameOver()
        {
            _gameOverPopUpView.gameObject.SetActive(true);
            _gameOverPopUpView.SetScore(_gameSystem.Score);
            _gameResultView.gameObject.SetActive(false);
            _gameInfoView.gameObject.SetActive(false);
        }

        private void OnRoundStarted()
        {
            _nextRound.gameObject.SetActive(false);
            _startGame.gameObject.SetActive(false);
            _timer.gameObject.SetActive(true);
            _gameOverPopUpView.gameObject.SetActive(false);
            _timerCoroutine = StartCoroutine(ShowRemainingTime(_gameSystem.RoundDuration));
            _gameResultView.gameObject.SetActive(false);
        }

        private void OnResultReady(GameResult gameResult)
        {
            if (_gameSystem.LivesLeft > 0)
                _nextRound.gameObject.SetActive(true);
            _gameInfoView.SetScore(_gameSystem.Score);
            _gameInfoView.SetLife(_gameSystem.LivesLeft);
            _gameResultView.SetResult(gameResult);
            _gameResultView.gameObject.SetActive(true);
        }

        private IEnumerator ShowRemainingTime(int timeRemaining)
        {
            _timer.SetText("{0}", timeRemaining);
            while (timeRemaining > 0)
            {
                yield return new WaitForSeconds(1);
                timeRemaining--;
                _timer.SetText("{0}", timeRemaining);
            }
        }

        private void OnDestroy()
        {
            _gameSystem.ResultReady -= OnResultReady;
            _gameSystem.RoundStarted -= OnRoundStarted;
            _gameSystem.GameOver -= OnGameOver;
            _gameSystem.GameStarted -= OnGameStarted;
            _gameSystem.RoundEnded -= OnRoundEnded;
            _nextRound.onClick.RemoveListener(StartNewRound);
            _startGame.onClick.RemoveListener(StartNewRound);
        }
    }
}