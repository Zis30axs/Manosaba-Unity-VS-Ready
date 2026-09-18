using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.Localization
{
    [Token(Token = "0x2000006")]
    public interface ILocalizationManager : IService, ILocalizationProvider, IFontProvider
    {
    	[Token(Token = "0x6000007")]
    	UniTask ChangeLocaleAsync(LocaleKind locale, [Optional] CancellationToken ct);
    }
}
