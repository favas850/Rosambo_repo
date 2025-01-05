using System;
using UnityEngine;
using UnityEngine.Events;

namespace Rosambo.Animation
{
    [Serializable]
    public class AnimationEvent
    {
        [field: SerializeField, Range(0, 1)] public float Progress { get; private set; }

        [field: SerializeField] public UnityEvent Event { get; private set; }

        public AnimationEvent(float progress, UnityEvent unityEvent)
        {
            Progress = progress;
            Event = unityEvent;
        }
    }
}