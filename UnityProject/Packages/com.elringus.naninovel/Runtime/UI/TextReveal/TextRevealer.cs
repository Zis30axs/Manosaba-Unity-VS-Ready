using System;
using UnityEngine;

namespace Naninovel.UI
{
    /// <summary>
    /// Uses <see cref="IRevealableText"/> to reveal text asynchronously with normalized speed.
    /// </summary>
    public class TextRevealer
    {
        private readonly IRevealableText text;
        private readonly Action<char, AsyncToken> handleCharRevealed;

        public TextRevealer (IRevealableText text, Action<char, AsyncToken> handleCharRevealed = null)
        {
            this.text = text;
            this.handleCharRevealed = handleCharRevealed;
        }

        /// <summary>
        /// Reveals text until <see cref="IRevealableText.RevealProgress"/> is 1.0 (or confirmation is required to proceed)
        /// with normalized speed based on <see cref="delay"/> (reveal speed won't depend on framerate).
        /// </summary>
        /// <param name="delay">Time (in seconds) for each character to reveal.</param>
        /// <param name="token">The reveal process will be canceled or completed ASAP when requested.</param>
        /// <returns>True when the text has been completely revealed; false when confirmation is required to proceed.</returns>
        public virtual async UniTask<bool> Reveal (float delay, AsyncToken token)
        {
            var lastRevealTime = Engine.Time.Time;
            var charsToReveal = 1;
            while (text.RevealProgress < 1)
            {
                if (!text.RevealNextChars(charsToReveal, delay, token)) return false;
                handleCharRevealed?.Invoke(text.GetLastRevealedChar(), token);
                lastRevealTime = Engine.Time.Time;
                int count = 0;
                while (token.EnsureNotCanceledOrCompleted() && count == 0)
                {
                    await AsyncUtils.WaitEndOfFrame(token);
                    charsToReveal = count = Mathf.FloorToInt((Engine.Time.Time - lastRevealTime) / delay);
                }
                if (token.Completed) text.RevealProgress = 1;
            }
            return true;
        }
    }
}
