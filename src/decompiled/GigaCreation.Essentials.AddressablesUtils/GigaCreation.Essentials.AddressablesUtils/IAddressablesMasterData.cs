using System.Collections.Generic;
using GigaCreation.Essentials.MasterData;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.AddressablesUtils;

[Token(Token = "0x2000023")]
public interface IAddressablesMasterData : IMasterDataBase
{
	[Token(Token = "0x1700000D")]
	IEnumerable<string> AssetKeyNames
	{
		[Token(Token = "0x6000060")]
		get;
	}

	[Token(Token = "0x1700000E")]
	IReadOnlyList<AdditionalCatalogData> AdditionalCatalogDataList
	{
		[Token(Token = "0x6000061")]
		get;
	}
}
