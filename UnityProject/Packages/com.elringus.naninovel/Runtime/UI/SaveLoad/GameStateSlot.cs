using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Naninovel.UI
{
    public class GameStateSlot : ScriptableGridSlot
    {
        [Serializable]
        private class OnTitleTextChangedEvent : UnityEvent<string> { }

        public override string Id => SlotNumber.ToString();
        public virtual int SlotNumber { get; private set; }
        public virtual GameStateMap State { get; private set; }
        public virtual bool Empty => State == null;
        public virtual float LastSelectTime { get; private set; }

        [ManagedText("DefaultUI")]
        protected static string EmptySlotLabel = "Empty";

        protected virtual Button DeleteButton => deleteButton;
        protected virtual RawImage ThumbnailImage => thumbnailImage;
        protected virtual Texture2D EmptySlotThumbnail => emptySlotThumbnail;
        protected virtual string DateFormat => dateFormat;
        protected virtual string TitleTemplate => titleTemplate;

        [Tooltip("Format of the date set in the title. For available options see C# docs for date and time format strings.")]
        [SerializeField] private string dateFormat = "yyyy-MM-dd HH:mm:ss";
        [Tooltip("Title template. `{N}` is replaced with the slot number, `{D}` — with the date (or empty label when the slot is empty).")]
        [SerializeField] private string titleTemplate = "{N}. {D}";
        [SerializeField] private Button deleteButton;
        [SerializeField] private RawImage thumbnailImage;
        [SerializeField] private Texture2D emptySlotThumbnail;
        [SerializeField] private OnTitleTextChangedEvent onTitleTextChanged;

        private Action<int> onClicked, onDeleteClicked;
        private ScriptableUIBehaviour deleteButtonBehaviour;
        private ISaveLoadUI saveLoadUI;
        private ILocalizationManager l10n;

        public virtual void Initialize (Action<int> onClicked, Action<int> onDeleteClicked)
        {
            this.onClicked = onClicked;
            this.onDeleteClicked = onDeleteClicked;
            l10n = Engine.GetServiceOrErr<ILocalizationManager>();
            l10n.OnLocaleChanged += HandleLocaleChanged;
            saveLoadUI = Engine.GetServiceOrErr<IUIManager>().GetUI<ISaveLoadUI>();
            if (Engine.GetServiceOrErr<IInputManager>().GetDelete() is { } deleteInput)
                deleteInput.OnStart += HandleDeleteInputActivated;
        }

        public virtual void Bind (int slotNumber, GameStateMap state)
        {
            State = state;
            SlotNumber = slotNumber;
            if (Empty) SetEmptyState(slotNumber);
            else SetNonEmptyState(slotNumber, state);
        }

        public override void OnPointerEnter (PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            if (deleteButtonBehaviour)
                deleteButtonBehaviour.Show();
        }

        public override void OnPointerExit (PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            if (deleteButtonBehaviour)
                deleteButtonBehaviour.Hide();
        }

        protected override void Awake ()
        {
            base.Awake();
            this.AssertRequiredObjects(DeleteButton, ThumbnailImage);

            if (!EmptySlotThumbnail) emptySlotThumbnail = Texture2D.whiteTexture;
            DeleteButton.TryGetComponent<ScriptableUIBehaviour>(out deleteButtonBehaviour);
            DeleteButton.onClick.AddListener(HandleDeleteButtonClicked);
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();

            DeleteButton.onClick.RemoveListener(HandleDeleteButtonClicked);
            if (l10n != null) l10n.OnLocaleChanged -= HandleLocaleChanged;
        }

        protected virtual void SetEmptyState (int slotNumber)
        {
            DeleteButton.gameObject.SetActive(false);
            SetTitleText(TitleTemplate.Replace("{N}", slotNumber.ToString()).Replace("{D}", EmptySlotLabel));
            ThumbnailImage.texture = EmptySlotThumbnail;
        }

        protected virtual void SetNonEmptyState (int slotNumber, GameStateMap state)
        {
            DeleteButton.gameObject.SetActive(true);
            var date = state.SaveDateTime.ToString(DateFormat);
            SetTitleText(TitleTemplate.Replace("{N}", slotNumber.ToString()).Replace("{D}", date));
            ThumbnailImage.texture = state.Thumbnail;
        }

        protected virtual void SetTitleText (string value)
        {
            onTitleTextChanged?.Invoke(value);
        }

        protected override void OnButtonClick ()
        {
            base.OnButtonClick();
            onClicked?.Invoke(SlotNumber);
        }

        protected virtual void HandleDeleteButtonClicked ()
        {
            onDeleteClicked?.Invoke(SlotNumber);
        }

        protected virtual void HandleDeleteInputActivated ()
        {
            if (saveLoadUI.Visible && gameObject.activeInHierarchy && Selected)
                onDeleteClicked?.Invoke(SlotNumber);
        }

        protected virtual void HandleLocaleChanged (LocaleChangedArgs _)
        {
            if (Empty) SetEmptyState(SlotNumber); // Update "Empty" label.
        }

        public override void OnSelect (BaseEventData eventData)
        {
            base.OnSelect(eventData);
            LastSelectTime = Engine.Time.UnscaledTime;
        }
    }
}
