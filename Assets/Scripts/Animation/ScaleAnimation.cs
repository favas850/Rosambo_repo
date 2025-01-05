using UnityEngine;

namespace Rosambo.Animation
{
    public class ScaleAnimation : Animation
    {
        #region Serialized Private Fields

        [SerializeField] private Vector3 _target;

        #endregion

        #region Private Fields

        private Vector3 _initialScale;

        #endregion

        #region Overrides Of Animation

        protected override void InitializeAnimation()
        {
            base.InitializeAnimation();
            _initialScale = transform.localScale;
        }

        protected override void UpdateAnimation(float animationValue)
        {
            base.UpdateAnimation(animationValue);
            transform.localScale = Vector3.Lerp(_initialScale, _target, animationValue);
        }

        #endregion
    }
}