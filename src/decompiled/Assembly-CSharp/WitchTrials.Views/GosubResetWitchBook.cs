using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FastEnumUtility;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using Naninovel.Metadata;
using UnityEngine;
using WitchTrials.Debugging;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000069")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[Branch(BranchTraits.Endpoint | BranchTraits.Return, null, null)]
[CommandAlias("gosubResetBook")]
public class GosubResetWitchBook : Gosub
{
	[StructLayout(3)]
	[Token(Token = "0x200006D")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__4
	{
		[Token(Token = "0x4000169")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400016A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400016B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GosubResetWitchBook _003C_003E4__this;

		[Token(Token = "0x400016C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x400016D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000160")]
		[Address(RVA = "0x329A90", Offset = "0x328490", VA = "0x180329A90", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000161")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200006E")]
	[CompilerGenerated]
	private struct _003CExportScriptFile_003Ed__7
	{
		[Token(Token = "0x400016E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400016F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000170")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GosubResetWitchBook _003C_003E4__this;

		[Token(Token = "0x4000171")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private StreamWriter _003Cwriter_003E5__2;

		[Token(Token = "0x4000172")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private object _003C_003E7__wrap2;

		[Token(Token = "0x4000173")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private int _003C_003E7__wrap3;

		[Token(Token = "0x4000174")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private TaskAwaiter _003C_003Eu__1;

		[Token(Token = "0x4000175")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Dictionary<WitchBookCategory, IdVersionPair[]>.Enumerator _003C_003E7__wrap4;

		[Token(Token = "0x4000176")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
		private WitchBookCategory _003Ccategory_003E5__6;

		[Token(Token = "0x4000177")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
		private IdVersionPair[] _003C_003E7__wrap6;

		[Token(Token = "0x4000178")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
		private int _003C_003E7__wrap7;

		[Token(Token = "0x4000179")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
		private ValueTaskAwaiter _003C_003Eu__2;

		[Token(Token = "0x6000162")]
		[Address(RVA = "0x32A300", Offset = "0x328D00", VA = "0x18032A300", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000163")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	// The evidence snapshot from the previous check (null until the first
	// `CheckEvidenceState`, and again after `ResetStaticFields`).
	[Token(Token = "0x400015F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static Dictionary<WitchBookCategory, IdVersionPair[]> s_prevEvidences;

	[Token(Token = "0x4000160")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public IntegerParameter Version;

	[Token(Token = "0x4000161")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public BooleanParameter IsDebugDirectJump;

	[Token(Token = "0x4000162")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private ScenarioInfo _info;

	// RECONSTRUCTED | Confidence: A | RVA: 0x328530, 383 B (entry) + MoveNext 0x329A90
	// State 0 awaits `ExportScriptFile()`; state 1 awaits the base gosub task.
	// - `PlaybackSpot` (+0x18, first field `ScriptPath`)
	//   `.Split('/')` (char overload, options 0); the last element is passed to
	//   `ScriptHelper.TryParseScenarioName` with the out target `ref _info`
	//   (+0x50). On failure, vtable +0x1A8 = Command slot 7 `Warn` with literal
	//   0x2E51390, then SetResult.
	// - `DebugOptions.CheckWitchBookState` (called, 0x3AA470) and the static
	//   `CheckEvidenceState()`. When both hold, it awaits `ExportScriptFile()` (the
	//   kickoff is inlined); afterwards, `_info.Part` (+0x18) == 2 (`Trial`)
	//   ends the command. When either check fails, that Part test is skipped.
	// - `IsDebugDirectJump` (+0x48): when assigned (ICommandParameter slot 0),
	//   `Nullable<bool>` -> bool, and a false value ends the command.
	// - The rest (slots 16 `PushReturnSpot`, 17 `TryGetScriptPathAndLabel`,
	//   18 `ShouldNavigatePlayedScript`, 19 `NavigatePlayedScript`,
	//   `LoadAndPlayAtLabel`/`LoadAndPlay`) is byte-for-byte the base
	//   `Gosub.Execute` body (0x4EFC80, disassembled for comparison), inlined;
	//   its UniTask is awaited. The base ignores `token`.
	[Token(Token = "0x6000150")]
	[Address(RVA = "0x328530", Offset = "0x326F30", VA = "0x180328530", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__4))]
	public override async UniTask Execute([Optional] AsyncToken token)
	{
		string[] pathParts = PlaybackSpot.ScriptPath.Split('/');
		if (!ScriptHelper.TryParseScenarioName(pathParts[pathParts.Length - 1], out _info))
		{
			Warn("魔女図鑑リセットが想定されていないシナリオで実行されました。");
			return;
		}
		if (DebugOptions.CheckWitchBookState && CheckEvidenceState())
		{
			await ExportScriptFile();
			if (_info.Part == ScenarioPartKind.Trial)
			{
				return;
			}
		}
		if (Assigned(IsDebugDirectJump) && !IsDebugDirectJump)
		{
			return;
		}
		// RECONSTRUCTED | Confidence: A | <>n__0 base-call accessor, RVA: 0x329360, 301 B
		// Compiler-generated for the `base.Execute` call inside this async method.
		// Its body is the base `Gosub.Execute` (0x4EFC80) inlined into the accessor,
		// with the same slot 16-19 calls and LoadAndPlay/LoadAndPlayAtLabel.
		await base.Execute(token);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3286B0, 1643 B
	// - `scriptPath` = `Path` (+0x30) -> `Nullable<NamedString>.Value.Name`, as in
	//   the base 0x4F0040.
	// - `label` replaces the base's `Path.Value.Value`, and is formatted from
	//   `_info` (+0x50):
	//   - When `Version` (+0x40) is assigned: `string.Format("Reset{0}-{1}-{2}{3}",
	//     object[4] { box Act, box Chapter, Part.FastToString(), Version })`. The
	//     parameter object itself is the fourth argument, so its ToString is
	//     used.
	//   - Otherwise the three-argument `"Reset{0}-{1}-{2}"`.
	// - The tail matches the base exactly. When `scriptPath` is null or
	//   whitespace (inlined IsNullOrWhiteSpace loop) and the player's
	//   `PlayedScript` (via the inlined `Player` getter, then fixed-slot thunk
	//   `sub_3590`) is not alive (`m_CachedPtr` == 0): vtable +0x1B8 = slot 8
	//   `Err` with the Naninovel message, return false.
	// - Otherwise true.
	[Token(Token = "0x6000151")]
	[Address(RVA = "0x3286B0", Offset = "0x3270B0", VA = "0x1803286B0", Slot = "17")]
	protected override bool TryGetScriptPathAndLabel(out string scriptPath, out string label)
	{
		scriptPath = Path.Value.Name;
		if (Assigned(Version))
		{
			label = string.Format("Reset{0}-{1}-{2}{3}", _info.Act, _info.Chapter, _info.Part.FastToString(), Version);
		}
		else
		{
			label = string.Format("Reset{0}-{1}-{2}", _info.Act, _info.Chapter, _info.Part.FastToString());
		}
		if (string.IsNullOrWhiteSpace(scriptPath) && !Player.PlayedScript)
		{
			Err("Failed to execute '@goto' command: script path is not specified and no script is currently played.");
			return false;
		}
		return true;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x328D20, 1103 B
	// - Display class 6_0: `witchBookUi` (+0x10) and `currentEvidences` (+0x18).
	// - `FastEnum.GetValues<WitchBookCategory>()` (called through an aliased
	//   shared body).
	// - `witchBookUi` = `Engine.GetServiceOrErr<IUIManager>().GetUI<IWitchBookUi>()`.
	// - `currentEvidences` = `ToDictionary(values, <>c.b__6_0 (identity, 0x329530),
	//   b__1)`, where b__1 (0x329540) is `witchBookUi.GetActiveEvidences(category)`
	//   (IWitchBookUi slot 3) `.OrderBy(<>c.b__6_3 = pair.Id).ToArray()`.
	// - The result is true when `s_prevEvidences` is null; otherwise it is
	//   `values.Any(b__2)`.
	//   - b__2 (0x329780): true when the current and previous array lengths
	//     differ.
	//   - Otherwise `current.Where(b__4).Any()`.
	//   - b__4 (0x329950) looks up `s_prevEvidences[category][idx]` and returns
	//     the negated inlined identity check: previous null -> true; same
	//     reference -> false; `Id` string equality, then `Version` (+0x18)
	//     inequality.
	//   - That check is byte-identical (negated) to `IdVersionPair.Equals
	//     (IdVersionPair)` at 0x3A2E80, which was disassembled for comparison.
	// - Finally `s_prevEvidences = currentEvidences`, stored even when the result
	//   is true, and the bool is returned.
	[Token(Token = "0x6000152")]
	[Address(RVA = "0x328D20", Offset = "0x327720", VA = "0x180328D20")]
	private static bool CheckEvidenceState()
	{
		var categories = FastEnum.GetValues<WitchBookCategory>();
		IWitchBookUi witchBookUi = Engine.GetServiceOrErr<IUIManager>().GetUI<IWitchBookUi>();
		Dictionary<WitchBookCategory, IdVersionPair[]> currentEvidences = categories.ToDictionary(category => category, category => witchBookUi.GetActiveEvidences(category).OrderBy(pair => pair.Id).ToArray());
		bool changed = s_prevEvidences == null || categories.Any(category => currentEvidences[category].Length != s_prevEvidences[category].Length || currentEvidences[category].Where((pair, idx) => !pair.Equals(s_prevEvidences[category][idx])).Any());
		s_prevEvidences = currentEvidences;
		return changed;
	}

	// RECONSTRUCTED | Confidence: A (writes), B (`await using` lowering details) | RVA: 0x329170, 332 B (entry) + MoveNext 0x32A300, 5204 B
	// States 0-5 are TaskAwaiters and state 6 is the `DisposeAsync` ValueTaskAwaiter.
	// - `new StreamWriter(Path.Combine(Environment.GetFolderPath(0 = Desktop),
	//   "ResetWitchBook.nani"), append: true)` goes to +0x20.
	// - wrap2 (+0x28) and wrap3 (+0x30) are the `await using` exception and flow
	//   slots. A caught Exception is rethrown through `ExceptionDispatchInfo`.
	// - TextWriter vtable +0x2F8 = slot 28 `WriteLineAsync(string)` and
	//   +0x308 = slot 29 `WriteLineAsync()`. They run in order:
	//   1. `"# " + PlaybackSpot.ScriptPath.Split('/')[last]`
	//   2. (empty line)
	//   3. `"@clearBook"`
	//   4. For each (category, pairs) in `s_prevEvidences` (enumerator +0x40,
	//      KeyValuePair.Deconstruct), for each pair:
	//      `string.Format("@update {0} category:{1} version:{2}", pair.Id,
	//      category.FastToString(), box pair.Version)`.
	//   5. `"@return"`
	//   6. (empty line)
	// - Then wrap3 = 1, followed by `writer.DisposeAsync()` (IAsyncDisposable).
	//   A wrap3 value of 1 is how the compiler marks a return from inside the
	//   `await using` block. Whether the source had an explicit trailing
	//   `return;` cannot be told; behavior is the same.
	// Writes a Naninovel script snippet that rebuilds the current witch-book state.
	// It only runs under the debug option `CheckWitchBookState`.
	[Token(Token = "0x6000153")]
	[Address(RVA = "0x329170", Offset = "0x327B70", VA = "0x180329170")]
	[AsyncStateMachine(typeof(_003CExportScriptFile_003Ed__7))]
	private async UniTask ExportScriptFile()
	{
		await using StreamWriter writer = new StreamWriter(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ResetWitchBook.nani"), true);
		string[] pathParts = PlaybackSpot.ScriptPath.Split('/');
		await writer.WriteLineAsync("# " + pathParts[pathParts.Length - 1]);
		await writer.WriteLineAsync();
		await writer.WriteLineAsync("@clearBook");
		foreach (var (category, pairs) in s_prevEvidences)
		{
			foreach (IdVersionPair pair in pairs)
			{
				await writer.WriteLineAsync(string.Format("@update {0} category:{1} version:{2}", pair.Id, category.FastToString(), pair.Version));
			}
		}
		await writer.WriteLineAsync("@return");
		await writer.WriteLineAsync();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3292C0, 150 B
	// Stores null to static +0x0 `s_prevEvidences`.
	[Token(Token = "0x6000154")]
	[Address(RVA = "0x3292C0", Offset = "0x327CC0", VA = "0x1803292C0")]
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void ResetStaticFields()
	{
		s_prevEvidences = null;
	}

	[Token(Token = "0x6000155")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public GosubResetWitchBook()
	{
	}
}
