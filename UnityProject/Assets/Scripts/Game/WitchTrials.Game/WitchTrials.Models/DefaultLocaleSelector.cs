using UnityEngine;
using FastEnumUtility;
using System.Text;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Localization;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace WitchTrials.Models
{
    [Token(Token = "0x200016C")]
    public class DefaultLocaleSelector : IDefaultLocaleSelector, IService
    {
    	// RECONSTRUCTED | Confidence: A
    	// Backing fields for the two auto-properties below. dump.cs shows these as
    	// e.g. `<DefaultLocale>k__BackingField`, a metadata-only name that isn't
    	// valid C# source syntax, so ILSpy rendered the properties as get/set method
    	// pairs with no field declaration at all. Offsets confirmed by disassembly.
    	private LocaleKind _defaultLocale;
    	private IList<string> _availableTextLanguages;

    	[Token(Token = "0x200016D")]
    	public class ConfigJson
    	{
    		[Token(Token = "0x40006E9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		[JsonProperty("available_text_lang")]
    		public IList<string> AvailableTextLang;

    		[Token(Token = "0x40006EA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		[JsonProperty("unavailable_lang_fallback")]
    		public IDictionary<string, string> UnavailableLangFallback;

    		[Token(Token = "0x600062E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public ConfigJson()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200016F")]
    	[CompilerGenerated]
    	private struct _003CLoadAsync_003Ed__9
    {
    		[Token(Token = "0x40006EE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40006EF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40006F0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CancellationToken ct;

    		[Token(Token = "0x40006F1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public DefaultLocaleSelector _003C_003E4__this;

    		[Token(Token = "0x40006F2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private string _003Ckey_003E5__2;

    		[Token(Token = "0x40006F3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private string _003CjsonPath_003E5__3;

    		[Token(Token = "0x40006F4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private TaskAwaiter<string> _003C_003Eu__1;

    		[Token(Token = "0x6000633")]
    		[Address(RVA = "0x3982A0", Offset = "0x396CA0", VA = "0x1803982A0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000634")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x170000FD")]
    	public LocaleKind DefaultLocale
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x331650 (shared generic 4-byte getter, +0x10)
    		[Token(Token = "0x6000628")]
    		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return _defaultLocale;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0x397FF0 (shared generic 4-byte setter, +0x10)
    		[Token(Token = "0x6000629")]
    		[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
    		[CompilerGenerated]
    		private set
    		{
    			_defaultLocale = value;
    		}
    	}

    	[Token(Token = "0x170000FE")]
    	public IList<string> AvailableTextLanguages
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (shared generic reference-field getter, +0x18)
    		[Token(Token = "0x600062A")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "5")]
    		[CompilerGenerated]
    		get
    		{
    			return _availableTextLanguages;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0x36C200 (shared generic reference-field
    		// setter with GC write barrier, +0x18)
    		[Token(Token = "0x600062B")]
    		[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
    		[CompilerGenerated]
    		private set
    		{
    			_availableTextLanguages = value;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x398000 (kickoff) / 0x3982A0 (<LoadAsync>d__9.MoveNext, 2638 B)
    	// The body lives in LoadAsyncCore (XxxCore convention; the <LoadAsync>d__9 metadata stays).
    	//   FastEnum.GetValues<LocaleKind>().ToDictionary(<>c.b__9_0, <>c.b__9_1): b__9_0 (0x398250) is ToSystemLanguage
    	//   inlined (0 -> 22 Japanese, 1 -> 10 English, 2 -> 40 ChineseSimplified, 3 -> 41 ChineseTraditional, 4 -> 23 Korean),
    	//   b__9_1 (0x398290) tail-jumps to ToTagName.  key (<key>5__2, +0x28) = GetValueOrDefault(map, Application.systemLanguage)
    	//   with '' when absent (cmovne at 0x180398723); jsonPath (+0x30) = Path.Combine(streamingAssetsPath, "config.json");
    //   desktop reads File.ReadAllTextAsync(jsonPath, Encoding.UTF8, ct); Android reads the same URL through
    //   UnityWebRequest because StreamingAssets is packaged in the APK.
    	//   Text null -> LogError("Failed to load config.json: " + jsonPath).  Otherwise ConfigJson from JsonConvert;
    	//   AvailableTextLanguages (+0x18) = AvailableTextLang; ICollection<string>.Contains(key) (slot 4) -> DefaultLocale (+0x10)
    	//   = key.ToLocaleKind(); else UnavailableLangFallback.TryGetValue(key) then TryGetValue("default") (IDictionary slot 7);
    	//   nothing found -> LogError("The config.json structure is invalid.").  Every call in the body is accounted for: no
    	//   user-level catch, only the compiler's SetException continuation at 0x180398C74.
    	//   The stub left DefaultLocale at Ja, so a fresh install ran in Japanese: the zh-Hans title logo and every title
    	//   button's Label@ZhHans hover sprite (LocalizeCanvasGroup, alpha = locale == ZhHans) stayed hidden.
    	// Annotated disassembly: work/title-path/disas/dls_398000, dls_3982A0, dls_398250, dls_398290.txt
    	[Token(Token = "0x600062C")]
    	[Address(RVA = "0x398000", Offset = "0x396A00", VA = "0x180398000")]
    	[AsyncStateMachine(typeof(_003CLoadAsync_003Ed__9))]
    	public UniTask LoadAsync([Optional] CancellationToken ct)
    	{
    		return LoadAsyncCore(ct);
    	}

    	private async UniTask LoadAsyncCore(CancellationToken ct)
    	{
    		Dictionary<SystemLanguage, string> tagsBySystemLanguage = FastEnum.GetValues<LocaleKind>().ToDictionary((LocaleKind locale) => locale.ToSystemLanguage(), (LocaleKind locale) => locale.ToTagName());
    		string key = tagsBySystemLanguage.GetValueOrDefault(Application.systemLanguage) ?? "";
		string jsonPath = Path.Combine(Application.streamingAssetsPath, "config.json");
		string json;
#if UNITY_ANDROID && !UNITY_EDITOR
		// Android packages StreamingAssets inside the APK.  The resulting jar:file:// URL
		// cannot be opened through System.IO, so use UnityWebRequest for the player build.
		using (UnityWebRequest request = UnityWebRequest.Get(jsonPath))
		{
			await request.SendWebRequest().WithCancellation(ct);
			if (request.result != UnityWebRequest.Result.Success)
			{
				throw new IOException("Failed to load config.json: " + jsonPath + " (" + request.error + ")");
			}
			json = request.downloadHandler.text;
		}
#else
		json = await File.ReadAllTextAsync(jsonPath, Encoding.UTF8, ct);
#endif
    		// 0x1803988C2: the shipped binary tests the file text before parsing it
    		if (json != null)
    		{
    			ConfigJson config = JsonConvert.DeserializeObject<ConfigJson>(json);
    			AvailableTextLanguages = config.AvailableTextLang;
    			// 0x180398969: the shipped binary tests the stored list before asking it for the key
    			if (AvailableTextLanguages != null && config.AvailableTextLang.Contains(key))
    			{
    				DefaultLocale = key.ToLocaleKind();
    				return;
    			}
    			// 0x1803989AA: the shipped binary tests the fallback table before looking the key up in it
    			if (config.UnavailableLangFallback != null)
    			{
    				if (config.UnavailableLangFallback.TryGetValue(key, out string fallbackTag))
    				{
    					DefaultLocale = fallbackTag.ToLocaleKind();
    					return;
    				}
    				if (config.UnavailableLangFallback.TryGetValue("default", out string defaultTag))
    				{
    					DefaultLocale = defaultTag.ToLocaleKind();
    					return;
    				}
    			}
    			UnityEngine.Debug.LogError("The config.json structure is invalid.");
    		}
    		else
    		{
    			UnityEngine.Debug.LogError("Failed to load config.json: " + jsonPath);
    		}
    	}

    	[Token(Token = "0x600062D")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public DefaultLocaleSelector()
    	{
    	}
    }
}
