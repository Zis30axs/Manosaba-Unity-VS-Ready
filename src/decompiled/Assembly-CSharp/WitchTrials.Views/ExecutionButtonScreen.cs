using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using Naninovel;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x20000B4")]
public class ExecutionButtonScreen : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x20000B5")]
	[CompilerGenerated]
	private struct _003COnFullFillAsync_003Ed__26
{
		[Token(Token = "0x400033A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400033B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400033C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ExecutionButtonScreen _003C_003E4__this;

		[Token(Token = "0x400033D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x400033E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600029C")]
		[Address(RVA = "0x34C310", Offset = "0x34AD10", VA = "0x18034C310", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600029D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000B6")]
	[CompilerGenerated]
	private struct _003CPressAsync_003Ed__24
{
		[Token(Token = "0x400033F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000340")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000341")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ExecutionButtonScreen _003C_003E4__this;

		[Token(Token = "0x4000342")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000343")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x4000344")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600029E")]
		[Address(RVA = "0x34C9B0", Offset = "0x34B3B0", VA = "0x18034C9B0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600029F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000B7")]
	[CompilerGenerated]
	private struct _003CReleaseAsync_003Ed__25
{
		[Token(Token = "0x4000345")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000346")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000347")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ExecutionButtonScreen _003C_003E4__this;

		[Token(Token = "0x4000348")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000349")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x400034A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60002A0")]
		[Address(RVA = "0x34D2A0", Offset = "0x34BCA0", VA = "0x18034D2A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60002A1")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000329")]
	private const string ExecutionConfirmedLabelName = "ExecutionConfirmed";

	[Token(Token = "0x400032A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private RectTransform _buttonContainer;

	[Token(Token = "0x400032B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Button _button;

	[Token(Token = "0x400032C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Image _blood;

	[Token(Token = "0x400032D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private CanvasGroup _checkIcon;

	[Token(Token = "0x400032E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[Header("Parameters")]
	[SerializeField]
	private float _scaleOnPressed;

	[Token(Token = "0x400032F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
	[SerializeField]
	private float _scaleDuration;

	[Token(Token = "0x4000330")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[SerializeField]
	private float _increaseSpeed;

	[Token(Token = "0x4000331")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x4C")]
	[SerializeField]
	private float _decreaseSpeed;

	[Token(Token = "0x4000332")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[SerializeField]
	private float _delayAfterFullFill;

	[Token(Token = "0x4000333")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _chargingSfx;

	[Token(Token = "0x4000334")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _completeSfx;

	[Token(Token = "0x4000335")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private readonly CompositeDisposable _disposables;

	[Token(Token = "0x4000336")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	private IAudioManager _naniAudioManager;

	[Token(Token = "0x4000337")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	private IScriptPlayer _naniScriptPlayer;

	[Token(Token = "0x4000338")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	private bool _pressed;

	[Token(Token = "0x4000339")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private CancellationTokenSource _cts;

	[Token(Token = "0x17000049")]
	private IAudioManager NaniAudioManager
	{
		[Token(Token = "0x6000290")]
		[Address(RVA = "0x34B4B0", Offset = "0x349EB0", VA = "0x18034B4B0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700004A")]
	private IScriptPlayer NaniScriptPlayer
	{
		[Token(Token = "0x6000291")]
		[Address(RVA = "0x34B570", Offset = "0x349F70", VA = "0x18034B570")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000292")]
	[Address(RVA = "0x34B630", Offset = "0x34A030", VA = "0x18034B630")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000293")]
	[Address(RVA = "0x34B670", Offset = "0x34A070", VA = "0x18034B670")]
	public void Ready()
	{
	}

	[Token(Token = "0x6000294")]
	[Address(RVA = "0x34B8A0", Offset = "0x34A2A0", VA = "0x18034B8A0")]
	public void Dispose()
	{
	}

	[Token(Token = "0x6000295")]
	[Address(RVA = "0x34B900", Offset = "0x34A300", VA = "0x18034B900")]
	[AsyncStateMachine(typeof(_003CPressAsync_003Ed__24))]
	private Cysharp.Threading.Tasks.UniTask PressAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x6000296")]
	[Address(RVA = "0x34BAB0", Offset = "0x34A4B0", VA = "0x18034BAB0")]
	[AsyncStateMachine(typeof(_003CReleaseAsync_003Ed__25))]
	private Cysharp.Threading.Tasks.UniTask ReleaseAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x6000297")]
	[Address(RVA = "0x34BC60", Offset = "0x34A660", VA = "0x18034BC60")]
	[AsyncStateMachine(typeof(_003COnFullFillAsync_003Ed__26))]
	private Cysharp.Threading.Tasks.UniTask OnFullFillAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x6000298")]
	[Address(RVA = "0x34BE00", Offset = "0x34A800", VA = "0x18034BE00")]
	public ExecutionButtonScreen()
	{
	}
}
