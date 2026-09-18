namespace Naninovel
{
    /// <summary>
    /// Represents <see cref="Command"/> parameter.
    /// </summary>
    public interface ICommandParameter
    {
        /// <summary>
        /// Whether the parameter has a value assigned (no matter raw or other).
        /// </summary>
        bool HasValue { get; }
        /// <summary>
        /// Whether value of the parameter contains script expressions and has to be evaluated at runtime.
        /// </summary>
        bool DynamicValue { get; }
        /// <summary>
        /// Raw scenario script value associated with the parameter; null when not associated.
        /// </summary>
        RawValue? RawValue { get; }
        /// <summary>
        /// Scenario script playback position (spot) associated with the parameter; null when not associated.
        /// </summary>
        PlaybackSpot? PlaybackSpot { get; }

        /// <summary>
        /// Attempts to parse and assign specified raw scenario script parameter value and playback spot.
        /// </summary>
        /// <param name="raw">The raw value to parse and assign; when null will reset (un-assign) value.</param>
        /// <param name="spot">Scenario script playback position (spot) associated with the parameter; null when none.</param>
        /// <param name="errors">Parse errors (if any) or null when parse succeeded.</param>
        void AssignRaw (RawValue? raw, PlaybackSpot? spot, out string errors);
    }
}
