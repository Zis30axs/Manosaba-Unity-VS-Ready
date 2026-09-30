#nullable enable annotations
﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000334")]
    public interface ICustomVariableManager : IEngineService<CustomVariablesConfiguration>, IEngineService
    {
    	[Token(Token = "0x1700020D")]
    	IReadOnlyCollection<CustomVariable> Variables
    	{
    		[Token(Token = "0x6000F4F")]
    		get;
    	}

    	[Token(Token = "0x14000048")]
    	event Action<CustomVariableUpdatedArgs> OnVariableUpdated;

    	[Token(Token = "0x6000F50")]
    	bool VariableExists(string name);

    	[Token(Token = "0x6000F51")]
    	CustomVariableValue GetVariableValue(string name);

    	[Token(Token = "0x6000F52")]
    	void SetVariableValue(string name, CustomVariableValue value);

    	[Token(Token = "0x6000F53")]
    	void ResetVariable(string name);

    	[Token(Token = "0x6000F54")]
    	void ResetAllVariables([Optional] CustomVariableScope? scope);
    }
}
