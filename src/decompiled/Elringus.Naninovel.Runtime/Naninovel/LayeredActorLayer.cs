using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000098")]
public class LayeredActorLayer : MonoBehaviour
{
	[Token(Token = "0x400022B")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private IntUnityEvent onLayerHeld;

	[Token(Token = "0x400022C")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private IntUnityEvent onLayerReleased;

	[Token(Token = "0x60002C0")]
	[Address(RVA = "0x461470", Offset = "0x45FE70", VA = "0x180461470")]
	public void NotifyLayerHeld(int cameraCullingLayer)
	{
	}

	[Token(Token = "0x60002C1")]
	[Address(RVA = "0x4614D0", Offset = "0x45FED0", VA = "0x1804614D0")]
	public void NotifyLayerReleased(int cameraCullingLayer)
	{
	}

	[Token(Token = "0x60002C2")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public LayeredActorLayer()
	{
	}
}
