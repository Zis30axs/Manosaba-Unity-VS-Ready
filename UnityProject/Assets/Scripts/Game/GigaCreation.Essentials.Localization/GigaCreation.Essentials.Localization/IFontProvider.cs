using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace GigaCreation.Essentials.Localization
{
    [Token(Token = "0x2000005")]
    public interface IFontProvider
    {
    	[Token(Token = "0x6000005")]
    	UniTask<TMP_FontAsset> GetOrLoadFontAssetAsync(LocaleKind locale, [Optional] CancellationToken ct);

    	[Token(Token = "0x6000006")]
    	UniTask<Material[]> GetOrLoadFontMaterialsAsync(LocaleKind locale, [Optional] CancellationToken ct);
    }
}
