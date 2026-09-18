using UnityEngine;

namespace Naninovel
{
    public static class AspectMatcher
    {
        public static float Crop (Vector2 hostSize, Vector2 contentSize)
        {
            return Mathf.Max(hostSize.x / contentSize.x, hostSize.y / contentSize.y);
        }

        public static float Fit (Vector2 hostSize, Vector2 contentSize)
        {
            return Mathf.Min(hostSize.x / contentSize.x, hostSize.y / contentSize.y);
        }

        public static float Ratio (Vector2 hostSize, Vector2 contentSize, float ratio)
        {
            const float logBase = 2f;
            var logWidth = Mathf.Log(hostSize.x / contentSize.x, logBase);
            var logHeight = Mathf.Log(hostSize.y / contentSize.y, logBase);
            var logWeightedAverage = Mathf.Lerp(logWidth, logHeight, ratio);
            return Mathf.Pow(logBase, logWeightedAverage);
        }

        public static float Match (AspectMatchMode mode, Vector2 hostSize, Vector2 contentSize, float ratio) => mode switch {
            AspectMatchMode.Crop => Crop(hostSize, contentSize),
            AspectMatchMode.Fit => Fit(hostSize, contentSize),
            AspectMatchMode.Custom => Ratio(hostSize, contentSize, ratio),
            AspectMatchMode.Disable => 1,
            _ => throw new Error($"Unsupported match mode: '{mode}'.")
        };
    }
}
