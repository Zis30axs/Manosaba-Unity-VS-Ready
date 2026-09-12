using System;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;

namespace GigaCreation.NaninovelExtender.Common;

[Token(Token = "0x2000019")]
public class NaninovelInteractionBlocker : CancelEventBlocker, INaninovelInteractionBlocker, IDisposable
{
	[Token(Token = "0x4000045")]
	[FieldOffset(Offset = "0x18")]
	private readonly IClickThroughPanelExtended _clickThroughPanel;

	[Token(Token = "0x4000046")]
	[FieldOffset(Offset = "0x20")]
	private int? _originalSortingOrder;

	[Token(Token = "0x6000034")]
	[Address(RVA = "0xCFBE30", Offset = "0xCFA830", VA = "0x180CFBE30")]
	public NaninovelInteractionBlocker()
	{
	}

	[Token(Token = "0x6000035")]
	[Address(RVA = "0xCFBEB0", Offset = "0xCFA8B0", VA = "0x180CFBEB0")]
	public NaninovelInteractionBlocker(ICancelEventBlockable blockable)
	{
	}

	[Token(Token = "0x6000036")]
	[Address(RVA = "0xCFBFD0", Offset = "0xCFA9D0", VA = "0x180CFBFD0")]
	public NaninovelInteractionBlocker(params string[] allowedSamplers)
	{
	}

	[Token(Token = "0x6000037")]
	[Address(RVA = "0xCFC030", Offset = "0xCFAA30", VA = "0x180CFC030")]
	public NaninovelInteractionBlocker(ICancelEventBlockable[] blockables, params string[] allowedSamplers)
	{
	}

	[Token(Token = "0x6000038")]
	[Address(RVA = "0xCFC310", Offset = "0xCFAD10", VA = "0x180CFC310", Slot = "6")]
	public void SetSortingOrder(int order)
	{
	}

	[Token(Token = "0x6000039")]
	[Address(RVA = "0xCFC400", Offset = "0xCFAE00", VA = "0x180CFC400", Slot = "5")]
	public override void Dispose()
	{
	}
}
