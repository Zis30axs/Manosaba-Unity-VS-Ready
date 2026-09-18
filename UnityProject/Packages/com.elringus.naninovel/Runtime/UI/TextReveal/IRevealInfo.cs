using System;

namespace Naninovel.UI
{
    /// <summary>
    /// Provides information about text characters reveal state.
    /// </summary>
    public interface IRevealInfo
    {
        /// <summary>
        /// Notifies when reveal state is changed.
        /// </summary>
        event Action OnChange;

        /// <summary>
        /// Index of the last revealed character.
        /// </summary>
        int LastRevealedCharIndex { get; }
        /// <summary>
        /// Index of the last character before the text was appended.
        /// </summary>
        int LastAppendIndex { get; }

        /// <summary>
        /// Returns reveal progress (in 0.0 to 1.0 range) of the character with the specified index.
        /// </summary>
        /// <param name="charIndex">Index of the character to get the ratio for.</param>
        /// <param name="modifier">Duration modifier; use to modify relative ratio range.</param>
        float GetRevealRatio (int charIndex, float modifier = 1);
        /// <summary>
        /// Returns time (in seconds since game start) when the character was appended (added) to the text.
        /// </summary>
        float GetAppendTime (int charIndex);
    }
}
