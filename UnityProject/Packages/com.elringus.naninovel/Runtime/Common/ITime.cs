namespace Naninovel
{
    /// <summary>
    /// An interface to time APIs used by Naninovel.
    /// </summary>
    public interface ITime
    {
        /// <inheritdoc cref="UnityEngine.Time.time"/>
        float Time { get; }
        /// <inheritdoc cref="UnityEngine.Time.deltaTime"/>
        float DeltaTime { get; }
        /// <inheritdoc cref="UnityEngine.Time.unscaledTime"/>
        float UnscaledTime { get; }
        /// <inheritdoc cref="UnityEngine.Time.unscaledDeltaTime"/>
        float UnscaledDeltaTime { get; }
        /// <inheritdoc cref="UnityEngine.Time.timeScale"/>
        float TimeScale { get; set; }
        /// <inheritdoc cref="UnityEngine.Time.frameCount"/>
        int FrameCount { get; }
    }
}
