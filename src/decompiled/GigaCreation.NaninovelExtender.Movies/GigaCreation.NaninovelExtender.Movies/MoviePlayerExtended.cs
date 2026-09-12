using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Movies;

[Token(Token = "0x2000004")]
[UsedImplicitly]
[InitializeAtRuntime(0, typeof(MoviePlayer))]
public class MoviePlayerExtended : MoviePlayer
{
	[Token(Token = "0x4000006")]
	[FieldOffset(Offset = "0x80")]
	private readonly IAudioManager _naniAudioManager;

	[Token(Token = "0x17000001")]
	public new MoviesConfigurationExtended Configuration
	{
		[Token(Token = "0x6000003")]
		[Address(RVA = "0x46D4E0", Offset = "0x46BEE0", VA = "0x18046D4E0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000004")]
	[Address(RVA = "0xD33040", Offset = "0xD31A40", VA = "0x180D33040")]
	public MoviePlayerExtended(MoviesConfigurationExtended configExtended, MoviesConfiguration config, IResourceProviderManager resources, ILocalizationManager l10n, IInputManager input, IAudioManager audio)
	{
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
