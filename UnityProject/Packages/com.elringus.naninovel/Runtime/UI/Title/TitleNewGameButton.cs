using System;
using System.Linq;
using UnityEngine;

namespace Naninovel.UI
{
    public class TitleNewGameButton : ScriptableButton
    {
        [Tooltip("Services to exclude from state reset when starting a new game.")]
        [SerializeField] private string[] excludeFromReset = Array.Empty<string>();

        private string startScriptPath;
        private TitleMenu titleMenu;
        private IScriptPlayer player;
        private IStateManager state;
        private IScriptManager scripts;

        protected override async void Awake ()
        {
            base.Awake();

            scripts = Engine.GetServiceOrErr<IScriptManager>();
            startScriptPath = await ResolveStartScriptPath(scripts);
            titleMenu = GetComponentInParent<TitleMenu>();
            player = Engine.GetServiceOrErr<IScriptPlayer>();
            state = Engine.GetServiceOrErr<IStateManager>();
            this.AssertRequiredObjects(titleMenu);
        }

        protected override void Start ()
        {
            base.Start();

            if (string.IsNullOrEmpty(startScriptPath))
                UIComponent.interactable = false;
        }

        protected override async void OnButtonClick ()
        {
            if (string.IsNullOrEmpty(startScriptPath))
            {
                Engine.Err("Can't start new game: specify start script in scripts configuration.");
                return;
            }

            await PlayTitleNewGame();
            titleMenu.Hide();
            using (await LoadingScreen.Show())
                await state.ResetState(excludeFromReset,
                    () => player.LoadAndPlay(startScriptPath));
        }

        protected virtual async UniTask PlayTitleNewGame ()
        {
            const string label = "OnNewGame";

            var scriptPath = scripts.Configuration.TitleScript;
            if (string.IsNullOrEmpty(scriptPath)) return;
            var script = (Script)await scripts.ScriptLoader.LoadOrErr(scripts.Configuration.TitleScript);
            if (!script.LabelExists(label)) return;

            player.ResetService();
            await player.LoadAndPlayAtLabel(scriptPath, label);
            await UniTask.WaitWhile(() => player.Playing);
        }

        protected virtual async UniTask<string> ResolveStartScriptPath (IScriptManager scripts)
        {
            if (!string.IsNullOrEmpty(scripts.Configuration.StartGameScript))
                return scripts.Configuration.StartGameScript;
            if (!Application.isEditor)
                Engine.Warn("Please specify 'Start Game Script' in the scripts configuration. " +
                            "When not specified, Naninovel will pick first available script, " +
                            "which may differ between the editor and build environments.");
            return (await scripts.ScriptLoader.Locate()).OrderBy(p => p).FirstOrDefault();
        }
    }
}
