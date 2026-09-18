using System;

namespace Naninovel.UI
{
    /// <summary>
    /// Represents a full-screen invisible UI panel, which blocks UI interaction and all (or a subset of) the input samplers while visible,
    /// but can hide itself and execute (if registered) `onClick` callback when user clicks the panel.
    /// </summary>
    public interface IClickThroughPanel : IManagedUI
    {
        /// <summary>
        /// Shows the panel, blocking the UI interaction and input sampling. 
        /// </summary>
        /// <param name="hideOnClick">Whether to hide the panel when clicked/interacted.</param>
        /// <param name="onClick">Action to invoke when clicked/interacted.</param>
        /// <param name="allowedSamplers">List of the input samplers to allow while the panel is visible.</param>
        void Show (bool hideOnClick, Action onClick, params string[] allowedSamplers);
        /// <summary>
        /// Hides the panel, unblocking the UI interaction and input sampling.
        /// </summary>
        void Hide ();
    }
}
