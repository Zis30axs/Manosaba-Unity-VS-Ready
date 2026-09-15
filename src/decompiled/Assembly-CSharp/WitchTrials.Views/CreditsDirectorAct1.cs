using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using LitMotion;
using Naninovel;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x20000A0")]
public sealed class CreditsDirectorAct1 : CreditsDirectorBase
{
	[StructLayout(3)]
	[Token(Token = "0x20000A1")]
	[CompilerGenerated]
	private struct _003CPlayAsync_003Ed__17
{
		[Token(Token = "0x40002B8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002B9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40002BA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CreditsDirectorAct1 _003C_003E4__this;

		[Token(Token = "0x40002BB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x40002BC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private MotionHandle _003CscrollMotion_003E5__2;

		[Token(Token = "0x40002BD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600023E")]
		[Address(RVA = "0x3413F0", Offset = "0x33FDF0", VA = "0x1803413F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600023F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40002AB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[Space]
	[SerializeField]
	private CanvasGroup _back;

	[Token(Token = "0x40002AC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[SerializeField]
	private CanvasGroup _titleLogoAct1;

	[Token(Token = "0x40002AD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[SerializeField]
	private Scrollbar _scrollbar;

	[Token(Token = "0x40002AE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	[SerializeField]
	private CanvasGroup _noiseCanvasGroup;

	[Token(Token = "0x40002AF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	[Space]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _noiseBgmPath;

	[Token(Token = "0x40002B0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	[SerializeField]
	private float _noiseDuration;

	[Token(Token = "0x40002B1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x7C")]
	[SerializeField]
	private float _noiseInterval;

	[Token(Token = "0x40002B2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	[SerializeField]
	private float _noiseBgmVolume;

	[Token(Token = "0x40002B3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	[Space]
	[SerializeField]
	private string _reverseLabelName;

	[Token(Token = "0x40002B4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	[SerializeField]
	private float _reverseDuration;

	[Token(Token = "0x40002B5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x94")]
	[SerializeField]
	private float _noisePowerUpDuration;

	[Token(Token = "0x40002B6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	[SerializeField]
	private float _endingInterval;

	[Token(Token = "0x40002B7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
	private IAudioManager _naniAudioManager;

	[Token(Token = "0x17000044")]
	private IAudioManager NaniAudioManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x340EE0, 202 B
		// Lazy cache in +0xA0 via `Engine.GetServiceOrErr<IAudioManager>()`.
		[Token(Token = "0x6000236")]
		[Address(RVA = "0x340EE0", Offset = "0x33F8E0", VA = "0x180340EE0")]
		get
		{
			return _naniAudioManager ??= Engine.GetServiceOrErr<IAudioManager>();
		}
	}

	[Token(Token = "0x6000237")]
	[Address(RVA = "0x340FB0", Offset = "0x33F9B0", VA = "0x180340FB0", Slot = "4")]
	protected override void Reset()
	{
	}

	[Token(Token = "0x6000238")]
	[Address(RVA = "0x341110", Offset = "0x33FB10", VA = "0x180341110", Slot = "5")]
	public override void Initialize()
	{
	}

	[Token(Token = "0x6000239")]
	[Address(RVA = "0x341180", Offset = "0x33FB80", VA = "0x180341180", Slot = "7")]
	[AsyncStateMachine(typeof(_003CPlayAsync_003Ed__17))]
	public override Cysharp.Threading.Tasks.UniTask PlayAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x600023A")]
	[Address(RVA = "0x341330", Offset = "0x33FD30", VA = "0x180341330", Slot = "8")]
	public override void Stop()
	{
	}

	[Token(Token = "0x600023B")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public CreditsDirectorAct1()
	{
	}
}
