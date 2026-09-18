namespace Naninovel.Commands
{
    [Doc(
        @"
Makes [UI elements](/guide/user-interface#ui-customization) with the specified names invisible.
When no names are specified, will stop rendering (hide) the entire UI (including all the built-in UIs).",
        @"
When hiding the entire UI with this command and `allowToggle` parameter is false (default), user won't be able to re-show the UI
back with hotkeys or by clicking anywhere on the screen; use [@showUI] command to make the UI visible again.",
        @"
; Given a custom 'Calendar' UI, the following command will hide it.
@hideUI Calendar",
        @"
; Hide the entire UI, won't allow user to re-show it.
@hideUI
...
; Make the UI visible again.
@showUI",
        @"
; Hide the entire UI, but allow the user to toggle it back.
@hideUI allowToggle!",
        @"
; Simultaneously hide built-in 'TipsUI' and custom 'Calendar' UIs.
@hideUI TipsUI,Calendar"
    )]
    public class HideUI : Command
    {
        [Doc("Name of the UI elements to hide.")]
        [ParameterAlias(NamelessParameterAlias), ResourceContext(UIConfiguration.DefaultUIPathPrefix)]
        public StringListParameter UINames;
        [Doc("When hiding the entire UI, controls whether to allow the user to re-show the UI with hotkeys or " +
             "by clicking anywhere on the screen (false by default). Has no effect when hiding a particular UI.")]
        [ParameterDefaultValue("false")]
        public BooleanParameter AllowToggle = false;
        [Doc("Duration (in seconds) of the hide animation. When not specified, will use UI-specific duration.")]
        [ParameterAlias("time")]
        public DecimalParameter Duration;
        [Doc("Whether to wait for the UI fade-out animation before playing next command.")]
        public BooleanParameter Wait;

        public override UniTask Execute (AsyncToken token = default)
        {
            return WaitOrForget(Hide, Wait, token);
        }

        protected virtual async UniTask Hide (AsyncToken token)
        {
            var uiManager = Engine.GetServiceOrErr<IUIManager>();

            if (!Assigned(UINames))
            {
                uiManager.SetUIVisibleWithToggle(false, AllowToggle);
                return;
            }

            using var _ = ListPool<UniTask>.Rent(out var tasks);
            foreach (var name in UINames)
            {
                var ui = uiManager.GetUI(name);
                if (ui is null)
                {
                    Warn($"Failed to hide '{name}' UI: managed UI with the specified resource name not found.");
                    continue;
                }

                tasks.Add(ui.ChangeVisibility(false, Assigned(Duration) ? Duration : null));
            }

            await UniTask.WhenAll(tasks);
        }
    }
}
