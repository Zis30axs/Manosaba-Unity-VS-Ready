using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000257")]
    public class LocalFolderLocator : LocateFoldersRunner
    {
    	[Token(Token = "0x17000181")]
    	public virtual string RootPath
    	{
    		[Token(Token = "0x6000A7C")]
    		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0", Slot = "7")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000A7D")]
    	[Address(RVA = "0x530AC0", Offset = "0x52F4C0", VA = "0x180530AC0")]
    	public LocalFolderLocator(IResourceProvider provider, string rootPath, string resourcesPath)
    	{
    	}

    	[Token(Token = "0x6000A7E")]
    	[Address(RVA = "0x530B30", Offset = "0x52F530", VA = "0x180530B30", Slot = "4")]
    	public override UniTask Run()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000A7F")]
    	[Address(RVA = "0x530BC0", Offset = "0x52F5C0", VA = "0x180530BC0")]
    	public static IReadOnlyCollection<Folder> LocateFoldersAtPath(string rootPath, string resourcesPath)
    	{
    		return null;
    	}
    }
}
