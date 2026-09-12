using System.Diagnostics;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Async.CompilerServices;

[Token(Token = "0x2000658")]
internal class MoveNextRunner<TStateMachine> where TStateMachine : IAsyncStateMachine
{
	[Token(Token = "0x4001810")]
	[FieldOffset(Offset = "0x0")]
	public TStateMachine StateMachine;

	[Token(Token = "0x6001F4B")]
	[DebuggerHidden]
	public void Run()
	{
	}

	[Token(Token = "0x6001F4C")]
	public MoveNextRunner()
	{
	}
}
