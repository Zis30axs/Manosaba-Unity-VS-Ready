using System;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.AddressablesUtils;

[Token(Token = "0x2000022")]
public interface IAddressablesManagerRemoteSupported : IAddressablesManager, IContentCatalogManager, IAddressableResourceLocationProvider, IAddressableAssetLoader, IService, IDisposable, IAddressablesDownloader, IAddressablesCacheCleaner
{
}
