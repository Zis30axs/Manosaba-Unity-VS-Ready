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

    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xD332E0", Offset = "0xD31CE0", VA = "0x180D332E0", Slot = "22")]
    	public override UniTask InitializeService()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0xD33440", Offset = "0xD31E40", VA = "0x180D33440", Slot = "30")]
    	protected override AudioSource SetupAudioSource()
    	{
    		return null;
    	}
    }
}
