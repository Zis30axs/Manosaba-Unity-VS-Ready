using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20004A0")]
public class TransitionalMatcher : CameraMatcher
{
	[Token(Token = "0x17000332")]
	public Vector2 ReferenceSize
	{
		[Token(Token = "0x60016D2")]
		[Address(RVA = "0x5E9510", Offset = "0x5E7F10", VA = "0x1805E9510")]
		[CompilerGenerated]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x60016D3")]
		[Address(RVA = "0x5E9530", Offset = "0x5E7F30", VA = "0x1805E9530")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x17000333")]
	public float Scale
	{
		[Token(Token = "0x60016D4")]
		[Address(RVA = "0x3473F0", Offset = "0x345DF0", VA = "0x1803473F0")]
		[CompilerGenerated]
		get
		{
			return default(float);
		}
		[Token(Token = "0x60016D5")]
		[Address(RVA = "0x347400", Offset = "0x345E00", VA = "0x180347400")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x60016D6")]
	[Address(RVA = "0x5E9540", Offset = "0x5E7F40", VA = "0x1805E9540")]
	public TransitionalMatcher(ICameraManager cameraManager, Object target)
	{
	}

	[Token(Token = "0x60016D7")]
	[Address(RVA = "0x5E9550", Offset = "0x5E7F50", VA = "0x1805E9550")]
	public float GetScaleFactor(Vector2 contentSize)
	{
		return default(float);
	}

	[Token(Token = "0x60016D8")]
	[Address(RVA = "0x5E95B0", Offset = "0x5E7FB0", VA = "0x1805E95B0", Slot = "8")]
	protected override bool TryGetReferenceSize(out Vector2 referenceSize)
	{
		referenceSize = default;
		referenceSize = default;
		return default(bool);
	}

	[Token(Token = "0x60016D9")]
	[Address(RVA = "0x347400", Offset = "0x345E00", VA = "0x180347400", Slot = "10")]
	protected override void ApplyScale(float scaleFactor)
	{
	}
}
