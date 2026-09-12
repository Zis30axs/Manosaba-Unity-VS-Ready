using System;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UniRx;

namespace GigaCreation.Tools.Debugging.Core;

[Token(Token = "0x2000007")]
public interface IDebugManager : IService, IDisposable
{
	[Token(Token = "0x17000002")]
	IReactiveProperty<bool> IsDebugMode
	{
		[Token(Token = "0x600000D")]
		get;
	}
}
