using Rosambo.GestureSystem;
using Rosambo.Systems;
using UnityEngine;

namespace Rosambo.HandBehaviour
{
    public class HandController : MonoBehaviour
    {
        [SerializeField] private HandGestureBehaviour _handBehaviour;
        [SerializeField] private bool _player;

        private GameSystem _gameSystem;

        private void Start()
        {
            _gameSystem = RosamboApplication.Instance.GameSystem;
            _gameSystem.GestureSelected += OnGestureSelected;
            _handBehaviour.AnimationEnded += OnHandAnimationEnded;
            _gameSystem.GameStarted += OnGameStarted;
            _gameSystem.GameOver += OnGameOver;
            gameObject.SetActive(false);
        }

        private void OnGameOver()
        {
            gameObject.SetActive(false);
        }

        private void OnGameStarted()
        {
            gameObject.SetActive(true);
        }

        private void OnHandAnimationEnded()
        {
            _gameSystem.ShowResult();
        }

        private void OnGestureSelected(bool isPlayer, IGesture handGestureData)
        {
            if (isPlayer == _player)
            {
                _handBehaviour.SetAnimation(handGestureData.AnimationHash);
            }
        }

        private void OnDestroy()
        {
            _gameSystem.GestureSelected -= OnGestureSelected;
            _handBehaviour.AnimationEnded -= OnHandAnimationEnded;
            _gameSystem.GameStarted -= OnGameStarted;
            _gameSystem.GameOver -= OnGameOver;
        }
    }
}