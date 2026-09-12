using Il2CppDummyDll;

namespace CustomInspector;

[Token(Token = "0x200000A")]
public enum InspectorButtonState
{
	[Token(Token = "0x40000AA")]
	isPressed,
	[Token(Token = "0x40000AB")]
	isSelected,
	[Token(Token = "0x40000AC")]
	notSelected
}
