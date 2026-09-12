using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x200040F")]
public interface IScriptParser
{
	[Token(Token = "0x60013B6")]
	Script ParseText(string scriptPath, string scriptText, [Optional] ParseOptions options);
}
