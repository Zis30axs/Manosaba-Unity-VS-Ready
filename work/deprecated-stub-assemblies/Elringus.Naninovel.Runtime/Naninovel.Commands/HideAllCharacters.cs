using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x200066D")]
    [CommandAlias("hideChars")]
    public class HideAllCharacters : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200066F")]
    	[CompilerGenerated]
    	private struct _003CHide_003Ed__4
    {
    		[Token(Token = "0x4001868")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001869")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400186A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AsyncToken token;

    		[Token(Token = "0x400186B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public HideAllCharacters _003C_003E4__this;

    		[Token(Token = "0x400186C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001F88")]
    		[Address(RVA = "0x4D7F20", Offset = "0x4D6920", VA = "0x1804D7F20", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001F89")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001863")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("time")]
    	[ParameterDefaultValue("0.35")]
    	public DecimalParameter Duration;

    	[Token(Token = "0x4001864")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterDefaultValue("false")]
    	public BooleanParameter Lazy;

    	[Token(Token = "0x4001865")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	public BooleanParameter Wait;

    	[Token(Token = "0x6001F83")]
    	[Address(RVA = "0x4D7BF0", Offset = "0x4D65F0", VA = "0x1804D7BF0", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001F84")]
    	[Address(RVA = "0x4D7CA0", Offset = "0x4D66A0", VA = "0x1804D7CA0", Slot = "16")]
    	[AsyncStateMachine(typeof(_003CHide_003Ed__4))]
    	protected virtual UniTask Hide(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001F85")]
    	[Address(RVA = "0x4D2580", Offset = "0x4D0F80", VA = "0x1804D2580")]
    	public HideAllCharacters()
    	{
    	}
    }
}
