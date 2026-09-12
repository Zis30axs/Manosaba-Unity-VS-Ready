using System.Threading;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000128")]
public class CancelOnDestroy : MonoBehaviour
{
	[Token(Token = "0x400049B")]
	[FieldOffset(Offset = "0x20")]
	private readonly CancellationTokenSource cts;

	[Token(Token = "0x170000EF")]
	public CancellationToken Token
	{
		[Token(Token = "0x6000590")]
		[Address(RVA = "0x511230", Offset = "0x50FC30", VA = "0x180511230")]
		get
		{
			return default(CancellationToken);
		}
	}

	[Token(Token = "0x6000591")]
	[Address(RVA = "0x511250", Offset = "0x50FC50", VA = "0x180511250")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000592")]
	[Address(RVA = "0x5112D0", Offset = "0x50FCD0", VA = "0x1805112D0")]
	public CancelOnDestroy()
	{
	}
}
