using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000295")]
public class VirtualResourceProvider : IResourceProvider
{
	[Token(Token = "0x4000A0A")]
	[FieldOffset(Offset = "0x28")]
	protected readonly Dictionary<string, Resource> Resources;

	[Token(Token = "0x4000A0B")]
	[FieldOffset(Offset = "0x30")]
	protected readonly HashSet<string> FolderPaths;

	[Token(Token = "0x17000199")]
	public bool RemoveResourcesOnUnload
	{
		[Token(Token = "0x6000B8B")]
		[Address(RVA = "0x44D320", Offset = "0x44BD20", VA = "0x18044D320")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000B8C")]
		[Address(RVA = "0x30C130", Offset = "0x30AB30", VA = "0x18030C130")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x1700019A")]
	public bool IsLoading
	{
		[Token(Token = "0x6000B8D")]
		[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360", Slot = "8")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x1700019B")]
	public float LoadProgress
	{
		[Token(Token = "0x6000B8E")]
		[Address(RVA = "0x366350", Offset = "0x364D50", VA = "0x180366350", Slot = "9")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x1700019C")]
	public IReadOnlyCollection<Resource> LoadedResources
	{
		[Token(Token = "0x6000B8F")]
		[Address(RVA = "0x538100", Offset = "0x536B00", VA = "0x180538100", Slot = "10")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1400001E")]
	public event Action<float> OnLoadProgress
	{
		[Token(Token = "0x6000B90")]
		[Address(RVA = "0x538150", Offset = "0x536B50", VA = "0x180538150", Slot = "4")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000B91")]
		[Address(RVA = "0x538260", Offset = "0x536C60", VA = "0x180538260", Slot = "5")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400001F")]
	public event Action<string> OnMessage
	{
		[Token(Token = "0x6000B92")]
		[Address(RVA = "0x538370", Offset = "0x536D70", VA = "0x180538370", Slot = "6")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000B93")]
		[Address(RVA = "0x538480", Offset = "0x536E80", VA = "0x180538480", Slot = "7")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6000B94")]
	public bool SupportsType<T>() where T : UnityEngine.Object
	{
		return default(bool);
	}

	[Token(Token = "0x6000B95")]
	public void AddResource<T>(string path, T obj) where T : UnityEngine.Object
	{
	}

	[Token(Token = "0x6000B96")]
	public void SetResource<T>(string path, T obj) where T : UnityEngine.Object
	{
	}

	[Token(Token = "0x6000B97")]
	[Address(RVA = "0x538590", Offset = "0x536F90", VA = "0x180538590")]
	public void RemoveResource(string path)
	{
	}

	[Token(Token = "0x6000B98")]
	[Address(RVA = "0x5385F0", Offset = "0x536FF0", VA = "0x1805385F0")]
	public void RemoveAllResources()
	{
	}

	[Token(Token = "0x6000B99")]
	[Address(RVA = "0x538640", Offset = "0x537040", VA = "0x180538640")]
	public void AddFolder(string folderPath)
	{
	}

	[Token(Token = "0x6000B9A")]
	[Address(RVA = "0x5386A0", Offset = "0x5370A0", VA = "0x1805386A0")]
	public void RemoveFolder(string path)
	{
	}

	[Token(Token = "0x6000B9B")]
	public Resource<T> LoadResourceBlocking<T>(string path) where T : UnityEngine.Object
	{
		return null;
	}

	[Token(Token = "0x6000B9C")]
	public UniTask<Resource<T>> LoadResource<T>(string path) where T : UnityEngine.Object
	{
		return default(UniTask<Resource<T>>);
	}

	[Token(Token = "0x6000B9D")]
	public IReadOnlyCollection<Resource<T>> LoadResourcesBlocking<T>(string path) where T : UnityEngine.Object
	{
		return null;
	}

	[Token(Token = "0x6000B9E")]
	public UniTask<IReadOnlyCollection<Resource<T>>> LoadResources<T>(string path) where T : UnityEngine.Object
	{
		return default(UniTask<IReadOnlyCollection<Resource<T>>>);
	}

	[Token(Token = "0x6000B9F")]
	[Address(RVA = "0x538700", Offset = "0x537100", VA = "0x180538700")]
	public IReadOnlyCollection<Folder> LocateFoldersBlocking(string path)
	{
		return null;
	}

	[Token(Token = "0x6000BA0")]
	[Address(RVA = "0x538910", Offset = "0x537310", VA = "0x180538910", Slot = "15")]
	public UniTask<IReadOnlyCollection<Folder>> LocateFolders(string path)
	{
		return default(UniTask<IReadOnlyCollection<Folder>>);
	}

	[Token(Token = "0x6000BA1")]
	public IReadOnlyCollection<string> LocateResourcesBlocking<T>(string path) where T : UnityEngine.Object
	{
		return null;
	}

	[Token(Token = "0x6000BA2")]
	public UniTask<IReadOnlyCollection<string>> LocateResources<T>(string path) where T : UnityEngine.Object
	{
		return default(UniTask<IReadOnlyCollection<string>>);
	}

	[Token(Token = "0x6000BA3")]
	public bool ResourceExistsBlocking<T>(string path) where T : UnityEngine.Object
	{
		return default(bool);
	}

	[Token(Token = "0x6000BA4")]
	public UniTask<bool> ResourceExists<T>(string path) where T : UnityEngine.Object
	{
		return default(UniTask<bool>);
	}

	[Token(Token = "0x6000BA5")]
	[Address(RVA = "0x5389B0", Offset = "0x5373B0", VA = "0x1805389B0", Slot = "19")]
	public bool ResourceLoaded(string path)
	{
		return default(bool);
	}

	[Token(Token = "0x6000BA6")]
	[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360", Slot = "20")]
	public bool ResourceLoading(string path)
	{
		return default(bool);
	}

	[Token(Token = "0x6000BA7")]
	[Address(RVA = "0x538A00", Offset = "0x537400", VA = "0x180538A00", Slot = "17")]
	public void UnloadResourceBlocking(string path)
	{
	}

	[Token(Token = "0x6000BA8")]
	[Address(RVA = "0x538A60", Offset = "0x537460", VA = "0x180538A60")]
	public UniTask UnloadResource(string path)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000BA9")]
	[Address(RVA = "0x538B00", Offset = "0x537500", VA = "0x180538B00", Slot = "18")]
	public void UnloadResourcesBlocking()
	{
	}

	[Token(Token = "0x6000BAA")]
	[Address(RVA = "0x538B60", Offset = "0x537560", VA = "0x180538B60")]
	public UniTask UnloadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000BAB")]
	public Resource<T> GetLoadedResourceOrNull<T>(string path) where T : UnityEngine.Object
	{
		return null;
	}

	[Token(Token = "0x6000BAC")]
	[Address(RVA = "0x538BF0", Offset = "0x5375F0", VA = "0x180538BF0")]
	public VirtualResourceProvider()
	{
	}
}
