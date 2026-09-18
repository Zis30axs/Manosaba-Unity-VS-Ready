using System.Collections.Generic;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Commands;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x2000031")]
    [UsedImplicitly]
    [CommandAlias("animate")]
    public class AnimateActorExtended : AnimateActor
    {
    	[Token(Token = "0x2000032")]
    	public enum AdditionalParam
    	{
    		[Token(Token = "0x40000A8")]
    		MatchScreenWidth,
    		[Token(Token = "0x40000A9")]
    		ScenePositionX,
    		[Token(Token = "0x40000AA")]
    		ScenePositionY,
    		[Token(Token = "0x40000AB")]
    		EasingTypeNameForPosX,
    		[Token(Token = "0x40000AC")]
    		EasingTypeNameForPosY
    	}

    	[Token(Token = "0x40000A2")]
    	[FieldOffset(Offset = "0xA0")]
    	[UsedImplicitly]
    	[ParameterAlias("easingPosX")]
    	public StringParameter EasingTypeNameForPosX;

    	[Token(Token = "0x40000A3")]
    	[FieldOffset(Offset = "0xA8")]
    	[UsedImplicitly]
    	[ParameterAlias("easingPosY")]
    	public StringParameter EasingTypeNameForPosY;

    	[Token(Token = "0x40000A4")]
    	[FieldOffset(Offset = "0xB0")]
    	[UsedImplicitly]
    	public BooleanParameter Relative;

    	[Token(Token = "0x40000A5")]
    	[FieldOffset(Offset = "0xB8")]
    	[UsedImplicitly]
    	[ParameterAlias("multi")]
    	public DecimalParameter Multiplier;

    	[Token(Token = "0x40000A6")]
    	[FieldOffset(Offset = "0xC0")]
    	[UsedImplicitly]
    	[ParameterAlias("match")]
    	public BooleanParameter MatchScreenWidth;

    	[Token(Token = "0x60000A9")]
    	[Address(RVA = "0xD1E940", Offset = "0xD1D340", VA = "0x180D1E940", Slot = "17")]
    	protected override IReadOnlyList<string> GetParametersForActor(string actorId)
    	{
    		return null;
    	}

    	[Token(Token = "0x60000AA")]
    	[Address(RVA = "0xD1F3C0", Offset = "0xD1DDC0", VA = "0x180D1F3C0")]
    	private string ModifyKeysAsRelative(StringParameter parameter, float multiplier, float actorScreenPosition)
    	{
    		return null;
    	}

    	[Token(Token = "0x60000AB")]
    	[Address(RVA = "0xD1F760", Offset = "0xD1E160", VA = "0x180D1F760")]
    	public AnimateActorExtended()
    	{
    	}
    }
}
