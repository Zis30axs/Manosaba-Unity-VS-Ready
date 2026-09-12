using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000041")]
[EditInProjectSettings]
public class CharactersConfiguration : OrthoActorManagerConfiguration<CharacterMetadata>
{
	[Token(Token = "0x40000E5")]
	public const string DefaultPathPrefix = "Characters";

	[Token(Token = "0x40000E6")]
	public const string DefaultAvatarsPathPrefix = "CharacterAvatars";

	[Token(Token = "0x40000E7")]
	[FieldOffset(Offset = "0x38")]
	[Tooltip("Whether to evenly distribute characters by X-axis when adding a new character without a specified position.")]
	public bool AutoArrangeOnAdd;

	[Token(Token = "0x40000E8")]
	[FieldOffset(Offset = "0x3C")]
	[Tooltip("Start (x) and end (y) positions (in 0.0 to 1.0 range) relative to scene width representing the range over which the characters are arranged.")]
	public Vector2 ArrangeRange;

	[Token(Token = "0x40000E9")]
	[FieldOffset(Offset = "0x48")]
	[Tooltip("Metadata to use by default when creating character actors and custom metadata for the created actor ID doesn't exist.")]
	public CharacterMetadata DefaultMetadata;

	[Token(Token = "0x40000EA")]
	[FieldOffset(Offset = "0x50")]
	[Tooltip("Metadata to use when creating character actors with specific IDs.")]
	public CharacterMetadata.Map Metadata;

	[Token(Token = "0x40000EB")]
	[FieldOffset(Offset = "0x58")]
	[Tooltip("Configuration of the resource loader used with character avatar texture resources.")]
	public ResourceLoaderConfiguration AvatarLoader;

	[Token(Token = "0x40000EC")]
	[FieldOffset(Offset = "0x60")]
	[Tooltip("Named states (poses) shared between the characters; pose name can be used as appearance in [@char] commands to set enabled properties of the associated state.")]
	public List<CharacterMetadata.Pose> SharedPoses;

	[Token(Token = "0x1700001A")]
	public override CharacterMetadata DefaultActorMetadata
	{
		[Token(Token = "0x60000F4")]
		[Address(RVA = "0x453470", Offset = "0x451E70", VA = "0x180453470", Slot = "7")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700001B")]
	public override ActorMetadataMap<CharacterMetadata> ActorMetadataMap
	{
		[Token(Token = "0x60000F5")]
		[Address(RVA = "0x453480", Offset = "0x451E80", VA = "0x180453480", Slot = "8")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60000F6")]
	[Address(RVA = "0x453490", Offset = "0x451E90", VA = "0x180453490")]
	public CharactersConfiguration()
	{
	}

	[Token(Token = "0x60000F7")]
	protected override ActorPose<TState> GetSharedPose<TState>(string poseName)
	{
		return null;
	}
}
