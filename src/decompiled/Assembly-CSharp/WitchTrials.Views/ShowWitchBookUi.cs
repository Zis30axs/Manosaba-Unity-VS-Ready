using GigaCreation.NaninovelExtender.Ui;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x200006F")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("book")]
public class ShowWitchBookUi : Command
{
	[StructLayout(3)]
	[Token(Token = "0x2000070")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__18
{
		[Token(Token = "0x4000182")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000183")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000184")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ShowWitchBookUi _003C_003E4__this;

		[Token(Token = "0x4000185")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4000186")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600016C")]
		[Address(RVA = "0x32C030", Offset = "0x32AA30", VA = "0x18032C030", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600016D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000071")]
	[CompilerGenerated]
	private struct _003CShowWitchBook_003Ed__19
{
		[Token(Token = "0x4000187")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000188")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000189")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ShowWitchBookUi _003C_003E4__this;

		[Token(Token = "0x400018A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public WitchBookPresentationMode mode;

		[Token(Token = "0x400018B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public IReadOnlyList<WitchBookCategory> categories;

		[Token(Token = "0x400018C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public IReadOnlyCollection<MapAreaKind> areas;

		[Token(Token = "0x400018D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public AsyncToken token;

		[Token(Token = "0x400018E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600016E")]
		[Address(RVA = "0x32C3F0", Offset = "0x32ADF0", VA = "0x18032C3F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600016F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400017A")]
	private const string EnableDebugSkipVarName = "enableDebugSkip";

	[Token(Token = "0x400017B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	public StringListParameter CategoryNames;

	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x32BF50, 221 B
	// The constructor is only the field initializers: `BooleanParameter.op_Implicit(false)`
	// into +0x38 `Back` and again into +0x40 `Global` (each with a write barrier), then a
	// tail jump to `Command..ctor`.
	[Token(Token = "0x400017C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterDefaultValue("false")]
	public BooleanParameter Back = false;

	[Token(Token = "0x400017D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterDefaultValue("false")]
	public BooleanParameter Global = false;

	[Token(Token = "0x400017E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private IWitchTrialsNaninovelManager _witchTrialsNaninovelManager;

	[Token(Token = "0x400017F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private ICustomVariableManager _naniVarManager;

	[Token(Token = "0x4000180")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private IUIManager _naniUiManager;

	[Token(Token = "0x4000181")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private IWitchBookUi _witchBookUi;

	[Token(Token = "0x1700001E")]
	protected IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x32B760, 186 B
		// Lazy cache in +0x48 via `ServiceLocator.Get<IWitchTrialsNaninovelManager>(true)`
		// (`mov cl, 1`: includeDerivedType).
		[Token(Token = "0x6000164")]
		[Address(RVA = "0x32B760", Offset = "0x32A160", VA = "0x18032B760")]
		get
		{
			return _witchTrialsNaninovelManager ??= GigaCreation.Tools.Service.ServiceLocator.Get<IWitchTrialsNaninovelManager>(true);
		}
	}

	[Token(Token = "0x1700001F")]
	protected ICustomVariableManager NaniVarManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x32B820
		// Lazy cache in +0x50 via `Engine.GetServiceOrErr<ICustomVariableManager>()`.
		[Token(Token = "0x6000165")]
		[Address(RVA = "0x32B820", Offset = "0x32A220", VA = "0x18032B820")]
		get
		{
			return _naniVarManager ??= Engine.GetServiceOrErr<ICustomVariableManager>();
		}
	}

	[Token(Token = "0x17000020")]
	protected IUIManager NaniUiManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x32B8E0
		// Lazy cache in +0x58 via `Engine.GetServiceOrErr<IUIManager>()`.
		[Token(Token = "0x6000166")]
		[Address(RVA = "0x32B8E0", Offset = "0x32A2E0", VA = "0x18032B8E0")]
		get
		{
			return _naniUiManager ??= Engine.GetServiceOrErr<IUIManager>();
		}
	}

	[Token(Token = "0x17000021")]
	protected IWitchBookUi WitchBookUi
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x32B9A0
		// Lazy cache in +0x60 via `NaniUiManager.GetUI<IWitchBookUi>()` (getter inlined).
		[Token(Token = "0x6000167")]
		[Address(RVA = "0x32B9A0", Offset = "0x32A3A0", VA = "0x18032B9A0")]
		get
		{
			return _witchBookUi ??= NaniUiManager.GetUI<IWitchBookUi>();
		}
	}

	[Token(Token = "0x17000022")]
	protected bool IsEnabledDebugSkip
	{
		// RECONSTRUCTED | Confidence: A (`&&` spelling: B) | RVA: 0x32BB10
		// `NaniVarManager.TryGetVariableValue<bool>("enableDebugSkip", out value)`, then `and al, value`:
		// the native compiler folds the short-circuit over a plain local into a bitwise and.
		[Token(Token = "0x6000168")]
		[Address(RVA = "0x32BB10", Offset = "0x32A510", VA = "0x18032BB10")]
		get
		{
			return NaniVarManager.TryGetVariableValue(EnableDebugSkipVarName, out bool value) && value;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x32BB80 (stub), MoveNext 0x32C030
	// `Global` (+0x40) through Nullable<bool> op_Implicit -> `setne` = mode (1 Global / 0 Local); categories =
	// Assigned(CategoryNames) (ICommandParameter slot 0) ? ParseCategories(string[] implicit) : null; areas null;
	// awaits ShowWitchBook (0x32BD00, real call). `Back` (+0x38) is not read here.
	[Token(Token = "0x6000169")]
	[Address(RVA = "0x32BB80", Offset = "0x32A580", VA = "0x18032BB80", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__18))]
	public override async Naninovel.UniTask Execute([Optional] AsyncToken token)
	{
		await ShowWitchBook(Global ? WitchBookPresentationMode.Global : WitchBookPresentationMode.Local, Assigned(CategoryNames) ? WitchBookDataHelper.ParseCategories(CategoryNames) : null, null, token);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x32BD00 (stub), MoveNext 0x32C3F0
	// WitchBookUi getter per call; IWitchBookUi slot 0 SetPresentationMode(mode), slot 1
	// SetRestrictionCategories(categories), slot 2 SetRestrictionMapAreas(areas); then ManagedUiExtensions.PresentAsync
	// (AsyncToken overload, kickoff inlined: duration null, token) awaited through the Cysharp awaiter.
	[Token(Token = "0x600016A")]
	[Address(RVA = "0x32BD00", Offset = "0x32A700", VA = "0x18032BD00")]
	[AsyncStateMachine(typeof(_003CShowWitchBook_003Ed__19))]
	protected async Naninovel.UniTask ShowWitchBook(WitchBookPresentationMode mode, IReadOnlyList<WitchBookCategory> categories, IReadOnlyCollection<MapAreaKind> areas, AsyncToken token)
	{
		WitchBookUi.SetPresentationMode(mode);
		WitchBookUi.SetRestrictionCategories(categories);
		WitchBookUi.SetRestrictionMapAreas(areas);
		await WitchBookUi.PresentAsync(null, token);
	}

	[Token(Token = "0x600016B")]
	[Address(RVA = "0x32BF50", Offset = "0x32A950", VA = "0x18032BF50")]
	public ShowWitchBookUi()
	{
	}
}
