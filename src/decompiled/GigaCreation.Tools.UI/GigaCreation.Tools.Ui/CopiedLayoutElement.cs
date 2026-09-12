using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GigaCreation.Tools.Ui;

[Token(Token = "0x200000C")]
[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class CopiedLayoutElement : UIBehaviour, ILayoutElement
{
	[Token(Token = "0x4000020")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private bool _shouldCopyMinWidth;

	[Token(Token = "0x4000021")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private RectTransform _copySourceOfMinWidth;

	[Token(Token = "0x4000022")]
	[FieldOffset(Offset = "0x30")]
	[Space]
	[SerializeField]
	private bool _shouldCopyMinHeight;

	[Token(Token = "0x4000023")]
	[FieldOffset(Offset = "0x38")]
	[SerializeField]
	private RectTransform _copySourceOfMinHeight;

	[Token(Token = "0x4000024")]
	[FieldOffset(Offset = "0x40")]
	[Space]
	[SerializeField]
	private bool _shouldCopyPreferredWidth;

	[Token(Token = "0x4000025")]
	[FieldOffset(Offset = "0x48")]
	[SerializeField]
	private RectTransform _copySourceOfPreferredWidth;

	[Token(Token = "0x4000026")]
	[FieldOffset(Offset = "0x50")]
	[Space]
	[SerializeField]
	private bool _shouldCopyPreferredHeight;

	[Token(Token = "0x4000027")]
	[FieldOffset(Offset = "0x58")]
	[SerializeField]
	private RectTransform _copySourceOfPreferredHeight;

	[Token(Token = "0x4000028")]
	[FieldOffset(Offset = "0x60")]
	[Space]
	[SerializeField]
	private Vector2 _padding;

	[Token(Token = "0x17000001")]
	public float minWidth
	{
		[Token(Token = "0x6000034")]
		[Address(RVA = "0xD567E0", Offset = "0xD551E0", VA = "0x180D567E0", Slot = "19")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x17000002")]
	public float minHeight
	{
		[Token(Token = "0x6000035")]
		[Address(RVA = "0xD56AA0", Offset = "0xD554A0", VA = "0x180D56AA0", Slot = "22")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x17000003")]
	public float preferredWidth
	{
		[Token(Token = "0x6000036")]
		[Address(RVA = "0xD56D60", Offset = "0xD55760", VA = "0x180D56D60", Slot = "20")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x17000004")]
	public float preferredHeight
	{
		[Token(Token = "0x6000037")]
		[Address(RVA = "0xD57020", Offset = "0xD55A20", VA = "0x180D57020", Slot = "23")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x17000005")]
	public int layoutPriority
	{
		[Token(Token = "0x6000038")]
		[Address(RVA = "0x89A3C0", Offset = "0x898DC0", VA = "0x18089A3C0", Slot = "25")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x17000006")]
	public float flexibleHeight
	{
		[Token(Token = "0x6000039")]
		[Address(RVA = "0xD572E0", Offset = "0xD55CE0", VA = "0x180D572E0", Slot = "24")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x17000007")]
	public float flexibleWidth
	{
		[Token(Token = "0x600003A")]
		[Address(RVA = "0xD572E0", Offset = "0xD55CE0", VA = "0x180D572E0", Slot = "21")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x600003B")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "17")]
	public void CalculateLayoutInputHorizontal()
	{
	}

	[Token(Token = "0x600003C")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "18")]
	public void CalculateLayoutInputVertical()
	{
	}

	[Token(Token = "0x600003D")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public CopiedLayoutElement()
	{
	}
}
