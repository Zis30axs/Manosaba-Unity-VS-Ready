using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using JetBrains.Annotations;

namespace GigaCreation.Tools.UniTasks
{
    [Token(Token = "0x2000004")]
    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public interface ISimpleWaitable
    {
    	[Token(Token = "0x17000001")]
    	bool IsPending
    	{
    		[Token(Token = "0x6000003")]
    		get;
    	}

    	[Token(Token = "0x6000004")]
    	UniTask<float> WaitForCompletionAsync([Optional] CancellationToken ct);
    }
}
