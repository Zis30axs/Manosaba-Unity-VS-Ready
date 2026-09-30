using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000018")]
    public abstract class ActorManagerConfiguration : Configuration
    {
    	[Token(Token = "0x4000076")]
    	[FieldOffset(Offset = "0x18")]
    	[Tooltip("Default duration (in seconds) for all the actor modifications (changing appearance, position, tint, etc).")]
    	public float DefaultDuration;

    	[Token(Token = "0x4000077")]
    	[FieldOffset(Offset = "0x1C")]
    	[Tooltip("Easing function to use by default for all the actor modification animations (changing appearance, position, tint, etc).")]
    	public EasingType DefaultEasing;

    	[Token(Token = "0x4000078")]
    	[FieldOffset(Offset = "0x20")]
    	[Tooltip("Whether to automatically reveal (show) an actor when executing modification commands.")]
    	public bool AutoShowOnModify;

    	[Token(Token = "0x17000007")]
    	public abstract ActorMetadataMap MetadataMap
    	{
    		[Token(Token = "0x6000059")]
    		get;
    	}

    	[Token(Token = "0x600005A")]
    	[Address(RVA = "0x44B700", Offset = "0x44A100", VA = "0x18044B700")]
    	public ActorMetadata GetMetadataOrDefault(string actorId)
    	{
    		return null;
    	}

    	[Token(Token = "0x600005B")]
    	public ActorPose<TState> GetActorOrSharedPose<TState>(string actorId, string poseName) where TState : ActorState
    	{
    		return null;
    	}

    	[Token(Token = "0x600005C")]
    	protected abstract ActorMetadata GetMetadataOrDefaultNonGeneric(string actorId);

    	[Token(Token = "0x600005D")]
    	protected abstract ActorPose<TState> GetSharedPose<TState>(string poseName) where TState : ActorState;

    	[Token(Token = "0x600005E")]
    	[Address(RVA = "0x44B720", Offset = "0x44A120", VA = "0x18044B720")]
    	protected ActorManagerConfiguration()
    	{
    	}
    }
    [Token(Token = "0x2000019")]
    public abstract class ActorManagerConfiguration<TMeta> : ActorManagerConfiguration where TMeta : ActorMetadata
    {
    	[Token(Token = "0x17000008")]
    	public abstract TMeta DefaultActorMetadata
    	{
    		[Token(Token = "0x600005F")]
    		get;
    	}

    	[Token(Token = "0x17000009")]
    	public abstract ActorMetadataMap<TMeta> ActorMetadataMap
    	{
    		[Token(Token = "0x6000060")]
    		get;
    	}

    	[Token(Token = "0x1700000A")]
    	public override ActorMetadataMap MetadataMap
    	{
    		[Token(Token = "0x6000061")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000062")]
    	public new TMeta GetMetadataOrDefault(string actorId)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000063")]
    	protected override ActorMetadata GetMetadataOrDefaultNonGeneric(string actorId)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000064")]
    	protected ActorManagerConfiguration()
    	{
    	}
    }
}
