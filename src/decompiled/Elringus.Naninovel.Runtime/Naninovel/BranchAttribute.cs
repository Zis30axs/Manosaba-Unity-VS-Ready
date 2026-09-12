using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Metadata;

namespace Naninovel;

[Token(Token = "0x20000D3")]
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class BranchAttribute : Attribute
{
	[Token(Token = "0x400033B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	public readonly BranchTraits Traits;

	[Token(Token = "0x400033C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	public readonly string SwitchRoot;

	[Token(Token = "0x400033D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	public readonly string Endpoint;

	[Token(Token = "0x600040F")]
	[Address(RVA = "0x476E80", Offset = "0x475880", VA = "0x180476E80")]
	public BranchAttribute(BranchTraits traits, [Optional] string switchRoot, [Optional] string endpoint)
	{
	}
}
