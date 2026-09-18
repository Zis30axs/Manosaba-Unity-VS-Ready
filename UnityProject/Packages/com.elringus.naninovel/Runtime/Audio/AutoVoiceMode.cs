namespace Naninovel
{
    /// <summary>
    /// Represent available methods to associate voice clips with @print commands,
    /// when using <see cref="AudioConfiguration.EnableAutoVoicing"/>.
    /// </summary>
    public enum AutoVoiceMode
    {
        /// <summary>
        /// Voice clips are associated by playback spots.
        /// </summary>
        PlaybackSpot,
        /// <summary>
        /// Voice clips are associated by localizable text IDs.
        /// </summary>
        TextId
    }
}
