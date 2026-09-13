using System;
using System.Runtime.CompilerServices;
using GigaCreation.NaninovelExtender.Core;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using Naninovel;
using UniRx;

namespace WitchTrials.Models;

[Token(Token = "0x2000179")]
public class WitchTrialsNaninovelManager : NaninovelManager, IWitchTrialsNaninovelManager, INaninovelManager, IService, INaninovelStateSaver, IDisposable
{
	[Token(Token = "0x4000705")]
	private const string MaxHpVarName = "maxHp";

	[Token(Token = "0x4000706")]
	private const string PerspectiveStageOffsetXVarName = "stageOffsetX";

	[Token(Token = "0x4000707")]
	private const string PerspectiveStageOffsetYVarName = "stageOffsetY";

	[Token(Token = "0x4000708")]
	private const string PerspectiveStageOffsetZVarName = "stageOffsetZ";

	[Token(Token = "0x4000709")]
	private const string VoicePitchName = "Voice Pitch";

	[Token(Token = "0x400070A")]
	[FieldOffset(Offset = "0x20")]
	private readonly ReactiveProperty<bool> _isInTitleScreen;

	[Token(Token = "0x400070B")]
	[FieldOffset(Offset = "0x28")]
	private readonly ReactiveProperty<bool> _isTrialMode;

	[Token(Token = "0x400070C")]
	[FieldOffset(Offset = "0x30")]
	private readonly ReactiveProperty<bool> _canFastForwardDebate;

	[Token(Token = "0x400070D")]
	[FieldOffset(Offset = "0x38")]
	private readonly Timer _trialTimer;

	[Token(Token = "0x400070E")]
	[FieldOffset(Offset = "0x40")]
	private IPerspectiveStageManager _perspectiveStageManager;

	[Token(Token = "0x400070F")]
	[FieldOffset(Offset = "0x48")]
	private IAudioManager _naniAudioManager;

	[Token(Token = "0x4000710")]
	[FieldOffset(Offset = "0x50")]
	private IScriptPlayer _naniScriptPlayer;

	[Token(Token = "0x4000711")]
	[FieldOffset(Offset = "0x58")]
	private ICustomVariableManager _naniVarManager;

	[Token(Token = "0x4000712")]
	[FieldOffset(Offset = "0x60")]
	private IMasterData _masterData;

	[Token(Token = "0x4000713")]
	[FieldOffset(Offset = "0x68")]
	private int _pauseRequestCount;

	[Token(Token = "0x4000714")]
	[FieldOffset(Offset = "0x6C")]
	private float _originalSkipTimeScale;

