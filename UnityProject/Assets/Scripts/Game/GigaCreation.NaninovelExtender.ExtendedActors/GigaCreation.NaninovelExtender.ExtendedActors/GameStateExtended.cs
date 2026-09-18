using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Serializable]
    [Token(Token = "0x200001B")]
    public class GameStateExtended<TState> : ISerializationCallbackReceiver where TState : ActorState, new()
    {
    	[Token(Token = "0x4000057")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private SerializableLiteralStringMap _actorsJsonMap;

    	[Token(Token = "0x17000004")]
    	public IDictionary<string, TState> ActorsMap
    	{
    		[Token(Token = "0x6000043")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6000044")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x6000045")]
    	public void OnBeforeSerialize()
    	{
    	}

    	[Token(Token = "0x6000046")]
    	public void OnAfterDeserialize()
    	{
    	}

    	[Token(Token = "0x6000047")]
    	public GameStateExtended()
    	{
    	}
    }
}
