using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Naninovel.UI
{
    [Token(Token = "0x2000523")]
    public class FocusOnSubmit : MonoBehaviour, ISubmitHandler, IEventSystemHandler
    {
    	[Token(Token = "0x600198E")]
    	[Address(RVA = "0x61BB80", Offset = "0x61A580", VA = "0x18061BB80", Slot = "4")]
    	public void OnSubmit(BaseEventData eventData)
    	{
    	}

    	[Token(Token = "0x600198F")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public FocusOnSubmit()
    	{
    	}
    }
}
