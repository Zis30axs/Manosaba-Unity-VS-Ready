using System.Collections.Generic;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.UI;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Token(Token = "0x2000005")]
    [UsedImplicitly]
    [InitializeAtRuntime(0, typeof(UIManager))]
    public class UiManagerExtended : UIManager
    {
    	[Token(Token = "0x4000008")]
    	[FieldOffset(Offset = "0xB0")]
    	private readonly IScriptPlayer _naniScriptPlayer;

    	[Token(Token = "0x4000009")]
    	[FieldOffset(Offset = "0xB8")]
    	private readonly IList<IManagedUI> _currentModalUis;

    	[Token(Token = "0x400000A")]
    	[FieldOffset(Offset = "0xC0")]
    	private GameObject _modalContainer;

    	[Token(Token = "0x400000B")]
    	[FieldOffset(Offset = "0xC8")]
    	private CanvasGroup _containerGroup;

    	[Token(Token = "0x17000001")]
    	private GameObject ModalContainer
    	{
    		[Token(Token = "0x6000006")]
    		[Address(RVA = "0xD34230", Offset = "0xD32C30", VA = "0x180D34230")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000002")]
    	private CanvasGroup ContainerGroup
    	{
    		[Token(Token = "0x6000007")]
    		[Address(RVA = "0xD34390", Offset = "0xD32D90", VA = "0x180D34390")]
    		get
    		{
    			return null;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD34510, 404 B
    	//
    	// Native evidence (annotated disassembly, `work/reversing/disas.py 0xD34510`):
    	//
    	//   0x180D34531  mov rsi,r9        -> 3rd parameter  (l10n,          ILocalizationManager)
    	//   0x180D34534  mov rbp,r8        -> 2nd parameter  (resources,     IResourceProviderManager)
    	//   0x180D34537  mov r14,rdx       -> 1st parameter  (config,        UIConfiguration)
    	//   0x180D3453A  mov rdi,rcx       -> this
    	//   ... new List<IManagedUI>() then `mov [rdi+0xb8],rbx`  -> the field initializer of _currentModalUis
    	//       (0xB8, per [FieldOffset]) emitted *before* the base call, which is what a C# field initializer
    	//       compiles to.
    	//   The incoming *stack* parameters sit at [rsp+0x28].. at function entry; the prologue's
    	//   `push r14` + `sub rsp,0x40` shifts them by 0x48, so the base call's reads line up exactly:
    	//   0x180D3461A  mov [rsp+0x20],rax   <- [rsp+0x70] = entry [rsp+0x28] = 4th parameter (communityL10n)
    	//   0x180D34610  mov [rsp+0x28],rax   <- [rsp+0x78] = 5th parameter            (cameraManager)
    	//   0x180D34600  mov [rsp+0x30],rax   <- [rsp+0x80] = 6th parameter            (inputManager)
    	//   0x180D34605  mov rdx,r14 / mov r8,rbp / mov r9,rsi / mov rcx,rdi
    	//   0x180D3461F  call 0x1805fd570     -> Naninovel.UIManager$$.ctor
    	//   i.e. `base(config, resources, l10n, communityL10n, cameraManager, inputManager)` - six arguments in
    	//   declaration order, matching the base constructor's signature exactly.
    	//   0x180D3462B  mov rax,[rsp+0x88]   -> the 7th parameter (scriptPlayer), which the base does *not* take
    	//   0x180D34633  mov [rdi+0xb0],rax   -> stored into this class's own readonly field _naniScriptPlayer (0xB0)
    	[Token(Token = "0x6000008")]
    	[Address(RVA = "0xD34510", Offset = "0xD32F10", VA = "0x180D34510")]
    	public UiManagerExtended(UIConfiguration config, IResourceProviderManager resources, ILocalizationManager l10n, ICommunityLocalization communityL10n, ICameraManager cameraManager, IInputManager inputManager, IScriptPlayer scriptPlayer)
    		: base(config, resources, l10n, communityL10n, cameraManager, inputManager)
    	{
    		_currentModalUis = new List<IManagedUI>();
    		_naniScriptPlayer = scriptPlayer;
    	}

    	[Token(Token = "0x6000009")]
    	[Address(RVA = "0xD346B0", Offset = "0xD330B0", VA = "0x180D346B0", Slot = "50")]
    	public override void AddModalUI(IManagedUI managedUI)
    	{
    		base.AddModalUI(managedUI);
    	}

    	[Token(Token = "0x600000A")]
    	[Address(RVA = "0xD34C70", Offset = "0xD33670", VA = "0x180D34C70", Slot = "51")]
    	public override void RemoveModalUI(IManagedUI managedUI)
    	{
    		base.RemoveModalUI(managedUI);
    	}

    	[Token(Token = "0x600000B")]
    	[Address(RVA = "0xD350E0", Offset = "0xD33AE0", VA = "0x180D350E0")]
    	private static void SetCancelEventBlocked(bool isBlocked, params ICancelEventBlockable[] blockables)
    	{
    	}

    	[Token(Token = "0x600000C")]
    	[Address(RVA = "0xD35210", Offset = "0xD33C10", VA = "0x180D35210", Slot = "55")]
    	protected override bool IsModal(IManagedUI managedUI)
    	{
    		return base.IsModal(managedUI);
    	}

    	[Token(Token = "0x600000D")]
    	[Address(RVA = "0xD35280", Offset = "0xD33C80", VA = "0x180D35280", Slot = "61")]
    	protected override void ToggleUI()
    	{
    		base.ToggleUI();
    	}
    }
}
