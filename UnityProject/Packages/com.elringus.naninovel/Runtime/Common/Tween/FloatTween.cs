using System;

namespace Naninovel
{
    public readonly struct FloatTween : ITweenValue
    {
        public Tween Props { get; }

        private readonly float from;
        private readonly float to;
        private readonly Action<float> onTween;

        public FloatTween (float from, float to, Tween props, Action<float> onTween)
        {
            this.from = from;
            this.to = to;
            Props = props;
            this.onTween = onTween;
        }

        public void Tween (float ratio)
        {
            var newValue = Props.Easing.Tween(from, to, ratio);
            onTween.Invoke(newValue);
        }
    }
}
