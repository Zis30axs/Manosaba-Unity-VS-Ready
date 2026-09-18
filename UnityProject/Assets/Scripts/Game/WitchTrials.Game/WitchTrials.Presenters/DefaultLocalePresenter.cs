using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using WitchTrials.Models;

namespace WitchTrials.Presenters
{
    [Token(Token = "0x2000149")]
    public class DefaultLocalePresenter : IDisposable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200014A")]
    	[CompilerGenerated]
    	private struct _003CLoadAsync_003Ed__3
    {
    		[Token(Token = "0x4000657")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000658")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000659")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public DefaultLocalePresenter _003C_003E4__this;

    		[Token(Token = "0x400065A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x400065B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600057E")]
    		[Address(RVA = "0x38D3C0", Offset = "0x38BDC0", VA = "0x18038D3C0", Slot = "4")]
    		private void MoveNext()
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x38D3C0 (1093 bytes)
    			//
    			// This state machine is *compiler-generated* from the async method below; its recovered
    			// semantics now live in `LoadAsyncCore`, which the C# compiler turns back into an
    			// equivalent state machine.  Keeping a hand-written body here as well would duplicate the
    			// logic (and the compiler would emit a second nested state machine with this name), so the
    			// recovered sequence is documented on `LoadAsyncCore` instead.  Native evidence for this
    			// exact function (annotated disassembly) is in
    			// tools/unity-rebuild/out/logs/DefaultLocalePresenter_d__3_MoveNext_38D3C0.txt.
    		}

