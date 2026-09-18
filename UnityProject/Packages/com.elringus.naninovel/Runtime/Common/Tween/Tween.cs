using System;

namespace Naninovel
{
    /// <summary>
    /// Common properties of an animation that interpolates between two states aka in-between or tween.
    /// </summary>
    public readonly struct Tween : IEquatable<Tween>
    {
        /// <summary>
        /// The duration, in seconds, of the animation.
        /// </summary>
        public readonly float Duration;
        /// <summary>
        /// The easing function of the interpolation.
        /// </summary>
        public readonly EasingType Easing;
        /// <summary>
        /// Whether the <see cref="Duration"/> is affected by Unity's time scale.
        /// </summary>
        public readonly bool Scale;
        /// <summary>
        /// Whether to complete the animation (in case running) before starting next one.
        /// </summary>
        public readonly bool Complete;

        public Tween (float duration, EasingType easing = default, bool scale = true, bool complete = true)
        {
            Duration = duration;
            Easing = easing;
            Scale = scale;
            Complete = complete;
        }

        public bool Equals (Tween other)
        {
            return Duration.Equals(other.Duration) &&
                   Easing == other.Easing &&
                   Scale == other.Scale &&
                   Complete == other.Complete;
        }

        public override bool Equals (object obj)
        {
            return obj is Tween other && Equals(other);
        }

        public override int GetHashCode ()
        {
            return HashCode.Combine(Duration, (int)Easing, Scale, Complete);
        }
    }
}
