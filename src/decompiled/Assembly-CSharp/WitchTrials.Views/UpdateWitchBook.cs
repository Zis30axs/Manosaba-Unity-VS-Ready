using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x2000072")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("update")]
public class UpdateWitchBook : Command
{
	[Token(Token = "0x400018F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	public StringParameter Id;

	[Token(Token = "0x4000190")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public StringParameter Category;

	[Token(Token = "0x4000191")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public IntegerParameter Version;

	[Token(Token = "0x6000170")]
	[Address(RVA = "0x32C8A0", Offset = "0x32B2A0", VA = "0x18032C8A0", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000171")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public UpdateWitchBook()
	{
	}
}
