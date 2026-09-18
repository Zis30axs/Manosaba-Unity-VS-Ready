using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Naninovel.UI
{
    /// <summary>
    /// Wrapper over <see cref="NaninovelTMProText"/> with support for gradual text reveal.
    /// </summary>
    public class RevealableText : NaninovelTMProText, IRevealableText
    {
        public virtual IRevealInfo RevealInfo => RevealState;
        public virtual string Text { get => text; set => SetTextValue(value); }
        public virtual Color TextColor { get => color; set => color = value; }
        public virtual float RevealProgress { get => GetRevealProgress(); set => SetRevealProgress(value); }

        protected virtual RevealState RevealState { get; } = new();
        protected virtual HashSet<TextRevealEffect> ConfirmationRequests { get; } = new();
        protected virtual string TMPText { get => base.text; set => base.text = value; }
        protected virtual int LastCharIndex => Mathf.Max(-1, textInfo.characterCount - 1);
        protected virtual int LastRevealedCharIndex => RevealState.LastRevealedCharIndex;
        protected virtual bool RevealRubyInstantly => revealRubyInstantly;

        [Tooltip("Whether to reveal characters under ruby (furigana) tag instantly.")]
        [SerializeField] private bool revealRubyInstantly = true;

        private new string text = "";

        public virtual bool RevealNextChars (int count, float duration, AsyncToken token)
        {
            RevealState.SetLast(GetNextRevealable(LastRevealedCharIndex + count), duration, token);
            return ConfirmationRequests.Count == 0;
        }

        public virtual Vector2 GetLastRevealedCharPosition ()
        {
            var lastOnPage = GetLastOnDisplayedPage();
            var lastVisible = LastRevealedCharIndex > lastOnPage ? lastOnPage : LastRevealedCharIndex;
            if (!TryGetCharInfo(GetPreviousNonRuby(lastVisible), out var info)) return default;
            var x = isRightToLeftText ? info.bottomLeft.x : info.bottomRight.x;
            return rectTransform.TransformPoint(new Vector2(x, info.baseLine));
        }

        public virtual char GetLastRevealedChar ()
        {
            return TryGetCharInfo(LastRevealedCharIndex, out var info) ? info.character : default;
        }

        /// <summary>
        /// Text reveal effect invokes this with reference to self to indicate that in order to complete
        /// the reveal in full, a user confirmation is required (eg, page may need to be changed).
        /// </summary>
        /// <returns>False when the owner has already added a request and it's pending (wasn't resolved).</returns>
        public virtual bool RequestConfirmation (TextRevealEffect owner)
        {
            return ConfirmationRequests.Add(owner);
        }

        /// <summary>
        /// Text reveal effect invokes this to remove previously added (pending) confirmation requirement.
        /// </summary>
        /// <returns>False when the request is not pending (wasn't added or was already resolved).</returns>
        public virtual bool ResolveConfirmation (TextRevealEffect owner)
        {
            return ConfirmationRequests.Remove(owner);
        }

        protected override void Awake ()
        {
            base.Awake();
            text = TMPText;
        }

        protected virtual void SetTextValue (string value)
        {
            RevealState.SetLatAppendIndex(LastCharIndex);
            text = TMPText = value;
            // Forced update is required to report correct reveal progress right after text is changed; 
            // otherwise the update (which feeds TMP_Text.textInfo) is delayed by the end of frame.
            ForceMeshUpdate();
        }

        protected virtual float GetRevealProgress ()
        {
            if (LastCharIndex <= 0) return LastRevealedCharIndex >= 0 ? 1 : 0;
            return Mathf.Clamp01(LastRevealedCharIndex / (float)LastCharIndex);
        }

        protected virtual void SetRevealProgress (float progress)
        {
            if (LastCharIndex < 0 || Mathf.Approximately(progress, 0)) RevealState.Reset();
            else RevealState.SetLast(GetNextRevealable(Mathf.CeilToInt(LastCharIndex * Mathf.Clamp01(progress))));
        }

        protected virtual int GetNextRevealable (int charIndex)
        {
            if (RevealRubyInstantly) charIndex = GetNextNonRuby(charIndex);
            return Mathf.Min(LastCharIndex, charIndex);
        }

        protected virtual bool TryGetCharInfo (int charIndex, out TMP_CharacterInfo info)
        {
            var valid = charIndex >= 0 && textInfo.characterCount > charIndex;
            info = valid ? textInfo.characterInfo[charIndex] : default;
            return valid;
        }

        protected virtual int GetLastOnDisplayedPage ()
        {
            if (overflowMode != TextOverflowModes.Page) return LastCharIndex;
            var pageIdx = pageToDisplay - 1;
            if (!textInfo.pageInfo.IsIndexValid(pageIdx)) return LastCharIndex;
            return textInfo.pageInfo[pageIdx].lastCharacterIndex;
        }

        protected virtual int GetPreviousNonRuby (int charIndex)
        {
            for (int i = charIndex; i >= 0; i--)
                if (!IsRuby(i))
                    return i;
            return charIndex;
        }

        protected virtual int GetNextNonRuby (int charIndex)
        {
            for (int i = charIndex; i <= LastCharIndex; i++)
                if (!IsRuby(i))
                    return i;
            return LastCharIndex;
        }
    }
}
