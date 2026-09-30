using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200004E")]
    [EditInProjectSettings]
    public class ChoiceHandlersConfiguration : ActorManagerConfiguration<ChoiceHandlerMetadata>
    {
    	[Token(Token = "0x4000101")]
    	public const string DefaultPathPrefix = "ChoiceHandlers";

    	[Token(Token = "0x4000102")]
    	[FieldOffset(Offset = "0x28")]
    	[Tooltip("ID of the choice handler to use by default.")]
    	public string DefaultHandlerId;

    	[Token(Token = "0x4000103")]
    	[FieldOffset(Offset = "0x30")]
    	[Tooltip("Configuration of the resource loader used for loading custom choice buttons.")]
    	public ResourceLoaderConfiguration ChoiceButtonLoader;

    	[Token(Token = "0x4000104")]
    	[FieldOffset(Offset = "0x38")]
    	[Tooltip("Metadata to use by default when creating choice handler actors and custom metadata for the created actor ID doesn't exist.")]
    	public ChoiceHandlerMetadata DefaultMetadata;

    	[Token(Token = "0x4000105")]
    	[FieldOffset(Offset = "0x40")]
    	[Tooltip("Metadata to use when creating choice handler actors with specific IDs.")]
    	public ChoiceHandlerMetadata.Map Metadata;

    	[Token(Token = "0x17000020")]
    	public override ChoiceHandlerMetadata DefaultActorMetadata
    	{
    		[Token(Token = "0x600011E")]
    		[Address(RVA = "0x347410", Offset = "0x345E10", VA = "0x180347410", Slot = "7")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000021")]
    	public override ActorMetadataMap<ChoiceHandlerMetadata> ActorMetadataMap
    	{
    		[Token(Token = "0x600011F")]
    		[Address(RVA = "0x44CE10", Offset = "0x44B810", VA = "0x18044CE10", Slot = "8")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000120")]
    	protected override ActorPose<TState> GetSharedPose<TState>(string poseName)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000121")]
    	[Address(RVA = "0x454B80", Offset = "0x453580", VA = "0x180454B80")]
    	private static ChoiceHandlerMetadata CreateBuiltinMeta()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000122")]
    	[Address(RVA = "0x454CB0", Offset = "0x4536B0", VA = "0x180454CB0")]
    	public ChoiceHandlersConfiguration()
    	{
    	}
    }
}
