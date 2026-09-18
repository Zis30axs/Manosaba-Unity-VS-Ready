using Naninovel.UI;

namespace Naninovel.Commands
{
    [Doc(
        @"
Shows a UI for general-purpose self-hiding popup notification (aka ""toast"")
with the specified text and (optionally) appearance and duration.
The UI is automatically hidden after the specified (or default) duration.",
        @"
Appearance name is the name of a game object with `Toast Appearance`
component inside the `ToastUI` UI prefab (case-insensitive).",
        @"
; Shows a default toast with 'Hello World!' content.
@toast ""Hello World!""",
        @"
; Shows a toast with a 'warning' appearance.
@toast ""You're in danger!"" appearance:warning",
        @"
; The toast will disappear in one second.
@toast ""I'll disappear in 1 second."" time:1"
    )]
    [CommandAlias("toast")]
    public class ShowToastUI : Command, Command.IPreloadable, Command.ILocalizable
    {
        [Doc("The text content to set for the toast.")]
        [ParameterAlias(NamelessParameterAlias)]
        public LocalizableTextParameter Text;
        [Doc("Appearance variant (game object name) of the toast. " +
             "When not specified, will use default appearance set in Toast UI prefab.")]
        public StringParameter Appearance;
        [Doc("Seconds to wait before hiding the toast. " +
             "When not specified, will use duration set by default in Toast UI prefab.")]
        [ParameterAlias("time")]
        public DecimalParameter Duration;

        public UniTask PreloadResources () => PreloadStaticTextResources(Text);
        public void ReleaseResources () => ReleaseStaticTextResources(Text);

        public override async UniTask Execute (AsyncToken token = default)
        {
            using var _ = await LoadDynamicTextResources(Text);
            var toastUI = Engine.GetServiceOrErr<IUIManager>().GetUI<IToastUI>();
            toastUI?.Show(Text, Appearance, Duration);
        }
    }
}
