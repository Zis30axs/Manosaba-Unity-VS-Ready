using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.AddressablesUtils;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.AddressableAssets;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x20000A2")]
public sealed class CreditsDirectorAct2 : CreditsDirectorBase
{
	[Token(Token = "0x20000A4")]
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass17_0
	{
		[Token(Token = "0x40002D9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public CancellationToken ct;

		[Token(Token = "0x6000259")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public _003C_003Ec__DisplayClass17_0()
		{
		}

		[Token(Token = "0x600025A")]
		[Address(RVA = "0x344040", Offset = "0x342A40", VA = "0x180344040")]
		internal UniTask _003CPlayAsync_003Eb__0(CreditRoll roll)
		{
			return default(UniTask);
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000A5")]
	[CompilerGenerated]
	private struct _003CPlayAsync_003Ed__17
{
		[Token(Token = "0x40002DA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002DB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40002DC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CancellationToken ct;

		[Token(Token = "0x40002DD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CreditsDirectorAct2 _003C_003E4__this;

		[Token(Token = "0x40002DE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private _003C_003Ec__DisplayClass17_0 _003C_003E8__1;

		[Token(Token = "0x40002DF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private IList<UniTask> _003CrebuildLayoutTasks_003E5__2;

		[Token(Token = "0x40002E0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private int _003Ci_003E5__3;

		[Token(Token = "0x40002E1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600025B")]
		[Address(RVA = "0x344120", Offset = "0x342B20", VA = "0x180344120", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600025C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000A6")]
	[CompilerGenerated]
	private struct _003CPreloadResourcesAsync_003Ed__16
{
		[Token(Token = "0x40002E2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002E3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40002E4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CreditsDirectorAct2 _003C_003E4__this;

		[Token(Token = "0x40002E5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x40002E6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<SpecialThanksData>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600025D")]
		[Address(RVA = "0x345220", Offset = "0x343C20", VA = "0x180345220", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600025E")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000A7")]
	[CompilerGenerated]
	private struct _003CShowStillsAsync_003Ed__20
{
		[Token(Token = "0x40002E7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002E8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40002E9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CreditsDirectorAct2 _003C_003E4__this;

		[Token(Token = "0x40002EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x40002EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40002EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private EndingStill[] _003C_003E7__wrap1;

		[Token(Token = "0x40002ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private int _003C_003E7__wrap2;

		[Token(Token = "0x40002EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private EndingStill _003Cstill_003E5__4;

		[Token(Token = "0x600025F")]
		[Address(RVA = "0x346530", Offset = "0x344F30", VA = "0x180346530", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000260")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40002BE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[Space]
	[SerializeField]
	private AssetReferenceT<SpecialThanksData> _specialThanksDataAsset;

	[Token(Token = "0x40002BF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[Space]
	[SerializeField]
	private RectTransform _rectTransform;

	[Token(Token = "0x40002C0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[SerializeField]
	private EndingStill[] _stills;

	[Token(Token = "0x40002C1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	[SerializeField]
	private CreditRoll[] _creditRolls;

	[Token(Token = "0x40002C2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	[Space]
	[SerializeField]
	private float _scrollSpeed;

	[Token(Token = "0x40002C3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x74")]
	[SerializeField]
	private float _stillDelayUnits;

	[Token(Token = "0x40002C4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	[SerializeField]
	private float _stillFadeUnits;

	[Token(Token = "0x40002C5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x7C")]
	[SerializeField]
	private float _stillDisplayUnits;

	[Token(Token = "0x40002C6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	private IAddressableAssetLoader _addressableAssetLoader;

	[Token(Token = "0x40002C7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private TextSizeByCourse[][] _specialThanksCourseData;

	[Token(Token = "0x40002C8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	private Dictionary<SpecialThanksCourse, Dictionary<LocaleKind, string>[]> _specialThanksCreditNames;

	[Token(Token = "0x40002C9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	private int _specialThanksPhase;

	[Token(Token = "0x17000045")]
	private IAddressableAssetLoader AddressableAssetLoader
	{
		[Token(Token = "0x6000240")]
		[Address(RVA = "0x342E80", Offset = "0x341880", VA = "0x180342E80")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000241")]
	[Address(RVA = "0x342F50", Offset = "0x341950", VA = "0x180342F50", Slot = "4")]
	protected override void Reset()
	{
	}

	[Token(Token = "0x6000242")]
	[Address(RVA = "0x343090", Offset = "0x341A90", VA = "0x180343090", Slot = "5")]
	public override void Initialize()
	{
	}

	[Token(Token = "0x6000243")]
	[Address(RVA = "0x343130", Offset = "0x341B30", VA = "0x180343130", Slot = "6")]
	[AsyncStateMachine(typeof(_003CPreloadResourcesAsync_003Ed__16))]
	public override UniTask PreloadResourcesAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000244")]
	[Address(RVA = "0x3432E0", Offset = "0x341CE0", VA = "0x1803432E0", Slot = "7")]
	[AsyncStateMachine(typeof(_003CPlayAsync_003Ed__17))]
	public override UniTask PlayAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000245")]
	[Address(RVA = "0x343490", Offset = "0x341E90", VA = "0x180343490", Slot = "8")]
	public override void Stop()
	{
	}

	[Token(Token = "0x6000246")]
	[Address(RVA = "0x3435D0", Offset = "0x341FD0", VA = "0x1803435D0")]
	private void PrepareSpecialThanksRoll(SpecialThanksRoll roll)
	{
	}

	[Token(Token = "0x6000247")]
	[Address(RVA = "0x3436F0", Offset = "0x3420F0", VA = "0x1803436F0")]
	[AsyncStateMachine(typeof(_003CShowStillsAsync_003Ed__20))]
	private UniTask ShowStillsAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000248")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public CreditsDirectorAct2()
	{
	}
}
