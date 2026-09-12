using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;

namespace Naninovel;

[Token(Token = "0x200009E")]
public class LayeredCharacterBehaviour : LayeredActorBehaviour
{
	[Token(Token = "0x4000238")]
	[FieldOffset(Offset = "0x70")]
	[Tooltip("Invoked when the character becomes the author of the printed text message.")]
	[SerializeField]
	private UnityEvent onStartedSpeaking;

	[Token(Token = "0x4000239")]
	[FieldOffset(Offset = "0x78")]
	[Tooltip("Invoked after `On Started Speaking` when the message is fully revealed or (when auto voicing is enabled) voice clip finish playing.")]
	[SerializeField]
	private UnityEvent onFinishedSpeaking;

	[Token(Token = "0x14000010")]
	public event Action<bool> OnIsSpeakingChanged
	{
		[Token(Token = "0x60002D4")]
		[Address(RVA = "0x462390", Offset = "0x460D90", VA = "0x180462390")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x60002D5")]
		[Address(RVA = "0x4624A0", Offset = "0x460EA0", VA = "0x1804624A0")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x60002D6")]
	[Address(RVA = "0x4625B0", Offset = "0x460FB0", VA = "0x1804625B0")]
	public void NotifyIsSpeakingChanged(bool value)
	{
	}

	[Token(Token = "0x60002D7")]
	[Address(RVA = "0x462610", Offset = "0x461010", VA = "0x180462610")]
	public LayeredCharacterBehaviour()
	{
	}
}
