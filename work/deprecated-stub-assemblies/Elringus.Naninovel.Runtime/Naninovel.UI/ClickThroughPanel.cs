using System;
using Il2CppDummyDll;
using UnityEngine.EventSystems;

namespace Naninovel.UI
{
    [Token(Token = "0x2000519")]
    public class ClickThroughPanel : CustomUI, IClickThroughPanel, IManagedUI, IPointerClickHandler, IEventSystemHandler
    {
    	[Token(Token = "0x40013C1")]
    	[FieldOffset(Offset = "0xE0")]
    	private IInputManager inputManager;

    	[Token(Token = "0x40013C2")]
    	[FieldOffset(Offset = "0xE8")]
    	private Action onClick;

    	[Token(Token = "0x40013C3")]
    	[FieldOffset(Offset = "0xF0")]
    	private bool hideOnClick;

    	[Token(Token = "0x600195E")]
    	[Address(RVA = "0x619010", Offset = "0x617A10", VA = "0x180619010", Slot = "98")]
    	public virtual void Show(bool hideOnClick, Action onClick, params string[] allowedSamplers)
    	{
    	}

    	[Token(Token = "0x600195F")]
    	[Address(RVA = "0x619100", Offset = "0x617B00", VA = "0x180619100", Slot = "48")]
    	public override void Hide()
    	{
    	}

    	[Token(Token = "0x6001960")]
    	[Address(RVA = "0x6191F0", Offset = "0x617BF0", VA = "0x1806191F0", Slot = "99")]
    	public virtual void OnPointerClick(PointerEventData eventData)
    	{
    	}

    	[Token(Token = "0x6001961")]
    	[Address(RVA = "0x619240", Offset = "0x617C40", VA = "0x180619240", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001962")]
    	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
    	public ClickThroughPanel()
    	{
    	}
    }
}
