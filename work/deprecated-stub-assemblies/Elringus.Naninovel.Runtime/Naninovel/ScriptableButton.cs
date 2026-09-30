using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine.UI;

namespace Naninovel
{
    [Token(Token = "0x20002B1")]
    public class ScriptableButton : ScriptableUIControl<Button>
    {
    	[Token(Token = "0x170001BA")]
    	public override bool Interactable
    	{
    		[Token(Token = "0x6000C9D")]
    		[Address(RVA = "0x53DF90", Offset = "0x53C990", VA = "0x18053DF90", Slot = "31")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x14000041")]
    	public event Action OnButtonClicked
    	{
    		[Token(Token = "0x6000C9B")]
    		[Address(RVA = "0x53DD90", Offset = "0x53C790", VA = "0x18053DD90")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000C9C")]
    		[Address(RVA = "0x53DE90", Offset = "0x53C890", VA = "0x18053DE90")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x6000C9E")]
    	[Address(RVA = "0x53E0B0", Offset = "0x53CAB0", VA = "0x18053E0B0", Slot = "50")]
    	public override void SetInteractable(bool interactable)
    	{
    	}

    	[Token(Token = "0x6000C9F")]
    	[Address(RVA = "0x53E1F0", Offset = "0x53CBF0", VA = "0x18053E1F0", Slot = "60")]
    	protected override void BindUIEvents()
    	{
    	}

    	[Token(Token = "0x6000CA0")]
    	[Address(RVA = "0x53E310", Offset = "0x53CD10", VA = "0x18053E310", Slot = "61")]
    	protected override void UnbindUIEvents()
    	{
    	}

    	[Token(Token = "0x6000CA1")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "62")]
    	protected virtual void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x6000CA2")]
    	[Address(RVA = "0x53E490", Offset = "0x53CE90", VA = "0x18053E490")]
    	private void InvokeOnButtonClicked()
    	{
    	}

    	[Token(Token = "0x6000CA3")]
    	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
    	public ScriptableButton()
    	{
    	}
    }
}
