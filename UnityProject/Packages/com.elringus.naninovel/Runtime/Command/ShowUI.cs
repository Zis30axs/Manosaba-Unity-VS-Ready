namespace Naninovel.Commands
{
    [Doc(
        @"
Makes [UI elements](/guide/user-interface) with the specified resource names visible.
When no names are specified, will reveal the entire UI (in case it was hidden with [@hideUI]).",
        null,
        @"
; Given you've added a custom UI with 'Calendar' name,
; the following will make it visible on the scene.
@showUI Calendar",
        @"
; Given you've hidden the entire UI with @hideUI, show it back.
@showUI",
        @"
; Simultaneously reveal built-in 'TipsUI' and custom 'Calendar' UIs.
@showUI TipsUI,Calendar"
    )]
    public class ShowUI : Command
    {
        [Doc("Name of the UI resource to make visible.")]
        [ParameterAlias(NamelessParameterAlias), ResourceContext(UIConfiguration.DefaultUIPathPrefix)]
        public StringListParameter UINames;
        [Doc("Duration (in seconds) of the show animation. When not specified, will use UI-specific duration.")]
        [ParameterAlias("time")]
        public DecimalParameter Duration;
        [Doc("Whether to wait for the UI fade-in animation before playing next command.")]
        public BooleanParameter Wait;

        public override UniTask Execute (AsyncToken token = default)
        {
            return WaitOrForget(Show, Wait, token);
        }

        protected virtual async UniTask Show (AsyncToken token)
        {
            var uiManager = Engine.GetServiceOrErr<IUIManager>();

            if (!Assigned(UINames))
            {
                uiManager.SetUIVisibleWithToggle(true);
                return;
            }

            using var _ = ListPool<UniTask>.Rent(out var tasks);
            foreach (var name in UINames)
            {
                var ui = uiManager.GetUI(name);
                if (ui is null)
                {
                    Warn($"Failed to show '{name}' UI: managed UI with the specified resource name not found.");
                    continue;
                }
                tasks.Add(ui.ChangeVisibility(true, Assigned(Duration) ? Duration : null));
            }

            await UniTask.WhenAll(tasks);
        }
    }
}
