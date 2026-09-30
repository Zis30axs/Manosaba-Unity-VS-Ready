using System;
using Il2CppDummyDll;
using Naninovel.UI;

namespace Naninovel
{
    [Token(Token = "0x2000363")]
    public interface IInputManager : IEngineService<InputConfiguration>, IEngineService
    {
    	[Token(Token = "0x1700022C")]
    	bool ProcessInput
    	{
    		[Token(Token = "0x6001046")]
    		get;
    		[Token(Token = "0x6001047")]
    		set;
    	}

    	[Token(Token = "0x1700022D")]
    	InputMode InputMode
    	{
    		[Token(Token = "0x6001048")]
    		get;
    		[Token(Token = "0x6001049")]
    		set;
    	}

    	[Token(Token = "0x14000053")]
    	event Action<InputMode> OnInputModeChanged;

    	[Token(Token = "0x600104A")]
    	IInputSampler GetSampler(string bindingName);

    	[Token(Token = "0x600104B")]
    	void AddBlockingUI(IManagedUI ui, params string[] allowedSamplers);

    	[Token(Token = "0x600104C")]
    	void RemoveBlockingUI(IManagedUI ui);

    	[Token(Token = "0x600104D")]
    	bool IsSampling(string bindingName);
    }
}
