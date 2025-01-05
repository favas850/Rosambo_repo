using UnityEngine;

namespace Rosambo.Animation
{
    public class RotationAnimation : Animation
    {
        #region Serialized Private Fields

        [SerializeField] private Quaternion _target;

        #endregion

        #region Private Fields

        private Quaternion _initialRotation;

        #endregion

        #region Overrides Of Animation

        protected override void InitializeAnimation()
        {
            base.InitializeAnimation();
            _initialRotation = transform.rotation;
        }

        protected override void UpdateAnimation(float animationValue)
        {
            base.UpdateAnimation(animationValue);
            transform.rotation = Quaternion.Slerp(_initialRotation, _target, animationValue);
        }

        #endregion
    }
}