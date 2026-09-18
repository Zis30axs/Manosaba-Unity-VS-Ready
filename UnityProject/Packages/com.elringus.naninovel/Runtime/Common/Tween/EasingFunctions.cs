using UnityEngine;

namespace Naninovel
{
    public static class EasingFunctions
    {
        public static float Tween (this EasingType type, float from, float to, float ratio) => type switch {
            EasingType.Linear => Linear(from, to, ratio),
            EasingType.SmoothStep => SmoothStep(from, to, ratio),
            EasingType.Spring => Spring(from, to, ratio),
            EasingType.EaseInQuad => EaseInQuad(from, to, ratio),
            EasingType.EaseOutQuad => EaseOutQuad(from, to, ratio),
            EasingType.EaseInOutQuad => EaseInOutQuad(from, to, ratio),
            EasingType.EaseInCubic => EaseInCubic(from, to, ratio),
            EasingType.EaseOutCubic => EaseOutCubic(from, to, ratio),
            EasingType.EaseInOutCubic => EaseInOutCubic(from, to, ratio),
            EasingType.EaseInQuart => EaseInQuart(from, to, ratio),
            EasingType.EaseOutQuart => EaseOutQuart(from, to, ratio),
            EasingType.EaseInOutQuart => EaseInOutQuart(from, to, ratio),
            EasingType.EaseInQuint => EaseInQuint(from, to, ratio),
            EasingType.EaseOutQuint => EaseOutQuint(from, to, ratio),
            EasingType.EaseInOutQuint => EaseInOutQuint(from, to, ratio),
            EasingType.EaseInSine => EaseInSine(from, to, ratio),
            EasingType.EaseOutSine => EaseOutSine(from, to, ratio),
            EasingType.EaseInOutSine => EaseInOutSine(from, to, ratio),
            EasingType.EaseInExpo => EaseInExpo(from, to, ratio),
            EasingType.EaseOutExpo => EaseOutExpo(from, to, ratio),
            EasingType.EaseInOutExpo => EaseInOutExpo(from, to, ratio),
            EasingType.EaseInCirc => EaseInCirc(from, to, ratio),
            EasingType.EaseOutCirc => EaseOutCirc(from, to, ratio),
            EasingType.EaseInOutCirc => EaseInOutCirc(from, to, ratio),
            EasingType.EaseInBounce => EaseInBounce(from, to, ratio),
            EasingType.EaseOutBounce => EaseOutBounce(from, to, ratio),
            EasingType.EaseInOutBounce => EaseInOutBounce(from, to, ratio),
            EasingType.EaseInBack => EaseInBack(from, to, ratio),
            EasingType.EaseOutBack => EaseOutBack(from, to, ratio),
            EasingType.EaseInOutBack => EaseInOutBack(from, to, ratio),
            EasingType.EaseInElastic => EaseInElastic(from, to, ratio),
            EasingType.EaseOutElastic => EaseOutElastic(from, to, ratio),
            EasingType.EaseInOutElastic => EaseInOutElastic(from, to, ratio),
            _ => throw new Error($"Unsupported easing type: {type}")
        };

        public static float Linear (float from, float to, float ratio)
        {
            return Mathf.Lerp(from, to, ratio);
        }

        public static float SmoothStep (float from, float to, float ratio)
        {
            return Mathf.SmoothStep(from, to, ratio);
        }

        public static float Spring (float from, float to, float ratio)
        {
            ratio = Mathf.Clamp01(ratio);
            ratio = (Mathf.Sin(ratio * Mathf.PI * (.2f + 2.5f * ratio * ratio * ratio)) * Mathf.Pow(1f - ratio, 2.2f) + ratio) * (1f + 1.2f * (1f - ratio));
            return from + (to - from) * ratio;
        }

        public static float EaseInQuad (float from, float to, float ratio)
        {
            to -= from;
            return to * ratio * ratio + from;
        }

        public static float EaseOutQuad (float from, float to, float ratio)
        {
            to -= from;
            return -to * ratio * (ratio - 2) + from;
        }

