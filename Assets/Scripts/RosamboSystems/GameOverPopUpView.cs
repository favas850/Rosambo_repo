using System;
using Rosambo.Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rosambo.GestureSystem.View
{
    public class GameOverPopUpView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;

        [SerializeField] private Button _restart;

        private void Start()
        {
            _restart.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            RosamboApplication.Instance.GameSystem.StartNewGame();
        }

        public void SetScore(int score)
        {
            _score.SetText("SCORE:{0}", score);
        }

        private void OnDestroy()
        {
            _restart.onClick.RemoveListener(RestartGame);
        }
    }
}