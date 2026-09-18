using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.AddressablesUtils;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace GigaCreation.NaninovelExtender.ResourceProvider
{
    [Token(Token = "0x2000004")]
    [UsedImplicitly]
    public class AddressableResourceProviderExtended : AddressableResourceProvider
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000005")]
    	[CompilerGenerated]
    	private struct _003CLoadAllLocations_003Ed__4
    	{
    		[Token(Token = "0x4000007")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000008")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<List<IResourceLocation>> _003C_003Et__builder;

    		[Token(Token = "0x4000009")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AddressableResourceProviderExtended _003C_003E4__this;

    		[Token(Token = "0x400000A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private Cysharp.Threading.Tasks.UniTask<List<IResourceLocation>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000006")]
    		[Address(RVA = "0xD33A70", Offset = "0xD32470", VA = "0x180D33A70", Slot = "4")]
    		private void MoveNext()
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0xD33A70, 1190 B
    			//
    			// This state machine is *compiler-generated* from `LoadAllLocations` below; its recovered
    			// sequence now lives in `LoadAllLocationsCore`, which the C# compiler turns back into an
    			// equivalent state machine.  Writing the body here as well would duplicate the logic (and the
    			// compiler would emit a second nested state machine carrying this same name), so the recovered
    			// sequence is documented on `LoadAllLocationsCore` instead.
    			//
    			// Annotated disassembly, in state order:
    			//   tools/unity-rebuild/out/logs/AddressableResourceProviderExtended_MoveNext_0xD33A70.txt
    			//
    			// Shape (read off 0x180D33B0C..0x180D33E31): exactly ONE suspend point, so `<>u__1`
    			// (+0x28, 24 B, Cysharp UniTask<List<IResourceLocation>>.Awaiter) is the only awaiter slot and
    			// the only states are -1 (first run) and 0 (resumed after that await):
    			//   0x180D33B19  mov   eax,[rdi]            -> <>1__state (+0x0)
    			//   0x180D33B1B  mov   r14,[rdi+0x20]       -> <>4__this (+0x20)
    			//   0x180D33B21  jne   0x33B4D              -> state != 0 resumes into the *body*, so the
    			//                                            0x33B23..0x33B48 arm (restore `<>u__1`, clear it,
    			//                                            state = -1) is the post-await resume path.
    			// The body itself is documented step by step on `LoadAllLocationsCore`.
    		}

    		[Token(Token = "0x6000007")]
    		[Address(RVA = "0xD33F20", Offset = "0xD32920", VA = "0x180D33F20", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0xD33F20, 42 B
    			//
    			// Functionally empty.  This RVA is NOT the shared empty `ret 0` body at 0x30C000, but the
    			// whole function is the type-init guard plus a return:
    			//   0x180D33F24  cmp byte [rip+0x23A9462],0 / jne 0x33F45   -> one-time metadata guard
    			//   0x180D33F2D  METHODINFO Naninovel.Async.CompilerServices
    			//                .AsyncUniTaskMethodBuilder<List<IResourceLocation>>.SetStateMachine()
    			//                + il2cpp_codegen_initialize_runtime_metadata (0x250FB0)
    			//   0x180D33F45  add rsp,0x28 ; ret                            -> no managed work
    			//
    			// Log: tools/unity-rebuild/out/logs/AddressableResourceProviderExtended_SetStateMachine_0xD33F20.txt
    		}
    	}

    	[Token(Token = "0x4000006")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	private IAddressableResourceLocationProvider _addressableResourceLocationProvider;

    	[Token(Token = "0x17000001")]
    	private IAddressableResourceLocationProvider AddressableResourceLocationProvider
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD337D0, 186 B
    		//
    		// Lazily cached service getter; same `_field ??= ServiceLocator.Get<T>(true)` pattern already
    		// reconstructed repeatedly in this project (cf. SplashScreen.InputManager / OverlayFader).
    		// Native evidence:
    		//   0x180D3380B  mov   rax,[rbx+0x68]        -> the cached field (dump.cs +0x68)
    		//   0x180D33812  jne   0x33D884              -> `??` short-circuit: cached value returned as-is
    		//   0x180D33828  METHODINFO ServiceLocator.Get<IAddressablesManager>()
    		//   0x180D3382F  mov   cl,1                  -> includeDerivedType: true
    		//   0x180D33831  call  0x751910              -> ServiceLocator$$Get<object> (shared generic body)
    		//   0x180D33840  mov   [rbx+0x68],rax        -> store back into the field (`??=`), then the GC
    		//                                             write barrier for the interface field
    		//
    		// The generic argument is `IAddressablesManager`, NOT `IAddressableResourceLocationProvider`:
    		// `ServiceLocator.Get<IAddressablesManager>()` is the only ServiceLocator.Get<> MethodInfo whose
    		// name mentions either interface (src/il2cpp/script.json:995281), and no
    		// `ServiceLocator.Get<IAddressableResourceLocationProvider>()` MethodInfo exists at all.  No cast
    		// follows the call because `IAddressablesManager : IAddressableResourceLocationProvider`
    		// (dump.cs:727981), so the store into the narrower-typed field is a plain subsumption.
    		//
    		// Log: tools/unity-rebuild/out/logs/AddressableResourceProviderExtended_get_AddressableResourceLocationProvider_0xD337D0.txt
    		[Token(Token = "0x6000003")]
    		[Address(RVA = "0xD337D0", Offset = "0xD321D0", VA = "0x180D337D0")]
    		get
    		{
    			return _addressableResourceLocationProvider ??= ServiceLocator.Get<IAddressablesManager>(true);
    		}
    	}

    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xD33890", Offset = "0xD32290", VA = "0x180D33890")]
    	public AddressableResourceProviderExtended() : base("Naninovel")
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD33890, 68 B
    		//
    		// Tail call, no other work:
    		//   0x180D338BA  mov rdx,[rip+0x212783F]   -> STR 'Naninovel' (script.json:801511; the only
    		//                                             "Naninovel" string literal in the binary) = mainLabel
    		//   0x180D338C1  xor r9d,r9d               -> MethodInfo* (last parameter, per README)
    		//   0x180D338C4  xor r8d,r8d               -> extraLabels = null
    		//   0x180D338CF  jmp 0x18052EC10           -> Naninovel.AddressableResourceProvider$$.ctor
    		//                                             (dump.cs:153990, `.ctor(string mainLabel =
    		//                                             "UNITY_COMMON", string[] extraLabels)`)
    		//
    		// SEMANTICS (this is the constant the whole path -> location mapping hangs on):
    		// `AddressableResourceProvider.MainLabel` therefore becomes "Naninovel", and the base class builds
    		// its Addressables key as `$"{MainLabel}/{Path}"` in AddressableResourceLoader<TResource>
    		// (Packages/com.elringus.naninovel/Runtime/Common/ResourceProvider/AddressableResourceLoader.cs:22).
    		//
    		// Log: tools/unity-rebuild/out/logs/AddressableResourceProviderExtended_ctor_0xD33890.txt
    	}

    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xD338E0", Offset = "0xD322E0", VA = "0x180D338E0", Slot = "56")]
    	[AsyncStateMachine(typeof(_003CLoadAllLocations_003Ed__4))]
    	protected override Naninovel.UniTask<List<IResourceLocation>> LoadAllLocations()
    	{
    		// The recovered body lives in LoadAllLocationsCore: writing it *here* as an async method would
    		// make the compiler emit a nested state machine whose name collides with the decompiled
    		// `_003CLoadAllLocations_003Ed__4` retained above as evidence-bearing metadata.
    		return LoadAllLocationsCore();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD338E0 (kickoff, 393 B) / 0xD33A70 (state machine MoveNext, 1190 B)
    	//
    	// Recovered sequence (numbered; every step carries its native evidence).  Annotated disassembly:
    	//   tools/unity-rebuild/out/logs/AddressableResourceProviderExtended_LoadAllLocations_0xD338E0.txt
    	//   tools/unity-rebuild/out/logs/AddressableResourceProviderExtended_MoveNext_0xD33A70.txt
    	//
    	// KICKOFF 0x180D338E0 — an ordinary `async` kickoff, nothing game-specific: a zeroed
    	//   <LoadAllLocations>d__4 whose `<>4__this` (+0x20) = this and whose `<>1__state` (+0x0) = -1 (written
    	//   through the stack copy at 0x180D33A2A), handed to
    	//   METHODINFO Naninovel.Async.CompilerServices.AsyncUniTaskMethodBuilder<List<IResourceLocation>>
    	//   .Start<AddressableResourceProviderExtended.<LoadAllLocations>d__4>() (0x331870), then
    	//   `.get_Task()` (0x180BE4760) returns the UniTask by value.  The builder is the *Naninovel-vendored*
    	//   one (Naninovel.Async.CompilerServices), so the method returns Naninovel.UniTask<T> — while the
    	//   awaited task inside is the *Cysharp* UniTask; both appear in this one method, see step 3.
    	//
    	// MOVE-NEXT, in state order (state -1 = first run, 0 = resumed after the single await):
    	//
    	//  1. `self = <>4__this` (+0x20) and the first-run/resume fork:
    	//       0x180D33B19  mov  eax,[rdi]      ; state
    	//       0x180D33B1B  mov  r14,[rdi+0x20] ; self
    	//       0x180D33B21  jne  0x33B4D        ; state != 0 -> body; state == 0 -> resume arm
    	//       0x180D33B23..0x180D33B48        ; resume arm: awaiter = <>u__1 (+0x28, 24 B);
    	//                                       ;   <>u__1 = default; state = -1; jmp GetResult
    	//       0x180D33B4D  test r14,r14 / 0x180D33F11 THROW NullReferenceException (0x246730)
    	//  2. the inlined `AddressableResourceLocationProvider` getter — the compiler inlined the property
    	//     reconstructed above instead of calling 0xD337D0; identical shape, identical field and identical
    	//     MethodInfo token:
    	//       0x180D33B88  mov  rsi,[r14+0x68] ; cached _addressableResourceLocationProvider (+0x68)
    	//       0x180D33B8F  je   0x33BA3        ; null -> fetch
    	//       0x180D33BB8  METHODINFO ServiceLocator.Get<IAddressablesManager>()
    	//       0x180D33BBF  mov  cl,1          ; includeDerivedType: true
    	//       0x180D33BC1  call 0x751910      ; ServiceLocator$$Get<object>
    	//       0x180D33BC9  mov  [r14+0x68],rax; ??= store-back (+ write barrier)
    	//       0x180D33C24  je -> 0x180D33F0C THROW NullReferenceException ; null-check before the call below
    	//  3. the single await: `IAddressableResourceLocationProvider.GetResourceLocationsAsync(ct)`.
    	//       0x180D33C2A  TYPE GigaCreation.Essentials.AddressablesUtils
    	//                    .IAddressableResourceLocationProvider_TypeInfo, slot index r15 = 0
    	//                    (dump.cs:727923 `Slot: 0` = GetResourceLocationsAsync) -- searched in the object's
    	//                    interface list, falling back to the resolver 0x243A20
    	//       0x180D33C79  call rax            ; rcx = sret buffer, rdx = provider (this), r8 = 0,
    	//                                       ;   r9 = MethodInfo*; ONE argument, value 0
    	//       0x180D33C80  mov  r8,rbx         ; rbx was zeroed at 0x180D33B94/0x180D33C1E -> the argument is
    	//                                       ;   an all-zero CancellationToken struct = `default`
    	//       0x180D33C9D  METHODINFO Cysharp.Threading.Tasks.UniTask<List<IResourceLocation>>.GetAwaiter()
    	//       0x180D33C8C..0x180D33D43        ; the 24-byte awaiter is produced and copied to [rbp+0x10]
    	//       0x180D33D48  METHODINFO Cysharp.Threading.Tasks
    	//                    .UniTask.Awaiter<List<IResourceLocation>>.get_IsCompleted()
    	//       0x180D33DBB  je 0x33E50         ; not completed -> suspend
    	//       0x180D33E50  mov [rdi],r15d (=0)                  ; state = 0 (the resume state)
    	//       0x180D33E57  movups [rdi+0x28],awaiter (24 B)     ; <>u__1 = awaiter
    	//       0x180D33EBD  METHODINFO Naninovel.Async.CompilerServices
    	//                    .AsyncUniTaskMethodBuilder<List<IResourceLocation>>
    	//                    .AwaitUnsafeOnCompleted<Cysharp UniTask.Awaiter<List<IResourceLocation>>,
    	//                    AddressableResourceProviderExtended.<LoadAllLocations>d__4>()
    	//                    (0x18063D2D0); then return.
    	//     NOTE (evidence, not a guess): the awaited task type is the Cysharp UniTask, i.e.
    	//     `IAddressableResourceLocationProvider` returns Cysharp's UniTask<List<IResourceLocation>>,
    	//     while the enclosing method returns the Naninovel-vendored UniTask — the two coexist here.
    	//  4. `awaiter.GetResult()` -> the List<IResourceLocation>:
    	//       0x180D33DC1  METHODINFO Cysharp.Threading.Tasks
    	//                    .UniTask.Awaiter<List<IResourceLocation>>.GetResult()
    	//                    -> destination [rbp+0xB0]
    	//  5. `CacheLocations(locations)` — the base class' protected virtual hook (dump.cs:154049, Slot 57):
    	//       0x180D33DD8  test r14,r14 / 0x180D33F07 THROW NullReferenceException
    	//       0x180D33DE1  mov r8,[r14]                  ; r8 = self's Il2CppClass*
    	//       0x180D33DE4  mov rax,[r8+0x4C8]            ; VirtualInvokeData.methodPtr
    	//       0x180D33DEB  mov r8,[r8+0x4D0]             ; VirtualInvokeData.methodInfo (+8)
    	//       0x180D33DF2  mov rbx,[rbp+0xB0]            ; rbx = the awaited list
    	//       0x180D33DF9  rdx = rbx (one reference argument), rcx = r14 (this), call rax
    	//       The identity of slot 0x4C8 is pinned by the *base* implementation, whose
    	//       <LoadAllLocations>d__16.MoveNext (0x52FAF0) uses the very same [klass+0x4C8]/[klass+0x4D0]
    	//       pair with one reference argument and a discarded return at 0x52FE05/0x52FE0C, immediately
    	//       before its own SetResult — and in the base that call can only be `CacheLocations(locations)`,
    	//       the second-to-last statement of AddressableResourceProvider.LoadAllLocations
    	//       (Packages/com.elringus.naninovel/.../AddressableResourceProvider.cs:86).
    	//       Log: tools/unity-rebuild/out/logs/AddressableResourceProvider_BaseMoveNext_0x52FAF0.txt
    	//       Confirmed a second, independent way by vtable arithmetic: `get_MainLabel` is Slot 54
    	//       (dump.cs:153982) and the inlined call to it inside AddressableResourceLoader<TResource>..ctor
    	//       (0xBB8A80) goes through [klass+0x498]/[klass+0x4A0] at 0x180BB8B99/0x180BB8BA0.
    	//       VirtualInvokeData is 16 bytes, so 0x498 + 3 * 16 = 0x4C8 exactly - the pair read here -
    	//       and Slot 57 is CacheLocations.  Log:
    	//       tools/unity-rebuild/out/logs/AddressableResourceLoader_ctor_0xBB8A80.txt
    	//     This call is not cosmetic: `ResourceProvider.ResourceExists<T>`/`LocateResources<T>` switch to the
    	//     cached path as soon as `LocationsCache.Count > 0` (ResourceProvider.cs:116/129), and
    	//     `CacheLocations` fills that cache with `location.PrimaryKey.GetAfterFirst("/")`.
    	//  6. `return locations;` == builder.SetResult(locations):
    	//       0x180D33E02  mov dword [rdi],0xFFFFFFFE   ; state = -2
    	//       0x180D33E0C  METHODINFO Naninovel.Async.CompilerServices
    	//                    .AsyncUniTaskMethodBuilder<List<IResourceLocation>>.SetResult()
    	//       0x180D33E13  rdx = rbx (the same List that was passed to CacheLocations), call 0x180BE4D50
    	//     The unwind arm (0x180D33ED6) is the standard `state = -2` + builder.SetException and carries no
    	//     game logic.
    	//
    	// SEMANTICS — how a Naninovel path becomes an Addressables location in this implementation:
    	//   * This override replaces the base implementation, but NOT because the base is broken: the
    	//     shipped catalog does contain a key literally named "Naninovel" (catalog_dump_shipped.json:
    	//     key "Naninovel", location_count 28150 = exactly the number of `Naninovel/...` primary keys),
    	//     which is what the base `Addressables.LoadResourceLocationsAsync(MainLabel)` resolves.  The
    	//     override routes the same query through the game's own manager, which also handles the
    	//     additional/remote catalogs.
    	//     CORRECTION: an earlier revision of this comment claimed the shipped catalog has no "Naninovel"
    	//     label and that the base implementation could therefore never work.  That was wrong.  Beware
    	//     that the dump's `labels`/`is_label` fields are unreliable (it reports labels = ["default"] and
    	//     zero label keys, yet a label must itself appear in the key table); the key table is evidence.
    	//   * the single await above is `IAddressablesManager.GetResourceLocationsAsync` ->
    	//     `AddressablesManager.LoadResourceLocationsAsync` -> `Addressables.LoadResourceLocationsAsync(
    	//     masterData.AssetKeyNames, MergeMode.Union, null)` (r8d = 1 at 0x180CC26C6; the game's own enum
    	//     is None = 0, UseFirst = 0, Union = 1, Intersection = 2 -- dump.cs:669573..669580 -- so the
    	//     constant is Union, not UseFirst).
    	//   * `masterData.AssetKeyNames` is `WitchTrials.Models.MasterData._assetKeyNames` (+0x18), supplied
    	//     by `MasterDataProvider.Data`, which `LoadMasterData` fills with
    	//     `Resources.Load<MasterDataBase>("MasterData")` (0xCD3D40; log
    	//     tools/unity-rebuild/out/logs/MasterDataProvider_LoadMasterData_0xCD3D40.txt).  It is NOT a
    	//     per-asset list: the shipped asset decodes to exactly ONE element, the string "Naninovel"
    	//     (manosaba_Data/resources.assets @1599288: `0a 00 00 00` "MasterData", then array count
    	//     `01 00 00 00`, one element `09 00 00 00` "Naninovel", then count `00 00 00 00` for
    	//     _additionalCatalogDataList, then floats 10.0 / 5.0 / 1.5 = DefaultHp /
    	//     SkipTimeScaleDuringDebate / VoicePitchOnSkip, i.e. the field order of MasterData.cs).
    	//     So the query is Locate("Naninovel") -- the one group key covering every Naninovel-managed
    	//     asset, the 440 scripts included.
    	//   * CONTENT-SIDE consequences (neither is a defect of this class):
    	//       - `AddressablesManager.LoadResourceLocationsAsync` returns `new List<IResourceLocation>()`
    	//         and logs 'The provided master data does not include the data for Addressable Asset
    	//         System.' when `MasterDataProvider.Data as IAddressablesMasterData` is null (0xCC268B ->
    	//         0xCC29B7: Debug.LogError, il2cpp_codegen_object_new List<IResourceLocation>, .ctor,
    	//         SetResult).  This rebuild has no Resources/MasterData asset at all, so that branch is the
    	//         one currently taken.
    	//       - with an empty location list the loader's `locations.Exists(...)` guard below is false, the
    	//         asset stays null, `Resource<TResource>.Valid` is false, and `LoadOrErr` throws
    	//         Naninovel.Error naming the path and type -- NOT a NullReferenceException, which is what
    	//         the stubbed version produced.
    	//       - the catalog must also expose the key "Naninovel": this rebuild's built catalog has 32459
    	//         `Naninovel/...` primary keys (all 440 `Naninovel/Scripts/...` among them) but no
    	//         "Naninovel" key and no label other than "default".
    	//   * the shipped catalog's `primary_key` for the 440 scripts is `Naninovel/Scripts/<script>`
    	//     (e.g. `Naninovel/Scripts/Act01_Chapter01/Act01_Chapter01_Adv01`, resource_type
    	//     Naninovel.Script), one location per script.
    	//   * Loading then addresses the asset as `$"{MainLabel}/{Path}"` = "Naninovel/" + the Naninovel
    	//     resource path (PathPrefix "Scripts" + the script's path), i.e. exactly that primary key; the
    	//     base `CacheLocations` strips the leading "Naninovel/" back off for path lookup.  The prefix this
    	//     class contributes is the single string the constructor above passes to the base ctor.
    	//     Native proof for the April build (i.e. not merely the package source):
    	//     AddressableResourceLoader<TResource>..ctor (0xBB8A80) calls get_MainLabel through
    	//     [klass+0x498] and then `System.String$$Concat(MainLabel, STR '/', this.Path)` at
    	//     0x180BB8BBA, storing the result into the loader's `resourceAddress` field (+0x40) -
    	//     `this.Path` being ResourceRunner.Path at +0x10 (dump.cs:156699).
    	//   * which address form ends up where (measured on catalog_dump_shipped.json, 29097 locations /
    	//     57212 keys): the shipped key table contains ZERO keys that start with "Assets/" and zero that
    	//     end in ".nani", and no key equals any location's internal_id -- so `Assets/#WitchTrials/...` is
    	//     the location's internal_id (bundle member path), never a key.  The form that reaches
    	//     `CacheLocations` is the NANINOVEL one: `IResourceLocation.PrimaryKey` = `Naninovel/Scripts/<script>`,
    	//     re-keyed to `Scripts/<script>` (+ the location's ResourceType) for the Naninovel path dictionary,
    	//     while the load itself asks Addressables for the un-stripped `Naninovel/Scripts/<script>` string.
    	//
    	// TODO(evidence): the `ct` argument is an all-zero struct, so `default` is what the native code shows;
    	//   `CancellationToken.None` would be observationally identical, and dump.cs declares the parameter
    	//   without `[Optional]` (727924/727649), so it cannot have been omitted — but `default` vs an
    	//   equivalent spelling is not decidable from the bytes.  The reconstructed
    	//   `IAddressableResourceLocationProvider.cs` in this repo marks it `[Optional]`, which does not match
    	//   dump.cs; that file is out of scope here and was left untouched.
    	private async Naninovel.UniTask<List<IResourceLocation>> LoadAllLocationsCore()
    	{
    		var locations = await AddressableResourceLocationProvider.GetResourceLocationsAsync(default);
    		CacheLocations(locations);
    		return locations;
    	}
    }
}
