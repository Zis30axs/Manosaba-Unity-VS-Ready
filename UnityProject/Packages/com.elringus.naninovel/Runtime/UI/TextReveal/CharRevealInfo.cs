namespace Naninovel.UI
{
    /// <summary>
    /// Represents reveal state of text character.
    /// </summary>
    public readonly struct CharRevealInfo
    {
        /// <summary>
        /// Time (in seconds, since game start) when the character was revealed.
        /// </summary>
        public float StartTime { get; }
        /// <summary>
        /// Expected reveal duration of the character.
        /// </summary>
        public float Duration { get; }
        /// <summary>
        /// The reveal process should be cancelled or completed ASAP when requested.
        /// </summary>
        public AsyncToken Token { get; }

        public CharRevealInfo (float startTime, float duration, AsyncToken token)
        {
            StartTime = startTime;
            Duration = duration;
            Token = token;
        }
    }
}
