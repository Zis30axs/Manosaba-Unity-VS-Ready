using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x2000714")]
[CommandAlias("bokeh")]
public class SpawnBokeh : SpawnEffect
{
	[Token(Token = "0x4001B05")]
	[FieldOffset(Offset = "0x40")]
	[ParameterAlias("focus")]
	public StringParameter FocusObjectName;

	[Token(Token = "0x4001B06")]
	[FieldOffset(Offset = "0x48")]
	[ParameterAlias("dist")]
	public DecimalParameter FocusDistance;

	[Token(Token = "0x4001B07")]
	[FieldOffset(Offset = "0x50")]
	[ParameterAlias("power")]
	public DecimalParameter FocalLength;

	[Token(Token = "0x4001B08")]
	[FieldOffset(Offset = "0x58")]
	[ParameterAlias("time")]
	public DecimalParameter Duration;

	[Token(Token = "0x170004EB")]
	protected override string Path
	{
		[Token(Token = "0x60021A2")]
		[Address(RVA = "0x50B570", Offset = "0x509F70", VA = "0x18050B570", Slot = "18")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004EC")]
	protected override bool DestroyWhen
	{
		[Token(Token = "0x60021A3")]
		[Address(RVA = "0x50B5B0", Offset = "0x509FB0", VA = "0x18050B5B0", Slot = "19")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x60021A4")]
	[Address(RVA = "0x50B660", Offset = "0x50A060", VA = "0x18050B660", Slot = "23")]
	protected override StringListParameter GetSpawnParameters()
	{
		return null;
	}

	[Token(Token = "0x60021A5")]
	[Address(RVA = "0x50B7B0", Offset = "0x50A1B0", VA = "0x18050B7B0", Slot = "24")]
	protected override StringListParameter GetDestroyParameters()
	{
		return null;
	}

	[Token(Token = "0x60021A6")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public SpawnBokeh()
	{
	}
}
