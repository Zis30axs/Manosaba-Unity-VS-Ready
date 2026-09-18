using System;
using System.Runtime.InteropServices;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine.InputSystem;

namespace GigaCreation.Essentials.Input
{
    [Token(Token = "0x2000004")]
    public interface IInputManager<out TControls> : IInputManager, IService, IDisposable where TControls : class
    {
    	[Token(Token = "0x17000001")]
    	TControls Controls
    	{
    		[Token(Token = "0x6000003")]
    		get;
    	}
    }
    [Token(Token = "0x2000005")]
    public interface IInputManager : IService, IDisposable
    {
    	[Token(Token = "0x6000004")]
    	void PlayResponseSfx(ResponseKind kind);

    	[Token(Token = "0x6000005")]
    	InputAction GetAction(string actionName, [Optional] string actionMapName);

    	[Token(Token = "0x6000006")]
    	bool TryGetAction(string actionName, out InputAction action, [Optional] string actionMapName);
    }
}
