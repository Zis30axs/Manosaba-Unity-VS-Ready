using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;

namespace WitchTrials.Models
{
    [Token(Token = "0x200018A")]
    public interface IPerspectiveStageLoader
    {
    	[Token(Token = "0x60006B6")]
    	UniTask LoadStageAsync(PerspectiveStageKind kind, [Optional] CancellationToken ct);

    	[Token(Token = "0x60006B7")]
    	void UnloadStage();
    }
}
