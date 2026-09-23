using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GigaCreation.Essentials.Input
{
    [Token(Token = "0x2000006")]
    public class InputChecker : MonoBehaviour
    {
    	[Token(Token = "0x4000006")]
    	[FieldOffset(Offset = "0x20")]
    	[Header("Assets")]
    	[SerializeField]
    	private InputActionAsset _actionAsset;

    	[Token(Token = "0x4000007")]
    	[FieldOffset(Offset = "0x28")]
    	[Header("Parameters")]
    	[SerializeField]
    	private bool _enable;

    	[Token(Token = "0x4000008")]
    	[FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private string _actionMapName;

    	[Token(Token = "0x4000009")]
    	[FieldOffset(Offset = "0x38")]
    	private InputActionMap _actionMap;

    	[Token(Token = "0x17000002")]
    	private InputActionMap ActionMap
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCCFB00 -- lazy: _actionMap (+0x38), else _actionAsset (+0x20)
    		//   .FindActionMap(_actionMapName (+0x30), false)
    		// Annotated disassembly: work/title-path/disas/ic_0xCCFB00.txt
    		[Token(Token = "0x6000007")]
    		[Address(RVA = "0xCCFB00", Offset = "0xCCE500", VA = "0x180CCFB00")]
    		get
    		{
    			if (_actionMap == null)
    			{
    				_actionMap = _actionAsset.FindActionMap(_actionMapName);
    			}
    			return _actionMap;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCCFB80, 698 B -- only with _enable (+0x28): for every action of ActionMap,
    	//   performed (m_OnPerformed +0x170) += LogInputNamePerformed and canceled (m_OnCanceled +0x120) += LogInputNameCanceled
    	// Annotated disassembly: work/title-path/disas/ic_0xCCFB80.txt
    	[Token(Token = "0x6000008")]
    	[Address(RVA = "0xCCFB80", Offset = "0xCCE580", VA = "0x180CCFB80")]
    	private void Start()
    	{
    		if (!_enable)
    		{
    			return;
    		}
    		foreach (InputAction item in ActionMap)
    		{
    			item.performed += LogInputNamePerformed;
    			item.canceled += LogInputNameCanceled;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCCFE40, 698 B -- Start in reverse (-= on both events)
    	// Annotated disassembly: work/title-path/disas/ic_0xCCFE40.txt
    	[Token(Token = "0x6000009")]
    	[Address(RVA = "0xCCFE40", Offset = "0xCCE840", VA = "0x180CCFE40")]
    	private void OnDestroy()
    	{
    		if (!_enable)
    		{
    			return;
    		}
    		foreach (InputAction item in ActionMap)
    		{
    			item.performed -= LogInputNamePerformed;
    			item.canceled -= LogInputNameCanceled;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD0100, 340 B -- Debug.Log(string.Concat(string[5]) of
    	//   "[UnityEngine.InputSystem.InputAction] '", _actionMapName, "/", context.action.name, "' performed.")
    	// Annotated disassembly: work/title-path/disas/ic_0xCD0100.txt
    	[Token(Token = "0x600000A")]
    	[Address(RVA = "0xCD0100", Offset = "0xCCEB00", VA = "0x180CD0100")]
    	private void LogInputNamePerformed(InputAction.CallbackContext context)
    	{
    		Debug.Log("[UnityEngine.InputSystem.InputAction] '" + _actionMapName + "/" + context.action.name + "' performed.");
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD0260, 340 B -- as LogInputNamePerformed with "' canceled."
    	// Annotated disassembly: work/title-path/disas/ic_0xCD0260.txt
    	[Token(Token = "0x600000B")]
    	[Address(RVA = "0xCD0260", Offset = "0xCCEC60", VA = "0x180CD0260")]
    	private void LogInputNameCanceled(InputAction.CallbackContext context)
    	{
    		Debug.Log("[UnityEngine.InputSystem.InputAction] '" + _actionMapName + "/" + context.action.name + "' canceled.");
    	}

    	[Token(Token = "0x600000C")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public InputChecker()
    	{
    	}
    }
}
