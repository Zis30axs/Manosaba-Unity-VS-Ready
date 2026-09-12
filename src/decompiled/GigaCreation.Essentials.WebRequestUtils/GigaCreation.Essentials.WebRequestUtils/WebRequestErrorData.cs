using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.WebRequestUtils;

[Token(Token = "0x2000005")]
public class WebRequestErrorData
{
	[Token(Token = "0x4000006")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	public string Uri;

	[Token(Token = "0x4000007")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	public WebRequestErrorType ErrorType;

	[Token(Token = "0x4000008")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	public CancellationToken Token;

	[Token(Token = "0x4000009")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	public UniTask Task;

	[Token(Token = "0x6000006")]
	[Address(RVA = "0xCF19D0", Offset = "0xCF03D0", VA = "0x180CF19D0")]
	public void SetData(string uri, WebRequestErrorType errorType, [Optional] CancellationToken ct)
	{
	}

	[Token(Token = "0x6000007")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public WebRequestErrorData()
	{
	}
}
