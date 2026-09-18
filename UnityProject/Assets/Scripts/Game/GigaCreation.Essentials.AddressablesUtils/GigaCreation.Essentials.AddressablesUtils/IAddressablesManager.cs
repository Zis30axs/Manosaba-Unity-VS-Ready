using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine;
// RECONSTRUCTED (using directive, no behaviour) | Confidence: A | restored because the recovered body of
// IAddressablesManager.<InitializeAsync>d__4.MoveNext (RVA 0xCC7C70) dereferences the Addressables static
// instance (static field +0x8 of Addressables_TypeInfo at 0x180CC7DCA) and calls into AddressablesImpl
// (0x18188B580 / 0x18188B910 / 0x18188CCB0); the source-level entry point of that inlined chain is the
// public UnityEngine.AddressableAssets.Addressables.InitializeAsync() at RVA 0x1886E30.
using UnityEngine.AddressableAssets;

namespace GigaCreation.Essentials.AddressablesUtils
{
    [Token(Token = "0x2000020")]
    public interface IAddressablesManager : IContentCatalogManager, IAddressableResourceLocationProvider, IAddressableAssetLoader, IService, IDisposable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000021")]
    	[CompilerGenerated]
    	private struct _003CInitializeAsync_003Ed__4
    	{
    		[Token(Token = "0x400007D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400007E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400007F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000080")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600005E")]
    		[Address(RVA = "0xCC7C70", Offset = "0xCC6670", VA = "0x180CC7C70", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600005F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: n/a (static storage +0x0 of this interface's Il2CppClass)
    	//
    	// Backing storage of the static auto-property below. dump.cs declares it as
    	// `private static UniTaskCompletionSource <InitializeUcs>k__BackingField; // 0x0` (dump.cs:727985) --
    	// a metadata-only name that is not valid C# source, so the decompiled property carries no field at
    	// all. Renamed here; this is the repo's established handling of that shape (DefaultLocaleSelector.cs
    	// :18-24, Timer.cs:80-81). The slot is +0x0 of this interface's static-field block, reached as
    	// `[IAddressablesManager_TypeInfo + 0xB8] + 0x0` by get_InitializeUcs (0x180CC79A5/0x180CC79B3),
    	// by set_InitializeUcs (0x180CC7BF7... inlined into ResetStaticProperties), by ResetStaticProperties
    	// (0x180CC7BF7/0x180CC7C05) and by <InitializeAsync>d__4.MoveNext (0x180CC8003/0x180CC8011).
    	private static UniTaskCompletionSource _initializeUcs;

    	[Token(Token = "0x1700000C")]
    	static UniTaskCompletionSource InitializeUcs
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCC7980, 59 B (0x3B)
    		//
    		// Native evidence (tools/unity-rebuild/out/logs/IAddressablesManager_get_InitializeUcs_0xCC7980.txt):
    		//   the IAddressablesManager class-init guard (0x180CC7984..0x180CC799E), then
    		//   0x180CC79A5 `rax = [IAddressablesManager_TypeInfo + 0xB8]` (the static-field block) and
    		//   0x180CC79B3 `rax = [rax]` (static field +0x0). Nothing else -- a compiler-generated
    		//   auto-property getter, i.e. a bare read of the field above.
    		[Token(Token = "0x600005A")]
    		[Address(RVA = "0xCC7980", Offset = "0xCC6380", VA = "0x180CC7980")]
    		[CompilerGenerated]
    		get
    		{
    			return _initializeUcs;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCC79C0, 151 B (0x97)
    		//
    		// Native evidence (tools/unity-rebuild/out/logs/IAddressablesManager_set_InitializeUcs_0xCC79C0.txt):
    		//   rbx = value, then 0x180CC79F1/0x180CC79F8 `rdx = [IAddressablesManager_TypeInfo + 0xB8]` /
    		//   `[rdx] = rbx` -- a bare store into static field +0x0 -- followed by the standard GC write
    		//   barrier (0x180CC79FB..0x180CC7A4F; guard data_30D91C4, card table data_3134580) that IL2CPP
    		//   emits for a reference store. Nothing else: the auto-property setter.
    		[Token(Token = "0x600005B")]
    		[Address(RVA = "0xCC79C0", Offset = "0xCC63C0", VA = "0x180CC79C0")]
    		[CompilerGenerated]
    		private set
    		{
    			_initializeUcs = value;
    		}
    	}

    	[Token(Token = "0x600005C")]
    	[Address(RVA = "0xCC7A60", Offset = "0xCC6460", VA = "0x180CC7A60")]
    	[AsyncStateMachine(typeof(_003CInitializeAsync_003Ed__4))]
    	static UniTask InitializeAsync([Optional] CancellationToken ct)
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCC7A60, 317 B (0x13D)
    		//
    		// Native evidence (tools/unity-rebuild/out/logs/IAddressablesManager_InitializeAsync_0xCC7A60.txt):
    		//   The standard Cysharp async kickoff for a 16-byte UniTask by value: rcx = the hidden return
    		//   buffer -> rdi, rdx = ct -> rbx, r8 = MethodInfo*. A 0x38-byte state machine is built on the
    		//   stack at [rsp+0x30] and matches the retained d__4 struct field for field:
    		//     state   +0x0  [rsp+0x30] = 0 (0x180CC7A9A), then 0x180CC7B6A `mov dword [rsp+0x30], 0xffffffff`
    		//                   -- i.e. `state = -1` immediately before Start, so the entry arm of MoveNext
    		//                   is the one that runs first;
    		//     builder +0x8  [rsp+0x38..0x48] zeroed (0x180CC7ABB) with its reference-field GC write
    		//                   barrier at 0x180CC7AC4..0x180CC7B57 (guard data_30D91C4, card table
    		//                   data_30EE2A0);
    		//     ct      +0x18 [rsp+0x48] = rbx (0x180CC7B0F) with the barrier at 0x180CC7B14..;
    		//     <>u__1  +0x20 [rsp+0x50..0x68] = 0 (0x180CC7AA2 / 0x180CC7AAA / 0x180CC7AB5) -- the single
    		//                   24-byte awaiter slot, which also pins the struct's total size at 0x38.
    		//   Then METHODINFO AsyncUniTaskMethodBuilder.Start<IAddressablesManager.<InitializeAsync>d__4>()
    		//   (rip slot 0x219E0B8) drives 0x180331870 (Start's shared-generic body), and 0x1803313C0 --
    		//   AsyncUniTaskMethodBuilder.get_Task(), confirmed by its own disassembly in
    		//   AsyncUniTaskMethodBuilder_get_Task_0x3313C0.txt: it tests the builder's runner-promise pair
    		//   ([rdx]/[rdx+8]) and otherwise returns the cached static UniTask.CompletedTask -- has its
    		//   16-byte result copied into the caller's return buffer (0x180CC7B8E/0x180CC7B94). Nothing else
    		//   happens in these 317 bytes, i.e. `async UniTask` with no statement before the state machine.
    		//
    		// The recovered body lives in InitializeAsyncCore: writing it *here* as an async method would
    		// make the compiler emit a nested state machine whose name collides with the decompiled
    		// `_003CInitializeAsync_003Ed__4` retained above as evidence-bearing metadata.
    		return InitializeAsyncCore(ct);
    	}

    	// RECONSTRUCTED | Confidence: A (every call target, constant, string literal, field offset, interface
    	// slot, awaiter slot and await state is pinned by the bytes; only the two source spellings called out
    	// in the TODO(evidence) notes below are not separable from them) |
    	// RVA: 0xCC7C70 (state-machine body, 1210 B = 0x4BA) / 0xCC7A60 (kickoff, 317 B = 0x13D)
    	//
    	// Native evidence -- annotated disassemblies in tools/unity-rebuild/out/logs/:
    	//   IAddressablesManager_InitializeAsync_MoveNext_0xCC7C70.txt (this body),
    	//   IAddressablesManager_InitializeAsync_0xCC7A60.txt (kickoff),
    	//   Addressables_InitializeAsync_0x1886E30.txt / Addressables_InitializeAsync_bool_0x1886F60.txt
    	//     (the static wrapper that was inlined at steps 3-7),
    	//   AddressablesImpl_InitializeAsync_0x188D0F0.txt / _bool_0x188D190.txt (the impl wrapper that was
    	//     inlined on top of it), AddressablesImpl_get_RuntimePath_0x188B620.txt,
    	//   AddressablesImpl_InitializeAsync3_0x188CCB0.txt (the method actually called),
    	//   AddressablesImpl_get_PlayerBuildDataPath_0x188B580.txt, AddressablesImpl_ResolveInternalId_0x188B910.txt,
    	//   UniTaskCompletionSource_TrySetResult_0x181D8E0.txt, UniTaskCompletionSource_TrySignalCompletion_0x181E030.txt.
    	//
    	// AWAIT COUNT = 1. The retained d__4 struct declares exactly ONE awaiter slot (<>u__1, +0x20, a
    	// 24-byte UniTask<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator>.Awaiter, so the
    	// struct is 0x38 bytes -- which the kickoff's stack frame layout confirms) and the MoveNext dispatch
    	// handles exactly two states: 0x180CC7D19 `mov eax,[rbx]` / `test eax,eax` / `jne 0x180CC7D49`, where
    	// state 0 is the resume arm (0x180CC7D1F..0x180CC7D44: copy <>u__1 out of [rbx+0x20], re-zero the
    	// slot, `state = -1`, `jmp 0x180CC7FCB` straight to GetResult) and every other value is the entry arm.
    	// There is exactly one AwaitUnsafeOnCompleted call site (0x180CC80F3) and no call to
    	// Addressables.LoadContentCatalogAsync (0x18870A0) or to any other Addressables API anywhere in these
    	// 1210 bytes.
    	//
    	// Numbered recovered sequence (VA 0x180CC7xxx == RVA 0xCC7xxx):
    	//   1. Entry (state = -1 from the kickoff) -> 0x180CC7D49. Nothing at all runs before the awaited
    	//      expression: 0x180CC7D49..0x180CC7DC5 is only the inlined prologue of Addressables
    	//      .InitializeAsync() (three Addressables_TypeInfo class-init guards, each
    	//      `cmp dword [rcx+0xE4],0 / call 0x18024CE60`).
    	//   2. 0x180CC7DCA `rax = [rip -> Addressables_TypeInfo]`, `rcx = [rax+0xB8]` (the static-field block),
    	//      `rdi = [rcx+8]` = static field +0x8 = `Addressables.m_AddressablesInstance` (dump.cs:669588,
    	//      the storage behind the private static `m_Addressables` property, dump.cs:669596/669612). Then
    	//      0x180CC7DDC `test rdi,rdi / je 0x180CC8125` -> THROW NullReferenceException (0x180246730):
    	//      the inlined get_m_Addressables null check, so the source unconditionally dereferences the
    	//      static instance here.
    	//      The taken edge is a THROW, not a skip. 0x180CC8125 is the function's LAST instruction: the
    	//      5-byte `call` there ends exactly at 0xCC812A = 0xCC7C70 + 0x4BA, the recovered function size,
    	//      and there is no `ret` after it, so a taken `je` never returns to this body. Nothing below is
    	//      conditionally skipped, so there is no in-source `if` to reproduce: the source unconditionally
    	//      uses the static instance, and this edge is only the compiler's null check for that use.
    	//      The helper is pinned as the NullReferenceException raiser by NAME, not merely by the repo's
    	//      helper table (work/reversing/README.md:73): 0x180246730 is `sub rsp,0x28 / call 0x18025C070`;
    	//      0x18025C070 is `lea rcx,[rsp+0x20] / call 0x180244D50` (that helper zeroes a 16-byte temp and
    	//      returns it, 0x180244D50: `xor eax,eax / mov [rcx],rax / mov [rcx+8],rax / mov rax,rcx / ret`)
    	//      followed by `mov rcx,rax / call 0x18025C090`; 0x18025C090 calls 0x18025CFA0, which loads its
    	//      type name from data_2AA5C78 -- read straight out of the image that address holds the ASCII
    	//      bytes "NullReferenceException" -- and then raises it through 0x18025BFD0. The sibling stub
    	//      0x180246740 resolves its name from data_2AA5A08 = "IndexOutOfRangeException" instead, which is
    	//      what distinguishes the two stubs. This is therefore the standard IL2CPP null check emitted for
    	//      `Addressables.m_AddressablesInstance.<member>` after the inlined `m_Addressables` getter
    	//      (Addressables.cs:564-582, `return m_AddressablesInstance;`).
    	//   3. 0x180CC7E0B `call 0x18188B580` = AddressablesImpl.get_PlayerBuildDataPath (dump.cs:670750) on
    	//      rdi with rdx = 0 (MethodInfo*). It is an INSTANCE property of the impl -- not a static -- so
    	//      the left operand of the `+` below is Addressables' PlayerBuildDataPath.
    	//   4. 0x180CC7E13 `rdx = STR '/settings.json'` (rip slot 0x219E266) and 0x180CC7E1D
    	//      `call 0x180E265E0` = System.String.Concat(string, string) (r8 = 0 = MethodInfo*) with
    	//      rcx = the value from step 3, i.e. `PlayerBuildDataPath + "/settings.json"`.
    	//   5. 0x180CC7E2B `call 0x18188B910` = AddressablesImpl.ResolveInternalId (dump.cs:670780) on rdi
    	//      with rdx = that path -> the resolved settings.json location string.
    	//   6. 0x180CC7E4B `call 0x18188CCB0` = AddressablesImpl.InitializeAsync(string runtimeDataPath,
    	//      string providerSuffix, bool autoReleaseHandle = true) (dump.cs:670801-670802) with
    	//      rcx = the hidden return buffer [rbp+0x50] (a 24-byte AsyncOperationHandle<IResourceLocator>),
    	//      rdx = rdi (this), r8 = the resolved id from step 5, r9 = 0 (providerSuffix = null),
    	//      stack [rsp+0x20] = 1 (autoReleaseHandle = true) and [rsp+0x28] = 0 (MethodInfo*, the last
    	//      parameter). THIS is the call that actually initialises Addressables.
    	//   7. Steps 3-6 are instruction-for-instruction the body of the Addressables wrappers, which is what
    	//      pins the source-level expression to `Addressables.InitializeAsync()`:
    	//      Addressables.InitializeAsync() (0x1886E30) loads the same static +0x8, performs the same three
    	//      calls with the same arguments and calls the same 0x188CCB0; AddressablesImpl.InitializeAsync()
    	//      (0x188D0F0) is that same sequence again with `this` instead of the static. The installed 2.4.6
    	//      sources spell the chain out (they are the authority for semantics only):
    	//      Addressables.cs:1021-1024 `return m_Addressables.InitializeAsync();` and
    	//      AddressablesImpl.cs:428-437 (non-EDITOR branch) `var settingsPath = RuntimePath +
    	//      "/settings.json"; return InitializeAsync(ResolveInternalId(settingsPath));`, with the 7-byte
    	//      `RuntimePath` getter (0x188B620) being a bare `jmp` to get_PlayerBuildDataPath -- which is why
    	//      step 3 sees the PlayerBuildDataPath call directly. Both wrappers were INLINED into this state
    	//      machine: neither 0x1886E30 nor 0x188D0F0 is called from anywhere in these 1210 bytes.
    	//   8. 0x180CC7E50..0x180CC7E8C the returned 24-byte handle is copied from [rbp+0x50] to [rbp+0x30],
    	//      which becomes the extension method's `this` argument.
    	//   9. 0x180CC7E90 `call 0x18064E330` = Cysharp.Threading.Tasks.AddressablesAsyncExtensions
    	//      .ToUniTask<__Il2CppFullySharedGenericType> with rcx = &handle, rdx = 0 (progress = null),
    	//      r8d = 8 (timing = PlayerLoopTiming.Update; PlayerLoopHelper.cs:71-84 enumerates
    	//      Initialization = 0, EarlyUpdate = 2, FixedUpdate = 4, PreUpdate = 6, ... Update = 8),
    	//      r9 = [rbx+0x18] = d__4.ct -- the ONLY read of ct in this whole body -- and the stack slots
    	//      [rsp+0x20] = 0 (cancelImmediately), [rsp+0x28] = 0 (autoReleaseWhenCanceled),
    	//      [rsp+0x30] = the hidden return buffer for UniTask<IResourceLocator>, [rsp+0x38] = METHODINFO
    	//      Method$...ToUniTask<IResourceLocator>(). Those are exactly the six parameters after the handle
    	//      in the installed signature `ToUniTask<T>(this AsyncOperationHandle<T> handle,
    	//      IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update,
    	//      CancellationToken cancellationToken = default, bool cancelImmediately = false,
    	//      bool autoReleaseWhenCanceled = false)`
    	//      (Packages/com.cysharp.unitask/Addressables/AddressablesAsyncExtensions.cs:282), and every one
    	//      of them carries its default value -- so the only argument the source supplied is the token.
    	//   10. 0x180CC7EA7..0x180CC7FC3 the awaited task: UniTask<IResourceLocator>.GetAwaiter (METHODINFO
    	//      at 0x219E112), whose generic awaiter is materialised through the runtime's dynamic-allocation
    	//      helpers (0x1802D3060 / 0x180306BB0 / 0x180002850 / 0x180306510, size taken from the rgctx)
    	//      into the stack temp [rbp+0x10], then the inlined
    	//      UniTask<IResourceLocator>.Awaiter.get_IsCompleted (METHODINFO at 0x219E077,
    	//      `test eax,eax / je 0x180CC8085`).
    	//   11. 0x180CC8085 (not completed) `state = 0`, `<>u__1 = the 24-byte awaiter`
    	//      (0x180CC808B..0x180CC8098 -> [rbx+0x20..0x38]) under the standard GC write barrier
    	//      (0x180CC809D..0x180CC80DF), then 0x180CC80F3 `call 0x180331940` with r9 = METHODINFO
    	//      Method$...AwaitUnsafeOnCompleted<UniTask.Awaiter<IResourceLocator>,
    	//      IAddressablesManager.<InitializeAsync>d__4>() (0x219DB14), rcx = &builder ([rbx+8]),
    	//      r8 = rbx (the state machine) -> return.
    	//   12. 0x180CC7FCB -- reached both by synchronous completion falling through from step 10 and by the
    	//      resume arm of the dispatch -- is where `state = -2`/GetResult begins: at 0x180CC7FDD
    	//      `call 0x180331BF0` = UniTask<...>.Awaiter.GetResult (METHODINFO at 0x219E026) writing the
    	//      IResourceLocator into [rbp+0xA0]. That buffer is never read again --
    	//      no field store, no argument, no branch -- so the awaited value is DISCARDED.
    	//   13. 0x180CC7FE2..0x180CC8003 the IAddressablesManager class-init guard (guard data_30DD0DF), then
    	//      `mov rcx,[IAddressablesManager_TypeInfo + 0xB8]` / `mov rcx,[rcx]` = static field +0x0 =
    	//      `<InitializeUcs>k__BackingField` (dump.cs:727985); 0x180CC8014 `test rcx,rcx / je 0x180CC8120`
    	//      -> THROW NullReferenceException. The object is null-checked before an instance call, so this is
    	//      a direct call and NOT a null-conditional one (a `?.` would skip the call instead of throwing).
    	//   14. 0x180CC8025 `call 0x18181E030` with edx = 1 and r8 = 0 (MethodInfo*) =
    	//      UniTaskCompletionSource.TrySignalCompletion(UniTaskStatus.Succeeded); its bool result (eax) is
    	//      dropped. This is `InitializeUcs.TrySetResult()` INLINED: TrySetResult's own body (0x181D8E0,
    	//      13 B) is `xor r8d,r8d / mov edx,1 / jmp 0x18181E030`, and UniTaskStatus.Succeeded = 1
    	//      (Packages/com.cysharp.unitask/IUniTaskSource.cs:13-21). TrySignalCompletion is private, so the
    	//      source line can only have been the public TrySetResult().
    	//   15. 0x180CC802B..0x180CC8069 the normal exit: `state = -2`, the runner-promise null check and the
    	//      builder's SetResult through the IStateMachineRunnerPromise interface thunk 0x1800022E0 (slot 2)
    	//      -- the non-generic AsyncUniTaskMethodBuilder, i.e. a plain `async UniTask` with no result.
    	//   16. 0x180CC80FE..0x180CC811B the standard unwind arm: `state = -2` plus builder.SetException
    	//      (0x180331550); no game logic.
    	//
    	// TODO(evidence): the awaited expression's exact overload.
    	//   `Addressables.InitializeAsync()` (used below) and `Addressables.InitializeAsync(autoReleaseHandle:
    	//   true)` are indistinguishable in these bytes: the bool wrapper (Addressables_InitializeAsync_bool
    	//   _0x1886F60.txt) inlines to the very same call sequence with the constant `true` folded into the
    	//   same `mov byte ptr [rsp+0x20], 1`. The parameterless spelling is the canonical one; either is
    	//   identical at runtime.
    	// TODO(evidence): step 12's source spelling. `await <task>;` (used below) and an unused
    	//   `var locator = await <task>;` compile to the same GetResult-and-drop, and the retained d__4
    	//   struct declares no 5__ hoisted local, so nothing is carried across the await either way.
    	//
    	// FINDING (reproduced as found, not repaired): the recovered body initialises Addressables on every
    	//   call -- there is no guard of its own at this level; the "already started" guard lives inside
    	//   AddressablesImpl.InitializeAsync (installed 2.4.6 AddressablesImpl.cs:357). Two sharper
    	//   consequences of the reproduced shape:
    	//   (a) step 13 runs only on the success path. If the await at step 9-12 faults (Addressables
    	//       failure, or cancellation through ct), the UniTaskCompletionSource is never signalled and
    	//       InitializeUcs stays pending forever, so any consumer awaiting `InitializeUcs.Task` hangs --
    	//       the exception goes to the builder, not to the completion source.
    	//   (b) step 13 calls InitializeUcs unconditionally: not a `?.`, and the emitted null check THROWS
    	//       (0x180CC8120) instead of skipping. The field's only writer is ResetStaticProperties (below),
    	//       so if that RuntimeInitializeOnLoadMethod has not run by then, the last statement of the
    	//       recovered body raises NullReferenceException inside the state machine -- and because
    	//       GameInitializer.cs:259 fires this method with `.Forget()`, that exception is delivered to
    	//       UniTaskScheduler instead of to the boot chain.
    	// WHY THIS AWAIT IS NOT LoadContentCatalogAsync -- the measured G4b failure, explained.
    	//   An earlier revision of this member reconstructed TWO awaits, the second spelled
    	//   `await Addressables.LoadContentCatalogAsync(PlayerBuildDataPath + "/settings.json", true)`.
    	//   That second call does not exist in these 1210 bytes. LoadContentCatalogAsync lives at RVA
    	//   0x188D240 (AddressablesImpl.cs:526-536) and at 0x18870A0 / 0x1887190 (Addressables.cs:1051-1076)
    	//   and is called from nowhere in this body; it wraps its argument in a ContentCatalogProvider
    	//   location (CreateCatalogLocationWithHashDependencies) and drives ResourceManager group/chain
    	//   operations, which is not the observed sequence. What IS observed is the inlined expansion of the
    	//   InitializeAsync family only, so the second await was an artifact of reading that one expansion as
    	//   two hand-written Addressables calls. The Play Mode failure it produced was the direct
    	//   consequence:
    	//     * `settings.json` is the RUNTIME DATA (settings) file, not a catalog. It is 864 B in every copy
    	//       on disk, and in each of them it is a serialized ResourceManagerRuntimeData whose
    	//       m_CatalogLocations[0].m_InternalId is
    	//       "{UnityEngine.AddressableAssets.Addressables.RuntimePath}/catalog.bin":
    	//         our content build  UnityProject/Library/com.unity.addressables/aa/Windows/ -> settings.json
    	//           864 B + catalog.bin 5,820,243 B + catalog.hash 32 B
    	//         the shipped game   UnityProject/Assets/StreamingAssets/shipped-aa/ (also copied into the
    	//           rebuilt player as manosaba-rebuild_Data/StreamingAssets/shipped-aa/) -> settings.json 864 B
    	//           + catalog.bin 5,551,003 B + catalog.hash 32 B
    	//         the rebuilt player's own manosaba-rebuild_Data/StreamingAssets/aa/ -> settings.json 864 B +
    	//           catalog.bin 481,135 B + catalog.hash 32 B
    	//       So the real catalog is the BINARY catalog.bin in all three, exactly as this project builds it,
    	//       and no catalog.json exists anywhere in the tree (searched, package cache excluded).
    	//     * InitializeAsync(runtimeDataPath, providerSuffix, autoReleaseHandle) hands that path to
    	//       Initialization.InitializationOperation.CreateInitializationOperation as `playerSettingsLocation`
    	//       and materialises it through a JsonAssetProvider location whose resource type is
    	//       ResourceManagerRuntimeData (InitializationOperation.cs:46-67, the location at :54); the
    	//       settings then name the catalog location, which ContentCatalogProvider loads as catalog.bin.
    	//       That is the correct and only working route for this string.
    	//     * LoadContentCatalogAsync instead treats the very same string AS a catalog and the provider
    	//       rejects it on the file extension alone while !ENABLE_JSON_CATALOG:
    	//       ContentCatalogProvider.cs:174-177 completes the operation with exactly the message G4b
    	//       measured -- "Expecting to load catalogs in binary format but the catalog provided is in .json
    	//       format. To load it enable Addressable Asset Settings > Catalog > Enable Json Catalog." -- and
    	//       the mirror check for ENABLE_JSON_CATALOG sits at :186-188. So
    	//       `PlayerBuildDataPath + "/settings.json"` is a valid runtime-data path and can never be a
    	//       valid catalog path: with JSON catalogs enabled the file would have to be `catalog.json`,
    	//       and with binary catalogs (this project and the shipped player) it must be `catalog.bin`.
    	//   VERDICT: the pinned string "/settings.json" is CORRECT here, because the only call it feeds is
    	//   InitializeAsync(runtimeDataPath, ...). Keeping it is not a deviation. What was wrong was the
    	//   consumer of the string, and that consumer is removed rather than repaired -- no DEVIATION from the
    	//   original string, and no behaviour invented on top of the bytes.
    	private static async UniTask InitializeAsyncCore(CancellationToken ct)
    	{
    		await Addressables.InitializeAsync().ToUniTask(cancellationToken: ct);

    		InitializeUcs.TrySetResult();
    	}

    	[Token(Token = "0x600005D")]
    	[Address(RVA = "0xCC7BA0", Offset = "0xCC65A0", VA = "0x180CC7BA0")]
    	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    	static void ResetStaticProperties()
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCC7BA0, 199 B (0xC7)
    		//
    		// Native evidence (tools/unity-rebuild/out/logs/IAddressablesManager_ResetStaticProperties_0xCC7BA0.txt):
    		//   0x180CC7BC7 `rcx = TYPE Cysharp.Threading.Tasks.UniTaskCompletionSource_TypeInfo` and
    		//   0x180CC7BCE `call 0x1802980E0` = il2cpp_codegen_object_new -> rbx, i.e. `new
    		//   UniTaskCompletionSource()`. No constructor call follows because that ctor's managed body is
    		//   the shared empty body (dump.cs:464924-464925, RVA 0x30C000), the same `ret 0` this file's
    		//   SetStateMachine points at. Then the IAddressablesManager class-init guard
    		//   (0x180CC7BD3..0x180CC7BF0) and 0x180CC7BF7/0x180CC7C05
    		//   `rdx = [IAddressablesManager_TypeInfo + 0xB8]` / `[rdx] = rbx`: the store into static field
    		//   +0x0, i.e. the auto-property setter inlined (the very store set_InitializeUcs performs at
    		//   0x180CC79F8), followed by the standard GC write barrier (0x180CC7C08..0x180CC7C5F).
    		//
    		//   This is the only writer of the field among this type's own members (get_InitializeUcs reads
    		//   it, set_InitializeUcs stores it, d__4.MoveNext reads it, this method stores it), and it is a
    		//   [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)] hook -- the
    		//   source-level stand-in for a static field initializer, which the language does not allow on an
    		//   interface's static auto-property. The non-null InitializeUcs required by InitializeAsyncCore's
    		//   last statement therefore comes from here, which is why this member belongs to the same code
    		//   path and is reconstructed rather than listed as trivial.
    		InitializeUcs = new UniTaskCompletionSource();
    	}
    }
}
