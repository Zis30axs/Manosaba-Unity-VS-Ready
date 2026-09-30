using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Naninovel
{
    [Token(Token = "0x2000374")]
    public class InputSampler : IInputSampler
    {
    	[Token(Token = "0x4000CE0")]
    	[FieldOffset(Offset = "0x38")]
    	private readonly IInputManager inputManager;

    	[Token(Token = "0x4000CE1")]
    	[FieldOffset(Offset = "0x40")]
    	private readonly InputConfiguration config;

    	[Token(Token = "0x4000CE2")]
    	[FieldOffset(Offset = "0x48")]
    	private readonly HashSet<GameObject> objectTriggers;

    	[Token(Token = "0x4000CE3")]
    	[FieldOffset(Offset = "0x50")]
    	private readonly Stack<InputInterceptRequest> intercepts;

    	[Token(Token = "0x4000CE4")]
    	[FieldOffset(Offset = "0x58")]
    	private CancellationTokenSource onNextCTS;

    	[Token(Token = "0x4000CE5")]
    	[FieldOffset(Offset = "0x60")]
    	private int lastActiveFrame;

    	[Token(Token = "0x4000CE6")]
    	[FieldOffset(Offset = "0x64")]
    	private float lastTouchTime;

    	[Token(Token = "0x4000CE7")]
    	[FieldOffset(Offset = "0x68")]
    	private bool readyForNextTap;

    	[Token(Token = "0x4000CE8")]
    	[FieldOffset(Offset = "0x6C")]
    	private Vector2 lastTouchBeganPosition;

    	[Token(Token = "0x4000CE9")]
    	[FieldOffset(Offset = "0x78")]
    	private InputAction inputAction;

    	[Token(Token = "0x17000237")]
    	public virtual InputBinding Binding
    	{
    		[Token(Token = "0x60010A8")]
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060", Slot = "22")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000238")]
    	public virtual bool Enabled
    	{
    		[Token(Token = "0x60010A9")]
    		[Address(RVA = "0x43E270", Offset = "0x43CC70", VA = "0x18043E270", Slot = "23")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x60010AA")]
    		[Address(RVA = "0x43E280", Offset = "0x43CC80", VA = "0x18043E280", Slot = "24")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000239")]
    	public virtual bool Active
    	{
    		[Token(Token = "0x60010AB")]
    		[Address(RVA = "0x5799D0", Offset = "0x5783D0", VA = "0x1805799D0", Slot = "25")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700023A")]
    	public virtual float Value
    	{
    		[Token(Token = "0x60010AC")]
    		[Address(RVA = "0x4C0D50", Offset = "0x4BF750", VA = "0x1804C0D50", Slot = "26")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x60010AD")]
    		[Address(RVA = "0x4C0D60", Offset = "0x4BF760", VA = "0x1804C0D60")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700023B")]
    	public virtual bool StartedDuringFrame
    	{
    		[Token(Token = "0x60010AE")]
    		[Address(RVA = "0x579A00", Offset = "0x578400", VA = "0x180579A00", Slot = "27")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700023C")]
    	public virtual bool EndedDuringFrame
    	{
    		[Token(Token = "0x60010AF")]
    		[Address(RVA = "0x579AA0", Offset = "0x5784A0", VA = "0x180579AA0", Slot = "28")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x14000058")]
    	public event Action OnStart
    	{
    		[Token(Token = "0x60010A2")]
    		[Address(RVA = "0x5793F0", Offset = "0x577DF0", VA = "0x1805793F0", Slot = "4")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60010A3")]
    		[Address(RVA = "0x5794E0", Offset = "0x577EE0", VA = "0x1805794E0", Slot = "5")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000059")]
    	public event Action OnEnd
    	{
    		[Token(Token = "0x60010A4")]
    		[Address(RVA = "0x5795D0", Offset = "0x577FD0", VA = "0x1805795D0", Slot = "6")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60010A5")]
    		[Address(RVA = "0x5796C0", Offset = "0x5780C0", VA = "0x1805796C0", Slot = "7")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x1400005A")]
    	public event Action<float> OnChange
    	{
    		[Token(Token = "0x60010A6")]
    		[Address(RVA = "0x5797B0", Offset = "0x5781B0", VA = "0x1805797B0", Slot = "8")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60010A7")]
    		[Address(RVA = "0x5798C0", Offset = "0x5782C0", VA = "0x1805798C0", Slot = "9")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x60010B0")]
    	[Address(RVA = "0x579B50", Offset = "0x578550", VA = "0x180579B50")]
    	public InputSampler(InputConfiguration config, InputBinding binding, IEnumerable<GameObject> objectTriggers, IInputManager inputManager)
    	{
    	}

    	[Token(Token = "0x60010B1")]
    	[Address(RVA = "0x579E20", Offset = "0x578820", VA = "0x180579E20", Slot = "29")]
    	public virtual void AddObjectTrigger(GameObject obj)
    	{
    	}

    	[Token(Token = "0x60010B2")]
    	[Address(RVA = "0x579E80", Offset = "0x578880", VA = "0x180579E80", Slot = "30")]
    	public virtual void RemoveObjectTrigger(GameObject obj)
    	{
    	}

    	[Token(Token = "0x60010B3")]
    	[Address(RVA = "0x579EE0", Offset = "0x5788E0", VA = "0x180579EE0", Slot = "31")]
    	public virtual CancellationToken GetNext()
    	{
    		return default(CancellationToken);
    	}

    	[Token(Token = "0x60010B4")]
    	[Address(RVA = "0x579FA0", Offset = "0x5789A0", VA = "0x180579FA0", Slot = "32")]
    	public virtual CancellationToken InterceptNext(CancellationToken token)
    	{
    		return default(CancellationToken);
    	}

    	[Token(Token = "0x60010B5")]
    	[Address(RVA = "0x57A110", Offset = "0x578B10", VA = "0x18057A110", Slot = "33")]
    	public virtual void Activate(float value)
    	{
    	}

    	[Token(Token = "0x60010B6")]
    	[Address(RVA = "0x57A130", Offset = "0x578B30", VA = "0x18057A130", Slot = "34")]
    	public virtual void SampleInput()
    	{
    	}

    	[Token(Token = "0x60010B7")]
    	[Address(RVA = "0x57A2B0", Offset = "0x578CB0", VA = "0x18057A2B0", Slot = "35")]
    	protected virtual void SampleObjectTriggers()
    	{
    	}

    	[Token(Token = "0x60010B8")]
    	[Address(RVA = "0x57A660", Offset = "0x579060", VA = "0x18057A660")]
    	protected void InitializeInputAction()
    	{
    	}

    	[Token(Token = "0x60010B9")]
    	[Address(RVA = "0x57A9B0", Offset = "0x5793B0", VA = "0x18057A9B0", Slot = "36")]
    	protected virtual void SetInputValue(float value)
    	{
    	}

    	[Token(Token = "0x60010BA")]
    	[Address(RVA = "0x57AC10", Offset = "0x579610", VA = "0x18057AC10", Slot = "37")]
    	protected virtual bool HandleInterceptRequest(InputInterceptRequest request)
    	{
    		return default(bool);
    	}
    }
}
