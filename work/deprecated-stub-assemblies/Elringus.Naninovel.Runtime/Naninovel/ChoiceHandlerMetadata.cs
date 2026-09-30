using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x200004C")]
    public class ChoiceHandlerMetadata : ActorMetadata
    {
    	[Serializable]
    	[Token(Token = "0x200004D")]
    	public class Map : ActorMetadataMap<ChoiceHandlerMetadata>
    	{
    		[Token(Token = "0x600011D")]
    		[Address(RVA = "0x454B40", Offset = "0x453540", VA = "0x180454B40")]
    		public Map()
    		{
    		}
    	}

    	[Token(Token = "0x4000100")]
    	[FieldOffset(Offset = "0x30")]
    	[Tooltip("Whether to wait until the choice handler UI is completely hidden before proceeding when a choice is picked.")]
    	public bool WaitHideOnChoice;

    	[Token(Token = "0x600011B")]
    	[Address(RVA = "0x454930", Offset = "0x453330", VA = "0x180454930")]
    	public ChoiceHandlerMetadata()
    	{
    	}

    	[Token(Token = "0x600011C")]
    	public override ActorPose<TState> GetPose<TState>(string poseName)
    	{
    		return null;
    	}
    }
}
