using TMPro;
using UnityEngine;

namespace Naninovel.UI
{
    /// <summary>
    /// When text overflow is <see cref="TextOverflowModes.Page"/> keeps current
    /// page in sync with the reveal progress.
    /// </summary>
    public class RevealPaginator : TextRevealEffect
    {
        private void OnEnable ()
        {
            Info.OnChange += HandleRevealChanged;
        }

        private void OnDisable ()
        {
            if (Text) Info.OnChange -= HandleRevealChanged;
        }

        private void HandleRevealChanged ()
        {
            var pageNumber = GetPageNumber(Info.LastRevealedCharIndex);
            if (Text.ResolveConfirmation(this) || Mathf.Approximately(Text.RevealProgress, 1))
                Text.pageToDisplay = pageNumber;
            else if (pageNumber != Text.pageToDisplay)
                if (AnyVisibleCharsBeforePageBreak())
                    Text.RequestConfirmation(this);
                else Text.pageToDisplay = pageNumber;
        }

        private int GetPageNumber (int charIndex)
        {
            for (int i = 0; i < Text.textInfo.pageCount; i++)
                if (IsInsidePage(Text.textInfo.pageInfo[i], charIndex))
                    return i + 1;
            return 1;
        }

        private bool IsInsidePage (TMP_PageInfo page, int charIndex)
        {
            return charIndex >= page.firstCharacterIndex && charIndex <= page.lastCharacterIndex;
        }

        private bool AnyVisibleCharsBeforePageBreak ()
        {
            var lastCharIdx = Text.textInfo.pageInfo[Text.pageToDisplay].lastCharacterIndex;
            for (int i = Info.LastAppendIndex + 1; i <= lastCharIdx; i++)
                if (Text.textInfo.characterInfo[i].isVisible)
                    return true;
            return false;
        }
    }
}
