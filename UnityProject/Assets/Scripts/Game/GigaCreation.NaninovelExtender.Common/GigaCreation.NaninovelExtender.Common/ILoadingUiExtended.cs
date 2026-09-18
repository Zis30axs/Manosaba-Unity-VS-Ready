using Il2CppDummyDll;
using Naninovel.UI;

namespace GigaCreation.NaninovelExtender.Common
{
    [Token(Token = "0x200001A")]
    public interface ILoadingUiExtended : ILoadingUI, IManagedUI
    {
    	[Token(Token = "0x17000005")]
    	LoadingMode CurrentMode
    	{
    		[Token(Token = "0x600003A")]
    		set;
    	}
    }
}
