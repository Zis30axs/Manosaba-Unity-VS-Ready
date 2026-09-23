using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Token(Token = "0x2000017")]
    public static class ManagedUiExtensions
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000018")]
    	[CompilerGenerated]
    	private struct _003CDismissAsync_003Ed__1
    {
    		[Token(Token = "0x400004A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400004B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400004C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public IManagedUI self;

    		[Token(Token = "0x400004D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x400004E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public float? duration;

    		[Token(Token = "0x400004F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x4000050")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000051")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000052")]
    		[Address(RVA = "0xD3A180", Offset = "0xD38B80", VA = "0x180D3A180", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000053")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000019")]
    	[CompilerGenerated]
    	private struct _003CDismissAsync_003Ed__3
    {
    		[Token(Token = "0x4000052")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000053")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000054")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public IManagedUI self;

    		[Token(Token = "0x4000055")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float? duration;

    		[Token(Token = "0x4000056")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000057")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000058")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000054")]
    		[Address(RVA = "0xD3A8D0", Offset = "0xD392D0", VA = "0x180D3A8D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000055")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200001A")]
    	[CompilerGenerated]
    	private struct _003CPresentAsync_003Ed__0
    {
    		[Token(Token = "0x4000059")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400005A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400005B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public IManagedUI self;

    		[Token(Token = "0x400005C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x400005D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public float? duration;

    		[Token(Token = "0x400005E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x400005F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000060")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000056")]
    		[Address(RVA = "0xD3AE60", Offset = "0xD39860", VA = "0x180D3AE60", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000057")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200001B")]
    	[CompilerGenerated]
    	private struct _003CPresentAsync_003Ed__2
    {
    		[Token(Token = "0x4000061")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000062")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000063")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public IManagedUI self;

    		[Token(Token = "0x4000064")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float? duration;

    		[Token(Token = "0x4000065")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000066")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000067")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000058")]
    		[Address(RVA = "0xD3B5B0", Offset = "0xD39FB0", VA = "0x180D3B5B0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000059")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD39A40 (kickoff) / 0xD3AE60 (<PresentAsync>d__0.MoveNext, 1857 B): a CustomUI
    	//   carrying a NaninovelScreen (isinst + TryGetComponent via its GameObject) is presented through it with a token
    	//   source linked from the AsyncToken (<linkedCts>5__2, disposed in a finally); any other UI gets the Naninovel
    	//   ChangeVisibility(true, duration, token) (<>u__2). Body in PresentAsyncCore.
    	//   (The skeleton returned default: Options / Gallery / Load on the title screen did nothing.)
    	// Annotated disassembly: work/title-path/disas/mue_0xD3AE60.txt
    	[Token(Token = "0x600004E")]
    	[Address(RVA = "0xD39A40", Offset = "0xD38440", VA = "0x180D39A40")]
    	[AsyncStateMachine(typeof(_003CPresentAsync_003Ed__0))]
    	public static Cysharp.Threading.Tasks.UniTask PresentAsync(this IManagedUI self, [Optional] float? duration, [Optional] AsyncToken token)
    	{
    		return PresentAsyncCore(self, duration, token);
    	}

    	private static async Cysharp.Threading.Tasks.UniTask PresentAsyncCore(IManagedUI self, float? duration, AsyncToken token)
    	{
    		if (self is CustomUI customUI && customUI.TryGetComponent<NaninovelScreen>(out NaninovelScreen component))
    		{
    			using CancellationTokenSource linkedCts = token.CreateLinkedTokenSource();
    			await component.PresentAsync(duration, linkedCts.Token);
    		}
    		else
    		{
    			await self.ChangeVisibility(true, duration, token);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD39C10 (kickoff) / 0xD3A180 (<DismissAsync>d__1.MoveNext, 1857 B): PresentAsync
    	//   with NaninovelScreen.DismissAsync and ChangeVisibility(false, ...). Body in DismissAsyncCore.
    	// Annotated disassembly: work/title-path/disas/mue_0xD3A180.txt
    	[Token(Token = "0x600004F")]
    	[Address(RVA = "0xD39C10", Offset = "0xD38610", VA = "0x180D39C10")]
    	[AsyncStateMachine(typeof(_003CDismissAsync_003Ed__1))]
    	public static Cysharp.Threading.Tasks.UniTask DismissAsync(this IManagedUI self, [Optional] float? duration, [Optional] AsyncToken token)
    	{
    		return DismissAsyncCore(self, duration, token);
    	}

    	private static async Cysharp.Threading.Tasks.UniTask DismissAsyncCore(IManagedUI self, float? duration, AsyncToken token)
    	{
    		if (self is CustomUI customUI && customUI.TryGetComponent<NaninovelScreen>(out NaninovelScreen component))
    		{
    			using CancellationTokenSource linkedCts = token.CreateLinkedTokenSource();
    			await component.DismissAsync(duration, linkedCts.Token);
    		}
    		else
    		{
    			await self.ChangeVisibility(false, duration, token);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD39DE0 (kickoff) / 0xD3B5B0 (<PresentAsync>d__2.MoveNext, 1416 B): a CustomUI
    	//   carrying a NaninovelScreen is presented through it with ct; any other UI gets ChangeVisibility(true, duration,
    	//   new AsyncToken(ct)). Body in PresentAsyncCore.
    	// Annotated disassembly: work/title-path/disas/mue_0xD3B5B0.txt
    	[Token(Token = "0x6000050")]
    	[Address(RVA = "0xD39DE0", Offset = "0xD387E0", VA = "0x180D39DE0")]
    	[AsyncStateMachine(typeof(_003CPresentAsync_003Ed__2))]
    	public static Cysharp.Threading.Tasks.UniTask PresentAsync(this IManagedUI self, [Optional] float? duration, [Optional] CancellationToken ct)
    	{
    		return PresentAsyncCore(self, duration, ct);
    	}

    	private static async Cysharp.Threading.Tasks.UniTask PresentAsyncCore(IManagedUI self, float? duration, CancellationToken ct)
    	{
    		if (self is CustomUI customUI && customUI.TryGetComponent<NaninovelScreen>(out NaninovelScreen component))
    		{
    			await component.PresentAsync(duration, ct);
    		}
    		else
    		{
    			await self.ChangeVisibility(true, duration, new AsyncToken(ct));
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD39FB0 (kickoff) / 0xD3A8D0 (<DismissAsync>d__3.MoveNext, 1416 B): the
    	//   CancellationToken PresentAsync with NaninovelScreen.DismissAsync and ChangeVisibility(false, ...). Body in
    	//   DismissAsyncCore.
    	// Annotated disassembly: work/title-path/disas/mue_0xD3A8D0.txt
    	[Token(Token = "0x6000051")]
    	[Address(RVA = "0xD39FB0", Offset = "0xD389B0", VA = "0x180D39FB0")]
    	[AsyncStateMachine(typeof(_003CDismissAsync_003Ed__3))]
    	public static Cysharp.Threading.Tasks.UniTask DismissAsync(this IManagedUI self, [Optional] float? duration, [Optional] CancellationToken ct)
    	{
    		return DismissAsyncCore(self, duration, ct);
    	}

    	private static async Cysharp.Threading.Tasks.UniTask DismissAsyncCore(IManagedUI self, float? duration, CancellationToken ct)
    	{
    		if (self is CustomUI customUI && customUI.TryGetComponent<NaninovelScreen>(out NaninovelScreen component))
    		{
    			await component.DismissAsync(duration, ct);
    		}
    		else
    		{
    			await self.ChangeVisibility(false, duration, new AsyncToken(ct));
    		}
    	}
    }
}
