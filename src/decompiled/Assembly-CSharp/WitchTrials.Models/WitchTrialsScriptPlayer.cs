using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using GigaCreation.NaninovelExtender.Common;
using Il2CppDummyDll;
using YieldAwaitable = Naninovel.YieldAwaitable;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace WitchTrials.Models;

[Token(Token = "0x200017F")]
[UsedImplicitly]
[InitializeAtRuntime(0, typeof(ScriptPlayer))]
public class WitchTrialsScriptPlayer : ScriptPlayer, IWitchTrialsScriptPlayer, IScriptPlayer, IEngineService<ScriptPlayerConfiguration>, IEngineService
{
	[StructLayout(3)]
	[Token(Token = "0x2000180")]
	[CompilerGenerated]
	private struct _003CProcessAutoObjectionAsync_003Ed__9
{
		[Token(Token = "0x4000726")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000727")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000728")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public WitchTrialsScriptPlayer _003C_003E4__this;

		[Token(Token = "0x4000729")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string[] objectionIds;

		[Token(Token = "0x400072A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private AsyncToken _003CasyncToken_003E5__2;

		[Token(Token = "0x400072B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private string[] _003C_003E7__wrap2;

		[Token(Token = "0x400072C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private int _003C_003E7__wrap3;

		[Token(Token = "0x400072D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400072E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private YieldAwaitable.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600069A")]
		[Address(RVA = "0x39CAE0", Offset = "0x39B4E0", VA = "0x18039CAE0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600069B")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000722")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
	private readonly IStateManagerExtended _stateManagerExtended;

	[Token(Token = "0x4000723")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
	private IWitchTrialsNaninovelManager _witchTrialsNaninovelManager;

	[Token(Token = "0x4000724")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
	private bool _dontDisableAutoPlayAndSkip;

	[Token(Token = "0x4000725")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x118")]
	private CancellationTokenSource _cts;

	[Token(Token = "0x1700011A")]
	private IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
	{
		[Token(Token = "0x6000691")]
		[Address(RVA = "0x39C070", Offset = "0x39AA70", VA = "0x18039C070")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000692")]
	[Address(RVA = "0x39C140", Offset = "0x39AB40", VA = "0x18039C140")]
	public WitchTrialsScriptPlayer(ScriptPlayerConfiguration cfg, IInputManager input, IScriptManager scripts, IStateManagerExtended extended)
	{
	}

	[Token(Token = "0x6000693")]
	[Address(RVA = "0x39C1C0", Offset = "0x39ABC0", VA = "0x18039C1C0", Slot = "73")]
	public override void ResetService()
	{
	}

	[Token(Token = "0x6000694")]
	[Address(RVA = "0x39C470", Offset = "0x39AE70", VA = "0x18039C470", Slot = "74")]
	public override void DestroyService()
	{
	}

	[Token(Token = "0x6000695")]
	[Address(RVA = "0x39C560", Offset = "0x39AF60", VA = "0x18039C560", Slot = "113")]
	[AsyncStateMachine(typeof(_003CProcessAutoObjectionAsync_003Ed__9))]
	public UniTask ProcessAutoObjectionAsync(string[] objectionIds)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000696")]
	[Address(RVA = "0x39C700", Offset = "0x39B100", VA = "0x18039C700", Slot = "114")]
	public void CancelGosub()
	{
	}

	[Token(Token = "0x6000697")]
	[Address(RVA = "0x39C850", Offset = "0x39B250", VA = "0x18039C850", Slot = "97")]
	protected override void DisableSkip()
	{
	}

	[Token(Token = "0x6000698")]
	[Address(RVA = "0x39C870", Offset = "0x39B270", VA = "0x18039C870", Slot = "100")]
	protected override void DisableAutoPlay()
	{
	}

	[Token(Token = "0x6000699")]
	[Address(RVA = "0x39C890", Offset = "0x39B290", VA = "0x18039C890", Slot = "104")]
	protected override bool GetSkipAllowed()
	{
		return default(bool);
	}
}
