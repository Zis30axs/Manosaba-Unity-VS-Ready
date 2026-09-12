using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000138")]
public class StageCameraController : MonoBehaviour, IPerspectiveStageCameraModifier
{
	[StructLayout(3)]
	[Token(Token = "0x2000139")]
	[CompilerGenerated]
	private struct _003CModifyCameraAsync_003Ed__10
{
		[Token(Token = "0x40005FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40005FB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40005FC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public int cameraIndex;

		[Token(Token = "0x40005FD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public StageCameraController _003C_003E4__this;

		[Token(Token = "0x40005FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public ModifyCameraHeightTween heightTween;

		[Token(Token = "0x40005FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public ModifyCameraRotationTween rotationTween;

		[Token(Token = "0x4000600")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public ModifyCameraDistanceTween distanceTween;

		[Token(Token = "0x4000601")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public CancellationToken ct;

		[Token(Token = "0x4000602")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x4000603")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000540")]
		[Address(RVA = "0x3850A0", Offset = "0x383AA0", VA = "0x1803850A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000541")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200013A")]
	[CompilerGenerated]
	private struct _003CShakeCameraAsync_003Ed__11
{
		[Token(Token = "0x4000604")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000605")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000606")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public int cameraIndex;

		[Token(Token = "0x4000607")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public StageCameraController _003C_003E4__this;

		[Token(Token = "0x4000608")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x4000609")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public Vector3 force;

		[Token(Token = "0x400060A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
		public float duration;

		[Token(Token = "0x400060B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public int frequency;

		[Token(Token = "0x400060C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
		public float dampingRatio;

		[Token(Token = "0x400060D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		public bool randomly;

		[Token(Token = "0x400060E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x400060F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000542")]
		[Address(RVA = "0x386190", Offset = "0x384B90", VA = "0x180386190", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000543")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40005F5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[SerializeField]
	private StageCameraContainer[] _cameraContainers;

	[Token(Token = "0x40005F6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private ITrialStateManager _trialStateManager;

	[Token(Token = "0x40005F7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private UniTask[][] _modifyCameraTasks;

	[Token(Token = "0x40005F8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private UniTask[][] _shakeCameraTasks;

	[Token(Token = "0x40005F9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private CancellationTokenSource[] _tokenSources;

	[Token(Token = "0x170000A9")]
	private ITrialStateManager TrialStateManager
	{
		[Token(Token = "0x6000536")]
		[Address(RVA = "0x384600", Offset = "0x383000", VA = "0x180384600")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000537")]
	[Address(RVA = "0x3846C0", Offset = "0x3830C0", VA = "0x1803846C0")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000538")]
	[Address(RVA = "0x384A10", Offset = "0x383410", VA = "0x180384A10")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000539")]
	[Address(RVA = "0x384AC0", Offset = "0x3834C0", VA = "0x180384AC0")]
	private void Reset()
	{
	}

	[Token(Token = "0x600053A")]
	[Address(RVA = "0x384B50", Offset = "0x383550", VA = "0x180384B50", Slot = "4")]
	[AsyncStateMachine(typeof(_003CModifyCameraAsync_003Ed__10))]
	public UniTask ModifyCameraAsync(int cameraIndex, ModifyCameraHeightTween heightTween, ModifyCameraRotationTween rotationTween, ModifyCameraDistanceTween distanceTween, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600053B")]
	[Address(RVA = "0x384E00", Offset = "0x383800", VA = "0x180384E00", Slot = "5")]
	[AsyncStateMachine(typeof(_003CShakeCameraAsync_003Ed__11))]
	public UniTask ShakeCameraAsync(int cameraIndex, Vector3 force, float duration, int frequency, float dampingRatio, bool randomly, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600053C")]
	[Address(RVA = "0x385000", Offset = "0x383A00", VA = "0x180385000")]
	public void Present()
	{
	}

	[Token(Token = "0x600053D")]
	[Address(RVA = "0x385010", Offset = "0x383A10", VA = "0x180385010")]
	public void Dismiss()
	{
	}

	[Token(Token = "0x600053E")]
	[Address(RVA = "0x385020", Offset = "0x383A20", VA = "0x180385020")]
	private void SetCameraEnabled(bool active)
	{
	}

	[Token(Token = "0x600053F")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public StageCameraController()
	{
	}
}
