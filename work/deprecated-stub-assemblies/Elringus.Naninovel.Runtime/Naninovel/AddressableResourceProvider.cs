using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Naninovel
{
    [Token(Token = "0x200024D")]
    public class AddressableResourceProvider : ResourceProvider
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200024E")]
    	[CompilerGenerated]
    	private struct _003CLoadAllLocations_003Ed__16
    {
    		[Token(Token = "0x4000906")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000907")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<List<IResourceLocation>> _003C_003Et__builder;

    		[Token(Token = "0x4000908")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AddressableResourceProvider _003C_003E4__this;

    		[Token(Token = "0x4000909")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private AsyncOperationHandle<IList<IResourceLocation>> _003Ctask_003E5__2;

    		[Token(Token = "0x400090A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000A40")]
    		[Address(RVA = "0x52FAF0", Offset = "0x52E4F0", VA = "0x18052FAF0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000A41")]
    		[Address(RVA = "0x530060", Offset = "0x52EA60", VA = "0x180530060", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200024F")]
    	[CompilerGenerated]
    	private struct _003CLoadResource_003Ed__9<T> where T : Object
    {
    		[Token(Token = "0x400090B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400090C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<Resource<T>> _003C_003Et__builder;

    		[Token(Token = "0x400090D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AddressableResourceProvider _003C_003E4__this;

    		[Token(Token = "0x400090E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string path;

    		[Token(Token = "0x400090F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<List<IResourceLocation>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000910")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<Resource<T>>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000A42")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000A43")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000250")]
    	[CompilerGenerated]
    	private struct _003CLocateFolders_003Ed__11
    {
    		[Token(Token = "0x4000911")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000912")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<IReadOnlyCollection<Folder>> _003C_003Et__builder;

    		[Token(Token = "0x4000913")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AddressableResourceProvider _003C_003E4__this;

    		[Token(Token = "0x4000914")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string path;

    		[Token(Token = "0x4000915")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<List<IResourceLocation>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000916")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask<IReadOnlyCollection<Folder>>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000A44")]
    		[Address(RVA = "0x530090", Offset = "0x52EA90", VA = "0x180530090", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000A45")]
    		[Address(RVA = "0x5306E0", Offset = "0x52F0E0", VA = "0x1805306E0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000251")]
    	[CompilerGenerated]
    	private struct _003CLocateResources_003Ed__10<T> where T : Object
    {
    		[Token(Token = "0x4000917")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000918")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<IReadOnlyCollection<string>> _003C_003Et__builder;

    		[Token(Token = "0x4000919")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AddressableResourceProvider _003C_003E4__this;

    		[Token(Token = "0x400091A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string path;

    		[Token(Token = "0x400091B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<List<IResourceLocation>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400091C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<IReadOnlyCollection<string>>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000A46")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000A47")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000905")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	private List<IResourceLocation> locations;

    	[Token(Token = "0x1700017C")]
    	public virtual string MainLabel
    	{
    		[Token(Token = "0x6000A30")]
    		[Address(RVA = "0x453480", Offset = "0x451E80", VA = "0x180453480", Slot = "54")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700017D")]
    	public virtual IReadOnlyCollection<string> ExtraLabels
    	{
    		[Token(Token = "0x6000A31")]
    		[Address(RVA = "0x453950", Offset = "0x452350", VA = "0x180453950", Slot = "55")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000A32")]
    	[Address(RVA = "0x52EC10", Offset = "0x52D610", VA = "0x18052EC10")]
    	public AddressableResourceProvider(string mainLabel = "UNITY_COMMON", string[] extraLabels = default)
    	{
    	}

    	[Token(Token = "0x6000A33")]
    	public override bool SupportsType<T>()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000A34")]
    	[AsyncStateMachine(typeof(_003CLoadResource_003Ed__9<>))]
    	public override UniTask<Resource<T>> LoadResource<T>(string path)
    	{
    		return default(UniTask<Resource<T>>);
    	}

    	[Token(Token = "0x6000A35")]
    	[AsyncStateMachine(typeof(_003CLocateResources_003Ed__10<>))]
    	public override UniTask<IReadOnlyCollection<string>> LocateResources<T>(string path)
    	{
    		return default(UniTask<IReadOnlyCollection<string>>);
    	}

    	[Token(Token = "0x6000A36")]
    	[Address(RVA = "0x52EDC0", Offset = "0x52D7C0", VA = "0x18052EDC0", Slot = "35")]
    	[AsyncStateMachine(typeof(_003CLocateFolders_003Ed__11))]
    	public override UniTask<IReadOnlyCollection<Folder>> LocateFolders(string path)
    	{
    		return default(UniTask<IReadOnlyCollection<Folder>>);
    	}

    	[Token(Token = "0x6000A37")]
    	protected override LoadResourceRunner<T> CreateLoadResourceRunner<T>(string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000A38")]
    	protected override LocateResourcesRunner<T> CreateLocateResourcesRunner<T>(string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000A39")]
    	[Address(RVA = "0x52EFB0", Offset = "0x52D9B0", VA = "0x18052EFB0", Slot = "39")]
    	protected override LocateFoldersRunner CreateLocateFoldersRunner(string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000A3A")]
    	[Address(RVA = "0x52F080", Offset = "0x52DA80", VA = "0x18052F080", Slot = "40")]
    	protected override void DisposeResource(Resource resource)
    	{
    	}

    	[Token(Token = "0x6000A3B")]
    	[Address(RVA = "0x52F110", Offset = "0x52DB10", VA = "0x18052F110", Slot = "56")]
    	[AsyncStateMachine(typeof(_003CLoadAllLocations_003Ed__16))]
    	protected virtual UniTask<List<IResourceLocation>> LoadAllLocations()
    	{
    		return default(UniTask<List<IResourceLocation>>);
    	}

    	[Token(Token = "0x6000A3C")]
    	[Address(RVA = "0x52F2B0", Offset = "0x52DCB0", VA = "0x18052F2B0", Slot = "57")]
    	protected virtual void CacheLocations(IEnumerable<IResourceLocation> locations)
    	{
    	}
    }
}
