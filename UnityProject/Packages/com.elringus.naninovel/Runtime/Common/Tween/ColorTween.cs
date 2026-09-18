using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents available tween modes for <see cref="Color"/> values.
    /// </summary>
    public enum ColorTweenMode
    {
        All,
        RGB,
        Alpha
    }

    public readonly struct ColorTween : ITweenValue
    {
        public Tween Props { get; }

        private readonly Color from;
        private readonly Color to;
        private readonly ColorTweenMode mode;
        private readonly Action<Color> onTween;

        public ColorTween (Color from, Color to, Tween props, ColorTweenMode mode, Action<Color> onTween)
        {
            this.from = from;
            this.to = to;
            Props = props;
            this.mode = mode;
            this.mode = mode;
            this.onTween = onTween;
        }

        public void Tween (float ratio)
        {
            var newColor = default(Color);
            newColor.r = mode == ColorTweenMode.Alpha ? from.r : Props.Easing.Tween(from.r, to.r, ratio);
            newColor.g = mode == ColorTweenMode.Alpha ? from.g : Props.Easing.Tween(from.g, to.g, ratio);
            newColor.b = mode == ColorTweenMode.Alpha ? from.b : Props.Easing.Tween(from.b, to.b, ratio);
            newColor.a = mode == ColorTweenMode.RGB ? from.a : Props.Easing.Tween(from.a, to.a, ratio);
            onTween.Invoke(newColor);
        }
    }
}
