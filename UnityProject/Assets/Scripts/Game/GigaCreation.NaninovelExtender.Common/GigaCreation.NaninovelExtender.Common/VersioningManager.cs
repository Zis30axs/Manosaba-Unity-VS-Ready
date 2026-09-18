using System;
using GigaCreation.Essentials.SaveLoad;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Commands;

namespace GigaCreation.NaninovelExtender.Common
{
    [Token(Token = "0x2000031")]
    [UsedImplicitly]
    [Goto.DontReset]
    [InitializeAtRuntime(-2147483646, null)]
    public class VersioningManager : IVersioningManager, IEngineService, IStatefulService<SettingsStateMap>, IStatefulService<GlobalStateMap>, IStatefulService<GameStateMap>
    {
    	[Serializable]
    	[Token(Token = "0x2000032")]
    	public class VersioningState
    	{
    		[Token(Token = "0x40000A9")]
    		[FieldOffset(Offset = "0x10")]
    		public VersionNumber CreatedVersion;

    		[Token(Token = "0x40000AA")]
    		[FieldOffset(Offset = "0x1C")]
    		public VersionNumber EditedVersion;

    		[Token(Token = "0x600008C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public VersioningState()
    		{
    		}
    	}

    	[Token(Token = "0x40000A6")]
    	[FieldOffset(Offset = "0x10")]
    	private VersionNumber _settingsCreatedVersion;

    	[Token(Token = "0x40000A7")]
    	[FieldOffset(Offset = "0x1C")]
    	private VersionNumber _globalCreatedVersion;

    	[Token(Token = "0x40000A8")]
    	[FieldOffset(Offset = "0x28")]
    	private VersionNumber _gameCreatedVersion;

    	[Token(Token = "0x6000082")]
    	[Address(RVA = "0xD020C0", Offset = "0xD00AC0", VA = "0x180D020C0", Slot = "4")]
    	public UniTask InitializeService()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000083")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    	public void ResetService()
    	{
    	}

    	[Token(Token = "0x6000084")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "6")]
    	public void DestroyService()
    	{
    	}

    	[Token(Token = "0x6000085")]
    	[Address(RVA = "0xD02130", Offset = "0xD00B30", VA = "0x180D02130", Slot = "7")]
    	public void SaveServiceState(SettingsStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x6000086")]
    	[Address(RVA = "0xD021E0", Offset = "0xD00BE0", VA = "0x180D021E0", Slot = "8")]
    	public UniTask LoadServiceState(SettingsStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000087")]
    	[Address(RVA = "0xD02290", Offset = "0xD00C90", VA = "0x180D02290", Slot = "9")]
    	public void SaveServiceState(GlobalStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x6000088")]
    	[Address(RVA = "0xD02340", Offset = "0xD00D40", VA = "0x180D02340", Slot = "10")]
    	public UniTask LoadServiceState(GlobalStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000089")]
    	[Address(RVA = "0xD023F0", Offset = "0xD00DF0", VA = "0x180D023F0", Slot = "11")]
    	public void SaveServiceState(GameStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x600008A")]
    	[Address(RVA = "0xD024A0", Offset = "0xD00EA0", VA = "0x180D024A0", Slot = "12")]
    	public UniTask LoadServiceState(GameStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600008B")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public VersioningManager()
    	{
    	}
    }
}
