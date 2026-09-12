using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000020")]
public abstract class ActorPose<TState> where TState : ActorState
{
	[Token(Token = "0x4000089")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private string name;

	[Token(Token = "0x400008A")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private TState actorState;

	[Token(Token = "0x400008B")]
	[FieldOffset(Offset = "0x0")]
	[HideInInspector]
	[SerializeField]
	private string[] overriddenProperties;

	[Token(Token = "0x1700000F")]
	public string Name
	{
		[Token(Token = "0x600008A")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000010")]
	public TState ActorState
	{
		[Token(Token = "0x600008B")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600008C")]
	public bool IsPropertyOverridden(string propertyName)
	{
		return default(bool);
	}

	[Token(Token = "0x600008D")]
	protected ActorPose()
	{
	}
}
