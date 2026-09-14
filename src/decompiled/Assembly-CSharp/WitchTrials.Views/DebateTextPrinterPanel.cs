using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.Localization;
using GigaCreation.Essentials.Ui;
using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using LitMotion;
using LitMotion.Extensions;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x20000E2")]
public class DebateTextPrinterPanel : UITextPrinterPanel, ILocalizableUI
{
	[Serializable]
	[Token(Token = "0x20000E3")]
	public class LabelSpaceData
	{
		[Token(Token = "0x4000458")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		[SerializeField]
		private LocaleKind _localeKind;

		[Token(Token = "0x4000459")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
		[SerializeField]
		private float _space;

		[Token(Token = "0x1700007D")]
		public LocaleKind LocaleKind
		{
			[Token(Token = "0x6000386")]
			[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
			get
			{
				return default(LocaleKind);
			}
		}

		[Token(Token = "0x1700007E")]
		public float Space
		{
			[Token(Token = "0x6000387")]
			[Address(RVA = "0x3686E0", Offset = "0x3670E0", VA = "0x1803686E0")]
			get
			{
				return default(float);
			}
		}

		[Token(Token = "0x6000388")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public LabelSpaceData()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000E5")]
	[CompilerGenerated]
	private struct _003CGosub_003Ed__36
{
		[Token(Token = "0x4000462")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000463")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000464")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public string id;

		[Token(Token = "0x4000465")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000392")]
		[Address(RVA = "0x368AC0", Offset = "0x3674C0", VA = "0x180368AC0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000393")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000E6")]
	[CompilerGenerated]
	private struct _003CRevealMessages_003Ed__30
{
		[Token(Token = "0x4000466")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000467")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000468")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public DebateTextPrinterPanel _003C_003E4__this;

		[Token(Token = "0x4000469")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x400046A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000394")]
		[Address(RVA = "0x368E80", Offset = "0x367880", VA = "0x180368E80", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000395")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000E7")]
	[CompilerGenerated]
	private struct _003CWatAndRevealAsync_003Ed__35
{
		[Token(Token = "0x400046B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400046C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400046D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public float delay;

		[Token(Token = "0x400046E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x400046F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public DebateText label;

		[Token(Token = "0x4000470")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000471")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000396")]
		[Address(RVA = "0x3697F0", Offset = "0x3681F0", VA = "0x1803697F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000397")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400044C")]
	private const string EnableDebugSkipVarName = "enableDebugSkip";

	[Token(Token = "0x400044D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x138")]
	[Header("References for Debate Printer")]
	[SerializeField]
	private FontMaterialChanger _fontMaterialChanger;

	[Token(Token = "0x400044E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x140")]
	[SerializeField]
	private AutoLayoutSupporter _autoLayoutSupporter;

	[Token(Token = "0x400044F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x148")]
	[SerializeField]
	private HorizontalOrVerticalLayoutGroup _layoutGroup;

	[Token(Token = "0x4000450")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x150")]
	[SerializeField]
	private DebateText[] _labels;

	[Token(Token = "0x4000451")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x158")]
	[Header("parameters for Debate Printer")]
	[SerializeField]
	private LabelSpaceData[] _labelSpaceData;

	[Token(Token = "0x4000452")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x160")]
	private readonly StringBuilder _builder = new StringBuilder();

	[Token(Token = "0x4000453")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x168")]
	private ILocalizationProvider _localizationProvider;

	[Token(Token = "0x4000454")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x170")]
	private ICustomVariableManager _naniVarManager;

	[Token(Token = "0x4000455")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x178")]
	private IDictionary<LocaleKind, float> _labelSpaces;

	[Token(Token = "0x4000456")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x180")]
	private CancellationTokenSource _revealCts;

	[Token(Token = "0x17000078")]
	private ILocalizationProvider LocalizationProvider
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x366090, 202 B
		// Lazy cache on `+0x168`: null check, then `GigaCreation.Tools.Service
		// .ServiceLocator.Get<GigaCreation.Essentials.Localization
		// .ILocalizationManager>()` (the zeroed first argument is
		// `includeDerivedType`, whose declared default is `false`), store
		// back, return. The lookup type is `ILocalizationManager` (which
		// extends `ILocalizationProvider`), not the field's own type. Both
		// names are fully qualified because `Naninovel.ILocalizationManager`
		// is also in scope. This exact body is also inlined in
		// `RevealMessages`.
		[Token(Token = "0x6000371")]
		[Address(RVA = "0x366090", Offset = "0x364A90", VA = "0x180366090")]
		get
		{
			return _localizationProvider ??= GigaCreation.Tools.Service.ServiceLocator.Get<GigaCreation.Essentials.Localization.ILocalizationManager>();
		}
	}

	[Token(Token = "0x17000079")]
	private ICustomVariableManager NaniVarManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x366160, 202 B
		// Lazy cache on `+0x170` via `Naninovel.Engine
		// .GetServiceOrErr<ICustomVariableManager>()` -- same shape as
		// `StageScreenController.TrialStateManager`.
		[Token(Token = "0x6000372")]
		[Address(RVA = "0x366160", Offset = "0x364B60", VA = "0x180366160")]
		get
		{
			return _naniVarManager ??= Naninovel.Engine.GetServiceOrErr<ICustomVariableManager>();
		}
	}

	[Token(Token = "0x1700007A")]
	private bool IsEnabledDebugSkip
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x366230, 275 B
		// `NaniVarManager` (inlined in full), then
		// `CustomVariableManagerExtensions.TryGetVariableValue<bool>(
		// "enableDebugSkip", out value)` with the out local pre-zeroed, and
		// the result is `returned & value` (`and al, [rsp+0x30]`). With a
		// side-effect-free pre-zeroed local, `&&` and `&` are the same
		// behavior; `&&` is written. The literal equals this class's
		// `EnableDebugSkipVarName` const; a const compiles to the literal, so
		// using the const name is Confidence B (the value is A).
		[Token(Token = "0x6000373")]
		[Address(RVA = "0x366230", Offset = "0x364C30", VA = "0x180366230")]
		get
		{
			return NaniVarManager.TryGetVariableValue<bool>(EnableDebugSkipVarName, out bool enabled) && enabled;
		}
	}

	[Token(Token = "0x1700007B")]
	public override float RevealProgress
	{
		[Token(Token = "0x6000374")]
		[Address(RVA = "0x366350", Offset = "0x364D50", VA = "0x180366350", Slot = "96")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6000375")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "97")]
		set
		{
		}
	}

	[Token(Token = "0x1700007C")]
	public override string Appearance
	{
		[Token(Token = "0x6000376")]
		[Address(RVA = "0x366360", Offset = "0x364D60", VA = "0x180366360", Slot = "98")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000377")]
		[Address(RVA = "0x366370", Offset = "0x364D70", VA = "0x180366370", Slot = "99")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x6000378")]
	[Address(RVA = "0x3663D0", Offset = "0x364DD0", VA = "0x1803663D0", Slot = "4")]
	protected override void Awake()
	{
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3666F0, 229 B
	// `base.OnDestroy()` (direct call to `UITextPrinterPanel.OnDestroy`),
	// then, if `_revealCts` (`+0x180`) is non-null: `Cancel()`, `Dispose()`,
	// null. The `Cancel()` is inlined: same `data_30DEB5A` class-init flag,
	// same `+0x28` disposed check into `ThrowObjectDisposedException`, same
	// `NotifyCancellation(false)` as an independent disassembly of
	// `CancellationTokenSource.Cancel()` (RVA 0x10A23C0). `RevealMessages`
	// overwrites `_revealCts` without cancelling or disposing the previous
	// one; among the methods examined so far, this is the only cleanup.
	// (`Awake`/`Initialize`/`SetMessages`/`AddMessage`/`SetFont` are still
	// unexamined.)
	[Token(Token = "0x6000379")]
	[Address(RVA = "0x3666F0", Offset = "0x3650F0", VA = "0x1803666F0", Slot = "8")]
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (_revealCts != null)
		{
			_revealCts.Cancel();
			_revealCts.Dispose();
			_revealCts = null;
		}
	}

	[Token(Token = "0x600037A")]
	[Address(RVA = "0x3667E0", Offset = "0x3651E0", VA = "0x1803667E0", Slot = "83")]
	public override Naninovel.UniTask Initialize()
	{
		return default(Naninovel.UniTask);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x366B20, 1415 B
	// - `_revealCts` cancel/dispose/null, byte-identical to `OnDestroy`'s
	//   block (inlined `Cancel()`, same `data_30DEB5A` flag).
	// - `foreach` over `_labels` setting `alpha = 0f` (inlined
	//   `TMP_Text.set_alpha`, same shape as `SetText`/`AddText`). The array
	//   is loaded into a register once before the loop and never re-read
	//   from the field, which is how C# compiles `foreach` over an array
	//   (it caches the array in a hidden local), unlike the `for` loops in
	//   `SetText`/`AddText` that reload `_labels` every pass.
	// - `Messages.ReplaceWith(messages)`.
	// - `messages.Count` (`IReadOnlyCollection<T>` index 0) is dispatched
	//   once and then compared against 0 and 1, so the source evaluates it
	//   once (a `switch`); two `messages.Count` expressions would need two
	//   interface calls. 0 -> `SetText("")` (a literal `""` load, not the
	//   `String.Empty` static read seen in `SetText`); 1 ->
	//   `SetText(FormatMessage(messages[0]))` (`IReadOnlyList<T>` index 0 =
	//   indexer, index argument 0; `FormatMessage` is `UITextPrinterPanel`
	//   Slot 114 at `klass+0x858`); otherwise `_builder.Length = 0`, a
	//   `foreach` over `messages` (enumerator disposed via `IDisposable` in
	//   the finally path) appending `FormatMessage(message)` to `_builder`,
	//   then `SetText(_builder.ToString())` (`StringBuilder` vtable Slot 3).
	//   Nothing is appended between messages, so any line break must come
	//   from `FormatMessage` itself.
	// The three branches reach one native `SetText` call site; whether the
	// source had one call per case (merged by the C++ backend) or a shared
	// local is not discriminable (B), so the per-case form is written.
	// This is the confirmed caller of `SetText`.
	[Token(Token = "0x600037B")]
	[Address(RVA = "0x366B20", Offset = "0x365520", VA = "0x180366B20", Slot = "107")]
	public override void SetMessages(IReadOnlyList<PrintedMessage> messages)
	{
		if (_revealCts != null)
		{
			_revealCts.Cancel();
			_revealCts.Dispose();
			_revealCts = null;
		}
		foreach (DebateText label in _labels)
		{
			label.alpha = 0f;
		}
		Messages.ReplaceWith(messages);
		switch (messages.Count)
		{
		case 0:
			SetText("");
			break;
		case 1:
			SetText(FormatMessage(messages[0]));
			break;
		default:
			_builder.Length = 0;
			foreach (PrintedMessage message in messages)
			{
				_builder.Append(FormatMessage(message));
			}
			SetText(_builder.ToString());
			break;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3670B0, 191 B
	// `Messages.Add(message)` (`List<PrintedMessage>.Add`), then
	// `AddText(FormatMessage(message))` (`FormatMessage` = Slot 114, direct
	// call to `AddText`). Unlike `SetMessages` it neither cancels the
	// current reveal nor resets label alpha. This is the confirmed caller of
	// `AddText`. Combined with `SetMessages -> SetText`, an `AddMessage`
	// after a partially-filled `SetMessages` hits the index mismatch noted
	// on `SetText`; whether Naninovel's base printer ever makes that call
	// sequence is third-party behavior not examined here.
	[Token(Token = "0x600037C")]
	[Address(RVA = "0x3670B0", Offset = "0x365AB0", VA = "0x1803670B0", Slot = "108")]
	public override void AddMessage(PrintedMessage message)
	{
		Messages.Add(message);
		AddText(FormatMessage(message));
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x367170, 1069 B
	//
	// `Messages` (the base `UITextPrinterPanel` property this reads/writes
	// throughout, `protected virtual List<PrintedMessage> Messages { get; }`,
	// `[CompilerGenerated]`, a trivial field-read getter -- see
	// `UITextPrinterPanel.cs`) is dispatched through FOUR separate `call`
	// instructions in this function. That count does not map 1:1 onto
	// source-level occurrences of `Messages`: because the getter is a
	// trivial `[CompilerGenerated]` field read, IL2CPP's C++ backend (MSVC)
	// is free to elide/merge redundant dispatches within a basic block, so
	// the call count is a codegen artifact, not evidence for how many times
	// (or into how many locals) the original source referenced `Messages`.
	// Confidence A covers the BEHAVIOR below; the exact number of source-level
	// `Messages` references is not discriminable from these bytes and isn't
	// asserted.
	//
	// - `if (Messages.Count == 0)`: if there are no existing messages yet,
	//   this dispatches straight to this same class's own `AddMessage`
	//   override (vtable Slot 108, confirmed via the `klass+0x7f8/+0x800`
	//   VirtualInvokeData offsets = `0x138 + 108*16`) with a freshly
	//   constructed `PrintedMessage` -- confirmed by the inlined
	//   struct-construction pattern (three 8-byte fields written directly:
	//   `text`, `author.id`, `author.label`, matching `PrintedMessage`'s
	//   `{LocalizableText text; MessageAuthor author;}` layout and
	//   `MessageAuthor`'s `{string id; LocalizableText label;}`), no `.ctor`
	//   call (trivial constructors are inlined project-wide).
	// - Otherwise: rents a pooled `List<PrintedMessage>` (`using
	//   (CollectionPool<List<PrintedMessage>, PrintedMessage>.Rent(out ...))`
	//   -- the `Rent(out TCollection)` overload confirmed by signature in
	//   `CollectionPool.cs`, its `PooledObject<T>` disposal confirmed by the
	//   static-init block's `PooledObject<List<PrintedMessage>>.System
	//   .IDisposable.Dispose()` MethodInfo reference plus the `lea rcx,
	//   [rsp+0x30]` receiver address matching the `PooledObject` struct's own
	//   stack slot -- order+dispatch is Confidence A, whether the C# is a
	//   `using` statement or hand-written `try/finally` is Confidence B, same
	//   caveat as `StageCameraController.ModifyCameraAsync`). Inside:
	//     - Reads the last message (`Messages[Messages.Count - 1]`) and
	//       replaces it with `new PrintedMessage(lastMessage.Text + text)`
	//       (`Naninovel.LocalizableText$$op_Addition`, confirmed by name).
	//       **The replacement message's `author` fields are explicitly
	//       zeroed** (`[rsp+0x58]`/`[rsp+0x60]` both set from a zero
	//       register before the struct copy, matching `PrintedMessage`'s
	//       `author.id`/`author.label` slots) -- i.e. appending text to a
	//       message DISCARDS its author, same as the `Count == 0` branch's
	//       fresh message (`[Optional] MessageAuthor author` defaulting to
	//       `default(MessageAuthor)` in both branches, confirmed by the
	//       identical zero-fill pattern at both call sites). If the printer
	//       renders a speaker label from `PrintedMessage.Author`, appending
	//       text to an existing message would visibly drop that label.
	//     - Copies the (now-updated) `Messages` into the pooled list
	//       (`Naninovel.LinqUtils.ReplaceWith<PrintedMessage>()`, an
	//       `ICollection<T>.ReplaceWith(IEnumerable<T>)` extension confirmed
	//       by signature in `LinqUtils.cs`) and passes that pooled list to
	//       `SetMessages` (this class's own override, Slot 107, `klass+0x7e8`
	//       confirmed) -- i.e. `Messages` is mutated in place and then
	//       resubmitted as a whole via `SetMessages`, presumably to trigger
	//       whatever change-notification `SetMessages` (still to examine
	//       elsewhere in the base class) performs, avoiding an extra
	//       allocation for the temporary copy via the pool.
	[Token(Token = "0x600037D")]
	[Address(RVA = "0x367170", Offset = "0x365B70", VA = "0x180367170", Slot = "109")]
	public override void AppendText(Naninovel.LocalizableText text)
	{
		if (Messages.Count == 0)
		{
			AddMessage(new PrintedMessage(text));
			return;
		}
		using (CollectionPool<List<PrintedMessage>, PrintedMessage>.Rent(out List<PrintedMessage> pooledList))
		{
			PrintedMessage lastMessage = Messages[Messages.Count - 1];
			Messages[Messages.Count - 1] = new PrintedMessage(lastMessage.Text + text);
			pooledList.ReplaceWith(Messages);
			SetMessages(pooledList);
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3675A0 (383 B, entry) + 0x368E80 (2411 B, MoveNext)
	//
	// Entry is the standard `AsyncUniTaskMethodBuilder.Start<>()` kickoff.
	// `<RevealMessages>d__30` hoists only `<>4__this` and `token` -- there is
	// NO `delay` field, so **the `delay` argument is never read**: the
	// per-label stagger below is a hard-coded `i * 0.2f` (constant
	// `data_2AE2670`, read from the PE as the float 0.2). One await point
	// (`AwaitUnsafeOnCompleted<Cysharp...UniTask.Awaiter, ...>`).
	//
	// - `_revealCts` (`+0x180`) = `CreateLinkedTokenSource` over a freshly
	//   allocated 1-element `CancellationToken[]` holding
	//   `token.CancellationToken` (the state machine's `token+0x0`, the
	//   `AsyncToken` getter being a first-field read) -- i.e. the
	//   `params CancellationToken[]` overload. The previous `_revealCts` is
	//   overwritten with no `Cancel()`/`Dispose()` in this method.
	// - `_layoutGroup.spacing` = per-locale spacing: `_labelSpaces`
	//   (`IDictionary<LocaleKind, float>`) interface method index 7
	//   (`TryGetValue`) keyed by `LocalizationProvider.CurrentLocale`
	//   (`ILocalizationProvider` index 1 = `get_CurrentLocale`, per
	//   dump.cs), explicit `0f` when the key is missing. The
	//   `LayoutGroup.SetProperty<float>(ref [+0x60], value)` call is a
	//   byte-for-byte inlining of `HorizontalOrVerticalLayoutGroup
	//   .set_spacing(float)` (RVA 0x247FD50, disassembled). `_layoutGroup`
	//   is loaded before the provider/dictionary calls, so this is one
	//   assignment statement, not a precomputed local.
	// - `await _autoLayoutSupporter.ExecuteRebuildingAsync(_revealCts.Token)`
	//   (GigaCreation `AutoLayoutSupporter`, returns a Cysharp `UniTask`).
	// - After resuming: `_labels.Where(b__30_0).ToArray()`, where
	//   `<RevealMessages>b__30_0` (RVA 0x368960, disassembled) is a straight
	//   tail-jump into the `Behaviour.get_enabled` icall (same
	//   `data_2604C08` descriptor as `AddText`'s lambda) with NO negation --
	//   the opposite filter from `AddText`: this reveals the labels that are
	//   currently ENABLED.
	// - Per label: `IsEnabledDebugSkip` (inlined, re-evaluated every
	//   iteration) chooses `0f` vs `i * 0.2f`; that value is computed before
	//   the array element is read, so it's a separate local. Then
	//   `WatAndRevealAsync(label, labelDelay, _revealCts.Token)` is
	//   fire-and-forget: the call goes straight into
	//   `Naninovel.UniTaskExtensions.ForgetCore`, which is exactly what
	//   `Naninovel...Forget(UniTask)` (RVA 0x51AD40, 7 bytes: `xor edx,edx;
	//   jmp ForgetCore`) inlines to. So this method's own task finishes as
	//   soon as every label's reveal has been STARTED, not when they end.
	[Token(Token = "0x600037E")]
	[Address(RVA = "0x3675A0", Offset = "0x365FA0", VA = "0x1803675A0", Slot = "111")]
	[AsyncStateMachine(typeof(_003CRevealMessages_003Ed__30))]
	public override async Naninovel.UniTask RevealMessages(float delay, AsyncToken token)
	{
		_revealCts = CancellationTokenSource.CreateLinkedTokenSource(token.CancellationToken);
		_layoutGroup.spacing = _labelSpaces.TryGetValue(LocalizationProvider.CurrentLocale, out float space) ? space : 0f;
		await _autoLayoutSupporter.ExecuteRebuildingAsync(_revealCts.Token);
		DebateText[] labels = _labels.Where((DebateText label) => label.enabled).ToArray();
		for (int i = 0; i < labels.Length; i++)
		{
			float labelDelay = IsEnabledDebugSkip ? 0f : i * 0.2f;
			WatAndRevealAsync(labels[i], labelDelay, _revealCts.Token).Forget();
		}
	}

	[Token(Token = "0x600037F")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "112")]
	public override void SetWaitForInputIndicatorVisible(bool visible)
	{
	}

	[Token(Token = "0x6000380")]
	[Address(RVA = "0x367720", Offset = "0x366120", VA = "0x180367720", Slot = "84")]
	public override void SetFont(TMP_FontAsset font)
	{
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x367AB0, 865 B
	//
	// Shares its opening shape with `AddText` below (same inlined
	// `IsNullOrWhiteSpace` + `Split("<br>")` sequence, byte-identical), but
	// diverges in two confirmed ways:
	//
	//   - The bounds check compares `lines.Length` against `_labels.Length`
	//     directly (`[r13+0x150]`, this class's raw `_labels` field) --
	//     there is no `.Where`/LINQ call here at all (no `<>c` cached-lambda
	//     class-init check anywhere in this function, unlike `AddText`), so
	//     this method does NOT filter for currently-disabled labels; it
	//     always walks the full `_labels` array.
	//   - The warning string is a DIFFERENT interned `System.String` object
	//     than `AddText`'s (confirmed: the two `lea rcx, [rip+...]`
	//     references resolve to distinct data-section RVAs, 0x2E59640 here
	//     vs. 0x2E59610 in `AddText`, extracted via the project's own
	//     `il2cpp_db` string table -- not eyeballed from the disassembler's
	//     raw/garbled terminal rendering of the UTF-16 bytes). The text is
	//     "渡されたメッセージの行数がラベル数を超えています。" ("the number of
	//     lines in the received message exceeds the number of labels") --
	//     notably missing `AddText`'s "未使用" ("unused") qualifier, which is
	//     independent confirmation that this method reasons about the total
	//     label count, not the free-slot count.
	//
	// The per-index body is byte-identical in shape to `AddText`'s (same
	// `Behaviour.set_enabled`/`TMP_Text.set_alpha`/vtable-Slot-66 `text`
	// setter dispatches, same "empty string" fallback via `data_30D76E0+0xb8`
	// for `i >= lines.Length`), but the loop here runs over ALL of
	// `_labels`, not a `.Where`-filtered subset -- `hasLine = i <
	// lines.Length` decides per-slot enable/fill instead of a prefix cutoff.
	//
	// **Cross-method observation** (not asserted as a live bug -- no caller
	// evidence yet for either method): if `SetText` runs first with fewer
	// lines than `_labels.Length`, it leaves `_labels[0..lines.Length)`
	// enabled and `_labels[lines.Length..)` disabled -- i.e. the
	// currently-free (disabled) labels after a `SetText` call are a SUFFIX
	// of the array, not a prefix. `AddText`'s loop, however, writes to
	// `_labels[0]`, `_labels[1]`, ... in order regardless of where the
	// `.Where(!enabled)`-filtered slots actually live. If `AddText` is ever
	// called against a `_labels` array left in that post-`SetText` state,
	// its writes would land on the WRONG (already-enabled, in-use) slots
	// instead of the free suffix. This is exactly the mismatch flagged as
	// "unverified" in `AddText`'s own comment; reconstructing `SetText` did
	// not resolve whether it happens in practice (that needs `SetText`'s
	// and `AddText`'s actual call sites), but it does show the two methods'
	// index conventions are NOT compatible with each other's, not just with
	// AddText's own loop bound.
	[Token(Token = "0x6000381")]
	[Address(RVA = "0x367AB0", Offset = "0x3664B0", VA = "0x180367AB0")]
	private void SetText(string text)
	{
		string[] lines = string.IsNullOrWhiteSpace(text) ? Array.Empty<string>() : text.Split("<br>");
		if (lines.Length > _labels.Length)
		{
			UnityEngine.Debug.LogWarning("渡されたメッセージの行数がラベル数を超えています。");
			return;
		}
		for (int i = 0; i < _labels.Length; i++)
		{
			bool hasLine = i < lines.Length;
			_labels[i].enabled = hasLine;
			_labels[i].alpha = 0f;
			_labels[i].text = hasLine ? lines[i] : string.Empty;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x367E20, 1322 B
	//
	// Every third-party API surface touched below (Behaviour.enabled,
	// TMP_Text.alpha, the text setter) is confirmed by independently
	// disassembling that exact member and matching bytes -- not inferred
	// from a plausible-sounding name -- per this project's inlining
	// discipline:
	//
	//   - `string.IsNullOrWhiteSpace(text)`: the inlined null-check +
	//     char-by-char `System.Char.IsWhiteSpace` loop at the top of this
	//     function is structurally identical (down to sharing the SAME
	//     `data_30D76D8` class-init flag) to an independent disassembly of
	//     `System.String.IsNullOrWhiteSpace` itself (RVA 0xE25060).
	//   - `text.Split("<br>")`: not just an overload-signature match --
	//     the call site is a COMPLETE inlining of `String.Split(string
	//     separator, StringSplitOptions options = 0)` (RVA 0xE29360)
	//     itself, confirmed by disassembling that wrapper and finding the
	//     exact same instruction sequence, including its (here dead, since
	//     "<br>" is a non-null literal) internal null-separator guard --
	//     which resolved what `data_30D76E0 + 0xb8` actually is (see the
	//     `text` line below).
	//   - `_labels.Where(label => !label.enabled).ToArray()`: the
	//     `.Where` predicate delegate is cached in the standard compiler
	//     `<>c.<>9__34_0` static-field pattern (same shape as `PrintDebateText
	//     .<Print>b__21_0/1` from this same round). Its body,
	//     `<AddText>b__34_0(DebateText label)` (RVA 0x368A20), is confirmed
	//     byte-for-byte identical (same `data_2604C08` cached-icall
	//     descriptor, same `MarshalNotNull<Behaviour>` class-init flag) to
	//     an independent disassembly of `UnityEngine.Behaviour.get_enabled`
	//     (RVA 0x2060B80), followed by a `sete al` negation -- i.e. this
	//     collects the currently-DISABLED labels (the "free" slots this
	//     call is about to repurpose), not the visible ones.
	//   - If the incoming message has more lines than there are free label
	//     slots, this logs a warning (using the exact literal string read
	//     from the data section: "渡されたメッセージの行数が未使用ラベル数を
	//     超えています。" -- "the number of lines in the received message
	//     exceeds the number of unused labels") and returns without
	//     touching any label.
	//   - Otherwise, for `i` in `[0, availableLabels.Length)` -- **indexing
	//     `_labels[i]` directly, not `availableLabels[i]`, per the
	//     disassembly (`0x36813F`/`0x368221` both reload `this._labels`,
	//     never the `[rsp+0x98]` `ToArray()` result, inside the loop body;
	//     only the loop BOUND at `0x368130` comes from
	//     `availableLabels.Length`)**. This is a genuine mismatch between
	//     "how many free slots exist" and "which slots get written," not a
	//     misread on my part -- if the free (disabled) labels are not a
	//     `_labels[0..count)` prefix, this call would silently overwrite a
	//     label that's currently showing text. Whether that can actually
	//     happen depends on whether every caller resets all of `_labels`
	//     to disabled before repopulating (plausible for the sibling
	//     `SetText`, still a stub) -- unverified, so this is recorded as a
	//     bytes-confirmed behavioral finding, not explained away. Three
	//     writes happen UNCONDITIONALLY each iteration (the apparent `if`s
	//     around them in the disassembly are internal to the inlined
	//     setters below, not source-level branches on `hasLine`):
	//       - `_labels[i].enabled = hasLine` (`hasLine = i < lines.Length`)
	//         -- confirmed byte-for-byte against an independent
	//         disassembly of `Behaviour.set_enabled(bool)` (RVA 0x2060C20;
	//         same `MarshalNotNull<Behaviour>`/`data_30E84C6`/
	//         `data_30E84D0`/`data_2604C50` flags throughout).
	//       - `_labels[i].alpha = 0f` -- confirmed byte-for-byte against an
	//         independent disassembly of `TMP_Text.set_alpha(float)` (RVA
	//         0x1EFFDE0); that setter's OWN internal no-op guard (skip if
	//         the field already equals the new value) is exactly the
	//         `ucomiss`/`jp`/`je` shape seen at this call site, so no
	//         separate source-level `if` is needed here.
	//       - `_labels[i].text = hasLine ? lines[i] : string.Empty` -- the
	//         write dispatches through vtable Slot 66, which is
	//         `NaninovelTMProText`'s own `text` setter override (this
	//         file's neighbor `NaninovelTMProText.cs`, Slot 66). The
	//         "no line for this slot" fallback reads through
	//         `data_30D76E0 + 0xb8` -- the SAME address confirmed above to
	//         be `String.Split`'s own internal null-separator substitute,
	//         i.e. a generic "the empty string" static reference, not a
	//         reuse of the "<br>" literal (an earlier pass misread this as
	//         "<br>" reuse before checking `Split`'s own bytes).
	//   - `.enabled`/`.alpha` are left at "on, invisible" for filled slots;
	//     the actual fade-in is presumably owned by this class's own
	//     `RevealMessages`/`WatAndRevealAsync` (still stubs), not by this
	//     method.
	[Token(Token = "0x6000382")]
	[Address(RVA = "0x367E20", Offset = "0x366820", VA = "0x180367E20")]
	private void AddText(string text)
	{
		string[] lines = string.IsNullOrWhiteSpace(text) ? Array.Empty<string>() : text.Split("<br>");
		DebateText[] availableLabels = _labels.Where((DebateText label) => !label.enabled).ToArray();
		if (lines.Length > availableLabels.Length)
		{
			UnityEngine.Debug.LogWarning("渡されたメッセージの行数が未使用ラベル数を超えています。");
			return;
		}
		for (int i = 0; i < availableLabels.Length; i++)
		{
			bool hasLine = i < lines.Length;
			_labels[i].enabled = hasLine;
			_labels[i].alpha = 0f;
			_labels[i].text = hasLine ? lines[i] : string.Empty;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x368350 (427 B, entry) + 0x3697F0 (1409 B, MoveNext)
	//
	// Two await points: state 0 holds a Naninovel `UniTask.Awaiter`
	// (`<>u__1`), state 1 a Cysharp `UniTask.Awaiter` (`<>u__2`). All three
	// parameters are hoisted and used.
	// - `Naninovel.UniTask.Delay(TimeSpan.FromSeconds(delay), ...)`: the call
	//   passes `ignoreTimeScale = false` (edx=0), `delayTiming = 8`
	//   (`Naninovel.PlayerLoopTiming.Update`, the 9th member) and `ct`. The
	//   first two are that overload's declared defaults, so only
	//   `cancellationToken:` is written.
	// - Then a 0 -> 1 alpha tween: `LMotion.Create(0f, 1f, 0.4f)`. The call
	//   site hits the generic `Create<float, NoOptions, FloatMotionAdapter>
	//   (in from, in to, duration)`, which is exactly what the non-generic
	//   `LMotion.Create(float, float, float)` forwards to (RVA 0xDA06B0,
	//   disassembled). `from` = 0f, `to` = 1f, duration `data_2AE26EC` read
	//   from the PE as 0.4. Then `.BindToColorA(label)` (the `TMP_Text`
	//   overload per the MethodInfo; its native body is folded with
	//   `BindToFontSize` and others) and `.ToUniTask(ct)` (the 2-parameter
	//   overload, RVA 0xD83990 exactly). `BindToColorA` lives in
	//   `LitMotion.Extensions.dll`, which this project did not reference
	//   until now.
	// Each label therefore waits its stagger, then fades in over 0.4 s; the
	// caller (`RevealMessages`) forgets the returned task.
	[Token(Token = "0x6000383")]
	[Address(RVA = "0x368350", Offset = "0x366D50", VA = "0x180368350")]
	[AsyncStateMachine(typeof(_003CWatAndRevealAsync_003Ed__35))]
	private static async Naninovel.UniTask WatAndRevealAsync(DebateText label, float delay, [Optional] CancellationToken ct)
	{
		await Naninovel.UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: ct);
		await LMotion.Create(0f, 1f, 0.4f).BindToColorA(label).ToUniTask(ct);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x368500 (281 B, entry) + 0x368AC0 (953 B, MoveNext)
	//
	// One await point. Same `Gosub` construction already established in
	// `GosubToObjectionCutIn`/`GosubToModifyPerspectiveStageCamera`:
	// allocate plus inlined base `Command..ctor`, `NamedString` built via its
	// inlined `: base(name, value)` into `Named<NullableString>..ctor`,
	// stored to `Gosub.Path` (`+0x30`), then a vtable dispatch at
	// `klass+0x188` (Slot 5, `Execute(AsyncToken)`) with a zeroed 16-byte
	// `AsyncToken`. Differences from those commands: the `name` argument is
	// null (`xor edx, edx` into the first parameter) with `id` as the
	// `value`, rather than a `System/System_Subroutine` path; and this
	// helper is itself `async` and awaits the Gosub instead of tail-calling
	// it. (A null name conventionally means "the current script" in
	// Naninovel; that part is not verified from these bytes.)
	[Token(Token = "0x6000384")]
	[Address(RVA = "0x368500", Offset = "0x366F00", VA = "0x180368500")]
	[AsyncStateMachine(typeof(_003CGosub_003Ed__36))]
	private static async Naninovel.UniTask Gosub(string id)
	{
		Naninovel.Commands.Gosub gosub = new Naninovel.Commands.Gosub
		{
			Path = new NamedString(null, id)
		};
		await gosub.Execute(default(AsyncToken));
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x368620, 181 B
	// Allocates a `StringBuilder`, stores it to `_builder` (`+0x160`), and
	// only then tail-jumps into `UITextPrinterPanel..ctor`. A field
	// initializer runs before the base constructor call, while an assignment
	// in the constructor body would run after it, so this is the
	// `= new StringBuilder()` initializer on the `_builder` declaration; the
	// constructor body itself is empty.
	[Token(Token = "0x6000385")]
	[Address(RVA = "0x368620", Offset = "0x367020", VA = "0x180368620")]
	public DebateTextPrinterPanel()
	{
	}
}
