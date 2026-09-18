using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Serializable]
    [Token(Token = "0x200005E")]
    public class MessageDrawer
    {
    	[Token(Token = "0x400015C")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private string info;

    	[Token(Token = "0x6000110")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	[Conditional("UNITY_EDITOR")]
    	public void HideMessages(bool value)
    	{
    	}

    	[Token(Token = "0x6000111")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	[Conditional("UNITY_EDITOR")]
    	public void DrawMessage(string message, MessageBoxType type = MessageBoxType.None, bool replaceCurrents = false)
    	{
    	}

    	[Token(Token = "0x6000112")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	[Conditional("UNITY_EDITOR")]
    	public void DrawInfo(string message, bool replaceCurrents = false)
    	{
    	}

    	[Token(Token = "0x6000113")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	[Conditional("UNITY_EDITOR")]
    	public void DrawWarning(string message, bool replaceCurrents = false)
    	{
    	}

    	[Token(Token = "0x6000114")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	[Conditional("UNITY_EDITOR")]
    	public void DrawError(string message, bool replaceCurrents = false)
    	{
    	}

    	[Token(Token = "0x6000115")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public MessageDrawer()
    	{
    	}
    }
}
