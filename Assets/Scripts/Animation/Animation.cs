using System.Collections.Generic;
using UnityEngine;

namespace Rosambo.Animation
{
    public class Animation : MonoBehaviour
    {
        #region Serialized Private Fields

        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField] private float _duration;

        [SerializeField, Tooltip("Values less than zero means infinite loop")]
        private int _loop = 1;

        [SerializeField] private List<AnimationEvent> _animationEvents;

        #endregion

        #region Private Fields

        private float _elapsedTime;
        private float _progress;
        private int _loopCount;
        private int _direction = 1;

        #endregion

        #region Public Properties

        public bool IsPlaying { get; private set; }

        #endregion

        #region Private Methods

        private void Start()
        {
            InitializeAnimation();
        }

        // Update is called once per frame
        private void Update()
        {
            if (IsPlaying == false) return;

            var complete = _direction > 0 ? _elapsedTime >= _duration : _elapsedTime <= 0;
            if (complete)
            {
                foreach (var animationEvent in _animationEvents)
                {
                    if (Mathf.Approximately(animationEvent.Progress, 1))
                    {
                        animationEvent.Event.Invoke();
                    }
                }

                if (_loop > _loopCount || _loop < 0)
                {
                    _loopCount++;
                    _elapsedTime = _direction > 0 ? 0 : _duration;
                }
                else if (_loop >= 0)
                {
                    _loopCount = 0;
                    IsPlaying = false;
                    return;
                }
            }

            var progress = _elapsedTime / _duration;
            var animationValue = _animationCurve.Evaluate(progress);
            UpdateAnimation(animationValue);
            foreach (var animationEvent in _animationEvents)
            {
                if (Mathf.Approximately(animationEvent.Progress, progress) ||
                    (animationEvent.Progress * _direction < (progress * _direction) &&
                     animationEvent.Progress * _direction > (_progress * _direction)))
                {
                    animationEvent.Event.Invoke();
                }
            }

            _progress = progress;
            _elapsedTime += _direction * Time.deltaTime;
        }

        #endregion

        #region Protected Methods

        protected virtual void InitializeAnimation()
        {
        }

        protected virtual void UpdateAnimation(float animationValue)
        {
        }

        #endregion

        #region Public Methods

        public void AddAnimationEvent(AnimationEvent animationEvent)
        {
            _animationEvents.Add(animationEvent);
        }

        public void RemoveAnimationEvent(AnimationEvent animationEvent)
        {
            if (_animationEvents.Contains(animationEvent))
                _animationEvents.Remove(animationEvent);
        }

        public void Play()
        {
            _direction = 1;
            IsPlaying = true;
        }

        public void Rewind()
        {
            _direction = -1;
            IsPlaying = true;
            _loopCount = 0;
        }

        public void Restart()
        {
            _elapsedTime = _progress = 0;
            _direction = 1;
            _loopCount = 0;
            IsPlaying = true;
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        #endregion
    }
}