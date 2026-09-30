using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006FA")]
    public class ProcessInput : Command
    {
    	[Token(Token = "0x4001ABE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	public BooleanParameter InputEnabled;

    	[Token(Token = "0x4001ABF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterAlias("set")]
    	public NamedBooleanListParameter SetEnabled;

    	[Token(Token = "0x600215A")]
    	[Address(RVA = "0x5044C0", Offset = "0x502EC0", VA = "0x1805044C0", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600215B")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public ProcessInput()
    	{
    	}
    }
}
