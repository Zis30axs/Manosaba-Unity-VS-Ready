using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Dialogs.Models;
using GigaCreation.Essentials.Dialogs.Views;
using GigaCreation.Essentials.Input;
using GigaCreation.Essentials.Ui;
using GigaCreation.NaninovelExtender.Common;
using GigaCreation.NaninovelExtender.Ui;
using Il2CppDummyDll;
using Naninovel;
using UniRx;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    [Token(Token = "0x200011E")]
    public class WitchBookScreen : NaninovelScreen
    {
    	[Serializable]
    	[Token(Token = "0x200011F")]
    	public class PageDataCategoryItemMap
    	{
    		[Token(Token = "0x400056F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		[SerializeField]
    		private WitchBookCategory _category;

    		[Token(Token = "0x4000570")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		[SerializeField]
    		private PageDataItem _item;

    		[Token(Token = "0x170000A5")]
    		public WitchBookCategory Category
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x331650 (shared generic 4-byte getter, +0x10)
    			[Token(Token = "0x60004E9")]
    			[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
    			get
    			{
    				return _category;
    			}
    		}

    		[Token(Token = "0x170000A6")]
    		public PageDataItem Item
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (shared generic reference-field getter, +0x18)
    			[Token(Token = "0x60004EA")]
    			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    			get
    			{
    				return _item;
    			}
    		}

    		[Token(Token = "0x60004EB")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public PageDataCategoryItemMap()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x2000120")]
    	public class PageDataItem
    	{
    		[Token(Token = "0x4000571")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		[SerializeField]
    		private WitchTrialsTabToggle _tab;

    		[Token(Token = "0x4000572")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		[SerializeField]
    		private WitchBookPageBase _page;

    		[Token(Token = "0x170000A7")]
    		public WitchTrialsTabToggle Tab
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 (shared generic reference-field getter, +0x10)
    			[Token(Token = "0x60004EC")]
    			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    			get
    			{
    				return _tab;
    			}
    		}

    		[Token(Token = "0x170000A8")]
    		public WitchBookPageBase Page
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (shared generic reference-field getter, +0x18)
    			[Token(Token = "0x60004ED")]
    			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    			get
    			{
    				return _page;
    			}
    		}

    		[Token(Token = "0x60004EE")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public PageDataItem()
    		{
    		}
    	}

    	[Token(Token = "0x2000124")]
    	[CompilerGenerated]
    	private sealed class _003C_003Ec__DisplayClass49_0
    	{
    		[Token(Token = "0x400057A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public WitchBookScreen _003C_003E4__this;

    		[Token(Token = "0x400057B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CancellationTokenSource linkedCts;

    		[Token(Token = "0x60004F7")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public _003C_003Ec__DisplayClass49_0()
    		{
    		}

    		// RECONSTRUCTED | Confidence: A | RVA: 0x37BF60
    		// Compiler-generated body of the OnPresentEvidenceAsync lambda (written inline there):
    		// `<>4__this.PageData[category].Page.OnPresentEvidenceAsync(linkedCts.Token)` (slot 15, +0x228).
    		[Token(Token = "0x60004F8")]
    		[Address(RVA = "0x37BF60", Offset = "0x37A960", VA = "0x18037BF60")]
    		internal Cysharp.Threading.Tasks.UniTask<string> _003COnPresentEvidenceAsync_003Eb__0(WitchBookCategory category)
    		{
    			return default(Cysharp.Threading.Tasks.UniTask<string>);
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000125")]
    	[CompilerGenerated]
    	private struct _003CChoiceAreaAsync_003Ed__50
    {
    		[Token(Token = "0x400057C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400057D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<MapAreaKind> _003C_003Et__builder;

    		[Token(Token = "0x400057E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public WitchBookScreen _003C_003E4__this;

    		[Token(Token = "0x400057F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000580")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Cysharp.Threading.Tasks.UniTask<MapAreaKind>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60004F9")]
    		[Address(RVA = "0x37C020", Offset = "0x37AA20", VA = "0x18037C020", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60004FA")]
    		[Address(RVA = "0x37C550", Offset = "0x37AF50", VA = "0x18037C550", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000126")]
    	[CompilerGenerated]
    	private struct _003CChoiceEvidenceAsync_003Ed__48
    {
    		[Token(Token = "0x4000581")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000582")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<string> _003C_003Et__builder;

    		[Token(Token = "0x4000583")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public WitchBookScreen _003C_003E4__this;

    		[Token(Token = "0x4000584")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public IReadOnlyList<WitchBookCategory> categories;

    		[Token(Token = "0x4000585")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000586")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private string _003CevidenceId_003E5__2;

    		[Token(Token = "0x4000587")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private INaninovelInteractionBlocker _003Cblocker_003E5__3;

    		[Token(Token = "0x4000588")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private ProvidableDialog _003Cdialog_003E5__4;

    		[Token(Token = "0x4000589")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private Cysharp.Threading.Tasks.UniTask<string>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400058A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    		private object _003C_003E7__wrap4;

    		[Token(Token = "0x400058B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    		private int _003C_003E7__wrap5;

    		[Token(Token = "0x400058C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    		private Cysharp.Threading.Tasks.UniTask<ResponseKind>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x400058D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__3;

    		[Token(Token = "0x60004FB")]
    		[Address(RVA = "0x37C580", Offset = "0x37AF80", VA = "0x18037C580", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60004FC")]
    		[Address(RVA = "0x37D620", Offset = "0x37C020", VA = "0x18037D620", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000127")]
    	[CompilerGenerated]
    	private struct _003CInitializeAsync_003Ed__37
    {
    		[Token(Token = "0x400058E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400058F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000590")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public WitchBookScreen _003C_003E4__this;

    		[Token(Token = "0x4000591")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000592")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60004FD")]
    		[Address(RVA = "0x37D650", Offset = "0x37C050", VA = "0x18037D650", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60004FE")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000128")]
    	[CompilerGenerated]
    	private struct _003COnPresentEvidenceAsync_003Ed__49
    {
    		[Token(Token = "0x4000593")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000594")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<string> _003C_003Et__builder;

    		[Token(Token = "0x4000595")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public WitchBookScreen _003C_003E4__this;

    		[Token(Token = "0x4000596")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000597")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public IReadOnlyList<WitchBookCategory> categories;

    		[Token(Token = "0x4000598")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private _003C_003Ec__DisplayClass49_0 _003C_003E8__1;

    		[Token(Token = "0x4000599")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private Cysharp.Threading.Tasks.UniTask<(int winArgumentIndex, string result)>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60004FF")]
    		[Address(RVA = "0x37DA40", Offset = "0x37C440", VA = "0x18037DA40", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000500")]
    		[Address(RVA = "0x37E280", Offset = "0x37CC80", VA = "0x18037E280", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400055E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
    	[Header("Assets for WitchBook")]
    	[SerializeField]
    	private WitchBookItemButton _itemButtonPrefab;

    	[Token(Token = "0x400055F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
    	[Header("References for WitchBook")]
    	[SerializeField]
    	private CanvasGroup _closeButtonCanvasGroup;

    	[Token(Token = "0x4000560")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
    	[SerializeField]
    	private WitchBookItemList _itemList;

    	[Token(Token = "0x4000561")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
    	[Header("Parameters for WitchBook")]
    	[ResourcePopup("Audio")]
    	[SerializeField]
    	private string _closeSfxPath;

    	[Token(Token = "0x4000562")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
    	[Space]
    	[SerializeField]
    	private PageDataCategoryItemMap[] _pageDataMap;

    	[Token(Token = "0x4000563")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
    	[SerializeField]
    	private DialogData _confirmToChoiceEvidenceDialogData;

    	[Token(Token = "0x4000564")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
    	private GigaCreation.Essentials.Input.IInputManager _inputManager;

    	[Token(Token = "0x4000565")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
    	private IDialogProvider _dialogProvider;

    	[Token(Token = "0x4000566")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
    	private IWitchTrialsNaninovelManager _witchTrialsNaninovelManager;

    	[Token(Token = "0x4000567")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
    	private IAudioManager _naniAudioManager;

    	[Token(Token = "0x4000568")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
    	private WitchBookCategory _currentPage;

    	[Token(Token = "0x4000569")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
    	private IDictionary<WitchBookCategory, PageDataItem> _pageData;

    	[Token(Token = "0x400056A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
    	private IDisposable _cancelEventBlocker;

    	[Token(Token = "0x400056B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
    	private CompositeDisposable _disposables;

    	[Token(Token = "0x1700009D")]
    	// RECONSTRUCTED | Confidence: A | RVA: 0x3799B0
    	//
    	// Lazily-cached: `_pageData ??= _pageDataMap.ToDictionary(m => m.Category,
    	// m => m.Item);`. Most of this 807-byte function is Roslyn's standard
    	// "cache the static lambda in a field of the compiler-generated <>c class"
    	// boilerplate for the two non-capturing lambdas passed to ToDictionary
    	// (`<>9__15_0`/`<>9__15_1`, allocated once and reused on subsequent calls)
    	// -- not meaningful logic in its own right, just how C# compiles a
    	// LINQ call with lambda arguments that capture nothing.
    	private IDictionary<WitchBookCategory, PageDataItem> PageData
    	{
    		[Token(Token = "0x60004CD")]
    		[Address(RVA = "0x3799B0", Offset = "0x3783B0", VA = "0x1803799B0")]
    		get
    		{
    			return _pageData ??= _pageDataMap.ToDictionary(m => m.Category, m => m.Item);
    		}
    	}

    	[Token(Token = "0x1700009E")]
    	private GigaCreation.Essentials.Input.IInputManager InputManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x379CE0
    		// Lazy cache in +0xB8 via `ServiceLocator.Get<IInputManager>(true)` (`cl = 1`).
    		[Token(Token = "0x60004CE")]
    		[Address(RVA = "0x379CE0", Offset = "0x3786E0", VA = "0x180379CE0")]
    		get
    		{
    			return _inputManager ??= GigaCreation.Tools.Service.ServiceLocator.Get<GigaCreation.Essentials.Input.IInputManager>(true);
    		}
    	}

    	[Token(Token = "0x1700009F")]
    	private IDialogProvider DialogProvider
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x379DB0
    		// Lazy cache in +0xC0 via `ServiceLocator.Get<IDialogManager>(true)` (METHODINFO
    		// `ServiceLocator.Get<IDialogManager>()`), stored as `IDialogProvider` -- same shape
    		// as SaveLoadUi.DialogProvider.
    		[Token(Token = "0x60004CF")]
    		[Address(RVA = "0x379DB0", Offset = "0x3787B0", VA = "0x180379DB0")]
    		get
    		{
    			return _dialogProvider ??= GigaCreation.Tools.Service.ServiceLocator.Get<IDialogManager>(true);
    		}
    	}

    	[Token(Token = "0x170000A0")]
    	private IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x379E80, 202 B
    		// Lazy cache in +0xC8 via `ServiceLocator.Get<IWitchTrialsNaninovelManager>(true)`.
    		[Token(Token = "0x60004D0")]
    		[Address(RVA = "0x379E80", Offset = "0x378880", VA = "0x180379E80")]
    		get
    		{
    			return _witchTrialsNaninovelManager ??= GigaCreation.Tools.Service.ServiceLocator.Get<IWitchTrialsNaninovelManager>(true);
    		}
    	}

    	[Token(Token = "0x170000A1")]
    	private IAudioManager NaniAudioManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x379F50
    		// Lazy cache in +0xD0 via `Engine.GetServiceOrErr<IAudioManager>()`.
    		[Token(Token = "0x60004D1")]
    		[Address(RVA = "0x379F50", Offset = "0x378950", VA = "0x180379F50")]
    		get
    		{
    			return _naniAudioManager ??= Engine.GetServiceOrErr<IAudioManager>();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37A020 get, 0x37A030 set
    	// [CompilerGenerated] auto-property, backing field +0xF8 (4-byte load/store).
    	[Token(Token = "0x170000A2")]
    	public WitchBookPresentationMode PresentationMode
    	{
    		[Token(Token = "0x60004D2")]
    		[Address(RVA = "0x37A020", Offset = "0x378A20", VA = "0x18037A020")]
    		[CompilerGenerated]
    		private get;
    		[Token(Token = "0x60004D3")]
    		[Address(RVA = "0x37A030", Offset = "0x378A30", VA = "0x18037A030")]
    		[CompilerGenerated]
    		set;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37A040 get, 0x37A050 set
    	// [CompilerGenerated] auto-property, backing field +0x100 (setter carries the GC write barrier).
    	[Token(Token = "0x170000A3")]
    	public IReadOnlyList<WitchBookCategory> RestrictionCategories
    	{
    		[Token(Token = "0x60004D4")]
    		[Address(RVA = "0x37A040", Offset = "0x378A40", VA = "0x18037A040")]
    		[CompilerGenerated]
    		get;
    		[Token(Token = "0x60004D5")]
    		[Address(RVA = "0x37A050", Offset = "0x378A50", VA = "0x18037A050")]
    		[CompilerGenerated]
    		set;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37A0B0 get, 0x37A0C0 set
    	// [CompilerGenerated] auto-property, backing field +0x108 (setter carries the GC write barrier).
    	[Token(Token = "0x170000A4")]
    	public IReadOnlyCollection<MapAreaKind> RestrictionAreas
    	{
    		[Token(Token = "0x60004D6")]
    		[Address(RVA = "0x37A0B0", Offset = "0x378AB0", VA = "0x18037A0B0")]
    		[CompilerGenerated]
    		get;
    		[Token(Token = "0x60004D7")]
    		[Address(RVA = "0x37A0C0", Offset = "0x378AC0", VA = "0x18037A0C0")]
    		[CompilerGenerated]
    		set;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37A120
    	// Tail-jumps to Dispose (0x37B8E0).
    	[Token(Token = "0x60004D8")]
    	[Address(RVA = "0x37A120", Offset = "0x378B20", VA = "0x18037A120")]
    	private void OnDestroy()
    	{
    		Dispose();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37A130 (stub), MoveNext 0x37D650, lambda 0x37BC70 (<>c__DisplayClass37_0)
    	// Captures `this` and `ct`; `PageData` (slot 3 of IDictionary = Values) -> Enumerable.Select with
    	// `item => item.Page.InitializeAsync(_itemList, _itemButtonPrefab, ct)` (WitchBookPageBase slot 7,
    	// +0x1A8; fields +0x98, +0x88), then Cysharp `UniTaskExtensions.GetAwaiter(IEnumerable<UniTask>)`
    	// (0x1818204B0), i.e. `await` over the sequence (WhenAll).
    	[Token(Token = "0x60004D9")]
    	[Address(RVA = "0x37A130", Offset = "0x378B30", VA = "0x18037A130")]
    	[AsyncStateMachine(typeof(_003CInitializeAsync_003Ed__37))]
    	public async Cysharp.Threading.Tasks.UniTask InitializeAsync([Optional] CancellationToken ct)
    	{
    		await PageData.Values.Select(item => item.Page.InitializeAsync(_itemList, _itemButtonPrefab, ct));
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37A2D0
    	// 1. IDisposable slot 0 on `_disposables` (+0xF0) when non-null; new CompositeDisposable into +0xF0.
    	// 2. NaninovelScreen.BeginToPresent inlined (SetGraphicRaycastersEnabled(false), SetCanvasesEnabled(true),
    	//    `_onBeginToPresent` +0x38 invoke) -- byte-identical to base 0xD3CF30, written as `base.BeginToPresent()`.
    	// 3. IWitchTrialsNaninovelManager slot 1 (IsPresentEvidenceMode) into bl, then slot 0 (IsInDebate);
    	//    `or al, bl` gates slot 7 ChangeTrialPauseMode(true, true). Both getters run before the test.
    	// 4. `_closeButtonCanvasGroup` (+0x90): alpha = bl ? 0f : 1f; blocksRaycasts = bl ^ 1.
    	// 5. When bl: `new CancelEventBlocker(new ICancelEventBlockable[] { this })` (array ctor 0xCDB6B0) into +0xE8.
    	// 6. `_currentPage` (+0xD8) = RestrictionCategories (+0x100) non-null with IReadOnlyCollection Count != 0
    	//    ? IReadOnlyList slot 0 [0] : 0 (WitchBookCategory.Clue); then InitializePages (0x37A880).
    	// Spelling of the `|` and the local `isPresentEvidenceMode` is grade B; the dispatch order is exact.
    	[Token(Token = "0x60004DA")]
    	[Address(RVA = "0x37A2D0", Offset = "0x378CD0", VA = "0x18037A2D0", Slot = "8")]
    	protected override void BeginToPresent()
    	{
    		_disposables?.Dispose();
    		_disposables = new CompositeDisposable();
    		base.BeginToPresent();
    		bool isPresentEvidenceMode = WitchTrialsNaninovelManager.IsPresentEvidenceMode;
    		if (WitchTrialsNaninovelManager.IsInDebate | isPresentEvidenceMode)
    		{
    			WitchTrialsNaninovelManager.ChangeTrialPauseMode(true, true);
    		}
    		_closeButtonCanvasGroup.alpha = isPresentEvidenceMode ? 0f : 1f;
    		_closeButtonCanvasGroup.blocksRaycasts = !isPresentEvidenceMode;
    		if (isPresentEvidenceMode)
    		{
    			_cancelEventBlocker = new CancelEventBlocker(new ICancelEventBlockable[] { this });
    		}
    		_currentPage = (RestrictionCategories != null && RestrictionCategories.Count != 0) ? RestrictionCategories[0] : WitchBookCategory.Clue;
    		InitializePages();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37A670
    	// NaninovelScreen.EndToPresent inlined (byte-identical to base 0xD3CF70: raycasters on, EnableCancelEvent,
    	// `_onEndToPresent` +0x40 invoke), then IsPresentEvidenceMode (slot 1) gates ChangeTrialPauseMode(false, true).
    	[Token(Token = "0x60004DB")]
    	[Address(RVA = "0x37A670", Offset = "0x379070", VA = "0x18037A670", Slot = "9")]
    	protected override void EndToPresent()
    	{
    		base.EndToPresent();
    		if (WitchTrialsNaninovelManager.IsPresentEvidenceMode)
    		{
    			WitchTrialsNaninovelManager.ChangeTrialPauseMode(false, true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37A720
    	// Real call to NaninovelScreen.BeginToDismiss (0xD3D180), then tail-jump to Dispose (0x37B8E0).
    	[Token(Token = "0x60004DC")]
    	[Address(RVA = "0x37A720", Offset = "0x379120", VA = "0x18037A720", Slot = "10")]
    	protected override void BeginToDismiss()
    	{
    		base.BeginToDismiss();
    		Dispose();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37A740
    	// NaninovelScreen.EndToDismiss inlined (byte-identical to base 0xD3D330: SetCanvasesEnabled(false),
    	// `_onEndToDismiss` +0x50 invoke). Then IDictionary get_Item(`_currentPage`) -> Page -> slot 10 Hide (+0x1D8);
    	// IsInDebate (slot 0) -> ChangeTrialPauseMode(false, true), else IsPresentEvidenceMode (slot 1) ->
    	// ChangeTrialPauseMode(false, false).
    	[Token(Token = "0x60004DD")]
    	[Address(RVA = "0x37A740", Offset = "0x379140", VA = "0x18037A740", Slot = "11")]
    	protected override void EndToDismiss()
    	{
    		base.EndToDismiss();
    		PageData[_currentPage].Page.Hide();
    		if (WitchTrialsNaninovelManager.IsInDebate)
    		{
    			WitchTrialsNaninovelManager.ChangeTrialPauseMode(false, true);
    		}
    		else if (WitchTrialsNaninovelManager.IsPresentEvidenceMode)
    		{
    			WitchTrialsNaninovelManager.ChangeTrialPauseMode(false, false);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37A880, lambda 0x37BCE0 (<>c__DisplayClass42_0.<InitializePages>b__0)
    	// Enumerates PageData (IEnumerable<KeyValuePair<,>> GetEnumerator/MoveNext/Current); a fresh DisplayClass42_0
    	// per iteration captures `this` and the pair's Key.
    	// - RestrictionCategories non-null and !Enumerable.Contains -> WitchTrialsTabToggle.Hide (0x30D1A0),
    	//   page slot 10 Hide, continue.
    	// - Page slot 8 InitializeItems; `isinst MapPage` -> MapPage.InitializeAreas(RestrictionAreas +0x108).
    	// - Key != _currentPage -> slot 10 Hide; else Toggle.Set(true, true) (= inlined set_isOn), slot 13
    	//   InformAt(0), slot 9 Show(IsPresentEvidenceMode).
    	// - UniRx `OnValueChangedAsObservable()` -> Subscribe(lambda) -> AddTo(_disposables, this), then
    	//   WitchTrialsTabToggle.Show (0x30D160).
    	// Lambda: `isOn && _currentPage != category` -> IInputManager slot 0 PlayResponseSfx(3 = Tab), ChangePage.
    	// Deconstruction into `category`/`item` locals is grade B spelling.
    	[Token(Token = "0x60004DE")]
    	[Address(RVA = "0x37A880", Offset = "0x379280", VA = "0x18037A880")]
    	private void InitializePages()
    	{
    		foreach (KeyValuePair<WitchBookCategory, PageDataItem> pair in PageData)
    		{
    			WitchBookCategory category = pair.Key;
    			PageDataItem item = pair.Value;
    			if (RestrictionCategories != null && !RestrictionCategories.Contains(category))
    			{
    				item.Tab.Hide();
    				item.Page.Hide();
    				continue;
    			}
    			item.Page.InitializeItems();
    			if (item.Page is MapPage mapPage)
    			{
    				mapPage.InitializeAreas(RestrictionAreas);
    			}
    			if (category != _currentPage)
    			{
    				item.Page.Hide();
    			}
    			else
    			{
    				item.Tab.isOn = true;
    				item.Page.InformAt(0);
    				item.Page.Show(WitchTrialsNaninovelManager.IsPresentEvidenceMode);
    			}
    			item.Tab.OnValueChangedAsObservable()
    				.Subscribe(isOn =>
    				{
    					if (isOn && _currentPage != category)
    					{
    						InputManager.PlayResponseSfx(ResponseKind.Tab);
    						ChangePage(category);
    					}
    				})
    				.AddTo(_disposables, this);
    			item.Tab.Show();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37AE10
    	// get_Item(_currentPage).Page slot 10 Hide; store `category` into +0xD8; get_Item(category).Page slot 13
    	// InformAt(0); get_Item(category).Page slot 9 Show(IsPresentEvidenceMode). PageData is re-read per access.
    	[Token(Token = "0x60004DF")]
    	[Address(RVA = "0x37AE10", Offset = "0x379810", VA = "0x18037AE10")]
    	private void ChangePage(WitchBookCategory category)
    	{
    		PageData[_currentPage].Page.Hide();
    		_currentPage = category;
    		PageData[category].Page.InformAt(0);
    		PageData[category].Page.Show(WitchTrialsNaninovelManager.IsPresentEvidenceMode);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37AF60
    	// NaniAudioManager (getter inlined) slot 19 `PlaySfx(_closeSfxPath (+0xA0), 1f, 0f, false, null, default)`.
    	// Unlike SaveLoadUi there is no ForgetCore call: the returned UniTask is simply discarded.
    	[Token(Token = "0x60004E0")]
    	[Address(RVA = "0x37AF60", Offset = "0x379960", VA = "0x18037AF60", Slot = "12")]
    	protected override void PlayCancelSfx()
    	{
    		NaniAudioManager.PlaySfx(_closeSfxPath);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37B090
    	// get_Item(category).Page slot 14 GetActiveEvidences (+0x218).
    	[Token(Token = "0x60004E1")]
    	[Address(RVA = "0x37B090", Offset = "0x379A90", VA = "0x18037B090")]
    	public IReadOnlyCollection<IdVersionPair> GetActiveEvidences(WitchBookCategory category)
    	{
    		return PageData[category].Page.GetActiveEvidences();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37B110
    	// get_Item(category).Page slot 11 UpdateVersion(id, version) (+0x1E8).
    	[Token(Token = "0x60004E2")]
    	[Address(RVA = "0x37B110", Offset = "0x379B10", VA = "0x18037B110")]
    	public void UpdateVersion(WitchBookCategory category, string id, int version)
    	{
    		PageData[category].Page.UpdateVersion(id, version);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37B1B0
    	// get_Item(category).Page slot 12 ClearState (+0x1F8).
    	[Token(Token = "0x60004E3")]
    	[Address(RVA = "0x37B1B0", Offset = "0x379BB0", VA = "0x18037B1B0")]
    	public void ClearState(WitchBookCategory category)
    	{
    		PageData[category].Page.ClearState();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37B230 (stub), MoveNext 0x37C580
    	// Hoisted: evidenceId +0x38, blocker +0x40, dialog +0x48, wrap4 (pending exception) +0x68,
    	// wrap5 (return flag) +0x70 -- the lowering of `await` inside a finally.
    	// Loop: state -1 sets evidenceId = null and jumps to the ct.IsCancellationRequested check (0x37D1C9);
    	// body awaits OnPresentEvidenceAsync (0x37D20F). Empty id -> Debug.LogWarning("証拠 ID を取得できませんでした。")
    	// and result null. Else `new NaninovelInteractionBlocker()` (0xCFBE30, parameterless), DialogProvider
    	// GetOrAddDialog, INaninovelInteractionBlocker.SetSortingOrder(dialog.SortingOrder - 1), await
    	// ConfirmAsync(_confirmToChoiceEvidenceDialogData +0xB0, null, ct); Submit (1) sets wrap5 = 1.
    	// Finally: dialog non-null -> inlined async DisposeAsync awaited (u__3); then blocker IDisposable.Dispose.
    	// wrap5 == 1 returns evidenceId; otherwise both locals cleared (0x37D110) and the loop repeats.
    	// Falling out on cancellation returns evidenceId. The explicit try/finally is required (ProvidableDialog is IUniTaskAsyncDisposable, not IAsyncDisposable).
    	[Token(Token = "0x60004E4")]
    	[Address(RVA = "0x37B230", Offset = "0x379C30", VA = "0x18037B230")]
    	[AsyncStateMachine(typeof(_003CChoiceEvidenceAsync_003Ed__48))]
    	public async Cysharp.Threading.Tasks.UniTask<string> ChoiceEvidenceAsync(IReadOnlyList<WitchBookCategory> categories, [Optional] CancellationToken ct)
    	{
    		string evidenceId = null;
    		while (!ct.IsCancellationRequested)
    		{
    			evidenceId = await OnPresentEvidenceAsync(categories, ct);
    			if (string.IsNullOrEmpty(evidenceId))
    			{
    				UnityEngine.Debug.LogWarning("証拠 ID を取得できませんでした。");
    				return null;
    			}
    			using INaninovelInteractionBlocker blocker = new NaninovelInteractionBlocker();
    			ProvidableDialog dialog = DialogProvider.GetOrAddDialog();
    			try
    			{
    				blocker.SetSortingOrder(dialog.SortingOrder - 1);
    				if (await dialog.ConfirmAsync(_confirmToChoiceEvidenceDialogData, null, ct) == ResponseKind.Submit)
    				{
    					return evidenceId;
    				}
    			}
    			finally
    			{
    				if (dialog != null)
    				{
    					await dialog.DisposeAsync();
    				}
    			}
    		}
    		return evidenceId;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37B490 (stub), MoveNext 0x37DA40, lambda 0x37BF60
    	// DisplayClass49_0 captures `this` and `linkedCts` = CancellationTokenSource.CreateLinkedTokenSource(ct).
    	// `categories.Select(lambda).ToArray()` -> UniTask.WhenAny<string>(UniTask<string>[]) awaited as
    	// `(int winArgumentIndex, string result)`; then linkedCts.Cancel(); returns `result`; linkedCts
    	// disposed in the `using` finally.
    	[Token(Token = "0x60004E5")]
    	[Address(RVA = "0x37B490", Offset = "0x379E90", VA = "0x18037B490")]
    	[AsyncStateMachine(typeof(_003COnPresentEvidenceAsync_003Ed__49))]
    	private async Cysharp.Threading.Tasks.UniTask<string> OnPresentEvidenceAsync(IReadOnlyList<WitchBookCategory> categories, [Optional] CancellationToken ct)
    	{
    		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
    		var (winArgumentIndex, result) = await Cysharp.Threading.Tasks.UniTask.WhenAny(categories.Select(category => PageData[category].Page.OnPresentEvidenceAsync(linkedCts.Token)).ToArray());
    		linkedCts.Cancel();
    		return result;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37B6F0 (stub), MoveNext 0x37C020
    	// get_Item(_currentPage).Page `isinst MapPage` -> await MapPage.OnChoiceMapAreaAsync(ct); otherwise
    	// Debug.LogWarning("マップエリアの選択処理が実行されましたが、現在のページがマップではありません。") and 0.
    	[Token(Token = "0x60004E6")]
    	[Address(RVA = "0x37B6F0", Offset = "0x37A0F0", VA = "0x18037B6F0")]
    	[AsyncStateMachine(typeof(_003CChoiceAreaAsync_003Ed__50))]
    	public async Cysharp.Threading.Tasks.UniTask<MapAreaKind> ChoiceAreaAsync([Optional] CancellationToken ct)
    	{
    		if (PageData[_currentPage].Page is MapPage mapPage)
    		{
    			return await mapPage.OnChoiceMapAreaAsync(ct);
    		}
    		UnityEngine.Debug.LogWarning("マップエリアの選択処理が実行されましたが、現在のページがマップではありません。");
    		return default;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x37B8E0
    	// `_cancelEventBlocker` (+0xE8) IDisposable slot 0 when non-null, then cleared; `_disposables` (+0xF0)
    	// non-null -> Dispose and clear; then PageData.Values (IDictionary slot 3) enumerated, page slot 6 Dispose.
    	[Token(Token = "0x60004E7")]
    	[Address(RVA = "0x37B8E0", Offset = "0x37A2E0", VA = "0x18037B8E0")]
    	private void Dispose()
    	{
    		_cancelEventBlocker?.Dispose();
    		_cancelEventBlocker = null;
    		if (_disposables != null)
    		{
    			_disposables.Dispose();
    			_disposables = null;
    		}
    		foreach (PageDataItem item in PageData.Values)
    		{
    			item.Page.Dispose();
    		}
    	}

    	[Token(Token = "0x60004E8")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public WitchBookScreen()
    	{
    	}
    }
}
