using Rosambo.Animation;
using Rosambo.Systems;
using TMPro;
using UnityEngine;

namespace Rosambo.GestureSystem.View
{
    public class GameResultView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _result;

        [SerializeField] private Color _wonColor;
        [SerializeField] private Color _lostColor;

        [SerializeField] private ScaleAnimation _animation;

        public void SetResult(GameResult gameResult)
        {
            _animation.Restart();
            switch (gameResult)
            {
                case GameResult.Won:
                    _result.SetText("WON");
                    _result.color = _wonColor;
                    break;
                case GameResult.Lost:
                    _result.SetText("LOST");
                    _result.color = _lostColor;
                    break;
                case GameResult.Draw:
                    _result.SetText("DRAW");
                    _result.color = Color.white;
                    break;
            }
        }
    }
}