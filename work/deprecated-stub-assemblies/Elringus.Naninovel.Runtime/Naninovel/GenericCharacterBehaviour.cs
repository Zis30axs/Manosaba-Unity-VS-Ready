using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;

namespace Naninovel
{
    [Token(Token = "0x200008B")]
    public class GenericCharacterBehaviour : GenericActorBehaviour
    {
    	[Serializable]
    	[Token(Token = "0x200008C")]
    	private class LookDirectionChangedEvent : UnityEvent<CharacterLookDirection>
    	{
    		[Token(Token = "0x6000271")]
    		[Address(RVA = "0x45DE50", Offset = "0x45C850", VA = "0x18045DE50")]
    		public LookDirectionChangedEvent()
    		{
    		}
    	}

    	[Token(Token = "0x40001EA")]
    	[FieldOffset(Offset = "0x60")]
    	[Tooltip("Invoked when look direction of the character is changed.")]
    	[SerializeField]
    	private LookDirectionChangedEvent onLookDirectionChanged;

    	[Token(Token = "0x40001EB")]
    	[FieldOffset(Offset = "0x68")]
    	[Tooltip("Invoked when the character becomes the author of the printed text message.")]
    	[SerializeField]
    	private UnityEvent onStartedSpeaking;

    	[Token(Token = "0x40001EC")]
    	[FieldOffset(Offset = "0x70")]
    	[Tooltip("Invoked after `On Started Speaking` when the message is fully revealed or (when auto voicing is enabled) voice clip finish playing.")]
    	[SerializeField]
    	private UnityEvent onFinishedSpeaking;

    	[Token(Token = "0x40001ED")]
    	[FieldOffset(Offset = "0x78")]
    	[Tooltip("Whether to react to look direction changes by rotating the object's transform.")]
    	[SerializeField]
    	private bool transformByLookDirection;

    	[Token(Token = "0x40001EE")]
    	[FieldOffset(Offset = "0x7C")]
    	[Tooltip("When `transformByLookDirection` is enabled, controls the rotation angle.")]
    	[SerializeField]
    	private float lookDeltaAngle;

    	[Token(Token = "0x17000064")]
    	public bool TransformByLookDirection
    	{
    		[Token(Token = "0x600026C")]
    		[Address(RVA = "0x45DD00", Offset = "0x45C700", VA = "0x18045DD00")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000065")]
    	public float LookDeltaAngle
    	{
    		[Token(Token = "0x600026D")]
    		[Address(RVA = "0x45DD10", Offset = "0x45C710", VA = "0x18045DD10")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x1400000E")]
    	public event Action<CharacterLookDirection> OnLookDirectionChanged
    	{
    		[Token(Token = "0x6000268")]
    		[Address(RVA = "0x45D8C0", Offset = "0x45C2C0", VA = "0x18045D8C0")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000269")]
    		[Address(RVA = "0x45D9D0", Offset = "0x45C3D0", VA = "0x18045D9D0")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x1400000F")]
    	public event Action<bool> OnIsSpeakingChanged
    	{
    		[Token(Token = "0x600026A")]
    		[Address(RVA = "0x45DAE0", Offset = "0x45C4E0", VA = "0x18045DAE0")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x600026B")]
    		[Address(RVA = "0x45DBF0", Offset = "0x45C5F0", VA = "0x18045DBF0")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x600026E")]
    	[Address(RVA = "0x45DD20", Offset = "0x45C720", VA = "0x18045DD20")]
    	public void NotifyLookDirectionChanged(CharacterLookDirection value)
    	{
    	}

    	[Token(Token = "0x600026F")]
    	[Address(RVA = "0x45DDA0", Offset = "0x45C7A0", VA = "0x18045DDA0")]
    	public void NotifyIsSpeakingChanged(bool value)
    	{
    	}

    	[Token(Token = "0x6000270")]
    	[Address(RVA = "0x45DE00", Offset = "0x45C800", VA = "0x18045DE00")]
    	public GenericCharacterBehaviour()
    	{
    	}
    }
}
