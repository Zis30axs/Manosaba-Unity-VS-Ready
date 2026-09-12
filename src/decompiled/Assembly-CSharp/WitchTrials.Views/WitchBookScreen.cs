using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WitchTrials.Views;

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
			[Token(Token = "0x60004E9")]
			[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
			get
			{
				return default(WitchBookCategory);
			}
		}

		[Token(Token = "0x170000A6")]
		public PageDataItem Item
		{
			[Token(Token = "0x60004EA")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
			get
			{
				return null;
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
			[Token(Token = "0x60004EC")]
			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000A8")]
		public WitchBookPageBase Page
		{
			[Token(Token = "0x60004ED")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
			get
			{
				return null;
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
	private IDictionary<WitchBookCategory, PageDataItem> PageData
	{
		[Token(Token = "0x60004CD")]
		[Address(RVA = "0x3799B0", Offset = "0x3783B0", VA = "0x1803799B0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700009E")]
	private GigaCreation.Essentials.Input.IInputManager InputManager
	{
		[Token(Token = "0x60004CE")]
		[Address(RVA = "0x379CE0", Offset = "0x3786E0", VA = "0x180379CE0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700009F")]
	private IDialogProvider DialogProvider
	{
		[Token(Token = "0x60004CF")]
		[Address(RVA = "0x379DB0", Offset = "0x3787B0", VA = "0x180379DB0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000A0")]
	private IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
	{
		[Token(Token = "0x60004D0")]
		[Address(RVA = "0x379E80", Offset = "0x378880", VA = "0x180379E80")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000A1")]
	private IAudioManager NaniAudioManager
	{
		[Token(Token = "0x60004D1")]
		[Address(RVA = "0x379F50", Offset = "0x378950", VA = "0x180379F50")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000A2")]
	public WitchBookPresentationMode PresentationMode
	{
		[Token(Token = "0x60004D2")]
		[Address(RVA = "0x37A020", Offset = "0x378A20", VA = "0x18037A020")]
		[CompilerGenerated]
		private get
		{
			return default(WitchBookPresentationMode);
		}
		[Token(Token = "0x60004D3")]
		[Address(RVA = "0x37A030", Offset = "0x378A30", VA = "0x18037A030")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170000A3")]
	public IReadOnlyList<WitchBookCategory> RestrictionCategories
	{
		[Token(Token = "0x60004D4")]
		[Address(RVA = "0x37A040", Offset = "0x378A40", VA = "0x18037A040")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x60004D5")]
		[Address(RVA = "0x37A050", Offset = "0x378A50", VA = "0x18037A050")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170000A4")]
	public IReadOnlyCollection<MapAreaKind> RestrictionAreas
	{
		[Token(Token = "0x60004D6")]
		[Address(RVA = "0x37A0B0", Offset = "0x378AB0", VA = "0x18037A0B0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x60004D7")]
		[Address(RVA = "0x37A0C0", Offset = "0x378AC0", VA = "0x18037A0C0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x60004D8")]
	[Address(RVA = "0x37A120", Offset = "0x378B20", VA = "0x18037A120")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x60004D9")]
	[Address(RVA = "0x37A130", Offset = "0x378B30", VA = "0x18037A130")]
	[AsyncStateMachine(typeof(_003CInitializeAsync_003Ed__37))]
	public Cysharp.Threading.Tasks.UniTask InitializeAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x60004DA")]
	[Address(RVA = "0x37A2D0", Offset = "0x378CD0", VA = "0x18037A2D0", Slot = "8")]
	protected override void BeginToPresent()
	{
	}

	[Token(Token = "0x60004DB")]
	[Address(RVA = "0x37A670", Offset = "0x379070", VA = "0x18037A670", Slot = "9")]
	protected override void EndToPresent()
	{
	}

	[Token(Token = "0x60004DC")]
	[Address(RVA = "0x37A720", Offset = "0x379120", VA = "0x18037A720", Slot = "10")]
	protected override void BeginToDismiss()
	{
	}

	[Token(Token = "0x60004DD")]
	[Address(RVA = "0x37A740", Offset = "0x379140", VA = "0x18037A740", Slot = "11")]
	protected override void EndToDismiss()
	{
	}

	[Token(Token = "0x60004DE")]
	[Address(RVA = "0x37A880", Offset = "0x379280", VA = "0x18037A880")]
	private void InitializePages()
	{
	}

	[Token(Token = "0x60004DF")]
	[Address(RVA = "0x37AE10", Offset = "0x379810", VA = "0x18037AE10")]
	private void ChangePage(WitchBookCategory category)
	{
	}

	[Token(Token = "0x60004E0")]
	[Address(RVA = "0x37AF60", Offset = "0x379960", VA = "0x18037AF60", Slot = "12")]
	protected override void PlayCancelSfx()
	{
	}

	[Token(Token = "0x60004E1")]
	[Address(RVA = "0x37B090", Offset = "0x379A90", VA = "0x18037B090")]
	public IReadOnlyCollection<IdVersionPair> GetActiveEvidences(WitchBookCategory category)
	{
		return null;
	}

	[Token(Token = "0x60004E2")]
	[Address(RVA = "0x37B110", Offset = "0x379B10", VA = "0x18037B110")]
	public void UpdateVersion(WitchBookCategory category, string id, int version)
	{
	}

	[Token(Token = "0x60004E3")]
	[Address(RVA = "0x37B1B0", Offset = "0x379BB0", VA = "0x18037B1B0")]
	public void ClearState(WitchBookCategory category)
	{
	}

	[Token(Token = "0x60004E4")]
	[Address(RVA = "0x37B230", Offset = "0x379C30", VA = "0x18037B230")]
	[AsyncStateMachine(typeof(_003CChoiceEvidenceAsync_003Ed__48))]
	public Cysharp.Threading.Tasks.UniTask<string> ChoiceEvidenceAsync(IReadOnlyList<WitchBookCategory> categories, [Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask<string>);
	}

	[Token(Token = "0x60004E5")]
	[Address(RVA = "0x37B490", Offset = "0x379E90", VA = "0x18037B490")]
	[AsyncStateMachine(typeof(_003COnPresentEvidenceAsync_003Ed__49))]
	private Cysharp.Threading.Tasks.UniTask<string> OnPresentEvidenceAsync(IReadOnlyList<WitchBookCategory> categories, [Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask<string>);
	}

	[Token(Token = "0x60004E6")]
	[Address(RVA = "0x37B6F0", Offset = "0x37A0F0", VA = "0x18037B6F0")]
	[AsyncStateMachine(typeof(_003CChoiceAreaAsync_003Ed__50))]
	public Cysharp.Threading.Tasks.UniTask<MapAreaKind> ChoiceAreaAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask<MapAreaKind>);
	}

	[Token(Token = "0x60004E7")]
	[Address(RVA = "0x37B8E0", Offset = "0x37A2E0", VA = "0x18037B8E0")]
	private void Dispose()
	{
	}

	[Token(Token = "0x60004E8")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public WitchBookScreen()
	{
	}
}
