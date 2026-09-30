using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Naninovel.UI
{
    [Token(Token = "0x2000527")]
    public class ScrollOnFocus : MonoBehaviour, ISelectHandler, IEventSystemHandler
    {
    	[Token(Token = "0x40013F8")]
    	[FieldOffset(Offset = "0x20")]
    	[Tooltip("Content to focus.")]
    	[SerializeField]
    	private RectTransform content;

    	[Token(Token = "0x40013F9")]
    	[FieldOffset(Offset = "0x28")]
    	private ScrollRect rect;

    	[Token(Token = "0x60019A0")]
    	[Address(RVA = "0x61CD50", Offset = "0x61B750", VA = "0x18061CD50", Slot = "5")]
    	public virtual void OnSelect(BaseEventData eventData)
    	{
    	}

    	[Token(Token = "0x60019A1")]
    	[Address(RVA = "0x61D010", Offset = "0x61BA10", VA = "0x18061D010", Slot = "6")]
    	protected virtual void Awake()
    	{
    	}

    	[Token(Token = "0x60019A2")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public ScrollOnFocus()
    	{
    	}
    }
}
