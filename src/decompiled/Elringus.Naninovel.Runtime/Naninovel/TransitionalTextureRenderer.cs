using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20004AE")]
public class TransitionalTextureRenderer : TransitionalRenderer
{
	[Token(Token = "0x17000355")]
	public virtual RenderTexture RenderTexture
	{
		[Token(Token = "0x600175E")]
		[Address(RVA = "0x398D30", Offset = "0x397730", VA = "0x180398D30", Slot = "43")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x600175F")]
		[Address(RVA = "0x5C3F10", Offset = "0x5C2910", VA = "0x1805C3F10", Slot = "44")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x17000356")]
	public virtual Rect RenderRectangle
	{
		[Token(Token = "0x6001760")]
		[Address(RVA = "0x5F3000", Offset = "0x5F1A00", VA = "0x1805F3000", Slot = "45")]
		[CompilerGenerated]
		get
		{
			return default(Rect);
		}
		[Token(Token = "0x6001761")]
		[Address(RVA = "0x5F3010", Offset = "0x5F1A10", VA = "0x1805F3010", Slot = "46")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x6001762")]
	[Address(RVA = "0x5F3020", Offset = "0x5F1A20", VA = "0x1805F3020", Slot = "47")]
	protected virtual void Update()
	{
	}

	[Token(Token = "0x6001763")]
	[Address(RVA = "0x5F3080", Offset = "0x5F1A80", VA = "0x1805F3080", Slot = "40")]
	protected override (Vector2, Vector2) GetMainUVModifiers(Vector2 renderSize, Vector2 textureSize)
	{
		return default((Vector2, Vector2));
	}

	[Token(Token = "0x6001764")]
	[Address(RVA = "0x5F3200", Offset = "0x5F1C00", VA = "0x1805F3200")]
	private (Vector2, Vector2) CropUVs(Vector2 offset, Vector2 scale)
	{
		return default((Vector2, Vector2));
	}

	[Token(Token = "0x6001765")]
	[Address(RVA = "0x5F3330", Offset = "0x5F1D30", VA = "0x1805F3330")]
	public TransitionalTextureRenderer()
	{
	}
}
