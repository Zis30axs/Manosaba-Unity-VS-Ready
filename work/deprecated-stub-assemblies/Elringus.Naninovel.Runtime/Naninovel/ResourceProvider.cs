using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000284")]
    public abstract class ResourceProvider
    {
    	[Token(Token = "0x2000288")]
    	[CompilerGenerated]
    	private sealed class _003C_003Ec__DisplayClass38_0<T> where T : UnityEngine.Object
    	{
    		[Token(Token = "0x40009E2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string path;

    		[Token(Token = "0x6000B6C")]
    		public _003C_003Ec__DisplayClass38_0()
    		{
    		}

    		[Token(Token = "0x6000B6D")]
    		internal bool _003CResourceExists_003Eb__0(string p)
    		{
    			return default(bool);
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200028B")]
    	[CompilerGenerated]
    	private struct _003CLoadResource_003Ed__31<T> where T : UnityEngine.Object
    {
    		[Token(Token = "0x40009E8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40009E9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<Resource<T>> _003C_003Et__builder;

    		[Token(Token = "0x40009EA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public ResourceProvider _003C_003E4__this;

    		[Token(Token = "0x40009EB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string path;

    		[Token(Token = "0x40009EC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<Resource<T>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000B72")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000B73")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200028C")]
    	[CompilerGenerated]
    	private struct _003CLoadResources_003Ed__32<T> where T : UnityEngine.Object
    {
    		[Token(Token = "0x40009ED")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40009EE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<IReadOnlyCollection<Resource<T>>> _003C_003Et__builder;

    		[Token(Token = "0x40009EF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public ResourceProvider _003C_003E4__this;

    		[Token(Token = "0x40009F0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string path;

    		[Token(Token = "0x40009F1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<IReadOnlyCollection<string>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40009F2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<Resource<T>[]>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000B74")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000B75")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200028D")]
    	[CompilerGenerated]
    	private struct _003CLocateFolders_003Ed__40
    {
    		[Token(Token = "0x40009F3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40009F4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<IReadOnlyCollection<Folder>> _003C_003Et__builder;

    		[Token(Token = "0x40009F5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string path;

    		[Token(Token = "0x40009F6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public ResourceProvider _003C_003E4__this;

    		[Token(Token = "0x40009F7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<IReadOnlyCollection<Folder>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000B76")]
    		[Address(RVA = "0x5379E0", Offset = "0x5363E0", VA = "0x1805379E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000B77")]
    		[Address(RVA = "0x537FF0", Offset = "0x5369F0", VA = "0x180537FF0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200028E")]
    	[CompilerGenerated]
    	private struct _003CLocateResources_003Ed__39<T> where T : UnityEngine.Object
    {
    		[Token(Token = "0x40009F8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40009F9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<IReadOnlyCollection<string>> _003C_003Et__builder;

    		[Token(Token = "0x40009FA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public ResourceProvider _003C_003E4__this;

    		[Token(Token = "0x40009FB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string path;

    		[Token(Token = "0x40009FC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<IReadOnlyCollection<string>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000B78")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000B79")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200028F")]
    	[CompilerGenerated]
    	private struct _003CResourceExists_003Ed__38<T> where T : UnityEngine.Object
    {
    		[Token(Token = "0x40009FD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40009FE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

    		[Token(Token = "0x40009FF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string path;

    		[Token(Token = "0x4000A00")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public ResourceProvider _003C_003E4__this;

    		[Token(Token = "0x4000A01")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private _003C_003Ec__DisplayClass38_0<T> _003C_003E8__1;

    		[Token(Token = "0x4000A02")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<IReadOnlyCollection<string>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000B7A")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000B7B")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x17000190")]
    	public virtual bool IsLoading
    	{
    		[Token(Token = "0x6000B3C")]
    		[Address(RVA = "0x536880", Offset = "0x535280", VA = "0x180536880", Slot = "22")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000191")]
    	public virtual float LoadProgress
    	{
    		[Token(Token = "0x6000B3D")]
    		[Address(RVA = "0x4B8670", Offset = "0x4B7070", VA = "0x1804B8670", Slot = "23")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6000B3E")]
    		[Address(RVA = "0x4B8680", Offset = "0x4B7080", VA = "0x1804B8680")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000192")]
    	private IReadOnlyCollection<Resource> Naninovel_002EIResourceProvider_002ELoadedResources
    	{
    		[Token(Token = "0x6000B3F")]
    		[Address(RVA = "0x5368B0", Offset = "0x5352B0", VA = "0x1805368B0", Slot = "10")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000193")]
    	protected Dictionary<string, Resource> LoadedResources
    	{
    		[Token(Token = "0x6000B40")]
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000194")]
    	protected Dictionary<string, List<Folder>> LocatedFolders
    	{
    		[Token(Token = "0x6000B41")]
    		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000195")]
    	protected Dictionary<string, ResourceRunner> LoadRunners
    	{
    		[Token(Token = "0x6000B42")]
    		[Address(RVA = "0x347410", Offset = "0x345E10", VA = "0x180347410")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000196")]
    	protected Dictionary<Tuple<string, Type>, ResourceRunner> LocateRunners
    	{
    		[Token(Token = "0x6000B43")]
    		[Address(RVA = "0x44CE10", Offset = "0x44B810", VA = "0x18044CE10")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000197")]
    	protected List<CachedResourceLocation> LocationsCache
    	{
    		[Token(Token = "0x6000B44")]
    		[Address(RVA = "0x453470", Offset = "0x451E70", VA = "0x180453470")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1400001C")]
    	public event Action<float> OnLoadProgress
    	{
    		[Token(Token = "0x6000B38")]
    		[Address(RVA = "0x536440", Offset = "0x534E40", VA = "0x180536440", Slot = "4")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000B39")]
    		[Address(RVA = "0x536550", Offset = "0x534F50", VA = "0x180536550", Slot = "5")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x1400001D")]
    	public event Action<string> OnMessage
    	{
    		[Token(Token = "0x6000B3A")]
    		[Address(RVA = "0x536660", Offset = "0x535060", VA = "0x180536660", Slot = "6")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000B3B")]
    		[Address(RVA = "0x536770", Offset = "0x535170", VA = "0x180536770", Slot = "7")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x6000B45")]
    	public abstract bool SupportsType<T>() where T : UnityEngine.Object;

    	[Token(Token = "0x6000B46")]
    	public virtual Resource<T> GetLoadedResourceOrNull<T>(string path) where T : UnityEngine.Object
    	{
    		return null;
    	}

    	[Token(Token = "0x6000B47")]
    	[AsyncStateMachine(typeof(_003CLoadResource_003Ed__31<>))]
    	public virtual UniTask<Resource<T>> LoadResource<T>(string path) where T : UnityEngine.Object
    	{
    		return default(UniTask<Resource<T>>);
    	}

    	[Token(Token = "0x6000B48")]
    	[AsyncStateMachine(typeof(_003CLoadResources_003Ed__32<>))]
    	public virtual UniTask<IReadOnlyCollection<Resource<T>>> LoadResources<T>(string path) where T : UnityEngine.Object
    	{
    		return default(UniTask<IReadOnlyCollection<Resource<T>>>);
    	}

    	[Token(Token = "0x6000B49")]
    	[Address(RVA = "0x536900", Offset = "0x535300", VA = "0x180536900", Slot = "28")]
    	public virtual void UnloadResourceBlocking(string path)
    	{
    	}

    	[Token(Token = "0x6000B4A")]
    	[Address(RVA = "0x536B70", Offset = "0x535570", VA = "0x180536B70", Slot = "29")]
    	public virtual void UnloadResourcesBlocking()
    	{
    	}

    	[Token(Token = "0x6000B4B")]
    	[Address(RVA = "0x536E90", Offset = "0x535890", VA = "0x180536E90", Slot = "30")]
    	public virtual bool ResourceLoaded(string path)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000B4C")]
    	[Address(RVA = "0x536EF0", Offset = "0x5358F0", VA = "0x180536EF0", Slot = "31")]
    	public virtual bool ResourceLoading(string path)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000B4D")]
    	public virtual bool ResourceLocating<T>(string path)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000B4E")]
    	[AsyncStateMachine(typeof(_003CResourceExists_003Ed__38<>))]
    	public virtual UniTask<bool> ResourceExists<T>(string path) where T : UnityEngine.Object
    	{
    		return default(UniTask<bool>);
    	}

    	[Token(Token = "0x6000B4F")]
    	[AsyncStateMachine(typeof(_003CLocateResources_003Ed__39<>))]
    	public virtual UniTask<IReadOnlyCollection<string>> LocateResources<T>(string path) where T : UnityEngine.Object
    	{
    		return default(UniTask<IReadOnlyCollection<string>>);
    	}

    	[Token(Token = "0x6000B50")]
    	[Address(RVA = "0x52F900", Offset = "0x52E300", VA = "0x18052F900", Slot = "35")]
    	[AsyncStateMachine(typeof(_003CLocateFolders_003Ed__40))]
    	public virtual UniTask<IReadOnlyCollection<Folder>> LocateFolders(string path)
    	{
    		return default(UniTask<IReadOnlyCollection<Folder>>);
    	}

    	[Token(Token = "0x6000B51")]
    	[Address(RVA = "0x536F50", Offset = "0x535950", VA = "0x180536F50", Slot = "36")]
    	public virtual void LogMessage(string message)
    	{
    	}

    	[Token(Token = "0x6000B52")]
    	protected abstract LoadResourceRunner<T> CreateLoadResourceRunner<T>(string path) where T : UnityEngine.Object;

    	[Token(Token = "0x6000B53")]
    	protected abstract LocateResourcesRunner<T> CreateLocateResourcesRunner<T>(string path) where T : UnityEngine.Object;

    	[Token(Token = "0x6000B54")]
    	protected abstract LocateFoldersRunner CreateLocateFoldersRunner(string path);

    	[Token(Token = "0x6000B55")]
    	protected abstract void DisposeResource(Resource resource);

    	[Token(Token = "0x6000B56")]
    	protected virtual void RunResourceLoader<T>(LoadResourceRunner<T> loader) where T : UnityEngine.Object
    	{
    	}

    	[Token(Token = "0x6000B57")]
    	protected virtual void RunResourcesLocator<T>(LocateResourcesRunner<T> locator) where T : UnityEngine.Object
    	{
    	}

    	[Token(Token = "0x6000B58")]
    	[Address(RVA = "0x536F70", Offset = "0x535970", VA = "0x180536F70", Slot = "43")]
    	protected virtual void RunFoldersLocator(LocateFoldersRunner locator)
    	{
    	}

    	[Token(Token = "0x6000B59")]
    	protected virtual bool ResourceLoaded<T>(string path) where T : UnityEngine.Object
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000B5A")]
    	[Address(RVA = "0x536FB0", Offset = "0x5359B0", VA = "0x180536FB0", Slot = "45")]
    	protected virtual void CancelResourceLoading(string path)
    	{
    	}

    	[Token(Token = "0x6000B5B")]
    	protected virtual void CancelResourceLocating<T>(string path)
    	{
    	}

    	[Token(Token = "0x6000B5C")]
    	protected virtual void HandleResourceLoaded<T>(Resource<T> resource) where T : UnityEngine.Object
    	{
    	}

    	[Token(Token = "0x6000B5D")]
    	protected virtual void HandleResourcesLocated<T>(IReadOnlyCollection<string> locatedResourcePaths, string path) where T : UnityEngine.Object
    	{
    	}

    	[Token(Token = "0x6000B5E")]
    	[Address(RVA = "0x537090", Offset = "0x535A90", VA = "0x180537090", Slot = "49")]
    	protected virtual void HandleFoldersLocated(IReadOnlyCollection<Folder> locatedFolders, string path)
    	{
    	}

    	[Token(Token = "0x6000B5F")]
    	[Address(RVA = "0x537280", Offset = "0x535C80", VA = "0x180537280", Slot = "50")]
    	protected virtual void UpdateLoadProgress()
    	{
    	}

    	[Token(Token = "0x6000B60")]
    	[Address(RVA = "0x5373A0", Offset = "0x535DA0", VA = "0x1805373A0", Slot = "51")]
    	protected virtual bool AreTypesCompatible(Type sourceType, Type targetType)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000B61")]
    	protected virtual bool IsLocationCached<T>(string path)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000B62")]
    	protected virtual IReadOnlyCollection<string> LocateCached<T>(string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000B63")]
    	[Address(RVA = "0x5373E0", Offset = "0x535DE0", VA = "0x1805373E0")]
    	protected ResourceProvider()
    	{
    	}
    }
}
