using GigaCreation.Tools.UndoExtensions;
using System;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDC760, 469 B -- Color32 from color (Graphic slot 22); vh.Clear(); four
    	//   AddVert(position, color32, uv0) for _w (uv 0,0), _x (0,1 data 0x2AE2F40), _y (1,1 0x2AE2F50), _z (1,0 0x2AE2EA0);
    	//   AddTriangle(0, 1, 2), AddTriangle(2, 3, 0)
    	// Annotated disassembly: work/title-path/disas/quad_0xCDC760.txt
    	[Token(Token = "0x6000059")]
    	[Address(RVA = "0xCDC760", Offset = "0xCDB160", VA = "0x180CDC760", Slot = "44")]
    	protected override void OnPopulateMesh(VertexHelper vh)
    	{
    		Color32 color = this.color;
    		vh.Clear();
    		vh.AddVert(_w, color, new Vector2(0f, 0f));
    		vh.AddVert(_x, color, new Vector2(0f, 1f));
    		vh.AddVert(_y, color, new Vector2(1f, 1f));
    		vh.AddVert(_z, color, new Vector2(1f, 0f));
    		vh.AddTriangle(0, 1, 2);
    		vh.AddTriangle(2, 3, 0);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDC940, 478 B -- each corner from uvs when the array is long enough, else
    	//   Vector2.up / one / right / zero (static +0x10 / +0x8 / +0x28 / +0x0); then SetUvs(nx, ny, nz, nw)
    	// Annotated disassembly: work/title-path/disas/quad_0xCDC940.txt
    	[Token(Token = "0x600005A")]
    	[Address(RVA = "0xCDC940", Offset = "0xCDB340", VA = "0x180CDC940")]
    	public void SetUvs(Vector2[] uvs)
    	{
    		Vector2 nx = ((uvs.Length > 0) ? uvs[0] : Vector2.up);
    		Vector2 ny = ((uvs.Length > 1) ? uvs[1] : Vector2.one);
    		Vector2 nz = ((uvs.Length > 2) ? uvs[2] : Vector2.right);
    		Vector2 nw = ((uvs.Length > 3) ? uvs[3] : Vector2.zero);
    		SetUvs(nx, ny, nz, nw);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDCB20, 415 B -- rect = _rectTransform (+0xE0).rect (icall); each corner field
    	//   (+0xE8 / +0xF0 / +0xF8 / +0x100) = rect.position + rect.size * n (component-wise); then SetAllDirty() (slot 26)
    	// Annotated disassembly: work/title-path/disas/quad_0xCDCB20.txt
    	[Token(Token = "0x600005B")]
    	[Address(RVA = "0xCDCB20", Offset = "0xCDB520", VA = "0x180CDCB20")]
    	public void SetUvs(Vector2 nx, Vector2 ny, Vector2 nz, Vector2 nw)
    	{
    		Rect rect = _rectTransform.rect;
    		_x = rect.position + rect.size * nx;
    		_y = rect.position + rect.size * ny;
    		_z = rect.position + rect.size * nz;
    		_w = rect.position + rect.size * nw;
    		SetAllDirty();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDCCC0, 190 B / b__8_0 0xCDCD90 -- this.DoActionAsUndoable("Expand", _ =>
    	//   SetUvs(Array.Empty<Vector2>())): every corner falls back to its default
    	// Annotated disassembly: work/title-path/disas/quad_0xCDCCC0.txt, quad_0xCDCD90.txt
    	[Token(Token = "0x600005C")]
    	[Address(RVA = "0xCDCCC0", Offset = "0xCDB6C0", VA = "0x180CDCCC0")]
    	public void Expand()
    	{
    		this.DoActionAsUndoable("Expand", delegate
    		{
    			SetUvs(Array.Empty<Vector2>());
    		});
    	}

    	[Token(Token = "0x600005D")]
    	[Address(RVA = "0xCDCD80", Offset = "0xCDB780", VA = "0x180CDCD80")]
    	public Quadrilateral()
    	{
    	}
    }
}
