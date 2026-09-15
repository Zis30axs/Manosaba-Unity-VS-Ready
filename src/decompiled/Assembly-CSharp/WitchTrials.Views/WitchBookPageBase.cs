using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.AddressablesUtils;
using GigaCreation.Essentials.Input;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using WitchTrials.Models;

namespace WitchTrials.Views;

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
	private readonly TState _state;

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

	[Token(Token = "0x17000098")]
	protected LocaleKind CurrentLocale
	{
		[Token(Token = "0x60004A0")]
		get
		{
			return default(LocaleKind);
		}
	}

	[Token(Token = "0x17000099")]
	private IAddressableAssetLoader AddressableAssetLoader
	{
		[Token(Token = "0x60004A1")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700009A")]
	private GigaCreation.Essentials.Input.IInputManager InputManager
	{
		[Token(Token = "0x60004A2")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700009B")]
	private ILocalizationProvider LocalizationProvider
	{
		[Token(Token = "0x60004A3")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700009C")]
	private IStateManager NaniStateManager
	{
		[Token(Token = "0x60004A4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60004A5")]
	private void Awake()
	{
	}

	[Token(Token = "0x60004A6")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x60004A7")]
	public override void Dispose()
	{
	}

	[Token(Token = "0x60004A8")]
	[AsyncStateMachine(typeof(WitchBookPageBase<, >._003CInitializeAsync_003Ed__31))]
	public override Cysharp.Threading.Tasks.UniTask InitializeAsync(WitchBookItemList itemList, WitchBookItemButton itemButtonPrefab, [Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x60004A9")]
	public override void InitializeItems()
	{
	}

	[Token(Token = "0x60004AA")]
	public override void Show(bool asPresentEvidenceMode)
	{
	}

	[Token(Token = "0x60004AB")]
	public override void Hide()
	{
	}

	[Token(Token = "0x60004AC")]
	public override void UpdateVersion(string id, int version)
	{
	}

	[Token(Token = "0x60004AD")]
	public override void ClearState()
	{
	}

	[Token(Token = "0x60004AE")]
	public override void InformAt(int idx)
	{
	}

	[Token(Token = "0x60004AF")]
	public override IReadOnlyCollection<IdVersionPair> GetActiveEvidences()
	{
		return null;
	}

	[Token(Token = "0x60004B0")]
	[AsyncStateMachine(typeof(WitchBookPageBase<, >._003COnPresentEvidenceAsync_003Ed__39))]
	public override Cysharp.Threading.Tasks.UniTask<string> OnPresentEvidenceAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask<string>);
	}

	[Token(Token = "0x60004B1")]
	[AsyncStateMachine(typeof(WitchBookPageBase<, >._003CLoadDataAsync_003Ed__40))]
	protected virtual Cysharp.Threading.Tasks.UniTask<IList<VersionedItem<TItem>>> LoadDataAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask<IList<VersionedItem<TItem>>>);
	}

	[Token(Token = "0x60004B2")]
	private void SerializeState(GameStateMap stateMap)
	{
	}

	[Token(Token = "0x60004B3")]
	[AsyncStateMachine(typeof(WitchBookPageBase<, >._003CDeserializeState_003Ed__42))]
	private Naninovel.UniTask DeserializeState(GameStateMap stateMap)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x60004B4")]
	private bool ItemIdExists(string id)
	{
		return default(bool);
	}

	[Token(Token = "0x60004B5")]
	private bool ValidateAs(IdVersionPair pair)
	{
		return default(bool);
	}

	[Token(Token = "0x60004B6")]
	protected IdVersionPair[] GetDisplayableIdVersionPairs()
	{
		return null;
	}

	[Token(Token = "0x60004B7")]
	private void ShowItemList()
	{
	}

	[Token(Token = "0x60004B8")]
	private void HideItemList()
	{
	}

	[Token(Token = "0x60004B9")]
	private void ControlPresentEvidenceUi(bool isModeEnabled)
	{
	}

	[Token(Token = "0x60004BA")]
	protected abstract void SetupItemButton(WitchBookItemButton button, VersionedItem<TItem> map);

	[Token(Token = "0x60004BB")]
	protected abstract void RefreshPageContent(VersionedItem<TItem> map);

	[Token(Token = "0x60004BC")]
	protected WitchBookPageBase()
	{
	}
}
