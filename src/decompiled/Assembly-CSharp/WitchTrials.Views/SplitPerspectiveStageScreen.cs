using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000053")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("splitStageScreen")]
public class SplitPerspectiveStageScreen : Command
{
	[Token(Token = "0x400010F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	[RequiredParameter]
	public IntegerParameter Index;

	[Token(Token = "0x4000110")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("posX")]
	public DecimalParameter ScenePositionX;

	[Token(Token = "0x4000111")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public DecimalParameter Roll;

	[Token(Token = "0x4000112")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("direction")]
	public StringParameter SplitDirection;

	[Token(Token = "0x4000113")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private IPerspectiveStageScreenModifier _screenModifier;

	[Token(Token = "0x17000016")]
	private IPerspectiveStageScreenModifier ScreenModifier
	{
		[Token(Token = "0x600010D")]
		[Address(RVA = "0x321C20", Offset = "0x320620", VA = "0x180321C20")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600010E")]
	[Address(RVA = "0x321CE0", Offset = "0x3206E0", VA = "0x180321CE0", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600010F")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public SplitPerspectiveStageScreen()
	{
	}
}
