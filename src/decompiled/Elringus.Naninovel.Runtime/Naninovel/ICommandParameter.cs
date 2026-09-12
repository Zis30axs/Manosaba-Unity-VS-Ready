using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20003F1")]
public interface ICommandParameter
{
	[Token(Token = "0x17000298")]
	bool HasValue
	{
		[Token(Token = "0x6001300")]
		get;
	}

	[Token(Token = "0x17000299")]
	bool DynamicValue
	{
		[Token(Token = "0x6001301")]
		get;
	}

	[Token(Token = "0x1700029A")]
	RawValue? RawValue
	{
		[Token(Token = "0x6001302")]
		get;
	}

	[Token(Token = "0x1700029B")]
	PlaybackSpot? PlaybackSpot
	{
		[Token(Token = "0x6001303")]
		get;
	}

	[Token(Token = "0x6001304")]
	void AssignRaw(RawValue? raw, PlaybackSpot? spot, out string errors);
}
