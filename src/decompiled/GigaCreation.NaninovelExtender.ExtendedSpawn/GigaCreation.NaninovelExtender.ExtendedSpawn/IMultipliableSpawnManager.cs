using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.ExtendedSpawn;

[Token(Token = "0x2000008")]
public interface IMultipliableSpawnManager : ISpawnManager, IEngineService<SpawnConfiguration>, IEngineService
{
	[Token(Token = "0x6000011")]
	UniTask<SpawnedObject> Spawn(string path, string subId, [Optional] AsyncToken asyncToken);

	[Token(Token = "0x6000012")]
	bool IsSpawned(string path, string subId);

	[Token(Token = "0x6000013")]
	SpawnedObject GetSpawned(string path, string subId);

	[Token(Token = "0x6000014")]
	void DestroySpawned(string path, string subId);
}
