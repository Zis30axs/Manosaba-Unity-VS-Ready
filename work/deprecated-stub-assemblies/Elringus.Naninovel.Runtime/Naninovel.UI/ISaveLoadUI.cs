using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x2000570")]
    public interface ISaveLoadUI : IManagedUI
    {
    	[Token(Token = "0x170003D2")]
    	SaveLoadUIPresentationMode PresentationMode
    	{
    		[Token(Token = "0x6001AF9")]
    		get;
    		[Token(Token = "0x6001AFA")]
    		set;
    	}

    	[Token(Token = "0x6001AFB")]
    	SaveLoadUIPresentationMode GetLastLoadMode();
    }
}
