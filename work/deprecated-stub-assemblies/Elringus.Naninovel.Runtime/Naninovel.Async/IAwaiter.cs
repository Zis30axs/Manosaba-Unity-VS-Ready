using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Async
{
    [Token(Token = "0x2000608")]
    public interface IAwaiter : ICriticalNotifyCompletion, INotifyCompletion
    {
    	[Token(Token = "0x17000489")]
    	AwaiterStatus Status
    	{
    		[Token(Token = "0x6001E51")]
    		get;
    	}

    	[Token(Token = "0x1700048A")]
    	bool IsCompleted
    	{
    		[Token(Token = "0x6001E52")]
    		get;
    	}

    	[Token(Token = "0x6001E53")]
    	void GetResult();
    }
    [Token(Token = "0x2000609")]
    public interface IAwaiter<out T> : IAwaiter, ICriticalNotifyCompletion, INotifyCompletion
    {
    	[Token(Token = "0x6001E54")]
    	new T GetResult();
    }
}
