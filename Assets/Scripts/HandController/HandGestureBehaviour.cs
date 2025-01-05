using System;
using Rosambo.Animation;
using Rosambo.Systems;
using UnityEngine;
using UnityEngine.Events;
using AnimationEvent = Rosambo.Animation.AnimationEvent;

namespace Rosambo.HandBehaviour
{
    public class HandGestureBehaviour : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private RotationAnimation _rotationAnimation;
        [SerializeField] private AnimationClip _defaultAnimation;


        private AnimationEvent _onAnimationEnd;
        private AnimationEvent _onAnimationStart;

        private bool _animationStarted;
        private int _animationHash;

        public event Action AnimationEnded;

        private void Start()
        {
            RosamboApplication.Instance.GameSystem.RoundStarted += OnRoundStarted;
            RosamboApplication.Instance.GameSystem.GameOver += OnGameOver;
            RosamboApplication.Instance.GameSystem.GameStarted += OnGameStarted;

            var endUnityEvent = new UnityEvent();
            endUnityEvent.AddListener(OnAnimationEnd);

            var startUnityEvent = new UnityEvent();
            startUnityEvent.AddListener(OnAnimationStart);

            _onAnimationEnd = new AnimationEvent(0.5f, endUnityEvent);
            _onAnimationStart = new AnimationEvent(0, startUnityEvent);
        }

        private void OnGameStarted()
        {
            _rotationAnimation.RemoveAnimationEvent(_onAnimationEnd);
            _rotationAnimation.RemoveAnimationEvent(_onAnimationStart);
        }

        private void OnGameOver()
        {
            _rotationAnimation.Pause();
        }

        private void OnAnimationStart()
        {
            _animationStarted = true;
            _animator.Play(_animationHash);
        }

        private void OnRoundStarted()
        {
            _rotationAnimation.Play();
            _animator.Play(_defaultAnimation.name);
            _rotationAnimation.RemoveAnimationEvent(_onAnimationEnd);
            _rotationAnimation.RemoveAnimationEvent(_onAnimationStart);
        }

        /// <summary>
        /// Invoked when the rotation animation of the hand ends.
        /// </summary>
        private void OnAnimationEnd()
        {
            if (_animationStarted == false) return;
            _rotationAnimation.Pause();
            AnimationEnded?.Invoke();
            _animationStarted = false;
        }

        public void SetAnimation(int animationHash)
        {
            _animationHash = animationHash;
            _rotationAnimation.AddAnimationEvent(_onAnimationStart);
            _rotationAnimation.AddAnimationEvent(_onAnimationEnd);
        }

        private void OnDestroy()
        {
            RosamboApplication.Instance.GameSystem.RoundStarted -= OnRoundStarted;
            RosamboApplication.Instance.GameSystem.GameOver -= OnGameOver;
            RosamboApplication.Instance.GameSystem.GameStarted -= OnGameStarted;
        }
    }
}