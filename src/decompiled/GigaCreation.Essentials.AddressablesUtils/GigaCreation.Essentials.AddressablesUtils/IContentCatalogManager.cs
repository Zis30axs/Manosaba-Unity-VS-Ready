using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.AddressablesUtils;

[Token(Token = "0x2000024")]
public interface IContentCatalogManager
{
	[Token(Token = "0x1700000F")]
	bool IsAdditionalCatalogLoaded
	{
		[Token(Token = "0x6000062")]
		get;
	}

	[Token(Token = "0x6000063")]
	UniTask<bool> CheckIfMainCatalogExistsAsync([Optional] CancellationToken ct);

	[Token(Token = "0x6000064")]
	UniTask<bool> CheckIfAdditionalCatalogExistsAsync([Optional] CancellationToken ct);

	[Token(Token = "0x6000065")]
	UniTask<bool> LoadAdditionalCatalogAsync([Optional] CancellationToken ct);
}
