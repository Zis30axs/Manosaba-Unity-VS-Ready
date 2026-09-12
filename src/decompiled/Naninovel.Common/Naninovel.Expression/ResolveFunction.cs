using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.Expression;

[Token(Token = "0x2000094")]
public delegate IOperand ResolveFunction(string name, IReadOnlyList<IOperand> parameters);
