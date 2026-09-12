using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000045")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("courtFire")]
public class ChangeCourtFireFxActivity : Command
{
	[Token(Token = "0x40000C9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	[ParameterDefaultValue("true")]
	public BooleanParameter Enable;

	[Token(Token = "0x40000CA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private ICourtModifier _courtModifier;

	[Token(Token = "0x17000010")]
	private ICourtModifier CameraModifier
	{
		[Token(Token = "0x60000E5")]
		[Address(RVA = "0x31D5D0", Offset = "0x31BFD0", VA = "0x18031D5D0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60000E6")]
	[Address(RVA = "0x31D690", Offset = "0x31C090", VA = "0x18031D690", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000E7")]
	[Address(RVA = "0x31D800", Offset = "0x31C200", VA = "0x18031D800")]
	public ChangeCourtFireFxActivity()
	{
	}
}
