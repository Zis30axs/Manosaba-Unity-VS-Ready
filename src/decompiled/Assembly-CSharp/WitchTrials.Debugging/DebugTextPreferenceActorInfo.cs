using GigaCreation.Tools.Debugging.TextDisplays;
using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace WitchTrials.Debugging;

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
		[Token(Token = "0x60007AF")]
		[Address(RVA = "0x3B35B0", Offset = "0x3B1FB0", VA = "0x1803B35B0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000145")]
	protected override string LabelText
	{
		[Token(Token = "0x60007B0")]
		[Address(RVA = "0x3B36D0", Offset = "0x3B20D0", VA = "0x1803B36D0", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60007B1")]
	[Address(RVA = "0x3B3910", Offset = "0x3B2310", VA = "0x1803B3910")]
	private string BuildText()
	{
		return null;
	}

	[Token(Token = "0x60007B2")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugTextPreferenceActorInfo()
	{
	}
}
