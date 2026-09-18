namespace Naninovel.UI
{
    /// <inheritdoc cref="IPauseUI"/>
    public class PauseUI : CustomUI, IPauseUI
    {
        public override UniTask Initialize ()
        {
            BindInput(InputNames.Pause, ToggleVisibility, new() { WhenHidden = true });
            BindInput(InputNames.Cancel, Hide, new() { OnEnd = true });
            return UniTask.CompletedTask;
        }
    }
}
