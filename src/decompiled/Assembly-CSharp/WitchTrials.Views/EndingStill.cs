using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x20000AE")]
public class EndingStill : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x20000AF")]
	[CompilerGenerated]
	private struct _003CDismissAsync_003Ed__5
{
		[Token(Token = "0x4000311")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000312")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000313")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public EndingStill _003C_003E4__this;

		[Token(Token = "0x4000314")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000315")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float duration;

		[Token(Token = "0x4000316")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x4000317")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000281")]
		[Address(RVA = "0x349630", Offset = "0x348030", VA = "0x180349630", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000282")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000B0")]
	[CompilerGenerated]
	private struct _003CPresentAsync_003Ed__4
{
		[Token(Token = "0x4000318")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000319")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400031A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public EndingStill _003C_003E4__this;

		[Token(Token = "0x400031B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x400031C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float duration;

		[Token(Token = "0x400031D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x400031E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000283")]
		[Address(RVA = "0x349DC0", Offset = "0x3487C0", VA = "0x180349DC0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000284")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400030F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[SerializeField]
	private CanvasGroup _canvasGroup;

	[Token(Token = "0x4000310")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private CancellationTokenSource _cts;

	// RECONSTRUCTED | Confidence: A | RVA: 0x3491C0 (identical-code-folded with MapAreaLabel.Hide)
	// 
	// `_canvasGroup` (+0x20) -> set_alpha(0f): the still starts hidden.
	[Token(Token = "0x600027C")]
	[Address(RVA = "0x3491C0", Offset = "0x347BC0", VA = "0x1803491C0")]
	private void Awake()
	{
		_canvasGroup.alpha = 0f;
	}

	[Token(Token = "0x600027D")]
	[Address(RVA = "0x3491F0", Offset = "0x347BF0", VA = "0x1803491F0")]
	private void Reset()
	{
	}

	[Token(Token = "0x600027E")]
	[Address(RVA = "0x349290", Offset = "0x347C90", VA = "0x180349290")]
	[AsyncStateMachine(typeof(_003CPresentAsync_003Ed__4))]
	public UniTask PresentAsync(float duration, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600027F")]
	[Address(RVA = "0x349460", Offset = "0x347E60", VA = "0x180349460")]
	[AsyncStateMachine(typeof(_003CDismissAsync_003Ed__5))]
	public UniTask DismissAsync(float duration, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000280")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public EndingStill()
	{
	}
}
