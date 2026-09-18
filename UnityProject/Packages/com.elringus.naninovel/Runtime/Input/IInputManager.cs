using System;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to manage player input processing.
    /// </summary>
    public interface IInputManager : IEngineService<InputConfiguration>
    {
        /// <summary>
        /// Event invoked when <see cref="InputMode"/> changes.
        /// </summary>
        event Action<InputMode> OnInputModeChanged;

        /// <summary>
        /// Whether to process input. Individual samplers can be
        /// "muted" via <see cref="IInputSampler.Enabled"/> property.
        /// </summary>
        bool ProcessInput { get; set; }
        /// <summary>
        /// Current input mode detected by last active input device type (mouse, gamepad, touchscreen, etc).
        /// </summary>
        InputMode InputMode { get; set; }

        /// <summary>
        /// Returns input sampler with the specified name.
        /// </summary>
        IInputSampler GetSampler (string bindingName);
        /// <summary>
        /// Specified UI will block input processing of all the samplers, except <paramref name="allowedSamplers"/> when visible.
        /// </summary>
        void AddBlockingUI (UI.IManagedUI ui, params string[] allowedSamplers);
        /// <summary>
        /// Specified UI will no longer block input processing when visible.
        /// </summary>
        void RemoveBlockingUI (UI.IManagedUI ui);
        /// <summary>
        /// Whether input sampler with the specified name is currently being sampled.
        /// </summary>
        bool IsSampling (string bindingName);
    }
}
