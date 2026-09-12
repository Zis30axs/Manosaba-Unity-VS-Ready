using Il2CppDummyDll;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WitchTrials.Views;

[Token(Token = "0x2000016")]
[RequireComponent(typeof(TextMeshProUGUI))]
public sealed class PointerStayingTextColorSwapper : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerStaying
{
	[Token(Token = "0x4000027")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private TextMeshProUGUI _label;

	[Token(Token = "0x4000028")]
	[FieldOffset(Offset = "0x28")]
	[Space]
	[SerializeField]
	private Color _defaultColor;

	[Token(Token = "0x4000029")]
	[FieldOffset(Offset = "0x38")]
	[SerializeField]
	private Color _hoverColor;

	[Token(Token = "0x400002A")]
	[FieldOffset(Offset = "0x48")]
	private PointerStayingPropagator _pointerStayingPropagator;

	[Token(Token = "0x6000046")]
	[Address(RVA = "0x30EC20", Offset = "0x30D620", VA = "0x18030EC20")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000047")]
	[Address(RVA = "0x30ECC0", Offset = "0x30D6C0", VA = "0x18030ECC0", Slot = "4")]
	public void OnPointerEnter(PointerEventData eventData)
	{
	}

	[Token(Token = "0x6000048")]
	[Address(RVA = "0x30EDF0", Offset = "0x30D7F0", VA = "0x18030EDF0", Slot = "5")]
	public void OnPointerExit(PointerEventData eventData)
	{
	}

	[Token(Token = "0x6000049")]
	[Address(RVA = "0x30EF20", Offset = "0x30D920", VA = "0x18030EF20", Slot = "6")]
	public void DoPointerEnter(PointerEventData eventData)
	{
	}

	[Token(Token = "0x600004A")]
	[Address(RVA = "0x30EF60", Offset = "0x30D960", VA = "0x18030EF60", Slot = "7")]
	public void DoPointerExit(PointerEventData eventData)
	{
	}

	[Token(Token = "0x600004B")]
	[Address(RVA = "0x30EFA0", Offset = "0x30D9A0", VA = "0x18030EFA0", Slot = "8")]
	public void RegisterTo(PointerStayingPropagator pointerStayingPropagator)
	{
	}

	[Token(Token = "0x600004C")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public PointerStayingTextColorSwapper()
	{
	}
}
