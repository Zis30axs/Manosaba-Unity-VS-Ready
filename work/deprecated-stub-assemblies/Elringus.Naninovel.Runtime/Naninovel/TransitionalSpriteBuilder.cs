using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20004AB")]
    public class TransitionalSpriteBuilder
    {
    	[Token(Token = "0x400119E")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly Vector3[] vertices;

    	[Token(Token = "0x400119F")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly Vector2[] uvs;

    	[Token(Token = "0x40011A0")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly ushort[] triangles;

    	[Token(Token = "0x40011A1")]
    	[FieldOffset(Offset = "0x28")]
    	private readonly Mesh mesh;

    	[Token(Token = "0x600173A")]
    	[Address(RVA = "0x5F0310", Offset = "0x5EED10", VA = "0x1805F0310")]
    	public TransitionalSpriteBuilder(Mesh mesh)
    	{
    	}

    	[Token(Token = "0x600173B")]
    	[Address(RVA = "0x5F0590", Offset = "0x5EEF90", VA = "0x1805F0590")]
    	public void Build(Vector2 size, Vector2 pivot, float ppu)
    	{
    	}

    	[Token(Token = "0x600173C")]
    	[Address(RVA = "0x5F07E0", Offset = "0x5EF1E0", VA = "0x1805F07E0")]
    	private void BuildVertices(Vector2 size, float ppu)
    	{
    	}

    	[Token(Token = "0x600173D")]
    	[Address(RVA = "0x5F08C0", Offset = "0x5EF2C0", VA = "0x1805F08C0")]
    	private void ApplyPivot(Vector2 pivot)
    	{
    	}

    	[Token(Token = "0x600173E")]
    	[Address(RVA = "0x5F0A00", Offset = "0x5EF400", VA = "0x1805F0A00")]
    	private Rect EvaluateSpriteRect()
    	{
    		return default(Rect);
    	}

    	[Token(Token = "0x600173F")]
    	[Address(RVA = "0x5F0FB0", Offset = "0x5EF9B0", VA = "0x1805F0FB0")]
    	private void SetMeshData()
    	{
    	}
    }
}
