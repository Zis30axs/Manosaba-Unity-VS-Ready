using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000254")]
public interface IResourceLoader
{
	[Token(Token = "0x6000A52")]
	string GetFullPath(string localPath);

	[Token(Token = "0x6000A53")]
	string GetLocalPath(string fullPath);

	[Token(Token = "0x6000A54")]
	[CanBeNull]
	string GetLocalPath(UnityEngine.Object obj);

	[Token(Token = "0x6000A55")]
	UniTask<bool> Exists(string path);

	[Token(Token = "0x6000A56")]
	UniTask<IReadOnlyCollection<string>> Locate([Optional][CanBeNull] string path);

	[Token(Token = "0x6000A57")]
	bool IsLoaded(string path);

	[Token(Token = "0x6000A58")]
	[CanBeNull]
	Resource GetLoaded(string path);

	[Token(Token = "0x6000A59")]
	IReadOnlyCollection<Resource> GetAllLoaded();

	[Token(Token = "0x6000A5A")]
	UniTask<Resource> Load(string path, [Optional][CanBeNull] object holder);

	[Token(Token = "0x6000A5B")]
	UniTask<IReadOnlyCollection<Resource>> LoadAll([Optional][CanBeNull] string path, [Optional][CanBeNull] object holder);

	[Token(Token = "0x6000A5C")]
	void Hold(string path, object holder);

	[Token(Token = "0x6000A5D")]
	void Release(string path, object holder, bool unload = true);

	[Token(Token = "0x6000A5E")]
	void ReleaseAll(object holder, bool unload = true);

	[Token(Token = "0x6000A5F")]
	bool IsHeldBy(string path, object holder);

	[Token(Token = "0x6000A60")]
	int CountHolders([Optional][CanBeNull] string path);
}
[Token(Token = "0x2000255")]
public interface IResourceLoader<TResource> : IResourceLoader where TResource : UnityEngine.Object
{
	[Token(Token = "0x14000016")]
	event Action<Resource<TResource>> OnLoaded;

	[Token(Token = "0x14000017")]
	event Action<Resource<TResource>> OnUnloaded;

	[Token(Token = "0x6000A65")]
	[CanBeNull]
	string GetLocalPath(TResource obj);

	[Token(Token = "0x6000A66")]
	[CanBeNull]
	new Resource<TResource> GetLoaded(string path);

	[Token(Token = "0x6000A67")]
	new IReadOnlyCollection<Resource<TResource>> GetAllLoaded();

	[Token(Token = "0x6000A68")]
	new UniTask<Resource<TResource>> Load(string path, [Optional][CanBeNull] object holder);

	[Token(Token = "0x6000A69")]
	new UniTask<IReadOnlyCollection<Resource<TResource>>> LoadAll([Optional] string path, [Optional][CanBeNull] object holder);
}
