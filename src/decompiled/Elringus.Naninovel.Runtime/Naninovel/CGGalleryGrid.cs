using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;

namespace Naninovel;

[Token(Token = "0x20004B8")]
public class CGGalleryGrid : ScriptableGrid<CGGalleryGridSlot>
{
	[StructLayout(3)]
	[Token(Token = "0x20004B9")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__5
{
		[Token(Token = "0x40011EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40011EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40011ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CGGalleryGrid _003C_003E4__this;

		[Token(Token = "0x40011EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CGViewerPanel viewerPanel;

		[Token(Token = "0x40011EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public List<CGSlotData> slotData;

		[Token(Token = "0x40011F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001797")]
		[Address(RVA = "0x5F6F60", Offset = "0x5F5960", VA = "0x1805F6F60", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001798")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40011EA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
	private CGViewerPanel viewerPanel;

	[Token(Token = "0x17000361")]
	protected virtual List<CGSlotData> SlotData
	{
		[Token(Token = "0x6001790")]
		[Address(RVA = "0x5F6B60", Offset = "0x5F5560", VA = "0x1805F6B60", Slot = "76")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6001791")]
		[Address(RVA = "0x5F6B70", Offset = "0x5F5570", VA = "0x1805F6B70")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x6001792")]
	[Address(RVA = "0x5F6BD0", Offset = "0x5F55D0", VA = "0x1805F6BD0")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__5))]
	public UniTask Initialize(CGViewerPanel viewerPanel, List<CGSlotData> slotData)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001793")]
	[Address(RVA = "0x5F6DB0", Offset = "0x5F57B0", VA = "0x1805F6DB0")]
	protected new UniTask Initialize(int itemsCount)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001794")]
	[Address(RVA = "0x5F6E00", Offset = "0x5F5800", VA = "0x1805F6E00", Slot = "73")]
	protected override void InitializeSlot(CGGalleryGridSlot slot)
	{
	}

	[Token(Token = "0x6001795")]
	[Address(RVA = "0x5F6EA0", Offset = "0x5F58A0", VA = "0x1805F6EA0", Slot = "74")]
	protected override void BindSlot(CGGalleryGridSlot slot, int itemIndex)
	{
	}

	[Token(Token = "0x6001796")]
	[Address(RVA = "0x351480", Offset = "0x34FE80", VA = "0x180351480")]
	public CGGalleryGrid()
	{
	}
}
