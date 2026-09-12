using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.SaveLoad;

[Token(Token = "0x2000004")]
public interface ISaveLoadManager : IService
{
	[Token(Token = "0x6000003")]
	UniTask<TSavedData> LoadAsync<TSavedData>([Optional] CancellationToken ct) where TSavedData : SavedData, new();

	[Token(Token = "0x6000004")]
	UniTask SaveAsync<TSavedData>([Optional] CancellationToken ct) where TSavedData : SavedData, new();
}
