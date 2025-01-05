using System;
using System.Collections.Generic;
using Rosambo.GestureSystem.Data;
using Rosambo.GestureSystem.Model;
using UnityEngine;

namespace Rosambo.GestureSystem.View
{
    public class GestureView : MonoBehaviour
    {
        [SerializeField] private GestureButton _gestureButtonTemplate;
        [SerializeField] private RectTransform _content;
        [SerializeField] private Animation.Animation _animation;

        private List<GestureButton> _gestureButtons = new List<GestureButton>();


        public void Populate(IReadOnlyList<HandGesture> gestures, Action<GestureType> gestureButtonClicked)
        {
            foreach (var handGesture in gestures)
            {
                var gestureButton = Instantiate(_gestureButtonTemplate, _content);
                gestureButton.SetButton(handGesture.Data, gestureButtonClicked);
                _gestureButtons.Add(gestureButton);
            }
        }

        public void Activate(bool activate)
        {
            if (activate)
            {
                _animation.Play();
            }

            else
            {
                _animation.Rewind();
            }
        }
    }
}