using System;
using Il2CppDummyDll;
using Naninovel;

namespace WitchTrials.Models
{
    [Token(Token = "0x2000174")]
    public interface IWitchTrialsChoiceHandlerManager : IEngineService<WitchTrialsChoiceHandlersConfiguration>, IEngineService
    {
    	[Token(Token = "0x17000108")]
    	bool CriticalChoiceHintEnabled
    	{
    		[Token(Token = "0x6000640")]
    		get;
    		[Token(Token = "0x6000641")]
    		set;
    	}

    	[Token(Token = "0x14000001")]
    	event Action<bool> OnCriticalChoiceHintChanged;
    }
}
