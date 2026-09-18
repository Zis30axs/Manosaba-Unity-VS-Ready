using System;
using System.Threading;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to sample player input.
    /// </summary>
    public interface IInputSampler
    {
        /// <summary>
        /// Invoked when input activation started.
        /// </summary>
        event Action OnStart;
        /// <summary>
        /// Invoked when input activation ended.
        /// </summary>
        event Action OnEnd;
        /// <summary>
        /// Invoked when activation value changes.
        /// </summary>
        event Action<float> OnChange;

        /// <summary>
        /// Assigned input binding.
        /// </summary>
        InputBinding Binding { get; }
        /// <summary>
        /// Whether input should be sampled; can be used to temporary "mute" specific inputs.
        /// </summary>
        bool Enabled { get; set; }
        /// <summary>
        /// Whether input is being activated.
        /// </summary>
        bool Active { get; }
        /// <summary>
        /// Current value (activation force) of the input; zero means the input is not active.
        /// </summary>
        float Value { get; }
        /// <summary>
        /// Whether input started activation during current frame.
        /// </summary>
        bool StartedDuringFrame { get; }
        /// <summary>
        /// Whether input ended activation during current frame.
        /// </summary>
        bool EndedDuringFrame { get; }

        /// <summary>
        /// Activates the input.
        /// </summary>
        /// <param name="value">Value (force) of the activation, in 0.0 to 1.0 range.</param>
        void Activate (float value);
        /// <summary>
        /// When any of the specified game objects are clicked or touched, input event will trigger.
        /// </summary>
        void AddObjectTrigger (GameObject obj);
        /// <summary>
        /// Removes object added with <see cref="AddObjectTrigger"/>.
        /// </summary>
        void RemoveObjectTrigger (GameObject obj);
        /// <summary>
        /// Returned token will be canceled on next input start activation.
        /// </summary>
        CancellationToken GetNext ();
        /// <summary>
        /// Returned token will be canceled on next input start activation, and no other
        /// handlers are notified about the event occurence, unless specified token is cancelled.
        /// </summary>
        /// <remarks>
        /// Intercepting requests are stack-based: when event occurs, only last callee gets notified.
        /// If specified token is cancelled when processing the event, returned token won't be cancelled
        /// and next intercept request in the stack is processed instead, if any, otherwise the event is handled as usual.
        /// </remarks>
        /// <param name="token">The interception is ignored if the token is cancelled when the event occurs.</param>
        CancellationToken InterceptNext (CancellationToken token = default);
    }
}
