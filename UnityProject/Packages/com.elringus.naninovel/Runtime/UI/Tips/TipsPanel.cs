using System;
using System.Collections.Generic;
using System.Linq;
using Naninovel.ManagedText;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TipsPanel : CustomUI, ITipsUI
    {
        public const string DefaultUnlockableIdPrefix = "Tips";

        public virtual int TipsCount { get; private set; }

        protected virtual string UnlockableIdPrefix => unlockableIdPrefix;
        protected virtual RectTransform ItemsContainer => itemsContainer;
        protected virtual TipsListItem ItemPrefab => itemPrefab;
        protected virtual string SeparatorLiteral => separatorLiteral;
        protected virtual string SelectedPrefix => selectedPrefix;
        protected virtual IReadOnlyList<TipsListItem> ListItems => listItems;
        protected virtual string SelectedItemId { get; set; }

        [Header("Tips Setup")]
        [Tooltip("All the unlockable item IDs with the specified prefix will be considered Tips items.")]
        [SerializeField] private string unlockableIdPrefix = DefaultUnlockableIdPrefix;
        [Tooltip("Text character to separate title, category and tip text in managed text record.")]
        [SerializeField] private string separatorLiteral = "^";
        [Tooltip("Prefix to add to unlockable ID to indicate that it was selected (seen) at least once.")]
        [SerializeField] private string selectedPrefix = "TIP_SELECTED_";

        [Header("UI Setup")]
        [SerializeField] private ScrollRect itemsScrollRect;
        [SerializeField] private RectTransform itemsContainer;
        [SerializeField] private TipsListItem itemPrefab;
        [SerializeField] private StringUnityEvent onTitleChanged;
        [SerializeField] private StringUnityEvent onNumberChanged;
        [SerializeField] private StringUnityEvent onCategoryChanged;
        [SerializeField] private StringUnityEvent onDescriptionChanged;

        private readonly List<TipsListItem> listItems = new();
        private IUnlockableManager unlockables;
        private ILocalizationManager l10n;
        private ITextManager docs;

        public override async UniTask Initialize ()
        {
            await FillListItems();
            BindInput(InputNames.NavigateY, HandleNavigationInput);
            BindInput(InputNames.Cancel, Hide, new() { OnEnd = true });
        }

        public virtual void SelectTipRecord (string tipId)
        {
            var unlockableId = $"{UnlockableIdPrefix}/{tipId}";
            var item = listItems.Find(i => i.UnlockableId == unlockableId);
            if (item is null) throw new Error($"Failed to select `{tipId}` tip record: item with the ID is not found.");
            itemsScrollRect.ScrollTo(item.GetComponent<RectTransform>());
            SelectItem(item);
        }

        public virtual bool HasUnselectedItem ()
        {
            foreach (var item in listItems)
                if (unlockables.ItemUnlocked(item.UnlockableId) &&
                    !WasItemSelectedOnce(item.UnlockableId))
                    return true;
            return false;
        }

        protected override void Awake ()
        {
            base.Awake();
            this.AssertRequiredObjects(itemsScrollRect, ItemsContainer, ItemPrefab);

            unlockables = Engine.GetServiceOrErr<IUnlockableManager>();
            docs = Engine.GetServiceOrErr<ITextManager>();
            l10n = Engine.GetServiceOrErr<ILocalizationManager>();

            ClearSelection();
        }

        protected override void OnEnable ()
        {
            base.OnEnable();

            l10n.AddChangeLocaleTask(HandleLocaleChanged);
            unlockables.OnItemUpdated += HandleUnlockableItemUpdated;
        }

        protected override void OnDisable ()
        {
            base.OnDisable();

            l10n?.RemoveChangeLocaleTask(HandleLocaleChanged);
            if (unlockables != null)
                unlockables.OnItemUpdated -= HandleUnlockableItemUpdated;
        }

        protected virtual async UniTask FillListItems ()
        {
            using var _ = ListPool<UniTask>.Rent(out var tasks);
            foreach (var record in docs.GetDocument(ManagedTextPaths.Tips)?.Records ?? Array.Empty<ManagedTextRecord>())
                tasks.Add(FillListItem(record));
            await UniTask.WhenAll(tasks);
            foreach (var item in listItems)
                item.SetUnlocked(unlockables.ItemUnlocked(item.UnlockableId));
            TipsCount = listItems.Count;
        }

        protected virtual async UniTask FillListItem (ManagedTextRecord record)
        {
            var unlockableId = $"{UnlockableIdPrefix}/{record.Key}";
            var value = string.IsNullOrEmpty(record.Value) ? GetRecordValueOrDefault(record.Key, ManagedTextPaths.Tips) : record.Value;
            var title = value.GetBefore(SeparatorLiteral) ?? value;
            var selectedOnce = WasItemSelectedOnce(unlockableId);
            var item = await TipsListItem.Instantiate(ItemPrefab, unlockableId, title, selectedOnce, SelectItem);
            item.transform.SetParent(ItemsContainer, false);
            listItems.Add(item);
        }

        protected virtual void ClearListItems ()
        {
            foreach (var item in listItems.ToArray())
                ObjectUtils.DestroyOrImmediate(item.gameObject);
            listItems.Clear();
            ItemsContainer.DetachChildren();
            TipsCount = 0;
        }

        protected virtual void ClearSelection ()
        {
            SetTitle(string.Empty);
            SetNumber(string.Empty);
            SetCategory(string.Empty);
            SetDescription(string.Empty);
            foreach (var item in listItems)
                item.SetSelected(false);
        }

        protected virtual void SelectItem (TipsListItem item)
        {
            if (!unlockables.ItemUnlocked(item.UnlockableId)) return;

            SelectedItemId = item.UnlockableId;
            SetItemSelectedOnce(item.UnlockableId);
            foreach (var listItem in listItems)
                listItem.SetSelected(listItem.UnlockableId.EqualsFast(item.UnlockableId));
            var recordValue = GetRecordValueOrDefault(item.UnlockableId.GetAfterFirst($"{UnlockableIdPrefix}/"), ManagedTextPaths.Tips);
            SetTitle(recordValue.GetBefore(SeparatorLiteral)?.Trim() ?? recordValue);
            SetNumber(item.Number.ToString());
            SetCategory(recordValue.GetBetween(SeparatorLiteral)?.Trim() ?? string.Empty);
            SetDescription(recordValue.GetAfter(SeparatorLiteral)?.Replace("\\n", "\n").Trim() ?? string.Empty);
            EventUtils.Select(item.gameObject);
        }

        protected virtual void HandleUnlockableItemUpdated (UnlockableItemUpdatedArgs args)
        {
            if (!args.Id.StartsWithFast(UnlockableIdPrefix)) return;

            var unlockedItem = listItems.Find(i => i.UnlockableId.EqualsFast(args.Id));
            if (unlockedItem) unlockedItem.SetUnlocked(args.Unlocked);
        }

        protected virtual void SetTitle (string value)
        {
            onTitleChanged?.Invoke(value);
        }

        protected virtual void SetNumber (string value)
        {
            onNumberChanged?.Invoke(value);
        }

        protected virtual void SetCategory (string value)
        {
            onCategoryChanged?.Invoke(value);
        }

        protected virtual void SetDescription (string value)
        {
            onDescriptionChanged?.Invoke(value);
        }

        protected virtual bool WasItemSelectedOnce (string unlockableId)
        {
            return unlockables.ItemUnlocked(SelectedPrefix + unlockableId);
        }

        protected virtual void SetItemSelectedOnce (string unlockableId)
        {
            unlockables.SetItemUnlocked(SelectedPrefix + unlockableId, true);
        }

        protected virtual async UniTask HandleLocaleChanged (LocaleChangedArgs _)
        {
            ClearSelection();
            ClearListItems();
            await FillListItems();
        }

        public override async UniTask ChangeVisibility (bool visible, float? duration = null, AsyncToken token = default)
        {
            if (!visible)
                using (new InteractionBlocker())
                    await Engine.GetServiceOrErr<IStateManager>().SaveGlobal();
            await base.ChangeVisibility(visible, duration, token);
        }

        protected override void HandleVisibilityChanged (bool visible)
        {
            base.HandleVisibilityChanged(visible);

            if (visible && (FindSelectedItem() ?? FindFirstUnlockedItem()) is { } item)
                SelectItem(item);
        }

        protected virtual TipsListItem FindSelectedItem ()
        {
            return listItems.FirstOrDefault(i => i.UnlockableId == SelectedItemId);
        }

        protected virtual TipsListItem FindFirstUnlockedItem ()
        {
            return listItems.FirstOrDefault(i => unlockables.ItemUnlocked(i.UnlockableId));
        }

        protected virtual TipsListItem FindLastUnlockedItem ()
        {
            return listItems.LastOrDefault(i => unlockables.ItemUnlocked(i.UnlockableId));
        }

        protected virtual void HandleNavigationInput (float value)
        {
            if (!Visible) return;
            if (value <= -1f) SelectPreviousUnlockedItem();
            if (value >= 1f) SelectNextUnlockedItem();
        }

        protected virtual void SelectPreviousUnlockedItem ()
        {
            for (var i = listItems.IndexOf(FindSelectedItem()) - 1; i >= 0; i--)
                if (unlockables.ItemUnlocked(listItems[i].UnlockableId))
                {
                    SelectItem(listItems[i]);
                    return;
                }
            if (FindLastUnlockedItem() is { } last) SelectItem(last);
        }

        protected virtual void SelectNextUnlockedItem ()
        {
            for (var i = listItems.IndexOf(FindSelectedItem()) + 1; i < listItems.Count; i++)
                if (unlockables.ItemUnlocked(listItems[i].UnlockableId))
                {
                    SelectItem(listItems[i]);
                    return;
                }
            if (FindFirstUnlockedItem() is { } first) SelectItem(first);
        }

        protected virtual string GetRecordValueOrDefault (string key, string documentPath)
        {
            var value = docs.GetRecordValue(key, documentPath);
            if (value is null) return $"{documentPath}/{key}";
            if (value.Length == 0 && docs.TryGetRecord(key, documentPath, out var record))
                if (!string.IsNullOrEmpty(record.Comment)) return record.Comment;
                else return $"{documentPath}/{key}";
            return value;
        }
    }
}
