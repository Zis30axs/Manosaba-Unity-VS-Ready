using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace WitchTrials.Views;

[Token(Token = "0x200003B")]
[UsedImplicitly]
public class QuickSave : Command
{
	[StructLayout(3)]
	[Token(Token = "0x200003C")]
	[CompilerGenerated]
	private struct _003CDoQuickSaveAsync_003Ed__4
{
		[Token(Token = "0x40000A5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000A6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000A7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public QuickSave _003C_003E4__this;

		[Token(Token = "0x40000A8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private IDisposable _003C__003E5__2;

		[Token(Token = "0x40000A9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000CA")]
		[Address(RVA = "0x31AC30", Offset = "0x319630", VA = "0x18031AC30", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000CB")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40000A2")]
	public const string LastQuickSaveSlotNumberVarName = "lastQuickSaveSlotNumber";

	[Token(Token = "0x40000A3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	[RequiredParameter]
	[ParameterAlias("")]
	public IntegerParameter SlotNumber;

	[Token(Token = "0x40000A4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly]
	public BooleanParameter Wait;

	// RECONSTRUCTED | Confidence: A | RVA: 0x31AA60, 171 B
	// `new Func<AsyncToken, UniTask>(DoQuickSaveAsync)`, then vtable +0x208 =
	// slot 13 `Command.WaitOrForget(fn, Wait (+0x38), token)`.
	[Token(Token = "0x60000C7")]
	[Address(RVA = "0x31AA60", Offset = "0x319460", VA = "0x18031AA60", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return WaitOrForget(DoQuickSaveAsync, Wait, token);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x31AB10, 281 B (entry) + MoveNext 0x31AC30
	// `Show`'s last parameter is `params ICancelEventBlockable[]`, so the
	// `Array.Empty<ICancelEventBlockable>()` in the bytes is the compiler's
	// lowering of an omitted params argument.
	// - `Engine.GetServiceOrErr<ICustomVariableManager>().TrySetVariableValue<int>
	//   ("lastQuickSaveSlotNumber", SlotNumber)`, using the `Nullable<int>.op_Implicit`
	//   conversion of the parameter object.
	// - `using` (IDisposable at +0x20, disposed only when state < 0)
	//   `NaninovelLoadingScreen.Show(LoadingMode.Subtle (0), haltInput: false,
	//   Array.Empty<ICancelEventBlockable>())`:
	//   - `Engine.GetServiceOrErr<IUIManager>().GetUI<IClickThroughPanelExtended>()`
	//     slot 1 `DontBlockInteractionOnNext()`, with no null guard.
	//   - Await `Engine.GetServiceOrErr<IStateManagerExtended>()` slot 3
	//     `QuickSaveAsync(SlotNumber.Value)`; the result is discarded.
	// - `token` is not used.
	[Token(Token = "0x60000C8")]
	[Address(RVA = "0x31AB10", Offset = "0x319510", VA = "0x18031AB10")]
	[AsyncStateMachine(typeof(_003CDoQuickSaveAsync_003Ed__4))]
	private async UniTask DoQuickSaveAsync(AsyncToken token)
	{
		Engine.GetServiceOrErr<ICustomVariableManager>().TrySetVariableValue(LastQuickSaveSlotNumberVarName, (int)(Naninovel.Nullable<int>)SlotNumber);
		using (GigaCreation.NaninovelExtender.Common.NaninovelLoadingScreen.Show(GigaCreation.NaninovelExtender.Common.LoadingMode.Subtle, false))
		{
			Engine.GetServiceOrErr<IUIManager>().GetUI<GigaCreation.NaninovelExtender.Common.IClickThroughPanelExtended>().DontBlockInteractionOnNext();
			await Engine.GetServiceOrErr<GigaCreation.NaninovelExtender.Common.IStateManagerExtended>().QuickSaveAsync(SlotNumber.Value);
		}
	}

	[Token(Token = "0x60000C9")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public QuickSave()
	{
	}
}
