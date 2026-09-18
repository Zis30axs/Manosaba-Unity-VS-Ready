using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Models
{
    [Token(Token = "0x2000175")]
    [UsedImplicitly]
    [InitializeAtRuntime(0, typeof(ChoiceHandlerManager))]
    public class WitchTrialsChoiceHandlerManager : ChoiceHandlerManager, IWitchTrialsChoiceHandlerManager, IEngineService<WitchTrialsChoiceHandlersConfiguration>, IEngineService, IStatefulService<SettingsStateMap>
    {
    	[Serializable]
    	[Token(Token = "0x2000176")]
    	public class SettingsWitchTrials
    	{
    		[Token(Token = "0x4000703")]
    		[FieldOffset(Offset = "0x10")]
    		public bool CriticalChoiceHintEnabled;

    		[Token(Token = "0x600064D")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public SettingsWitchTrials()
    		{
    		}
    	}

    	[Token(Token = "0x4000700")]
    	[FieldOffset(Offset = "0x60")]
    	private bool _criticalChoiceHintEnabled;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x398D30, 5 B (folded)
    	// Get-only auto-property: returns +0x68 `<Configuration>k__BackingField`.
    	[Token(Token = "0x17000109")]
    	public new WitchTrialsChoiceHandlersConfiguration Configuration
    	{
    		[Token(Token = "0x6000644")]
    		[Address(RVA = "0x398D30", Offset = "0x397730", VA = "0x180398D30", Slot = "54")]
    		[CompilerGenerated]
    		get;
    	}

    	[Token(Token = "0x1700010A")]
    	public bool CriticalChoiceHintEnabled
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x398D40, 5 B
    		// Returns byte +0x60 `_criticalChoiceHintEnabled`.
    		[Token(Token = "0x6000645")]
    		[Address(RVA = "0x398D40", Offset = "0x397740", VA = "0x180398D40", Slot = "50")]
    		get
    		{
    			return _criticalChoiceHintEnabled;
    		}
    		// Shares RVA 0x398D50 with `SetCriticalChoiceHintEnabled` (identical
    		// bodies are folded). Whether the source setter forwarded to it or
    		// duplicated it cannot be told from the bytes; the forwarding spelling
    		// has the same behavior.
    		[Token(Token = "0x6000646")]
    		[Address(RVA = "0x398D50", Offset = "0x397750", VA = "0x180398D50", Slot = "51")]
    		set
    		{
    			SetCriticalChoiceHintEnabled(value);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x399150 / 0x399260, 262 B each
    	// The standard field-like event accessors: a `Delegate.Combine`/`Remove`
    	// plus `Interlocked.CompareExchange` loop on the backing field +0x70
    	// `OnCriticalChoiceHintChanged` (dump.cs).
    	[Token(Token = "0x14000002")]
    	public event Action<bool> OnCriticalChoiceHintChanged;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x398D80, 500 B
    	// The base `ChoiceHandlerManager(config, resources, l10n)` ctor (0x453960)
    	// is inlined:
    	// - Its field initializer `<PickedChoices>k__BackingField` (+0x40) =
    	//   `new Dictionary<PlaybackSpot, PlaybackSpot>()`.
    	// - `ActorManager<...>..ctor(config)`.
    	// - `resources` -> +0x48 and `l10n` -> +0x50.
    	// Then `configWitchTrials` -> +0x68 `<Configuration>k__BackingField`.
    	[Token(Token = "0x6000647")]
    	[Address(RVA = "0x398D80", Offset = "0x397780", VA = "0x180398D80")]
    	public WitchTrialsChoiceHandlerManager(WitchTrialsChoiceHandlersConfiguration configWitchTrials, ChoiceHandlersConfiguration config, IResourceProviderManager resources, ILocalizationManager l10n)
    		: base(config, resources, l10n)
    	{
    		Configuration = configWitchTrials;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x398F80, 248 B
    	// `new SettingsWitchTrials()` with +0x10 = `_criticalChoiceHintEnabled`,
    	// then the inlined `StateMap.SetState<SettingsWitchTrials>(state, null)`:
    	// the key is the Type of the state's class (vtable slot 27 on the
    	// System.Type), and the state is stored with `Dictionary<string, object>
    	// .set_Item` into the map's +0x10 dictionary.
    	[Token(Token = "0x6000648")]
    	[Address(RVA = "0x398F80", Offset = "0x397980", VA = "0x180398F80", Slot = "55")]
    	public void SaveServiceState(SettingsStateMap stateMap)
    	{
    		SettingsWitchTrials state = new SettingsWitchTrials
    		{
    			CriticalChoiceHintEnabled = _criticalChoiceHintEnabled
    		};
    		stateMap.SetState(state);
    	}

    	// RECONSTRUCTED | Confidence: A (behavior), B (branch order / CompletedTask spelling) | RVA: 0x399080, 202 B
    	// `stateMap.GetState<SettingsWitchTrials>(null)`. When non-null, the inlined
    	// `SetCriticalChoiceHintEnabled(state.CriticalChoiceHintEnabled)` runs.
    	// Otherwise it runs with `Configuration.DefaultCriticalChoiceHintEnabled`
    	// (+0x68 -> +0x18). Returns the all-zero UniTask after a class-init check.
    	[Token(Token = "0x6000649")]
    	[Address(RVA = "0x399080", Offset = "0x397A80", VA = "0x180399080", Slot = "56")]
    	public UniTask LoadServiceState(SettingsStateMap stateMap)
    	{
    		SettingsWitchTrials state = stateMap.GetState<SettingsWitchTrials>();
    		if (state != null)
    		{
    			SetCriticalChoiceHintEnabled(state.CriticalChoiceHintEnabled);
    		}
    		else
    		{
    			SetCriticalChoiceHintEnabled(Configuration.DefaultCriticalChoiceHintEnabled);
    		}
    		return UniTask.CompletedTask;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x398D50, 44 B
    	// Stores `enable` to +0x60, then when the +0x70 delegate is non-null invokes
    	// it directly (method pointer +0x18, target +0x40) with `enable`.
    	[Token(Token = "0x600064C")]
    	[Address(RVA = "0x398D50", Offset = "0x397750", VA = "0x180398D50")]
    	private void SetCriticalChoiceHintEnabled(bool enable)
    	{
    		_criticalChoiceHintEnabled = enable;
    		OnCriticalChoiceHintChanged?.Invoke(enable);
    	}
    }
}
