using System;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;

namespace Naninovel.UI
{
    [Token(Token = "0x20005D7")]
    public class ToastAppearance : MonoBehaviour
    {
    	[Serializable]
    	[Token(Token = "0x20005D8")]
    	private class TextChangedEvent : UnityEvent<string>
    	{
    		[Token(Token = "0x6001D27")]
    		[Address(RVA = "0x4AEFC0", Offset = "0x4AD9C0", VA = "0x1804AEFC0")]
    		public TextChangedEvent()
    		{
    		}
    	}

    	[Token(Token = "0x400165F")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private TextChangedEvent onTextChanged;

    	[Token(Token = "0x4001660")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private UnityEvent onSelected;

    	[Token(Token = "0x4001661")]
    	[FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private UnityEvent onDeselected;

    	[Token(Token = "0x6001D24")]
    	[Address(RVA = "0x4AEF00", Offset = "0x4AD900", VA = "0x1804AEF00", Slot = "4")]
    	public virtual void SetText(string text)
    	{
    	}

    	[Token(Token = "0x6001D25")]
    	[Address(RVA = "0x4AEF60", Offset = "0x4AD960", VA = "0x1804AEF60", Slot = "5")]
    	public virtual void SetSelected(bool selected)
    	{
    	}

    	[Token(Token = "0x6001D26")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public ToastAppearance()
    	{
    	}
    }
}
