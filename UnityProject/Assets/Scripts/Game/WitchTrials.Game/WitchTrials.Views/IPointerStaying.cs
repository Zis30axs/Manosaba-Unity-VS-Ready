using Il2CppDummyDll;
using UnityEngine.EventSystems;

namespace WitchTrials.Views
{
    [Token(Token = "0x200000D")]
    public interface IPointerStaying
    {
    	[Token(Token = "0x6000025")]
    	void DoPointerEnter(PointerEventData eventData);

    	[Token(Token = "0x6000026")]
    	void DoPointerExit(PointerEventData eventData);

    	[Token(Token = "0x6000027")]
    	void RegisterTo(PointerStayingPropagator pointerStayingPropagator);
    }
}
