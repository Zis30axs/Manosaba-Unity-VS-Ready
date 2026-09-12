using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.Debugging.TextDisplays;

[Token(Token = "0x2000008")]
public class DebugTextPreferenceFps : DebugTextPreferenceBase
{
	[Token(Token = "0x4000014")]
	[FieldOffset(Offset = "0x50")]
	[Space]
	[SerializeField]
	private int _bufferSize;

	[Token(Token = "0x4000015")]
	[FieldOffset(Offset = "0x54")]
	[SerializeField]
	private bool _ignoreTimeScale;

	[Token(Token = "0x4000016")]
	[FieldOffset(Offset = "0x58")]
	private string _fpsText;

	[Token(Token = "0x17000003")]
	protected override string LabelText
	{
		[Token(Token = "0x6000018")]
		[Address(RVA = "0x453950", Offset = "0x452350", VA = "0x180453950", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000019")]
	[Address(RVA = "0xD46180", Offset = "0xD44B80", VA = "0x180D46180", Slot = "5")]
	protected override IDisposable ActivateLabelUpdate()
	{
		return null;
	}

	[Token(Token = "0x600001A")]
	[Address(RVA = "0xD46670", Offset = "0xD45070", VA = "0x180D46670")]
	public DebugTextPreferenceFps()
	{
	}
}
