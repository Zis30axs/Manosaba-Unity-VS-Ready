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
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x2000089")]
public class ControlPanel : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x200008A")]
	[CompilerGenerated]
	private struct _003CReturnToTitleAsync_003Ed__50
{
		[Token(Token = "0x4000235")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000236")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000237")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ControlPanel _003C_003E4__this;

		[Token(Token = "0x4000238")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000239")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private INaninovelInteractionBlocker _003Cblocker_003E5__2;

		[Token(Token = "0x400023A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private ProvidableDialog _003Cdialog_003E5__3;

		[Token(Token = "0x400023B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask<ResponseKind>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400023C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60001DE")]
		[Address(RVA = "0x339250", Offset = "0x337C50", VA = "0x180339250", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60001DF")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000218")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly int s_blink;

	[Token(Token = "0x4000219")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private NaninovelScriptPlayer _returnToTitleScript;

	[Token(Token = "0x400021A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[Space]
	[SerializeField]
	private CanvasGroup _menuCanvasGroup;

	[Token(Token = "0x400021B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Button _logButton;

	[Token(Token = "0x400021C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private Button _saveButton;

	[Token(Token = "0x400021D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[SerializeField]
	private Button _loadButton;

	[Token(Token = "0x400021E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[SerializeField]
	private Button _optionsButton;

	[Token(Token = "0x400021F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[SerializeField]
	private Button _titleButton;

	[Token(Token = "0x4000220")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[Space]
	[SerializeField]
	private CanvasGroup _homeCanvasGroup;

	[Token(Token = "0x4000221")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[SerializeField]
	private HintDisplay _hintDisplay;

	[Token(Token = "0x4000222")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	[SerializeField]
	private Animator _naviAnimator;

	[Token(Token = "0x4000223")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	[Space]
	[SerializeField]
	private CanvasGroup _openButtonCanvasGroup;

	[Token(Token = "0x4000224")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	[SerializeField]
	private Button _openButton;

	[Token(Token = "0x4000225")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	[SerializeField]
	private CanvasGroup _closeButtonCanvasGroup;

	[Token(Token = "0x4000226")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	[SerializeField]
	private Button _closeButton;

	[Token(Token = "0x4000227")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	[SerializeField]
	private Button _rayBlocker;

	[Token(Token = "0x4000228")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	[Header("Parameters")]
	[SerializeField]
	private DialogData _returnToTitleDialogData;

	[Token(Token = "0x4000229")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _openSfxPath;

	[Token(Token = "0x400022A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _closeSfxPath;

	[Token(Token = "0x400022B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
	private GigaCreation.Essentials.Input.IInputManager _inputManager;

	[Token(Token = "0x400022C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
	private IDialogProvider _dialogProvider;

	[Token(Token = "0x400022D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
	private Naninovel.IInputManager _naniInputManager;

	[Token(Token = "0x400022E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
	private IAudioManager _naniAudioManager;

	[Token(Token = "0x400022F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
	private IUIManager _naniUiManager;

	[Token(Token = "0x4000230")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
	private IScriptPlayer _naniScriptPlayer;

	[Token(Token = "0x4000231")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	private IBacklogUI _logUi;

	[Token(Token = "0x4000232")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	private ISaveLoadUI _saveLoadUi;

	[Token(Token = "0x4000233")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	private ISettingsUI _optionsUi;

	[Token(Token = "0x4000234")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	private bool _isOpened;

	[Token(Token = "0x1700002C")]
	private GigaCreation.Essentials.Input.IInputManager InputManager
	{
		[Token(Token = "0x60001C2")]
		[Address(RVA = "0x336FF0", Offset = "0x3359F0", VA = "0x180336FF0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700002D")]
	private IDialogProvider DialogProvider
	{
		[Token(Token = "0x60001C3")]
		[Address(RVA = "0x3370C0", Offset = "0x335AC0", VA = "0x1803370C0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700002E")]
	private Naninovel.IInputManager NaniInputManager
	{
		[Token(Token = "0x60001C4")]
		[Address(RVA = "0x337190", Offset = "0x335B90", VA = "0x180337190")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700002F")]
	private IAudioManager NaniAudioManager
	{
		[Token(Token = "0x60001C5")]
		[Address(RVA = "0x337260", Offset = "0x335C60", VA = "0x180337260")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000030")]
	private IUIManager NaniUiManager
	{
		[Token(Token = "0x60001C6")]
		[Address(RVA = "0x337330", Offset = "0x335D30", VA = "0x180337330")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000031")]
	private IScriptPlayer NaniScriptPlayer
	{
		[Token(Token = "0x60001C7")]
		[Address(RVA = "0x337400", Offset = "0x335E00", VA = "0x180337400")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000032")]
	private IBacklogUI LogUi
	{
		[Token(Token = "0x60001C8")]
		[Address(RVA = "0x3374D0", Offset = "0x335ED0", VA = "0x1803374D0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000033")]
	private ISaveLoadUI SaveLoadUi
	{
		[Token(Token = "0x60001C9")]
		[Address(RVA = "0x337590", Offset = "0x335F90", VA = "0x180337590")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000034")]
	private ISettingsUI OptionsUi
	{
		[Token(Token = "0x60001CA")]
		[Address(RVA = "0x337650", Offset = "0x336050", VA = "0x180337650")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60001CB")]
	[Address(RVA = "0x337710", Offset = "0x336110", VA = "0x180337710")]
	private void Start()
	{
	}

	[Token(Token = "0x60001CC")]
	[Address(RVA = "0x337D40", Offset = "0x336740", VA = "0x180337D40")]
	private void OnEnable()
	{
	}

	[Token(Token = "0x60001CD")]
	[Address(RVA = "0x337EB0", Offset = "0x3368B0", VA = "0x180337EB0")]
	private void OnDisable()
	{
	}

	[Token(Token = "0x60001CE")]
	[Address(RVA = "0x338030", Offset = "0x336A30", VA = "0x180338030")]
	[AsyncStateMachine(typeof(_003CReturnToTitleAsync_003Ed__50))]
	private Cysharp.Threading.Tasks.UniTask ReturnToTitleAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x60001CF")]
	[Address(RVA = "0x3381E0", Offset = "0x336BE0", VA = "0x1803381E0")]
	private void Open()
	{
	}

	[Token(Token = "0x60001D0")]
	[Address(RVA = "0x338460", Offset = "0x336E60", VA = "0x180338460")]
	private void Close()
	{
	}

	[Token(Token = "0x60001D1")]
	[Address(RVA = "0x338710", Offset = "0x337110", VA = "0x180338710")]
	private void ToggleVisibility()
	{
	}

	[Token(Token = "0x60001D2")]
	[Address(RVA = "0x338730", Offset = "0x337130", VA = "0x180338730")]
	public void OnShow()
	{
	}

	[Token(Token = "0x60001D3")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public ControlPanel()
	{
	}
}
