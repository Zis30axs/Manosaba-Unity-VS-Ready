using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;

namespace WitchTrials.Views
{
    // NEW IN THE 2026-10-02 GAME UPDATE, like its sibling `AutoSave`.  `System/System_BadEnd` carries a
    // managed reference to `WitchTrials.Views.ShowLoadUi (Assembly-CSharp)` (`{ "AutoSave": true }`), and
    // the shipped MonoScript table lists it, but this checkout's pre-update dump has no such type.
    // Shape, tokens and bodies come from a fresh Il2CppDumper run over the updated
    // GameAssembly.dll / global-metadata.dat (work/il2cpp_out) plus capstone disassembly.
    [Token(Token = "0x2000041")]
    [UsedImplicitly]
    public class ShowLoadUi : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000042")]
    	[CompilerGenerated]
    	private struct _003CShowAsync_003Ed__3
    	{
    		[Token(Token = "0x40000C3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000C4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000C5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ShowLoadUi _003C_003E4__this;

    		// The only one of these commands whose state machine keeps the token (AutoSave/QuickSave drop it).
    		[Token(Token = "0x40000C6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x40000C7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60000DC")]
    		[Address(RVA = "0x31D1A0", Offset = "0x31B9A0", VA = "0x18031D1A0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000DD")]
    		[Address(RVA = "0x30C000", Offset = "0x30A800", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40000C1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly]
    	public BooleanParameter AutoSave;

    	[Token(Token = "0x40000C2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[UsedImplicitly]
    	public BooleanParameter Wait;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x31CF70, 171 B
    	// Same shape as `AutoSave.Execute`: `new Func<AsyncToken, UniTask>(ShowAsync)`, then vtable +0x208 =
    	// slot 13 `Command.WaitOrForget(fn, Wait (+0x38), token)`.
    	[Token(Token = "0x60000D9")]
    	[Address(RVA = "0x31CF70", Offset = "0x31B770", VA = "0x18031CF70", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return WaitOrForget(ShowAsync, Wait, token);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x31D020, 297 B (entry) + MoveNext 0x31D1A0
    	// - `Engine.GetServiceOrErr<IUIManager>().GetUI<ISaveLoadUI>()`, null guarded; the instance is then
    	//   used for the interface call in the next step and as `PresentAsync`'s `self`.
    	// - `AutoSave` is read through BooleanParameter's `bool?` operator (`Nullable<bool>.op_Implicit`),
    	//   and the result selects the presentation mode: `AutoSave` true yields 0 (`QuickLoad`), false
    	//   yields 1 (`Load`) — enumerated as `SaveLoadUIPresentationMode { QuickLoad = 0, Load = 1,
    	//   Save = 2 }`.  `ISaveLoadUI` slot 1 is `set_PresentationMode`, and that is the slot the
    	//   interface-invoke helper at 0x3700 is handed (`ecx = 1`) together with the UI.
    	// - Awaits the extension `ManagedUiExtensions.PresentAsync(this IManagedUI self,
    	//   float? duration, AsyncToken token)` (RVA 0xD41A90) with `duration` unset (`r8 = 0`) and the
    	//   state machine's own `token` (`r9 = [d__3 + 0x20]`), so this command, unlike `AutoSave`,
    	//   forwards its token.
    	[Token(Token = "0x60000DA")]
    	[Address(RVA = "0x31D020", Offset = "0x31B820", VA = "0x18031D020")]
    	[AsyncStateMachine(typeof(_003CShowAsync_003Ed__3))]
    	private async UniTask ShowAsync(AsyncToken token)
    	{
    		ISaveLoadUI saveLoadUI = Engine.GetServiceOrErr<IUIManager>().GetUI<ISaveLoadUI>();
    		saveLoadUI.PresentationMode = ((bool?)AutoSave).Value
    			? SaveLoadUIPresentationMode.QuickLoad
    			: SaveLoadUIPresentationMode.Load;
    		await GigaCreation.NaninovelExtender.Ui.ManagedUiExtensions.PresentAsync(saveLoadUI, default, token);
    	}

    	[Token(Token = "0x60000DB")]
    	[Address(RVA = "0x318720", Offset = "0x316F20", VA = "0x180318720")]
    	public ShowLoadUi()
    	{
    	}
    }
}
