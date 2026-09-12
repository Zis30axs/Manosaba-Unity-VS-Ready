using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using LitMotion;
using LitMotion.Adapters;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x2000131")]
public class StageCameraContainer : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x2000132")]
	[CompilerGenerated]
	private struct _003CModifyDistanceAsync_003Ed__7
{
		[Token(Token = "0x40005BF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40005C0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40005C1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StageCameraContainer _003C_003E4__this;

		[Token(Token = "0x40005C2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float distance;

		[Token(Token = "0x40005C3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public float duration;

		[Token(Token = "0x40005C4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public Ease ease;

		[Token(Token = "0x40005C5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public CancellationToken ct;

		[Token(Token = "0x40005C6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600052A")]
		[Address(RVA = "0x3822D0", Offset = "0x380CD0", VA = "0x1803822D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600052B")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000133")]
	[CompilerGenerated]
	private struct _003CModifyHeightAsync_003Ed__5
{
		[Token(Token = "0x40005C7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40005C8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40005C9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StageCameraContainer _003C_003E4__this;

		[Token(Token = "0x40005CA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float height;

		[Token(Token = "0x40005CB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public float duration;

		[Token(Token = "0x40005CC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public Ease ease;

		[Token(Token = "0x40005CD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public CancellationToken ct;

		[Token(Token = "0x40005CE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600052C")]
		[Address(RVA = "0x382770", Offset = "0x381170", VA = "0x180382770", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600052D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000134")]
	[CompilerGenerated]
	private struct _003CModifyRotationAsync_003Ed__6
{
		[Token(Token = "0x40005CF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40005D0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40005D1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public float?[] rotation;

		[Token(Token = "0x40005D2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public StageCameraContainer _003C_003E4__this;

		[Token(Token = "0x40005D3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float duration;

		[Token(Token = "0x40005D4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public Ease ease;

		[Token(Token = "0x40005D5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public CancellationToken ct;

		[Token(Token = "0x40005D6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600052E")]
		[Address(RVA = "0x382C10", Offset = "0x381610", VA = "0x180382C10", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600052F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000135")]
	[CompilerGenerated]
	private struct _003CShakeCameraXAsync_003Ed__8
{
		[Token(Token = "0x40005D7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40005D8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40005D9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StageCameraContainer _003C_003E4__this;

		[Token(Token = "0x40005DA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public bool randomly;

		[Token(Token = "0x40005DB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public float x;

		[Token(Token = "0x40005DC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float duration;

		[Token(Token = "0x40005DD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public int frequency;

		[Token(Token = "0x40005DE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public float dampingRatio;

		[Token(Token = "0x40005DF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public CancellationToken ct;

		[Token(Token = "0x40005E0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000530")]
		[Address(RVA = "0x3838E0", Offset = "0x3822E0", VA = "0x1803838E0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000531")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000136")]
	[CompilerGenerated]
	private struct _003CShakeCameraYAsync_003Ed__9
{
		[Token(Token = "0x40005E1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40005E2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40005E3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StageCameraContainer _003C_003E4__this;

		[Token(Token = "0x40005E4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public bool randomly;

		[Token(Token = "0x40005E5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public float y;

		[Token(Token = "0x40005E6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float duration;

		[Token(Token = "0x40005E7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public int frequency;

		[Token(Token = "0x40005E8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public float dampingRatio;

		[Token(Token = "0x40005E9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public CancellationToken ct;

		[Token(Token = "0x40005EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000532")]
		[Address(RVA = "0x383D40", Offset = "0x382740", VA = "0x180383D40", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000533")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000137")]
	[CompilerGenerated]
	private struct _003CShakeCameraZAsync_003Ed__10
{
		[Token(Token = "0x40005EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40005EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40005ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StageCameraContainer _003C_003E4__this;

		[Token(Token = "0x40005EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public bool randomly;

		[Token(Token = "0x40005EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public float z;

		[Token(Token = "0x40005F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float duration;

		[Token(Token = "0x40005F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public int frequency;

		[Token(Token = "0x40005F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public float dampingRatio;

		[Token(Token = "0x40005F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public CancellationToken ct;

		[Token(Token = "0x40005F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000534")]
		[Address(RVA = "0x3841A0", Offset = "0x382BA0", VA = "0x1803841A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000535")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40005BC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[SerializeField]
	private Transform _transform;

	[Token(Token = "0x40005BD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Camera _camera;

	[Token(Token = "0x40005BE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Transform _cameraTransform;

	[Token(Token = "0x600051F")]
	[Address(RVA = "0x380FA0", Offset = "0x37F9A0", VA = "0x180380FA0")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000520")]
	[Address(RVA = "0x3407C0", Offset = "0x33F1C0", VA = "0x1803407C0")]
	public void SetCameraEnabled(bool enable)
	{
	}

	[Token(Token = "0x6000521")]
	[Address(RVA = "0x381110", Offset = "0x37FB10", VA = "0x180381110")]
	[AsyncStateMachine(typeof(_003CModifyHeightAsync_003Ed__5))]
	public UniTask ModifyHeightAsync(float height, Ease ease, float duration, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000522")]
	[Address(RVA = "0x3812F0", Offset = "0x37FCF0", VA = "0x1803812F0")]
	[AsyncStateMachine(typeof(_003CModifyRotationAsync_003Ed__6))]
	public UniTask ModifyRotationAsync(float?[] rotation, Ease ease, float duration, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000523")]
	[Address(RVA = "0x381520", Offset = "0x37FF20", VA = "0x180381520")]
	[AsyncStateMachine(typeof(_003CModifyDistanceAsync_003Ed__7))]
	public UniTask ModifyDistanceAsync(float distance, Ease ease, float duration, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000524")]
	[Address(RVA = "0x381700", Offset = "0x380100", VA = "0x180381700")]
	[AsyncStateMachine(typeof(_003CShakeCameraXAsync_003Ed__8))]
	public UniTask ShakeCameraXAsync(float x, float duration, int frequency, float dampingRatio, bool randomly, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000525")]
	[Address(RVA = "0x381900", Offset = "0x380300", VA = "0x180381900")]
	[AsyncStateMachine(typeof(_003CShakeCameraYAsync_003Ed__9))]
	public UniTask ShakeCameraYAsync(float y, float duration, int frequency, float dampingRatio, bool randomly, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000526")]
	[Address(RVA = "0x381B00", Offset = "0x380500", VA = "0x180381B00")]
	[AsyncStateMachine(typeof(_003CShakeCameraZAsync_003Ed__10))]
	public UniTask ShakeCameraZAsync(float z, float duration, int frequency, float dampingRatio, bool randomly, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000527")]
	[Address(RVA = "0x381D00", Offset = "0x380700", VA = "0x180381D00")]
	private static MotionBuilder<float, ShakeOptions, FloatShakeMotionAdapter> CreateShakeMotion(float start, float strength, float duration, int frequency, float dampingRatio)
	{
		return default(MotionBuilder<float, ShakeOptions, FloatShakeMotionAdapter>);
	}

	[Token(Token = "0x6000528")]
	[Address(RVA = "0x381FA0", Offset = "0x3809A0", VA = "0x180381FA0")]
	private static MotionBuilder<float, PunchOptions, FloatPunchMotionAdapter> CreatePunchMotion(float from, float to, float duration, int frequency, float dampingRatio)
	{
		return default(MotionBuilder<float, PunchOptions, FloatPunchMotionAdapter>);
	}

	[Token(Token = "0x6000529")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public StageCameraContainer()
	{
	}
}
