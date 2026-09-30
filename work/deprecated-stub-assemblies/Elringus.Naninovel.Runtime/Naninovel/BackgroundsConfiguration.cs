using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200002C")]
    [EditInProjectSettings]
    public class BackgroundsConfiguration : OrthoActorManagerConfiguration<BackgroundMetadata>
    {
    	[Token(Token = "0x400009F")]
    	public const string MainActorId = "MainBackground";

    	[Token(Token = "0x40000A0")]
    	public const string DefaultPathPrefix = "Backgrounds";

    	[Token(Token = "0x40000A1")]
    	[FieldOffset(Offset = "0x38")]
    	[Tooltip("Metadata to use by default when creating background actors and custom metadata for the created actor ID doesn't exist.")]
    	public BackgroundMetadata DefaultMetadata;

    	[Token(Token = "0x40000A2")]
    	[FieldOffset(Offset = "0x40")]
    	[Tooltip("Metadata to use when creating background actors with specific IDs.")]
    	public BackgroundMetadata.Map Metadata;

    	[Token(Token = "0x40000A3")]
    	[FieldOffset(Offset = "0x48")]
    	[Tooltip("Named states (poses) shared between the backgrounds; pose name can be used as appearance in [@back] commands to set enabled properties of the associated state.")]
    	public List<BackgroundMetadata.Pose> SharedPoses;

    	[Token(Token = "0x17000017")]
    	public override BackgroundMetadata DefaultActorMetadata
    	{
    		[Token(Token = "0x60000B5")]
    		[Address(RVA = "0x347410", Offset = "0x345E10", VA = "0x180347410", Slot = "7")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000018")]
    	public override ActorMetadataMap<BackgroundMetadata> ActorMetadataMap
    	{
    		[Token(Token = "0x60000B6")]
    		[Address(RVA = "0x44CE10", Offset = "0x44B810", VA = "0x18044CE10", Slot = "8")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60000B7")]
    	protected override ActorPose<TState> GetSharedPose<TState>(string poseName)
    	{
    		return null;
    	}

    	[Token(Token = "0x60000B8")]
    	[Address(RVA = "0x44CE20", Offset = "0x44B820", VA = "0x18044CE20")]
    	public BackgroundsConfiguration()
    	{
    	}
    }
}
