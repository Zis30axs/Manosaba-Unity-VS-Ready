using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UniRx;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace GigaCreation.Essentials.AddressablesUtils
{
    [Token(Token = "0x2000012")]
    public class AddressablesManagerRemoteSupported : AddressablesManager, IAddressablesManagerRemoteSupported, IAddressablesManager, IContentCatalogManager, IAddressableResourceLocationProvider, IAddressableAssetLoader, IService, IDisposable, IAddressablesDownloader, IAddressablesCacheCleaner
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000014")]
    	[CompilerGenerated]
    	private struct _003CCheckForCatalogUpdatesAsync_003Ed__3
    {
    		[Token(Token = "0x4000054")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000055")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

    		[Token(Token = "0x4000056")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000057")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AddressablesManagerRemoteSupported _003C_003E4__this;

    		[Token(Token = "0x4000058")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public bool logResult;

    		[Token(Token = "0x4000059")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<List<string>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000043")]
    		[Address(RVA = "0xCC3A90", Offset = "0xCC2490", VA = "0x180CC3A90", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000044")]
    		[Address(RVA = "0xCC4240", Offset = "0xCC2C40", VA = "0x180CC4240", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000015")]
    	[CompilerGenerated]
    	private struct _003CCleanBundleCacheAsync_003Ed__5
    {
    		[Token(Token = "0x400005A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400005B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

    		[Token(Token = "0x400005C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x400005D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private AsyncOperationHandle _003Chandle_003E5__2;

    		[Token(Token = "0x400005E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000045")]
    		[Address(RVA = "0xCC4270", Offset = "0xCC2C70", VA = "0x180CC4270", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000046")]
    		[Address(RVA = "0xCC4D00", Offset = "0xCC3700", VA = "0x180CC4D00", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000016")]
    	[CompilerGenerated]
    	private struct _003CClearCacheAsync_003Ed__9
    {
    		[Token(Token = "0x400005F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000060")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

    		[Token(Token = "0x4000061")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AddressablesManagerRemoteSupported _003C_003E4__this;

    		[Token(Token = "0x4000062")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000063")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<bool>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000047")]
    		[Address(RVA = "0xCC4D30", Offset = "0xCC3730", VA = "0x180CC4D30", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000048")]
    		[Address(RVA = "0xCC52D0", Offset = "0xCC3CD0", VA = "0x180CC52D0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000017")]
    	[CompilerGenerated]
    	private struct _003CDownloadAsync_003Ed__7
    {
    		[Token(Token = "0x4000064")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000065")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

    		[Token(Token = "0x4000066")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AddressablesManagerRemoteSupported _003C_003E4__this;

    		[Token(Token = "0x4000067")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000068")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private long _003CtotalSize_003E5__2;

    		[Token(Token = "0x4000069")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<List<IResourceLocation>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400006A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private IEnumerator<IGrouping<int, IResourceLocation>> _003C_003E7__wrap2;

    		[Token(Token = "0x400006B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private AsyncOperationHandle _003Chandle_003E5__4;

    		[Token(Token = "0x400006C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000049")]
    		[Address(RVA = "0xCC5300", Offset = "0xCC3D00", VA = "0x180CC5300", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600004A")]
    		[Address(RVA = "0xCC66E0", Offset = "0xCC50E0", VA = "0x180CC66E0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000018")]
    	[CompilerGenerated]
    	private struct _003CGetDownloadSizeAsync_003Ed__6
    {
    		[Token(Token = "0x400006D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400006E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<(bool, long)> _003C_003Et__builder;

    		[Token(Token = "0x400006F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AddressablesManagerRemoteSupported _003C_003E4__this;

    		[Token(Token = "0x4000070")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000071")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private AsyncOperationHandle<long> _003Chandle_003E5__2;

    		[Token(Token = "0x4000072")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask<List<IResourceLocation>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000073")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    		private UniTask<long>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600004B")]
    		[Address(RVA = "0xCC6710", Offset = "0xCC5110", VA = "0x180CC6710", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600004C")]
    		[Address(RVA = "0xCC71D0", Offset = "0xCC5BD0", VA = "0x180CC71D0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000019")]
    	[CompilerGenerated]
    	private struct _003CUpdateCatalogsAsync_003Ed__4
    {
    		[Token(Token = "0x4000074")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000075")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

    		[Token(Token = "0x4000076")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AddressablesManagerRemoteSupported _003C_003E4__this;

    		[Token(Token = "0x4000077")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000078")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<List<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600004D")]
    		[Address(RVA = "0xCC7200", Offset = "0xCC5C00", VA = "0x180CC7200", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600004E")]
    		[Address(RVA = "0xCC78F0", Offset = "0xCC62F0", VA = "0x180CC78F0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400004E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private readonly ISubject<(float, float)> _downloadProgressSubject;

    	[Token(Token = "0x400004F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private IList<string> _catalogsToUpdate;

    	[Token(Token = "0x4000050")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	private long _downloadSize;

    	[Token(Token = "0x6000036")]
    	[Address(RVA = "0xCC2AA0", Offset = "0xCC14A0", VA = "0x180CC2AA0", Slot = "12")]
    	[AsyncStateMachine(typeof(_003CCheckForCatalogUpdatesAsync_003Ed__3))]
    	public UniTask<bool> CheckForCatalogUpdatesAsync(bool logResult = false, CancellationToken ct = default)
    	{
    		return default(UniTask<bool>);
    	}

    	[Token(Token = "0x6000037")]
    	[Address(RVA = "0xCC2CC0", Offset = "0xCC16C0", VA = "0x180CC2CC0", Slot = "13")]
    	[AsyncStateMachine(typeof(_003CUpdateCatalogsAsync_003Ed__4))]
    	public UniTask<bool> UpdateCatalogsAsync(CancellationToken ct = default)
    	{
    		return default(UniTask<bool>);
    	}

    	[Token(Token = "0x6000038")]
    	[Address(RVA = "0xCC2EB0", Offset = "0xCC18B0", VA = "0x180CC2EB0", Slot = "14")]
    	[AsyncStateMachine(typeof(_003CCleanBundleCacheAsync_003Ed__5))]
    	public UniTask<bool> CleanBundleCacheAsync(CancellationToken ct = default)
    	{
    		return default(UniTask<bool>);
    	}

    	[Token(Token = "0x6000039")]
    	[Address(RVA = "0xCC3050", Offset = "0xCC1A50", VA = "0x180CC3050", Slot = "15")]
    	[AsyncStateMachine(typeof(_003CGetDownloadSizeAsync_003Ed__6))]
    	public UniTask<(bool, long)> GetDownloadSizeAsync(CancellationToken ct = default)
    	{
    		return default(UniTask<(bool, long)>);
    	}

    	[Token(Token = "0x600003A")]
    	[Address(RVA = "0xCC3250", Offset = "0xCC1C50", VA = "0x180CC3250", Slot = "16")]
    	[AsyncStateMachine(typeof(_003CDownloadAsync_003Ed__7))]
    	public UniTask<bool> DownloadAsync(CancellationToken ct = default)
    	{
    		return default(UniTask<bool>);
    	}

    	[Token(Token = "0x600003B")]
    	[Address(RVA = "0xCC3450", Offset = "0xCC1E50", VA = "0x180CC3450", Slot = "17")]
    	public IObservable<(float, float)> OnDownloadProgressAsObservable()
    	{
    		return null;
    	}

    	[Token(Token = "0x600003C")]
    	[Address(RVA = "0xCC34C0", Offset = "0xCC1EC0", VA = "0x180CC34C0", Slot = "18")]
    	[AsyncStateMachine(typeof(_003CClearCacheAsync_003Ed__9))]
    	public UniTask<bool> ClearCacheAsync(CancellationToken ct = default)
    	{
    		return default(UniTask<bool>);
    	}

    	[Token(Token = "0x600003D")]
    	[Address(RVA = "0xCC36B0", Offset = "0xCC20B0", VA = "0x180CC36B0")]
    	private static void DeleteContentCatalogs()
    	{
    	}

    	[Token(Token = "0x600003E")]
    	[Address(RVA = "0xCC37E0", Offset = "0xCC21E0", VA = "0x180CC37E0")]
    	public AddressablesManagerRemoteSupported()
    	{
    	}
    }
}
