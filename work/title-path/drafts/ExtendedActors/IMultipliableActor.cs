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

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD18B00, 105 B
    	// rcx = actorId, rdx = subId: `subId != null && subId.Length > 0` (0x180D18B31 - 0x180D18B3A) ->
    	// String.Concat(actorId, "-", subId) (tail call 0x180E267A0 with STR '-'), otherwise actorId unchanged.
    	// Annotated disassembly: work/title-path/disas/imab_01_BuildId.txt
    	[Token(Token = "0x600004D")]
    	[Address(RVA = "0xD18B00", Offset = "0xD17500", VA = "0x180D18B00")]
    	static string BuildId(string actorId, string subId)
    	{
    		return string.IsNullOrEmpty(subId) ? actorId : actorId + "-" + subId;
    	}
    }
}
