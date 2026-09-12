using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x200045B")]
public interface ISpawnManager : IEngineService<SpawnConfiguration>, IEngineService
{
	[Token(Token = "0x170002FC")]
	IReadOnlyCollection<SpawnedObject> Spawned
	{
		[Token(Token = "0x6001583")]
		get;
	}

	[Token(Token = "0x6001584")]
	UniTask<SpawnedObject> Spawn(string path, [Optional] AsyncToken token);

	[Token(Token = "0x6001585")]
	bool IsSpawned(string path);

	[Token(Token = "0x6001586")]
	SpawnedObject GetSpawned(string path);

	[Token(Token = "0x6001587")]
	void DestroySpawned(string path);

	[Token(Token = "0x6001588")]
	UniTask HoldResources(string path, object holder);

	[Token(Token = "0x6001589")]
	void ReleaseResources(string path, object holder);
}
