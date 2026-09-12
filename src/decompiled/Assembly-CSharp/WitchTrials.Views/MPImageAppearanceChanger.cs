using Il2CppDummyDll;
using MPUIKIT;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x200000F")]
public class MPImageAppearanceChanger : MonoBehaviour
{
	[Token(Token = "0x400001A")]
	[FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private MPImage _mpImage;

	[Token(Token = "0x400001B")]
	[FieldOffset(Offset = "0x28")]
	[Header("Parameters")]
	[SerializeField]
	private float[] _strokes;

	[Token(Token = "0x400001C")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Color[] _colors;

	[Token(Token = "0x600002C")]
	[Address(RVA = "0x30D940", Offset = "0x30C340", VA = "0x18030D940", Slot = "4")]
	protected virtual void Reset()
	{
	}

	[Token(Token = "0x600002D")]
	[Address(RVA = "0x30D9E0", Offset = "0x30C3E0", VA = "0x18030D9E0", Slot = "5")]
	public virtual void ChangeAppearance(int appearanceIndex)
	{
	}

	[Token(Token = "0x600002E")]
	[Address(RVA = "0x30DB70", Offset = "0x30C570", VA = "0x18030DB70", Slot = "6")]
	public virtual void ChangeAppearance(bool zeroOrOne)
	{
	}

	[Token(Token = "0x600002F")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public MPImageAppearanceChanger()
	{
	}
}
