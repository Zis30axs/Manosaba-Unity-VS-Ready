using UnityEngine.Video;
using UnityEngine.Audio;
using System.Linq;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Movies
{
    [Token(Token = "0x2000004")]
    [UsedImplicitly]
    [InitializeAtRuntime(0, typeof(MoviePlayer))]
    public class MoviePlayerExtended : MoviePlayer
    {
    	[Token(Token = "0x4000006")]
    	[FieldOffset(Offset = "0x80")]
    	private readonly IAudioManager _naniAudioManager;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x46D4E0, 8 B (folded)
    	// Get-only auto-property: the trivial `[CompilerGenerated]` read of the backing field
    	// `<Configuration>k__BackingField` at +0x88 (dump.cs); the RVA is shared with dozens of
    	// unrelated trivial getters. `get;` instead of the stub `return null;` is required so the
    	// recovered constructor can assign the backing field.
    	[Token(Token = "0x17000001")]
    	public new MoviesConfigurationExtended Configuration
    	{
    		[Token(Token = "0x6000003")]
    		[Address(RVA = "0x46D4E0", Offset = "0x46BEE0", VA = "0x18046D4E0")]
    		[CompilerGenerated]
    		get;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD33040, 671 B
    	// ABI: rcx=__this, rdx=configExtended, r8=config, r9=resources; 5th (l10n) = [rsp+0x28],
    	// 6th (input) = [rsp+0x30], 7th (audio) = [rsp+0x38] (the prologue only saves registers).
    	// There is no `call MoviePlayer..ctor`: MSVC inlined the base constructor. The five stores
    	// it performs land exactly on the base MoviePlayer fields of dump.cs:
    	// +0x20 <Configuration>k__BackingField = config, +0x48 resources, +0x50 l10n, +0x40 input,
    	// +0x58 audio == base(config, resources, l10n, input, audio).
    	// The body then assigns, in this order: +0x88 <Configuration>k__BackingField = configExtended,
    	// +0x80 _naniAudioManager = audio ([rsp+0x38]). No field initializer of this class is emitted.
    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xD33040", Offset = "0xD31A40", VA = "0x180D33040")]
    	public MoviePlayerExtended(MoviesConfigurationExtended configExtended, MoviesConfiguration config, IResourceProviderManager resources, ILocalizationManager l10n, IInputManager input, IAudioManager audio)
    		: base(config, resources, l10n, input, audio)
    	{
    		Configuration = configExtended;
    		_naniAudioManager = audio;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD332E0, 338 B (vtable slot 22)
    	// - The stub returned default(UniTask): MoviePlayer.InitializeService never ran, `videoLoader` stayed null and the first
    	//   PlayMovie.PreloadResources of the story (MoviePlayer.HoldResources -> LoadOrErr) threw an NRE (G4b round 21, Adv03 -> next script).
    	// - base.InitializeService() (0x1805966D0) first; its UniTask is returned at the end.
    	// - Player (vtable +0x268, slot 19, protected virtual): set_controlledAudioTrackCount(1) (0x1824D3150), set_audioOutputMode
    	//   (icall setter, value 1 = VideoAudioOutputMode.AudioSource), SetTargetAudioSource(0, AudioSource) with the AudioSource
    	//   property (vtable +0x278, slot 20) at 0x1824D3430.
    	// Annotated disassembly: work/title-path/disas/mpe_01_InitializeService.txt
    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xD332E0", Offset = "0xD31CE0", VA = "0x180D332E0", Slot = "22")]
    	public override UniTask InitializeService()
    	{
    		UniTask result = base.InitializeService();
    		Player.controlledAudioTrackCount = 1;
    		Player.audioOutputMode = VideoAudioOutputMode.AudioSource;
    		Player.SetTargetAudioSource(0, AudioSource);
    		return result;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD33440, 534 B (vtable slot 30)
    	// - MoviePlayer.SetupAudioSource with two differences: the source volume is set from the extended configuration
    	//   (+0x88 Configuration, MoviesConfigurationExtended.MovieVolume at +0x20) and the mixer group is looked up by
    	//   Configuration.AudioMixerGroup (+0x18) instead of "Master".
    	// - Player.gameObject.AddComponent<AudioSource>() (0x1806B68F0), playOnAwake false, bypassReverbZones and bypassEffects true.
    	// - `_naniAudioManager` (+0x80) AudioMixer (interface call), tested with UnityEngine.Object's implicit bool (m_CachedPtr at +0x10),
    	//   then FindMatchingGroups (0x181FCB4C0) and Enumerable.FirstOrDefault (0x180686B90).
    	// Annotated disassembly: work/title-path/disas/mpe_02_SetupAudioSource.txt
    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0xD33440", Offset = "0xD31E40", VA = "0x180D33440", Slot = "30")]
    	protected override AudioSource SetupAudioSource()
    	{
    		AudioSource source = Player.gameObject.AddComponent<AudioSource>();
    		source.playOnAwake = false;
    		source.bypassReverbZones = true;
    		source.bypassEffects = true;
    		source.volume = Configuration.MovieVolume;
    		if (_naniAudioManager.AudioMixer)
    		{
    			AudioMixerGroup[] matchingGroups = _naniAudioManager.AudioMixer.FindMatchingGroups(Configuration.AudioMixerGroup);
    			AudioMixerGroup outputGroup = null;
    			// 0x180D33614: the shipped binary tests the returned array for null before FirstOrDefault (the stock code's `?.`)
    			if (matchingGroups != null)
    			{
    				outputGroup = matchingGroups.FirstOrDefault();
    			}
    			source.outputAudioMixerGroup = outputGroup;
    		}
    		return source;
    	}
    }
}
