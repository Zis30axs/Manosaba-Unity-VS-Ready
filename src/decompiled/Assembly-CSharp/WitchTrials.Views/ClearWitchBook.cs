using FastEnumUtility;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using WitchTrials.Debugging;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000068")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("clearBook")]
public class ClearWitchBook : Command
{
	[Token(Token = "0x400015E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	public StringListParameter CategoryNames;

	[Token(Token = "0x1700001D")]
	private bool IsAct2Chapter1Adv
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x327E70
		// Same prefix as GosubResetWitchBook.Execute: PlaybackSpot (+0x18) ScriptPath `.Split('/')`, last element into
		// ScriptHelper.TryParseScenarioName(out info); then info Act (+0x10) == 2, Chapter (+0x14) == 1, Part (+0x18) == 0 (Adv).
		[Token(Token = "0x600014D")]
		[Address(RVA = "0x327E70", Offset = "0x326870", VA = "0x180327E70")]
		get
		{
			string[] pathParts = PlaybackSpot.ScriptPath.Split('/');
			return ScriptHelper.TryParseScenarioName(pathParts[pathParts.Length - 1], out ScenarioInfo info) && info.Act == 2 && info.Chapter == 1 && info.Part == ScenarioPartKind.Adv;
		}
	}

	// RECONSTRUCTED | Confidence: A (debug-array spelling: B) | RVA: 0x327FD0
	// - `Engine.GetServiceOrErr<IUIManager>().GetUI<IWitchBookUi>()` first.
	// - `DebugOptions.CheckWitchBookState` inlined (IsDebugMode, static +0x1A): IsAct2Chapter1Adv inlined ->
	//   `FastEnum.GetValues<WitchBookCategory>()`, otherwise a fresh `WitchBookCategory[1]` with no element store
	//   (Roslyn omits default-valued initializer elements, so `{ Clue }` is B).
	// - Otherwise Assigned(CategoryNames) ? ParseCategories(CategoryNames) : FastEnum.GetValues<WitchBookCategory>().
	// - foreach (IEnumerable<T> enumerator, disposed) -> IWitchBookUi slot 7 ClearState(category); returns default.
	[Token(Token = "0x600014E")]
	[Address(RVA = "0x327FD0", Offset = "0x3269D0", VA = "0x180327FD0", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		IWitchBookUi witchBookUi = Engine.GetServiceOrErr<IUIManager>().GetUI<IWitchBookUi>();
		IEnumerable<WitchBookCategory> categories;
		if (DebugOptions.CheckWitchBookState)
		{
			categories = IsAct2Chapter1Adv ? FastEnum.GetValues<WitchBookCategory>() : new[] { WitchBookCategory.Clue };
		}
		else
		{
			categories = Assigned(CategoryNames) ? WitchBookDataHelper.ParseCategories(CategoryNames) : FastEnum.GetValues<WitchBookCategory>();
		}
		foreach (WitchBookCategory category in categories)
		{
			witchBookUi.ClearState(category);
		}
		return UniTask.CompletedTask;
	}

	[Token(Token = "0x600014F")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public ClearWitchBook()
	{
	}
}
