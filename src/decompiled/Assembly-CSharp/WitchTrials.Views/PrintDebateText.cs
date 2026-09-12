using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Il2CppDummyDll;
using YieldAwaitable = Naninovel.YieldAwaitable;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000055")]
[UsedImplicitly]
[CommandAlias("printDebate")]
public class PrintDebateText : PrintText
{
	[Token(Token = "0x2000057")]
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass19_0
	{
		[Token(Token = "0x4000123")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public PrintDebateText _003C_003E4__this;

		[Token(Token = "0x4000124")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ITextPrinterActor printer;

		[Token(Token = "0x6000127")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public _003C_003Ec__DisplayClass19_0()
		{
		}

		[Token(Token = "0x6000128")]
		[Address(RVA = "0x323A50", Offset = "0x322450", VA = "0x180323A50")]
		internal UniTask _003CExecute_003Eb__0(AsyncToken asyncToken)
		{
			return default(UniTask);
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000058")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__19
{
		[Token(Token = "0x4000125")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000126")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000127")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PrintDebateText _003C_003E4__this;

		[Token(Token = "0x4000128")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4000129")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private _003C_003Ec__DisplayClass19_0 _003C_003E8__1;

		[Token(Token = "0x400012A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<ITextPrinterActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400012B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000129")]
		[Address(RVA = "0x323A90", Offset = "0x322490", VA = "0x180323A90", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600012A")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000059")]
	[CompilerGenerated]
	private struct _003CPrint_003Ed__21
{
		[Token(Token = "0x400012C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400012D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400012E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PrintDebateText _003C_003E4__this;

		[Token(Token = "0x400012F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public ITextPrinterActor printer;

		[Token(Token = "0x4000130")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x4000131")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600012B")]
		[Address(RVA = "0x324180", Offset = "0x322B80", VA = "0x180324180", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600012C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200005A")]
	[CompilerGenerated]
	private struct _003CWaitForDebate_003Ed__25
{
		[Token(Token = "0x4000132")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000133")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000134")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PrintDebateText _003C_003E4__this;

		[Token(Token = "0x4000135")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float time;

		[Token(Token = "0x4000136")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x4000137")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private float _003CelapsedTime_003E5__2;

		[Token(Token = "0x4000138")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600012D")]
		[Address(RVA = "0x324E60", Offset = "0x323860", VA = "0x180324E60", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600012E")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000115")]
	private const string DebatePrinterId = "Debate";

	[Token(Token = "0x4000116")]
	private const string DebateWaitTimeVarName = "debateWaitTime";

	[Token(Token = "0x4000117")]
	private const string EnableDebugSkipVarName = "enableDebugSkip";

	[Token(Token = "0x4000118")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly Regex s_pattern1;

	[Token(Token = "0x4000119")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
	private static readonly Regex s_pattern2;

	[Token(Token = "0x400011A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private static readonly Regex s_linkIdRegex;

	[Token(Token = "0x400011B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	[UsedImplicitly]
	[ParameterAlias("pos")]
	public DecimalListParameter ScenePosition;

	[Token(Token = "0x400011C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	[UsedImplicitly]
	public DecimalParameter Roll;

	[Token(Token = "0x400011D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
	[UsedImplicitly]
	[ParameterAlias("anim")]
	public StringParameter AnimationKind;

	[Token(Token = "0x400011E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
	private ICustomVariableManager _naniVarManager;

	[Token(Token = "0x400011F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
	private IWitchTrialsScriptPlayer _witchTrialsScriptPlayer;

	[Token(Token = "0x17000018")]
	protected override string AssignedPrinterId
	{
		[Token(Token = "0x6000113")]
		[Address(RVA = "0x322230", Offset = "0x320C30", VA = "0x180322230", Slot = "16")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000019")]
	private ICustomVariableManager NaniVarManager
	{
		[Token(Token = "0x6000114")]
		[Address(RVA = "0x322270", Offset = "0x320C70", VA = "0x180322270")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700001A")]
	private IWitchTrialsScriptPlayer WitchTrialsScriptPlayer
	{
		[Token(Token = "0x6000115")]
		[Address(RVA = "0x322340", Offset = "0x320D40", VA = "0x180322340")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700001B")]
	private bool IsEnabledDebugSkip
	{
		[Token(Token = "0x6000116")]
		[Address(RVA = "0x322410", Offset = "0x320E10", VA = "0x180322410")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6000117")]
	[Address(RVA = "0x322480", Offset = "0x320E80", VA = "0x180322480", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__19))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000118")]
	[Address(RVA = "0x322610", Offset = "0x321010", VA = "0x180322610", Slot = "15")]
	protected override IDisposable CompleteOnContinue(ref AsyncToken token)
	{
		return null;
	}

	[Token(Token = "0x6000119")]
	[Address(RVA = "0x322810", Offset = "0x321210", VA = "0x180322810", Slot = "26")]
	[AsyncStateMachine(typeof(_003CPrint_003Ed__21))]
	protected override UniTask Print(ITextPrinterActor printer, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600011A")]
	[Address(RVA = "0x322A00", Offset = "0x321400", VA = "0x180322A00", Slot = "33")]
	protected override bool ShouldPlayAutoVoice()
	{
		return default(bool);
	}

	[Token(Token = "0x600011B")]
	[Address(RVA = "0x322AF0", Offset = "0x3214F0", VA = "0x180322AF0", Slot = "40")]
	protected override UniTask WaitAutoVoice(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600011C")]
	[Address(RVA = "0x322B40", Offset = "0x321540", VA = "0x180322B40")]
	private void ModifyDebatePrinter(IDebateTextPrinterActor printer)
	{
	}

	[Token(Token = "0x600011D")]
	[Address(RVA = "0x323180", Offset = "0x321B80", VA = "0x180323180")]
	[AsyncStateMachine(typeof(_003CWaitForDebate_003Ed__25))]
	private UniTask WaitForDebate(float time, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600011E")]
	[Address(RVA = "0x323330", Offset = "0x321D30", VA = "0x180323330")]
	private float CalculateWaitTime()
	{
		return default(float);
	}

	[Token(Token = "0x600011F")]
	[Address(RVA = "0x3234E0", Offset = "0x321EE0", VA = "0x1803234E0")]
	private static float CalculateWaitTimeFromMessage(string message)
	{
		return default(float);
	}

	[Token(Token = "0x6000120")]
	[Address(RVA = "0x323690", Offset = "0x322090", VA = "0x180323690")]
	public PrintDebateText()
	{
	}
}