	[Token(Token = "0x1700010D")]
	public bool IsInDebate
	{
		[Token(Token = "0x600065A")]
		[Address(RVA = "0x399380", Offset = "0x397D80", VA = "0x180399380", Slot = "10")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x600065B")]
		[Address(RVA = "0x399390", Offset = "0x397D90", VA = "0x180399390")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x1700010E")]
	public bool IsPresentEvidenceMode
	{
		[Token(Token = "0x600065C")]
		[Address(RVA = "0x3993A0", Offset = "0x397DA0", VA = "0x1803993A0", Slot = "11")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x600065D")]
		[Address(RVA = "0x3993B0", Offset = "0x397DB0", VA = "0x1803993B0", Slot = "12")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x1700010F")]
	public override string StartGameScenarioName
	{
		[Token(Token = "0x600065E")]
		[Address(RVA = "0x3993C0", Offset = "0x397DC0", VA = "0x1803993C0", Slot = "7")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000110")]
	private IPerspectiveStageManager PerspectiveStageManager
	{
		[Token(Token = "0x600065F")]
		[Address(RVA = "0x399500", Offset = "0x397F00", VA = "0x180399500")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000111")]
	private IAudioManager NaniAudioManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x3995C0
		// Lazily-cached engine service lookup, same pattern used throughout
		// this codebase: `_naniAudioManager ??= Engine.GetServiceOrErr<IAudioManager>();`
		[Token(Token = "0x6000660")]
		[Address(RVA = "0x3995C0", Offset = "0x397FC0", VA = "0x1803995C0")]
		get
		{
			return _naniAudioManager ??= Engine.GetServiceOrErr<IAudioManager>();
		}
	}

	[Token(Token = "0x17000112")]
	private IScriptPlayer NaniScriptPlayer
	{
		[Token(Token = "0x6000661")]
		[Address(RVA = "0x399680", Offset = "0x398080", VA = "0x180399680")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000113")]
	private ICustomVariableManager NaniVarManager
	{
		[Token(Token = "0x6000662")]
		[Address(RVA = "0x399740", Offset = "0x398140", VA = "0x180399740")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000114")]
	private IMasterData MasterData
	{
		[Token(Token = "0x6000663")]
		[Address(RVA = "0x399800", Offset = "0x398200", VA = "0x180399800")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000664")]
	[Address(RVA = "0x3998D0", Offset = "0x3982D0", VA = "0x1803998D0", Slot = "13")]
	public void SetTitleScreenShown(bool isIn)
	{
	}

	[Token(Token = "0x6000665")]
	[Address(RVA = "0x399940", Offset = "0x398340", VA = "0x180399940", Slot = "14")]
	public void ChangeTrialMode(bool enable)
	{
	}

	[Token(Token = "0x6000666")]
	[Address(RVA = "0x3999B0", Offset = "0x3983B0", VA = "0x1803999B0", Slot = "15")]
	public void SetTrialTimer(float time)
	{
	}

	[Token(Token = "0x6000667")]
	[Address(RVA = "0x399A80", Offset = "0x398480", VA = "0x180399A80", Slot = "16")]
	public void ChangeDebateMode(bool enable)
	{
	}

	// RECONSTRUCTED | Confidence: A (structure and field writes), B (one call)
	// RVA: 0x399BE0
	//
	// Native evidence for the overall shape:
	//   enable branch:  _pauseRequestCount++; _canFastForwardDebate.Value = false;
	//                   if (IsInDebate) { <freeze-time block, value 0f> }
	//                   if (!processTimer) return;
	//                   _trialTimer.Speed = 0f;
	//   disable branch: _pauseRequestCount = Math.Max(_pauseRequestCount - 1, 0);
	//                   if (_pauseRequestCount > 0) return;
	//                   _canFastForwardDebate.Value = IsInDebate;
	//                   <freeze-time block, value 1f>   -- unconditional here,
	//                   unlike the enable branch's IsInDebate-gated version
	//                   if (!processTimer) return;
	//                   _trialTimer.Speed = 1f;
	//   (_trialTimer.Speed is a direct field write in the disassembly, at the
	//   exact offset (+0x28) of Timer's own set_Speed -- which is itself a
	//   trivial one-line setter, confirmed by disassembling it separately, so
	//   `_trialTimer.Speed = ...` reproduces it exactly either way.)
	//
	// The "freeze-time block" (Confidence B, not A): resolves and calls a
	// cached delegate/function pointer via a helper taking the literal string
	// "UnityEngine.Time" (confirmed by reading the raw bytes at that data
	// address), passing a float (1.0f in the disable branch, 0.0f in the
	// enable branch, both confirmed by reading the raw float constants) --
	// and the resolve-or-throw pattern matches the shape IL2CPP generates
	// elsewhere for exception-on-missing-member. This is written below as
	// `Time.timeScale = <value>` because the evidence (target type name +
	// numeric value 1.0/0.0, the conventional "restore/freeze time" values)
	// strongly implies that specific well-known API, but the precise
	// call mechanism (looks reflection-based rather than a direct property
	// set) was not fully traced, hence B rather than A on this one line only.
	[Token(Token = "0x6000668")]
	[Address(RVA = "0x399BE0", Offset = "0x3985E0", VA = "0x180399BE0", Slot = "17")]
	public void ChangeTrialPauseMode(bool enable, bool processTimer)
	{
		if (enable)
		{
			_pauseRequestCount++;
			_canFastForwardDebate.Value = false;
			if (IsInDebate)
			{
				UnityEngine.Time.timeScale = 0f;
				NaniAudioManager.AudioMixer.SetFloat("Voice Pitch", 0f);
			}
			if (!processTimer)
			{
				return;
			}
			_trialTimer.Speed = 0f;
		}
		else
		{
			_pauseRequestCount = Math.Max(_pauseRequestCount - 1, 0);
			if (_pauseRequestCount > 0)
			{
				return;
			}
			_canFastForwardDebate.Value = IsInDebate;
			UnityEngine.Time.timeScale = 1f;
			NaniAudioManager.AudioMixer.SetFloat("Voice Pitch", 1f);
			if (!processTimer)
			{
				return;
			}
			_trialTimer.Speed = 1f;
		}
	}

	[Token(Token = "0x6000669")]
	[Address(RVA = "0x399E60", Offset = "0x398860", VA = "0x180399E60", Slot = "21")]
	public void Dispose()
	{
	}

	[Token(Token = "0x600066A")]
	[Address(RVA = "0x399EE0", Offset = "0x3988E0", VA = "0x180399EE0", Slot = "18")]
	public IObservable<Unit> OnEnterTitleScreenAsObservable()
	{
		return null;
	}

	[Token(Token = "0x600066B")]
	[Address(RVA = "0x39A0E0", Offset = "0x398AE0", VA = "0x18039A0E0", Slot = "19")]
	public IObservable<TimeSpan> OnRemainingTimeChangedAsObservable()
	{
		return null;
	}

	[Token(Token = "0x600066C")]
	[Address(RVA = "0x39A160", Offset = "0x398B60", VA = "0x18039A160", Slot = "20")]
	public IObservable<bool> OnDebateSkippableChangedAsObservable()
	{
		return null;
	}

	[Token(Token = "0x600066D")]
	[Address(RVA = "0x39A1D0", Offset = "0x398BD0", VA = "0x18039A1D0")]
	private void StartTrial()
	{
	}

	[Token(Token = "0x600066E")]
	[Address(RVA = "0x39A410", Offset = "0x398E10", VA = "0x18039A410")]
	private void StopTrial()
	{
	}

	[Token(Token = "0x600066F")]
	[Address(RVA = "0x39A470", Offset = "0x398E70", VA = "0x18039A470")]
	private void StartDebate()
	{
	}

	[Token(Token = "0x6000670")]
	[Address(RVA = "0x39A780", Offset = "0x399180", VA = "0x18039A780")]
	private void StopDebate()
	{
	}

	[Token(Token = "0x6000671")]
	[Address(RVA = "0x39A8D0", Offset = "0x3992D0", VA = "0x18039A8D0")]
	private void PauseTrial(bool processTimer)
	{
	}

	[Token(Token = "0x6000672")]
	[Address(RVA = "0x39AA00", Offset = "0x399400", VA = "0x18039AA00")]
	private void UnpauseTrial(bool processTimer)
	{
	}

	[Token(Token = "0x6000673")]
	[Address(RVA = "0x39AB50", Offset = "0x399550", VA = "0x18039AB50")]
	private void PauseOrUnpauseVoicePlayer(bool pause)
	{
	}

	[Token(Token = "0x6000674")]
	[Address(RVA = "0x39ABF0", Offset = "0x3995F0", VA = "0x18039ABF0")]
	private void OnSkipModeChangedDuringDebate(bool enable)
	{
	}

	[Token(Token = "0x6000675")]
	[Address(RVA = "0x39AD50", Offset = "0x399750", VA = "0x18039AD50")]
	public WitchTrialsNaninovelManager()
	{
	}
}
