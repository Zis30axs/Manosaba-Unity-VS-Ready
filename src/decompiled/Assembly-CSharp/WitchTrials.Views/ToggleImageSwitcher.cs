using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x200001A")]
public class ToggleImageSwitcher : MonoBehaviour
{
	[Token(Token = "0x4000030")]
	[FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private Toggle _toggle;

	[Token(Token = "0x4000031")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Image _image;

	[Token(Token = "0x4000032")]
	[FieldOffset(Offset = "0x30")]
	[Header("Sprites")]
	[SerializeField]
	private Sprite _onSprite;

	[Token(Token = "0x4000033")]
	[FieldOffset(Offset = "0x38")]
	[SerializeField]
	private Sprite _offSprite;

	[Token(Token = "0x6000059")]
	[Address(RVA = "0x30F600", Offset = "0x30E000", VA = "0x18030F600")]
	private void Reset()
	{
	}

	[Token(Token = "0x600005A")]
	[Address(RVA = "0x30F730", Offset = "0x30E130", VA = "0x18030F730")]
	private void Awake()
	{
	}

	[Token(Token = "0x600005B")]
	[Address(RVA = "0x30F820", Offset = "0x30E220", VA = "0x18030F820")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x600005C")]
	[Address(RVA = "0x30FA50", Offset = "0x30E450", VA = "0x18030FA50")]
	private void OnValueChanged(bool isOn)
	{
	}

	[Token(Token = "0x600005D")]
	[Address(RVA = "0x30FA50", Offset = "0x30E450", VA = "0x18030FA50")]
	private void Change(bool isOn)
	{
	}

	[Token(Token = "0x600005E")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public ToggleImageSwitcher()
	{
	}
}
