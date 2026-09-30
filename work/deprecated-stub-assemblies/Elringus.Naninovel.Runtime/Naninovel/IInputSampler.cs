using System;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000364")]
    public interface IInputSampler
    {
    	[Token(Token = "0x1700022E")]
    	InputBinding Binding
    	{
    		[Token(Token = "0x6001054")]
    		get;
    	}

    	[Token(Token = "0x1700022F")]
    	bool Enabled
    	{
    		[Token(Token = "0x6001055")]
    		get;
    		[Token(Token = "0x6001056")]
    		set;
    	}

    	[Token(Token = "0x17000230")]
    	bool Active
    	{
    		[Token(Token = "0x6001057")]
    		get;
    	}

    	[Token(Token = "0x17000231")]
    	float Value
    	{
    		[Token(Token = "0x6001058")]
    		get;
    	}

    	[Token(Token = "0x17000232")]
    	bool StartedDuringFrame
    	{
    		[Token(Token = "0x6001059")]
    		get;
    	}

    	[Token(Token = "0x17000233")]
    	bool EndedDuringFrame
    	{
    		[Token(Token = "0x600105A")]
    		get;
    	}

    	[Token(Token = "0x14000054")]
    	event Action OnStart;

    	[Token(Token = "0x14000055")]
    	event Action OnEnd;

    	[Token(Token = "0x14000056")]
    	event Action<float> OnChange;

    	[Token(Token = "0x600105B")]
    	void Activate(float value);

    	[Token(Token = "0x600105C")]
    	void AddObjectTrigger(GameObject obj);

    	[Token(Token = "0x600105D")]
    	void RemoveObjectTrigger(GameObject obj);

    	[Token(Token = "0x600105E")]
    	CancellationToken GetNext();

    	[Token(Token = "0x600105F")]
    	CancellationToken InterceptNext([Optional] CancellationToken token);
    }
}
