using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x2000718")]
public abstract class SpawnLocalizedEffect : SpawnEffect
{
	[Token(Token = "0x4001B11")]
	[FieldOffset(Offset = "0x40")]
	[ParameterAlias("pos")]
	[VectorContext("X,Y,Z", -1, null)]
	public DecimalListParameter ScenePosition;

	[Token(Token = "0x4001B12")]
	[FieldOffset(Offset = "0x48")]
	[VectorContext("X,Y,Z", -1, null)]
	public DecimalListParameter Position;

	[Token(Token = "0x4001B13")]
	[FieldOffset(Offset = "0x50")]
	[VectorContext("X,Y,Z", -1, null)]
	public DecimalListParameter Rotation;

	[Token(Token = "0x4001B14")]
	[FieldOffset(Offset = "0x58")]
	[VectorContext("X,Y,Z", -1, null)]
	public DecimalListParameter Scale;

	[Token(Token = "0x60021BD")]
	[Address(RVA = "0x44CE10", Offset = "0x44B810", VA = "0x18044CE10", Slot = "25")]
	protected override DecimalListParameter GetScenePosition()
	{
		return null;
	}

	[Token(Token = "0x60021BE")]
	[Address(RVA = "0x453470", Offset = "0x451E70", VA = "0x180453470", Slot = "26")]
	protected override DecimalListParameter GetPosition()
	{
		return null;
	}

	[Token(Token = "0x60021BF")]
	[Address(RVA = "0x453480", Offset = "0x451E80", VA = "0x180453480", Slot = "27")]
	protected override DecimalListParameter GetRotation()
	{
		return null;
	}

	[Token(Token = "0x60021C0")]
	[Address(RVA = "0x453950", Offset = "0x452350", VA = "0x180453950", Slot = "28")]
	protected override DecimalListParameter GetScale()
	{
		return null;
	}

	[Token(Token = "0x60021C1")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	protected SpawnLocalizedEffect()
	{
	}
}
