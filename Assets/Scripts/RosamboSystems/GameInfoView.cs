using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rosambo.GestureSystem.View
{
    public class GameInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;

        [SerializeField] GameObject _life;

        [SerializeField] private RectTransform _lifeContainer;

        private List<GameObject> _instantiatedLives = new List<GameObject>();

        public void SetScore(int score)
        {
            _score.SetText("SCORE:{0}", score);
        }

        public void SetLife(int life)
        {
            int i = 0;
            for (; i < _instantiatedLives.Count; i++)
            {
                _instantiatedLives[i].SetActive(i < life);
            }

            for (; i < life; i++)
            {
                var lifeIcon = Instantiate(_life, _lifeContainer);
                _instantiatedLives.Add(lifeIcon);
            }
        }
    }
}