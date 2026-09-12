using System.Collections.Generic;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x20000C4")]
public class StillGrid : CGGalleryGrid
{
	[Token(Token = "0x4000370")]
	[FieldOffset(Offset = "0xD8")]
	[SerializeField]
	private string[] _order;

	[Token(Token = "0x4000371")]
	[FieldOffset(Offset = "0xE0")]
	private List<CGSlotData> _sortedSlotData;

	[Token(Token = "0x17000054")]
	protected override List<CGSlotData> SlotData
	{
		[Token(Token = "0x60002C7")]
		[Address(RVA = "0x3512E0", Offset = "0x34FCE0", VA = "0x1803512E0", Slot = "76")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60002C8")]
	[Address(RVA = "0x351480", Offset = "0x34FE80", VA = "0x180351480")]
	public StillGrid()
	{
	}
}
