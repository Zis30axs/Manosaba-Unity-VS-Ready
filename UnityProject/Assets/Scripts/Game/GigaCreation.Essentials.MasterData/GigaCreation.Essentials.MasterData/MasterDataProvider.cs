using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.MasterData
{
    [Token(Token = "0x2000006")]
    public static class MasterDataProvider
    {
    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD3C60, 59 B (getter) and 0xCD3CA0, 151 B (setter)
    	//
    	// This is an auto-property, not a hand-written accessor pair.  dump.cs (TypeDefIndex: 18019) shows
    	//
    	//     [CompilerGenerated] private static IMasterDataBase <Data>k__BackingField; // 0x0
    	//     public static IMasterDataBase Data { get; set; }     // set_Data is private in the method list
    	//
    	// and the stub marks both accessors `[CompilerGenerated]`, which is exactly the shape Roslyn emits for
    	// `public static IMasterDataBase Data { get; private set; }`.  `<Data>k__BackingField` is a metadata-only
    	// name that is not valid C# source, so the recovered form is the auto-property whose lowered backing
    	// field carries that name; it is the only static field of this type, at static-fields offset 0x0.
    	//
    	// Native evidence for the getter (disassembled with work/reversing/disas.py 0xCD3C60):
    	//   0x180CD3C60  sub rsp,0x28                            ; frame
    	//   0x180CD3C64  cmp byte [rip+0x24094C7],0 / jne        ; one-shot type-init guard (data_30DD132)
    	//   0x180CD3C6D  lea rcx,[MasterDataProvider_TypeInfo]  ; TYPE GigaCreation.Essentials.MasterData
    	//                call il2cpp_codegen_initialize_runtime_metadata ;   .MasterDataProvider_TypeInfo
    	//   0x180CD3C85  mov rax,[rip+0x2186F2C]                 ; the same Il2CppClass*
    	//   0x180CD3C8C  mov rax,[rax+0xB8]                      ; +0xB8 = Il2CppClass::static_fields (same anchor
    	//                                                        ;   as GigaCreation.Tools.Service/ServiceLocator.cs:72-73)
    	//   0x180CD3C93  mov rax,[rax]                           ; static field at +0x0
    	//   0x180CD3C96  add rsp,0x28 / ret                      ; -> and nothing else: a bare field read
    	//   There is no type check, no cast and no call, so the property hands back the storage unmodified.
    	//
    	// Native evidence for the setter (disassembled with work/reversing/disas.py 0xCD3CA0):
    	//   0x180CD3CA0  push rbx / sub rsp,0x20
    	//   0x180CD3CA6  cmp byte [rip+0x2409486],0 / mov rbx,rcx / jne ; guard (data_30DD133); the incoming
    	//                                                                `value` (rcx - the only argument) is
    	//                                                                kept in rbx
    	//   0x180CD3CB2  lea rcx,[MasterDataProvider_TypeInfo] / call il2cpp_codegen_initialize_runtime_metadata
    	//   0x180CD3CCA  mov rax,[rip+0x2186EE7]                 ; TYPE MasterDataProvider_TypeInfo
    	//   0x180CD3CD1  mov rdx,[rax+0xB8]                      ; +0xB8 = Il2CppClass::static_fields
    	//   0x180CD3CD8  mov [rdx],rbx                           ; static field +0x0 = value
    	//   0x180CD3CDB  cmp dword [rip+0x24054E2],0 / je epilogue ; GC write barrier for that store
    	//   0x180CD3CE4..0x180CD3D2F  bts rcx,r8 / lock cmpxchg [rdx],rcx (card table data_3134580)
    	//                                                        ; scaffolding, not game logic
    	//                                                        ; (work/reversing/README.md, the two boilerplate
    	//                                                        ;  patterns that must be ignored)
    	//   0x180CD3D31  add rsp,0x20 / pop rbx / ret
    	//   The write barrier (instead of a plain 8-byte store) is itself evidence that the destination is a
    	//   *reference*-typed slot: an integer field would be stored without it.  That rules out +0x0 holding
    	//   anything other than the `IMasterDataBase` reference dump.cs names.
    	//
    	// Confidence A for both accessors: both bodies are three instructions of work around boilerplate, the
    	// field offset is pinned by dump.cs (`// 0x0`) and by the write barrier's existence, and no other member
    	// of this type can touch that slot.
    	[Token(Token = "0x17000001")]
    	public static IMasterDataBase Data
    	{
    		[Token(Token = "0x6000004")]
    		[Address(RVA = "0xCD3C60", Offset = "0xCD2660", VA = "0x180CD3C60")]
    		[CompilerGenerated]
    		get;
    		[Token(Token = "0x6000005")]
    		[Address(RVA = "0xCD3CA0", Offset = "0xCD26A0", VA = "0x180CD3CA0")]
    		[CompilerGenerated]
    		private set;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD3D40, 215 B
    	//
    	// Native evidence (annotated disassembly preserved as
    	// tools/unity-rebuild/out/logs/MasterDataProvider_LoadMasterData_0xCD3D40.txt):
    	//   1. 0x180CD3D46  cmp byte [rip+0x24093E7],0 / jne    ; one-shot guard (data_30DD134) wrapping the two
    	//                                                        metadata initializations this body performs:
    	//        0x180CD3D4F  METHODINFO Method$UnityEngine.Resources.Load<MasterDataBase>()
    	//                     - the *generic* method slot, so the type argument of the recovered call is
    	//                       `MasterDataBase` (neither `IMasterDataBase` nor the game's `MasterData`)
    	//        0x180CD3D60  STR 'MasterData'               - the path literal, from a ScriptString slot
    	//   2. 0x180CD3D78  mov rdx,[rip+0x21938C1]             ; = that MethodInfo*
    	//      0x180CD3D7F  mov rcx,[rip+0x21938B2]             ; = the 'MasterData' string
    	//      0x180CD3D86  call 0x180750150                    ; -> UnityEngine.Resources$$Load<object>
    	//      `Resources.Load<T>(string path)` is static, so there is no `this`; the string is the first
    	//      argument (rcx) and the MethodInfo* the last (rdx), per work/reversing/README.md
    	//      ("MethodInfo* 是最后一个参数，不是隐藏的第一个参数").
    	//   3. 0x180CD3DAF  mov rcx,[MasterDataProvider_TypeInfo] ; mov rdx,[rcx+0xB8] ; mov [rdx],rbx
    	//                   -> the returned object is stored into the type's static_fields block at +0x0, i.e.
    	//                      into the very slot the accessors above read and write:
    	//                      `Data = Resources.Load<MasterDataBase>("MasterData");`
    	//      The `bts rcx,r8` / `lock cmpxchg [rdx],rcx` loop at 0x180CD3DC0..0x180CD3E0F is the GC write
    	//      barrier for that reference store.
    	//   4. The method is `void` (csig `void ..._LoadMasterData (const MethodInfo* method)`), reads no other
    	//      field, calls nothing else and performs no null or type check: the single statement above is the
    	//      whole body.  There is no `if (Data == null)` guard, so a missing asset leaves `Data` null rather
    	//      than throwing in this method.
    	//
    	// Why this method matters for the boot chain (measured, not assumed): nothing else assigns `Data`, and
    	// the game's own addressables manager reads it back -
    	//   `AddressablesManager.<LoadResourceLocationsAsync>d__25.MoveNext` (RVA 0xCC24D0) does
    	//   `MasterDataProvider.Data as IAddressablesMasterData` (isinst at 0x180CC2683, null -> 0x180CC29B7 via
    	//   `test rax,rax; je`) and, when that is null, logs "The provided master data does not include the data
    	//   for Addressable Asset System." and returns an EMPTY location list without touching Addressables
    	//   (0x180CC29B7 Debug.LogError -> il2cpp_codegen_object_new List<IResourceLocation> -> .ctor ->
    	//   SetResult).  With an empty list the reconstructed Naninovel resource provider caches no locations, no
    	//   script can be resolved, and `ResourceLoaderExtensions.LoadOrErr<TResource>` fails on the requested
    	//   path - which is where the reconstructed boot chain stopped.
    	//
    	// TODO(evidence): `"MasterData"` is written as the literal because that is what the metadata contains.  It
    	//   is NOT decidable whether the original source spelled the literal or
    	//   `IMasterDataBase.MasterDataFileName`: that const has exactly this value, Roslyn inlines a const, and
    	//   both spellings therefore produce the identical ScriptString slot and the identical call site.  The two
    	//   are byte-for-byte equivalent at run time; the literal was kept because the interface's const is only
    	//   circumstantial evidence of which form was written.
    	// TODO(evidence): whether the shipped project relied on this method running again after boot.
    	//   `[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]` (dump.cs renders the same
    	//   attribute as `[RuntimeInitializeOnLoadMethod(1)]`, and BeforeSceneLoad == 1) makes Unity call it once
    	//   per player start before the first scene loads.  The body is unconditional, so from the native side it
    	//   cannot be told whether a later call is expected to be a no-op.
    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0xCD3D40", Offset = "0xCD2740", VA = "0x180CD3D40")]
    	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    	public static void LoadMasterData()
    	{
    		Data = Resources.Load<MasterDataBase>("MasterData");
    	}
    }
}
