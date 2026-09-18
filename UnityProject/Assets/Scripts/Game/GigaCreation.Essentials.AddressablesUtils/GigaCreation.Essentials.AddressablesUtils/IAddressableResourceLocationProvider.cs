using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace GigaCreation.Essentials.AddressablesUtils
{
    [Token(Token = "0x200001D")]
    public interface IAddressableResourceLocationProvider
    {
    	[Token(Token = "0x6000052")]
    	UniTask<List<IResourceLocation>> GetResourceLocationsAsync([Optional] CancellationToken ct);
    }
}
