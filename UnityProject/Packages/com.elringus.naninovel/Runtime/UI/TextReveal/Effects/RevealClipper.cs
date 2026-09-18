namespace Naninovel.UI
{
    /// <summary>
    /// A text reveal effect that clips exceeding characters, aka typewriter.
    /// </summary>
    public class RevealClipper : TextRevealEffect
    {
        private void OnEnable ()
        {
            Info.OnChange += HandleChange;
        }

        private void OnDisable ()
        {
            if (Text) Info.OnChange -= HandleChange;
        }

        private void HandleChange ()
        {
            Text.maxVisibleCharacters = Info.LastRevealedCharIndex + 1;
        }
    }
}
