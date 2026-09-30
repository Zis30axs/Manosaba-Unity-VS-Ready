using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20001C6")]
    internal static class CompletedTasks
    {
    	[Token(Token = "0x4000796")]
    	[FieldOffset(Offset = "0x0")]
    	public static readonly UniTask<bool> True;

    	[Token(Token = "0x4000797")]
    	[FieldOffset(Offset = "0x10")]
    	public static readonly UniTask<bool> False;

    	[Token(Token = "0x4000798")]
    	[FieldOffset(Offset = "0x20")]
    	public static readonly UniTask<int> Zero;

    	[Token(Token = "0x4000799")]
    	[FieldOffset(Offset = "0x30")]
    	public static readonly UniTask<int> MinusOne;

    	[Token(Token = "0x400079A")]
    	[FieldOffset(Offset = "0x40")]
    	public static readonly UniTask<int> One;
    }
}
