using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.AddressablesUtils;

[Token(Token = "0x200001E")]
public interface IAddressablesCacheCleaner
{
	[Token(Token = "0x6000053")]
	UniTask<bool> ClearCacheAsync([Optional] CancellationToken ct);
}
