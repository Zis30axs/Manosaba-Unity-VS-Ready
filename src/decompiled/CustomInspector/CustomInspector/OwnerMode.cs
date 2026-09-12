using Il2CppDummyDll;

namespace CustomInspector;

[Token(Token = "0x2000035")]
public enum OwnerMode
{
	[Token(Token = "0x4000105")]
	Self,
	[Token(Token = "0x4000106")]
	Children,
	[Token(Token = "0x4000107")]
	Parent,
	[Token(Token = "0x4000108")]
	Root,
	[Token(Token = "0x4000109")]
	Parents,
	[Token(Token = "0x400010A")]
	DirectChildren
}
