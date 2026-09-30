using Il2CppDummyDll;

namespace Naninovel.Commands
{
    [Token(Token = "0x2000717")]
    [CommandAlias("glitch")]
    public class SpawnGlitch : SpawnEffect
    {
    	[Token(Token = "0x4001B0F")]
    	[FieldOffset(Offset = "0x40")]
    	[ParameterAlias("time")]
    	public DecimalParameter Duration;

    	[Token(Token = "0x4001B10")]
    	[FieldOffset(Offset = "0x48")]
    	[ParameterAlias("power")]
    	public DecimalParameter Intensity;

    	[Token(Token = "0x170004F0")]
    	protected override string Path
    	{
    		[Token(Token = "0x60021BA")]
    		[Address(RVA = "0x50C6F0", Offset = "0x50B0F0", VA = "0x18050C6F0", Slot = "18")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60021BB")]
    	[Address(RVA = "0x50C730", Offset = "0x50B130", VA = "0x18050C730", Slot = "23")]
    	protected override StringListParameter GetSpawnParameters()
    	{
    		return null;
    	}

    	[Token(Token = "0x60021BC")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public SpawnGlitch()
    	{
    	}
    }
}
