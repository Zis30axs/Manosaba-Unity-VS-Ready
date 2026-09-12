using GigaCreation.Essentials.MasterData;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.SaveLoad;

[Token(Token = "0x2000005")]
public interface ISaveLoadMasterData : IMasterDataBase
{
	[Token(Token = "0x17000001")]
	string SaveSystemIdentifier
	{
		[Token(Token = "0x6000005")]
		get;
	}
}
