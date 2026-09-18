#nullable enable annotations
using GigaCreation.Essentials.Localization;
using GigaCreation.Tools.Debugging.General;
using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace WitchTrials.Debugging
{
    [Token(Token = "0x20001B0")]
    public class DebugOptions : DebugOptionsBase
    {
    	[Token(Token = "0x40007D7")]
    	[FieldOffset(Offset = "0x0")]
    	private static bool s_enableSteamworks;

    	[Token(Token = "0x40007D8")]
    	[FieldOffset(Offset = "0x1")]
    	private static bool s_overrideLocale;

    	[Token(Token = "0x40007D9")]
    	[FieldOffset(Offset = "0x4")]
    	private static LocaleKind s_locale;

    	[Token(Token = "0x40007DA")]
    	[FieldOffset(Offset = "0x8")]
    	private static bool s_skipSplashScreen;

    	[Token(Token = "0x40007DB")]
    	[FieldOffset(Offset = "0x9")]
    	private static bool s_skipEntrySequenceAtFirstTime;

    	[Token(Token = "0x40007DC")]
    	[FieldOffset(Offset = "0xA")]
    	private static bool s_overrideStartGameScenario;

    	[Token(Token = "0x40007DD")]
    	[FieldOffset(Offset = "0x10")]
    	private static string s_startGameScenario;

    	[Token(Token = "0x40007DE")]
    	[FieldOffset(Offset = "0x18")]
    	private static bool s_debugSkip;

    	[Token(Token = "0x40007DF")]
    	[FieldOffset(Offset = "0x19")]
    	private static bool s_allowContinueDebate;

    	[Token(Token = "0x40007E0")]
    	[FieldOffset(Offset = "0x1A")]
    	private static bool s_checkWitchBookState;

    	[Token(Token = "0x40007E1")]
    	[FieldOffset(Offset = "0x20")]
    	[Header("Platforms")]
    	[SerializeField]
    	private bool _enableSteamworks;

    	[Token(Token = "0x40007E2")]
    	[FieldOffset(Offset = "0x21")]
    	[Header("Localization")]
    	[SerializeField]
    	private bool _overrideLocale;

    	[Token(Token = "0x40007E3")]
    	[FieldOffset(Offset = "0x24")]
    	[SerializeField]
    	private LocaleKind _locale;

    	[Token(Token = "0x40007E4")]
    	[FieldOffset(Offset = "0x28")]
    	[Header("Entry")]
    	[SerializeField]
    	private bool _skipSplashScreen;

    	[Token(Token = "0x40007E5")]
    	[FieldOffset(Offset = "0x29")]
    	[SerializeField]
    	private bool _skipEntrySequenceAtFirstTime;

    	[Token(Token = "0x40007E6")]
    	[FieldOffset(Offset = "0x2A")]
    	[Header("Naninovel")]
    	[SerializeField]
    	private bool _overrideStartGameScenario;

    	[Token(Token = "0x40007E7")]
    	[FieldOffset(Offset = "0x30")]
    	[ResourcePopup("Scripts", "Scripts", null)]
    	[SerializeField]
    	private string _startGameScenario;

    	[Token(Token = "0x40007E8")]
    	[FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private bool _debugSkip;

    	[Token(Token = "0x40007E9")]
    	[FieldOffset(Offset = "0x39")]
    	[SerializeField]
    	private bool _allowContinueDebate;

    	[Token(Token = "0x40007EA")]
    	[FieldOffset(Offset = "0x3A")]
    	[Space]
    	[SerializeField]
    	private bool _checkWitchBookState;

    	[Token(Token = "0x1700013B")]
    	public static bool EnableSteamworks
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3AA230, 77 B
    		// Inverted default: `IsDebugMode` false returns `mov al, 1`, so a release build always enables
    		// Steamworks; only in debug mode does static +0x0 `s_enableSteamworks` decide.
    		[Token(Token = "0x6000771")]
    		[Address(RVA = "0x3AA230", Offset = "0x3A8C30", VA = "0x1803AA230")]
    		get
    		{
    			return !IsDebugMode || s_enableSteamworks;
    		}
    	}

    	[Token(Token = "0x1700013C")]
    	public static LocaleKind? Locale
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3AA280, 140 B
    		// `IsDebugMode` and static +0x1 `s_overrideLocale` -> `new LocaleKind?(s_locale (+0x4))`; else a
    		// zeroed Nullable (null).
    		[Token(Token = "0x6000772")]
    		[Address(RVA = "0x3AA280", Offset = "0x3A8C80", VA = "0x1803AA280")]
    		get
    		{
    			return IsDebugMode && s_overrideLocale ? new LocaleKind?(s_locale) : null;
    		}
    	}

    	[Token(Token = "0x1700013D")]
    	public static bool SkipSplashScreen
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3AA310, 76 B
    		// `IsDebugMode`, then static +0x8 `s_skipSplashScreen`. Same shape as `DebugSkip`.
    		[Token(Token = "0x6000773")]
    		[Address(RVA = "0x3AA310", Offset = "0x3A8D10", VA = "0x1803AA310")]
    		get
    		{
    			return IsDebugMode && s_skipSplashScreen;
    		}
    	}

    	[Token(Token = "0x1700013E")]
    	public static bool SkipEntrySequenceAtFirstTime
    	{
    		// RECONSTRUCTED | Confidence: A (behavior) | RVA: 0x3AA360, 3 B
    		// The shipped getter is the universal 3-byte `xor eax, eax; ret` stub shared with dozens of BCL
    		// `return false` members, so it always returns false and never reads `s_skipEntrySequenceAtFirstTime`.
    		// The field is still written by UpdateStaticFields. The source very likely had a conditional-compilation
    		// or constant-false guard, but no evidence of its form survives.
    		[Token(Token = "0x6000774")]
    		[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360")]
    		get
    		{
    			return false;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3AA370, 84 B
    	// `IsDebugMode`, then the static byte +0xA `s_overrideStartGameScenario`;
    	// when both are set it returns +0x10 `s_startGameScenario`, otherwise null.
    	[Token(Token = "0x1700013F")]
    	public static string StartGameScenario
    	{
    		[Token(Token = "0x6000775")]
    		[Address(RVA = "0x3AA370", Offset = "0x3A8D70", VA = "0x1803AA370")]
    		get
    		{
    			return IsDebugMode && s_overrideStartGameScenario ? s_startGameScenario : null;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3AA3D0, 76 B
    	// `DebugOptionsBase.get_IsDebugMode`; if false its result is returned,
    	// otherwise the DebugOptions static byte +0x18 = `s_debugSkip`. Same shape as
    	// `AllowContinueDebate`.
    	[Token(Token = "0x17000140")]
    	public static bool DebugSkip
    	{
    		[Token(Token = "0x6000776")]
    		[Address(RVA = "0x3AA3D0", Offset = "0x3A8DD0", VA = "0x1803AA3D0")]
    		get
    		{
    			return IsDebugMode && s_debugSkip;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3AA420, 76 B
    	// Calls the static `DebugOptionsBase.get_IsDebugMode`; when it is false,
    	// returns its (false) result directly; otherwise returns the byte at
    	// DebugOptions static-fields +0x19 = `s_allowContinueDebate`.
    	[Token(Token = "0x17000141")]
    	public static bool AllowContinueDebate
    	{
    		[Token(Token = "0x6000777")]
    		[Address(RVA = "0x3AA420", Offset = "0x3A8E20", VA = "0x1803AA420")]
    		get
    		{
    			return IsDebugMode && s_allowContinueDebate;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3AA470, 76 B
    	// `IsDebugMode`, then the static byte +0x1A `s_checkWitchBookState`.
    	[Token(Token = "0x17000142")]
    	public static bool CheckWitchBookState
    	{
    		[Token(Token = "0x6000778")]
    		[Address(RVA = "0x3AA470", Offset = "0x3A8E70", VA = "0x1803AA470")]
    		get
    		{
    			return IsDebugMode && s_checkWitchBookState;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3AA4C0, 344 B
    	// 
    	// Ten straight copies from the serialized instance fields (+0x20..+0x3A) to the statics (+0x0..+0x1A),
    	// in declaration order. The string store has a write barrier. There is no base call.
    	[Token(Token = "0x6000779")]
    	[Address(RVA = "0x3AA4C0", Offset = "0x3A8EC0", VA = "0x1803AA4C0", Slot = "4")]
    	protected override void UpdateStaticFields()
    	{
    		s_enableSteamworks = _enableSteamworks;
    		s_overrideLocale = _overrideLocale;
    		s_locale = _locale;
    		s_skipSplashScreen = _skipSplashScreen;
    		s_skipEntrySequenceAtFirstTime = _skipEntrySequenceAtFirstTime;
    		s_overrideStartGameScenario = _overrideStartGameScenario;
    		s_startGameScenario = _startGameScenario;
    		s_debugSkip = _debugSkip;
    		s_allowContinueDebate = _allowContinueDebate;
    		s_checkWitchBookState = _checkWitchBookState;
    	}

    	[Token(Token = "0x600077A")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DebugOptions()
    	{
    	}
    }
}
