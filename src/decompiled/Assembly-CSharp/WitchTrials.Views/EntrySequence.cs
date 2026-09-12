using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Overlays.Models;
using GigaCreation.NaninovelExtender.Localization;
using Il2CppDummyDll;
using Naninovel;
using UniRx;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000142")]
public class EntrySequence : MonoBehaviour
{
	[Token(Token = "0x2000143")]
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass25_0
	{
		[Token(Token = "0x400063A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public EntrySequence _003C_003E4__this;

		[Token(Token = "0x400063B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CancellationToken ct;

		[Token(Token = "0x600056A")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public _003C_003Ec__DisplayClass25_0()
		{
		}

		[Token(Token = "0x600056B")]
		[Address(RVA = "0x38AA30", Offset = "0x389430", VA = "0x18038AA30")]
		internal void _003CPlayInitialSequenceAsync_003Eb__0(Unit _)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000144")]
	[CompilerGenerated]
	private struct _003CEnterTitleScreenAsync_003Ed__27
{
		[Token(Token = "0x400063C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400063D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400063E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public EntrySequence _003C_003E4__this;

		[Token(Token = "0x400063F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000640")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600056C")]
		[Address(RVA = "0x38AA90", Offset = "0x389490", VA = "0x18038AA90", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600056D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000145")]
	[CompilerGenerated]
	private struct _003CPlayInitialSequenceAsync_003Ed__25
{
		[Token(Token = "0x4000641")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000642")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000643")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public EntrySequence _003C_003E4__this;

		[Token(Token = "0x4000644")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000645")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private _003C_003Ec__DisplayClass25_0 _003C_003E8__1;

		[Token(Token = "0x4000646")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000647")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600056E")]
		[Address(RVA = "0x38B000", Offset = "0x389A00", VA = "0x18038B000", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600056F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400062D")]
	private const string AllowRollbackVarName = "allowRollback";

	[Token(Token = "0x400062E")]
	private const string AllowContinueDebateVarName = "allowContinueDebate";

	[Token(Token = "0x400062F")]
	private const string IsDebugDirectJumpVarName = "isDebugDirectJump";

	[Token(Token = "0x4000630")]
	private const string EnableDebugSkipVarName = "enableDebugSkip";

	[Token(Token = "0x4000631")]
	private const string IsDemoVarName = "isDemo";

	[Token(Token = "0x4000632")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("Parameters")]
	[SerializeField]
	private float _fadeInDuration;

	[Token(Token = "0x4000633")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
	[SerializeField]
	private Color _fadeColor;

	[Token(Token = "0x4000634")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private IOverlayFader _overlayFader;

	[Token(Token = "0x4000635")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private IWitchTrialsNaninovelManager _witchTrialsNaninovelManager;

	[Token(Token = "0x4000636")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private IScriptManager _naniScriptManager;

	[Token(Token = "0x4000637")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private IScriptPlayer _naniScriptPlayer;

	[Token(Token = "0x4000638")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private ICustomVariableManager _naniVarManager;

	[Token(Token = "0x4000639")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private ILocalizationManagerExtended _naniLocalizationManager;

	[Token(Token = "0x170000AD")]
	private IOverlayFader OverlayFader
	{
		[Token(Token = "0x6000560")]
		[Address(RVA = "0x38A030", Offset = "0x388A30", VA = "0x18038A030")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000AE")]
	private IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
	{
		[Token(Token = "0x6000561")]
		[Address(RVA = "0x38A0F0", Offset = "0x388AF0", VA = "0x18038A0F0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000AF")]
	private IScriptManager NaniScriptManager
	{
		[Token(Token = "0x6000562")]
		[Address(RVA = "0x38A1B0", Offset = "0x388BB0", VA = "0x18038A1B0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000B0")]
	private IScriptPlayer NaniScriptPlayer
	{
		[Token(Token = "0x6000563")]
		[Address(RVA = "0x38A270", Offset = "0x388C70", VA = "0x18038A270")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000B1")]
	private ICustomVariableManager NaniVarManager
	{
		[Token(Token = "0x6000564")]
		[Address(RVA = "0x38A330", Offset = "0x388D30", VA = "0x18038A330")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000B2")]
	private ILocalizationManagerExtended NaniLocalizationManager
	{
		[Token(Token = "0x6000565")]
		[Address(RVA = "0x38A3F0", Offset = "0x388DF0", VA = "0x18038A3F0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000566")]
	[Address(RVA = "0x38A4B0", Offset = "0x388EB0", VA = "0x18038A4B0")]
	[AsyncStateMachine(typeof(_003CPlayInitialSequenceAsync_003Ed__25))]
	public Cysharp.Threading.Tasks.UniTask PlayInitialSequenceAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x6000567")]
	[Address(RVA = "0x38A660", Offset = "0x389060", VA = "0x18038A660")]
	private void ResetDebugVariables()
	{
	}

	[Token(Token = "0x6000568")]
	[Address(RVA = "0x38A8A0", Offset = "0x3892A0", VA = "0x18038A8A0")]
	[AsyncStateMachine(typeof(_003CEnterTitleScreenAsync_003Ed__27))]
	private Cysharp.Threading.Tasks.UniTask EnterTitleScreenAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x6000569")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public EntrySequence()
	{
	}
}
