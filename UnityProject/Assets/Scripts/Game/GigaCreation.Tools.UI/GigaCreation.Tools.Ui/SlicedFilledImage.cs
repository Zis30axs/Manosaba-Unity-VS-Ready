using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace GigaCreation.Tools.Ui
{
    [Token(Token = "0x2000011")]
    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    [RequireComponent(typeof(CanvasRenderer))]
    [AddComponentMenu("UI/Sliced Filled Image", 11)]
    public class SlicedFilledImage : MaskableGraphic
    {
    	[Token(Token = "0x2000012")]
    	public enum FillDirection
    	{
    		[Token(Token = "0x400004E")]
    		Right,
    		[Token(Token = "0x400004F")]
    		Left,
    		[Token(Token = "0x4000050")]
    		Up,
    		[Token(Token = "0x4000051")]
    		Down
    	}

    	[Token(Token = "0x2000013")]
    	private static class SetPropertyUtility
    	{
    		[Token(Token = "0x600007F")]
    		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6000080")]
    		public static bool SetClass<T>(ref T currentValue, T newValue) where T : UnityEngine.Object
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x400003F")]
    	[FieldOffset(Offset = "0x0")]
    	private static readonly Vector3[] s_vertices;

    	[Token(Token = "0x4000040")]
    	[FieldOffset(Offset = "0x8")]
    	private static readonly Vector2[] s_uVs;

    	[Token(Token = "0x4000041")]
    	[FieldOffset(Offset = "0x10")]
    	private static readonly Vector2[] s_slicedVertices;

    	[Token(Token = "0x4000042")]
    	[FieldOffset(Offset = "0x18")]
    	private static readonly Vector2[] s_slicedUVs;

    	[Token(Token = "0x4000043")]
    	[FieldOffset(Offset = "0x20")]
    	private static readonly List<SlicedFilledImage> s_trackedTexturelessImages;

    	[Token(Token = "0x4000044")]
    	[FieldOffset(Offset = "0x28")]
    	private static bool s_initialized;

    	[Token(Token = "0x4000045")]
    	[FieldOffset(Offset = "0xE0")]
    	[SerializeField]
    	private Sprite _sprite;

    	[Token(Token = "0x4000046")]
    	[FieldOffset(Offset = "0xE8")]
    	[SerializeField]
    	private FillDirection _fillDirection;

    	[Token(Token = "0x4000047")]
    	[FieldOffset(Offset = "0xEC")]
    	[SerializeField]
    	[Range(0f, 1f)]
    	private float _fillAmount;

    	[Token(Token = "0x4000048")]
    	[FieldOffset(Offset = "0xF0")]
    	[SerializeField]
    	private bool _fillCenter;

    	[Token(Token = "0x4000049")]
    	[FieldOffset(Offset = "0xF4")]
    	[SerializeField]
    	private float _pixelsPerUnitMultiplier;

    	[Token(Token = "0x400004A")]
    	[FieldOffset(Offset = "0xF8")]
    	[SerializeField]
    	private float _alphaHitTestMinimumThreshold;

    	[Token(Token = "0x400004B")]
    	[FieldOffset(Offset = "0xFC")]
    	private bool _tracked;

    	[NonSerialized]
    	[Token(Token = "0x400004C")]
    	[FieldOffset(Offset = "0x100")]
    	private Sprite _overrideSprite;

    	[Token(Token = "0x17000009")]
    	private int UnityEngine_002EUI_002EILayoutElement_002ElayoutPriority
    	{
    		[Token(Token = "0x6000054")]
    		[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "75")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x1700000A")]
    	private float UnityEngine_002EUI_002EILayoutElement_002EminWidth
    	{
    		[Token(Token = "0x6000055")]
    		[Address(RVA = "0x572FC0", Offset = "0x5719C0", VA = "0x180572FC0", Slot = "69")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x1700000B")]
    	private float UnityEngine_002EUI_002EILayoutElement_002EminHeight
    	{
    		[Token(Token = "0x6000056")]
    		[Address(RVA = "0x572FC0", Offset = "0x5719C0", VA = "0x180572FC0", Slot = "72")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x1700000C")]
    	private float UnityEngine_002EUI_002EILayoutElement_002EflexibleWidth
    	{
    		[Token(Token = "0x6000057")]
    		[Address(RVA = "0xD572E0", Offset = "0xD55CE0", VA = "0x180D572E0", Slot = "71")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x1700000D")]
    	private float UnityEngine_002EUI_002EILayoutElement_002EflexibleHeight
    	{
    		[Token(Token = "0x6000058")]
    		[Address(RVA = "0xD572E0", Offset = "0xD55CE0", VA = "0x180D572E0", Slot = "74")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x1700000E")]
    	private float UnityEngine_002EUI_002EILayoutElement_002EpreferredWidth
    	{
    		[Token(Token = "0x6000059")]
    		[Address(RVA = "0xD58F20", Offset = "0xD57920", VA = "0x180D58F20", Slot = "70")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x1700000F")]
    	private float UnityEngine_002EUI_002EILayoutElement_002EpreferredHeight
    	{
    		[Token(Token = "0x600005A")]
    		[Address(RVA = "0xD59040", Offset = "0xD57A40", VA = "0x180D59040", Slot = "73")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x17000010")]
    	public Sprite Sprite
    	{
    		[Token(Token = "0x600005B")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33E0B0, 8 B -- `mov rax, qword ptr [rcx + 0xe0]; ret`: the backing field _sprite (+0xE0 in dump.cs)
    		[Address(RVA = "0x33E0B0", Offset = "0x33CAB0", VA = "0x18033E0B0")]
    		get
    		{
    			return _sprite;
    		}
    		[Token(Token = "0x600005C")]
    		[Address(RVA = "0xD59160", Offset = "0xD57B60", VA = "0x180D59160")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000011")]
    	public FillDirection Direction
    	{
    		[Token(Token = "0x600005D")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x8A4E40, 7 B -- `mov eax, dword ptr [rcx + 0xe8]; ret`: the backing field _fillDirection (+0xE8 in dump.cs)
    		[Address(RVA = "0x8A4E40", Offset = "0x8A3840", VA = "0x1808A4E40")]
    		get
    		{
    			return _fillDirection;
    		}
    		[Token(Token = "0x600005E")]
    		[Address(RVA = "0xD591E0", Offset = "0xD57BE0", VA = "0x180D591E0")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000012")]
    	public float FillAmount
    	{
    		[Token(Token = "0x600005F")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD59260, 9 B -- `movss xmm0, dword ptr [rcx + 0xec]; ret`: the backing field _fillAmount (+0xEC in dump.cs)
    		[Address(RVA = "0xD59260", Offset = "0xD57C60", VA = "0x180D59260")]
    		get
    		{
    			return _fillAmount;
    		}
    		[Token(Token = "0x6000060")]
    		[Address(RVA = "0xD59270", Offset = "0xD57C70", VA = "0x180D59270")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000013")]
    	public bool FillCenter
    	{
    		[Token(Token = "0x6000061")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD59310, 8 B -- `movzx eax, byte ptr [rcx + 0xf0]; ret`: the backing field _fillCenter (+0xF0 in dump.cs)
    		[Address(RVA = "0xD59310", Offset = "0xD57D10", VA = "0x180D59310")]
    		get
    		{
    			return _fillCenter;
    		}
    		[Token(Token = "0x6000062")]
    		[Address(RVA = "0xD59320", Offset = "0xD57D20", VA = "0x180D59320")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000014")]
    	public float PixelsPerUnitMultiplier
    	{
    		[Token(Token = "0x6000063")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD593A0, 9 B -- `movss xmm0, dword ptr [rcx + 0xf4]; ret`: the backing field _pixelsPerUnitMultiplier (+0xF4 in dump.cs)
    		[Address(RVA = "0xD593A0", Offset = "0xD57DA0", VA = "0x180D593A0")]
    		get
    		{
    			return _pixelsPerUnitMultiplier;
    		}
    		[Token(Token = "0x6000064")]
    		[Address(RVA = "0xD593B0", Offset = "0xD57DB0", VA = "0x180D593B0")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000015")]
    	private float PixelsPerUnit
    	{
    		[Token(Token = "0x6000065")]
    		[Address(RVA = "0xD593D0", Offset = "0xD57DD0", VA = "0x180D593D0")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x17000016")]
    	public Sprite OverrideSprite
    	{
    		[Token(Token = "0x6000066")]
    		[Address(RVA = "0xD595B0", Offset = "0xD57FB0", VA = "0x180D595B0")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6000067")]
    		[Address(RVA = "0xD595C0", Offset = "0xD57FC0", VA = "0x180D595C0")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000017")]
    	private Sprite ActiveSprite
    	{
    		[Token(Token = "0x6000068")]
    		[Address(RVA = "0xD59640", Offset = "0xD58040", VA = "0x180D59640")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000018")]
    	public override Texture mainTexture
    	{
    		[Token(Token = "0x6000069")]
    		[Address(RVA = "0xD59730", Offset = "0xD58130", VA = "0x180D59730", Slot = "35")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000019")]
    	private bool HasBorder
    	{
    		[Token(Token = "0x600006A")]
    		[Address(RVA = "0xD59A10", Offset = "0xD58410", VA = "0x180D59A10")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700001A")]
    	public override Material material
    	{
    		[Token(Token = "0x600006B")]
    		[Address(RVA = "0xD59BE0", Offset = "0xD585E0", VA = "0x180D59BE0", Slot = "32")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x600006C")]
    		[Address(RVA = "0xD59E60", Offset = "0xD58860", VA = "0x180D59E60", Slot = "33")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700001B")]
    	private float AlphaHitTestMinimumThreshold
    	{
    		[Token(Token = "0x600006D")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD59E70, 9 B -- `movss xmm0, dword ptr [rcx + 0xf8]; ret`: the backing field _alphaHitTestMinimumThreshold (+0xF8 in dump.cs)
    		[Address(RVA = "0xD59E70", Offset = "0xD58870", VA = "0x180D59E70")]
    		get
    		{
    			return _alphaHitTestMinimumThreshold;
    		}
    	}

    	[Token(Token = "0x600006E")]
    	[Address(RVA = "0xD59E80", Offset = "0xD58880", VA = "0x180D59E80")]
    	protected SlicedFilledImage()
    	{
    	}

    	[Token(Token = "0x600006F")]
    	[Address(RVA = "0xD59EC0", Offset = "0xD588C0", VA = "0x180D59EC0", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x6000070")]
    	[Address(RVA = "0xD59F20", Offset = "0xD58920", VA = "0x180D59F20", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x6000071")]
    	[Address(RVA = "0xD59FC0", Offset = "0xD589C0", VA = "0x180D59FC0", Slot = "76")]
    	private bool UnityEngine_002EICanvasRaycastFilter_002EIsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000072")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "67")]
    	private void UnityEngine_002EUI_002EILayoutElement_002ECalculateLayoutInputHorizontal()
    	{
    	}

    	[Token(Token = "0x6000073")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "68")]
    	private void UnityEngine_002EUI_002EILayoutElement_002ECalculateLayoutInputVertical()
    	{
    	}

    	[Token(Token = "0x6000074")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "65")]
    	private void UnityEngine_002EISerializationCallbackReceiver_002EOnBeforeSerialize()
    	{
    	}

    	[Token(Token = "0x6000075")]
    	[Address(RVA = "0xD5A9C0", Offset = "0xD593C0", VA = "0x180D5A9C0", Slot = "66")]
    	private void UnityEngine_002EISerializationCallbackReceiver_002EOnAfterDeserialize()
    	{
    	}

    	[Token(Token = "0x6000076")]
    	[Address(RVA = "0xD5A9F0", Offset = "0xD593F0", VA = "0x180D5A9F0", Slot = "44")]
    	protected override void OnPopulateMesh(VertexHelper vh)
    	{
    	}

    	[Token(Token = "0x6000077")]
    	[Address(RVA = "0xD5AB00", Offset = "0xD59500", VA = "0x180D5AB00", Slot = "40")]
    	protected override void UpdateMaterial()
    	{
    	}

    	[Token(Token = "0x6000078")]
    	[Address(RVA = "0xD5ACE0", Offset = "0xD596E0", VA = "0x180D5ACE0")]
    	private void GenerateSlicedFilledSprite(VertexHelper vh)
    	{
    	}

    	[Token(Token = "0x6000079")]
    	[Address(RVA = "0xD5BA80", Offset = "0xD5A480", VA = "0x180D5BA80")]
    	private Vector4 GetAdjustedBorders(Vector4 border, Rect adjustedRect)
    	{
    		return default(Vector4);
    	}

    	[Token(Token = "0x600007A")]
    	[Address(RVA = "0xD5BE20", Offset = "0xD5A820", VA = "0x180D5BE20")]
    	private void GenerateFilledSprite(VertexHelper vh, Vector4 vertices, Vector4 uvs, float fillAmount)
    	{
    	}

    	[Token(Token = "0x600007B")]
    	[Address(RVA = "0xD5C470", Offset = "0xD5AE70", VA = "0x180D5C470")]
    	private void TrackImage()
    	{
    	}

    	[Token(Token = "0x600007C")]
    	[Address(RVA = "0xD5C760", Offset = "0xD5B160", VA = "0x180D5C760")]
    	private void UnTrackImage()
    	{
    	}

    	[Token(Token = "0x600007D")]
    	[Address(RVA = "0xD5C7F0", Offset = "0xD5B1F0", VA = "0x180D5C7F0")]
    	private static void RebuildImage(SpriteAtlas spriteAtlas)
    	{
    	}
    }
}
