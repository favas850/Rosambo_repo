using System.Collections.Generic;
using Editor.Attributes;
using UnityEngine;

namespace Rosambo.GestureSystem.Data
{
    [CreateAssetMenu(fileName = "HandGestureData", menuName = "ScriptableObjects/HandGestureData")]
    public class HandGestureData : ScriptableObject
    {
        [SerializeField] private List<GestureType> _goodAgainst;
        [SerializeField] private AnimationClip _animationClip;
        [field: SerializeField, ReadOnly] public int AnimationHash { get; private set; }
        [field: SerializeField] public GestureType GestureType { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }

        public IReadOnlyList<GestureType> GoodAgainst => _goodAgainst;


        private void OnValidate()
        {
            AnimationHash = Animator.StringToHash(_animationClip.name);
        }
    }
}