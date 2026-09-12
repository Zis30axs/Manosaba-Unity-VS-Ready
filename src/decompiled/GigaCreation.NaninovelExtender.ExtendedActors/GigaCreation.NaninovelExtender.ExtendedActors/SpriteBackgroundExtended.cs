using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x2000030")]
[UsedImplicitly]
[ActorResources(typeof(Texture2D), true)]
public class SpriteBackgroundExtended : SpriteBackground, IMultipliableActor, IPivotChangeableActor, IMaskabilityChangeableActor
{
	[Token(Token = "0x17000010")]
	public string SubId
	{
		[Token(Token = "0x600009F")]
		[Address(RVA = "0x492CE0", Offset = "0x4916E0", VA = "0x180492CE0", Slot = "73")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000011")]
	public Vector2 Pivot
	{
		[Token(Token = "0x60000A0")]
		[Address(RVA = "0xD1CE90", Offset = "0xD1B890", VA = "0x180D1CE90", Slot = "74")]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x60000A1")]
		[Address(RVA = "0xD1CED0", Offset = "0xD1B8D0", VA = "0x180D1CED0", Slot = "75")]
		set
		{
		}
	}

	[Token(Token = "0x17000012")]
	public bool Masked
	{
		[Token(Token = "0x60000A2")]
		[Address(RVA = "0xD1E410", Offset = "0xD1CE10", VA = "0x180D1E410", Slot = "77")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x60000A3")]
		[Address(RVA = "0xD1E510", Offset = "0xD1CF10", VA = "0x180D1E510", Slot = "78")]
		set
		{
		}
	}

	[Token(Token = "0x60000A4")]
	[Address(RVA = "0x46C8C0", Offset = "0x46B2C0", VA = "0x18046C8C0")]
	public SpriteBackgroundExtended(string id = default, BackgroundMetadata metadata = default, StandaloneAppearanceLoader<Texture2D> loader = default)
	{
	}

	[Token(Token = "0x60000A5")]
	[Address(RVA = "0x313980", Offset = "0x312380", VA = "0x180313980")]
	public SpriteBackgroundExtended(string actorId, string subId, BackgroundMetadata metadata, StandaloneAppearanceLoader<Texture2D> loader)
	{
	}

	[Token(Token = "0x60000A6")]
	[Address(RVA = "0xD1E620", Offset = "0xD1D020", VA = "0x180D1E620", Slot = "76")]
	public void ChangePivot(float? x, float? y)
	{
	}

	[Token(Token = "0x60000A7")]
	[Address(RVA = "0xD1E510", Offset = "0xD1CF10", VA = "0x180D1E510", Slot = "79")]
	public void ChangeMaskability(bool enable)
	{
	}

	[Token(Token = "0x60000A8")]
	[Address(RVA = "0xD1E410", Offset = "0xD1CE10", VA = "0x180D1E410")]
	private bool IsMasked()
	{
		return default(bool);
	}
}
