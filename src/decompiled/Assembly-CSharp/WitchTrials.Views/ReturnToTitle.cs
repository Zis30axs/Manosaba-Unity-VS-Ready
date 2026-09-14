using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.Overlays.Models;
using GigaCreation.NaninovelExtender.Common;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x200003F")]
public class ReturnToTitle : Stop
{
	[StructLayout(3)]
	[Token(Token = "0x2000040")]
	[CompilerGenerated]
	private struct _003CReturnToTitleAsync_003Ed__13
{
		[Token(Token = "0x40000B5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000B6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000B7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ReturnToTitle _003C_003E4__this;

		[Token(Token = "0x40000B8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40000B9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private NaninovelInteractionBlocker _003C_003E7__wrap1;

		[Token(Token = "0x40000BA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40000BB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60000D6")]
		[Address(RVA = "0x31BC80", Offset = "0x31A680", VA = "0x18031BC80", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000D7")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40000AF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	[ParameterAlias("time")]
	[ParameterDefaultValue("1.2")]
	public DecimalParameter FadeDuration;

	[Token(Token = "0x40000B0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly]
	[ParameterAlias("delay")]
	[ParameterDefaultValue("0.6")]
	public DecimalParameter MinDelayDuration;

	[Token(Token = "0x40000B1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly]
	public BooleanParameter Wait;

	[Token(Token = "0x40000B2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private IOverlayFader _overlayFader;

	[Token(Token = "0x40000B3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private IWitchTrialsNaninovelManager _witchTrialsNaninovelManager;

	[Token(Token = "0x40000B4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private IPerspectiveStageManager _perspectiveStageManager;

	[Token(Token = "0x1700000B")]
	private IOverlayFader OverlayFader
	{
		[Token(Token = "0x60000D0")]
		[Address(RVA = "0x31B6F0", Offset = "0x31A0F0", VA = "0x18031B6F0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700000C")]
	private IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x31B7B0, 186 B
		// Lazy cache in +0x50 via `ServiceLocator.Get<IWitchTrialsNaninovelManager>()`
		// with the default `includeDerivedType` (`xor ecx, ecx`).
		[Token(Token = "0x60000D1")]
		[Address(RVA = "0x31B7B0", Offset = "0x31A1B0", VA = "0x18031B7B0")]
		get
		{
			return _witchTrialsNaninovelManager ??= GigaCreation.Tools.Service.ServiceLocator.Get<IWitchTrialsNaninovelManager>();
		}
	}

	[Token(Token = "0x1700000D")]
	private IPerspectiveStageManager PerspectiveStageManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x31B870, 186 B
		// Lazy cache in +0x58 via `ServiceLocator.Get<IPerspectiveStageManager>()`
		// with the default `includeDerivedType` (`xor ecx, ecx`).
		[Token(Token = "0x60000D2")]
		[Address(RVA = "0x31B870", Offset = "0x31A270", VA = "0x18031B870")]
		get
		{
			return _perspectiveStageManager ??= GigaCreation.Tools.Service.ServiceLocator.Get<IPerspectiveStageManager>();
		}
	}

	[Token(Token = "0x60000D3")]
	[Address(RVA = "0x31B930", Offset = "0x31A330", VA = "0x18031B930", Slot = "5")]
	public override Naninovel.UniTask Execute([Optional] AsyncToken token)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x60000D4")]
	[Address(RVA = "0x31BA00", Offset = "0x31A400", VA = "0x18031BA00")]
	[AsyncStateMachine(typeof(_003CReturnToTitleAsync_003Ed__13))]
	private Naninovel.UniTask ReturnToTitleAsync(AsyncToken token)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x60000D5")]
	[Address(RVA = "0x31BB90", Offset = "0x31A590", VA = "0x18031BB90")]
	public ReturnToTitle()
	{
	}
}
