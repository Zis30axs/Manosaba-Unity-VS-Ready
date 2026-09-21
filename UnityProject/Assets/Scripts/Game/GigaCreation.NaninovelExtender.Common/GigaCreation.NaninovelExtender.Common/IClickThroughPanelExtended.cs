using Il2CppDummyDll;
using Naninovel.UI;

namespace GigaCreation.NaninovelExtender.Common
{
    [Token(Token = "0x2000013")]
    public interface IClickThroughPanelExtended : IClickThroughPanel, IManagedUI
    {
    	[Token(Token = "0x17000003")]
    	bool Exists
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCFA5B0 -- default interface member, same code as ICancelEventBlockable.Exists:
    		//   isinst UnityEngine.Object, then the inlined Object != null
    		[Token(Token = "0x6000022")]
    		[Address(RVA = "0xCFA5B0", Offset = "0xCF8FB0", VA = "0x180CFA5B0", Slot = "0")]
    		get
    		{
    			return (this as UnityEngine.Object) != null;
    		}
    	}

    	[Token(Token = "0x6000023")]
    	void DontBlockInteractionOnNext();

    	[Token(Token = "0x6000024")]
    	void SetSortingOrder(int order);
    }
}
