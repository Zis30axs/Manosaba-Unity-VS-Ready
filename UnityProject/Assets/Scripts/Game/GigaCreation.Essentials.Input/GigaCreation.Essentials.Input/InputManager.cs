using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine.InputSystem;

namespace GigaCreation.Essentials.Input
{
    [Token(Token = "0x2000007")]
    public class InputManager<TControls> : IInputManager<TControls>, IInputManager, IService, IDisposable where TControls : class, new()
    {
    	[Token(Token = "0x400000A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly IDictionary<string, InputAction> _actions;

    	[Token(Token = "0x17000003")]
    	public TControls Controls
    	{
    		[Token(Token = "0x600000D")]
    		[CompilerGenerated]
    		get
    		{
    			return default(TControls);
    		}
    	}

    	[Token(Token = "0x600000E")]
    	public InputManager()
    	{
    	}

    	[Token(Token = "0x600000F")]
    	public void Dispose()
    	{
    	}

    	[Token(Token = "0x6000010")]
    	public InputAction GetAction(string actionName, [Optional] string actionMapName)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000011")]
    	public bool TryGetAction(string actionName, out InputAction action, [Optional] string actionMapName)
    	{
    		action = default;
    		action = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6000012")]
    	public virtual void PlayResponseSfx(ResponseKind kind)
    	{
    	}

    	[Token(Token = "0x6000013")]
    	protected virtual string BuildActionNameOrId(string actionName, [Optional] string actionMapName)
    	{
    		return null;
    	}
    }
}
