using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace WitchTrials.Views;

[Token(Token = "0x2000039")]
[UsedImplicitly]
public class QuickLoad : Command
{
	[StructLayout(3)]
	[Token(Token = "0x200003A")]
	[CompilerGenerated]
	private struct _003CDoQuickLoadAsync_003Ed__3
{
		[Token(Token = "0x400009D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400009E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400009F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public QuickLoad _003C_003E4__this;

		[Token(Token = "0x40000A0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private IDisposable _003C__003E5__2;

		[Token(Token = "0x40000A1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000C5")]
		[Address(RVA = "0x31A370", Offset = "0x318D70", VA = "0x18031A370", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000C6")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400009B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	[ParameterAlias("")]
	public IntegerParameter SlotNumber;

	[Token(Token = "0x400009C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly]
	public BooleanParameter Wait;

	// RECONSTRUCTED | Confidence: A | RVA: 0x31A060, 171 B
	// `new Func<AsyncToken, UniTask>(DoQuickLoadAsync)`, then vtable +0x208 =
	// slot 13 `Command.WaitOrForget(fn, Wait (+0x38), token)`.
	[Token(Token = "0x60000C1")]
	[Address(RVA = "0x31A060", Offset = "0x318A60", VA = "0x18031A060", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return WaitOrForget(DoQuickLoadAsync, Wait, token);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x31A110, 281 B (entry) + MoveNext 0x31A370
	// (The `Array.Empty<ICancelEventBlockable>()` in the bytes is the lowering of
	// `Show`'s omitted `params` argument.)
	// - The inlined `TryGetSlotNumber(out slotNumber)` (same bytes as 0x31A230).
	//   On false, vtable +0x1A8 = slot 7 `Warn("No quick save slot number
	//   found.")`, and execution **continues** with the out value (0 when the
	//   variable is missing).
	// - `using` `NaninovelLoadingScreen.Show(LoadingMode.Long (2), haltInput: true,
	//   Array.Empty<ICancelEventBlockable>())`: await
	//   `Engine.GetServiceOrErr<IStateManagerExtended>()` slot 4
	//   `QuickLoadAsync(slotNumber)`; the result is discarded.
	// - `token` is not used.
	[Token(Token = "0x60000C2")]
	[Address(RVA = "0x31A110", Offset = "0x318B10", VA = "0x18031A110")]
	[AsyncStateMachine(typeof(_003CDoQuickLoadAsync_003Ed__3))]
	private async UniTask DoQuickLoadAsync(AsyncToken token)
	{
		if (!TryGetSlotNumber(out int slotNumber))
		{
			Warn("No quick save slot number found.");
		}
		using (GigaCreation.NaninovelExtender.Common.NaninovelLoadingScreen.Show(GigaCreation.NaninovelExtender.Common.LoadingMode.Long, true))
		{
			await Engine.GetServiceOrErr<GigaCreation.NaninovelExtender.Common.IStateManagerExtended>().QuickLoadAsync(slotNumber);
		}
	}

	[Token(Token = "0x60000C3")]
	[Address(RVA = "0x31A230", Offset = "0x318C30", VA = "0x18031A230")]
	private bool TryGetSlotNumber(out int slotNumber)
	{
		// RECONSTRUCTED | Confidence: A (behavior), B (const-vs-literal spelling) | RVA: 0x31A230, 313 B
		// When `SlotNumber` (+0x30) is assigned (ICommandParameter slot 0), the result
		// is `Nullable<int>.get_Value` into `slotNumber`, and it returns true.
		// Otherwise it tail-jumps to `CustomVariableManagerExtensions
		// .TryGetVariableValue<int>(Engine.GetServiceOrErr<ICustomVariableManager>(),
		// "lastQuickSaveSlotNumber", out slotNumber)`. The literal equals
		// `QuickSave.LastQuickSaveSlotNumberVarName`, but a const reference would
		// be folded into the same literal, so the bytes cannot tell the two apart.
		if (Assigned(SlotNumber))
		{
			slotNumber = SlotNumber.Value;
			return true;
		}
		return Engine.GetServiceOrErr<ICustomVariableManager>().TryGetVariableValue(QuickSave.LastQuickSaveSlotNumberVarName, out slotNumber);
	}

	[Token(Token = "0x60000C4")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public QuickLoad()
	{
	}
}
