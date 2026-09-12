using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x200071A")]
[CommandAlias("shake")]
public class SpawnShake : SpawnEffect
{
	[Token(Token = "0x4001B19")]
	[FieldOffset(Offset = "0x40")]
	[ParameterAlias("")]
	[ActorContext("*", -1, null)]
	public StringParameter ActorId;

	[Token(Token = "0x4001B1A")]
	[FieldOffset(Offset = "0x48")]
	[ParameterAlias("count")]
	public IntegerParameter ShakeCount;

	[Token(Token = "0x4001B1B")]
	[FieldOffset(Offset = "0x50")]
	[ParameterAlias("time")]
	public DecimalParameter ShakeDuration;

	[Token(Token = "0x4001B1C")]
	[FieldOffset(Offset = "0x58")]
	[ParameterAlias("deltaTime")]
	public DecimalParameter DurationVariation;

	[Token(Token = "0x4001B1D")]
	[FieldOffset(Offset = "0x60")]
	[ParameterAlias("power")]
	public DecimalParameter ShakeAmplitude;

	[Token(Token = "0x4001B1E")]
	[FieldOffset(Offset = "0x68")]
	[ParameterAlias("deltaPower")]
	public DecimalParameter AmplitudeVariation;

	[Token(Token = "0x4001B1F")]
	[FieldOffset(Offset = "0x70")]
	[ParameterAlias("hor")]
	public BooleanParameter ShakeHorizontally;

	[Token(Token = "0x4001B20")]
	[FieldOffset(Offset = "0x78")]
	[ParameterAlias("ver")]
	public BooleanParameter ShakeVertically;

	[Token(Token = "0x4001B21")]
	private const string cameraId = "Camera";

	[Token(Token = "0x170004F3")]
	protected override string Path
	{
		[Token(Token = "0x60021C7")]
		[Address(RVA = "0x50CB20", Offset = "0x50B520", VA = "0x18050CB20", Slot = "18")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004F4")]
	protected override bool DestroyWhen
	{
		[Token(Token = "0x60021C8")]
		[Address(RVA = "0x50CB40", Offset = "0x50B540", VA = "0x18050CB40", Slot = "19")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x60021C9")]
	[Address(RVA = "0x50CBF0", Offset = "0x50B5F0", VA = "0x18050CBF0", Slot = "23")]
	protected override StringListParameter GetSpawnParameters()
	{
		return null;
	}

	[Token(Token = "0x60021CA")]
	[Address(RVA = "0x50CEB0", Offset = "0x50B8B0", VA = "0x18050CEB0", Slot = "33")]
	protected virtual string ResolvePath()
	{
		return null;
	}

	[Token(Token = "0x60021CB")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public SpawnShake()
	{
	}
}
