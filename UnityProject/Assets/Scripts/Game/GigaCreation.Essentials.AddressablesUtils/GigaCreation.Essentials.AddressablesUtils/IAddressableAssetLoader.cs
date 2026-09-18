using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using UnityEngine.AddressableAssets;

namespace GigaCreation.Essentials.AddressablesUtils
{
    [Token(Token = "0x200001C")]
    public interface IAddressableAssetLoader
    {
    	[Token(Token = "0x6000050")]
    	UniTask<T> GetOrLoadAddressableAsset<T>(string address, [Optional] CancellationToken ct);

    	[Token(Token = "0x6000051")]
    	UniTask<T> GetOrLoadAddressableAsset<T>(AssetReference reference, [Optional] CancellationToken ct);
    }
}
