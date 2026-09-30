using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Naninovel
{
    [Token(Token = "0x2000247")]
    public class AddressableFolderLocator : LocateFoldersRunner
    {
    	[Token(Token = "0x40008F2")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly List<IResourceLocation> locations;

    	[Token(Token = "0x6000A1F")]
    	[Address(RVA = "0x52E400", Offset = "0x52CE00", VA = "0x18052E400")]
    	public AddressableFolderLocator(AddressableResourceProvider provider, string resourcePath, List<IResourceLocation> locations)
    	{
    	}

    	[Token(Token = "0x6000A20")]
    	[Address(RVA = "0x52E470", Offset = "0x52CE70", VA = "0x18052E470", Slot = "4")]
    	public override UniTask Run()
    	{
    		return default(UniTask);
    	}
    }
}
