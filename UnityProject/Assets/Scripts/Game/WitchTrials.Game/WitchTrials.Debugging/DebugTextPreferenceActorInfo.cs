using GigaCreation.Tools.Debugging.TextDisplays;
using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace WitchTrials.Debugging
{
    [Token(Token = "0x20001C4")]
    public class DebugTextPreferenceActorInfo : DebugTextPreferenceBase
    {
    	[Token(Token = "0x400085F")]
    	[FieldOffset(Offset = "0x50")]
    	[Space]
    	[SerializeField]
    	private string _actorId;

    	[Token(Token = "0x4000860")]
    	[FieldOffset(Offset = "0x58")]
    	private IActorManager _actorManager;

    	[Token(Token = "0x17000144")]
    	private IActorManager ActorManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3B35B0, 282 B; predicate <get_ActorManager>b__3_0 0x3B3AF0, 93 B
    		// Lazy cache in +0x58 via `Engine.FindService<IActorManager>(Predicate)`. The predicate is an
    		// instance lambda on `this` (no display class): IActorManager slot 6 `ActorExists(_actorId (+0x50))`.
    		[Token(Token = "0x60007AF")]
    		[Address(RVA = "0x3B35B0", Offset = "0x3B1FB0", VA = "0x1803B35B0")]
    		get
    		{
    			return _actorManager ??= Engine.FindService<IActorManager>((IActorManager manager) => manager.ActorExists(_actorId));
    		}
    	}

    	[Token(Token = "0x17000145")]
    	protected override string LabelText
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3B36D0, 566 B
    		// `Engine.Initialized` ? BuildText() (0x3B3910, inlined byte-for-byte) : 'Naninovel has not been initialized'.
    		[Token(Token = "0x60007B0")]
    		[Address(RVA = "0x3B36D0", Offset = "0x3B20D0", VA = "0x1803B36D0", Slot = "4")]
    		get
    		{
    			return Engine.Initialized ? BuildText() : "Naninovel 尚未初始化";
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3B3910, 464 B
    	// 
    	// The ActorManager getter is called three times: null check, slot 6 `ActorExists`, slot 7 `GetActor`.
    	// IActor slot 5 is `get_Position`; the Vector3 is boxed into `string.Format(string, object, object)`.
    	// The fallback is a three-string Concat.
    	[Token(Token = "0x60007B1")]
    	[Address(RVA = "0x3B3910", Offset = "0x3B2310", VA = "0x1803B3910")]
    	private string BuildText()
    	{
    		if (ActorManager != null && ActorManager.ActorExists(_actorId))
    		{
    			return string.Format("[{0}] 位置={1}", _actorId, ActorManager.GetActor(_actorId).Position);
    		}
    		return "[" + _actorId + "] 不存在";
    	}

    	[Token(Token = "0x60007B2")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DebugTextPreferenceActorInfo()
    	{
    	}
    }
}
