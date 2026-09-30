using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20002BA")]
    public class ScriptableLabeledButton : ScriptableUIControl<LabeledButton>
    {
    	[Token(Token = "0x170001C6")]
    	public override bool Interactable
    	{
    		[Token(Token = "0x6000CDB")]
    		[Address(RVA = "0x53F370", Offset = "0x53DD70", VA = "0x18053F370", Slot = "31")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x14000043")]
    	public event Action OnButtonClicked
    	{
    		[Token(Token = "0x6000CD9")]
    		[Address(RVA = "0x53F170", Offset = "0x53DB70", VA = "0x18053F170")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000CDA")]
    		[Address(RVA = "0x53F270", Offset = "0x53DC70", VA = "0x18053F270")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x6000CDC")]
    	[Address(RVA = "0x53F490", Offset = "0x53DE90", VA = "0x18053F490", Slot = "50")]
    	public override void SetInteractable(bool interactable)
    	{
    	}

    	[Token(Token = "0x6000CDD")]
    	[Address(RVA = "0x53F5D0", Offset = "0x53DFD0", VA = "0x18053F5D0", Slot = "60")]
    	protected override void BindUIEvents()
    	{
    	}

    	[Token(Token = "0x6000CDE")]
    	[Address(RVA = "0x53F6F0", Offset = "0x53E0F0", VA = "0x18053F6F0", Slot = "61")]
    	protected override void UnbindUIEvents()
    	{
    	}

    	[Token(Token = "0x6000CDF")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "62")]
    	protected virtual void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x6000CE0")]
    	[Address(RVA = "0x53E490", Offset = "0x53CE90", VA = "0x18053E490")]
    	private void InvokeOnButtonClicked()
    	{
    	}

    	[Token(Token = "0x6000CE1")]
    	[Address(RVA = "0x53F870", Offset = "0x53E270", VA = "0x18053F870")]
    	public ScriptableLabeledButton()
    	{
    	}
    }
}
