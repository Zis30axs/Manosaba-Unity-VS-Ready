using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.UniTasks;

[Token(Token = "0x2000004")]
public class AwaitableRequestData
{
	[Token(Token = "0x4000006")]
	[FieldOffset(Offset = "0x10")]
	public CancellationToken Token;

	[Token(Token = "0x4000007")]
	[FieldOffset(Offset = "0x18")]
	public UniTask Task;

	[Token(Token = "0x6000003")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public AwaitableRequestData()
	{
	}
}
