using UnityEngine;

namespace Naninovel.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TitleMenu : CustomUI, ITitleUI
    {
        private IScriptPlayer player;
        private IScriptManager scripts;
        private string titleScriptPath;

        protected override void Awake ()
        {
            base.Awake();

            player = Engine.GetServiceOrErr<IScriptPlayer>();
            scripts = Engine.GetServiceOrErr<IScriptManager>();
            titleScriptPath = Engine.GetConfiguration<ScriptsConfiguration>().TitleScript;
        }

        public override async UniTask ChangeVisibility (bool visible, float? duration = null, AsyncToken token = default)
        {
            if (visible && !string.IsNullOrEmpty(titleScriptPath))
                using (new InteractionBlocker())
                    await PlayTitleScript(token);
            await base.ChangeVisibility(visible, duration, token);
        }

        protected virtual async UniTask PlayTitleScript (AsyncToken token)
        {
            while (Engine.Initializing) await AsyncUtils.WaitEndOfFrame();

            if (string.IsNullOrEmpty(scripts.Configuration.TitleScript)) return;
            await player.LoadAndPlay(scripts.Configuration.TitleScript);
            token.ThrowIfCanceled();
            while (player.Playing) await AsyncUtils.WaitEndOfFrame();
            token.ThrowIfCanceled();
        }
    }
}
