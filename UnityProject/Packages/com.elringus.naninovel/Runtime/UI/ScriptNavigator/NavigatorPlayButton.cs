using System;
using UnityEngine;
using UnityEngine.Events;

namespace Naninovel.UI
{
    public class NavigatorPlayButton : ScriptableButton
    {
        [Serializable]
        private class OnLabelChangedEvent : UnityEvent<string> { }

        [SerializeField] private OnLabelChangedEvent onLabelChanged;

        private NavigatorPanel navigator;
        private string scriptPath;
        private IScriptPlayer player;
        private IStateManager stateManager;
        private bool isInitialized;

        public virtual void Initialize (NavigatorPanel navigator, string scriptPath, IScriptPlayer player)
        {
            this.navigator = navigator;
            this.scriptPath = scriptPath;
            this.player = player;
            name = "PlayScript: " + scriptPath;
            SetLabel(scriptPath);
            isInitialized = true;
            UIComponent.interactable = true;
        }

        protected override void Awake ()
        {
            base.Awake();

            SetLabel(null);
            UIComponent.interactable = false;
            stateManager = Engine.GetServiceOrErr<IStateManager>();
        }

        protected override void OnEnable ()
        {
            base.OnEnable();

            stateManager.GameSlotManager.OnBeforeLoad += ControlInteractability;
            stateManager.GameSlotManager.OnLoaded += ControlInteractability;
            stateManager.GameSlotManager.OnBeforeSave += ControlInteractability;
            stateManager.GameSlotManager.OnSaved += ControlInteractability;
        }

        protected override void OnDisable ()
        {
            base.OnDisable();

            stateManager.GameSlotManager.OnBeforeLoad -= ControlInteractability;
            stateManager.GameSlotManager.OnLoaded -= ControlInteractability;
            stateManager.GameSlotManager.OnBeforeSave -= ControlInteractability;
            stateManager.GameSlotManager.OnSaved -= ControlInteractability;
        }

        protected override void OnButtonClick ()
        {
            Debug.Assert(isInitialized);
            navigator.Hide();
            Engine.GetService<IUIManager>()?.GetUI<ITitleUI>()?.Hide();
            PlayScript();
        }

        protected virtual void SetLabel (string value)
        {
            onLabelChanged?.Invoke(value);
        }

        protected virtual void PlayScript ()
        {
            stateManager.ResetState(() => player.LoadAndPlay(scriptPath)).Forget();
        }

        protected virtual void ControlInteractability (string _)
        {
            UIComponent.interactable = !stateManager.GameSlotManager.Loading && !stateManager.GameSlotManager.Saving;
        }
    }
}
