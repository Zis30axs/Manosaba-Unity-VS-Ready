using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Common
{
    [Token(Token = "0x2000004")]
    [UsedImplicitly]
    [CommandAlias("auto")]
    public class ChangeAutoPlayMode : Command
    {
    	[Token(Token = "0x4000006")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly]
    	[ParameterAlias("")]
    	[ParameterDefaultValue("true")]
    	public BooleanParameter Enable;

    	[Token(Token = "0x6000003")]
    	[Address(RVA = "0xCF79D0", Offset = "0xCF63D0", VA = "0x180CF79D0", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0x31D800", Offset = "0x31C200", VA = "0x18031D800")]
    	public ChangeAutoPlayMode()
    	{
    	}
    }
}
