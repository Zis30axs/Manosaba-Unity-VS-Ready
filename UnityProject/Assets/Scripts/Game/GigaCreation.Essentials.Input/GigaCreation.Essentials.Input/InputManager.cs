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
    	private readonly TControls _controls = new TControls();

    	[Token(Token = "0x17000003")]
    	public TControls Controls
    	{
    		[Token(Token = "0x600000D")]
    		[CompilerGenerated]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x48E560 -- auto-property backing field.
    		get
    		{
    			return _controls;
    		}
    	}

    	[Token(Token = "0x600000E")]
    	public InputManager()
    	{
    		// RECONSTRUCTED | Confidence: B | RVA: 0x831E30 -- `_actions = new Dictionary<string, InputAction>()`,
    		//   `Controls = new TControls()` (Activator.CreateInstance), then one interface call with no arguments on the
    		//   controls cast to IInputActionCollection (RGCTX call) = Enable(). Annotated disassembly: work/title-path/disas/imgr_*.txt (regenerate with work/reversing/disas.py).
    		_actions = new Dictionary<string, InputAction>();
    		((IInputActionCollection)_controls).Enable();
    	}

    	[Token(Token = "0x600000F")]
    	public void Dispose()
    	{
    		// RECONSTRUCTED | Confidence: B | RVA: 0x8320B0 -- the same cast-and-call shape as the ctor with a single
    		//   no-argument call: the controls' Dispose (the generated wrapper destroys the asset it built).
    		(_controls as IDisposable)?.Dispose();
    	}

    	[Token(Token = "0x6000010")]
    	public InputAction GetAction(string actionName, [Optional] string actionMapName)
    	{
    		// RECONSTRUCTED | Confidence: B | RVA: 0x832210 -- key = BuildActionNameOrId (virtual slot 10); cache hit via the
    		//   IDictionary lookup; on a miss IInputActionCollection2.FindAction(key) and the result is cached.
    		string key = BuildActionNameOrId(actionName, actionMapName);
    		if (!_actions.TryGetValue(key, out InputAction action))
    		{
    			action = ((IInputActionCollection2)_controls).FindAction(key, true);
    			_actions[key] = action;
    		}
    		return action;
    	}

    	[Token(Token = "0x6000011")]
    	public bool TryGetAction(string actionName, out InputAction action, [Optional] string actionMapName)
    	{
    		// RECONSTRUCTED | Confidence: B | RVA: 0x8324E0 -- same as GetAction but a miss that FindAction cannot resolve
    		//   returns false (nothing cached).
    		string key = BuildActionNameOrId(actionName, actionMapName);
    		if (_actions.TryGetValue(key, out action))
    		{
    			return true;
    		}
    		action = ((IInputActionCollection2)_controls).FindAction(key, false);
    		if (action == null)
    		{
    			return false;
    		}
    		_actions[key] = action;
    		return true;
    	}

    	[Token(Token = "0x6000012")]
    	public virtual void PlayResponseSfx(ResponseKind kind)
    	{
    	}

    	[Token(Token = "0x6000013")]
    	protected virtual string BuildActionNameOrId(string actionName, [Optional] string actionMapName)
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x832780 -- null/whitespace-only map name -> actionName, else map + "/" + action.
    		return string.IsNullOrWhiteSpace(actionMapName) ? actionName : actionMapName + "/" + actionName;
    	}
    }
}
