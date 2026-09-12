using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.NaninovelExtender.Ui;
using Il2CppDummyDll;
using LitMotion;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x20000C6")]
public class LoadingIndicatorRolling : LoadingIndicatorBase
{
	[StructLayout(3)]
	[Token(Token = "0x20000C7")]
	[CompilerGenerated]
	private struct _003CShowAsync_003Ed__7
{
		[Token(Token = "0x4000377")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000378")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000379")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public LoadingIndicatorRolling _003C_003E4__this;

		[Token(Token = "0x400037A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x400037B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x400037C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60002D1")]
		[Address(RVA = "0x351C00", Offset = "0x350600", VA = "0x180351C00", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60002D2")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000372")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Transform _transform;

	[Token(Token = "0x4000373")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private float _duration;

	[Token(Token = "0x4000374")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
	[SerializeField]
	private Ease _ease;

	[Token(Token = "0x4000375")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private float _delay;

	[Token(Token = "0x4000376")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private CancellationTokenSource _cts;

	[Token(Token = "0x60002CB")]
	[Address(RVA = "0x3515C0", Offset = "0x34FFC0", VA = "0x1803515C0", Slot = "4")]
	protected override void Reset()
	{
	}

	[Token(Token = "0x60002CC")]
	[Address(RVA = "0x3516D0", Offset = "0x3500D0", VA = "0x1803516D0", Slot = "5")]
	public override void OnShow()
	{
	}

	[Token(Token = "0x60002CD")]
	[Address(RVA = "0x351880", Offset = "0x350280", VA = "0x180351880")]
	[AsyncStateMachine(typeof(_003CShowAsync_003Ed__7))]
	private UniTask ShowAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60002CE")]
	[Address(RVA = "0x351A30", Offset = "0x350430", VA = "0x180351A30", Slot = "6")]
	public override void OnHide()
	{
	}

	[Token(Token = "0x60002CF")]
	[Address(RVA = "0x351B10", Offset = "0x350510", VA = "0x180351B10")]
	private void SetRoll(float value)
	{
	}

	[Token(Token = "0x60002D0")]
	[Address(RVA = "0x351BB0", Offset = "0x3505B0", VA = "0x180351BB0")]
	public LoadingIndicatorRolling()
	{
	}
}
