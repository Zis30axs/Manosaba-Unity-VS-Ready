using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x200002E")]
public class LayerModifier : MonoBehaviour
{
	[Token(Token = "0x400009D")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private Renderer[] _layers;

	[Token(Token = "0x6000097")]
	[Address(RVA = "0xD1DAF0", Offset = "0xD1C4F0", VA = "0x180D1DAF0")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000098")]
	[Address(RVA = "0xD1DF90", Offset = "0xD1C990", VA = "0x180D1DF90")]
	public void ModifyLayers(int cameraCullingLayer)
	{
	}

	[Token(Token = "0x6000099")]
	[Address(RVA = "0xD1E140", Offset = "0xD1CB40", VA = "0x180D1E140")]
	public void SetRendererEnabled(bool enable)
	{
	}

	[Token(Token = "0x600009A")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public LayerModifier()
	{
	}
}
