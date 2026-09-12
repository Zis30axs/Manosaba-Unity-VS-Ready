using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x2000036")]
internal class CommandParser
{
	[Token(Token = "0x400007D")]
	[FieldOffset(Offset = "0x10")]
	[CompilerGenerated]
	private ISyntax _003Cstx_003EP;

	[Token(Token = "0x400007E")]
	[FieldOffset(Offset = "0x0")]
	private static readonly Command emptyBody;

	[Token(Token = "0x400007F")]
	[FieldOffset(Offset = "0x8")]
	private static readonly MixedValue emptyValue;

	[Token(Token = "0x4000080")]
	[FieldOffset(Offset = "0x18")]
	private readonly MixedValueParser valueParser;

	[Token(Token = "0x4000081")]
	[FieldOffset(Offset = "0x20")]
	private readonly List<Parameter> parameters;

	[Token(Token = "0x4000082")]
	[FieldOffset(Offset = "0x28")]
	private LineWalker walker;

	[Token(Token = "0x4000083")]
	[FieldOffset(Offset = "0x30")]
	private Command commandBody;

	[Token(Token = "0x4000084")]
	[FieldOffset(Offset = "0x38")]
	private PlainText commandId;

	[Token(Token = "0x4000085")]
	[FieldOffset(Offset = "0x40")]
	private MixedValue paramValue;

	[Token(Token = "0x4000086")]
	[FieldOffset(Offset = "0x48")]
	private PlainText? paramId;

	[Token(Token = "0x4000087")]
	[FieldOffset(Offset = "0x50")]
	private Token paramIdToken;

	[Token(Token = "0x6000133")]
	[Address(RVA = "0x10D9AA0", Offset = "0x10D84A0", VA = "0x1810D9AA0")]
	public CommandParser(ISyntax stx)
	{
	}

	[Token(Token = "0x6000134")]
	[Address(RVA = "0x10D9DF0", Offset = "0x10D87F0", VA = "0x1810D9DF0")]
	public Command Parse(LineWalker walker)
	{
		return null;
	}

	[Token(Token = "0x6000135")]
	[Address(RVA = "0x10D9E30", Offset = "0x10D8830", VA = "0x1810D9E30")]
	private void ResetCommandState(LineWalker walker)
	{
	}

	[Token(Token = "0x6000136")]
	[Address(RVA = "0x10DA050", Offset = "0x10D8A50", VA = "0x1810DA050")]
	private void ResetParameterState()
	{
	}

	[Token(Token = "0x6000137")]
	[Address(RVA = "0x10DA170", Offset = "0x10D8B70", VA = "0x1810DA170")]
	private bool TryCommandId()
	{
		return default(bool);
	}

	[Token(Token = "0x6000138")]
	[Address(RVA = "0x10DA3D0", Offset = "0x10D8DD0", VA = "0x1810DA3D0")]
	private void ParseCommandId(Token commandIdToken)
	{
	}

	[Token(Token = "0x6000139")]
	[Address(RVA = "0x10DA4E0", Offset = "0x10D8EE0", VA = "0x1810DA4E0")]
	private bool TryParameter()
	{
		return default(bool);
	}

	[Token(Token = "0x600013A")]
	[Address(RVA = "0x10DAA70", Offset = "0x10D9470", VA = "0x1810DAA70")]
	private void ParseParameterId(Token paramIdToken)
	{
	}

	[Token(Token = "0x600013B")]
	[Address(RVA = "0x10DAB80", Offset = "0x10D9580", VA = "0x1810DAB80")]
	private void ParseBoolFlag(Token flagToken)
	{
	}

	[Token(Token = "0x600013C")]
	[Address(RVA = "0x10DAE40", Offset = "0x10D9840", VA = "0x1810DAE40")]
	private void ParseParameterValue(Token valueToken)
	{
	}

	[Token(Token = "0x600013D")]
	[Address(RVA = "0x10DAF20", Offset = "0x10D9920", VA = "0x1810DAF20")]
	private void ParseParameter(Token paramToken)
	{
	}

	[Token(Token = "0x600013E")]
	[Address(RVA = "0x10DB000", Offset = "0x10D9A00", VA = "0x1810DB000")]
	private void ParseCommandBody(Token bodyToken)
	{
	}
}
