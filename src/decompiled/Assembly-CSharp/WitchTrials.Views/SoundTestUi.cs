using System.Collections.Generic;
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
		// RECONSTRUCTED | Confidence: A | RVA: 0x35FEC0, 202 B
		// Lazy cache in +0x138 via `Engine.GetServiceOrErr<IAudioManager>()`.
		[Token(Token = "0x6000339")]
		[Address(RVA = "0x35FEC0", Offset = "0x35E8C0", VA = "0x18035FEC0")]
		get
		{
			return _naniAudioManager ??= Engine.GetServiceOrErr<IAudioManager>();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x35FF90, 625 B
	// - new List<string>; IAudioManager slot 27 GetPlayedBgm(list).
	// - Count > 1: ResetPlayer(), Debug.Log("More than one BGM is playing: " + string.Join(", ", list)).
	// - Count == 0: tail jump to ResetPlayer.
	// - Otherwise _titleLabel.SetText(trackName); slot 30 GetAudioTrack(list[0]) type-checked as AudioTrack ->
	//   UpdatePlayerAsync(track, destroyCancellationToken).Forget() (Naninovel ForgetCore).
	[Token(Token = "0x600033A")]
	[Address(RVA = "0x35FF90", Offset = "0x35E990", VA = "0x18035FF90", Slot = "95")]
	public void ModifyPlayer(string trackName)
	{
		List<string> playedBgm = new List<string>();
		NaniAudioManager.GetPlayedBgm(playedBgm);
		if (playedBgm.Count > 1)
		{
			ResetPlayer();
			UnityEngine.Debug.Log("More than one BGM is playing: " + string.Join(", ", playedBgm));
			return;
		}
		if (playedBgm.Count == 0)
		{
			ResetPlayer();
			return;
		}
		_titleLabel.SetText(trackName);
		if (NaniAudioManager.GetAudioTrack(playedBgm[0]) is AudioTrack track)
		{
			UpdatePlayerAsync(track, destroyCancellationToken).Forget();
		}
	}

	// RECONSTRUCTED | Confidence: A (Color.clear spelling: B) | RVA: 0x360210, 524 B
	// Labels "−" / "−" / "− / −" (U+2212); blocksRaycasts = false; Slider vtable slot 48 SetValueWithoutNotify(0f);
	// Graphic slot 23 set_color with an all-zero Color on both images; `_cts` (+0x140) non-null -> inlined Cancel()
	// (disposed check + NotifyCancellation(false)), Dispose(), null.
	[Token(Token = "0x600033B")]
	[Address(RVA = "0x360210", Offset = "0x35EC10", VA = "0x180360210")]
	public void ResetPlayer()
	{
		_titleLabel.SetText("−");
		_kindLabel.SetText("−");
		_timeLabel.SetText("− / −");
		_playerSliderCanvasGroup.blocksRaycasts = false;
		_playerSlider.SetValueWithoutNotify(0f);
		_fillImage.color = Color.clear;
		_handleImage.color = Color.clear;
		if (_cts != null)
		{
			_cts.Cancel();
			_cts.Dispose();
			_cts = null;
		}
	}

	// RECONSTRUCTED | Confidence: A (hoisted-local clear and Yield spelling: B) | RVA: 0x360420 (stub), MoveNext 0x360810, 4484 B; lambda 0x3606B0
	// - DisplayClass15_0 {track}; `_cts` Cancel+Dispose when present, new CancellationTokenSource, `<linkedCts>5__2` =
	//   CreateLinkedTokenSource(_cts.Token, ct) (never disposed).
	// - ObjectUtils.IsValid(track.IntroClip +0x28): "Intro" label, blocksRaycasts false, fill = _introColor (+0x118), handle
	//   clear; introClipLength = samples * 1e7 / frequency ticks (GetTimeSpan inlined); startDspTime = AudioSettings.dspTime
	//   (icall get_dspTime); introLength = clip.length. Loop while not cancelled and dspTime < start + length: elapsed =
	//   (long)((dspTime - start) * 1e7) ticks; SetValueWithoutNotify((float)(elapsed.TotalSeconds / introClipLength.TotalSeconds));
	//   _timeLabel = string.Format("{0:mm\\:ss} / {1:mm\\:ss}", elapsed, introClipLength); await Yield(Update = 8, token).
	//   On leaving the block `<introClipLength>5__4` (+0x48) is zeroed (compiler hoisted-local cleanup).
	// - "Loop" label, blocksRaycasts true, both images _loopColor (+0x128); clipLength from track.Clip (+0x20);
	//   _playerSlider.OnValueChangedAsObservable().Skip(1).Subscribe(b__0) registered on linkedCts.Token via the inlined
	//   Cysharp AddTo (RegisterWithoutCaptureExecutionContext + cached dispose callback).
	// - Loop while not cancelled: elapsed from track.Source (+0x30) timeSamples (icall) and clip frequency; same slider /
	//   label update against clipLength; await Yield(Update, linkedCts.Token).
	// - b__0(value): track.Source.timeSamples = (int)(track.Clip.samples * value).
	[Token(Token = "0x600033C")]
	[Address(RVA = "0x360420", Offset = "0x35EE20", VA = "0x180360420")]
	[AsyncStateMachine(typeof(_003CUpdatePlayerAsync_003Ed__15))]
	private async UniTask UpdatePlayerAsync(AudioTrack track, [Optional] CancellationToken ct)
	{
		if (_cts != null)
		{
			_cts.Cancel();
			_cts.Dispose();
		}
		_cts = new CancellationTokenSource();
		CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, ct);
		if (ObjectUtils.IsValid(track.IntroClip))
		{
			_kindLabel.SetText("Intro");
			_playerSliderCanvasGroup.blocksRaycasts = false;
			_fillImage.color = _introColor;
			_handleImage.color = Color.clear;
			TimeSpan introClipLength = GetTimeSpan(track.IntroClip.samples, track.IntroClip.frequency);
			double startDspTime = AudioSettings.dspTime;
			float introLength = track.IntroClip.length;
			while (!linkedCts.Token.IsCancellationRequested && AudioSettings.dspTime < startDspTime + introLength)
			{
				TimeSpan elapsed = new TimeSpan((long)((AudioSettings.dspTime - startDspTime) * TimeSpan.TicksPerSecond));
				SetValueWithoutNotify((float)(elapsed.TotalSeconds / introClipLength.TotalSeconds));
				_timeLabel.SetText(string.Format("{0:mm\\:ss} / {1:mm\\:ss}", elapsed, introClipLength));
				await UniTask.Yield(PlayerLoopTiming.Update, linkedCts.Token);
			}
		}
		_kindLabel.SetText("Loop");
		_playerSliderCanvasGroup.blocksRaycasts = true;
		_fillImage.color = _loopColor;
		_handleImage.color = _loopColor;
		TimeSpan clipLength = GetTimeSpan(track.Clip.samples, track.Clip.frequency);
		Cysharp.Threading.Tasks.CancellationTokenExtensions.AddTo(
			UniRx.ObservableExtensions.Subscribe(UniRx.Observable.Skip(UniRx.UnityUIComponentExtensions.OnValueChangedAsObservable(_playerSlider), 1),
				value => track.Source.timeSamples = (int)(track.Clip.samples * value)),
			linkedCts.Token);
		while (!linkedCts.Token.IsCancellationRequested)
		{
			TimeSpan elapsed = GetTimeSpan(track.Source.timeSamples, track.Clip.frequency);
			SetValueWithoutNotify((float)(elapsed.TotalSeconds / clipLength.TotalSeconds));
			_timeLabel.SetText(string.Format("{0:mm\\:ss} / {1:mm\\:ss}", elapsed, clipLength));
			await UniTask.Yield(PlayerLoopTiming.Update, linkedCts.Token);
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x360620, 104 B
	// progress < 0.001f -> 0; > 0.999f -> 1; otherwise the inlined Mathf.Clamp(progress, 0.001f, 0.999f); then Slider
	// vtable slot 48 SetValueWithoutNotify (tail jump).
	[Token(Token = "0x600033D")]
	[Address(RVA = "0x360620", Offset = "0x35F020", VA = "0x180360620")]
	private void SetValueWithoutNotify(float progress)
	{
		_playerSlider.SetValueWithoutNotify(progress < 0.001f ? 0f : progress > 0.999f ? 1f : Mathf.Clamp(progress, 0.001f, 0.999f));
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x360690, 19 B
	// (long)samples * 10,000,000 / frequency ticks.
	[Token(Token = "0x600033E")]
	[Address(RVA = "0x360690", Offset = "0x35F090", VA = "0x180360690")]
	private static TimeSpan GetTimeSpan(int samples, int frequency)
	{
		return new TimeSpan(samples * TimeSpan.TicksPerSecond / frequency);
	}

	[Token(Token = "0x600033F")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public SoundTestUi()
	{
	}
}
