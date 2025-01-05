using System;
using Rosambo.GestureSystem.Data;
using Rosambo.GestureSystem.Model;
using Rosambo.GestureSystem.View;
using Rosambo.Systems;
using Random = UnityEngine.Random;

namespace Rosambo.GestureSystem.Controller
{
    public class GestureController : IDisposable
    {
        private readonly GestureView _view;
        private readonly HandGestureModel _model;

        private readonly GameSystem _gameSystem;

        private IGesture _playerGesture;
        private bool _roundActive;

        public GestureController(GestureView view, HandGestureModel model)
        {
            _view = view;
            _model = model;
            _view.Populate(_model.HandGestures, OnGestureButtonPressed);
            _gameSystem = RosamboApplication.Instance.GameSystem;
            _gameSystem.RoundStarted += OnRoundStarted;
        }


        private void OnRoundEnded()
        {
            SetGesture();
        }

        private void SetGesture()
        {
            _roundActive = false;
            _gameSystem.RoundEnded -= OnRoundEnded;
            SetAIGesture();
            SetPlayerGesture();
            _view.Activate(false);
        }

        private void OnRoundStarted()
        {
            _playerGesture = null;
            _view.Activate(true);
            _gameSystem.RoundEnded += OnRoundEnded;
            _roundActive = true;
        }

        private void SetAIGesture()
        {
            var enemyGesture = Random.Range(0, _model.HandGestures.Count);
            _gameSystem.SetGesture(false, _model.GetGesture(enemyGesture));
        }

        private void SetPlayerGesture()
        {
            _playerGesture ??= _model.GetGesture(0);
            _gameSystem.SetGesture(true, _playerGesture);
        }

        private void OnGestureButtonPressed(GestureType gestureType)
        {
            if (_roundActive == false) return;
            _playerGesture = _model.GetGesture(gestureType);
            SetGesture();
        }


        public void Dispose()
        {
            _gameSystem.RoundStarted -= OnRoundStarted;
            _gameSystem.RoundEnded -= OnRoundEnded;
        }
    }
}