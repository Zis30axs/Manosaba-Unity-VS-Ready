using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x2000680")]
public abstract class ModifyOrthoActor<TActor, TState, TMeta, TConfig, TManager> : ModifyActor<TActor, TState, TMeta, TConfig, TManager> where TActor : class, IActor where TState : ActorState<TActor>, new() where TMeta : OrthoActorMetadata where TConfig : OrthoActorManagerConfiguration<TMeta>, new() where TManager : class, IActorManager<TActor, TState, TMeta, TConfig>
{
	[Token(Token = "0x40018DD")]
	[FieldOffset(Offset = "0x0")]
	[ParameterAlias("pos")]
	[VectorContext("X,Y,Z", -1, null)]
	public DecimalListParameter ScenePosition;

	[Token(Token = "0x40018DE")]
	[FieldOffset(Offset = "0x0")]
	private float?[] worldPosition;

	[Token(Token = "0x40018DF")]
	[FieldOffset(Offset = "0x0")]
	private float?[] uniformScale;

	[Token(Token = "0x170004BF")]
	protected override float?[] AssignedPosition
	{
		[Token(Token = "0x6001FD9")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004C0")]
	protected override float?[] AssignedScale
	{
		[Token(Token = "0x6001FDA")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004C1")]
	protected virtual ICameraManager CameraManager
	{
		[Token(Token = "0x6001FDB")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001FDC")]
	private float?[] AttemptScenePosition()
	{
		return null;
	}

	[Token(Token = "0x6001FDD")]
	private float?[] AttemptUniformScale()
	{
		return null;
	}

	[Token(Token = "0x6001FDE")]
	protected ModifyOrthoActor()
	{
	}
}
