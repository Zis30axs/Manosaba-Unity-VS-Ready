using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine.UI;

namespace Naninovel
{
    [Token(Token = "0x20002BC")]
    public class ScriptableSlider : ScriptableUIControl<Slider>
    {
    	[Token(Token = "0x14000045")]
    	public event Action<float> OnSliderValueChanged
    	{
    		[Token(Token = "0x6000CE9")]
    		[Address(RVA = "0x53FE20", Offset = "0x53E820", VA = "0x18053FE20")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000CEA")]
    		[Address(RVA = "0x53FF40", Offset = "0x53E940", VA = "0x18053FF40")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x6000CEB")]
    	[Address(RVA = "0x540060", Offset = "0x53EA60", VA = "0x180540060", Slot = "60")]
    	protected override void BindUIEvents()
    	{
    	}

    	[Token(Token = "0x6000CEC")]
    	[Address(RVA = "0x540190", Offset = "0x53EB90", VA = "0x180540190", Slot = "61")]
    	protected override void UnbindUIEvents()
    	{
    	}

    	[Token(Token = "0x6000CED")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "62")]
    	protected virtual void OnValueChanged(float value)
    	{
    	}

    	[Token(Token = "0x6000CEE")]
    	[Address(RVA = "0x540320", Offset = "0x53ED20", VA = "0x180540320")]
    	private void InvokeOnSliderValueChanged(float value)
    	{
    	}

    	[Token(Token = "0x6000CEF")]
    	[Address(RVA = "0x540350", Offset = "0x53ED50", VA = "0x180540350")]
    	public ScriptableSlider()
    	{
    	}
    }
}
