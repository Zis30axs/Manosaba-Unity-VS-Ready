using UnityEngine;

namespace Naninovel.UI
{
    /// <summary>
    /// Base class for text reveal effects.
    /// </summary>
    public abstract class TextRevealEffect : MonoBehaviour
    {
        /// <summary>
        /// Text component to apply the reveal effect for.
        /// </summary>
        protected virtual RevealableText Text => text;
        /// <summary>
        /// Current state of <see cref="Text"/> reveal process.
        /// </summary>
        protected virtual IRevealInfo Info => Text.RevealInfo;

        [Tooltip("Text component to apply the reveal effect for.")]
        [SerializeField] private RevealableText text;

        private void Awake ()
        {
            this.AssertRequiredObjects(Text);
        }
    }
}
