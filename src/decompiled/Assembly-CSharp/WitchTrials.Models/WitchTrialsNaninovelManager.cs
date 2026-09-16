using System;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.MasterData;
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
	private readonly ReactiveProperty<bool> _isInTitleScreen = new ReactiveProperty<bool>(false);

	[Token(Token = "0x400070B")]
	[FieldOffset(Offset = "0x28")]
	private readonly ReactiveProperty<bool> _isTrialMode = new ReactiveProperty<bool>();

	[Token(Token = "0x400070C")]
	[FieldOffset(Offset = "0x30")]
	private readonly ReactiveProperty<bool> _canFastForwardDebate = new ReactiveProperty<bool>();

	[Token(Token = "0x400070D")]
	[FieldOffset(Offset = "0x38")]
	private readonly Timer _trialTimer = new Timer(true);

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

	// Auto-property. The backing field is the byte at +0x70, written directly
	// (`mov byte ptr [rbx+0x70], 0/1`) by StartDebate/StopDebate/ChangeDebateMode
	// and read by PauseTrial/UnpauseTrial. The accessor bodies are shared tiny
	// getters/setters (uncounted).
	[Token(Token = "0x1700010D")]
	public bool IsInDebate
	{
		[Token(Token = "0x600065A")]
		[Address(RVA = "0x399380", Offset = "0x397D80", VA = "0x180399380", Slot = "10")]
		[CompilerGenerated]
		get;
		[Token(Token = "0x600065B")]
		[Address(RVA = "0x399390", Offset = "0x397D90", VA = "0x180399390")]
		[CompilerGenerated]
		private set;
	}

	[Token(Token = "0x1700010E")]
	public bool IsPresentEvidenceMode
	{
		[Token(Token = "0x600065C")]
		[Address(RVA = "0x3993A0", Offset = "0x397DA0", VA = "0x1803993A0", Slot = "11")]
		[CompilerGenerated]
		get;
		[Token(Token = "0x600065D")]
		[Address(RVA = "0x3993B0", Offset = "0x397DB0", VA = "0x1803993B0", Slot = "12")]
		[CompilerGenerated]
		set;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3993C0, 319 B
	// Starts with the inlined `DebugOptions.StartGameScenario` (0x3AA370:
	// IsDebugMode && static +0xA `s_overrideStartGameScenario` ?
	// +0x10 `s_startGameScenario` : null); a non-null result is returned.
	// Otherwise the remaining bytes (lazy +0x18 `_naniScriptManager` via
	// `Engine.GetServiceOrErr<IScriptManager>()`, IEngineService<ScriptsConfiguration>
	// slot 0 `Configuration`, +0x48 `StartGameScript`) are byte-identical to the
	// base `NaninovelManager.get_StartGameScenarioName` at 0xD026C0, which
	// was disassembled for comparison. That base getter is inlined here; the
	// private `NaniScriptManager` it uses is not accessible from this class.
	[Token(Token = "0x1700010F")]
	public override string StartGameScenarioName
	{
		[Token(Token = "0x600065E")]
		[Address(RVA = "0x3993C0", Offset = "0x397DC0", VA = "0x1803993C0", Slot = "7")]
		get
		{
			return WitchTrials.Debugging.DebugOptions.StartGameScenario ?? base.StartGameScenarioName;
		}
	}

	[Token(Token = "0x17000110")]
	private IPerspectiveStageManager PerspectiveStageManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x399500
		// `_perspectiveStageManager ??= ServiceLocator.Get<IPerspectiveStageManager>(includeDerivedType: true);`
		[Token(Token = "0x600065F")]
		[Address(RVA = "0x399500", Offset = "0x397F00", VA = "0x180399500")]
		get
		{
			return _perspectiveStageManager ??= ServiceLocator.Get<IPerspectiveStageManager>(includeDerivedType: true);
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
		// RECONSTRUCTED | Confidence: A | RVA: 0x399680
		// `_naniScriptPlayer ??= Engine.GetServiceOrErr<IScriptPlayer>();`
		[Token(Token = "0x6000661")]
		[Address(RVA = "0x399680", Offset = "0x398080", VA = "0x180399680")]
		get
		{
			return _naniScriptPlayer ??= Engine.GetServiceOrErr<IScriptPlayer>();
		}
	}

	[Token(Token = "0x17000113")]
	private ICustomVariableManager NaniVarManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x399740
		// `_naniVarManager ??= Engine.GetServiceOrErr<ICustomVariableManager>();`
		[Token(Token = "0x6000662")]
		[Address(RVA = "0x399740", Offset = "0x398140", VA = "0x180399740")]
		get
		{
			return _naniVarManager ??= Engine.GetServiceOrErr<ICustomVariableManager>();
		}
	}

	[Token(Token = "0x17000114")]
	private IMasterData MasterData
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x399800
		// Different resolution path than the other services here: reads
		// `GigaCreation.Essentials.MasterData.MasterDataProvider.Data` (a static
		// IMasterDataBase property) and casts to IMasterData. The disassembly's
		// cast helper tolerates a null result without throwing (the possibly-null
		// value is cached and returned either way), matching a safe `as` cast
		// rather than a hard cast that would throw on an incompatible type.
		[Token(Token = "0x6000663")]
		[Address(RVA = "0x399800", Offset = "0x398200", VA = "0x180399800")]
		get
		{
			return _masterData ??= MasterDataProvider.Data as IMasterData;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3998D0, 96 B
	// `ReactiveProperty<bool>.set_Value` on `_isInTitleScreen` (+0x20).
	[Token(Token = "0x6000664")]
	[Address(RVA = "0x3998D0", Offset = "0x3982D0", VA = "0x1803998D0", Slot = "13")]
	public void SetTitleScreenShown(bool isIn)
	{
		_isInTitleScreen.Value = isIn;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x399940, 99 B
	// `enable` -> tail jump to StartTrial (0x39A1D0); otherwise `_isTrialMode` (+0x28) ReactiveProperty<bool>.Value = false.
	[Token(Token = "0x6000665")]
	[Address(RVA = "0x399940", Offset = "0x398340", VA = "0x180399940", Slot = "14")]
	public void ChangeTrialMode(bool enable)
	{
		if (enable)
		{
			StartTrial();
		}
		else
		{
			_isTrialMode.Value = false;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3999B0, 200 B
	// `_trialTimer` (+0x38) is loaded first; TimeSpan.Interval((double)time, 1000) is the inlined TimeSpan.FromSeconds;
	// Timer.SetTime (0x3A6E80); then `_pauseRequestCount` (+0x68) = 0.
	[Token(Token = "0x6000666")]
	[Address(RVA = "0x3999B0", Offset = "0x3983B0", VA = "0x1803999B0", Slot = "15")]
	public void SetTrialTimer(float time)
	{
		_trialTimer.SetTime(TimeSpan.FromSeconds(time));
		_pauseRequestCount = 0;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x399A80, 350 B
	// `enable` -> tail jump to `StartDebate`. Otherwise the whole `StopDebate`
	// body is inlined here, byte-identical to RVA 0x39A780.
	[Token(Token = "0x6000667")]
	[Address(RVA = "0x399A80", Offset = "0x398480", VA = "0x180399A80", Slot = "16")]
	public void ChangeDebateMode(bool enable)
	{
		if (enable)
		{
			StartDebate();
		}
		else
		{
			StopDebate();
		}
	}

	// RECONSTRUCTED | Confidence: A
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
	// The "freeze-time block": the cached resolved-icall pointer is called with
	// a float. This line was previously graded B because only the type name had
	// been read. The icall-name string it resolves (data 0x2608FF8, shared with
	// PauseTrial/UnpauseTrial) is now read in full as
	// "UnityEngine.Time::set_timeScale(System.Single)", so
	// `UnityEngine.Time.timeScale = <value>` is Confidence A. The enclosing
	// method is the inlined pair PauseTrial/UnpauseTrial below.
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

	// RECONSTRUCTED | Confidence: A | RVA: 0x399E60, 115 B
	// `ReactiveProperty<bool>.Dispose` on +0x20, +0x28 and +0x30 (the last one a
	// tail jump). `_trialTimer` is not disposed here.
	[Token(Token = "0x6000669")]
	[Address(RVA = "0x399E60", Offset = "0x398860", VA = "0x180399E60", Slot = "21")]
	public void Dispose()
	{
		_isInTitleScreen.Dispose();
		_isTrialMode.Dispose();
		_canFastForwardDebate.Dispose();
	}

	[Token(Token = "0x600066A")]
	[Address(RVA = "0x399EE0", Offset = "0x3988E0", VA = "0x180399EE0", Slot = "18")]
	public IObservable<Unit> OnEnterTitleScreenAsObservable()
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x399EE0, 502 B
		// +0x20 `_isInTitleScreen` -> `ReactivePropertyExtensions
		// .SkipLatestValueOnSubscribe<bool>` (method metadata; called through an
		// aliased shared body) -> `Observable.Where<bool>` with the cached
		// `<>c.<OnEnterTitleScreenAsObservable>b__42_0` (static slot +8; its body is
		// folded with `ByteEqualityComparer.GetHashCode`, i.e. it returns the bool
		// unchanged) -> tail jump to `AsUnitObservable<bool>`. Emits only on
		// later transitions into the title screen, never the current value.
		return _isInTitleScreen.SkipLatestValueOnSubscribe().Where(x => x).AsUnitObservable();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39A0E0, 118 B
	// `_trialTimer` (+0x38) then its +0x18 `_remainingTime`, which is the inlined
	// public `Timer.RemainingTime` getter (RVA 0x33A4E0). A tail jump to
	// `Observable.AsObservable<TimeSpan>`.
	[Token(Token = "0x600066B")]
	[Address(RVA = "0x39A0E0", Offset = "0x398AE0", VA = "0x18039A0E0", Slot = "19")]
	public IObservable<TimeSpan> OnRemainingTimeChangedAsObservable()
	{
		return _trialTimer.RemainingTime.AsObservable();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39A160, 104 B
	// A tail jump to `Observable.AsObservable<bool>` on `_canFastForwardDebate` (+0x30).
	[Token(Token = "0x600066C")]
	[Address(RVA = "0x39A160", Offset = "0x398B60", VA = "0x18039A160", Slot = "20")]
	public IObservable<bool> OnDebateSkippableChangedAsObservable()
	{
		return _canFastForwardDebate.AsObservable();
	}

	// RECONSTRUCTED | Confidence: A (structure), B (MaxHp property identity)
	// RVA: 0x39A1D0
	//
	// Native evidence: sets _isTrialMode, then seeds four Naninovel custom
	// variables from real game state (using this class's own already-declared
	// *VarName constants, whose string values match the disassembly's STR
	// literals exactly). NaniVarManager.TrySetVariableValue is called fresh
	// for every one of the four values (not cached in a local first), and
	// PerspectiveStageManager.StageOffset is likewise re-fetched three separate
	// times (once per X/Y/Z component) rather than cached in a local Vector3 --
	// reproduced exactly as evidenced rather than "cleaned up" into fewer calls.
	//
	// MasterData's float property is graded B, not A: the interface dispatch
	// itself doesn't name the method (shared thunk), but IMasterData exposes
	// exactly one float property whose name plausibly matches "maxHp" --
	// DefaultHp -- with no other candidate on the interface, so used it while
	// flagging it's inferred from naming/uniqueness rather than a resolved
	// MethodInfo slot.
	[Token(Token = "0x600066D")]
	[Address(RVA = "0x39A1D0", Offset = "0x398BD0", VA = "0x18039A1D0")]
	private void StartTrial()
	{
		_isTrialMode.Value = true;
		NaniVarManager.TrySetVariableValue(MaxHpVarName, MasterData.DefaultHp);
		NaniVarManager.TrySetVariableValue(PerspectiveStageOffsetXVarName, PerspectiveStageManager.StageOffset.x);
		NaniVarManager.TrySetVariableValue(PerspectiveStageOffsetYVarName, PerspectiveStageManager.StageOffset.y);
		NaniVarManager.TrySetVariableValue(PerspectiveStageOffsetZVarName, PerspectiveStageManager.StageOffset.z);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39A410, 84 B
	// `_isTrialMode.Value = false` (`mov byte ptr [rsp+0x30], 0` into `set_Value`).
	[Token(Token = "0x600066E")]
	[Address(RVA = "0x39A410", Offset = "0x398E10", VA = "0x18039A410")]
	private void StopTrial()
	{
		_isTrialMode.Value = false;
	}

	// RECONSTRUCTED | Confidence: A (behavior), B (`StartAsync(1f)` spelling) | RVA: 0x39A470, 774 B
	// - `IsInDebate` backing byte (+0x70) = 1.
	// - A new `Action<bool>` over `OnSkipModeChangedDuringDebate` goes to
	//   `sub_3140` with ecx = 8 = IScriptPlayer slot 8 `add_OnSkip`.
	// - `_originalSkipTimeScale` (+0x6C) is saved from the player configuration
	//   (`sub_5760` = IEngineService<ScriptPlayerConfiguration> slot 0
	//   `Configuration`, field +0x1C `SkipTimeScale`). The same field is then
	//   overwritten with IMasterData slot 1 `SkipTimeScaleDuringDebate`.
	// - The trial timer's inlined `Timer.StartAsync` kickoff has state-machine
	//   `initialSpeed` = 1f and a zero `ct`; the task goes to Cysharp `Forget`.
	//   1f is also the parameter's default, so `StartAsync()` would compile
	//   identically (B).
	[Token(Token = "0x600066F")]
	[Address(RVA = "0x39A470", Offset = "0x398E70", VA = "0x18039A470")]
	private void StartDebate()
	{
		IsInDebate = true;
		NaniScriptPlayer.OnSkip += OnSkipModeChangedDuringDebate;
		_originalSkipTimeScale = NaniScriptPlayer.Configuration.SkipTimeScale;
		NaniScriptPlayer.Configuration.SkipTimeScale = MasterData.SkipTimeScaleDuringDebate;
		_trialTimer.StartAsync(1f).Forget();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39A780, 326 B
	// - `IsInDebate` backing byte = 0.
	// - `sub_3140` with ecx = 9 = IScriptPlayer slot 9 `remove_OnSkip`, passing
	//   a fresh `Action<bool>` over the same method (delegate equality makes
	//   the removal work).
	// - "Voice Pitch" is reset to 1f on `IAudioManager.AudioMixer` (`sub_5E30`,
	//   slot 0).
	// - The player's `Configuration.SkipTimeScale` is restored from
	//   `_originalSkipTimeScale`.
	// The trial timer is not stopped here.
	[Token(Token = "0x6000670")]
	[Address(RVA = "0x39A780", Offset = "0x399180", VA = "0x18039A780")]
	private void StopDebate()
	{
		IsInDebate = false;
		NaniScriptPlayer.OnSkip -= OnSkipModeChangedDuringDebate;
		NaniAudioManager.AudioMixer.SetFloat(VoicePitchName, 1f);
		NaniScriptPlayer.Configuration.SkipTimeScale = _originalSkipTimeScale;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39A8D0, 301 B
	// The standalone enable half of `ChangeTrialPauseMode`.
	// - `_pauseRequestCount` (+0x68) is incremented, and
	//   `_canFastForwardDebate.Value = false`.
	// - Only while in a debate: icall `Time::set_timeScale` (name string
	//   0x2608FF8) with 0f, and "Voice Pitch" set to 0f.
	// - With `processTimer`, the timer's `Speed` field (+0x28) is set to 0f.
	[Token(Token = "0x6000671")]
	[Address(RVA = "0x39A8D0", Offset = "0x3992D0", VA = "0x18039A8D0")]
	private void PauseTrial(bool processTimer)
	{
		_pauseRequestCount++;
		_canFastForwardDebate.Value = false;
		if (IsInDebate)
		{
			UnityEngine.Time.timeScale = 0f;
			NaniAudioManager.AudioMixer.SetFloat(VoicePitchName, 0f);
		}
		if (processTimer)
		{
			_trialTimer.Speed = 0f;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39AA00, 324 B
	// The standalone disable half of `ChangeTrialPauseMode`.
	// - The count decrements with a floor of 0 (`cmovle`), and the method
	//   returns while it is still positive.
	// - `_canFastForwardDebate.Value = IsInDebate`, then time scale 1f and
	//   "Voice Pitch" 1f, both unconditional.
	// - With `processTimer`, the timer's `Speed` is set to 1f.
	[Token(Token = "0x6000672")]
	[Address(RVA = "0x39AA00", Offset = "0x399400", VA = "0x18039AA00")]
	private void UnpauseTrial(bool processTimer)
	{
		_pauseRequestCount = Math.Max(_pauseRequestCount - 1, 0);
		if (_pauseRequestCount > 0)
		{
			return;
		}
		_canFastForwardDebate.Value = IsInDebate;
		UnityEngine.Time.timeScale = 1f;
		NaniAudioManager.AudioMixer.SetFloat(VoicePitchName, 1f);
		if (processTimer)
		{
			_trialTimer.Speed = 1f;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39AB50, 144 B
	// `NaniAudioManager.AudioMixer` (`sub_5E30`, slot 0), then a tail jump to
	// `AudioMixer.SetFloat("Voice Pitch", pause ? 0f : 1f)`.
	[Token(Token = "0x6000673")]
	[Address(RVA = "0x39AB50", Offset = "0x399550", VA = "0x18039AB50")]
	private void PauseOrUnpauseVoicePlayer(bool pause)
	{
		NaniAudioManager.AudioMixer.SetFloat(VoicePitchName, pause ? 0f : 1f);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39ABF0, 338 B
	// The `IScriptPlayer.OnSkip` handler while in a debate. `_trialTimer` (+0x38)
	// is loaded first.
	// - Skip off: timer `Speed` = 1f and "Voice Pitch" = 1f.
	// - Skip on: timer `Speed` = player `Configuration.SkipTimeScale` (the debate
	//   value set by `StartDebate`), and "Voice Pitch" = IMasterData slot 2
	//   `VoicePitchOnSkip`.
	[Token(Token = "0x6000674")]
	[Address(RVA = "0x39ABF0", Offset = "0x3995F0", VA = "0x18039ABF0")]
	private void OnSkipModeChangedDuringDebate(bool enable)
	{
		if (!enable)
		{
			_trialTimer.Speed = 1f;
			NaniAudioManager.AudioMixer.SetFloat(VoicePitchName, 1f);
		}
		else
		{
			_trialTimer.Speed = NaniScriptPlayer.Configuration.SkipTimeScale;
			NaniAudioManager.AudioMixer.SetFloat(VoicePitchName, MasterData.VoicePitchOnSkip);
		}
	}

	// RECONSTRUCTED | Confidence: A (values), B (initializer-vs-ctor spelling) | RVA: 0x39AD50, 572 B
	// Only the four readonly-field stores, now written as field initializers.
	// The base `NaninovelManager..ctor` is the empty shared 0x30C000 and emits
	// no call.
	// - +0x20 = `new ReactiveProperty<bool>(false)` (the value ctor 0x8C4A20
	//   with a zero byte).
	// - +0x28 and +0x30 = `new ReactiveProperty<bool>()` (parameterless 0x8C4900).
	// - +0x38 = `WitchTrials.Models.Timer` with its ctor(bool useRealtime)
	//   inlined: +0x28 `Speed` = 1f initializer, +0x10 `_useRealtime` = true.
	[Token(Token = "0x6000675")]
	[Address(RVA = "0x39AD50", Offset = "0x399750", VA = "0x18039AD50")]
	public WitchTrialsNaninovelManager()
	{
	}
}
