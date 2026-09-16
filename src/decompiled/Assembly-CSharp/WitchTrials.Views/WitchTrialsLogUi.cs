using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using GigaCreation.NaninovelExtender.Ui;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000097")]
public class WitchTrialsLogUi : BacklogPanelModified
{
	[StructLayout(3)]
	[Token(Token = "0x2000098")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__4
{
		[Token(Token = "0x4000289")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400028A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400028B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public WitchTrialsLogUi _003C_003E4__this;

		[Token(Token = "0x400028C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private Naninovel.UniTask _003Ctask_003E5__2;

		[Token(Token = "0x400028D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400028E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000216")]
		[Address(RVA = "0x33EB70", Offset = "0x33D570", VA = "0x18033EB70", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000217")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000287")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x158")]
	[SerializeField]
	private AuthorTextBuilder.Settings _authorTextSettings;

	[Token(Token = "0x4000288")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x160")]
	private AuthorTextBuilder _authorTextBuilder;

	[Token(Token = "0x1700003C")]
	private AuthorTextBuilder AuthorTextBuilder
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x33E630
		// Lazy `_authorTextBuilder` (+0x160) ??= new AuthorTextBuilder(_authorTextSettings +0x158).
		[Token(Token = "0x6000211")]
		[Address(RVA = "0x33E630", Offset = "0x33D030", VA = "0x18033E630")]
		get
		{
			return _authorTextBuilder ??= new AuthorTextBuilder(_authorTextSettings);
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x33E700 (stub), MoveNext 0x33EB70, <>n__0 base accessor 0x33E9B0
	// - `<task>5__2` (+0x20) = base.Initialize(): BacklogPanel.Initialize inlined through <>n__0 (BindInput slot 93
	//   "ShowBacklog" -> Show (slot 47), "Cancel" -> Hide (slot 48), returns default).
	// - `_authorTextBuilder = new AuthorTextBuilder(_authorTextSettings)` stored directly (not via the getter), reloaded,
	//   then `InitializeAsync(destroyCancellationToken)` awaited through the Cysharp awaiter.
	// - `task` awaited last through the Naninovel IAwaiter (state 1).
	[Token(Token = "0x6000212")]
	[Address(RVA = "0x33E700", Offset = "0x33D100", VA = "0x18033E700", Slot = "83")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__4))]
	public override async Naninovel.UniTask Initialize()
	{
		Naninovel.UniTask task = base.Initialize();
		_authorTextBuilder = new AuthorTextBuilder(_authorTextSettings);
		await _authorTextBuilder.InitializeAsync(destroyCancellationToken);
		await task;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x33E830
	// BacklogPanel.SpawnMessage (real call); `LastMessage` (slot 100) exact-type test against the sealed
	// WitchTrialsLogMessageUi -> ModifyAuthorPanel(AuthorTextBuilder) with the getter inlined.
	[Token(Token = "0x6000213")]
	[Address(RVA = "0x33E830", Offset = "0x33D230", VA = "0x18033E830", Slot = "114")]
	protected override void SpawnMessage(BacklogMessage message)
	{
		base.SpawnMessage(message);
		if (LastMessage is WitchTrialsLogMessageUi messageUi)
		{
			messageUi.ModifyAuthorPanel(AuthorTextBuilder);
		}
	}

	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x33E9A0, 7 B
	// Tail jump to BacklogPanel..ctor; no field initializers.
	[Token(Token = "0x6000214")]
	[Address(RVA = "0x33E9A0", Offset = "0x33D3A0", VA = "0x18033E9A0")]
	public WitchTrialsLogUi()
	{
	}
}
