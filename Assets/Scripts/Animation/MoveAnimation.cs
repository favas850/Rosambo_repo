using UnityEngine;

namespace Rosambo.Animation
{
    public class MoveAnimation : Animation
    {
        #region Serialized Private Fields

        [SerializeField] private Vector3 _target;

        #endregion

        #region Private Fields

        private Vector3 _initialPosition;

        #endregion

        #region Overrides Of Animation

        protected override void InitializeAnimation()
        {
            base.InitializeAnimation();
            _initialPosition = transform.position;
        }

        protected override void UpdateAnimation(float animationValue)
        {
            base.UpdateAnimation(animationValue);
            transform.position = Vector3.Lerp(_initialPosition, _target, animationValue);
        }

        #endregion
    }
}