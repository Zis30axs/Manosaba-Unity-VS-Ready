using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using FastEnumUtility;
using GigaCreation.NaninovelExtender.Ui;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000063")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("evidence")]
public class ChoiceEvidence : ShowWitchBookUi
{
	[StructLayout(3)]
	[Token(Token = "0x2000065")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__2
{
		[Token(Token = "0x400014B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400014C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400014D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ChoiceEvidence _003C_003E4__this;

		[Token(Token = "0x400014E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x400014F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private List<WitchBookCategory> _003Ccategories_003E5__2;

		[Token(Token = "0x4000150")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000151")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Naninovel.UniTask<string>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x4000152")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__3;

		[Token(Token = "0x6000146")]
		[Address(RVA = "0x326140", Offset = "0x324B40", VA = "0x180326140", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000147")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000147")]
	private const string ChosenEvidenceIdVarName = "chosenEvidenceId";

	[Token(Token = "0x4000148")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("correct")]
	public StringParameter CorrectEvidenceId;

	// RECONSTRUCTED | Confidence: A | RVA: 0x325E70, 389 B (entry) + MoveNext 0x326140
	// Three await points: state 0 `ShowWitchBook` (Naninovel UniTask), state 1
	// `IWitchBookUi.ChoiceEvidenceAsync` (Naninovel UniTask<string>), state 2
	// `ManagedUiExtensions.DismissAsync` (Cysharp UniTask). Anchors:
	// - `sub_4040` with ecx = 2 on IWitchTrialsNaninovelManager is slot 2 =
	//   `set_IsPresentEvidenceMode`, called with r9b = 1 first and 0 at the end.
	// - `Assigned(p)` is the inlined `p != null && p.HasValue`
	//   (ICommandParameter slot 0 through `sub_22E0`).
	// - `CategoryNames` goes through the `string[]` implicit operator
	//   (0x5AB6E0) into `WitchBookDataHelper.ParseCategories(string[])`.
	// - `ShowWitchBook` is called with mode 0 = `WitchBookPresentationMode.Local`
	//   and areas = null.
	// - With `correct:` assigned, active evidence ids are collected over the
	//   parsed categories, or `FastEnum.GetValues<WitchBookCategory>()` when
	//   none were given. Lambda `<Execute>b__2_0` (0x326010) is IWitchBookUi
	//   slot 3 `GetActiveEvidences(category)`; `<>c.<Execute>b__2_1` is the
	//   shared `IdVersionPair.Id` (+0x10) getter body. When the correct id is
	//   missing, `string.Format` gets the raw StringParameter object and the
	//   message goes to Command vtable +0x1B8 = slot 8 = `Err(string)` (not
	//   `Warn`). The literal (stringliteral.json 0x2E51550) is
	//   "正解の証拠が魔女図鑑に表示されていません：{0}".
	// - Debug skip: the correct id when assigned and not whitespace, else "*";
	//   no UI choice is awaited. Otherwise `ChoiceEvidenceAsync(categories,
	//   token)` = IWitchBookUi slot 4.
	// - The result goes to the custom variable "chosenEvidenceId" (using the
	//   const name is Confidence B), then the UI is dismissed with a null
	//   duration.
	// - After clearing present-evidence mode, debug skip also calls
	//   IScriptPlayer slot 0x22 = 34 = `SetSkipEnabled(true)`.
	[Token(Token = "0x6000140")]
	[Address(RVA = "0x325E70", Offset = "0x324870", VA = "0x180325E70", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__2))]
	public override async Naninovel.UniTask Execute([Optional] AsyncToken token)
	{
		WitchTrialsNaninovelManager.IsPresentEvidenceMode = true;
		List<WitchBookCategory> categories = Assigned(CategoryNames)
			? WitchBookDataHelper.ParseCategories(CategoryNames)
			: null;
		await ShowWitchBook(WitchBookPresentationMode.Local, categories, null, token);
		if (Assigned(CorrectEvidenceId))
		{
			string[] activeEvidenceIds = ((IEnumerable<WitchBookCategory>)categories ?? FastEnum.GetValues<WitchBookCategory>())
				.SelectMany((WitchBookCategory category) => WitchBookUi.GetActiveEvidences(category))
				.Select((IdVersionPair pair) => pair.Id)
				.ToArray();
			if (!activeEvidenceIds.Contains((string)CorrectEvidenceId))
			{
				Err(string.Format("正解の証拠が魔女図鑑に表示されていません：{0}", CorrectEvidenceId));
			}
		}
		string chosenEvidenceId;
		if (IsEnabledDebugSkip)
		{
			chosenEvidenceId = Assigned(CorrectEvidenceId) && !string.IsNullOrWhiteSpace(CorrectEvidenceId)
				? (string)CorrectEvidenceId
				: "*";
		}
		else
		{
			chosenEvidenceId = await WitchBookUi.ChoiceEvidenceAsync(categories, token);
		}
		NaniVarManager.TrySetVariableValue(ChosenEvidenceIdVarName, chosenEvidenceId);
		await WitchBookUi.DismissAsync(null, token);
		WitchTrialsNaninovelManager.IsPresentEvidenceMode = false;
		if (IsEnabledDebugSkip)
		{
			Engine.GetServiceOrErr<IScriptPlayer>().SetSkipEnabled(true);
		}
	}

	[Token(Token = "0x6000141")]
	[Address(RVA = "0x326000", Offset = "0x324A00", VA = "0x180326000")]
	public ChoiceEvidence()
	{
	}
}
