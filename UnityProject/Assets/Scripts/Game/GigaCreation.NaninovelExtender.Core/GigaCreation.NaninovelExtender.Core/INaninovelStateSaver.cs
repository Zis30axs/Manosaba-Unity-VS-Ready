using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;

namespace GigaCreation.NaninovelExtender.Core
{
    [Token(Token = "0x2000005")]
    public interface INaninovelStateSaver
    {
    	[Token(Token = "0x6000005")]
    	UniTask SaveSettingsAsync([Optional] CancellationToken ct);
    }
}
