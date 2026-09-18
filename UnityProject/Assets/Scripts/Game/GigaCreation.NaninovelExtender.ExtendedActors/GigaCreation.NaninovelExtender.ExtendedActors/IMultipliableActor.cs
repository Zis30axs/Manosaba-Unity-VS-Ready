using Il2CppDummyDll;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x200001E")]
    public interface IMultipliableActor
    {
    	[Token(Token = "0x17000006")]
    	string SubId
    	{
    		[Token(Token = "0x600004C")]
    		get;
    	}

    	[Token(Token = "0x600004D")]
    	[Address(RVA = "0xD18B00", Offset = "0xD17500", VA = "0x180D18B00")]
    	static string BuildId(string actorId, string subId)
    	{
    		return null;
    	}
    }
}
