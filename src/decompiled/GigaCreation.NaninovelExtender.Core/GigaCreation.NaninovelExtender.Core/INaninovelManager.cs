using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;

namespace GigaCreation.NaninovelExtender.Core;

[Token(Token = "0x2000004")]
public interface INaninovelManager : IService, INaninovelStateSaver
{
	[Token(Token = "0x17000001")]
	string StartGameScenarioName
	{
		[Token(Token = "0x6000003")]
		get;
	}

	[Token(Token = "0x6000004")]
	UniTask InitializeAsync([Optional] CancellationToken ct);
}
