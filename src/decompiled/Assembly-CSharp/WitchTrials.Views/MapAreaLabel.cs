using Il2CppDummyDll;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x20000F8")]
public class MapAreaLabel : MonoBehaviour
{
	[Token(Token = "0x40004A3")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private CanvasGroup _canvasGroup;

	[Token(Token = "0x40004A4")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private TextMeshProUGUI _label;

	[Token(Token = "0x40004A5")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Image[] _labelPlateImages;

	[Token(Token = "0x60003EA")]
	[Address(RVA = "0x36E6B0", Offset = "0x36D0B0", VA = "0x18036E6B0")]
	private void Reset()
	{
	}

	[Token(Token = "0x60003EB")]
	[Address(RVA = "0x36E750", Offset = "0x36D150", VA = "0x18036E750")]
	public void Show()
	{
	}

	[Token(Token = "0x60003EC")]
	[Address(RVA = "0x3491C0", Offset = "0x347BC0", VA = "0x1803491C0")]
	public void Hide()
	{
	}

	[Token(Token = "0x60003ED")]
	[Address(RVA = "0x36E780", Offset = "0x36D180", VA = "0x18036E780")]
	public void ModifyInEditor(float[] labelMaxWidths)
	{
	}

	[Token(Token = "0x60003EE")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public MapAreaLabel()
	{
	}
}
