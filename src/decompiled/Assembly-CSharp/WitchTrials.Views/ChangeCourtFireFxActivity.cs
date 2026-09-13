using System.Runtime.InteropServices;
using GigaCreation.Tools.Service;
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

	// RECONSTRUCTED | Confidence: A | RVA: 0x31D5D0
	// Despite the property's own name, the backing service is looked up as
	// `IPerspectiveStageManager` (with `includeDerivedType: true`) -- i.e. the
	// court-fire-toggling capability is implemented by the same
	// PerspectiveStageManager that also implements the camera/stage services,
	// requested here through its narrower `ICourtModifier` facet.
	[Token(Token = "0x17000010")]
	private ICourtModifier CameraModifier
	{
		[Token(Token = "0x60000E5")]
		[Address(RVA = "0x31D5D0", Offset = "0x31BFD0", VA = "0x18031D5D0")]
		get
		{
			return _courtModifier ??= ServiceLocator.Get<IPerspectiveStageManager>(true);
		}
	}

	// RECONSTRUCTED | Confidence: A (call shape), B (exact nullable-cast chain
	// through `Enable`) | RVA: 0x31D690
	// `ICourtModifier.ChangeFireFxActivity` is dump.cs Slot 0 (its only member).
	// `Enable` -> bool goes through BooleanParameter's own `bool?` operator,
	// same "small conversion op gets inlined" pattern documented elsewhere in
	// this project for IntegerParameter -> int?.
	[Token(Token = "0x60000E6")]
	[Address(RVA = "0x31D690", Offset = "0x31C090", VA = "0x18031D690", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		CameraModifier.ChangeFireFxActivity(((bool?)Enable).Value);
		return default(UniTask);
	}

	[Token(Token = "0x60000E7")]
	[Address(RVA = "0x31D800", Offset = "0x31C200", VA = "0x18031D800")]
	public ChangeCourtFireFxActivity()
	{
	}
}
