namespace Naninovel
{
    /// <summary>
    /// Default implementation of the Time using <see cref="UnityEngine.Time"/>.
    /// </summary>
    public class UnityTime : ITime
    {
        public float Time => UnityEngine.Time.time;
        public float DeltaTime => UnityEngine.Time.deltaTime;
        public float UnscaledTime => UnityEngine.Time.unscaledTime;
        public float UnscaledDeltaTime => UnityEngine.Time.unscaledDeltaTime;
        public float TimeScale { get => UnityEngine.Time.timeScale; set => UnityEngine.Time.timeScale = value; }
        public int FrameCount => UnityEngine.Time.frameCount;
    }
}
