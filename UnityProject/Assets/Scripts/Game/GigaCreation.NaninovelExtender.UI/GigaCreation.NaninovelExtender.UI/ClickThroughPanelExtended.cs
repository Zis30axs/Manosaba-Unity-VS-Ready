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

    	[Token(Token = "0x600001E")]
    	[Address(RVA = "0xD366E0", Offset = "0xD350E0", VA = "0x180D366E0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x600001F")]
    	[Address(RVA = "0xD367A0", Offset = "0xD351A0", VA = "0x180D367A0", Slot = "96")]
    	public void DontBlockInteractionOnNext()
    	{
    	}

    	[Token(Token = "0x6000020")]
    	[Address(RVA = "0xD367B0", Offset = "0xD351B0", VA = "0x180D367B0", Slot = "97")]
    	public void SetSortingOrder(int order)
    	{
    	}

    	[Token(Token = "0x6000021")]
    	[Address(RVA = "0xD367D0", Offset = "0xD351D0", VA = "0x180D367D0", Slot = "98")]
    	public void Show(bool hideOnClick, Action onClick, params string[] allowedSamplers)
    	{
    	}

    	[Token(Token = "0x6000022")]
    	[Address(RVA = "0xD368D0", Offset = "0xD352D0", VA = "0x180D368D0", Slot = "48")]
    	public override void Hide()
    	{
    	}

    	[Token(Token = "0x6000023")]
    	[Address(RVA = "0xD369C0", Offset = "0xD353C0", VA = "0x180D369C0", Slot = "100")]
    	public void OnPointerClick(PointerEventData eventData)
    	{
    	}

    	[Token(Token = "0x6000024")]
    	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
    	public ClickThroughPanelExtended()
    	{
    	}
    }
}
