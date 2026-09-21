using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Common
{
    [Token(Token = "0x200001C")]
    public class NaninovelLoadingScreen : IDisposable
    {
    	[Token(Token = "0x17000006")]
    	private IInputManager NaniInputManager
    	{
    		// RECONSTRUCTED | Confidence: A | get-only auto-property, backing field +0x10 (set by the constructor)
    		[Token(Token = "0x600003B")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get;
    	}

    	[Token(Token = "0x17000007")]
    	private ILoadingUiExtended LoadingUiExtended
    	{
    		// RECONSTRUCTED | Confidence: A | get-only auto-property, backing field +0x18 (set by the constructor)
    		[Token(Token = "0x600003C")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		[CompilerGenerated]
    		get;
    	}

    	[Token(Token = "0x17000008")]
    	private bool IsInputProcessingHalted
    	{
    		// RECONSTRUCTED | Confidence: A | auto-property, backing field +0x20
    		[Token(Token = "0x600003D")]
    		[Address(RVA = "0x468AC0", Offset = "0x4674C0", VA = "0x180468AC0")]
    		[CompilerGenerated]
    		get;
    		[Token(Token = "0x600003E")]
    		[Address(RVA = "0x468AD0", Offset = "0x4674D0", VA = "0x180468AD0")]
    		[CompilerGenerated]
    		set;
    	}

    	[Token(Token = "0x17000009")]
    	private ICancelEventBlockable[] Blockables
    	{
    		// RECONSTRUCTED | Confidence: A | auto-property, backing field +0x28
    		[Token(Token = "0x600003F")]
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060")]
    		[CompilerGenerated]
    		get;
    		[Token(Token = "0x6000040")]
    		[Address(RVA = "0x486D40", Offset = "0x485740", VA = "0x180486D40")]
    		[CompilerGenerated]
    		set;
    	}

    	[Token(Token = "0x1700000A")]
    	private CancellationTokenSource Cts
    	{
    		// RECONSTRUCTED | Confidence: A | auto-property, backing field +0x30
    		[Token(Token = "0x6000041")]
    		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0")]
    		[CompilerGenerated]
    		get;
    		[Token(Token = "0x6000042")]
    		[Address(RVA = "0x58DF10", Offset = "0x58C910", VA = "0x18058DF10")]
    		[CompilerGenerated]
    		set;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCFC550 -- Engine.TryGetService<IInputManager> -> +0x10, TryGetService<IUIManager>,
    	//   UIManagerExtensions.TryGetUI<ILoadingUiExtended> -> +0x18 (all inlined), then set_Cts(new CancellationTokenSource());
    	//   every miss logs its warning and stops
    	// Annotated disassembly: work/title-path/disas/nls_01_ctor.txt
    	[Token(Token = "0x6000043")]
    	[Address(RVA = "0xCFC550", Offset = "0xCFAF50", VA = "0x180CFC550")]
    	private NaninovelLoadingScreen()
    	{
    		if (!Engine.TryGetService<IInputManager>(out IInputManager inputManager))
    		{
    			Debug.LogWarning("Naninovel InputManager not found.");
    			return;
    		}
    		NaniInputManager = inputManager;
    		if (!Engine.TryGetService<IUIManager>(out IUIManager uiManager))
    		{
    			Debug.LogWarning("Naninovel UIManager not found.");
    			return;
    		}
    		if (!uiManager.TryGetUI<ILoadingUiExtended>(out ILoadingUiExtended loadingUi))
    		{
    			Debug.LogWarning("Naninovel LoadingUI not found.");
    			return;
    		}
    		LoadingUiExtended = loadingUi;
    		Cts = new CancellationTokenSource();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCFC960
    	// - Cts (+0x30) disposed when set, then cleared.
    	// - The LoadingUI (+0x18) when it is a live UnityEngine.Object (isinst + m_CachedPtr test): IManagedUI slot 9
    	//   ChangeVisibility(false, null, default) with Naninovel Forget().
    	// - Input processing restored (IInputManager slot 3 set_ProcessInput(true)) only when this screen halted it.
    	// - The blockables that still exist (<>c lambda: ICancelEventBlockable.Exists) are unblocked.
    	// Annotated disassembly: work/title-path/disas/nls_02_Dispose.txt
    	[Token(Token = "0x6000044")]
    	[Address(RVA = "0xCFC960", Offset = "0xCFB360", VA = "0x180CFC960", Slot = "4")]
    	public void Dispose()
    	{
    		// Shipped null test: test rcx, rcx / je past the Dispose.
    		if (Cts != null)
    		{
    			Cts.Dispose();
    		}
    		Cts = null;
    		if (LoadingUiExtended is UnityEngine.Object loadingUi && loadingUi != null)
    		{
    			LoadingUiExtended.ChangeVisibility(false, null, default(AsyncToken)).Forget();
    		}
    		if (NaniInputManager != null && IsInputProcessingHalted)
    		{
    			NaniInputManager.ProcessInput = true;
    		}
    		if (Blockables == null)
    		{
    			return;
    		}
    		foreach (ICancelEventBlockable blockable in Blockables.Where((ICancelEventBlockable b) => b.Exists))
    		{
    			blockable.IsCancelEventBlocked = false;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCFCED0
    	// - new NaninovelLoadingScreen() (0xCFC550).
    	// - With a LoadingUI: ILoadingUiExtended slot 0 set_CurrentMode(mode); IManagedUI slot 9 ChangeVisibility(true, null,
    	//   Cts.Token) with Naninovel Forget().
    	// - haltInput and IInputManager slot 2 ProcessInput: IsInputProcessingHalted (+0x20) = true, slot 3 ProcessInput = false.
    	// - Blockables (+0x28) stored, each blocked (ICancelEventBlockable slot 1 set_IsCancelEventBlocked(true)).
    	// Annotated disassembly: work/title-path/disas/nls_03_Show.txt
    	[Token(Token = "0x6000045")]
    	[Address(RVA = "0xCFCED0", Offset = "0xCFB8D0", VA = "0x180CFCED0")]
    	public static IDisposable Show(LoadingMode mode, bool haltInput, params ICancelEventBlockable[] blockables)
    	{
    		NaninovelLoadingScreen loadingScreen = new NaninovelLoadingScreen();
    		// Shipped null test: cmp [r14+0x18], 0 / je past the show.
    		if (loadingScreen.LoadingUiExtended != null)
    		{
    			loadingScreen.LoadingUiExtended.CurrentMode = mode;
    			loadingScreen.LoadingUiExtended.ChangeVisibility(true, null, loadingScreen.Cts.Token).Forget();
    		}
    		if (haltInput && loadingScreen.NaniInputManager.ProcessInput)
    		{
    			loadingScreen.IsInputProcessingHalted = true;
    			loadingScreen.NaniInputManager.ProcessInput = false;
    		}
    		loadingScreen.Blockables = blockables;
    		ICancelEventBlockable[] blockableArray = loadingScreen.Blockables;
    		foreach (ICancelEventBlockable blockable in blockableArray)
    		{
    			blockable.IsCancelEventBlocked = true;
    		}
    		return loadingScreen;
    	}
    }
}
