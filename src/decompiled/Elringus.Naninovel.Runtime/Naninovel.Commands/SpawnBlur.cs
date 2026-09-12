using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x2000713")]
[CommandAlias("blur")]
public class SpawnBlur : SpawnEffect
{
	[Token(Token = "0x4001B02")]
	[FieldOffset(Offset = "0x40")]
	[ParameterAlias("")]
	[ActorContext("*", -1, null)]
	[ParameterDefaultValue("MainBackground")]
	public StringParameter ActorId;

	[Token(Token = "0x4001B03")]
	[FieldOffset(Offset = "0x48")]
	[ParameterAlias("power")]
	public DecimalParameter Intensity;

	[Token(Token = "0x4001B04")]
	[FieldOffset(Offset = "0x50")]
	[ParameterAlias("time")]
	public DecimalParameter FadeDuration;

	[Token(Token = "0x170004E9")]
	protected override string Path
	{
		[Token(Token = "0x600219D")]
		[Address(RVA = "0x50B1C0", Offset = "0x509BC0", VA = "0x18050B1C0", Slot = "18")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004EA")]
	protected override bool DestroyWhen
	{
		[Token(Token = "0x600219E")]
		[Address(RVA = "0x50B250", Offset = "0x509C50", VA = "0x18050B250", Slot = "19")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x600219F")]
	[Address(RVA = "0x50B300", Offset = "0x509D00", VA = "0x18050B300", Slot = "23")]
	protected override StringListParameter GetSpawnParameters()
	{
		return null;
	}

	[Token(Token = "0x60021A0")]
	[Address(RVA = "0x50B420", Offset = "0x509E20", VA = "0x18050B420", Slot = "24")]
	protected override StringListParameter GetDestroyParameters()
	{
		return null;
	}

	[Token(Token = "0x60021A1")]
	[Address(RVA = "0x50B4E0", Offset = "0x509EE0", VA = "0x18050B4E0")]
	public SpawnBlur()
	{
	}
}
