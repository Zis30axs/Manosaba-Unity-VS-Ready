using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Commands
{
    [Token(Token = "0x2000681")]
    [CommandAlias("remove")]
    public class RemoveActors : Command
    {
    	[Token(Token = "0x40018E0")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	[ActorContext("*", -1, null)]
    	public StringListParameter ActorIds;

    	[Token(Token = "0x6001FDF")]
    	[Address(RVA = "0x4DABD0", Offset = "0x4D95D0", VA = "0x1804DABD0", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001FE0")]
    	[Address(RVA = "0x4DAC60", Offset = "0x4D9660", VA = "0x1804DAC60", Slot = "16")]
    	protected virtual bool ShouldRemoveAll()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001FE1")]
    	[Address(RVA = "0x4DAD70", Offset = "0x4D9770", VA = "0x1804DAD70", Slot = "17")]
    	protected virtual void RemoveAll()
    	{
    	}

    	[Token(Token = "0x6001FE2")]
    	[Address(RVA = "0x4DAFC0", Offset = "0x4D99C0", VA = "0x1804DAFC0", Slot = "18")]
    	protected virtual void RemoveSpecified()
    	{
    	}

    	[Token(Token = "0x6001FE3")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public RemoveActors()
    	{
    	}
    }
}
