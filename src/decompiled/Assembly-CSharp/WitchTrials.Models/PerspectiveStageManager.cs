using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Token(Token = "0x2000191")]
public class PerspectiveStageManager : IPerspectiveStageManager, IPerspectiveStageLoader, IPerspectiveStageScreenModifier, IPerspectiveStageCameraModifier, ICourtModifier, IService
{
	[StructLayout(3)]
	[Token(Token = "0x2000192")]
	[CompilerGenerated]
	private struct _003CChangeScreenVisibilityAsync_003Ed__10
{
		[Token(Token = "0x4000758")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000759")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400075A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PerspectiveStageManager _003C_003E4__this;

		[Token(Token = "0x400075B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public int screenIndex;

		[Token(Token = "0x400075C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public bool visible;

		[Token(Token = "0x400075D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float duration;

		[Token(Token = "0x400075E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public CancellationToken ct;

		[Token(Token = "0x400075F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60006FA")]
		[Address(RVA = "0x3A1640", Offset = "0x3A0040", VA = "0x1803A1640", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60006FB")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000193")]
	[CompilerGenerated]
	private struct _003CLoadStageAsync_003Ed__8
{
		[Token(Token = "0x4000760")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000761")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000762")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PerspectiveStageManager _003C_003E4__this;

		[Token(Token = "0x4000763")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public PerspectiveStageKind kind;

		[Token(Token = "0x4000764")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x4000765")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60006FC")]
		[Address(RVA = "0x3A1950", Offset = "0x3A0350", VA = "0x1803A1950", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60006FD")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000194")]
	[CompilerGenerated]
	private struct _003CModifyCameraAsync_003Ed__12
{
		[Token(Token = "0x4000766")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000767")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000768")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PerspectiveStageManager _003C_003E4__this;

		[Token(Token = "0x4000769")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public int cameraIndex;

		[Token(Token = "0x400076A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public ModifyCameraHeightTween heightTween;

		[Token(Token = "0x400076B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public ModifyCameraRotationTween rotationTween;

		[Token(Token = "0x400076C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public ModifyCameraDistanceTween distanceTween;

		[Token(Token = "0x400076D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public CancellationToken ct;

		[Token(Token = "0x400076E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60006FE")]
		[Address(RVA = "0x3A1C60", Offset = "0x3A0660", VA = "0x1803A1C60", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60006FF")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000195")]
	[CompilerGenerated]
	private struct _003CShakeCameraAsync_003Ed__13
{
		[Token(Token = "0x400076F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000770")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000771")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PerspectiveStageManager _003C_003E4__this;

		[Token(Token = "0x4000772")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public int cameraIndex;

		[Token(Token = "0x4000773")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public Vector3 power;

		[Token(Token = "0x4000774")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public float duration;

		[Token(Token = "0x4000775")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
		public int frequency;

		[Token(Token = "0x4000776")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public float dampingRatio;

		[Token(Token = "0x4000777")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
		public bool randomly;

		[Token(Token = "0x4000778")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public CancellationToken ct;

		[Token(Token = "0x4000779")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000700")]
		[Address(RVA = "0x3A1F80", Offset = "0x3A0980", VA = "0x1803A1F80", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000701")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000753")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private readonly IPerspectiveStageLoader _loader;

	[Token(Token = "0x4000754")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private readonly IPerspectiveStageScreenModifier _screenModifier;

	[Token(Token = "0x4000755")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private readonly IPerspectiveStageCameraModifier _cameraModifier;

	[Token(Token = "0x4000756")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private readonly ICourtModifier _courtModifier;

	[Token(Token = "0x17000128")]
	public Vector3 StageOffset
	{
		[Token(Token = "0x60006F1")]
		[Address(RVA = "0x3A0B30", Offset = "0x39F530", VA = "0x1803A0B30", Slot = "4")]
		[CompilerGenerated]
		get
		{
			return default(Vector3);
		}
	}

	[Token(Token = "0x60006F2")]
	[Address(RVA = "0x3A0B50", Offset = "0x39F550", VA = "0x1803A0B50")]
	public PerspectiveStageManager(IPerspectiveStageLoader loader, IPerspectiveStageScreenModifier screenModifier, IPerspectiveStageCameraModifier cameraModifier, ICourtModifier courtModifier, Vector3 stageOffset)
	{
	}

	[Token(Token = "0x60006F3")]
	[Address(RVA = "0x3A0CC0", Offset = "0x39F6C0", VA = "0x1803A0CC0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CLoadStageAsync_003Ed__8))]
	public UniTask LoadStageAsync(PerspectiveStageKind kind, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60006F4")]
	[Address(RVA = "0x3A0E70", Offset = "0x39F870", VA = "0x1803A0E70", Slot = "6")]
	public void UnloadStage()
	{
	}

	[Token(Token = "0x60006F5")]
	[Address(RVA = "0x3A0EC0", Offset = "0x39F8C0", VA = "0x1803A0EC0", Slot = "7")]
	[AsyncStateMachine(typeof(_003CChangeScreenVisibilityAsync_003Ed__10))]
	public UniTask ChangeScreenVisibilityAsync(int screenIndex, bool visible, float duration, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60006F6")]
	[Address(RVA = "0x3A10A0", Offset = "0x39FAA0", VA = "0x1803A10A0", Slot = "8")]
	public void SplitScreen(int screenIndex, float normalizedPosX, float roll, ScreenSplitDirection direction)
	{
	}

	[Token(Token = "0x60006F7")]
	[Address(RVA = "0x3A1130", Offset = "0x39FB30", VA = "0x1803A1130", Slot = "9")]
	[AsyncStateMachine(typeof(_003CModifyCameraAsync_003Ed__12))]
	public UniTask ModifyCameraAsync(int cameraIndex, ModifyCameraHeightTween heightTween, ModifyCameraRotationTween rotationTween, ModifyCameraDistanceTween distanceTween, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60006F8")]
	[Address(RVA = "0x3A13E0", Offset = "0x39FDE0", VA = "0x1803A13E0", Slot = "10")]
	[AsyncStateMachine(typeof(_003CShakeCameraAsync_003Ed__13))]
	public UniTask ShakeCameraAsync(int cameraIndex, Vector3 power, float duration, int frequency, float dampingRatio, bool randomly, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60006F9")]
	[Address(RVA = "0x3A15E0", Offset = "0x39FFE0", VA = "0x1803A15E0", Slot = "11")]
	public void ChangeFireFxActivity(bool active)
	{
	}
}
