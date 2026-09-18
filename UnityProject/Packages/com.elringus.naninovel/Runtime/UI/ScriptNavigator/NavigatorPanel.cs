using System.Collections.Generic;
using UnityEngine;

namespace Naninovel.UI
{
    public abstract class NavigatorPanel : CustomUI
    {
        protected virtual Transform ButtonsContainer => buttonsContainer;
        protected virtual GameObject PlayButtonPrototype => playButtonPrototype;

        [SerializeField] private Transform buttonsContainer;
        [SerializeField] private GameObject playButtonPrototype;

        protected virtual IScriptPlayer Player { get; private set; }
        protected virtual IScriptManager ScriptManager { get; private set; }

        protected override async void Awake ()
        {
            base.Awake();
            this.AssertRequiredObjects(ButtonsContainer, PlayButtonPrototype);
            Player = Engine.GetServiceOrErr<IScriptPlayer>();
            ScriptManager = Engine.GetServiceOrErr<IScriptManager>();
            GenerateScriptButtons(await LocateAllScriptPaths());
        }

        protected override void OnEnable ()
        {
            base.OnEnable();
            Player.OnPlay += HandlePlay;
        }

        protected override void OnDisable ()
        {
            base.OnDisable();
            if (Player != null)
                Player.OnPlay -= HandlePlay;
        }

        protected abstract UniTask<IReadOnlyCollection<string>> LocateAllScriptPaths ();

        protected virtual void GenerateScriptButtons (IEnumerable<string> scriptPaths)
        {
            if (ButtonsContainer)
                ObjectUtils.DestroyAllChildren(ButtonsContainer);

            foreach (var path in scriptPaths)
            {
                var scriptButton = Instantiate(PlayButtonPrototype, ButtonsContainer, false);
                scriptButton.GetComponent<NavigatorPlayButton>().Initialize(this, path, Player);
            }
        }

        private void HandlePlay (Script script)
        {
            if (ScriptManager.Configuration.TitleScript != script.Path)
                Hide();
        }
    }
}
