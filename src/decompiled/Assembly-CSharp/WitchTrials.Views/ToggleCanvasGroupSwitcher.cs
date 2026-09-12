using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x2000019")]
public class ToggleCanvasGroupSwitcher : MonoBehaviour
{
	[Token(Token = "0x400002D")]
	[FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private Toggle _toggle;

	[Token(Token = "0x400002E")]
	[FieldOffset(Offset = "0x28")]
	[Header("Canvas Group")]
	[SerializeField]
	private CanvasGroup _onCanvasGroup;

	[Token(Token = "0x400002F")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private CanvasGroup _offCanvasGroup;

	[Token(Token = "0x6000053")]
	[Address(RVA = "0x30F220", Offset = "0x30DC20", VA = "0x18030F220")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000054")]
	[Address(RVA = "0x30F300", Offset = "0x30DD00", VA = "0x18030F300")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000055")]
	[Address(RVA = "0x30F480", Offset = "0x30DE80", VA = "0x18030F480")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000056")]
	[Address(RVA = "0x30F520", Offset = "0x30DF20", VA = "0x18030F520")]
	private void OnValueChanged(bool isOn)
	{
	}

	[Token(Token = "0x6000057")]
	[Address(RVA = "0x30F530", Offset = "0x30DF30", VA = "0x18030F530")]
	private void Change(bool isOn)
	{
	}

	[Token(Token = "0x6000058")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public ToggleCanvasGroupSwitcher()
	{
	}
}
