using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000329")]
    [EditInProjectSettings]
    public class CustomVariablesConfiguration : Configuration
    {
    	[Token(Token = "0x4000BDC")]
    	[FieldOffset(Offset = "0x18")]
    	[Tooltip("The list of variables to initialize by default. Global variables (names starting with `g_`) are initialized on first application start, and others on each state reset.")]
    	public List<CustomVariablePredefine> PredefinedVariables;

    	[Token(Token = "0x6000F29")]
    	[Address(RVA = "0x562810", Offset = "0x561210", VA = "0x180562810")]
    	public static bool HasGlobalPrefix(string name)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000F2A")]
    	[Address(RVA = "0x562970", Offset = "0x561370", VA = "0x180562970")]
    	public CustomVariablesConfiguration()
    	{
    	}
    }
}
