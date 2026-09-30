using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x200059A")]
    public interface IInputIndicator
    {
    	[Token(Token = "0x17000401")]
    	bool Visible
    	{
    		[Token(Token = "0x6001BD6")]
    		get;
    	}

    	[Token(Token = "0x17000402")]
    	RectTransform RectTransform
    	{
    		[Token(Token = "0x6001BD7")]
    		get;
    	}

    	[Token(Token = "0x6001BD8")]
    	void Show();

    	[Token(Token = "0x6001BD9")]
    	void Hide();
    }
}
