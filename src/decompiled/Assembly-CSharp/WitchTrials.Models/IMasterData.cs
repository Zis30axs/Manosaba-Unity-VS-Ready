using GigaCreation.Essentials.AddressablesUtils;
using GigaCreation.Essentials.MasterData;
using Il2CppDummyDll;

namespace WitchTrials.Models;

[Token(Token = "0x2000170")]
public interface IMasterData : IAddressablesMasterData, IMasterDataBase
{
	[Token(Token = "0x170000FF")]
	float DefaultHp
	{
		[Token(Token = "0x6000635")]
		get;
	}

	[Token(Token = "0x17000100")]
	float SkipTimeScaleDuringDebate
	{
		[Token(Token = "0x6000636")]
		get;
	}

	[Token(Token = "0x17000101")]
	float VoicePitchOnSkip
	{
		[Token(Token = "0x6000637")]
		get;
	}
}
