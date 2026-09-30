using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Commands
{
    [Token(Token = "0x200066A")]
    [CommandAlias("hideAll")]
    public class HideAllActors : Command
    {
    	[Token(Token = "0x400185C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("time")]
    	[ParameterDefaultValue("0.35")]
    	public DecimalParameter Duration;

    	[Token(Token = "0x400185D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterDefaultValue("false")]
    	public BooleanParameter Lazy;

    	[Token(Token = "0x400185E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	public BooleanParameter Wait;

    	[Token(Token = "0x6001F7B")]
    	[Address(RVA = "0x4D7520", Offset = "0x4D5F20", VA = "0x1804D7520", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001F7C")]
    	[Address(RVA = "0x4D75D0", Offset = "0x4D5FD0", VA = "0x1804D75D0")]
    	private UniTask HideManagedActors(IActorManager manager, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001F7D")]
    	[Address(RVA = "0x4D2580", Offset = "0x4D0F80", VA = "0x1804D2580")]
    	public HideAllActors()
    	{
    	}
    }
}
