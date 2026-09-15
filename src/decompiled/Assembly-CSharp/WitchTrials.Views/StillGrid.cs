using System.Collections.Generic;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using System.Linq;
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
		// RECONSTRUCTED | Confidence: A | RVA: 0x3512E0, 410 B; lambda <get_SlotData>b__3_0 0x3514F0, 207 B
		// 
		// - `_sortedSlotData` (+0xE0) is built once from the base backing field (+0xC8, i.e. `base.SlotData`)
		//   with `OrderBy(lambda).ToList()`.
		// - The lambda (an instance method) returns `_order.IndexOf(data.Id)` (Naninovel `LinqUtils.IndexOf` over
		//   IReadOnlyList<string>). When the id is not listed (-1) it returns `base.SlotData.Count` (List._size,
		//   +0x18) plus the item's original index, so unlisted stills keep their order after the listed ones.
		// - Confidence B for the spelling of the lambda's base access: the lambda reads +0xC8 non-virtually, but
		//   `base.SlotData` inside a lambda makes Roslyn emit a `<>n__0` base-call helper, and dump.cs lists no such
		//   method. The behaviour (base list, not the sorted override) is byte-confirmed; the source form is not.
		[Token(Token = "0x60002C7")]
		[Address(RVA = "0x3512E0", Offset = "0x34FCE0", VA = "0x1803512E0", Slot = "76")]
		get
		{
			return _sortedSlotData ??= base.SlotData.OrderBy(delegate(CGSlotData data)
			{
				int num = _order.IndexOf(data.Id);
				return (num == -1) ? (base.SlotData.Count + base.SlotData.IndexOf(data)) : num;
			}).ToList();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x351480 (identical-code-folded with CGGalleryGrid..ctor)
	// The body only runs the base initializer (itemsPerPage +0xA0 = 9); StillGrid adds no field initializers.
	[Token(Token = "0x60002C8")]
	[Address(RVA = "0x351480", Offset = "0x34FE80", VA = "0x180351480")]
	public StillGrid()
	{
	}
}
