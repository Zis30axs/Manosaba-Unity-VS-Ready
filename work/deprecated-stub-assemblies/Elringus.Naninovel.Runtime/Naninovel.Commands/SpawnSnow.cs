using Il2CppDummyDll;

namespace Naninovel.Commands
{
    [Token(Token = "0x200071C")]
    [CommandAlias("snow")]
    public class SpawnSnow : SpawnLocalizedEffect
    {
    	[Token(Token = "0x4001B24")]
    	[FieldOffset(Offset = "0x60")]
    	[ParameterAlias("power")]
    	public DecimalParameter Intensity;

    	[Token(Token = "0x4001B25")]
    	[FieldOffset(Offset = "0x68")]
    	[ParameterAlias("time")]
    	public DecimalParameter FadeDuration;

    	[Token(Token = "0x170004F5")]
    	protected override string Path
    	{
    		[Token(Token = "0x60021CF")]
    		[Address(RVA = "0x50D3B0", Offset = "0x50BDB0", VA = "0x18050D3B0", Slot = "18")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170004F6")]
    	protected override bool DestroyWhen
    	{
    		[Token(Token = "0x60021D0")]
    		[Address(RVA = "0x50D3F0", Offset = "0x50BDF0", VA = "0x18050D3F0", Slot = "19")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x60021D1")]
    	[Address(RVA = "0x50D4A0", Offset = "0x50BEA0", VA = "0x18050D4A0", Slot = "23")]
    	protected override StringListParameter GetSpawnParameters()
    	{
    		return null;
    	}

    	[Token(Token = "0x60021D2")]
    	[Address(RVA = "0x50D590", Offset = "0x50BF90", VA = "0x18050D590", Slot = "24")]
    	protected override StringListParameter GetDestroyParameters()
    	{
    		return null;
    	}

    	[Token(Token = "0x60021D3")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public SpawnSnow()
    	{
    	}
    }
}
