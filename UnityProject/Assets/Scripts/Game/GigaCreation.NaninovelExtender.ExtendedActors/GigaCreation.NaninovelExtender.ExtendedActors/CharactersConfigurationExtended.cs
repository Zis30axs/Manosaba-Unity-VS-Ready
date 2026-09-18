using System;
using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x2000016")]
    [EditInProjectSettings]
    public class CharactersConfigurationExtended : Configuration
    {
    	[Serializable]
    	[Token(Token = "0x2000017")]
    	public class Option
    	{
    		[Token(Token = "0x4000053")]
    		[FieldOffset(Offset = "0x10")]
    		[ActorPopup("Characters")]
    		public string Character;

    		[Token(Token = "0x4000054")]
    		[FieldOffset(Offset = "0x18")]
    		public float Scale;

    		[Token(Token = "0x600003E")]
    		[Address(RVA = "0xD18810", Offset = "0xD17210", VA = "0x180D18810")]
    		public Option()
    		{
    		}
    	}

    	[Token(Token = "0x4000052")]
    	[FieldOffset(Offset = "0x18")]
    	public Option[] Options;

    	[Token(Token = "0x600003D")]
    	[Address(RVA = "0x398D20", Offset = "0x397720", VA = "0x180398D20")]
    	public CharactersConfigurationExtended()
    	{
    	}
    }
}
