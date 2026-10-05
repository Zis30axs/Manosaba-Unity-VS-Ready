using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.AddressablesUtils;
using GigaCreation.Essentials.Input;
using GigaCreation.Essentials.Localization;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000116")]
    public abstract class WitchBookPageBase : MonoBehaviour, IDisposable
    {
    	[Token(Token = "0x400052D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private Canvas _canvas;

    	[Token(Token = "0x400052E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private GraphicRaycaster _graphicRaycaster;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x379840, 292 B
    	// Unity editor `Reset` callback: `_canvas (+0x20) = GetComponent<Canvas>()`, then
    	// `_graphicRaycaster (+0x28) = GetComponent<GraphicRaycaster>()`.
    	[Token(Token = "0x6000494")]
    	[Address(RVA = "0x379840", Offset = "0x378240", VA = "0x180379840", Slot = "5")]
    	protected virtual void Reset()
    	{
    		_canvas = GetComponent<Canvas>();
    		_graphicRaycaster = GetComponent<GraphicRaycaster>();
    	}

    	[Token(Token = "0x6000495")]
    	public abstract void Dispose();

    	[Token(Token = "0x6000496")]
    	public abstract Cysharp.Threading.Tasks.UniTask InitializeAsync(WitchBookItemList itemList, WitchBookItemButton itemButtonPrefab, [Optional] CancellationToken ct);

    	[Token(Token = "0x6000497")]
    	public abstract void InitializeItems();

    	// RECONSTRUCTED | Confidence: A | RVA: 0x379970, 57 B
    	// `_canvas.enabled = true`, then `_graphicRaycaster.enabled = true` (tail jump).
    	// The only null checks are managed NRE checks; `asPresentEvidenceMode` is unused in the base.
    	[Token(Token = "0x6000498")]
    	[Address(RVA = "0x379970", Offset = "0x378370", VA = "0x180379970", Slot = "9")]
    	public virtual void Show(bool asPresentEvidenceMode)
    	{
    		_canvas.enabled = true;
    		_graphicRaycaster.enabled = true;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36F420, 57 B (identical-code-folded with MapFloor.Hide)
    	// Mirror of Show: `_canvas.enabled = false`, then `_graphicRaycaster.enabled = false` (tail jump).
    	[Token(Token = "0x6000499")]
    	[Address(RVA = "0x36F420", Offset = "0x36DE20", VA = "0x18036F420", Slot = "10")]
    	public virtual void Hide()
    	{
    		_canvas.enabled = false;
    		_graphicRaycaster.enabled = false;
    	}

    	[Token(Token = "0x600049A")]
    	public abstract void UpdateVersion(string id, int version);

    	[Token(Token = "0x600049B")]
    	public abstract void ClearState();

    	[Token(Token = "0x600049C")]
    	public abstract void InformAt(int idx);

    	[Token(Token = "0x600049D")]
    	public abstract IReadOnlyCollection<IdVersionPair> GetActiveEvidences();

    	[Token(Token = "0x600049E")]
    	public abstract Cysharp.Threading.Tasks.UniTask<string> OnPresentEvidenceAsync([Optional] CancellationToken ct);

    	[Token(Token = "0x600049F")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	protected WitchBookPageBase()
    	{
    	}
    }
    [Token(Token = "0x2000117")]
    public abstract class WitchBookPageBase<TItem, TState> : WitchBookPageBase where TState : VersionedState, new()
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000119")]
    	[CompilerGenerated]
    	private struct _003CDeserializeState_003Ed__42
    {
    		[Token(Token = "0x4000545")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000546")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Naninovel.Async.CompilerServices.AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000547")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public GameStateMap stateMap;

    		[Token(Token = "0x4000548")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public WitchBookPageBase<TItem, TState> _003C_003E4__this;

    		[Token(Token = "0x4000549")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60004C5")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60004C6")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200011A")]
    	[CompilerGenerated]
    	private struct _003CInitializeAsync_003Ed__31
    {
    		[Token(Token = "0x400054A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400054B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400054C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public WitchBookPageBase<TItem, TState> _003C_003E4__this;

    		[Token(Token = "0x400054D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public WitchBookItemList itemList;

    		[Token(Token = "0x400054E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public WitchBookItemButton itemButtonPrefab;

    		[Token(Token = "0x400054F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000550")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private Cysharp.Threading.Tasks.UniTask<IList<VersionedItem<TItem>>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60004C7")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60004C8")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200011B")]
    	[CompilerGenerated]
    	private struct _003CLoadDataAsync_003Ed__40
    {
    		[Token(Token = "0x4000551")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000552")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<IList<VersionedItem<TItem>>> _003C_003Et__builder;

    		[Token(Token = "0x4000553")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public WitchBookPageBase<TItem, TState> _003C_003E4__this;

    		[Token(Token = "0x4000554")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000555")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private Cysharp.Threading.Tasks.UniTask<WitchBookDataBase<TItem>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60004C9")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60004CA")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200011C")]
    	[CompilerGenerated]
    	private struct _003COnPresentEvidenceAsync_003Ed__39
    {
    		[Token(Token = "0x4000556")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000557")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder<string> _003C_003Et__builder;

    		[Token(Token = "0x4000558")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public WitchBookPageBase<TItem, TState> _003C_003E4__this;

    		[Token(Token = "0x4000559")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public CancellationToken ct;

    		[Token(Token = "0x400055A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private Cysharp.Threading.Tasks.UniTask<ResponseKind>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60004CB")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60004CC")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400052F")]
    	private const int MaxShownItemListCount = 10;

    	[Token(Token = "0x4000530")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	[Space]
    	[SerializeField]
    	private AssetReferenceT<WitchBookDataBase<TItem>> _witchBookDataAsset;

    	[Token(Token = "0x4000531")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private Transform _itemButtonPool;

    	[Token(Token = "0x4000532")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private WitchTrialsButton _presentEvidenceButton;

    	[Token(Token = "0x4000533")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly TState _state = new TState();

    	[Token(Token = "0x4000534")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private IAddressableAssetLoader _addressableAssetLoader;

    	[Token(Token = "0x4000535")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private GigaCreation.Essentials.Input.IInputManager _inputManager;

    	[Token(Token = "0x4000536")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private ILocalizationProvider _localizationProvider;

    	[Token(Token = "0x4000537")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private IStateManager _naniStateManager;

    	[Token(Token = "0x4000538")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private WitchBookItemList _itemList;

    	[Token(Token = "0x4000539")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private WitchBookItemButton _itemButtonPrefab;

    	[Token(Token = "0x400053A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private List<WitchBookItemButton> _itemButtons;

    	[Token(Token = "0x400053B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private IList<VersionedItem<TItem>> _loadedDataItemMap;

    	[Token(Token = "0x400053C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private VersionedItem<TItem>[] _displayableItemMaps;

    	[Token(Token = "0x400053D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private string[] _itemIds;

    	[Token(Token = "0x400053E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private string _currentItemId;

    	[Token(Token = "0x400053F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private IDisposable _paginationDisposable;

    	[Token(Token = "0x4000540")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private CompositeDisposable _itemButtonDisposables;

    	// Shared generic code: every member below is the single `WitchBookPageBase<__Il2CppFullySharedGenericType, object>`
    	// instantiation (RVAs from dump.cs GenericInstMethod).  Field offsets are fixed because all fields are references:
    	// +0x30 _witchBookDataAsset ... +0xB0 _itemButtonDisposables (MapPage's first own field is +0xB8).
    	// Annotated disassembly: work/title-path/disas/wbpb_*.txt (regenerate with work/reversing/disas.py).

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4D100, 241 B -- LocalizationProvider (getter inlined) then
    	//   ILocalizationProvider slot 1 `CurrentLocale`.
    	[Token(Token = "0x17000098")]
    	protected LocaleKind CurrentLocale => LocalizationProvider.CurrentLocale;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4D200, 186 B -- lazy +0x50 via ServiceLocator.Get<IAddressablesManager>(true).
    	[Token(Token = "0x17000099")]
    	private IAddressableAssetLoader AddressableAssetLoader =>
    		_addressableAssetLoader ??= ServiceLocator.Get<IAddressablesManager>(includeDerivedType: true);

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4D2C0, 186 B -- lazy +0x58 via ServiceLocator.Get<IInputManager>(true).
    	[Token(Token = "0x1700009A")]
    	private GigaCreation.Essentials.Input.IInputManager InputManager =>
    		_inputManager ??= ServiceLocator.Get<GigaCreation.Essentials.Input.IInputManager>(includeDerivedType: true);

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4D380, 186 B -- lazy +0x60 via ServiceLocator.Get<ILocalizationManager>(true)
    	//   (GigaCreation's manager, which is an ILocalizationProvider).
    	[Token(Token = "0x1700009B")]
    	private ILocalizationProvider LocalizationProvider =>
    		_localizationProvider ??= ServiceLocator.Get<GigaCreation.Essentials.Localization.ILocalizationManager>(includeDerivedType: true);

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4D440, 186 B -- lazy +0x68 via Engine.GetServiceOrErr<IStateManager>().
    	[Token(Token = "0x1700009C")]
    	private IStateManager NaniStateManager => _naniStateManager ??= Engine.GetServiceOrErr<IStateManager>();

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4D500, 270 B -- IStateManager slot 24 AddOnGameSerializeTask(SerializeState),
    	//   slot 26 AddOnGameDeserializeTask(DeserializeState); delegates built from RGCTX slots 0x18 / 0x20.
    	[Token(Token = "0x60004A5")]
    	private void Awake()
    	{
    		NaniStateManager.AddOnGameSerializeTask(SerializeState);
    		NaniStateManager.AddOnGameDeserializeTask(DeserializeState);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4D610, 270 B -- mirror of Awake with slots 25 / 27 (Remove...).
    	[Token(Token = "0x60004A6")]
    	private void OnDestroy()
    	{
    		NaniStateManager.RemoveOnGameSerializeTask(SerializeState);
    		NaniStateManager.RemoveOnGameDeserializeTask(DeserializeState);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4D720, 292 B -- dispose and clear +0xA8 and +0xB0.
    	[Token(Token = "0x60004A7")]
    	public override void Dispose()
    	{
    		_paginationDisposable?.Dispose();
    		_paginationDisposable = null;
    		_itemButtonDisposables?.Dispose();
    		_itemButtonDisposables = null;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4D850 (builder stub); MoveNext 0x9F4BC0, 1131 B
    	// - +0x70 = itemList, +0x78 = itemButtonPrefab, then `await LoadDataAsync(ct)` (virtual slot 16) into +0x88.
    	[Token(Token = "0x60004A8")]
    	[AsyncStateMachine(typeof(WitchBookPageBase<, >._003CInitializeAsync_003Ed__31))]
    	public override async Cysharp.Threading.Tasks.UniTask InitializeAsync(WitchBookItemList itemList, WitchBookItemButton itemButtonPrefab, [Optional] CancellationToken ct)
    	{
    		_itemList = itemList;
    		_itemButtonPrefab = itemButtonPrefab;
    		_loadedDataItemMap = await LoadDataAsync(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4DA90, 1453 B; lambdas <InitializeItems>b__32_0 0xB4F9F0 (ValidateAs
    	//   inlined: _state list Contains) and b__32_1 0xB4FA80 (virtual slot 13 = InformAt(index)).
    	// - +0xB0 disposed and replaced; +0x90 = `_loadedDataItemMap.Where(b__32_0).ToArray()`; +0x80 created when null.
    	// - Per displayable item: instantiate a button under +0x38 when the pool is short (`i > Count - 1`),
    	//   SetupItemButton (abstract), then `button._button.onClick.AsObservable()` (OnChoiceAsObservable inlined)
    	//   `.SubscribeWithState(i, b__32_1).AddTo(+0xB0, this)`.
    	// - Every surplus button gets ClearContent().
    	[Token(Token = "0x60004A9")]
    	public override void InitializeItems()
    	{
    		_itemButtonDisposables?.Dispose();
    		_itemButtonDisposables = new CompositeDisposable();
    		_displayableItemMaps = _loadedDataItemMap.Where(versioned => ValidateAs(versioned.IdVersionPair)).ToArray();
    		if (_itemButtons == null)
    		{
    			_itemButtons = new List<WitchBookItemButton>();
    		}
    		for (int i = 0; i < _displayableItemMaps.Length; i++)
    		{
    			if (i > _itemButtons.Count - 1)
    			{
    				_itemButtons.Add(UnityEngine.Object.Instantiate(_itemButtonPrefab, _itemButtonPool));
    			}
    			SetupItemButton(_itemButtons[i], _displayableItemMaps[i]);
    			_itemButtons[i].OnChoiceAsObservable()
    				.SubscribeWithState(i, (Unit _, int index) => InformAt(index))
    				.AddTo(_itemButtonDisposables, this);
    		}
    		for (int i = _displayableItemMaps.Length; i < _itemButtons.Count; i++)
    		{
    			_itemButtons[i].ClearContent();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4E040, 373 B -- ShowItemList (direct call), the present-evidence button
    	//   block of ControlPresentEvidenceUi inlined, then the base Show (Canvas / GraphicRaycaster enabled) inlined.
    	[Token(Token = "0x60004AA")]
    	public override void Show(bool asPresentEvidenceMode)
    	{
    		ShowItemList();
    		ControlPresentEvidenceUi(asPresentEvidenceMode);
    		base.Show(asPresentEvidenceMode);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4E1C0, 100 B -- base Hide inlined, then a tail jump to HideItemList.
    	[Token(Token = "0x60004AB")]
    	public override void Hide()
    	{
    		base.Hide();
    		HideItemList();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4E230, 127 B -- `_itemIds.Contains(id)` gates VersionedState.SetVersion.
    	[Token(Token = "0x60004AC")]
    	public override void UpdateVersion(string id, int version)
    	{
    		if (_itemIds.Contains(id))
    		{
    			_state.SetVersion(id, version);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4E2B0, 72 B -- VersionedState.ResetToDefault (`_list.Clear()`) inlined.
    	[Token(Token = "0x60004AD")]
    	public override void ClearState()
    	{
    		_state.ResetToDefault();
    	}

    	// RECONSTRUCTED | Confidence: A (warning text: B) | RVA: 0xB4E300, 503 B
    	// - `Length <= idx` logs a warning; the literal is stored mis-encoded in the metadata dump (only its shape,
    	//   "...{0}", survives), so the Japanese text here is the evident meaning, not the shipped bytes.
    	// - Otherwise +0xA0 = item.Id (getter inlined through the RGCTX field offset), then RefreshPageContent (slot 18).
    	[Token(Token = "0x60004AE")]
    	public override void InformAt(int idx)
    	{
    		if (idx >= _displayableItemMaps.Length)
    		{
    			UnityEngine.Debug.LogWarning(string.Format("指定されたインデックスは範囲外です。{0}", idx));
    			return;
    		}
    		_currentItemId = _displayableItemMaps[idx].Id;
    		RefreshPageContent(_displayableItemMaps[idx]);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4E500, 29 B -- tail jump to VersionedState.get_ActiveItems.
    	[Token(Token = "0x60004AF")]
    	public override IReadOnlyCollection<IdVersionPair> GetActiveEvidences()
    	{
    		return _state.ActiveItems;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4E520 (builder stub); MoveNext 0xA050F0, 1131 B
    	// - Unity-null +0x40 returns null; otherwise await RebuildableButton.OnClickAsync(ct) (result unused),
    	//   IInputManager slot 0 PlayResponseSfx(Submit) (the getter inlined), and return +0xA0.
    	[Token(Token = "0x60004B0")]
    	[AsyncStateMachine(typeof(WitchBookPageBase<, >._003COnPresentEvidenceAsync_003Ed__39))]
    	public override async Cysharp.Threading.Tasks.UniTask<string> OnPresentEvidenceAsync([Optional] CancellationToken ct)
    	{
    		if (!_presentEvidenceButton)
    		{
    			return null;
    		}
    		await _presentEvidenceButton.OnClickAsync(ct);
    		InputManager.PlayResponseSfx(ResponseKind.Submit);
    		return _currentItemId;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4E720 (builder stub); MoveNext 0x9F8FB0, 3396 B; lambdas
    	//   <>c.<LoadDataAsync>b__40_0 0x7B05A0 (`versioned.Id`) and b__40_1 0x7B0600 (`grouping.Key`).
    	// - AddressableAssetLoader (inlined) interface generic call GetOrLoadAddressableAsset<WitchBookDataBase<TItem>>(+0x30, ct).
    	// - Unity-null result: Debug.LogError(Format("Failed to load item data: {0}", +0x30)) and a null result.
    	// - Otherwise +0x98 = `Items.GroupBy(b__40_0).Select(b__40_1).ToArray()`, and Items (virtual slot 4) is returned.
    	[Token(Token = "0x60004B1")]
    	[AsyncStateMachine(typeof(WitchBookPageBase<, >._003CLoadDataAsync_003Ed__40))]
    	protected virtual async Cysharp.Threading.Tasks.UniTask<IList<VersionedItem<TItem>>> LoadDataAsync([Optional] CancellationToken ct)
    	{
    		WitchBookDataBase<TItem> data = await AddressableAssetLoader.GetOrLoadAddressableAsset<WitchBookDataBase<TItem>>(_witchBookDataAsset, ct);
    		if (!data)
    		{
    			UnityEngine.Debug.LogError(string.Format("Failed to load item data: {0}", _witchBookDataAsset));
    			return null;
    		}
    		_itemIds = data.Items.GroupBy(versioned => versioned.Id).Select(grouping => grouping.Key).ToArray();
    		return data.Items;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4E910, 319 B
    	// - `new VersionedState()` + Copy(+0x48), then StateMap.SetState<VersionedState>(copy, typeof(TState).Name):
    	//   the RGCTX type's MemberInfo slot 8 `Name`.  Shipped saves confirm the keys (InstanceID=ClueState,
    	//   MapState, NoteState, ProfileState, RuleState).
    	[Token(Token = "0x60004B2")]
    	private void SerializeState(GameStateMap stateMap)
    	{
    		var state = new VersionedState();
    		state.Copy(_state);
    		stateMap.SetState(state, typeof(TState).Name);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4EA50 (builder stub); MoveNext 0x800060, 989 B
    	// - GetState<VersionedState>(typeof(TState).Name): found -> _state.Copy(found); missing -> ResetToDefault
    	//   (`_list.Clear()` inlined); then `await UniTask.CompletedTask` (Cysharp awaiter, IUniTaskSource path).
    	[Token(Token = "0x60004B3")]
    	[AsyncStateMachine(typeof(WitchBookPageBase<, >._003CDeserializeState_003Ed__42))]
    	private async Naninovel.UniTask DeserializeState(GameStateMap stateMap)
    	{
    		VersionedState state = stateMap.GetState<VersionedState>(typeof(TState).Name);
    		if (state != null)
    		{
    			_state.Copy(state);
    		}
    		else
    		{
    			_state.ResetToDefault();
    		}
    		await Cysharp.Threading.Tasks.UniTask.CompletedTask;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4EBC0, 81 B -- tail jump to Enumerable.Contains<string>(+0x98, id).
    	[Token(Token = "0x60004B4")]
    	private bool ItemIdExists(string id)
    	{
    		return _itemIds.Contains(id);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4EC20, 94 B -- VersionedState.Validate (List<IdVersionPair>.Contains) inlined.
    	[Token(Token = "0x60004B5")]
    	private bool ValidateAs(IdVersionPair pair)
    	{
    		return _state.Validate(pair);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4EC80, 679 B; lambda <>c.<GetDisplayableIdVersionPairs>b__45_0 0x7B0650
    	//   (`versioned.IdVersionPair`) -- `+0x90.Select(b__45_0).ToArray()`.
    	[Token(Token = "0x60004B6")]
    	protected IdVersionPair[] GetDisplayableIdVersionPairs()
    	{
    		return _displayableItemMaps.Select(versioned => versioned.IdVersionPair).ToArray();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4EF30, 1361 B; lambda <ShowItemList>b__46_0 0xB4FAB0
    	// - +0xA8 disposed (not cleared); each displayable button's RectTransform -> SetParent(itemList.ContentRoot, false)
    	//   (icall, worldPositionStays 0).
    	// - `_itemList.ExecuteRebuildingAsync(destroyCancellationToken)` (state machine start inlined) .Forget().
    	// - Pagination.Present(_itemButtons.Count - 9, true): one page per item step, MaxShownItemListCount (10) visible.
    	// - +0xA8 = Pagination.OnPageChangedAsObservable() (`_currentPageIndex.AsObservable()` inlined)
    	//   .Subscribe(b__46_0).AddTo(this); b__46_0 sets ContentRoot.anchoredPosition.x = -ItemInterval * pageIdx, keeping y.
    	[Token(Token = "0x60004B7")]
    	private void ShowItemList()
    	{
    		_paginationDisposable?.Dispose();
    		for (int i = 0; i < _displayableItemMaps.Length; i++)
    		{
    			_itemButtons[i].RectTransform.SetParent(_itemList.ContentRoot, false);
    		}
    		_itemList.ExecuteRebuildingAsync(destroyCancellationToken).Forget();
    		_itemList.Pagination.Present(_itemButtons.Count - (MaxShownItemListCount - 1), true);
    		_paginationDisposable = _itemList.Pagination.OnPageChangedAsObservable()
    			.Subscribe(pageIdx =>
    			{
    				RectTransform contentRoot = _itemList.ContentRoot;
    				contentRoot.anchoredPosition = new Vector2(-_itemList.ItemInterval * pageIdx, contentRoot.anchoredPosition.y);
    			})
    			.AddTo(this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4F490, 850 B
    	// - +0xA8 disposed and cleared; a null +0x80 returns; foreach button (List.Enumerator): RectTransform
    	//   SetParent(+0x38, false) and anchoredPosition = Vector2.zero (set_anchoredPosition_Injected icall).
    	[Token(Token = "0x60004B8")]
    	private void HideItemList()
    	{
    		_paginationDisposable?.Dispose();
    		_paginationDisposable = null;
    		if (_itemButtons == null)
    		{
    			return;
    		}
    		foreach (WitchBookItemButton button in _itemButtons)
    		{
    			button.RectTransform.SetParent(_itemButtonPool, false);
    			button.RectTransform.anchoredPosition = Vector2.zero;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4F7F0, 312 B -- Unity-null +0x40, then WitchTrialsButton.Show/Hide inlined
    	//   (its +0x70 Canvas and +0x78 GraphicRaycaster enabled = isModeEnabled).
    	[Token(Token = "0x60004B9")]
    	private void ControlPresentEvidenceUi(bool isModeEnabled)
    	{
    		if (!_presentEvidenceButton)
    		{
    			return;
    		}
    		if (isModeEnabled)
    		{
    			_presentEvidenceButton.Show();
    		}
    		else
    		{
    			_presentEvidenceButton.Hide();
    		}
    	}

    	[Token(Token = "0x60004BA")]
    	protected abstract void SetupItemButton(WitchBookItemButton button, VersionedItem<TItem> map);

    	[Token(Token = "0x60004BB")]
    	protected abstract void RefreshPageContent(VersionedItem<TItem> map);

    	// RECONSTRUCTED | Confidence: A | RVA: 0xB4F930, 178 B -- the `_state` field initializer
    	//   (Activator.CreateInstance<TState>() = `new TState()`), then the MonoBehaviour base ctor.
    	[Token(Token = "0x60004BC")]
    	protected WitchBookPageBase()
    	{
    	}
    }
}
