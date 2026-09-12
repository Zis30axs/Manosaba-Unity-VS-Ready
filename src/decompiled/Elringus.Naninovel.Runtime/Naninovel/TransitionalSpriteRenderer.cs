using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20004AD")]
public class TransitionalSpriteRenderer : TransitionalRenderer
{
	[Token(Token = "0x40011AA")]
	private const string defaultSpriteShaderName = "Hidden/Naninovel/Transparent";

	[Token(Token = "0x40011AB")]
	private const string depthShaderName = "Hidden/Naninovel/DepthMask";

	[Token(Token = "0x40011AC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly int depthCutoffId;

	[Token(Token = "0x40011AD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x4")]
	private static readonly int opacityId;

	[Token(Token = "0x40011AE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	private ICameraManager cameraManager;

	[Token(Token = "0x40011AF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private TransitionalMatcher matcher;

	[Token(Token = "0x40011B0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	private float referencePPU;

	[Token(Token = "0x40011B1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	private TransitionalSpriteBuilder spriteBuilder;

	[Token(Token = "0x40011B2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
	private Mesh spriteMesh;

	[Token(Token = "0x40011B3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
	private RenderTexture renderTexture;

	[Token(Token = "0x40011B4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
	private Vector2 pivot;

	[Token(Token = "0x40011B5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
	private float pixelsPerUnit;

	[Token(Token = "0x1700034E")]
	public virtual Material SpriteMaterial
	{
		[Token(Token = "0x6001746")]
		[Address(RVA = "0x398D30", Offset = "0x397730", VA = "0x180398D30", Slot = "43")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6001747")]
		[Address(RVA = "0x5C3F10", Offset = "0x5C2910", VA = "0x1805C3F10")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x1700034F")]
	public virtual Material DepthMaterial
	{
		[Token(Token = "0x6001748")]
		[Address(RVA = "0x478200", Offset = "0x476C00", VA = "0x180478200", Slot = "44")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6001749")]
		[Address(RVA = "0x5F1240", Offset = "0x5EFC40", VA = "0x1805F1240")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000350")]
	public virtual float PixelsPerUnit
	{
		[Token(Token = "0x600174A")]
		[Address(RVA = "0x5F12A0", Offset = "0x5EFCA0", VA = "0x1805F12A0", Slot = "45")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x600174B")]
		[Address(RVA = "0x5F12B0", Offset = "0x5EFCB0", VA = "0x1805F12B0", Slot = "46")]
		set
		{
		}
	}

	[Token(Token = "0x17000351")]
	public virtual Rect Bounds
	{
		[Token(Token = "0x600174C")]
		[Address(RVA = "0x5F1300", Offset = "0x5EFD00", VA = "0x1805F1300", Slot = "47")]
		get
		{
			return default(Rect);
		}
	}

	[Token(Token = "0x17000352")]
	public virtual bool DepthPassEnabled
	{
		[Token(Token = "0x600174D")]
		[Address(RVA = "0x45DD00", Offset = "0x45C700", VA = "0x18045DD00", Slot = "48")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x600174E")]
		[Address(RVA = "0x5F15C0", Offset = "0x5EFFC0", VA = "0x1805F15C0", Slot = "49")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x17000353")]
	public virtual float DepthAlphaCutoff
	{
		[Token(Token = "0x600174F")]
		[Address(RVA = "0x5F15D0", Offset = "0x5EFFD0", VA = "0x1805F15D0", Slot = "50")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6001750")]
		[Address(RVA = "0x5F1660", Offset = "0x5F0060", VA = "0x1805F1660", Slot = "51")]
		set
		{
		}
	}

	[Token(Token = "0x17000354")]
	public override Vector2 Pivot
	{
		[Token(Token = "0x6001751")]
		[Address(RVA = "0x5F1700", Offset = "0x5F0100", VA = "0x1805F1700", Slot = "23")]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x6001752")]
		[Address(RVA = "0x5F1720", Offset = "0x5F0120", VA = "0x1805F1720", Slot = "24")]
		set
		{
		}
	}

	[Token(Token = "0x6001753")]
	[Address(RVA = "0x5F17B0", Offset = "0x5F01B0", VA = "0x1805F17B0", Slot = "52")]
	public virtual void Initialize(Vector2 pivot, float pixelsPerUnit, bool premultipliedAlpha, AspectMatchMode matchMode, float matchRatio, [Optional] Material customMaterial, [Optional] Material customSpriteMaterial)
	{
	}

	[Token(Token = "0x6001754")]
	[Address(RVA = "0x5F1DC0", Offset = "0x5F07C0", VA = "0x1805F1DC0", Slot = "53")]
	protected virtual void Update()
	{
	}

	[Token(Token = "0x6001755")]
	[Address(RVA = "0x5F2300", Offset = "0x5F0D00", VA = "0x1805F2300", Slot = "36")]
	protected override void OnDestroy()
	{
	}

	[Token(Token = "0x6001756")]
	[Address(RVA = "0x5F24E0", Offset = "0x5F0EE0", VA = "0x1805F24E0", Slot = "54")]
	protected virtual Vector2Int GetMeshSize()
	{
		return default(Vector2Int);
	}

	[Token(Token = "0x6001757")]
	[Address(RVA = "0x5F2740", Offset = "0x5F1140", VA = "0x1805F2740", Slot = "41")]
	protected override (Vector2, Vector2) GetTransitionUVModifiers(Vector2 renderSize, Vector2 textureSize)
	{
		return default((Vector2, Vector2));
	}

	[Token(Token = "0x6001758")]
	[Address(RVA = "0x5F2A00", Offset = "0x5F1400", VA = "0x1805F2A00")]
	private void PrepareRenderTexture()
	{
	}

	[Token(Token = "0x6001759")]
	[Address(RVA = "0x5F2D40", Offset = "0x5F1740", VA = "0x1805F2D40")]
	private void BuildMesh()
	{
	}

	[Token(Token = "0x600175A")]
	[Address(RVA = "0x5F2EE0", Offset = "0x5F18E0", VA = "0x1805F2EE0")]
	private void SetPivot(Vector2 value)
	{
	}

	[Token(Token = "0x600175B")]
	[Address(RVA = "0x5F12B0", Offset = "0x5EFCB0", VA = "0x1805F12B0")]
	private void SetPPU(float value)
	{
	}

	[Token(Token = "0x600175C")]
	[Address(RVA = "0x5F2F60", Offset = "0x5F1960", VA = "0x1805F2F60")]
	public TransitionalSpriteRenderer()
	{
	}
}
