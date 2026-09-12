using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Common;

[Token(Token = "0x200000E")]
[UsedImplicitly]
public class SetIfUndefined : Command
{
	[Token(Token = "0x400002B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	[RequiredParameter]
	[ParameterAlias("")]
	public StringParameter VarName;

	[Token(Token = "0x400002C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly]
	[ParameterAlias("num")]
	public DecimalParameter NumericValue;

	[Token(Token = "0x400002D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly]
	[ParameterAlias("str")]
	public StringParameter StringValue;

	[Token(Token = "0x400002E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[UsedImplicitly]
	[ParameterAlias("bool")]
	public BooleanParameter BooleanValue;

	[Token(Token = "0x600001B")]
	[Address(RVA = "0xCF9BD0", Offset = "0xCF85D0", VA = "0x180CF9BD0", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600001C")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public SetIfUndefined()
	{
	}
}
