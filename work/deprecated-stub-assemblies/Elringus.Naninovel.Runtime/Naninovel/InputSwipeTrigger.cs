using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000377")]
    public class InputSwipeTrigger
    {
    	[Token(Token = "0x4000CF3")]
    	[FieldOffset(Offset = "0x10")]
    	[Tooltip("Swipe of which direction should be registered.")]
    	public InputSwipeDirection Direction;

    	[Token(Token = "0x4000CF4")]
    	[FieldOffset(Offset = "0x14")]
    	[Tooltip("How much fingers (touches) should be active to register the swipe.")]
    	[Range(1f, 5f)]
    	public int FingerCount;

    	[Token(Token = "0x4000CF5")]
    	[FieldOffset(Offset = "0x18")]
    	[Tooltip("Minimum required swipe distance to activate the trigger, in pixels.")]
    	public float MinimumDistance;

    	[Token(Token = "0x4000CF6")]
    	[FieldOffset(Offset = "0x1C")]
    	[Tooltip("Whether to activate the input while moving fingers. When disabled, will only active when fingers are released.")]
    	public bool ActivateOnMove;

    	[Token(Token = "0x4000CF7")]
    	[FieldOffset(Offset = "0x20")]
    	private Vector2 startPosition;

    	[Token(Token = "0x60010C2")]
    	[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360")]
    	public bool Sample()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60010C3")]
    	[Address(RVA = "0x57B410", Offset = "0x579E10", VA = "0x18057B410")]
    	public InputSwipeTrigger()
    	{
    	}
    }
}
