using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000368")]
    public class InputBinding
    {
    	[Token(Token = "0x4000C95")]
    	[FieldOffset(Offset = "0x10")]
    	[Tooltip("Name (ID) of the binding used to access it via the input manager.")]
    	public string Name;

    	[Token(Token = "0x4000C96")]
    	[FieldOffset(Offset = "0x18")]
    	[Tooltip("Whether to always process the binding, even when out of the game and in menus.")]
    	public bool AlwaysProcess;

    	[Token(Token = "0x4000C97")]
    	[FieldOffset(Offset = "0x20")]
    	[Tooltip("Keys that should trigger this binding.")]
    	public List<KeyCode> Keys;

    	[Token(Token = "0x4000C98")]
    	[FieldOffset(Offset = "0x28")]
    	[Tooltip("Axes that should trigger this binding.")]
    	public List<InputAxisTrigger> Axes;

    	[Token(Token = "0x4000C99")]
    	[FieldOffset(Offset = "0x30")]
    	[Tooltip("Swipes (touch screen) that should trigger this binding.")]
    	public List<InputSwipeTrigger> Swipes;

    	[Token(Token = "0x6001063")]
    	[Address(RVA = "0x573050", Offset = "0x571A50", VA = "0x180573050")]
    	public InputBinding()
    	{
    	}
    }
}
