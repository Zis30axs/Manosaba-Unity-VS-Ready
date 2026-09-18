
namespace Naninovel.UI
{
    /// <summary>
    /// Represents a set of UI elements used to block user input until it confirms or cancels.
    /// </summary>
    public interface IConfirmationUI : IManagedUI
    {
        /// <summary>
        /// Blocks user input and shows the confirmation dialogue with the specified message.
        /// The async returns when user confirms or cancels.
        /// </summary>
        /// <param name="message">The confirmation message.</param>
        /// <returns>Whether the user confirmed.</returns>
        UniTask<bool> Confirm (string message);
        /// <summary>
        /// Blocks user input and shows notification dialogue with the specified message.
        /// The async returns when user closes the message.
        /// </summary>
        /// <param name="message">The notification message.</param>
        UniTask Notify (string message);
    }
}
