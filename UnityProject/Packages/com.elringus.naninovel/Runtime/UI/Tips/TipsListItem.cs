using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Naninovel
{
    public class TipsListItem : MonoBehaviour
    {
        [Serializable]
        private class OnLabelChangedEvent : UnityEvent<string> { }

        public virtual string UnlockableId { get; private set; }
        public virtual int Number => transform.GetSiblingIndex() + 1;

        protected virtual Button Button => button;
        protected virtual GameObject SelectedIndicator => selectedIndicator;

        [Tooltip("Tip label template. `{N}` will be replaced with the record number, `{T}` — with the title.")]
        [SerializeField] private string template = "{N}. {T}";
        [Tooltip("Non-selected tip label template. `{I}` will be replaced with the tip label content.")]
        [SerializeField] private string normalTemplate = "<alpha=#aa>{I}";
        [Tooltip("Selected tip label template. `{I}` will be replaced with the tip label content.")]
        [SerializeField] private string selectedTemplate = "{I}";
        [Tooltip("Label template for unlocked tip items that where never selected (seen). `{I}` will be replaced with the tip label content.")]
        [SerializeField] private string unseenTemplate = "<color=#6ac9d4>{I}";
        [Tooltip("Record title to set when the tip item is locked.")]
        [SerializeField] private string lockedTitle = "???";
        [Tooltip("The tip button.")]
        [SerializeField] private Button button;
        [Tooltip("When assigned, the game object will be activated when the tip is selected.")]
        [SerializeField] private GameObject selectedIndicator;
        [SerializeField] private OnLabelChangedEvent onLabelChanged;

        private Action<TipsListItem> onClick;
        private string title;
        private bool seen, unlocked, selected;

        public static async UniTask<TipsListItem> Instantiate (TipsListItem prototype, string unlockableId,
            string title, bool selectedOnce, Action<TipsListItem> onClick)
        {
            var item = await Engine.Instantiate(prototype);
            item.onClick = onClick;
            item.UnlockableId = unlockableId;
            item.title = title;
            item.seen = selectedOnce;
            return item;
        }

        public virtual void SetSelected (bool selected)
        {
            this.selected = selected;
            if (selected) seen = true;
            SetLabel(BuildLabel());
            if (SelectedIndicator)
                SelectedIndicator.SetActive(selected);
        }

        public virtual void SetUnlocked (bool unlocked)
        {
            this.unlocked = unlocked;
            SetLabel(BuildLabel());
            Button.interactable = unlocked;
        }

        protected virtual void Awake ()
        {
            this.AssertRequiredObjects(Button);
            if (SelectedIndicator)
                SelectedIndicator.SetActive(false);
        }

        protected virtual void OnEnable ()
        {
            Button.onClick.AddListener(HandleButtonClicked);
        }

        protected virtual void OnDisable ()
        {
            Button.onClick.RemoveListener(HandleButtonClicked);
        }

        protected virtual void SetLabel (string value)
        {
            onLabelChanged?.Invoke(value);
        }

        protected virtual void HandleButtonClicked ()
        {
            onClick?.Invoke(this);
        }

        protected virtual string BuildLabel ()
        {
            var label = template.Replace("{N}", Number.ToString()).Replace("{T}", unlocked ? title : lockedTitle);
            label = (!seen && unlocked ? unseenTemplate : selected ? selectedTemplate : normalTemplate).Replace("{I}", label);
            return label;
        }
    }
}
