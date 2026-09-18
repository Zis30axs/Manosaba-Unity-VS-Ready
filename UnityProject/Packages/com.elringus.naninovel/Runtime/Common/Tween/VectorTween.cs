using System;
using UnityEngine;

namespace Naninovel
{
    public readonly struct VectorTween : ITweenValue
    {
        public Tween Props { get; }

        private readonly Vector3 from;
        private readonly Vector3 to;
        private readonly Action<Vector3> onTween;

        public VectorTween (Vector3 from, Vector3 to, Tween props, Action<Vector3> onTween)
        {
            this.from = from;
            this.to = to;
            Props = props;
            this.onTween = onTween;
        }

        public void Tween (float ratio)
        {
            var newValue = new Vector3(
                Props.Easing.Tween(from.x, to.x, ratio),
                Props.Easing.Tween(from.y, to.y, ratio),
                Props.Easing.Tween(from.z, to.z, ratio)
            );
            onTween.Invoke(newValue);
        }
    }
}
