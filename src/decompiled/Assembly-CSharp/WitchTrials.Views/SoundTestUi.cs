using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using YieldAwaitable = Naninovel.YieldAwaitable;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x20000D9")]
public class SoundTestUi : CustomUI, ISoundTestUi, IManagedUI
{
	[Token(Token = "0x20000DA")]
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass15_0
	{
		[Token(Token = "0x400040B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public AudioTrack track;

		[Token(Token = "0x6000340")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public _003C_003Ec__DisplayClass15_0()
		{
		}

		[Token(Token = "0x6000341")]
		[Address(RVA = "0x3606B0", Offset = "0x35F0B0", VA = "0x1803606B0")]
		internal void _003CUpdatePlayerAsync_003Eb__0(float value)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000DB")]
	[CompilerGenerated]
	private struct _003CUpdatePlayerAsync_003Ed__15
{
		[Token(Token = "0x400040C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400040D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400040E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AudioTrack track;

		[Token(Token = "0x400040F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public SoundTestUi _003C_003E4__this;

		[Token(Token = "0x4000410")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x4000411")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private _003C_003Ec__DisplayClass15_0 _003C_003E8__1;

		[Token(Token = "0x4000412")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x4000413")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private TimeSpan _003CclipLength_003E5__3;

		[Token(Token = "0x4000414")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private TimeSpan _003CintroClipLength_003E5__4;

		[Token(Token = "0x4000415")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private double _003CstartDspTime_003E5__5;

		[Token(Token = "0x4000416")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private float _003CintroLength_003E5__6;

		[Token(Token = "0x4000417")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000342")]
		[Address(RVA = "0x360810", Offset = "0x35F210", VA = "0x180360810", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000343")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000400")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	[Header("References for Sound Test")]
	[SerializeField]
	private TMP_Text _titleLabel;

	[Token(Token = "0x4000401")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	[SerializeField]
	private TMP_Text _kindLabel;

	[Token(Token = "0x4000402")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	[SerializeField]
	private TMP_Text _timeLabel;

	[Token(Token = "0x4000403")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	[SerializeField]
	private CanvasGroup _playerSliderCanvasGroup;

	[Token(Token = "0x4000404")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
	[SerializeField]
	private Slider _playerSlider;

	[Token(Token = "0x4000405")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
	[SerializeField]
	private Image _fillImage;

	[Token(Token = "0x4000406")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
	[SerializeField]
	private Image _handleImage;

	[Token(Token = "0x4000407")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x118")]
	[Header("Parameters for Sound Test")]
	[SerializeField]
	private Color _introColor;

	[Token(Token = "0x4000408")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x128")]
	[SerializeField]
	private Color _loopColor;

	[Token(Token = "0x4000409")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x138")]
	private IAudioManager _naniAudioManager;

	[Token(Token = "0x400040A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x140")]
	private CancellationTokenSource _cts;

	[Token(Token = "0x1700006A")]
	private IAudioManager NaniAudioManager
	{
		[Token(Token = "0x6000339")]
		[Address(RVA = "0x35FEC0", Offset = "0x35E8C0", VA = "0x18035FEC0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600033A")]
	[Address(RVA = "0x35FF90", Offset = "0x35E990", VA = "0x18035FF90", Slot = "95")]
	public void ModifyPlayer(string trackName)
	{
	}

	[Token(Token = "0x600033B")]
	[Address(RVA = "0x360210", Offset = "0x35EC10", VA = "0x180360210")]
	public void ResetPlayer()
	{
	}

	[Token(Token = "0x600033C")]
	[Address(RVA = "0x360420", Offset = "0x35EE20", VA = "0x180360420")]
	[AsyncStateMachine(typeof(_003CUpdatePlayerAsync_003Ed__15))]
	private UniTask UpdatePlayerAsync(AudioTrack track, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600033D")]
	[Address(RVA = "0x360620", Offset = "0x35F020", VA = "0x180360620")]
	private void SetValueWithoutNotify(float progress)
	{
	}

	[Token(Token = "0x600033E")]
	[Address(RVA = "0x360690", Offset = "0x35F090", VA = "0x180360690")]
	private static TimeSpan GetTimeSpan(int samples, int frequency)
	{
		return default(TimeSpan);
	}

	[Token(Token = "0x600033F")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public SoundTestUi()
	{
	}
}
