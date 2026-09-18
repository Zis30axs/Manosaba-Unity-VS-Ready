using System;
using System.Collections.Generic;
using System.Diagnostics;
// RECONSTRUCTED (using directive, no behaviour) | Confidence: A | restored because the recovered body of
// <LoadResourceLocationsAsync>d__25.MoveNext (RVA 0xCC24D0) calls System.Linq.Enumerable.ToList: the
// MethodInfo Method$System.Linq.Enumerable.ToList<IResourceLocation>() is initialised in that body's
// type-init prologue (0x180CC258B) and its shared-generic body is inlined at 0x180CC2909..0x180CC2951.
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.WebRequestUtils;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace GigaCreation.Essentials.AddressablesUtils
{
    [Token(Token = "0x2000007")]
    public class AddressablesManager : IAddressablesManager, IContentCatalogManager, IAddressableResourceLocationProvider, IAddressableAssetLoader, IService, IDisposable
    {
    	[Serializable]
    	[Token(Token = "0x2000008")]
    	public class SettingsJson
    	{
    		[Token(Token = "0x4000016")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public SettingsJsonCatalogLocations[] m_CatalogLocations;

    		[Token(Token = "0x6000020")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public SettingsJson()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x2000009")]
    	public class SettingsJsonCatalogLocations
    	{
    		[Token(Token = "0x4000017")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public string[] m_Keys;

    		[Token(Token = "0x4000018")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public string m_InternalId;

    		[Token(Token = "0x6000021")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public SettingsJsonCatalogLocations()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000B")]
    	[CompilerGenerated]
    	private struct _003CCheckIfAdditionalCatalogExistsAsync_003Ed__19
    {
    		[Token(Token = "0x400001E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400001F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

    		[Token(Token = "0x4000020")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AddressablesManager _003C_003E4__this;

    		[Token(Token = "0x4000021")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000022")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private IEnumerator<string> _003C_003E7__wrap1;

    		[Token(Token = "0x4000023")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<string>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000024")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private TaskAwaiter<string> _003C_003Eu__2;

    		[Token(Token = "0x6000028")]
    		[Address(RVA = "0xCC0410", Offset = "0xCBEE10", VA = "0x180CC0410", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000029")]
    		[Address(RVA = "0xCC0BD0", Offset = "0xCBF5D0", VA = "0x180CC0BD0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000C")]
    	[CompilerGenerated]
    	private struct _003CCheckIfMainCatalogExistsAsync_003Ed__18
    {
    		[Token(Token = "0x4000025")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000026")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

    		[Token(Token = "0x4000027")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000028")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AddressablesManager _003C_003E4__this;

    		[Token(Token = "0x4000029")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private string _003CsettingJsonPath_003E5__2;

    		[Token(Token = "0x400002A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private TaskAwaiter<string> _003C_003Eu__1;

    		[Token(Token = "0x400002B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask<string>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600002A")]
    		[Address(RVA = "0xCC0C00", Offset = "0xCBF600", VA = "0x180CC0C00", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600002B")]
    		[Address(RVA = "0xCC1750", Offset = "0xCC0150", VA = "0x180CC1750", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000D")]
    	[CompilerGenerated]
    	private struct _003CGetOrLoadAddressableAsset_003Ed__22<T>
    {
    		[Token(Token = "0x400002C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400002D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<T> _003C_003Et__builder;

    		[Token(Token = "0x400002E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AddressablesManager _003C_003E4__this;

    		[Token(Token = "0x400002F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string address;

    		[Token(Token = "0x4000030")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000031")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask _003Ctask_003E5__2;

    		[Token(Token = "0x4000032")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private CancellationTokenSource _003ClinkedCts_003E5__3;

    		[Token(Token = "0x4000033")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000034")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<T>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600002C")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600002D")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000E")]
    	[CompilerGenerated]
    	private struct _003CGetOrLoadAddressableAsset_003Ed__23<T>
    {
    		[Token(Token = "0x4000035")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000036")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<T> _003C_003Et__builder;

    		[Token(Token = "0x4000037")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AddressablesManager _003C_003E4__this;

    		[Token(Token = "0x4000038")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AssetReference reference;

    		[Token(Token = "0x4000039")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public CancellationToken ct;

    		[Token(Token = "0x400003A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask _003Ctask_003E5__2;

    		[Token(Token = "0x400003B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400003C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<T>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600002E")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600002F")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000F")]
    	[CompilerGenerated]
    	private struct _003CGetResourceLocationsAsync_003Ed__21
    {
    		[Token(Token = "0x400003D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400003E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<List<IResourceLocation>> _003C_003Et__builder;

    		[Token(Token = "0x400003F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AddressablesManager _003C_003E4__this;

    		[Token(Token = "0x4000040")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000041")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<List<IResourceLocation>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000030")]
    		[Address(RVA = "0xCC1780", Offset = "0xCC0180", VA = "0x180CC1780", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000031")]
    		[Address(RVA = "0xCC1AC0", Offset = "0xCC04C0", VA = "0x180CC1AC0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000010")]
    	[CompilerGenerated]
    	private struct _003CLoadAdditionalCatalogAsync_003Ed__20
    {
    		[Token(Token = "0x4000042")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000043")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

    		[Token(Token = "0x4000044")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AddressablesManager _003C_003E4__this;

    		[Token(Token = "0x4000045")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000046")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private IEnumerator<string> _003C_003E7__wrap1;

    		[Token(Token = "0x4000047")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private string _003CjsonLocation_003E5__3;

    		[Token(Token = "0x4000048")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000032")]
    		[Address(RVA = "0xCC1AF0", Offset = "0xCC04F0", VA = "0x180CC1AF0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000033")]
    		[Address(RVA = "0xCC24A0", Offset = "0xCC0EA0", VA = "0x180CC24A0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000011")]
    	[CompilerGenerated]
    	private struct _003CLoadResourceLocationsAsync_003Ed__25
    {
    		[Token(Token = "0x4000049")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400004A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<List<IResourceLocation>> _003C_003Et__builder;

    		[Token(Token = "0x400004B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AddressablesManager _003C_003E4__this;

    		[Token(Token = "0x400004C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x400004D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<IList<IResourceLocation>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000034")]
    		[Address(RVA = "0xCC24D0", Offset = "0xCC0ED0", VA = "0x180CC24D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000035")]
    		[Address(RVA = "0xCC2A70", Offset = "0xCC1470", VA = "0x180CC2A70", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400000E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly IList<AsyncOperationHandle> _handles;

    	[Token(Token = "0x400000F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly IDictionary<string, object> _loadedAssets;

    	[Token(Token = "0x4000010")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private readonly IDictionary<string, UniTask> _pendingLoadAssetTasks;

    	[Token(Token = "0x4000011")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private IWebRequestManager _webRequestManager;

    	[Token(Token = "0x4000012")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	private IReadOnlyList<string> _additionalCatalogLocationsWithoutExt;

    	[Token(Token = "0x4000013")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private CancellationTokenSource _loadAssetCts;

    	// RECONSTRUCTED | Confidence: A (storage) | the two fields below are the compiler-generated backing
    	// fields of the auto-properties further down.  dump.cs:727600-727603 declares them as
    	// `private bool <IsAdditionalCatalogLoaded>k__BackingField; // 0x40` and
    	// `private List<IResourceLocation> <ResourceLocations>k__BackingField; // 0x48` -- metadata-only
    	// names that are not valid C# source, so they are renamed here and the accessors below are written
    	// out as ordinary bodies.  This is the same handling IAddressablesManager.cs:58-101 applies to its
    	// own `k__BackingField` static property.  dump.cs prints no [Token] for backing fields, so none is
    	// invented here; the offsets are dump.cs' own and match the ones the getters/setters address.
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	private bool _isAdditionalCatalogLoaded;

    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private List<IResourceLocation> _resourceLocations;

    	[Token(Token = "0x17000008")]
    	public bool IsAdditionalCatalogLoaded
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x456360 (get) / 0x5DA0D0 (set)
    		//
    		// Both accessors are `[CompilerGenerated]` auto-property accessors: dump.cs:727606 spells the
    		// property `public bool IsAdditionalCatalogLoaded { get; set; }`, and the two bodies are the
    		// shared generic-shaped ones Il2CppDumper assigns to every auto-property accessor of that
    		// storage class -- 0x456360 is the shared `bool` getter (load the field into eax) and 0x5DA0D0
    		// the shared `bool` setter (store dl into the field); neither is the shared empty `ret 0` at
    		// 0x30C000, so both really do move the value.
    		[Token(Token = "0x600000F")]
    		[Address(RVA = "0x456360", Offset = "0x454D60", VA = "0x180456360", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return _isAdditionalCatalogLoaded;
    		}
    		[Token(Token = "0x6000010")]
    		[Address(RVA = "0x5DA0D0", Offset = "0x5D8AD0", VA = "0x1805DA0D0")]
    		[CompilerGenerated]
    		private set
    		{
    			_isAdditionalCatalogLoaded = value;
    		}
    	}

    	[Token(Token = "0x17000009")]
    	protected List<IResourceLocation> ResourceLocations
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x453470 (get) / 0x30EFA0 (set)
    		//
    		// Auto-property accessors again: dump.cs:727607 spells the property
    		// `protected List<IResourceLocation> ResourceLocations { get; set; }`, 0x453470 is the shared
    		// reference getter and 0x30EFA0 the shared reference setter.  This property is NOT cosmetic: the
    		// recovered `GetResourceLocationsAsyncCore` below assigns the awaited location list to it and
    		// returns that same reference, and `Dispose` clears it -- with the previously retained stub
    		// bodies (`get => null`, empty `set`) the assignment was silently dropped and the boot chain
    		// received a null location list.  The offset in `Dispose` (0x180CBF2C7 writes [this+0x48]) and
    		// the store in GetResourceLocationsAsyncCore (0x180CC19C6 writes [this+0x48]) both address the
    		// `_resourceLocations` field declared above.
    		[Token(Token = "0x6000011")]
    		[Address(RVA = "0x453470", Offset = "0x451E70", VA = "0x180453470")]
    		[CompilerGenerated]
    		get
    		{
    			return _resourceLocations;
    		}
    		[Token(Token = "0x6000012")]
    		[Address(RVA = "0x30EFA0", Offset = "0x30D9A0", VA = "0x18030EFA0")]
    		[CompilerGenerated]
    		set
    		{
    			_resourceLocations = value;
    		}
    	}

    	[Token(Token = "0x1700000A")]
    	private IWebRequestManager WebRequestManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCBE940, 186 B (0xBA)
    		//
    		// Lazily cached service getter; the same `_field ??= ServiceLocator.Get<T>(true)` pattern this
    		// repository already reconstructed for AddressableResourceProviderExtended's
    		// AddressableResourceLocationProvider (0xD337D0) and for SplashScreen.InputManager/OverlayFader.
    		// Annotated disassembly of this RVA:
    		//   0x180CBE97B  mov rax,[rbx+0x28]                 -> the cached _webRequestManager (+0x28)
    		//   0x180CBE982  jne 0x180CBE9F4                    -> `??` short-circuit: cached value returned as-is
    		//   0x180CBE998  METHODINFO ServiceLocator.Get<IWebRequestManager>()
    		//   0x180CBE99F  mov cl,1                          -> includeDerivedType: true
    		//   0x180CBE9A1  call 0x751910                     -> ServiceLocator$$Get<object> (shared body)
    		//   0x180CBE9B0  mov [rbx+0x28],rax                -> store back into the field (`??=`), then the
    		//                                                    standard GC write barrier for an interface field
    		// The generic argument is pinned by the MethodInfo token loaded at 0x180CBE952/0x180CBE998, which
    		// names IWebRequestManager; field +0x28 is `_webRequestManager` per dump.cs:727597.
    		//
    		// Log: tools/unity-rebuild/out/logs/AddressablesManager_get_WebRequestManager_0xCBE940.txt
    		[Token(Token = "0x6000013")]
    		[Address(RVA = "0xCBE940", Offset = "0xCBD340", VA = "0x180CBE940")]
    		get
    		{
    			return _webRequestManager ??= ServiceLocator.Get<IWebRequestManager>(true);
    		}
    	}

    	[Token(Token = "0x1700000B")]
    	private CancellationTokenSource LoadAssetCts
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCBEA00, 170 B (0xAA)
    		//
    		// Same `??=` lazy-init shape as the getter above, but the value is allocated instead of resolved:
    		//   0x180CBEA2A  mov rax,[rbx+0x38]                 -> the cached _loadAssetCts (+0x38)
    		//   0x180CBEA31  jne 0x180CBEAA4                    -> `??` short-circuit
    		//   0x180CBEA33  TYPE System.Threading.CancellationTokenSource_TypeInfo
    		//   0x180CBEA3A  call 0x1802980E0                   -> il2cpp_codegen_object_new
    		//   0x180CBEA47/0x180CBEA5A  writes the object's own +0x24 = -1 and +0x20 = 1 -- the inlined
    		//                            CancellationTokenSource() constructor (its managed body is not
    		//                            reached through a call because IL2CPP inlined it), i.e.
    		//                            `new CancellationTokenSource()` rather than a resolved service
    		//   0x180CBEA61  mov [rbx+0x38],rax                -> `??=` store-back + GC write barrier
    		// The field is the one `ReleaseHandles` cancels, disposes and nulls (0x180CBF608..0x180CBF666)
    		// and the one `GetOrLoadAddressableAsset` links against.
    		//
    		// Log: tools/unity-rebuild/out/logs/AddressablesManager_get_LoadAssetCts_0xCBEA00.txt
    		[Token(Token = "0x6000014")]
    		[Address(RVA = "0xCBEA00", Offset = "0xCBD400", VA = "0x180CBEA00")]
    		get
    		{
    			return _loadAssetCts ??= new CancellationTokenSource();
    		}
    	}

    	[Token(Token = "0x6000015")]
    	[Address(RVA = "0xCBEAB0", Offset = "0xCBD4B0", VA = "0x180CBEAB0", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CCheckIfMainCatalogExistsAsync_003Ed__18))]
    	public UniTask<bool> CheckIfMainCatalogExistsAsync([Optional] CancellationToken ct)
    	{
    		return default(UniTask<bool>);
    	}

    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0xCBECB0", Offset = "0xCBD6B0", VA = "0x180CBECB0", Slot = "6")]
    	[AsyncStateMachine(typeof(_003CCheckIfAdditionalCatalogExistsAsync_003Ed__19))]
    	public UniTask<bool> CheckIfAdditionalCatalogExistsAsync([Optional] CancellationToken ct)
    	{
    		return default(UniTask<bool>);
    	}

    	[Token(Token = "0x6000017")]
    	[Address(RVA = "0xCBEEB0", Offset = "0xCBD8B0", VA = "0x180CBEEB0", Slot = "7")]
    	[AsyncStateMachine(typeof(_003CLoadAdditionalCatalogAsync_003Ed__20))]
    	public UniTask<bool> LoadAdditionalCatalogAsync([Optional] CancellationToken ct)
    	{
    		return default(UniTask<bool>);
    	}

    	[Token(Token = "0x6000018")]
    	[Address(RVA = "0xCBF0B0", Offset = "0xCBDAB0", VA = "0x180CBF0B0", Slot = "8")]
    	[AsyncStateMachine(typeof(_003CGetResourceLocationsAsync_003Ed__21))]
    	public UniTask<List<IResourceLocation>> GetResourceLocationsAsync([Optional] CancellationToken ct)
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCBF0B0, 504 B (0x1F8)
    		//
    		// Native evidence (tools/unity-rebuild/out/logs/AddressablesManager_GetResourceLocationsAsync_0xCBF0B0.txt):
    		//   Standard async kickoff for a 24-byte-by-value return: rcx = return buffer (sret) -> rsi,
    		//   rdx = __this -> rdi, r8 = ct -> rbx. The stack state machine at [rsp+0x40] is the retained
    		//   d__21 struct exactly: state +0x40, builder +0x48 (zeroed), <>4__this +0x60 (written from rdi
    		//   under a GC write barrier) and ct +0x68 (written from rbx under a GC write barrier). Then
    		//   `state = -1` (0x180CBF25A), METHODINFO
    		//   AsyncUniTaskMethodBuilder<List<IResourceLocation>>.Start<AddressablesManager.<GetResourceLocationsAsync>d__21>()
    		//   (0x331870) and that builder's get_Task() (0x355DD0) copied into the return buffer -- i.e. a
    		//   plain `async UniTask<List<IResourceLocation>>` method with nothing else in it.
    		//
    		// The recovered body lives in GetResourceLocationsAsyncCore: writing it *here* as an async method
    		// would make the compiler emit a nested state machine whose name collides with the decompiled
    		// `_003CGetResourceLocationsAsync_003Ed__21` retained above as evidence-bearing metadata.
    		return GetResourceLocationsAsyncCore(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCC1780 (state machine body, 819 B = 0x333) /
    	// 0xCBF0B0 (kickoff, 504 B = 0x1F8)
    	//
    	// Native evidence (annotated disassembly:
    	// tools/unity-rebuild/out/logs/AddressablesManager_GetResourceLocationsAsync_MoveNext_0xCC1780.txt and
    	// tools/unity-rebuild/out/logs/AddressablesManager_GetResourceLocationsAsync_0xCBF0B0.txt).
    	//
    	// State-machine layout as retained above -- state +0x0, builder +0x8, <>4__this +0x20, ct +0x28,
    	// UniTask<List<IResourceLocation>>.Awaiter <>u__1 +0x30: one awaiter slot and only two reachable
    	// states, so the body contains exactly ONE await:
    	//   1. 0x180CC1814  eax = [rbx] (`state`); `test eax,eax / jne 0x180CC1848`.
    	//      - state != 0 (entry, 0x180CC1848): the awaited expression, steps 3-5.
    	//      - state == 0 (the single resume arm, 0x180CC181E..0x180CC1843): <>u__1 is copied out of
    	//        [rbx+0x30] into a stack temp, [rbx+0x30..0x48] is re-zeroed and `state = -1`, then straight
    	//        to the awaiter's GetResult at 0x180CC199F. Nothing follows the await.
    	//   2. 0x180CC1816  rdi = [rbx+0x20] = d__21.<>4__this; 0x180CC184B `test rdi,rdi / je 0x180CC1AAE`
    	//      is the NullReferenceException arm of the instance call (THROW at 0x180CC1AAE).
    	//   3. 0x180CC185E  r8 = [rbx+0x28] = d__21.ct; rdx = rdi (`this`); rcx = [rbp+0x40] = the hidden
    	//      return buffer; `call 0x180CBF320` = AddressablesManager.LoadResourceLocationsAsync, the
    	//      sibling kickoff 624 bytes later in this very class (which starts <LoadResourceLocationsAsync>d__25).
    	//      So GetResourceLocationsAsync does not talk to Addressables at all: it forwards its own ct
    	//      verbatim to the class' own LoadResourceLocationsAsync, and that is its single await.
    	//      The returned 24-byte UniTask is spilled to [rbp+0x28] for the GetAwaiter argument.
    	//   4. 0x180CC187F..0x180CC199D  UniTask<List<IResourceLocation>>.GetAwaiter (MethodInfo loaded from
    	//      the rip-relative slot +0x21A47C2) + awaiter.get_IsCompleted (+0x21A471F); when not completed:
    	//      `state = 0` (0x180CC1A13), <>u__1 = the 24-byte awaiter ([rbx+0x30],
    	//      0x180CC1A19..0x180CC1A26) and
    	//      AwaitUnsafeOnCompleted<UniTask.Awaiter<List<IResourceLocation>>, ..d__21> (0x180CC1A46).
    	//   5. 0x180CC199F  awaiter.GetResult() -> [rbp+0xA0] (shared-generic body 0x331BF0); then
    	//      0x180CC19B6 `test rdi,rdi / je 0x180CC1AA8` (the null check of the implicit second `ldarg.0`)
    	//      and 0x180CC19C6 `mov r9,[rbp+0xA0]` / `mov [rdi+0x48],r9` =
    	//      `this.<ResourceLocations>k__BackingField = result` (dump.cs:727603; the backing field of the
    	//      protected auto-property `ResourceLocations`, +0x48), followed by the GC write barrier at
    	//      0x180CC19D3..0x180CC1A11 (guard `data_30D91C4`, card table `data_3134580`).
    	//   6. 0x180CC1A4E  `state = -2` and builder.SetResult(r9) with the SAME value that was just stored
    	//      (0x180CC1A5F `mov rdx,r9`) -- the returned value is the assignment expression's value. This
    	//      body's metadata prologue contains no MethodInfo for get_ResourceLocations, so the value is not
    	//      re-read through the property: `ResourceLocations = await ...; return ResourceLocations;` is
    	//      ruled out by the bytes. The SetException arm at 0x180CC1A7A is the standard unwind.
    	//
    	// TODO(evidence): steps 3-6 pin the data flow but not the source spelling. `return ResourceLocations
    	//   = await LoadResourceLocationsAsync(ct);` (used below) and `var locations = await
    	//   LoadResourceLocationsAsync(ct); ResourceLocations = locations; return locations;` compile to this
    	//   same body -- a local that is only used after the single await is not hoisted, and the retained
    	//   d__21 struct indeed has no 5__ field.
    	private async UniTask<List<IResourceLocation>> GetResourceLocationsAsyncCore(CancellationToken ct)
    	{
    		return ResourceLocations = await LoadResourceLocationsAsync(ct);
    	}

    	[Token(Token = "0x6000019")]
    	[AsyncStateMachine(typeof(_003CGetOrLoadAddressableAsset_003Ed__22<>))]
    	public UniTask<T> GetOrLoadAddressableAsset<T>(string address, [Optional] CancellationToken ct)
    	{
    		// RECONSTRUCTED | Confidence: B (contract and every member it touches pinned; source spelling inferred)
    		//
    		// The recovered contract lives in tools/unity-rebuild/out/addressables_manager_spec.md section 3.1.
    		// The state machine retained above (`_003CGetOrLoadAddressableAsset_003Ed__22<T>`) is the evidence:
    		// its fields pin the whole body shape --
    		//   _003Ctask_003E5__2       : UniTask        -> one NEW non-generic UniTask local
    		//   _003ClinkedCts_003E5__3  : CancellationTokenSource -> a LINKED source is created here
    		//   _003C_003Eu__1           : UniTask.Awaiter   -> the leading await (IAddressablesManager.InitializeAsync)
    		//   _003C_003Eu__2           : UniTask<T>.Awaiter -> the real asset load
    		// `_linkedCts_5__3` is what distinguishes this overload from the AssetReference one below, which has
    		// no such field and therefore forwards here instead of creating its own source.
    		//
    		// WHY THIS MATTERS (the measured round-45 failure): as a `return default(UniTask<T>)` stub this
    		// method awaited nothing and produced `default(T)` = null, so every AssetReference load in the game
    		// silently returned null. `AuthorTextBuilder.LoadDataAsync` (AuthorTextBuilder.cs:493) then threw
    		// NullReferenceException on `characterData.Items`, the fault travelled through the tuple-await and
    		// `UniTask.WhenAll` into `UIManager.InstantiateUIs` (UIManager.cs:344) and out of
    		// `Engine.Initialize` (Engine.cs:182), and `RuntimeInitializer` never observed it (it polls
    		// `IsCompleted`, which is true for a faulted task too) -- so `initializeTCS` stayed Pending and the
    		// engine never finished initializing while the game carried on playing.
    		//
    		// The body lives in a `Core` helper for the same reason as IAddressablesManager.InitializeAsyncCore:
    		// spelling it `async` here would make the compiler emit a nested state machine whose name collides
    		// with the retained d__22 struct above.
    		return GetOrLoadAddressableAssetCore<T>(address, ct);
    	}

    	// RECONSTRUCTED | Confidence: B | the recovered body of the `string` overload (see the note above).
    	// One `UniTask<T>` await and one non-generic `UniTask` await, matching d__22's two awaiter slots.
    	private async UniTask<T> GetOrLoadAddressableAssetCore<T>(string address, CancellationToken ct)
    	{
    		// 1. The one-time Addressables gate every async member of this class begins with
    		//    (spec section 6.1: d__22 and d__23 each hold `task_5__2 : UniTask` + `<>u__1 : UniTask.Awaiter`
    		//    ahead of the `<>u__2 : UniTask<T>.Awaiter`, and the interface's own `InitializeAsync` is the
    		//    only declared one-time entry point, guarded by the static InitializeUcs).
    		await IAddressablesManager.InitializeAsync(ct);

    		// 2. Fast path: "get OR load" -- a second request for the same address must not reload it.
    		if (_loadedAssets.TryGetValue(address, out object cached) && cached is T cachedAsset)
    			return cachedAsset;

    		// 3. Coalesce a concurrent request for the same address onto the in-flight load. The dictionary
    		//    holds the NON-generic projection of that load (that is why the field is IDictionary<string,
    		//    UniTask>), and the value is read back out of _loadedAssets afterwards.
    		if (_pendingLoadAssetTasks.TryGetValue(address, out UniTask pending))
    		{
    			await pending;
    			return _loadedAssets.TryGetValue(address, out object loaded) && loaded is T loadedAsset
    				? loadedAsset
    				: default;
    		}

    		// 4. Link the caller's token with the manager-wide LoadAssetCts so Dispose() can abort in-flight
    		//    loads (ReleaseHandles cancels exactly this source, spec section 3.3).
    		using CancellationTokenSource linkedCts =
    			CancellationTokenSource.CreateLinkedTokenSource(ct, LoadAssetCts.Token);

    		AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);

    		// 5. The handle must be retained: ReleaseHandles() releases what _handles holds, and the bundles
    		//    would otherwise be unloaded while the asset is still in use.
    		_handles.Add(handle);

    		UniTask<T> task = handle.ToUniTask(cancellationToken: linkedCts.Token, cancelImmediately: true);
    		_pendingLoadAssetTasks[address] = task;
    		try
    		{
    			T asset = await task;
    			if (handle.Status != AsyncOperationStatus.Succeeded) return default;

    			_loadedAssets[address] = asset;
    			return asset;
    		}
    		finally
    		{
    			// Removed here so a faulted or cancelled load does not poison the address forever.
    			_pendingLoadAssetTasks.Remove(address);
    		}
    	}

    	[Token(Token = "0x600001A")]
    	[AsyncStateMachine(typeof(_003CGetOrLoadAddressableAsset_003Ed__23<>))]
    	public UniTask<T> GetOrLoadAddressableAsset<T>(AssetReference reference, [Optional] CancellationToken ct)
    	{
    		// RECONSTRUCTED | Confidence: B (contract pinned; source spelling inferred) | see the note on the
    		// `string` overload above and spec section 3.2.
    		//
    		// The retained d__23 struct has NO `_003ClinkedCts_003E5__3` field. That is structural proof this
    		// overload does not create a CancellationTokenSource of its own: it forwards the reference's runtime
    		// key to the `string` overload, which creates the linked source. `<>u__1 : UniTask.Awaiter` is the
    		// same leading gate as above and `<>u__2 : UniTask<T>.Awaiter` is the forwarded load.
    		//
    		// `reference.LoadAssetAsync<T>()` is deliberately NOT used (spec section 3.2): it would bypass
    		// _loadedAssets / _pendingLoadAssetTasks / _handles and leak a handle ReleaseHandles cannot release.
    		return GetOrLoadAddressableAssetFromReferenceCore<T>(reference, ct);
    	}

    	// RECONSTRUCTED | Confidence: B | the recovered body of the AssetReference overload.
    	private async UniTask<T> GetOrLoadAddressableAssetFromReferenceCore<T>(AssetReference reference, CancellationToken ct)
    	{
    		await IAddressablesManager.InitializeAsync(ct);

    		// A null reference and a reference with no runtime key both fail fast instead of reaching
    		// Addressables with an empty key.
    		if (reference == null || !reference.RuntimeKeyIsValid()) return default;

    		return await GetOrLoadAddressableAssetCore<T>(reference.RuntimeKey.ToString(), ct);
    	}

    	[Token(Token = "0x600001B")]
    	[Address(RVA = "0xCBF2B0", Offset = "0xCBDCB0", VA = "0x180CBF2B0", Slot = "11")]
    	public void Dispose()
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCBF2B0, 104 B (0x68)
    		//
    		// Two statements, nothing else:
    		//   0x180CBF2B6  xor edx,edx                          -> the callee's MethodInfo* slot (this
    		//                                                        build's null, per README: no managed work)
    		//   0x180CBF2BB  call 0x180CBF520                      -> this.ReleaseHandles() (the 489-byte
    		//                                                        function 624 bytes further down)
    		//   0x180CBF2C7  mov qword [rbx+0x48], 0              -> this.<ResourceLocations>k__BackingField
    		//                                                        = null, i.e. `ResourceLocations = null;`
    		//                                                        (the auto-property setter inlined;
    		//                                                        dump.cs:727603 puts that field at +0x48,
    		//                                                        and 0x180CC19C6 in
    		//                                                        GetResourceLocationsAsyncCore writes the
    		//                                                        same offset)
    		//   0x180CBF2D1..0x180CBF30F  the standard GC write barrier for that reference store
    		// Clearing ResourceLocations is what lets a second Engine.Initialize on a fresh
    		// AddressableResourceProviderExtended re-run LoadAllLocations instead of serving a stale cache
    		// (ResourceProvider.LocationsCache is refilled from the returned list).
    		//
    		// Log: tools/unity-rebuild/out/logs/AddressablesManager_Dispose_0xCBF2B0.txt
    		ReleaseHandles();
    		ResourceLocations = null;
    	}

    	[Token(Token = "0x600001C")]
    	[Address(RVA = "0xCBF320", Offset = "0xCBDD20", VA = "0x180CBF320")]
    	[AsyncStateMachine(typeof(_003CLoadResourceLocationsAsync_003Ed__25))]
    	protected UniTask<List<IResourceLocation>> LoadResourceLocationsAsync([Optional] CancellationToken ct)
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCBF320, 504 B (0x1F8)
    		//
    		// Native evidence (tools/unity-rebuild/out/logs/AddressablesManager_LoadResourceLocationsAsync_0xCBF320.txt):
    		//   Byte-for-byte the same kickoff shape as GetResourceLocationsAsync's above (identical 504-byte
    		//   size, only the Start<> MethodInfo differs): rcx = return buffer (sret) -> rsi, rdx = __this ->
    		//   rdi, r8 = ct -> rbx; stack state machine at [rsp+0x40] = the retained d__25 struct (state
    		//   +0x40, builder +0x48, <>4__this +0x60 and ct +0x68, both written under a GC write barrier);
    		//   `state = -1`, AsyncUniTaskMethodBuilder<List<IResourceLocation>>
    		//   .Start<AddressablesManager.<LoadResourceLocationsAsync>d__25>() (0x331870) and
    		//   get_Task() (0x355DD0) into the return buffer.
    		//
    		// The recovered body lives in LoadResourceLocationsAsyncCore: writing it *here* as an async method
    		// would make the compiler emit a nested state machine whose name collides with the decompiled
    		// `_003CLoadResourceLocationsAsync_003Ed__25` retained above as evidence-bearing metadata.
    		return LoadResourceLocationsAsyncCore(ct);
    	}

    	// RECONSTRUCTED | Confidence: A (every call, constant, field offset and branch pinned; only the
    	// step-9 source spelling is not separable from the bytes -- see the TODO(evidence) below) |
    	// RVA: 0xCC24D0 (state machine body, 1426 B = 0x592) / 0xCBF320 (kickoff, 504 B = 0x1F8)
    	//
    	// Native evidence (annotated disassembly:
    	// tools/unity-rebuild/out/logs/AddressablesManager_LoadResourceLocationsAsync_MoveNext_0xCC24D0.txt,
    	// tools/unity-rebuild/out/logs/AddressablesManager_LoadResourceLocationsAsync_0xCBF320.txt; the callees
    	// it names are dumped in the same directory as
    	// Addressables_LoadResourceLocationsAsync_0x1887290.txt, AsyncOperationHandle_op_Implicit_0xBD19B0.txt,
    	// AsyncOperationHandle_ctor_folded_0x8D9010.txt, ICollection_AsyncOperationHandle_Add_0x660F0.txt,
    	// Enumerable_ToList_0x68BEF0.txt and interface_invoke_thunk_0x22E0.txt).
    	//
    	// State-machine layout as retained above -- state +0x0, builder +0x8, <>4__this +0x20, ct +0x28,
    	// UniTask<IList<IResourceLocation>>.Awaiter <>u__1 +0x30: one awaiter slot and two reachable states,
    	// and the master-data-null branch reaches SetResult without ever touching <>u__1 -- so exactly ONE
    	// await, on the other branch. Numbered recovered sequence:
    	//
    	//   1. 0x180CC2616  eax = [rbx] (`state`); `test eax,eax / jne 0x180CC264A`.
    	//      - state == 0 (resume, 0x180CC2620..0x180CC2645): <>u__1 copied out of [rbx+0x30], the slot
    	//        re-zeroed, `state = -1`, straight to the awaiter's GetResult at 0x180CC28E6.
    	//      - state != 0 (entry, 0x180CC264A): steps 2-10 below. rdi = [rbx+0x20] = <>4__this
    	//        (0x180CC2618).
    	//   2. 0x180CC266B  `mov rax,[MasterDataProvider_TypeInfo]` / `mov rcx,[rax+0xb8]` (the static-field
    	//      base) / `mov rcx,[rcx]` = static field +0x0, and dump.cs:747571 pins that as
    	//      `MasterDataProvider.<Data>k__BackingField` of type IMasterDataBase -> `MasterDataProvider.Data`.
    	//      0x180CC2683 `call 0x180298020` with rdx = TYPE ...IAddressablesMasterData_TypeInfo is il2cpp's
    	//      isinst-style helper (it returns null instead of throwing; the same helper is what
    	//      ServiceLocator.cs' reconstruction reads for `is IDisposable`), and 0x180CC268B
    	//      `test rax,rax / je 0x180CC29B7` -- so the C# is `MasterDataProvider.Data as
    	//      IAddressablesMasterData` (a castclass would have thrown), and the null branch is taken both
    	//      when Data is null and when it is not an IAddressablesMasterData.
    	//   3. 0x180CC29B7 (masterData == null): TYPE UnityEngine.Debug_TypeInfo drives the class-init, then
    	//      0x180CC29CE rcx = the string literal 'The provided master data does not include the data for
    	//      Addressable Asset System.' and 0x180CC29D5 `call 0x181FEEA50` =
    	//      UnityEngine.Debug.LogError(object) -- its csig has exactly one message parameter, rdx = 0 being
    	//      this build's NULL MethodInfo* slot, exactly as for the Addressables call in step 5. Then
    	//      0x180CC29DA il2cpp_codegen_object_new on TYPE
    	//      System.Collections.Generic.List<IResourceLocation>_TypeInfo + List<IResourceLocation>..ctor()
    	//      (0x180857BA0, the empty ctor: the same MethodInfo also appears in this body's type-init prologue
    	//      at 0x180CC25BE) = `new List<IResourceLocation>()`, and 0x180CC29F9 `state = -2` +
    	//      builder.SetResult(that list). NO throw, and NOT `default`: the branch returns an EMPTY LIST.
    	//   4. 0x180CC2691  `xor ecx,ecx` (slot index 0) / rdx = TYPE IAddressablesMasterData_TypeInfo /
    	//      r8 = masterData / 0x180CC269D `call 0x1800022E0`, the per-slot interface invoke thunk (it
    	//      walks klass->interfaceOffsets for that TypeInfo, dispatching the slot, with 0x180243A20 as the
    	//      resolve fallback) -> `IAddressablesMasterData.get_AssetKeyNames`, which dump.cs:728022-728023
    	//      pins as `Slot: 0`. Result -> rsi. The property type is IEnumerable<string>, not a List.
    	//   5. 0x180CC26A5  class-init guard for TYPE UnityEngine.AddressableAssets.Addressables; then
    	//      0x180CC26BA `mov qword [rsp+0x20],0` (the 5th argument slot = MethodInfo*), `xor r9d,r9d`,
    	//      `mov r8d,1`, `rdx = rsi` (keys), `rcx = [rbp+0x38]` (hidden return buffer) and
    	//      0x180CC26D3 `call 0x181887290` = UnityEngine.AddressableAssets.Addressables
    	//      .LoadResourceLocationsAsync. The callee's csig is
    	//      `AsyncOperationHandle<IList<IResourceLocation>> LoadResourceLocationsAsync(IEnumerable keys,
    	//      int32 mode, System.Type type, const MethodInfo*)`, matching dump.cs:669719 which spells the
    	//      2nd parameter `Addressables.MergeMode mode`; its prologue moves rdx -> rbp (keys),
    	//      r8d -> esi (mode) and r9 -> rdi (type), so **r8d = 1 is the merge-mode constant** and
    	//      r9 = 0 is the type filter (null). dump.cs:669573-669580 renders that enum as
    	//      None = 0, UseFirst = 0, Union = 1, Intersection = 2 -> the constant is
    	//      **Addressables.MergeMode.Union = 1**, not UseFirst (which shares 0 with None and could not
    	//      produce a 1 here anyway). The returned 24-byte AsyncOperationHandle<IList<IResourceLocation>>
    	//      is copied to [rbp+0x60] (0x180CC26D8..0x180CC26E4).
    	//      NOTE: this overload has no CancellationToken parameter at all, so `ct` cannot reach it; the
    	//      only use of `ct` in this whole body is step 7.
    	//   6. 0x180CC26EC..0x180CC2762  `_handles.Add(handle)`, placed BEFORE the ToUniTask call of step 7:
    	//      0x180CC26EC `test rdi,rdi / je 0x180CC2A5D` (THROW), 0x180CC26F5 `mov rdi,[rdi+0x10]` =
    	//      this._handles (dump.cs:727594, IList<AsyncOperationHandle>). The implicit
    	//      `AsyncOperationHandle<T> -> AsyncOperationHandle` conversion is inlined: 0x180CC273B
    	//      `call 0x1808D9010` with rdx/r8d/r9 = the handle's three qwords is instruction-for-instruction
    	//      the body of AsyncOperationHandle<TObject>.op_Implicit (0xBD19B0, dump.cs:649103), which itself
    	//      ends in that same folded 3-argument
    	//      `AsyncOperationHandle..ctor(IAsyncOperation op, int version, string locationName)` body
    	//      (0x8D9010 writes [rcx]=op, [rcx+8]=version, [rcx+0x10]=locationName). Then 0x180CC2762
    	//      `call 0x1800660F0` with r8 = _handles and r9 = &(the converted 24-byte handle): that thunk
    	//      hard-codes TYPE ICollection<AsyncOperationHandle>_TypeInfo and slot 2, and dump.cs:61882-61883
    	//      lists `ICollection<T>` `Slot: 2  void Add(T item)` (IList<T> does not redeclare Add, so the
    	//      interface method resolved for `_handles.Add(...)` is ICollection<T>.Add). The handle therefore
    	//      stays registered for ReleaseHandles(). The Addressables call of step 5 appears only once while
    	//      its result is used twice (here and in step 7), so the C# must hold it in a local variable.
    	//   7. 0x180CC2767..0x180CC27AB  the awaited task: the ORIGINAL handle (from [rbp+0x60]) is copied to
    	//      [rbp] and passed as the extension method's `this`; [rsp+0x20] = 0, [rsp+0x28] = 0,
    	//      [rsp+0x30] = the UniTask return buffer, [rsp+0x38] = METHODINFO
    	//      Method$Cysharp.Threading.Tasks.AddressablesAsyncExtensions
    	//      .ToUniTask<IList<IResourceLocation>>(), r9 = [rbx+0x28] = d__25.ct, rdx = 0, r8d = 8, and
    	//      0x180CC27AB `call 0x18064E330`. The callee's csig is
    	//      `UniTask<T> ToUniTask<T>(AsyncOperationHandle<T> handle, IProgress<float> progress,
    	//      int32 timing, CancellationToken cancellationToken, bool cancelImmediately,
    	//      bool autoReleaseWhenCanceled, const MethodInfo*)`, which matches the installed package verbatim
    	//      (Packages/com.cysharp.unitask/Addressables/AddressablesAsyncExtensions.cs:282); every parameter
    	//      after the handle is optional there, and the three argument registers carry
    	//      progress = null, timing = 8 = PlayerLoopTiming.Update (PlayerLoopHelper.cs) and
    	//      cancelImmediately/autoReleaseWhenCanceled = false -- i.e. all defaults, so the only argument
    	//      the source supplied is the token: `handle.ToUniTask(cancellationToken: ct)`. `ct` is thus
    	//      forwarded into the UniTask wrapper, but (step 5) NOT into Addressables.LoadResourceLocationsAsync.
    	//   8. 0x180CC27C2..0x180CC28E0  UniTask<IList<IResourceLocation>>.GetAwaiter (+0x21A37C7) +
    	//      awaiter.get_IsCompleted (+0x21A372C); when not completed `state = 0` (0x180CC297C),
    	//      <>u__1 = the 24-byte awaiter ([rbx+0x30], 0x180CC2982..0x180CC298F) and
    	//      AwaitUnsafeOnCompleted<UniTask.Awaiter<IList<IResourceLocation>>, ..d__25> (0x180CC29AF).
    	//   9. 0x180CC28E6  awaiter.GetResult() -> [rbp+0xA0] / r14 (0x180CC28FD); 0x180CC2904
    	//      `test r14,r14 / je 0x180CC295B`, and 0x180CC295B is again `new List<IResourceLocation>()`.
    	//      The non-null arm is Enumerable.ToList<IResourceLocation> with its body INLINED:
    	//      0x180CC2909..0x180CC2951 reproduces the shared-generic Enumerable.ToList body at 0x68BEF0
    	//      (dump.cs:581947-581951) instruction for instruction -- including the
    	//      `cmp qword [rdi+0x38],0 / call 0x2435F0` rgctx fixup and the object_new + `List<T>(IEnumerable<T>)`
    	//      pairing -- MINUS its null-throw, which the C++ compiler could delete only because the enclosing
    	//      branch had already proved r14 != null. That deletion is the positive proof that the null test is
    	//      a source-level one: ToList itself still throws ArgumentNullException for null (0x18068BF63 loads
    	//      STR 'source' for System.Linq.Error$$ArgumentNull), and neither that helper nor its type appears
    	//      in this body's type-init prologue (0x180CC24F6..0x180CC2602). 0x180CC2956
    	//      `test rdi,rdi / jne 0x180CC297A` is then the `??` guard on the ToList result, whose fall-through
    	//      is once more the empty list; the ToList result and the empty-list block converge on the single
    	//      `state = -2` + builder.SetResult(rdi) at 0x180CC29F9.
    	//   10. 0x180CC2A29 is the standard SetException unwind; no game logic.
    	//
    	// TODO(evidence): the source spelling of step 9. `x?.ToList() ?? new List<IResourceLocation>()`
    	//   (used below) and `(x != null ? x.ToList() : null) ?? new List<IResourceLocation>()` are
    	//   indistinguishable in these bytes; what IS pinned is (a) an explicit null test on the awaited value
    	//   BEFORE ToList, (b) a `??`-shaped null test on the ToList result, and (c) `new List<IResourceLocation>()`
    	//   as the fallback of both. A plain ternary without `??` (`x != null ? x.ToList() : new List<...>()`)
    	//   is ruled out: it would leave the ToList result untested, yet 0x180CC2956 tests it and falls through
    	//   to the empty list. Also open: the local variable `handle` of step 6 could have been written as a
    	//   `var`/typed local and the Addressables call could in principle have been spelled with an explicit
    	//   `AsyncOperationHandle<IList<IResourceLocation>>` type name -- the metadata carries the type either way
    	//   (the retained d__25 struct has no 5__ field, so nothing was hoisted).
    	private async UniTask<List<IResourceLocation>> LoadResourceLocationsAsyncCore(CancellationToken ct)
    	{
    		IAddressablesMasterData masterData = GigaCreation.Essentials.MasterData.MasterDataProvider.Data as IAddressablesMasterData;
    		if (masterData == null)
    		{
    			UnityEngine.Debug.LogError("The provided master data does not include the data for Addressable Asset System.");
    			return new List<IResourceLocation>();
    		}

    		AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync(masterData.AssetKeyNames, Addressables.MergeMode.Union, null);
    		_handles.Add(handle);

    		return (await handle.ToUniTask(cancellationToken: ct))?.ToList() ?? new List<IResourceLocation>();
    	}

    	[Token(Token = "0x600001D")]
    	[Address(RVA = "0xCBF520", Offset = "0xCBDD20", VA = "0x180CBF520")]
    	protected void ReleaseHandles()
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCBF520, 1465 B (0x5B9)
    		//
    		// Annotated disassembly: tools/unity-rebuild/out/logs/AddressablesManager_ReleaseHandles_0xCBF520.txt
    		// Numbered recovered sequence (VA 0x180CBFxxx == RVA 0xCBFxxx):
    		//
    		//   1. 0x180CBF608  `cmp qword [r14+0x38],0 / je 0x180CBF6BB` -- the `_loadAssetCts` field
    		//      (dump.cs:727599, +0x38). The non-null arm at 0x180CBF613 loads it into rbx and inlines
    		//      `CancellationTokenSource.Cancel()`: the class-init guard for CancellationTokenSource
    		//      (0x180CBF620..0x180CBF631), the inlined `ThrowIfDisposed` (`cmp byte [rbx+0x28],0 /
    		//      jne <cold>` at 0x180CBF638, whose cold arm at 0x180CBFA6C is
    		//      CancellationTokenSource.ThrowObjectDisposedException -- so +0x28 is the `_disposed`
    		//      flag) and 0x180CBF64A `call 0x1810A2FB0` = CancellationTokenSource.NotifyCancellation,
    		//      which is Cancel()'s own last statement. Then
    		//      0x180CBF64F/0x180CBF65E `rcx = [r14+0x38]` / `call 0x1810A27C0` =
    		//      CancellationTokenSource.Dispose(), and 0x180CBF666 `mov [r14+0x38],r15` (r15 = 0) with
    		//      the GC write barrier -- `_loadAssetCts = null`. The null-checked `je` arm skips all of it.
    		//   2. 0x180CBF6C5 `r8 = [r14+0x18]` (`_loadedAssets`, dump.cs:727595) with the null-check
    		//      `je 0x180CBFA66` (THROW NullReferenceException), then 0x180CBF6D2 `ecx = 3` with
    		//      rdx = TYPE ICollection<KeyValuePair<string, object>>_TypeInfo and 0x180CBF6DE
    		//      `call 0x1800022E0` (the per-slot interface invoke thunk). ICollection<T>'s slot 3 is
    		//      `Clear` (the same thunk family the repo already pinned slot 2 = `Add` for), so this is
    		//      `_loadedAssets.Clear()`.
    		//   3. 0x180CBF6E3 `rsi = [r14+0x10]` (`_handles`, dump.cs:727594); the `<>c` static cache is
    		//      consulted for `<>9__26_0` (dump.cs `<>c` field list: `static Func<AsyncOperationHandle,
    		//      bool> <>9__26_0`), the delegate is allocated and stored when missing
    		//      (0x180CBF735..0x180CBF7BE), and 0x180CBF7D6 `call 0x18068C2C0` =
    		//      System.Linq.Enumerable.Where<AsyncOperationHandle>(_handles, that predicate). So the
    		//      source enumerated `_handles.Where(<bool predicate>)`.
    		//   4. 0x180CBF7E4..0x180CBF831 the foreach header: IEnumerable<AsyncOperationHandle>.GetEnumerator
    		//      (slot 0) then System.Collections.IEnumerator.MoveNext (slot 0), both through the interface
    		//      thunk; 0x180CBF899..0x180CBF8AA IEnumerator<AsyncOperationHandle>.GetCurrent (slot 0,
    		//      resolved through 0x180243A20) producing the 24-byte handle, copied via
    		//      [rsp+0x38]/[rsp+0xB8] into the local at [rsp+0x50].
    		//   5. 0x180CBF8C0..0x180CBF8E8 the class-init guard for TYPE
    		//      UnityEngine.AddressableAssets.Addressables, then the handle local is re-materialised.
    		//      This guard is the only reason `Addressables` is touched at all in these 1465 bytes.
    		//   6. 0x180CBF930..0x180CBF978 the LOOP BODY, and it is the inlined body of
    		//      `AsyncOperationHandle.Release()`:
    		//        `r8 = [rsp+0x50]` = the handle's `m_InternalOp`;
    		//        0x180CBF938 `test r8,r8 / je 0x180CBFA86` -- and 0x180CBFA86 builds
    		//        `new System.Exception(STR 'Attempting to use an invalid operation handle')`
    		//        (0x180CBFA9D) and throws it, i.e. the inlined `AsyncOperationHandle.get_InternalOp()`
    		//        getter, whose installed source is
    		//        `if (m_InternalOp == null || m_InternalOp.Version != m_Version) throw new
    		//        Exception("Attempting to use an invalid operation handle"); return m_InternalOp;`
    		//        (Library/PackageCache/com.unity.addressables@16eddacf275e/Runtime/ResourceManager/
    		//        AsyncOperations/AsyncOperationHandle.cs:187-196);
    		//        0x180CBF93E `ecx = 2` with rdx = TYPE IAsyncOperation_TypeInfo then thunk = slot 2 =
    		//        `get_Version` (dump.cs IAsyncOperation slot table: 2 = get_Version) compared against
    		//        `[rsp+0x58]` = the handle's `m_Version` -- so a version mismatch takes the same throw;
    		//        0x180CBF967 `ecx = 4` then thunk = slot 4 = `DecrementReferenceCount`
    		//        (dump.cs IAsyncOperation slot table: 4 = `void DecrementReferenceCount()`);
    		//        0x180CBF978 `mov [rsp+0x50],r15` (r15 = 0) = `m_InternalOp = null`, Release()'s last
    		//        statement. That is exactly the installed two-statement body at
    		//        AsyncOperationHandle.cs:264-268 (`InternalOp.DecrementReferenceCount(); m_InternalOp = null;`).
    		//        The version-check-and-throw form is Release(), NOT `IsValid()`: `IsValid()` at
    		//        AsyncOperationHandle.cs:227-230 is `m_InternalOp != null && m_InternalOp.Version ==
    		//        m_Version` and returns false instead of throwing -- which is what the separate
    		//        `<>9__26_0` predicate is for.
    		//   7. 0x180CBF9EC..0x180CBFA37 the foreach epilogue: the enumerator's `IDisposable.Dispose`
    		//      (both the direct and the finally arm, 0x180CBF9FE / 0x180CBFA1C, slot 0 of IDisposable).
    		//   8. 0x180CBFA37 `r8 = [r14+0x10]` (`_handles`) with the null-check `je 0x180CBFA66` (THROW),
    		//      then 0x180CBFA40 `ecx = 3` with rdx = TYPE ICollection<AsyncOperationHandle>_TypeInfo and
    		//      0x180CBFA4C thunk = slot 3 = `Clear` -- `_handles.Clear()`. Then the epilogue and ret.
    		//
    		// TODO(evidence): the loop body's source spelling. `handle.Release();` (used below) and
    		//   `Addressables.Release(handle);` are indistinguishable: the latter's installed body is
    		//   `m_Addressables.Release(handle)` -> AddressablesImpl.Release -> `handle.Release()`, all three
    		//   inlined, and the step-5 guard for TYPE Addressables is the only class-init the chain would
    		//   leave behind. The direct spelling is used because it is the body the bytes actually show.
    		if (_loadAssetCts != null)
    		{
    			_loadAssetCts.Cancel();
    			_loadAssetCts.Dispose();
    			_loadAssetCts = null;
    		}

    		_loadedAssets.Clear();

    		foreach (var handle in _handles.Where(handle => handle.IsValid()))
    			handle.Release();

    		_handles.Clear();
    	}

    	[Token(Token = "0x600001E")]
    	[Address(RVA = "0xCBFAE0", Offset = "0xCBE4E0", VA = "0x180CBFAE0")]
    	private static IReadOnlyList<string> BuildAdditionalCatalogLocationsWithoutExt()
    	{
    		return null;
    	}

    	[Token(Token = "0x600001F")]
    	[Address(RVA = "0xCBFDF0", Offset = "0xCBE7F0", VA = "0x180CBFDF0")]
    	public AddressablesManager()
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCBFDF0, 489 B (0x1E9)
    		//
    		// This constructor was previously retained as an empty stub, which is what the measured G4b boot
    		// failure actually was: the three `readonly` collections were never allocated, so the first
    		// `_handles.Add(handle)` inside LoadResourceLocationsAsyncCore dereferenced null and the engine
    		// init chain died with
    		//   NullReferenceException
    		//     at AddressablesManager.LoadResourceLocationsAsyncCore (ct)
    		//     at AddressableResourceProviderExtended.LoadAllLocationsCore ()
    		//     at Naninovel.AddressableResourceProvider.LocateResources[T] (path)
    		//     at Naninovel.ResourceProvider.ResourceExists[T] (path)
    		//     at Naninovel.ResourceLoader<TResource>.Load (path, holder)
    		//     at Naninovel.CustomVariableManager.InitializeService ()
    		//     at Naninovel.Engine.Initialize (params)
    		// (tools/unity-rebuild/out/logs/game_start.log).  Recovered sequence -- one `il2cpp_codegen_object_new`
    		// plus one inlined `.ctor` per field, in declaration order, with the standard GC write barrier
    		// after each store:
    		//   0x180CBFE4F  METHODINFO Method$List<AsyncOperationHandle>..ctor()      -> operand stack
    		//   0x180CBFE67  TYPE List<AsyncOperationHandle>_TypeInfo                  -> operand stack
    		//   0x180CBFE78  rax = [List<AsyncOperationHandle>_TypeInfo]
    		//   0x180CBFE7F  call 0x1802980E0                       -> il2cpp_codegen_object_new
    		//   0x180CBFE84  rdx = METHODINFO Method$List<AsyncOperationHandle>..ctor()
    		//   0x180CBFE91  call 0x180857BA0                       -> List<T>() (shared generic body)
    		//   0x180CBFEA4  mov [rdi+0x10],rbx                    -> this._handles          (+0x10, readonly)
    		//   0x180CBFEE9..0x180CBFF02  object_new + .ctor for TYPE Dictionary<string, object>_TypeInfo
    		//   0x180CBFF0E  mov [rdi+0x18],rbx                    -> this._loadedAssets     (+0x18, readonly)
    		//   0x180CBFF59..0x180CBFF72  object_new + .ctor for TYPE Dictionary<string, UniTask>_TypeInfo
    		//   0x180CBFF7E  mov [rdi+0x20],rbx                    -> this._pendingLoadAssetTasks (+0x20, readonly)
    		//   0x180CBFFC9  epilogue / ret                         -> nothing else, no field is left null
    		// The three field offsets are dump.cs:727594-727596 and the instantiated types are pinned by the
    		// TypeInfo/MethodInfo tokens loaded above; the `mov [rip+...],1` writes at 0x180CBFE71/0x180CBFE9D/
    		// 0x180CBFF77/0x180CBFFC7 are only the one-time metadata-init guards for those tokens.
    		//
    		// Log: tools/unity-rebuild/out/logs/AddressablesManager_ctor_0xCBFDF0.txt
    		_handles = new List<AsyncOperationHandle>();
    		_loadedAssets = new Dictionary<string, object>();
    		_pendingLoadAssetTasks = new Dictionary<string, UniTask>();
    	}
    }
}
