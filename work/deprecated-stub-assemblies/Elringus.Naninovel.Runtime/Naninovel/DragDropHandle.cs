using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Naninovel
{
    [Token(Token = "0x20002AF")]
    [RequireComponent(typeof(RectTransform))]
    public class DragDropHandle : MonoBehaviour, IDragHandler, IEventSystemHandler
    {
    	[Token(Token = "0x14000040")]
    	public event Action<Vector2> OnDragged
    	{
    		[Token(Token = "0x6000C8F")]
    		[Address(RVA = "0x53CE80", Offset = "0x53B880", VA = "0x18053CE80")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000C90")]
    		[Address(RVA = "0x53CF90", Offset = "0x53B990", VA = "0x18053CF90")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x6000C91")]
    	[Address(RVA = "0x53D0A0", Offset = "0x53BAA0", VA = "0x18053D0A0", Slot = "5")]
    	public virtual void OnDrag(PointerEventData eventData)
    	{
    	}

    	[Token(Token = "0x6000C92")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DragDropHandle()
    	{
    	}
    }
}
