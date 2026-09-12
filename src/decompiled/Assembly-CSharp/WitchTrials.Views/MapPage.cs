using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x20000FC")]
public class MapPage : WitchBookPageBase<MapDataItem, MapState>
{
	[Token(Token = "0x20000FF")]
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass9_0
	{
		[Token(Token = "0x40004BC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public MapPage _003C_003E4__this;

		[Token(Token = "0x40004BD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CancellationTokenSource linkedCts;

		[Token(Token = "0x600040E")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public _003C_003Ec__DisplayClass9_0()
		{
		}

		[Token(Token = "0x600040F")]
		[Address(RVA = "0x370770", Offset = "0x36F170", VA = "0x180370770")]
		internal bool _003COnChoiceMapAreaAsync_003Eb__0(MapArea area)
		{
			return default(bool);
		}

		[Token(Token = "0x6000410")]
		[Address(RVA = "0x3707E0", Offset = "0x36F1E0", VA = "0x1803707E0")]
		internal UniTask<MapAreaKind> _003COnChoiceMapAreaAsync_003Eb__1(MapArea area)
		{
			return default(UniTask<MapAreaKind>);
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000100")]
	[CompilerGenerated]
	private struct _003CLoadDataAsync_003Ed__5
{
		[Token(Token = "0x40004BE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40004BF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<IList<VersionedItem<MapDataItem>>> _003C_003Et__builder;

		[Token(Token = "0x40004C0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public MapPage _003C_003E4__this;

		[Token(Token = "0x40004C1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x40004C2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<IList<VersionedItem<MapDataItem>>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000411")]
		[Address(RVA = "0x370A00", Offset = "0x36F400", VA = "0x180370A00", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000412")]
		[Address(RVA = "0x371040", Offset = "0x36FA40", VA = "0x180371040", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000101")]
	[CompilerGenerated]
	private struct _003COnChoiceMapAreaAsync_003Ed__9
{
		[Token(Token = "0x40004C3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40004C4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<MapAreaKind> _003C_003Et__builder;

		[Token(Token = "0x40004C5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public MapPage _003C_003E4__this;

		[Token(Token = "0x40004C6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x40004C7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private _003C_003Ec__DisplayClass9_0 _003C_003E8__1;

		[Token(Token = "0x40004C8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<(int winArgumentIndex, MapAreaKind result)>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000413")]
		[Address(RVA = "0x371070", Offset = "0x36FA70", VA = "0x180371070", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000414")]
		[Address(RVA = "0x371A50", Offset = "0x370450", VA = "0x180371A50", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40004B1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
	[SerializeField]
	private MapFloor[] _floors;

	[Token(Token = "0x40004B2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
	private IReadOnlyDictionary<IdVersionPair, IReadOnlyDictionary<LocaleKind, string>> _localizedTextData;

	[Token(Token = "0x40004B3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
	private IReadOnlyCollection<MapAreaKind> _restrictionAreas;

	[Token(Token = "0x40004B4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
	private MapArea[] _displayableAreas;

	[Token(Token = "0x60003FD")]
	[Address(RVA = "0x36F5B0", Offset = "0x36DFB0", VA = "0x18036F5B0", Slot = "5")]
	protected override void Reset()
	{
	}

	[Token(Token = "0x60003FE")]
	[Address(RVA = "0x36F690", Offset = "0x36E090", VA = "0x18036F690", Slot = "16")]
	[AsyncStateMachine(typeof(_003CLoadDataAsync_003Ed__5))]
	protected override UniTask<IList<VersionedItem<MapDataItem>>> LoadDataAsync([Optional] CancellationToken ct)
	{
		return default(UniTask<IList<VersionedItem<MapDataItem>>>);
	}

	[Token(Token = "0x60003FF")]
	[Address(RVA = "0x36F890", Offset = "0x36E290", VA = "0x18036F890", Slot = "17")]
	protected override void SetupItemButton(WitchBookItemButton button, VersionedItem<MapDataItem> map)
	{
	}

	[Token(Token = "0x6000400")]
	[Address(RVA = "0x36F9D0", Offset = "0x36E3D0", VA = "0x18036F9D0", Slot = "18")]
	protected override void RefreshPageContent(VersionedItem<MapDataItem> map)
	{
	}

	[Token(Token = "0x6000401")]
	[Address(RVA = "0x36FB30", Offset = "0x36E530", VA = "0x18036FB30")]
	public void InitializeAreas(IReadOnlyCollection<MapAreaKind> restrictions)
	{
	}

	[Token(Token = "0x6000402")]
	[Address(RVA = "0x36FF80", Offset = "0x36E980", VA = "0x18036FF80")]
	[AsyncStateMachine(typeof(_003COnChoiceMapAreaAsync_003Ed__9))]
	public UniTask<MapAreaKind> OnChoiceMapAreaAsync([Optional] CancellationToken ct)
	{
		return default(UniTask<MapAreaKind>);
	}

	[Token(Token = "0x6000403")]
	[Address(RVA = "0x370170", Offset = "0x36EB70", VA = "0x180370170")]
	public MapPage()
	{
	}
}