        public static float EaseInOutQuad (float from, float to, float ratio)
        {
            ratio /= .5f;
            to -= from;
            if (ratio < 1) return to * .5f * ratio * ratio + from;
            ratio--;
            return -to * .5f * (ratio * (ratio - 2) - 1) + from;
        }

        public static float EaseInCubic (float from, float to, float ratio)
        {
            to -= from;
            return to * ratio * ratio * ratio + from;
        }

        public static float EaseOutCubic (float from, float to, float ratio)
        {
            ratio--;
            to -= from;
            return to * (ratio * ratio * ratio + 1) + from;
        }

        public static float EaseInOutCubic (float from, float to, float ratio)
        {
            ratio /= .5f;
            to -= from;
            if (ratio < 1) return to * .5f * ratio * ratio * ratio + from;
            ratio -= 2;
            return to * .5f * (ratio * ratio * ratio + 2) + from;
        }

        public static float EaseInQuart (float from, float to, float ratio)
        {
            to -= from;
            return to * ratio * ratio * ratio * ratio + from;
        }

        public static float EaseOutQuart (float from, float to, float ratio)
        {
            ratio--;
            to -= from;
            return -to * (ratio * ratio * ratio * ratio - 1) + from;
        }

        public static float EaseInOutQuart (float from, float to, float ratio)
        {
            ratio /= .5f;
            to -= from;
            if (ratio < 1) return to * .5f * ratio * ratio * ratio * ratio + from;
            ratio -= 2;
            return -to * .5f * (ratio * ratio * ratio * ratio - 2) + from;
        }

        public static float EaseInQuint (float from, float to, float ratio)
        {
            to -= from;
            return to * ratio * ratio * ratio * ratio * ratio + from;
        }

        public static float EaseOutQuint (float from, float to, float ratio)
        {
            ratio--;
            to -= from;
            return to * (ratio * ratio * ratio * ratio * ratio + 1) + from;
        }

        public static float EaseInOutQuint (float from, float to, float ratio)
        {
            ratio /= .5f;
            to -= from;
            if (ratio < 1) return to * .5f * ratio * ratio * ratio * ratio * ratio + from;
            ratio -= 2;
            return to * .5f * (ratio * ratio * ratio * ratio * ratio + 2) + from;
        }

        public static float EaseInSine (float from, float to, float ratio)
        {
            to -= from;
            return -to * Mathf.Cos(ratio * (Mathf.PI * .5f)) + to + from;
        }

        public static float EaseOutSine (float from, float to, float ratio)
        {
            to -= from;
            return to * Mathf.Sin(ratio * (Mathf.PI * .5f)) + from;
        }

        public static float EaseInOutSine (float from, float to, float ratio)
        {
            to -= from;
            return -to * .5f * (Mathf.Cos(Mathf.PI * ratio) - 1) + from;
        }

        public static float EaseInExpo (float from, float to, float ratio)
        {
            to -= from;
            return to * Mathf.Pow(2, 10 * (ratio - 1)) + from;
        }

        public static float EaseOutExpo (float from, float to, float ratio)
        {
            to -= from;
            return to * (-Mathf.Pow(2, -10 * ratio) + 1) + from;
        }

        public static float EaseInOutExpo (float from, float to, float ratio)
        {
            ratio /= .5f;
            to -= from;
            if (ratio < 1) return to * .5f * Mathf.Pow(2, 10 * (ratio - 1)) + from;
            ratio--;
            return to * .5f * (-Mathf.Pow(2, -10 * ratio) + 2) + from;
        }

        public static float EaseInCirc (float from, float to, float ratio)
        {
            to -= from;
            return -to * (Mathf.Sqrt(1 - ratio * ratio) - 1) + from;
        }

        public static float EaseOutCirc (float from, float to, float ratio)
        {
            ratio--;
            to -= from;
            return to * Mathf.Sqrt(1 - ratio * ratio) + from;
        }

        public static float EaseInOutCirc (float from, float to, float ratio)
        {
            ratio /= .5f;
            to -= from;
            if (ratio < 1) return -to * .5f * (Mathf.Sqrt(1 - ratio * ratio) - 1) + from;
            ratio -= 2;
            return to * .5f * (Mathf.Sqrt(1 - ratio * ratio) + 1) + from;
        }

