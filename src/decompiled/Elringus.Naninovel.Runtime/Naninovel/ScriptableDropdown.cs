using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using TMPro;

namespace Naninovel;

[Token(Token = "0x20002B2")]
public class ScriptableDropdown : ScriptableUIControl<TMP_Dropdown>
{
	[Token(Token = "0x14000042")]
	public event Action<int> OnDropdownValueChanged
	{
		[Token(Token = "0x6000CA4")]
		[Address(RVA = "0x53E4B0", Offset = "0x53CEB0", VA = "0x18053E4B0")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000CA5")]
		[Address(RVA = "0x53E5D0", Offset = "0x53CFD0", VA = "0x18053E5D0")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6000CA6")]
	[Address(RVA = "0x53E6F0", Offset = "0x53D0F0", VA = "0x18053E6F0", Slot = "60")]
	protected override void BindUIEvents()
	{
	}

	[Token(Token = "0x6000CA7")]
	[Address(RVA = "0x53E820", Offset = "0x53D220", VA = "0x18053E820", Slot = "61")]
	protected override void UnbindUIEvents()
	{
	}

	[Token(Token = "0x6000CA8")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "62")]
	protected virtual void OnValueChanged(int value)
	{
	}

	[Token(Token = "0x6000CA9")]
	[Address(RVA = "0x53E9B0", Offset = "0x53D3B0", VA = "0x18053E9B0")]
	private void InvokeOnDropdownValueChanged(int value)
	{
	}

	[Token(Token = "0x6000CAA")]
	[Address(RVA = "0x53E9E0", Offset = "0x53D3E0", VA = "0x18053E9E0")]
	public ScriptableDropdown()
	{
	}
}
