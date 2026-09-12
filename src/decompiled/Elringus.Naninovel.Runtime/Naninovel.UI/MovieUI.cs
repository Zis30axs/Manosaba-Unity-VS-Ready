using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI;

[Token(Token = "0x2000565")]
public class MovieUI : CustomUI, IMovieUI, IManagedUI
{
	[Token(Token = "0x4001497")]
	[FieldOffset(Offset = "0xE0")]
	[SerializeField]
	private RawImage movieImage;

	[Token(Token = "0x4001498")]
	[FieldOffset(Offset = "0xE8")]
	[SerializeField]
	private RawImage fadeImage;

	[Token(Token = "0x170003C5")]
	protected virtual RawImage MovieImage
	{
		[Token(Token = "0x6001AC0")]
		[Address(RVA = "0x33E0B0", Offset = "0x33CAB0", VA = "0x18033E0B0", Slot = "96")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003C6")]
	protected virtual RawImage FadeImage
	{
		[Token(Token = "0x6001AC1")]
		[Address(RVA = "0x49D2A0", Offset = "0x49BCA0", VA = "0x18049D2A0", Slot = "97")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001AC2")]
	[Address(RVA = "0x62F0C0", Offset = "0x62DAC0", VA = "0x18062F0C0", Slot = "98")]
	public virtual void SetMovieTexture(Texture texture)
	{
	}

	[Token(Token = "0x6001AC3")]
	[Address(RVA = "0x62F1B0", Offset = "0x62DBB0", VA = "0x18062F1B0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001AC4")]
	[Address(RVA = "0x62F3F0", Offset = "0x62DDF0", VA = "0x18062F3F0", Slot = "55")]
	protected override void HandleVisibilityChanged(bool visible)
	{
	}

	[Token(Token = "0x6001AC5")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public MovieUI()
	{
	}
}
