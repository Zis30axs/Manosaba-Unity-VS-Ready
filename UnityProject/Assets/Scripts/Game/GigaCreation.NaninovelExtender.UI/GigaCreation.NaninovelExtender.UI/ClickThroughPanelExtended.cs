using System;
using GigaCreation.NaninovelExtender.Common;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UnityEngine.EventSystems;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Token(Token = "0x200000A")]
    public class ClickThroughPanelExtended : CustomUI, IClickThroughPanelExtended, IClickThroughPanel, IManagedUI, IPointerClickHandler, IEventSystemHandler
    {
    	[Token(Token = "0x4000015")]
    	[FieldOffset(Offset = "0xE0")]
    	private IInputManager _naniInputManager;

    	[Token(Token = "0x4000016")]
    	[FieldOffset(Offset = "0xE8")]
    	private Action _onClick;

    	[Token(Token = "0x4000017")]
    	[FieldOffset(Offset = "0xF0")]
    	private bool _hideOnClick;

    	[Token(Token = "0x4000018")]
    	[FieldOffset(Offset = "0xF1")]
    	private bool _dontBlockInteractionOnNext;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD366E0 -- the stock ClickThroughPanel.Awake:
    	//   0x180D36720 CustomUI.Awake, then 0x180D36741 Engine.GetServiceOrErr<IInputManager>() stored to +0xE0
    	// Annotated disassembly: work/title-path/disas/ctp_01_Awake.txt
    	[Token(Token = "0x600001E")]
    	[Address(RVA = "0xD366E0", Offset = "0xD350E0", VA = "0x180D366E0", Slot = "4")]
    	protected override void Awake()
    	{
    		base.Awake();
    		_naniInputManager = Engine.GetServiceOrErr<IInputManager>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD367A0 -- mov byte [rcx+0xF1], 1
    	[Token(Token = "0x600001F")]
    	[Address(RVA = "0xD367A0", Offset = "0xD351A0", VA = "0x180D367A0", Slot = "96")]
    	public void DontBlockInteractionOnNext()
    	{
    		_dontBlockInteractionOnNext = true;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD367B0 -- tail jump through vtable +0x388 = CustomUI slot 37 set_SortingOrder
    	[Token(Token = "0x6000020")]
    	[Address(RVA = "0xD367B0", Offset = "0xD351B0", VA = "0x180D367B0", Slot = "97")]
    	public void SetSortingOrder(int order)
    	{
    		SortingOrder = order;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD367D0 -- the stock Show plus the one-shot opt-out:
    	//   +0xF0 = hideOnClick, +0xE8 = onClick, vtable +0x428 = slot 47 Show();
    	//   _dontBlockInteractionOnNext (+0xF1) clear -> IInputManager slot 7 AddBlockingUI(this, allowedSamplers),
    	//   set -> it is reset to false and input is not blocked this time
    	// Annotated disassembly: work/title-path/disas/ctp_04_Show.txt
    	[Token(Token = "0x6000021")]
    	[Address(RVA = "0xD367D0", Offset = "0xD351D0", VA = "0x180D367D0", Slot = "98")]
    	public void Show(bool hideOnClick, Action onClick, params string[] allowedSamplers)
    	{
    		_hideOnClick = hideOnClick;
    		_onClick = onClick;
    		Show();
    		if (!_dontBlockInteractionOnNext)
    		{
    			_naniInputManager.AddBlockingUI(this, allowedSamplers);
    		}
    		else
    		{
    			_dontBlockInteractionOnNext = false;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD368D0 -- +0xE8 = null; IInputManager slot 8 RemoveBlockingUI(this);
    	//   then CustomUI.Hide inlined: vtable +0x3F8 = slot 44 ChangeVisibility(false, null, default).Forget()
    	// Annotated disassembly: work/title-path/disas/ctp_05_Hide.txt
    	[Token(Token = "0x6000022")]
    	[Address(RVA = "0xD368D0", Offset = "0xD352D0", VA = "0x180D368D0", Slot = "48")]
    	public override void Hide()
    	{
    		_onClick = null;
    		_naniInputManager.RemoveBlockingUI(this);
    		base.Hide();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD369C0 -- a right click ([eventData+0x148] == 1) is ignored;
    	//   otherwise the stock body: the shipped null test on _onClick (+0xE8), invoke, then Hide() (slot 48) when
    	//   _hideOnClick (+0xF0)
    	// Annotated disassembly: work/title-path/disas/ctp_06_OnPointerClick.txt
    	[Token(Token = "0x6000023")]
    	[Address(RVA = "0xD369C0", Offset = "0xD353C0", VA = "0x180D369C0", Slot = "100")]
    	public void OnPointerClick(PointerEventData eventData)
    	{
    		if (eventData.button == PointerEventData.InputButton.Right)
    		{
    			return;
    		}
    		if (_onClick != null)
    		{
    			_onClick();
    		}
    		if (_hideOnClick)
    		{
    			Hide();
    		}
    	}

    	[Token(Token = "0x6000024")]
    	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
    	public ClickThroughPanelExtended()
    	{
    	}
    }
}
