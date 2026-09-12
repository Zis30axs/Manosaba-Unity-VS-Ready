using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui;

[Token(Token = "0x2000019")]
[RequireComponent(typeof(CanvasRenderer))]
public class Quadrilateral : MaskableGraphic
{
	[Token(Token = "0x400004B")]
	[FieldOffset(Offset = "0xE0")]
	[Header("References")]
	[SerializeField]
	private RectTransform _rectTransform;

	[Token(Token = "0x400004C")]
	[FieldOffset(Offset = "0xE8")]
	[Header("Parameters")]
	[SerializeField]
	private Vector2 _x;

	[Token(Token = "0x400004D")]
	[FieldOffset(Offset = "0xF0")]
	[SerializeField]
	private Vector2 _y;

	[Token(Token = "0x400004E")]
	[FieldOffset(Offset = "0xF8")]
	[SerializeField]
	private Vector2 _z;

	[Token(Token = "0x400004F")]
	[FieldOffset(Offset = "0x100")]
	[SerializeField]
	private Vector2 _w;

	[Token(Token = "0x6000059")]
	[Address(RVA = "0xCDC760", Offset = "0xCDB160", VA = "0x180CDC760", Slot = "44")]
	protected override void OnPopulateMesh(VertexHelper vh)
	{
	}

	[Token(Token = "0x600005A")]
	[Address(RVA = "0xCDC940", Offset = "0xCDB340", VA = "0x180CDC940")]
	public void SetUvs(Vector2[] uvs)
	{
	}

	[Token(Token = "0x600005B")]
	[Address(RVA = "0xCDCB20", Offset = "0xCDB520", VA = "0x180CDCB20")]
	public void SetUvs(Vector2 nx, Vector2 ny, Vector2 nz, Vector2 nw)
	{
	}

	[Token(Token = "0x600005C")]
	[Address(RVA = "0xCDCCC0", Offset = "0xCDB6C0", VA = "0x180CDCCC0")]
	public void Expand()
	{
	}

	[Token(Token = "0x600005D")]
	[Address(RVA = "0xCDCD80", Offset = "0xCDB780", VA = "0x180CDCD80")]
	public Quadrilateral()
	{
	}
}
