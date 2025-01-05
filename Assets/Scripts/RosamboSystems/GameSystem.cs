using System;
using Rosambo.GestureSystem;
using Rosambo.Systems.TimerSystem;
using UnityEngine;

namespace Rosambo.Systems
{
    public class GameSystem : MonoBehaviour
    {
        public const string HighScoreKey = "high_score";

        #region Public Field

        #region Serilized

        [field: SerializeField, Tooltip("How long a round will Last in seconds")]
        public int RoundDuration { get; private set; }

        #endregion

        #region Non Serilized

        public int Score { get; private set; }
        public int HighScore { get; private set; }
        public int LivesLeft { get; private set; }

        #endregion

        #endregion

        #region Private Field

        #region Serilized

        [SerializeField] private int _numberOfLives;

        #endregion


        #region Non Serilized

        private IGesture _aiGesture;
        private ITimerService _timerService;
        private Timer _timer = new Timer();
        public GameResult _lastResult;

        #endregion

        #endregion

        #region Events

        public event Action<bool, IGesture> GestureSelected;

        public event Action RoundStarted;
        public event Action RoundEnded;

        public event Action<GameResult> ResultReady;

        public event Action GameStarted;
        public event Action GameOver;

        #endregion

        #region Public Method

        public void SetGesture(bool isPlayer, IGesture handGesture)
        {
            if (isPlayer)
            {
                CalculateResult(handGesture);
                EndRound();
                if (_timer.Finished == false)
                    _timerService.RemoveTimer(_timer);
            }
            else
            {
                _aiGesture = handGesture;
            }

            GestureSelected?.Invoke(isPlayer, handGesture);
        }

        public void StartNextRound()
        {
            _timer.ResetTo(RoundDuration);
            _timerService.AddTimer(_timer);
            RoundStarted?.Invoke();
        }

        public void StartNewGame()
        {
            LivesLeft = _numberOfLives;
            Score = 0;
            GameStarted?.Invoke();
        }

        public void ShowResult()
        {
            ResultReady?.Invoke(_lastResult);
            if (LivesLeft == 0)
            {
                if (Score > HighScore)
                {
                    PlayerPrefs.SetInt(HighScoreKey, Score);
                    HighScore = Score;
                }

                GameOver?.Invoke();
            }
        }

        #endregion

        #region Private Method

        private void Awake()
        {
            HighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        }

        private void Start()
        {
            RosamboApplication.ServiceLocator.RegisterForServiceChange<ITimerService>(OnTimerServiceAdded);
            _timer.Expired += EndRound;
        }

        private void EndRound()
        {
            RoundEnded?.Invoke();
        }

        private void OnTimerServiceAdded(IService service)
        {
            if (service is ITimerService timerService)
            {
                _timerService = timerService;
            }
        }

        private void CalculateResult(IGesture handGesture)
        {
            if (handGesture.GestureType == _aiGesture.GestureType)
            {
                _lastResult = GameResult.Draw;
                return;
            }

            var didAIWin = _aiGesture.CanWinAgainst(handGesture.GestureType);
            _lastResult = didAIWin ? GameResult.Lost : GameResult.Won;

            if (didAIWin)
            {
                LivesLeft--;
            }
            else
            {
                Score++;
            }
        }

        private void OnDestroy()
        {
            RosamboApplication.ServiceLocator.UnRegisterForServiceChange(OnTimerServiceAdded);
            _timer.Expired -= EndRound;
        }

        #endregion
    }

    public class Temp
    {
        #region Public Field

        #region Serilized

        #endregion

        #region Non Serilized

        #endregion

        #endregion

        #region Private Field

        #region Serilized

        #endregion

        #region Non Serilized

        #endregion

        #endregion

        #region Events

        #endregion

        #region Public Method

        #endregion

        #region Private Method

        #endregion

        #region Protected Method

        #endregion
    }
}