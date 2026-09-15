using FastEnumUtility;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using WitchTrials.Models;

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

	// RECONSTRUCTED | Confidence: A | RVA: 0x32C8A0
	// - `Engine.GetServiceOrErr<IUIManager>().GetUI<IWitchBookUi>()` first.
	// - `Category` (+0x38) to string (HasValue slot 9 / get_Value inlined) -> FastEnum.TryParse (TryParseInternal,
	//   ignoreCase false). On failure: string.Join(", ", FastEnum.GetValues<WitchBookCategory>()), then
	//   string.Format("Invalid category: {0}. Valid values are: {1}", raw Category object, joined) -> Command slot 8 Err.
	// - `Id` (+0x30) to string, `Version` (+0x40) Nullable<int> op_Implicit, IWitchBookUi slot 6
	//   UpdateVersion(category, id, version) -- also on the failure path (category stays 0). Returns default.
	[Token(Token = "0x6000170")]
	[Address(RVA = "0x32C8A0", Offset = "0x32B2A0", VA = "0x18032C8A0", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		IWitchBookUi witchBookUi = Engine.GetServiceOrErr<IUIManager>().GetUI<IWitchBookUi>();
		if (!FastEnum.TryParse(Category, out WitchBookCategory category))
		{
			Err(string.Format("Invalid category: {0}. Valid values are: {1}", Category, string.Join(", ", FastEnum.GetValues<WitchBookCategory>())));
		}
		witchBookUi.UpdateVersion(category, Id, Version);
		return UniTask.CompletedTask;
	}

	[Token(Token = "0x6000171")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public UpdateWitchBook()
	{
	}
}
