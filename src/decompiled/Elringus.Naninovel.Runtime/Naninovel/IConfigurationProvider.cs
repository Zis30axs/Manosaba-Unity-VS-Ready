using System;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x200030F")]
public interface IConfigurationProvider
{
	[Token(Token = "0x6000E97")]
	Configuration GetConfiguration(Type type);
}