    		[Token(Token = "0x600057F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000656")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly DefaultLocaleSelector _defaultLocaleSelector;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38D0E0
    	//
    	// Native evidence:
    	//   0x38D0ED  rbx = rcx (__this)
    	//   0x38D0F2..0x38D125  one-time type-init guard for
    	//             DefaultLocaleSelector_TypeInfo,
    	//             MethodInfo ServiceLocator.Register<IDefaultLocaleSelector>(),
    	//             ServiceLocator_TypeInfo
    	//   0x38D12C  rcx = DefaultLocaleSelector_TypeInfo
    	//   0x38D133  call il2cpp_codegen_object_new   -> `new DefaultLocaleSelector()`
    	//   0x38D13F  [rbx + 0x10] = rax  ->  this._defaultLocaleSelector
    	//             (offset 0x10 confirmed against dump.cs: the only instance field,
    	//             declared `private readonly DefaultLocaleSelector`), followed by the
    	//             GC write barrier at 0x38D143..0x38D17F (card table data_30D91C4).
    	//   0x38D188  rbx = [rbx + 0x10]  ->  reload this._defaultLocaleSelector
    	//   0x38D19A  rdx = MethodInfo ServiceLocator.Register<IDefaultLocaleSelector>()
    	//   0x38D1A9  jmp -> GigaCreation.Tools.Service.ServiceLocator$$Register<object>
    	//             tail call, rcx = the reloaded field.
    	//             ServiceLocator.Register<TService>(TService service) (ServiceLocator.cs:62)
    	//             is the matching overload, so the field is the argument.
    	//
    	// Note this is the exact structural twin of GameInitializer::Awake (RVA 0x30FA80):
    	// same fold pattern, same ServiceLocator.Register target (0x180752130), different
    	// field offset (0x10 vs 0x40).
    	[Token(Token = "0x600057B")]
    	[Address(RVA = "0x38D0E0", Offset = "0x38BAE0", VA = "0x18038D0E0")]
    	public DefaultLocalePresenter()
    	{
    		_defaultLocaleSelector = new DefaultLocaleSelector();
    		GigaCreation.Tools.Service.ServiceLocator.Register<IDefaultLocaleSelector>(_defaultLocaleSelector);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38D1B0
    	//
    	// Native evidence:
    	//   0x38D1C4..0x38D1E4  one-time type-init guard for
    	//             MethodInfo ServiceLocator.Unregister<IDefaultLocaleSelector>()
    	//             and ServiceLocator_TypeInfo
    	//   0x38D1F2  rbx = [rbx + 0x10]  ->  this._defaultLocaleSelector
    	//   0x38D1FF  call interface/virtual dispatch resolve (constraint check)
    	//   0x38D204  rdx = MethodInfo ServiceLocator.Unregister<IDefaultLocaleSelector>()
    	//   0x38D20B  rcx = rbx (the field)
    	//   0x38D213  jmp -> GigaCreation.Tools.Service.ServiceLocator$$Unregister<object>
    	//             (the same 0x180752790 target as GameInitializer::OnDestroy)
    	//
    	// Recovered behaviour: symmetric teardown of the constructor.
    	[Token(Token = "0x600057C")]
    	[Address(RVA = "0x38D1B0", Offset = "0x38BBB0", VA = "0x18038D1B0", Slot = "4")]
    	public void Dispose()
    	{
    		GigaCreation.Tools.Service.ServiceLocator.Unregister<IDefaultLocaleSelector>(_defaultLocaleSelector);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38D220 (kickoff, 401 bytes) / 0x38D3C0 (body, 1093 bytes)
    	//
    	// The recovered body lives in LoadAsyncCore: writing it *here* as an async method would make the
    	// compiler emit a nested state machine whose name collides with the decompiled
    	// `_003CLoadAsync_003Ed__3` retained above as evidence-bearing metadata.
    	[Token(Token = "0x600057D")]
    	[Address(RVA = "0x38D220", Offset = "0x38BC20", VA = "0x18038D220")]
    	[AsyncStateMachine(typeof(_003CLoadAsync_003Ed__3))]
    	public UniTask LoadAsync([Optional] CancellationToken ct)
    	{
    		return LoadAsyncCore(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38D220 (kickoff) / 0x38D3C0 (state machine body)
    	//
    	// Native evidence (annotated disassembly, produced by work/reversing/disas.py):
    	//   tools/unity-rebuild/out/logs/DefaultLocalePresenter_LoadAsync_38D220.txt
    	//   tools/unity-rebuild/out/logs/DefaultLocalePresenter_d__3_MoveNext_38D3C0.txt
    	//
    	// Kickoff (0x38D220):
    	//   0x38D23C rsi = rcx, 0x38D239 rdi = rdx, 0x38D236 rbx = r8.
    	//             `UniTask` is a 16-byte struct, so it is returned through a hidden sret pointer:
    	//             rcx is the return buffer, NOT __this -- confirmed at 0x38D39F..0x38D3A5, where
    	//             builder.get_Task()'s UniTask is copied to [rsi].  The dump's csig
    	//             `(__this, ct, method)` therefore maps to rdx = __this, r8 = ct,
    	//             r9 = MethodInfo (pitfall 3: MethodInfo* is the last parameter).
    	//   0x38D241..0x38D252  one-time type-init guard for
    	//             MethodInfo AsyncUniTaskMethodBuilder.Start<<LoadAsync>d__3>()
    	//   stack state machine at rsp+0x30, slot-for-slot with dump.cs:
    	//             +0x00 state = 0xffffffff (0x38D37A); +0x08 builder (zeroed);
    	//             +0x18 <>4__this = rdi = __this; +0x20 ct = rbx; +0x28 <>u__1 zeroed
    	//   0x38D382  call AsyncUniTaskMethodBuilder.Start<<LoadAsync>d__3> (0x180331870)
    	//   0x38D391  sub_3313C0 = builder.get_Task(), 0x38D3A2 copies it into the return buffer
    	//             -> the kickoff only starts the state machine and returns builder.Task; it has no
    	//             local variables, no await and no other managed call site.
    	//
    	// MoveNext (0x38D3C0) -- the whole recovered sequence is ONE awaited call:
    	//   0x38D41A  ecx = this.<>1__state; 0x38D41C rax = [rdi + 0x18] = <>4__this
    	//   state == 0 (resume): 0x38D424 reloads <>u__1 (+0x28, typed UniTask.Awaiter), zeroes the
    	//             slot, sets state = -1 and jumps straight to the shared GetResult path.  The state
    	//             machine has exactly one awaiter slot and no locals -- i.e. exactly one `await` of a
    	//             plain (non-generic) UniTask, whose result is therefore not consumed.
    	//   state != 0: 0x38D441 null check on <>4__this (THROW NRE at 0x38D800);
    	//             0x38D44A rsi = [rax + 0x10] = this._defaultLocaleSelector;
    	//             0x38D44E rbx = [rdi + 0x20] = ct; 0x38D452 null check on the field (NRE at 0x38D7FA)
    	//   0x38D464..0x38D5B2  one-time type-init guard, then
    	//             AsyncUniTaskMethodBuilder.Start<DefaultLocaleSelector.<LoadAsync>d__9>() with the
    	//             inner state machine built on the stack at rsp+0x50.  Its slots line up with
    	//             dump.cs `DefaultLocaleSelector.<LoadAsync>d__9` (ct +0x18, <>4__this +0x20):
    	//               +0x18 ct        = rbx = the incoming ct (forwarded verbatim, no substitution)
    	//               +0x20 <>4__this = rsi = this._defaultLocaleSelector
    	//             -> the awaited expression is `_defaultLocaleSelector.LoadAsync(ct)`.  The callee is
    	//             the class method, not an interface slot: the field's static type in dump.cs is
    	//             DefaultLocaleSelector and the Start<> instantiation names its d__9.
    	//   0x38D5C1  sub_3313C0 = builder.get_Task() -> the inner UniTask on the stack
    	//   0x38D6B8  sub_3000 (awaiter fast path); 0x38D6C1 state = 0;
    	//   0x38D72C  AsyncUniTaskMethodBuilder.AwaitOnCompleted<UniTask.Awaiter, <LoadAsync>d__3>
    	//             -> the single suspension point
    	//   0x38D774  sub_31D0 (awaiter GetResult); 0x38D77A state = -2;
    	//             interface call thunk 0x1800022E0 = builder.SetResult()
    	//             -> resume path finishes the method.
    	//   No other managed call site exists in the whole 1093-byte function (semantic skeleton from
    	//   tools/unity-rebuild/disas_skeleton.py lists exactly one Start<> instantiation).
    	//
    	// TODO(evidence): the two runtime helpers called on the awaiter, 0x180003000 and 0x1800031D0,
    	//   are unnamed in the tool's symbol table.  From their arguments (source + token + the
    	//   IUniTaskSource_TypeInfo) they read as the inlined UniTask.Awaiter IsCompleted/GetResult
    	//   plumbing that every `await UniTask` emits -- runtime scaffolding, not game logic -- so they
    	//   cannot add a statement to the body; this identification itself is inference, not proof.
    	private async UniTask LoadAsyncCore(CancellationToken ct)
    	{
    		await _defaultLocaleSelector.LoadAsync(ct);
    	}
    }
}
