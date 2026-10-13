using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Audio
{
    [Token(Token = "0x2000005")]
    [UsedImplicitly]
    [InitializeAtRuntime(0, typeof(AudioManager))]
    public class AudioManagerExtended : AudioManager, IAudioManagerExtended, IEngineService<AudioConfigurationExtended>, IEngineService
    {
    	[Token(Token = "0x4000007")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
    	private IDictionary<ResponseKind, string> _defaultSfxPaths;

    	[Token(Token = "0x4000008")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
    	private bool _allowOverlap;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x4717B0, 8 B (folded)
    	// Get-only auto-property: the getter is the trivial `[CompilerGenerated]` field read of
    	// the backing field `<Configuration>k__BackingField` at +0xB0 (dump.cs). RVA 0x4717B0 is
    	// shared with dozens of unrelated trivial getters (identical-code folding), so disas.py
    	// lists many managed methods for it. The stub body `return null;` is replaced by `get;`
    	// because the recovered constructor below has to assign the backing field.
    	[Token(Token = "0x17000001")]
    	public new AudioConfigurationExtended Configuration
    	{
    		[Token(Token = "0x6000004")]
    		[Address(RVA = "0x4717B0", Offset = "0x4701B0", VA = "0x1804717B0", Slot = "99")]
    		[CompilerGenerated]
    		get;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF45B0, 144 B
    	// ABI: rcx=__this, rdx=configExtended, r8=config, r9=resources; the 5th argument (l10n)
    	// is [rsp+0x60] and the 6th (chars) [rsp+0x68] after `push rdi` + `sub rsp,0x30`.
    	// 1. r10=resources, r11=config, rbx=configExtended (kept for step 3).
    	// 2. r9=l10n ([rsp+0x60]), r8=r10=resources, rdx=r11=config, chars -> [rsp+0x20],
    	//    [rsp+0x28]=0 (MethodInfo* is the last parameter)
    	//    -> `call 0x180478490 Naninovel.AudioManager$$.ctor` == base(config, resources, l10n, chars).
    	// 3. `mov [rdi+0xB0], rbx` (+ GC card-table write barrier) -> this.<Configuration>
    	//    k__BackingField (+0xB0) = configExtended.
    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xCF45B0", Offset = "0xCF2FB0", VA = "0x180CF45B0")]
    	public AudioManagerExtended(AudioConfigurationExtended configExtended, AudioConfiguration config, IResourceProviderManager resources, ILocalizationManager l10n, ICharacterManager chars)
    		: base(config, resources, l10n, chars)
    	{
    		Configuration = configExtended;
    	}

    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0xCF4640", Offset = "0xCF3040", VA = "0x180CF4640", Slot = "63")]
    	public override UniTask InitializeService()
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCF4640, 832 B (0x340)
    		//
    		// The retained stub here was not cosmetic and not merely incomplete: `AudioManager.InitializeService`
    		// is where `audioLoader`, `voiceLoader`, the mixer and its groups are created, so an override that
    		// never calls it leaves the manager half-built.  The measured consequence, in the standalone Player:
    		// `AudioManager.LoadServiceState(SettingsStateMap)` writes `VoiceLocale`, whose setter is
    		// `voiceLoader.OverrideLocale = value` (AudioManager.cs:64) -> NullReferenceException on the null
    		// loader -> `LoadAllServicesFromStateAsync` faults -> `StateManager.PerformPostEngineInitializationTasks`
    		// faults -> `Engine.Initialize` faults -> `initializeTCS` is never signalled; and because
    		// `RuntimeInitializer` polls that task with `IsCompleted` alone (RuntimeInitializer.cs:93) the exception
    		// is never observed, so the engine simply reports `Initializing = true` for ever while the game plays on.
    		// Probe evidence for that chain (tools/unity-rebuild/out/unity/player_game_start_evidence.json):
    		//   post_init_tasks        = 3 [StateManagerExtended.PerformPostEngineInitializationTasks,
    		//                               CustomVariableManager.InitializeLocalVariables,
    		//                               UiManagerExtended.InstantiateUIs]
    		//   engine_settings_state  = SettingsStateMap   (the engine's slot load had returned)
    		//   engine_global_state    = null               (the loop before it never finished)
    		//   LoadServiceState(SettingsStateMap): LocalizationManagerExtended=COMPLETED, VersioningManager=COMPLETED,
    		//     WitchTrialsScriptPlayer=COMPLETED, TextPrinterManager=COMPLETED, AudioManagerExtended=THREW,
    		//     CameraManagerExtended=COMPLETED, UiManagerExtended=COMPLETED, WitchTrialsChoiceHandlerManager=COMPLETED
    		//
    		// Recovered sequence -- one statement, then a TAIL CALL to the base:
    		//   0x180CF46BF  rax = [rsi+0xB0]        -> this.<Configuration>k__BackingField, the *extended*
    		//                                          configuration (assigned by the recovered ctor at +0xB0)
    		//   0x180CF46C9  test rax,rax / je 0x180CF496D (THROW NullReferenceException) -- receiver null check
    		//   0x180CF46F1  r14 = [rax+0x18]        -> AudioConfigurationExtended.ResponseSfxData (dump.cs:10-12
    		//                                          `[FieldOffset(Offset = "0x18")] public ResponseSfx[] ResponseSfxData`)
    		//   0x180CF471A / 0x180CF48B9  the two cached `<>c` lambdas: `<>9__6_0` is built from
    		//                TYPE System.Func<ResponseSfx, ResponseKind>_TypeInfo (the key selector) and
    		//                `<>9__6_1` from TYPE System.Func<ResponseSfx, string>_TypeInfo (the element
    		//                selector); `x => x.ResponseKind` / `x => x.Path` are the only members of those types
    		//                (ResponseSfxBase `_responseKind` +0x18, ResponseSfx `_path` +0x20)
    		//   0x180CF48DC  METHODINFO Enumerable.ToDictionary<ResponseSfx, ResponseKind, string>()
    		//   0x180CF48E8  call 0x18068BAB0        -> that ToDictionary's shared body
    		//   0x180CF4908  mov [rsi+0xA0],rax (+ GC write barrier) -> this._defaultSfxPaths (+0xA0)
    		//   0x180CF4959  xor edx,edx / mov rcx,rsi / epilogue / 0x180CF4968
    		//                `jmp 0x1804789C0` -> **Naninovel.AudioManager$$InitializeService**: a tail call with the
    		//                MethodInfo* slot (rdx) zeroed, i.e. `return base.InitializeService();` rather than
    		//                `await base.InitializeService();` -- an await would need a state machine here, and this
    		//                body hands the base's UniTask straight back in rax.
    		// `_allowOverlap` (+0xA8) is NOT touched by this method, and no other statement exists in these 832 bytes.
    		//
    		// Log: tools/unity-rebuild/out/logs/AudioManagerExtended_InitializeService_0xCF4640.txt
    		_defaultSfxPaths = Configuration.ResponseSfxData.ToDictionary(x => x.ResponseKind, x => x.Path);
    		return base.InitializeService();
    	}

    	[Token(Token = "0x6000007")]
    	[Address(RVA = "0xCF4980", Offset = "0xCF3380", VA = "0x180CF4980", Slot = "98")]
    	public void PlayResponseSfx(ResponseKind responseKind)
    	{
            // RECONSTRUCTED | Confidence: A | RVA: 0xCF4980
            // Resolve the configured response clip, then invoke AudioManager.PlaySfx (slot 81).
            // The original uses volume 1, no fade/loop/group and forgets the returned UniTask.
            if (_defaultSfxPaths.TryGetValue(responseKind, out var path))
                PlaySfx(path, 1f, 0f, false, null, default).Forget();
            else
                UnityEngine.Debug.LogWarning($"No response sound is configured for {responseKind}.");
    	}

    	[Token(Token = "0x6000008")]
    	[Address(RVA = "0xCF4BA0", Offset = "0xCF35A0", VA = "0x180CF4BA0", Slot = "84")]
    	public override UniTask PlayVoice(string path, float volume = 1f, string group = default, string authorId = default, AsyncToken token = default)
    	{
    		return base.PlayVoice(path, volume, group, authorId, token);
    	}

    	[Token(Token = "0x6000009")]
    	[Address(RVA = "0xCF4C60", Offset = "0xCF3660", VA = "0x180CF4C60", Slot = "88")]
    	public override void StopVoice()
    	{
    		base.StopVoice();
    	}
    }
}
