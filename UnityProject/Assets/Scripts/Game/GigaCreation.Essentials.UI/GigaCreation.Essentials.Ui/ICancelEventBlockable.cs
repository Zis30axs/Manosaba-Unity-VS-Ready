using Il2CppDummyDll;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000017")]
    public interface ICancelEventBlockable
    {
    	[Token(Token = "0x17000009")]
    	bool Exists
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCDBF60 -- default interface member: isinst UnityEngine.Object, then the
    		//   inlined Object != null (false for a non-Object implementer, else m_CachedPtr != 0)
    		[Token(Token = "0x6000055")]
    		[Address(RVA = "0xCDBF60", Offset = "0xCDA960", VA = "0x180CDBF60", Slot = "0")]
    		get
    		{
    			return (this as UnityEngine.Object) != null;
    		}
    	}

    	[Token(Token = "0x1700000A")]
    	bool IsCancelEventBlocked
    	{
    		[Token(Token = "0x6000056")]
    		set;
    	}
    }
}
