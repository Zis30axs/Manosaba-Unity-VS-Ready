using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000277")]
public class ResourceLoader<TResource> where TResource : UnityEngine.Object
{
	[Token(Token = "0x2000278")]
	protected class LoadedResource
	{
		[Token(Token = "0x4000991")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public readonly Resource<TResource> Resource;

		[Token(Token = "0x4000992")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public readonly ProvisionSource ProvisionSource;

		[Token(Token = "0x4000993")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public readonly string LocalPath;

		[Token(Token = "0x4000994")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly HashSet<object> holders;

		[Token(Token = "0x1700018C")]
		public UnityEngine.Object Object
		{
			[Token(Token = "0x6000B10")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x1700018D")]
		public string FullPath
		{
			[Token(Token = "0x6000B11")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x1700018E")]
		public bool Valid
		{
			[Token(Token = "0x6000B12")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x1700018F")]
		public IReadOnlyCollection<object> Holders
		{
			[Token(Token = "0x6000B13")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x6000B14")]
		public LoadedResource(Resource<TResource> resource, ProvisionSource provisionSource)
		{
		}

		[Token(Token = "0x6000B15")]
		public void AddHolder(object holder)
		{
		}

		[Token(Token = "0x6000B16")]
		public void RemoveHolder(object holder)
		{
		}

		[Token(Token = "0x6000B17")]
		public bool IsHeldBy(object holder)
		{
			return default(bool);
		}

		[Token(Token = "0x6000B18")]
		public void AddHoldersFrom(LoadedResource resource)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200027B")]
	[CompilerGenerated]
	private struct _003CExists_003Ed__29
{
		[Token(Token = "0x4000999")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400099A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

		[Token(Token = "0x400099B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ResourceLoader<TResource> _003C_003E4__this;

		[Token(Token = "0x400099C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string path;

		[Token(Token = "0x400099D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private List<ProvisionSource>.Enumerator _003C_003E7__wrap1;

		[Token(Token = "0x400099E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<bool>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000B1F")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000B20")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200027C")]
	[CompilerGenerated]
	private struct _003CLoad_003Ed__25
{
		[Token(Token = "0x400099F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40009A0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<Resource<TResource>> _003C_003Et__builder;

		[Token(Token = "0x40009A1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ResourceLoader<TResource> _003C_003E4__this;

		[Token(Token = "0x40009A2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string path;

		[Token(Token = "0x40009A3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public object holder;

		[Token(Token = "0x40009A4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private List<ProvisionSource>.Enumerator _003C_003E7__wrap1;

		[Token(Token = "0x40009A5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ProvisionSource _003Csource_003E5__3;

		[Token(Token = "0x40009A6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private string _003CfullPath_003E5__4;

		[Token(Token = "0x40009A7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<bool>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40009A8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<Resource<TResource>>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000B21")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000B22")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200027D")]
	[CompilerGenerated]
	private struct _003CLoadAll_003Ed__26
{
		[Token(Token = "0x40009A9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40009AA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<IReadOnlyCollection<Resource<TResource>>> _003C_003Et__builder;

		[Token(Token = "0x40009AB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ResourceLoader<TResource> _003C_003E4__this;

		[Token(Token = "0x40009AC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string path;

		[Token(Token = "0x40009AD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public object holder;

		[Token(Token = "0x40009AE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private List<Resource<TResource>> _003Cresult_003E5__2;

		[Token(Token = "0x40009AF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private HashSet<string> _003CaddedPaths_003E5__3;

		[Token(Token = "0x40009B0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private PooledObject<List<UniTask<Resource<TResource>>>> _003C__003E5__4;

		[Token(Token = "0x40009B1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private List<UniTask<Resource<TResource>>> _003CloadTasks_003E5__5;

		[Token(Token = "0x40009B2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Dictionary<string, (ProvisionSource, string)> _003CloadData_003E5__6;

		[Token(Token = "0x40009B3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private List<ProvisionSource>.Enumerator _003C_003E7__wrap6;

		[Token(Token = "0x40009B4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ProvisionSource _003Csource_003E5__8;

		[Token(Token = "0x40009B5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<IReadOnlyCollection<string>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40009B6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<Resource<TResource>[]>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000B23")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000B24")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200027E")]
	[CompilerGenerated]
	private struct _003CLocate_003Ed__28
{
		[Token(Token = "0x40009B7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40009B8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<IReadOnlyCollection<string>> _003C_003Et__builder;

		[Token(Token = "0x40009B9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ResourceLoader<TResource> _003C_003E4__this;

		[Token(Token = "0x40009BA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string path;

		[Token(Token = "0x40009BB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private PooledObject<List<UniTask<IEnumerable<string>>>> _003C__003E5__2;

		[Token(Token = "0x40009BC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<IEnumerable<string>[]>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000B25")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000B26")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200027F")]
	[CompilerGenerated]
	private struct _003CNaninovel_002DIResourceLoader_002DLoad_003Ed__36
{
		[Token(Token = "0x40009BD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40009BE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<Resource> _003C_003Et__builder;

		[Token(Token = "0x40009BF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ResourceLoader<TResource> _003C_003E4__this;

		[Token(Token = "0x40009C0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string path;

		[Token(Token = "0x40009C1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public object holder;

		[Token(Token = "0x40009C2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<Resource<TResource>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000B27")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000B28")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000280")]
	[CompilerGenerated]
	private struct _003CNaninovel_002DIResourceLoader_002DLoadAll_003Ed__37
{
		[Token(Token = "0x40009C3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40009C4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<IReadOnlyCollection<Resource>> _003C_003Et__builder;

		[Token(Token = "0x40009C5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ResourceLoader<TResource> _003C_003E4__this;

		[Token(Token = "0x40009C6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string path;

		[Token(Token = "0x40009C7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public object holder;

		[Token(Token = "0x40009C8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<IReadOnlyCollection<Resource<TResource>>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000B29")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000B2A")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400098B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	protected readonly List<ProvisionSource> ProvisionSources;

	[Token(Token = "0x400098C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	protected readonly Dictionary<string, LoadedResource> LoadedByFullPath;

	[Token(Token = "0x400098D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	protected readonly Dictionary<string, LoadedResource> LoadedByLocalPath;

	[Token(Token = "0x400098E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	protected readonly Dictionary<UnityEngine.Object, LoadedResource> LoadedByObject;

	[Token(Token = "0x400098F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	protected readonly IHoldersTracker HoldersTracker;

	[Token(Token = "0x4000990")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	protected readonly string PathPrefix;

	[Token(Token = "0x1400001A")]
	public event Action<Resource<TResource>> OnLoaded
	{
		[Token(Token = "0x6000AF2")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000AF3")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400001B")]
	public event Action<Resource<TResource>> OnUnloaded
	{
		[Token(Token = "0x6000AF4")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000AF5")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6000AF6")]
	public ResourceLoader(IList<IResourceProvider> providers = default, IHoldersTracker holdersTracker = default, string pathPrefix = "")
	{
	}

	[Token(Token = "0x6000AF7")]
	public string GetFullPath(string localPath)
	{
		return null;
	}

	[Token(Token = "0x6000AF8")]
	public virtual string GetLocalPath(string fullPath)
	{
		return null;
	}

	[Token(Token = "0x6000AF9")]
	public string GetLocalPath(UnityEngine.Object obj)
	{
		return null;
	}

	[Token(Token = "0x6000AFA")]
	public virtual string GetLocalPath(TResource obj)
	{
		return null;
	}

	[Token(Token = "0x6000AFB")]
	public virtual void Hold(string path, object holder)
	{
	}

	[Token(Token = "0x6000AFC")]
	public virtual void Release(string path, object holder, bool unload = true)
	{
	}

	[Token(Token = "0x6000AFD")]
	public virtual void ReleaseAll(object holder, bool unload = true)
	{
	}

	[Token(Token = "0x6000AFE")]
	public virtual bool IsHeldBy(string path, object holder)
	{
		return default(bool);
	}

	[Token(Token = "0x6000AFF")]
	public virtual int CountHolders([Optional] string path)
	{
		return default(int);
	}

	[Token(Token = "0x6000B00")]
	public virtual bool IsLoaded(string path)
	{
		return default(bool);
	}

	[Token(Token = "0x6000B01")]
	public virtual Resource<TResource> GetLoaded(string path)
	{
		return null;
	}

	[Token(Token = "0x6000B02")]
	[AsyncStateMachine(typeof(ResourceLoader<>._003CLoad_003Ed__25))]
	public virtual UniTask<Resource<TResource>> Load(string path, [Optional] object holder)
	{
		return default(UniTask<Resource<TResource>>);
	}

	[Token(Token = "0x6000B03")]
	[AsyncStateMachine(typeof(ResourceLoader<>._003CLoadAll_003Ed__26))]
	public virtual UniTask<IReadOnlyCollection<Resource<TResource>>> LoadAll([Optional] string path, [Optional] object holder)
	{
		return default(UniTask<IReadOnlyCollection<Resource<TResource>>>);
	}

	[Token(Token = "0x6000B04")]
	public virtual IReadOnlyCollection<Resource<TResource>> GetAllLoaded()
	{
		return null;
	}

	[Token(Token = "0x6000B05")]
	[AsyncStateMachine(typeof(ResourceLoader<>._003CLocate_003Ed__28))]
	public virtual UniTask<IReadOnlyCollection<string>> Locate([Optional] string path)
	{
		return default(UniTask<IReadOnlyCollection<string>>);
	}

	[Token(Token = "0x6000B06")]
	[AsyncStateMachine(typeof(ResourceLoader<>._003CExists_003Ed__29))]
	public virtual UniTask<bool> Exists(string path)
	{
		return default(UniTask<bool>);
	}

	[Token(Token = "0x6000B07")]
	[CanBeNull]
	protected virtual LoadedResource GetLoadedResourceOrNull(string localPath)
	{
		return null;
	}

	[Token(Token = "0x6000B08")]
	protected virtual void Release(LoadedResource resource, object holder, bool unload = true)
	{
	}

	[Token(Token = "0x6000B09")]
	protected virtual void AddLoadedResource(LoadedResource resource)
	{
	}

	[Token(Token = "0x6000B0A")]
	protected virtual void RemoveLoadedResource(LoadedResource resource)
	{
	}

	[Token(Token = "0x6000B0B")]
	private Resource Naninovel_002EIResourceLoader_002EGetLoaded(string path)
	{
		return null;
	}

	[Token(Token = "0x6000B0C")]
	private IReadOnlyCollection<Resource> Naninovel_002EIResourceLoader_002EGetAllLoaded()
	{
		return null;
	}

	[Token(Token = "0x6000B0D")]
	[AsyncStateMachine(typeof(ResourceLoader<>._003CNaninovel_002DIResourceLoader_002DLoad_003Ed__36))]
	private UniTask<Resource> Naninovel_002EIResourceLoader_002ELoad(string path, object holder)
	{
		return default(UniTask<Resource>);
	}

	[Token(Token = "0x6000B0E")]
	[AsyncStateMachine(typeof(ResourceLoader<>._003CNaninovel_002DIResourceLoader_002DLoadAll_003Ed__37))]
	private UniTask<IReadOnlyCollection<Resource>> Naninovel_002EIResourceLoader_002ELoadAll(string path, object holder)
	{
		return default(UniTask<IReadOnlyCollection<Resource>>);
	}

	[Token(Token = "0x6000B0F")]
	[Conditional("NANINOVEL_RESOURCES_DEBUG")]
	private static void LogDebug(string message)
	{
	}
}
