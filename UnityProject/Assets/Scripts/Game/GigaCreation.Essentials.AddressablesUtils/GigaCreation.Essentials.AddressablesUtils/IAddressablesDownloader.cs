using System;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.AddressablesUtils
{
    [Token(Token = "0x200001F")]
    public interface IAddressablesDownloader
    {
    	[Token(Token = "0x6000054")]
    	UniTask<bool> CheckForCatalogUpdatesAsync(bool logResult = false, CancellationToken ct = default);

    	[Token(Token = "0x6000055")]
    	UniTask<bool> UpdateCatalogsAsync(CancellationToken ct = default);

    	[Token(Token = "0x6000056")]
    	UniTask<bool> CleanBundleCacheAsync(CancellationToken ct = default);

    	[Token(Token = "0x6000057")]
    	UniTask<(bool, long)> GetDownloadSizeAsync(CancellationToken ct = default);

    	[Token(Token = "0x6000058")]
    	UniTask<bool> DownloadAsync(CancellationToken ct = default);

    	[Token(Token = "0x6000059")]
    	IObservable<(float, float)> OnDownloadProgressAsObservable();
    }
}
