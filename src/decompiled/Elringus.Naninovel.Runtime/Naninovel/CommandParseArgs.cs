using System;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel;

[Token(Token = "0x2000408")]
public readonly struct CommandParseArgs : IEquatable<CommandParseArgs>
{
	[Token(Token = "0x4000EC6")]
	[FieldOffset(Offset = "0x0")]
	public readonly Naninovel.Parsing.Command Model;

	[Token(Token = "0x4000EC7")]
	[FieldOffset(Offset = "0x8")]
	public readonly PlaybackSpot PlaybackSpot;

	[Token(Token = "0x4000EC8")]
	[FieldOffset(Offset = "0x18")]
	public readonly int Indent;

	[Token(Token = "0x4000EC9")]
	[FieldOffset(Offset = "0x1C")]
	public readonly bool Transient;

	[Token(Token = "0x600137D")]
	[Address(RVA = "0x5AC680", Offset = "0x5AB080", VA = "0x1805AC680")]
	public CommandParseArgs(Naninovel.Parsing.Command model, PlaybackSpot playbackSpot, int indent, bool transient)
	{
	}

	[Token(Token = "0x600137E")]
	[Address(RVA = "0x5AC740", Offset = "0x5AB140", VA = "0x1805AC740", Slot = "4")]
	public bool Equals(CommandParseArgs other)
	{
		return default(bool);
	}

	[Token(Token = "0x600137F")]
	[Address(RVA = "0x5AC870", Offset = "0x5AB270", VA = "0x1805AC870", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x6001380")]
	[Address(RVA = "0x5AC910", Offset = "0x5AB310", VA = "0x1805AC910", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}
}
