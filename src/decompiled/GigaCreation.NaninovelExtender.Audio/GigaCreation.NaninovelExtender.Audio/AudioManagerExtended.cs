using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Audio;

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

	[Token(Token = "0x17000001")]
	public new AudioConfigurationExtended Configuration
	{
		[Token(Token = "0x6000004")]
		[Address(RVA = "0x4717B0", Offset = "0x4701B0", VA = "0x1804717B0", Slot = "99")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000005")]
	[Address(RVA = "0xCF45B0", Offset = "0xCF2FB0", VA = "0x180CF45B0")]
	public AudioManagerExtended(AudioConfigurationExtended configExtended, AudioConfiguration config, IResourceProviderManager resources, ILocalizationManager l10n, ICharacterManager chars)
	{
	}

	[Token(Token = "0x6000006")]
	[Address(RVA = "0xCF4640", Offset = "0xCF3040", VA = "0x180CF4640", Slot = "63")]
	public override UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000007")]
	[Address(RVA = "0xCF4980", Offset = "0xCF3380", VA = "0x180CF4980", Slot = "98")]
	public void PlayResponseSfx(ResponseKind responseKind)
	{
	}

	[Token(Token = "0x6000008")]
	[Address(RVA = "0xCF4BA0", Offset = "0xCF35A0", VA = "0x180CF4BA0", Slot = "84")]
	public override UniTask PlayVoice(string path, float volume = 1f, string group = default, string authorId = default, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000009")]
	[Address(RVA = "0xCF4C60", Offset = "0xCF3660", VA = "0x180CF4C60", Slot = "88")]
	public override void StopVoice()
	{
	}
}
