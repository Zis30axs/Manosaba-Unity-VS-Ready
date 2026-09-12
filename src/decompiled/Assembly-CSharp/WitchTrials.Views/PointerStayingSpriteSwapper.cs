using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x2000015")]
[RequireComponent(typeof(Image))]
public sealed class PointerStayingSpriteSwapper : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerStaying
{
	[Token(Token = "0x4000023")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private Image _image;

	[Token(Token = "0x4000024")]
	[FieldOffset(Offset = "0x28")]
	[Space]
	[SerializeField]
	private Sprite _defaultSprite;

	[Token(Token = "0x4000025")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Sprite _hoverSprite;

	[Token(Token = "0x4000026")]
	[FieldOffset(Offset = "0x38")]
	private PointerStayingPropagator _pointerStayingPropagator;

	[Token(Token = "0x600003E")]
	[Address(RVA = "0x30E870", Offset = "0x30D270", VA = "0x18030E870")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x600003F")]
	[Address(RVA = "0x30E8A0", Offset = "0x30D2A0", VA = "0x18030E8A0")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000040")]
	[Address(RVA = "0x30E940", Offset = "0x30D340", VA = "0x18030E940", Slot = "4")]
	public void OnPointerEnter(PointerEventData eventData)
	{
	}

	[Token(Token = "0x6000041")]
	[Address(RVA = "0x30EA50", Offset = "0x30D450", VA = "0x18030EA50", Slot = "5")]
	public void OnPointerExit(PointerEventData eventData)
	{
	}

	[Token(Token = "0x6000042")]
	[Address(RVA = "0x30EB60", Offset = "0x30D560", VA = "0x18030EB60", Slot = "6")]
	public void DoPointerEnter(PointerEventData eventData)
	{
	}

	[Token(Token = "0x6000043")]
	[Address(RVA = "0x30EB90", Offset = "0x30D590", VA = "0x18030EB90", Slot = "7")]
	public void DoPointerExit(PointerEventData eventData)
	{
	}

	[Token(Token = "0x6000044")]
	[Address(RVA = "0x30EBC0", Offset = "0x30D5C0", VA = "0x18030EBC0", Slot = "8")]
	public void RegisterTo(PointerStayingPropagator pointerStayingPropagator)
	{
	}

	[Token(Token = "0x6000045")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public PointerStayingSpriteSwapper()
	{
	}
}
