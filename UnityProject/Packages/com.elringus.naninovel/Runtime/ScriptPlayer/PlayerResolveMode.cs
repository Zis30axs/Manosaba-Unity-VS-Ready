namespace Naninovel
{
    /// <summary>
    /// The mode in which script player handles missing playback spots when loading state.
    /// </summary>
    public enum PlayerResolveMode
    {
        /// <summary>
        /// Attempt to play from nearest next spot; in case no next spots, start from nearest previous one.
        /// </summary>
        Nearest,
        /// <summary>
        /// Start playing current script from start.
        /// </summary>
        Restart,
        /// <summary>
        /// Throw an error.
        /// </summary>
        Error
    }
}
