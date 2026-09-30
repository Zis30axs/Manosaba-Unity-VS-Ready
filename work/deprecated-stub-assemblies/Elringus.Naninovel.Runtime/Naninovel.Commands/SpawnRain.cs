using Il2CppDummyDll;

namespace Naninovel.Commands
{
    [Token(Token = "0x2000719")]
    [CommandAlias("rain")]
    public class SpawnRain : SpawnLocalizedEffect
    {
    	[Token(Token = "0x4001B15")]
    	[FieldOffset(Offset = "0x60")]
    	[ParameterAlias("power")]
    	public DecimalParameter Intensity;

    	[Token(Token = "0x4001B16")]
    	[FieldOffset(Offset = "0x68")]
    	[ParameterAlias("time")]
    	public DecimalParameter FadeDuration;

    	[Token(Token = "0x4001B17")]
    	[FieldOffset(Offset = "0x70")]
    	[ParameterAlias("xSpeed")]
    	public DecimalParameter XVelocity;

    	[Token(Token = "0x4001B18")]
    	[FieldOffset(Offset = "0x78")]
    	[ParameterAlias("ySpeed")]
    	public DecimalParameter YVelocity;

    	[Token(Token = "0x170004F1")]
    	protected override string Path
    	{
    		[Token(Token = "0x60021C2")]
    		[Address(RVA = "0x50C820", Offset = "0x50B220", VA = "0x18050C820", Slot = "18")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170004F2")]
    	protected override bool DestroyWhen
    	{
    		[Token(Token = "0x60021C3")]
    		[Address(RVA = "0x50C860", Offset = "0x50B260", VA = "0x18050C860", Slot = "19")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x60021C4")]
    	[Address(RVA = "0x50C910", Offset = "0x50B310", VA = "0x18050C910", Slot = "23")]
    	protected override StringListParameter GetSpawnParameters()
    	{
    		return null;
    	}

    	[Token(Token = "0x60021C5")]
    	[Address(RVA = "0x50CA60", Offset = "0x50B460", VA = "0x18050CA60", Slot = "24")]
    	protected override StringListParameter GetDestroyParameters()
    	{
    		return null;
    	}

    	[Token(Token = "0x60021C6")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public SpawnRain()
    	{
    	}
    }
}
