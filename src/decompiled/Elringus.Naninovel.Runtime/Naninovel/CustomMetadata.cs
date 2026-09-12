using System;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000054")]
public abstract class CustomMetadata<TActor> : CustomMetadata where TActor : IActor
{
	[Token(Token = "0x6000141")]
	protected CustomMetadata()
	{
	}
}
[Serializable]
[Token(Token = "0x2000055")]
public abstract class CustomMetadata
{
	[Token(Token = "0x6000142")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	internal CustomMetadata()
	{
	}
}
