using UnityEngine;

namespace Naninovel.UI
{
    public class TitleExitButton : ScriptableButton
    {
        private IScriptPlayer player;
        private IScriptManager scripts;
        private IStateManager state;

        protected override void Awake ()
        {
            base.Awake();

            scripts = Engine.GetServiceOrErr<IScriptManager>();
            player = Engine.GetServiceOrErr<IScriptPlayer>();
            state = Engine.GetServiceOrErr<IStateManager>();
        }

        protected override async void OnButtonClick ()
        {
            using (new InteractionBlocker())
            {
                await PlayTitleExit();
                await state.SaveGlobal();
                if (Application.platform == RuntimePlatform.WebGLPlayer)
                    WebUtils.OpenURL("about:blank");
                else Application.Quit();
            }
        }

        protected virtual async UniTask PlayTitleExit ()
        {
            const string label = "OnExit";

            var scriptPath = scripts.Configuration.TitleScript;
            if (string.IsNullOrEmpty(scriptPath)) return;
            var script = (Script)await scripts.ScriptLoader.LoadOrErr(scripts.Configuration.TitleScript);
            if (!script.LabelExists(label)) return;

            player.ResetService();
            await player.LoadAndPlayAtLabel(scriptPath, label: label);
            await UniTask.WaitWhile(() => player.Playing);
        }
    }
}
