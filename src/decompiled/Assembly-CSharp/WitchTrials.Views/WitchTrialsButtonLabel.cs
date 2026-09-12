using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x200001D")]
public class WitchTrialsButtonLabel : RebuildableLabel
{
	[Token(Token = "0x4000043")]
	[FieldOffset(Offset = "0x60")]
	[Header("Parameters for Emphasized Text")]
	[SerializeField]
	private Color _emphasizedInitialLabelColor;

	[Token(Token = "0x4000044")]
	[FieldOffset(Offset = "0x70")]
	[SerializeField]
	private Color _emphasizedBodyLabelColor;

	[Token(Token = "0x4000045")]
	[FieldOffset(Offset = "0x80")]
	[SerializeField]
	private Color _normalLabelColor;

	[Token(Token = "0x17000002")]
	private string InitialHtmlRgb
	{
		[Token(Token = "0x6000066")]
		[Address(RVA = "0x3114A0", Offset = "0x30FEA0", VA = "0x1803114A0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000003")]
	private string BodyHtmlRgb
	{
		[Token(Token = "0x6000067")]
		[Address(RVA = "0x3114C0", Offset = "0x30FEC0", VA = "0x1803114C0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000004")]
	protected override Color DefaultLabelColor
	{
		[Token(Token = "0x6000068")]
		[Address(RVA = "0x3114E0", Offset = "0x30FEE0", VA = "0x1803114E0", Slot = "4")]
		get
		{
			return default(Color);
		}
	}

	[Token(Token = "0x6000069")]
	[Address(RVA = "0x3114F0", Offset = "0x30FEF0", VA = "0x1803114F0")]
	public void ModifyEmphasizedText()
	{
	}

	[Token(Token = "0x600006A")]
	[Address(RVA = "0x311810", Offset = "0x310210", VA = "0x180311810")]
	public WitchTrialsButtonLabel()
	{
	}
}
