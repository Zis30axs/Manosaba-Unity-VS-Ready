using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x200001D")]
public abstract class ActorMetadata
{
	[Token(Token = "0x4000083")]
	[FieldOffset(Offset = "0x10")]
	[Tooltip("Assembly-qualified type name of the actor implementation.")]
	public string Implementation;

	[Token(Token = "0x4000084")]
	[FieldOffset(Offset = "0x18")]
	[Tooltip("Data describing how to load actor's resources.")]
	public ResourceLoaderConfiguration Loader;

	[Token(Token = "0x4000085")]
	[FieldOffset(Offset = "0x20")]
	[HideInInspector]
	[SerializeField]
	private string guid;

	[Token(Token = "0x4000086")]
	[FieldOffset(Offset = "0x28")]
	[SerializeReference]
	private CustomMetadata customData;

	[Token(Token = "0x1700000B")]
	public string Guid
	{
		[Token(Token = "0x600006C")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600006D")]
	public abstract ActorPose<TState> GetPose<TState>(string poseName) where TState : ActorState;

	[Token(Token = "0x600006E")]
	public virtual TData GetCustomData<TData>() where TData : CustomMetadata
	{
		return null;
	}

	[Token(Token = "0x600006F")]
	[Address(RVA = "0x44BC80", Offset = "0x44A680", VA = "0x18044BC80")]
	public string GetResourceCategoryId()
	{
		return null;
	}

	[Token(Token = "0x6000070")]
	[Address(RVA = "0x44BCE0", Offset = "0x44A6E0", VA = "0x18044BCE0")]
	protected ActorMetadata()
	{
	}
}
