using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x200071D")]
[CommandAlias("sun")]
public class SpawnSun : SpawnLocalizedEffect
{
	[Token(Token = "0x4001B26")]
	[FieldOffset(Offset = "0x60")]
	[ParameterAlias("power")]
	public DecimalParameter Intensity;

	[Token(Token = "0x4001B27")]
	[FieldOffset(Offset = "0x68")]
	[ParameterAlias("time")]
	public DecimalParameter FadeDuration;

	[Token(Token = "0x170004F7")]
	protected override string Path
	{
		[Token(Token = "0x60021D4")]
		[Address(RVA = "0x50D650", Offset = "0x50C050", VA = "0x18050D650", Slot = "18")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004F8")]
	protected override bool DestroyWhen
	{
		[Token(Token = "0x60021D5")]
		[Address(RVA = "0x50D690", Offset = "0x50C090", VA = "0x18050D690", Slot = "19")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x60021D6")]
	[Address(RVA = "0x50D740", Offset = "0x50C140", VA = "0x18050D740", Slot = "23")]
	protected override StringListParameter GetSpawnParameters()
	{
		return null;
	}

	[Token(Token = "0x60021D7")]
	[Address(RVA = "0x50D830", Offset = "0x50C230", VA = "0x18050D830", Slot = "24")]
	protected override StringListParameter GetDestroyParameters()
	{
		return null;
	}

	[Token(Token = "0x60021D8")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public SpawnSun()
	{
	}
}
