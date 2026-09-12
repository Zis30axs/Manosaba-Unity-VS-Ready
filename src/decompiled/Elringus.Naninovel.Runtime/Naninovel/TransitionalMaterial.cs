using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20004A1")]
public class TransitionalMaterial
{
	[Token(Token = "0x400114D")]
	private const string defaultShaderName = "Naninovel/TransitionalTexture";

	[Token(Token = "0x400114E")]
	private const string cloudsTexturePath = "Textures/Clouds";

	[Token(Token = "0x400114F")]
	private const string premultipliedAlphaKey = "PREMULTIPLIED_ALPHA";

	[Token(Token = "0x4001150")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly int mainTexId;

	[Token(Token = "0x4001151")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x4")]
	private static readonly int transitionTexId;

	[Token(Token = "0x4001152")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
	private static readonly int cloudsTexId;

	[Token(Token = "0x4001153")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC")]
	private static readonly int dissolveTexId;

	[Token(Token = "0x4001154")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private static readonly int transitionProgressId;

	[Token(Token = "0x4001155")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
	private static readonly int transitionParamsId;

	[Token(Token = "0x4001156")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private static readonly int randomSeedId;

	[Token(Token = "0x4001157")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
	private static readonly int tintColorId;

	[Token(Token = "0x4001158")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private static readonly int flipMainId;

	[Token(Token = "0x4001159")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private static Texture2D sharedCloudsTexture;

	[Token(Token = "0x17000334")]
	public Texture MainTexture
	{
		[Token(Token = "0x60016DA")]
		[Address(RVA = "0x5E95D0", Offset = "0x5E7FD0", VA = "0x1805E95D0")]
		get
		{
			return null;
		}
		[Token(Token = "0x60016DB")]
		[Address(RVA = "0x5E95F0", Offset = "0x5E7FF0", VA = "0x1805E95F0")]
		set
		{
		}
	}

	[Token(Token = "0x17000335")]
	public virtual Vector2 MainTextureOffset
	{
		[Token(Token = "0x60016DC")]
		[Address(RVA = "0x5E9610", Offset = "0x5E8010", VA = "0x1805E9610", Slot = "4")]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x60016DD")]
		[Address(RVA = "0x5E96A0", Offset = "0x5E80A0", VA = "0x1805E96A0", Slot = "5")]
		set
		{
		}
	}

	[Token(Token = "0x17000336")]
	public virtual Vector2 MainTextureScale
	{
		[Token(Token = "0x60016DE")]
		[Address(RVA = "0x5E9760", Offset = "0x5E8160", VA = "0x1805E9760", Slot = "6")]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x60016DF")]
		[Address(RVA = "0x5E97F0", Offset = "0x5E81F0", VA = "0x1805E97F0", Slot = "7")]
		set
		{
		}
	}

	[Token(Token = "0x17000337")]
	public Texture TransitionTexture
	{
		[Token(Token = "0x60016E0")]
		[Address(RVA = "0x5E98B0", Offset = "0x5E82B0", VA = "0x1805E98B0")]
		get
		{
			return null;
		}
		[Token(Token = "0x60016E1")]
		[Address(RVA = "0x5E9930", Offset = "0x5E8330", VA = "0x1805E9930")]
		set
		{
		}
	}

	[Token(Token = "0x17000338")]
	public virtual Vector2 TransitionTextureOffset
	{
		[Token(Token = "0x60016E2")]
		[Address(RVA = "0x5E99C0", Offset = "0x5E83C0", VA = "0x1805E99C0", Slot = "8")]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x60016E3")]
		[Address(RVA = "0x5E9A40", Offset = "0x5E8440", VA = "0x1805E9A40", Slot = "9")]
		set
		{
		}
	}

	[Token(Token = "0x17000339")]
	public virtual Vector2 TransitionTextureScale
	{
		[Token(Token = "0x60016E4")]
		[Address(RVA = "0x5E9AD0", Offset = "0x5E84D0", VA = "0x1805E9AD0", Slot = "10")]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x60016E5")]
		[Address(RVA = "0x5E9B50", Offset = "0x5E8550", VA = "0x1805E9B50", Slot = "11")]
		set
		{
		}
	}

	[Token(Token = "0x1700033A")]
	public Texture DissolveTexture
	{
		[Token(Token = "0x60016E6")]
		[Address(RVA = "0x5E9BE0", Offset = "0x5E85E0", VA = "0x1805E9BE0")]
		get
		{
			return null;
		}
		[Token(Token = "0x60016E7")]
		[Address(RVA = "0x5E9C60", Offset = "0x5E8660", VA = "0x1805E9C60")]
		set
		{
		}
	}

	[Token(Token = "0x1700033B")]
	public string TransitionName
	{
		[Token(Token = "0x60016E8")]
		[Address(RVA = "0x5E9CF0", Offset = "0x5E86F0", VA = "0x1805E9CF0")]
		get
		{
			return null;
		}
		[Token(Token = "0x60016E9")]
		[Address(RVA = "0x5E9EA0", Offset = "0x5E88A0", VA = "0x1805E9EA0")]
		set
		{
		}
	}

	[Token(Token = "0x1700033C")]
	public float TransitionProgress
	{
		[Token(Token = "0x60016EA")]
		[Address(RVA = "0x5E9F10", Offset = "0x5E8910", VA = "0x1805E9F10")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x60016EB")]
		[Address(RVA = "0x5E9F90", Offset = "0x5E8990", VA = "0x1805E9F90")]
		set
		{
		}
	}

	[Token(Token = "0x1700033D")]
	public Vector4 TransitionParams
	{
		[Token(Token = "0x60016EC")]
		[Address(RVA = "0x5EA020", Offset = "0x5E8A20", VA = "0x1805EA020")]
		get
		{
			return default(Vector4);
		}
		[Token(Token = "0x60016ED")]
		[Address(RVA = "0x5EA0B0", Offset = "0x5E8AB0", VA = "0x1805EA0B0")]
		set
		{
		}
	}

	[Token(Token = "0x1700033E")]
	public Vector2 RandomSeed
	{
		[Token(Token = "0x60016EE")]
		[Address(RVA = "0x5EA140", Offset = "0x5E8B40", VA = "0x1805EA140")]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x60016EF")]
		[Address(RVA = "0x5EA1D0", Offset = "0x5E8BD0", VA = "0x1805EA1D0")]
		set
		{
		}
	}

	[Token(Token = "0x1700033F")]
	public Color TintColor
	{
		[Token(Token = "0x60016F0")]
		[Address(RVA = "0x5EA280", Offset = "0x5E8C80", VA = "0x1805EA280")]
		get
		{
			return default(Color);
		}
		[Token(Token = "0x60016F1")]
		[Address(RVA = "0x5EA310", Offset = "0x5E8D10", VA = "0x1805EA310")]
		set
		{
		}
	}

	[Token(Token = "0x17000340")]
	public float Opacity
	{
		[Token(Token = "0x60016F2")]
		[Address(RVA = "0x5EA440", Offset = "0x5E8E40", VA = "0x1805EA440")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x60016F3")]
		[Address(RVA = "0x5EA4C0", Offset = "0x5E8EC0", VA = "0x1805EA4C0")]
		set
		{
		}
	}

	[Token(Token = "0x17000341")]
	public bool FlipMain
	{
		[Token(Token = "0x60016F4")]
		[Address(RVA = "0x5EA590", Offset = "0x5E8F90", VA = "0x1805EA590")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x60016F5")]
		[Address(RVA = "0x5EA610", Offset = "0x5E9010", VA = "0x1805EA610")]
		set
		{
		}
	}

	[Token(Token = "0x17000342")]
	public Material Object
	{
		[Token(Token = "0x60016F6")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60016F7")]
	[Address(RVA = "0x5EA6A0", Offset = "0x5E90A0", VA = "0x1805EA6A0")]
	public TransitionalMaterial(bool premultipliedAlpha, [Optional] Material custom, HideFlags hideFlags = HideFlags.HideAndDontSave)
	{
	}

	[Token(Token = "0x60016F8")]
	[Address(RVA = "0x5EAA90", Offset = "0x5E9490", VA = "0x1805EAA90")]
	public void UpdateRandomSeed()
	{
	}

	[Token(Token = "0x60016F9")]
	[Address(RVA = "0x5EA4C0", Offset = "0x5E8EC0", VA = "0x1805EA4C0")]
	private void SetOpacity(float value)
	{
	}
}
