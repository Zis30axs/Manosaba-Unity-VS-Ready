using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Models;

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

	[Token(Token = "0x17000109")]
	public new WitchTrialsChoiceHandlersConfiguration Configuration
	{
		[Token(Token = "0x6000644")]
		[Address(RVA = "0x398D30", Offset = "0x397730", VA = "0x180398D30", Slot = "54")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700010A")]
	public bool CriticalChoiceHintEnabled
	{
		[Token(Token = "0x6000645")]
		[Address(RVA = "0x398D40", Offset = "0x397740", VA = "0x180398D40", Slot = "50")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000646")]
		[Address(RVA = "0x398D50", Offset = "0x397750", VA = "0x180398D50", Slot = "51")]
		set
		{
		}
	}

	[Token(Token = "0x14000002")]
	public event Action<bool> OnCriticalChoiceHintChanged
	{
		[Token(Token = "0x600064A")]
		[Address(RVA = "0x399150", Offset = "0x397B50", VA = "0x180399150", Slot = "52")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x600064B")]
		[Address(RVA = "0x399260", Offset = "0x397C60", VA = "0x180399260", Slot = "53")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6000647")]
	[Address(RVA = "0x398D80", Offset = "0x397780", VA = "0x180398D80")]
	public WitchTrialsChoiceHandlerManager(WitchTrialsChoiceHandlersConfiguration configWitchTrials, ChoiceHandlersConfiguration config, IResourceProviderManager resources, ILocalizationManager l10n)
	{
	}

	[Token(Token = "0x6000648")]
	[Address(RVA = "0x398F80", Offset = "0x397980", VA = "0x180398F80", Slot = "55")]
	public void SaveServiceState(SettingsStateMap stateMap)
	{
	}

	[Token(Token = "0x6000649")]
	[Address(RVA = "0x399080", Offset = "0x397A80", VA = "0x180399080", Slot = "56")]
	public UniTask LoadServiceState(SettingsStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600064C")]
	[Address(RVA = "0x398D50", Offset = "0x397750", VA = "0x180398D50")]
	private void SetCriticalChoiceHintEnabled(bool enable)
	{
	}
}
