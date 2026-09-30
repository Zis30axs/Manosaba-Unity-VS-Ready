using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel
{
    [Token(Token = "0x20002BB")]
    public class ScriptableScrollRect : ScriptableUIControl<ScrollRect>
    {
    	[Token(Token = "0x14000044")]
    	public event Action<Vector2> OnPositionChanged
    	{
    		[Token(Token = "0x6000CE2")]
    		[Address(RVA = "0x53F8B0", Offset = "0x53E2B0", VA = "0x18053F8B0")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000CE3")]
    		[Address(RVA = "0x53F9D0", Offset = "0x53E3D0", VA = "0x18053F9D0")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x6000CE4")]
    	[Address(RVA = "0x53FAF0", Offset = "0x53E4F0", VA = "0x18053FAF0", Slot = "60")]
    	protected override void BindUIEvents()
    	{
    	}

    	[Token(Token = "0x6000CE5")]
    	[Address(RVA = "0x53FC20", Offset = "0x53E620", VA = "0x18053FC20", Slot = "61")]
    	protected override void UnbindUIEvents()
    	{
    	}

    	[Token(Token = "0x6000CE6")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "62")]
    	protected virtual void OnScrollPositionChanged(Vector2 scrollPosition)
    	{
    	}

    	[Token(Token = "0x6000CE7")]
    	[Address(RVA = "0x53FDB0", Offset = "0x53E7B0", VA = "0x18053FDB0")]
    	private void InvokeOnPositionChanged(Vector2 value)
    	{
    	}

    	[Token(Token = "0x6000CE8")]
    	[Address(RVA = "0x53FDE0", Offset = "0x53E7E0", VA = "0x18053FDE0")]
    	public ScriptableScrollRect()
    	{
    	}
    }
}
