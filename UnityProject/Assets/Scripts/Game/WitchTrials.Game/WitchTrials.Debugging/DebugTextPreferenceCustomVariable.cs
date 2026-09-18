using GigaCreation.Tools.Debugging.TextDisplays;
using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace WitchTrials.Debugging
{
    [Token(Token = "0x20001C6")]
    public class DebugTextPreferenceCustomVariable : DebugTextPreferenceBase
    {
    	[Token(Token = "0x4000863")]
    	[FieldOffset(Offset = "0x50")]
    	[Space]
    	[SerializeField]
    	private string _variableName;

    	[Token(Token = "0x4000864")]
    	[FieldOffset(Offset = "0x58")]
    	private ICustomVariableManager _naniVariableManager;

    	[Token(Token = "0x17000149")]
    	private ICustomVariableManager NaniVariableManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3B3F80
    		// Lazy cache in +0x58 via `Engine.GetServiceOrErr<ICustomVariableManager>()`.
    		[Token(Token = "0x60007B8")]
    		[Address(RVA = "0x3B3F80", Offset = "0x3B2980", VA = "0x1803B3F80")]
    		get
    		{
    			return _naniVariableManager ??= Engine.GetServiceOrErr<ICustomVariableManager>();
    		}
    	}

    	[Token(Token = "0x1700014A")]
    	protected override string LabelText
    	{
    		// RECONSTRUCTED | Confidence: A (switch spelling: B) | RVA: 0x3B4040, 523 B
    		// - Unlike its siblings, a non-initialized engine returns '-' (the same return block as the default
    		//   case), not 'Naninovel has not been initialized'.
    		// - Uses the non-generic `TryGetVariableValue(name, out CustomVariableValue)` (0x562670) with an
    		//   inlined getter. The struct layout is Type +0x0, String +0x8, Number +0x10, Boolean +0x14.
    		// - Failure -> `name + ": -"`.
    		// - String -> three-string Concat; Numeric and Boolean -> boxed `string.Format("{0}: {1}")`.
    		[Token(Token = "0x60007B9")]
    		[Address(RVA = "0x3B4040", Offset = "0x3B2A40", VA = "0x1803B4040", Slot = "4")]
    		get
    		{
    			if (!Engine.Initialized)
    			{
    				return "-";
    			}
    			if (!NaniVariableManager.TryGetVariableValue(_variableName, out CustomVariableValue value))
    			{
    				return _variableName + ": -";
    			}
    			switch (value.Type)
    			{
    			case CustomVariableValueType.String:
    				return _variableName + ": " + value.String;
    			case CustomVariableValueType.Numeric:
    				return string.Format("{0}: {1}", _variableName, value.Number);
    			case CustomVariableValueType.Boolean:
    				return string.Format("{0}: {1}", _variableName, value.Boolean);
    			default:
    				return "-";
    			}
    		}
    	}

    	[Token(Token = "0x60007BA")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DebugTextPreferenceCustomVariable()
    	{
    	}
    }
}
