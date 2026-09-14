using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using FastEnumUtility;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x200004A")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("loadStage")]
public class LoadPerspectiveStage : Command
{
	[StructLayout(3)]
	[Token(Token = "0x200004B")]
	[CompilerGenerated]
	private struct _003CLoadAsync_003Ed__6
{
		[Token(Token = "0x40000E5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000E6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000E7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public LoadPerspectiveStage _003C_003E4__this;

		[Token(Token = "0x40000E8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40000E9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000F6")]
		[Address(RVA = "0x31F610", Offset = "0x31E010", VA = "0x18031F610", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000F7")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40000E2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	[RequiredParameter]
	public StringParameter Id;

	[Token(Token = "0x40000E3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public BooleanParameter Wait;

	[Token(Token = "0x40000E4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private IPerspectiveStageManager _perspectiveStageManager;

	[Token(Token = "0x17000012")]
	private IPerspectiveStageManager PerspectiveStageManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x31F320, 186 B
		// Lazy cache in +0x40. Unlike the screen and camera commands, the first
		// argument here is `xor ecx, ecx`, so `includeDerivedType` is false (the
		// default).
		[Token(Token = "0x60000F2")]
		[Address(RVA = "0x31F320", Offset = "0x31DD20", VA = "0x18031F320")]
		get
		{
			return _perspectiveStageManager ??= ServiceLocator.Get<IPerspectiveStageManager>();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x31F3E0, 171 B
	// `WaitOrForget` (vtable +0x208, slot 13) over `LoadAsync` with `Wait` (+0x38).
	[Token(Token = "0x60000F3")]
	[Address(RVA = "0x31F3E0", Offset = "0x31DDE0", VA = "0x18031F3E0", Slot = "5")]
	public override Naninovel.UniTask Execute([Optional] AsyncToken token)
	{
		return WaitOrForget(LoadAsync, Wait, token);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x31F490, 383 B (entry) + MoveNext 0x31F610
	// `Id` goes through the inlined StringParameter -> string conversion into
	// `FastEnum.TryParse<PerspectiveStageKind>(value, ignoreCase: true, out kind)`
	// (`dl = 1`; `kind` is a zero-initialized local). On failure the method goes
	// straight to SetResult: no warning, no load.
	// On success, the inlined getter feeds `sub_3D50` with the IPerspectiveStageLoader
	// TypeInfo and arguments (kind, token's CancellationToken). Of that
	// interface's two slots, only slot 0 `LoadStageAsync(PerspectiveStageKind,
	// CancellationToken)` takes arguments (slot 1 `UnloadStage()` takes none), so
	// the call is identified by elimination.
	[Token(Token = "0x60000F4")]
	[Address(RVA = "0x31F490", Offset = "0x31DE90", VA = "0x18031F490")]
	[AsyncStateMachine(typeof(_003CLoadAsync_003Ed__6))]
	private async Naninovel.UniTask LoadAsync(AsyncToken token)
	{
		if (FastEnum.TryParse<PerspectiveStageKind>((string)Id, true, out PerspectiveStageKind kind))
		{
			await PerspectiveStageManager.LoadStageAsync(kind, token.CancellationToken);
		}
	}

	[Token(Token = "0x60000F5")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public LoadPerspectiveStage()
	{
	}
}
