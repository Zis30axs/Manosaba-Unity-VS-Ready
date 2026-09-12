using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Tools.Ui;

[Token(Token = "0x2000010")]
[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class SafeAreaAdjuster : MonoBehaviour
{
	[Token(Token = "0x400003B")]
	[FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private RectTransform _rectTransform;

	[Token(Token = "0x400003C")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Image _image;

	[Token(Token = "0x400003D")]
	[FieldOffset(Offset = "0x30")]
	[Header("Parameters")]
	[SerializeField]
	private bool _showBorder;

	[Token(Token = "0x400003E")]
	[FieldOffset(Offset = "0x31")]
	[SerializeField]
	private bool _setDirtyOnAdjust;

	[Token(Token = "0x600004F")]
	[Address(RVA = "0xD58960", Offset = "0xD57360", VA = "0x180D58960")]
	private void Start()
	{
	}

	[Token(Token = "0x6000050")]
	[Address(RVA = "0xD58970", Offset = "0xD57370", VA = "0x180D58970")]
	private void OnValidate()
	{
	}

	[Token(Token = "0x6000051")]
	[Address(RVA = "0xD58A60", Offset = "0xD57460", VA = "0x180D58A60")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000052")]
	[Address(RVA = "0xD58BA0", Offset = "0xD575A0", VA = "0x180D58BA0")]
	private void Adjust()
	{
	}

	[Token(Token = "0x6000053")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public SafeAreaAdjuster()
	{
	}
}
