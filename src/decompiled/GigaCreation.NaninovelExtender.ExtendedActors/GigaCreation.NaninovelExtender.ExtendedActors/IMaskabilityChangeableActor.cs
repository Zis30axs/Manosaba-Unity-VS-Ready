using Il2CppDummyDll;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x200001C")]
public interface IMaskabilityChangeableActor
{
	[Token(Token = "0x200001D")]
	public readonly struct ShaderPropertyIds
	{
		[Token(Token = "0x4000059")]
		[FieldOffset(Offset = "0x0")]
		public static readonly int StencilComp;
	}

	[Token(Token = "0x17000005")]
	bool Masked
	{
		[Token(Token = "0x6000048")]
		get;
		[Token(Token = "0x6000049")]
		set;
	}

	[Token(Token = "0x600004A")]
	void ChangeMaskability(bool enable);
}
