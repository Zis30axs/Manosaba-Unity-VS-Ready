using System.Collections.Generic;
using System.Threading.Tasks;
using Il2CppDummyDll;

namespace Naninovel.Bridging
{
    [Token(Token = "0x20000C7")]
    public interface IFiles
    {
    	[Token(Token = "0x6000407")]
    	Task<string> Read(string name);

    	[Token(Token = "0x6000408")]
    	Task Write(string name, string content);

    	[Token(Token = "0x6000409")]
    	Task<IReadOnlyCollection<string>> List();

    	[Token(Token = "0x600040A")]
    	void Watch(HandleFileChange handler);
    }
}
