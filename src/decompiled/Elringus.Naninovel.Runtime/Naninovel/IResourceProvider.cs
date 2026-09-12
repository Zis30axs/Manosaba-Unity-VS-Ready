using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000256")]
public interface IResourceProvider
{
	[Token(Token = "0x1700017E")]
	bool IsLoading
	{
		[Token(Token = "0x6000A6E")]
		get;
	}

	[Token(Token = "0x1700017F")]
	float LoadProgress
	{
		[Token(Token = "0x6000A6F")]
		get;
	}

	[Token(Token = "0x17000180")]
	IReadOnlyCollection<Resource> LoadedResources
	{
		[Token(Token = "0x6000A70")]
		get;
	}

	[Token(Token = "0x14000018")]
	event Action<float> OnLoadProgress;

	[Token(Token = "0x14000019")]
	event Action<string> OnMessage;

	[Token(Token = "0x6000A71")]
	bool SupportsType<T>() where T : UnityEngine.Object;

	[Token(Token = "0x6000A72")]
	UniTask<Resource<T>> LoadResource<T>(string path) where T : UnityEngine.Object;

	[Token(Token = "0x6000A73")]
	UniTask<IReadOnlyCollection<Resource<T>>> LoadResources<T>(string path) where T : UnityEngine.Object;

	[Token(Token = "0x6000A74")]
	UniTask<IReadOnlyCollection<string>> LocateResources<T>(string path) where T : UnityEngine.Object;

	[Token(Token = "0x6000A75")]
	UniTask<IReadOnlyCollection<Folder>> LocateFolders(string path);

	[Token(Token = "0x6000A76")]
	UniTask<bool> ResourceExists<T>(string path) where T : UnityEngine.Object;

	[Token(Token = "0x6000A77")]
	void UnloadResourceBlocking(string path);

	[Token(Token = "0x6000A78")]
	void UnloadResourcesBlocking();

	[Token(Token = "0x6000A79")]
	bool ResourceLoaded(string path);

	[Token(Token = "0x6000A7A")]
	bool ResourceLoading(string path);

	[Token(Token = "0x6000A7B")]
	Resource<T> GetLoadedResourceOrNull<T>(string path) where T : UnityEngine.Object;
}
