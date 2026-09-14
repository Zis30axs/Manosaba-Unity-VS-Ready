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
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x20000DC")]
[RequireComponent(typeof(CanvasGroup))]
public class TitleUi : CustomUI, ITitleUI, IManagedUI
{
	[StructLayout(3)]
	[Token(Token = "0x20000DD")]
	[CompilerGenerated]
	private struct _003CChangeVisibility_003Ed__53
{
		[Token(Token = "0x4000433")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000434")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public Naninovel.Async.CompilerServices.AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000435")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public TitleUi _003C_003E4__this;

		[Token(Token = "0x4000436")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public bool visible;

		[Token(Token = "0x4000437")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public float? duration;

		[Token(Token = "0x4000438")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken asyncToken;

		[Token(Token = "0x4000439")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000361")]
		[Address(RVA = "0x363C40", Offset = "0x362640", VA = "0x180363C40", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000362")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000DE")]
	[CompilerGenerated]
	private struct _003CLoadGameAsync_003Ed__58
{
		[Token(Token = "0x400043A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400043B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400043C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public TitleUi _003C_003E4__this;

		[Token(Token = "0x400043D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x400043E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000363")]
		[Address(RVA = "0x3643B0", Offset = "0x362DB0", VA = "0x1803643B0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000364")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000DF")]
	[CompilerGenerated]
	private struct _003CQuitGameAsync_003Ed__60
{
		[Token(Token = "0x400043F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000440")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000441")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public TitleUi _003C_003E4__this;

		[Token(Token = "0x4000442")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000443")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private INaninovelInteractionBlocker _003Cblocker_003E5__2;

		[Token(Token = "0x4000444")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private ProvidableDialog _003Cdialog_003E5__3;

		[Token(Token = "0x4000445")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask<ResponseKind>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000365")]
		[Address(RVA = "0x3646E0", Offset = "0x3630E0", VA = "0x1803646E0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000366")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000418")]
	private const string StartGameLabelName = "StartGame";

	[Token(Token = "0x4000419")]
	private const string QuitGameLabelName = "QuitGame";

	[Token(Token = "0x400041A")]
	private const string GameProgressVarName = "g_gameProgress";

	[Token(Token = "0x400041B")]
	private const string NextScenarioVarName = "nextScenario";

	[Token(Token = "0x400041C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	[Header("References for Title")]
	[SerializeField]
	private Button _loadGameButton;

	[Token(Token = "0x400041D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	[SerializeField]
	private Button _newGameButton;

	[Token(Token = "0x400041E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	[SerializeField]
	private Button _galleryButton;

	[Token(Token = "0x400041F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	[SerializeField]
	private Button _optionsButton;

	[Token(Token = "0x4000420")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
	[SerializeField]
	private Button _exitButton;

	[Token(Token = "0x4000421")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
	[SerializeField]
	private GoogleForm _googleForm;

	[Token(Token = "0x4000422")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
	[Header("Parameters for Title")]
	[SerializeField]
	private DialogData _confirmToQuitGameDialogData;

	[Token(Token = "0x4000423")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x118")]
	[SerializeField]
	private EasingType _easingTypeOnShow;

	[Token(Token = "0x4000424")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x11C")]
	[SerializeField]
	private EasingType _easingTypeOnHide;

	[Token(Token = "0x4000425")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x120")]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _startGameSfxPath;

	[Token(Token = "0x4000426")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x128")]
	private readonly Tweener<FloatTween> _fadeTweener;

	[Token(Token = "0x4000427")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x130")]
	private readonly CompositeDisposable _disposables;

	[Token(Token = "0x4000428")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x138")]
	private GigaCreation.Essentials.Input.IInputManager _inputManager;

	[Token(Token = "0x4000429")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x140")]
	private IDialogProvider _dialogProvider;

	[Token(Token = "0x400042A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x148")]
	private IWitchTrialsNaninovelManager _witchTrialsNaninovelManager;

	[Token(Token = "0x400042B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x150")]
	private ICustomVariableManager _naniVarManager;

	[Token(Token = "0x400042C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x158")]
	private IAudioManager _naniAudioManager;

	[Token(Token = "0x400042D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x160")]
	private IScriptPlayer _naniScriptPlayer;

	[Token(Token = "0x400042E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x168")]
	private IUIManager _naniUiManager;

	[Token(Token = "0x400042F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x170")]
	private ISaveLoadUI _saveLoadUi;

	[Token(Token = "0x4000430")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x178")]
	private ICGGalleryUI _galleryUi;

	[Token(Token = "0x4000431")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x180")]
	private ISettingsUI _optionsUi;

	[Token(Token = "0x4000432")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x188")]
	private bool _visibleSelf;

	[Token(Token = "0x1700006B")]
	public override bool Visible
	{
		[Token(Token = "0x6000344")]
		[Address(RVA = "0x3619A0", Offset = "0x3603A0", VA = "0x1803619A0", Slot = "28")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000345")]
		[Address(RVA = "0x3619B0", Offset = "0x3603B0", VA = "0x1803619B0", Slot = "29")]
		set
		{
		}
	}

	[Token(Token = "0x1700006C")]
	private GigaCreation.Essentials.Input.IInputManager InputManager
	{
		[Token(Token = "0x6000346")]
		[Address(RVA = "0x3619D0", Offset = "0x3603D0", VA = "0x1803619D0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700006D")]
	private IDialogProvider DialogProvider
	{
		[Token(Token = "0x6000347")]
		[Address(RVA = "0x361AA0", Offset = "0x3604A0", VA = "0x180361AA0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700006E")]
	private IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x361B70, 202 B
		// Lazy cache in +0x148 via `ServiceLocator.Get<IWitchTrialsNaninovelManager>(true)`.
		[Token(Token = "0x6000348")]
		[Address(RVA = "0x361B70", Offset = "0x360570", VA = "0x180361B70")]
		get
		{
			return _witchTrialsNaninovelManager ??= GigaCreation.Tools.Service.ServiceLocator.Get<IWitchTrialsNaninovelManager>(true);
		}
	}

	[Token(Token = "0x1700006F")]
	private ICustomVariableManager NaniVarManager
	{
		[Token(Token = "0x6000349")]
		[Address(RVA = "0x361C40", Offset = "0x360640", VA = "0x180361C40")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000070")]
	private IAudioManager NaniAudioManager
	{
		[Token(Token = "0x600034A")]
		[Address(RVA = "0x361D10", Offset = "0x360710", VA = "0x180361D10")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000071")]
	private IScriptPlayer NaniScriptPlayer
	{
		[Token(Token = "0x600034B")]
		[Address(RVA = "0x361DE0", Offset = "0x3607E0", VA = "0x180361DE0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000072")]
	private IUIManager NaniUiManager
	{
		[Token(Token = "0x600034C")]
		[Address(RVA = "0x361EB0", Offset = "0x3608B0", VA = "0x180361EB0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000073")]
	private ISaveLoadUI SaveLoadUi
	{
		[Token(Token = "0x600034D")]
		[Address(RVA = "0x361F80", Offset = "0x360980", VA = "0x180361F80")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000074")]
	private IManagedUI GalleryUi
	{
		[Token(Token = "0x600034E")]
		[Address(RVA = "0x362040", Offset = "0x360A40", VA = "0x180362040")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000075")]
	private IManagedUI OptionsUi
	{
		[Token(Token = "0x600034F")]
		[Address(RVA = "0x362100", Offset = "0x360B00", VA = "0x180362100")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000076")]
	private bool CanLoadGame
	{
		[Token(Token = "0x6000350")]
		[Address(RVA = "0x3621C0", Offset = "0x360BC0", VA = "0x1803621C0")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6000351")]
	[Address(RVA = "0x362240", Offset = "0x360C40", VA = "0x180362240", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6000352")]
	[Address(RVA = "0x3622B0", Offset = "0x360CB0", VA = "0x1803622B0", Slot = "44")]
	[AsyncStateMachine(typeof(_003CChangeVisibility_003Ed__53))]
	public override Naninovel.UniTask ChangeVisibility(bool visible, [Optional] float? duration, [Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000353")]
	[Address(RVA = "0x362460", Offset = "0x360E60", VA = "0x180362460", Slot = "45")]
	public override void SetVisibility(bool visible)
	{
	}

	[Token(Token = "0x6000354")]
	[Address(RVA = "0x362690", Offset = "0x361090", VA = "0x180362690")]
	public void OnBeginToPresentEvent()
	{
	}

	[Token(Token = "0x6000355")]
	[Address(RVA = "0x3626D0", Offset = "0x3610D0", VA = "0x1803626D0")]
	public void Activate()
	{
	}

	[Token(Token = "0x6000356")]
	[Address(RVA = "0x362B50", Offset = "0x361550", VA = "0x180362B50")]
	public void Deactivate()
	{
	}

	[Token(Token = "0x6000357")]
	[Address(RVA = "0x362B80", Offset = "0x361580", VA = "0x180362B80")]
	[AsyncStateMachine(typeof(_003CLoadGameAsync_003Ed__58))]
	private Cysharp.Threading.Tasks.UniTask LoadGameAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x6000358")]
	[Address(RVA = "0x362D20", Offset = "0x361720", VA = "0x180362D20")]
	private void StartGame()
	{
	}

	[Token(Token = "0x6000359")]
	[Address(RVA = "0x362E30", Offset = "0x361830", VA = "0x180362E30")]
	[AsyncStateMachine(typeof(_003CQuitGameAsync_003Ed__60))]
	private Cysharp.Threading.Tasks.UniTask QuitGameAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x600035A")]
	[Address(RVA = "0x362FE0", Offset = "0x3619E0", VA = "0x180362FE0")]
	public TitleUi()
	{
	}
}
