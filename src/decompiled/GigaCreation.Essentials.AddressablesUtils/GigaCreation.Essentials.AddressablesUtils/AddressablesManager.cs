using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace GigaCreation.Essentials.AddressablesUtils;

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

	[Token(Token = "0x17000008")]
	public bool IsAdditionalCatalogLoaded
	{
		[Token(Token = "0x600000F")]
		[Address(RVA = "0x456360", Offset = "0x454D60", VA = "0x180456360", Slot = "4")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000010")]
		[Address(RVA = "0x5DA0D0", Offset = "0x5D8AD0", VA = "0x1805DA0D0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000009")]
	protected List<IResourceLocation> ResourceLocations
	{
		[Token(Token = "0x6000011")]
		[Address(RVA = "0x453470", Offset = "0x451E70", VA = "0x180453470")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000012")]
		[Address(RVA = "0x30EFA0", Offset = "0x30D9A0", VA = "0x18030EFA0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x1700000A")]
	private IWebRequestManager WebRequestManager
	{
		[Token(Token = "0x6000013")]
		[Address(RVA = "0xCBE940", Offset = "0xCBD340", VA = "0x180CBE940")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700000B")]
	private CancellationTokenSource LoadAssetCts
	{
		[Token(Token = "0x6000014")]
		[Address(RVA = "0xCBEA00", Offset = "0xCBD400", VA = "0x180CBEA00")]
		get
		{
			return null;
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
		return default(UniTask<List<IResourceLocation>>);
	}

	[Token(Token = "0x6000019")]
	[AsyncStateMachine(typeof(_003CGetOrLoadAddressableAsset_003Ed__22<>))]
	public UniTask<T> GetOrLoadAddressableAsset<T>(string address, [Optional] CancellationToken ct)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x600001A")]
	[AsyncStateMachine(typeof(_003CGetOrLoadAddressableAsset_003Ed__23<>))]
	public UniTask<T> GetOrLoadAddressableAsset<T>(AssetReference reference, [Optional] CancellationToken ct)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x600001B")]
	[Address(RVA = "0xCBF2B0", Offset = "0xCBDCB0", VA = "0x180CBF2B0", Slot = "11")]
	public void Dispose()
	{
	}

	[Token(Token = "0x600001C")]
	[Address(RVA = "0xCBF320", Offset = "0xCBDD20", VA = "0x180CBF320")]
	[AsyncStateMachine(typeof(_003CLoadResourceLocationsAsync_003Ed__25))]
	protected UniTask<List<IResourceLocation>> LoadResourceLocationsAsync([Optional] CancellationToken ct)
	{
		return default(UniTask<List<IResourceLocation>>);
	}

	[Token(Token = "0x600001D")]
	[Address(RVA = "0xCBF520", Offset = "0xCBDF20", VA = "0x180CBF520")]
	protected void ReleaseHandles()
	{
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
	}
}