        public static float EaseInBounce (float from, float to, float ratio)
        {
            to -= from;
            const float d = 1f;
            return to - EaseOutBounce(0, to, d - ratio) + from;
        }

        public static float EaseOutBounce (float from, float to, float ratio)
        {
            ratio /= 1f;
            to -= from;
            if (ratio < 1 / 2.75f)
            {
                return to * (7.5625f * ratio * ratio) + from;
            }
            if (ratio < 2 / 2.75f)
            {
                ratio -= 1.5f / 2.75f;
                return to * (7.5625f * ratio * ratio + .75f) + from;
            }
            if (ratio < 2.5 / 2.75)
            {
                ratio -= 2.25f / 2.75f;
                return to * (7.5625f * ratio * ratio + .9375f) + from;
            }
            ratio -= 2.625f / 2.75f;
            return to * (7.5625f * ratio * ratio + .984375f) + from;
        }

        public static float EaseInOutBounce (float from, float to, float ratio)
        {
            to -= from;
            const float d = 1f;
            if (ratio < d * .5f) return EaseInBounce(0, to, ratio * 2) * .5f + from;
            return EaseOutBounce(0, to, ratio * 2 - d) * .5f + to * .5f + from;
        }

        public static float EaseInBack (float from, float to, float ratio)
        {
            to -= from;
            ratio /= 1;
            const float s = 1.70158f;
            return to * ratio * ratio * ((s + 1) * ratio - s) + from;
        }

        public static float EaseOutBack (float from, float to, float ratio)
        {
            const float s = 1.70158f;
            to -= from;
            ratio -= 1;
            return to * (ratio * ratio * ((s + 1) * ratio + s) + 1) + from;
        }

        public static float EaseInOutBack (float from, float to, float ratio)
        {
            float s = 1.70158f;
            to -= from;
            ratio /= .5f;
            if (ratio < 1)
            {
                s *= 1.525f;
                return to * .5f * (ratio * ratio * ((s + 1) * ratio - s)) + from;
            }
            ratio -= 2;
            s *= 1.525f;
            return to * .5f * (ratio * ratio * ((s + 1) * ratio + s) + 2) + from;
        }

        public static float EaseInElastic (float from, float to, float ratio)
        {
            to -= from;

            const float d = 1f;
            const float p = d * .3f;
            float s;
            float a = 0;

            if (ratio == 0) return from;

            if (Mathf.Approximately(ratio /= d, 1)) return from + to;

            if (a == 0f || a < Mathf.Abs(to))
            {
                a = to;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(to / a);
            }

            return -(a * Mathf.Pow(2, 10 * (ratio -= 1)) * Mathf.Sin((ratio * d - s) * (2 * Mathf.PI) / p)) + from;
        }

        public static float EaseOutElastic (float from, float to, float ratio)
        {
            to -= from;

            const float d = 1f;
            const float p = d * .3f;
            float s;
            float a = 0;

            if (ratio == 0) return from;

            if (Mathf.Approximately(ratio /= d, 1)) return from + to;

            if (a == 0f || a < Mathf.Abs(to))
            {
                a = to;
                s = p * .25f;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(to / a);
            }

            return a * Mathf.Pow(2, -10 * ratio) * Mathf.Sin((ratio * d - s) * (2 * Mathf.PI) / p) + to + from;
        }

        public static float EaseInOutElastic (float from, float to, float ratio)
        {
            to -= from;

            const float d = 1f;
            const float p = d * .3f;
            float s;
            float a = 0;

            if (ratio == 0) return from;

            if (Mathf.Approximately(ratio /= d * .5f, 2)) return from + to;

            if (a == 0f || a < Mathf.Abs(to))
            {
                a = to;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(to / a);
            }

            if (ratio < 1) return -.5f * (a * Mathf.Pow(2, 10 * (ratio -= 1)) * Mathf.Sin((ratio * d - s) * (2 * Mathf.PI) / p)) + from;
            return a * Mathf.Pow(2, -10 * (ratio -= 1)) * Mathf.Sin((ratio * d - s) * (2 * Mathf.PI) / p) * .5f + to + from;
        }
    }
}
