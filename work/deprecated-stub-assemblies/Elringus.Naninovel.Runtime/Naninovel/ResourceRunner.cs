using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000290")]
    public abstract class ResourceRunner
    {
    	[Token(Token = "0x4000A03")]
    	[FieldOffset(Offset = "0x10")]
    	public readonly string Path;

    	[Token(Token = "0x4000A04")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly Type ResourceType;

    	[Token(Token = "0x6000B7C")]
    	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
    	protected ResourceRunner(string path = default, Type resourceType = default)
    	{
    	}

    	[Token(Token = "0x6000B7D")]
    	[Address(RVA = "0x538020", Offset = "0x536A20", VA = "0x180538020")]
    	public UniTask.Awaiter GetAwaiter()
    	{
    		return default(UniTask.Awaiter);
    	}

    	[Token(Token = "0x6000B7E")]
    	public abstract UniTask Run();

    	[Token(Token = "0x6000B7F")]
    	public abstract void Cancel();

    	[Token(Token = "0x6000B80")]
    	protected abstract UniTask.Awaiter GetAwaiterImpl();
    }
    [Token(Token = "0x2000291")]
    public abstract class ResourceRunner<TResult> : ResourceRunner
    {
    	[Token(Token = "0x4000A06")]
    	[FieldOffset(Offset = "0x0")]
    	private readonly UniTaskCompletionSource<TResult> completionSource;

    	[Token(Token = "0x17000198")]
    	public TResult Result
    	{
    		[Token(Token = "0x6000B81")]
    		[CompilerGenerated]
    		get
    		{
    			return default(TResult);
    		}
    		[Token(Token = "0x6000B82")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x6000B83")]
    	protected ResourceRunner(string path = default, Type resourceType = default)
    	{
    	}

    	[Token(Token = "0x6000B84")]
    	public new UniTask<TResult>.Awaiter GetAwaiter()
    	{
    		return default(UniTask<TResult>.Awaiter);
    	}

    	[Token(Token = "0x6000B85")]
    	public override void Cancel()
    	{
    	}

    	[Token(Token = "0x6000B86")]
    	protected void SetResult(TResult result)
    	{
    	}

    	[Token(Token = "0x6000B87")]
    	protected override UniTask.Awaiter GetAwaiterImpl()
    	{
    		return default(UniTask.Awaiter);
    	}
    }
}
