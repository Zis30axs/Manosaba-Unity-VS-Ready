using System;
using UnityEngine;

namespace Naninovel
{
    public readonly struct QuaternionTween : ITweenValue
    {
        public Tween Props { get; }

        private readonly Quaternion startValue;
        private readonly Quaternion targetValue;
        private readonly Action<Quaternion> onTween;

        public QuaternionTween (Quaternion from, Quaternion to, Tween props, Action<Quaternion> onTween)
        {
            startValue = from;
            targetValue = to;
            Props = props;
            this.onTween = onTween;
        }

        public void Tween (float ratio)
        {
            var newValue = Props.Easing == EasingType.Linear
                ? Quaternion.Lerp(startValue, targetValue, ratio)
                : Quaternion.Slerp(startValue, targetValue, ratio);
            onTween.Invoke(newValue);
        }
    }
}
