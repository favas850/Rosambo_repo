using System;
using System.Collections.Generic;
using Rosambo.GestureSystem.Controller;
using Rosambo.GestureSystem.Data;
using Rosambo.GestureSystem.Model;
using Rosambo.GestureSystem.View;
using UnityEngine;

namespace Rosambo.GestureSystem
{
    public class GestureSystem : MonoBehaviour
    {
        [SerializeField] private GestureView _gestureView;
        [SerializeField] private List<HandGestureData> _handGestures;

        private GestureController _gestureController;

        private void Start()
        {
            var model = CreateModel(_handGestures);
            _gestureController = new GestureController(_gestureView, model);
        }

        private HandGestureModel CreateModel(List<HandGestureData> handGestures)
        {
            HandGestureModel model = new HandGestureModel();
            foreach (var handGesture in handGestures)
            {
                model.Add(handGesture);
            }

            return model;
        }

        private void OnDestroy()
        {
            _gestureController.Dispose();
        }
    }
}