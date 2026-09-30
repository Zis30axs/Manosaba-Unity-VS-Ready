using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x200025E")]
    public class ProjectFolderLocator : LocateFoldersRunner
    {
    	[Token(Token = "0x4000935")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly IReadOnlyDictionary<string, Type> projectResources;

    	[Token(Token = "0x6000A98")]
    	[Address(RVA = "0x531E40", Offset = "0x530840", VA = "0x180531E40")]
    	public ProjectFolderLocator(IResourceProvider provider, string resourcesPath, IReadOnlyDictionary<string, Type> projectResources)
    	{
    	}

    	[Token(Token = "0x6000A99")]
    	[Address(RVA = "0x531ED0", Offset = "0x5308D0", VA = "0x180531ED0", Slot = "4")]
    	public override UniTask Run()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000A9A")]
    	[Address(RVA = "0x531F50", Offset = "0x530950", VA = "0x180531F50")]
    	public static IReadOnlyCollection<Folder> LocateProjectFolders(string resourcesPath, IReadOnlyDictionary<string, Type> projectResources)
    	{
    		return null;
    	}
    }
}
