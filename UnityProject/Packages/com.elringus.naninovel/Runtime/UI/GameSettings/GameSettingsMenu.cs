using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class GameSettingsMenu : CustomUI, ISettingsUI
    {
        protected virtual Toggle[] Tabs => tabs;

        [Tooltip("Toggles representing menu tabs. Expected in left to right order.")]
        [SerializeField] private Toggle[] tabs;

        private IStateManager state => Engine.GetService<IStateManager>();
        private ITextManager docs => Engine.GetService<ITextManager>();
        private readonly List<TMP_Dropdown> dropdowns = new();
        private int tabIndex;

        public override async UniTask Initialize ()
        {
            await docs.DocumentLoader.Load(ManagedTextPaths.Locales, this);
            BindInput(InputNames.Tab, HandleTabInput);
            BindInput(InputNames.Cancel, HandleCancelInput, new() { OnEnd = true });
        }

        public virtual async UniTask SaveSettingsAndHide ()
        {
            using (new InteractionBlocker())
                await state.SaveSettings();
            Hide();
        }

        protected override void Awake ()
        {
            base.Awake();
            dropdowns.AddRange(GetComponentsInChildren<TMP_Dropdown>(true));
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();
            docs?.DocumentLoader?.ReleaseAll(this);
        }

        protected virtual void HandleCancelInput ()
        {
            foreach (var dropdown in dropdowns)
                if (dropdown.transform.childCount > 3) // A dropdown is open.
                    return;
            SaveSettingsAndHide().Forget();
        }

        protected virtual void HandleTabInput (float value)
        {
            if (tabs == null || tabs.Length == 0) return;
            if (value <= -1f) tabIndex--;
            if (value >= 1f) tabIndex++;
            tabIndex = Mathf.Clamp(tabIndex, 0, tabs.Length - 1);
            for (int i = 0; i < tabs.Length; i++)
                tabs[i].isOn = i == tabIndex;
            EventUtils.Select(FindFocusObject());
        }
    }
}
