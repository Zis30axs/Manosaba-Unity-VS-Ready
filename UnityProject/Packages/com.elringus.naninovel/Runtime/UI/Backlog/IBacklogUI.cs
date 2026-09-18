using System.Collections.Generic;

namespace Naninovel.UI
{
    /// <summary>
    /// Represents a set of UI elements used for managing backlog messages.
    /// </summary>
    public interface IBacklogUI : IManagedUI
    {
        /// <summary>
        /// Adds message to the log.
        /// </summary>
        /// <param name="text">Text of the message.</param>
        /// <param name="authorId">ID of the actor associated with the message or null.</param>
        /// <param name="rollbackSpot">Rollback spot associated with the message or null.</param>
        /// <param name="voicePath">Associated voice local resource path or null.</param>
        void AddMessage (LocalizableText text, string authorId = null, PlaybackSpot? rollbackSpot = null, string voicePath = null);
        /// <summary>
        /// Appends text to the last message of the log (if exists).
        /// </summary>
        /// <param name="text">Text to append to the last message.</param>
        /// <param name="voicePath">Associated voice local resource path or null.</param>
        void AppendMessage (LocalizableText text, string voicePath = null);
        /// <summary>
        /// Adds choice options to the log.
        /// </summary>
        /// <param name="choices">Options to add, in order.</param>
        void AddChoice (IReadOnlyList<BacklogChoice> choices);
        /// <summary>
        /// Removes all messages from the backlog.
        /// </summary>
        void Clear ();
    }
}
