using System;
using Rosambo.GestureSystem.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Rosambo.GestureSystem.View
{
    public class GestureButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;

        private GestureType _gestureType;
        private Action<GestureType> _gestureButtonClickedDelegate;

        private void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _gestureButtonClickedDelegate?.Invoke(_gestureType);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void SetButton(HandGestureData handGestureData, Action<GestureType> gestureButtonClicked)
        {
            _gestureButtonClickedDelegate = gestureButtonClicked;
            _gestureType = handGestureData.GestureType;
            _icon.sprite = handGestureData.Icon;
        }
    }
}