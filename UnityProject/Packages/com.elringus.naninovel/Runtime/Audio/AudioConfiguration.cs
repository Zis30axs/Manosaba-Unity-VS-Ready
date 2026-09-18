using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Naninovel
{
    [EditInProjectSettings]
    public class AudioConfiguration : Configuration
    {
        public const string DefaultAudioPathPrefix = "Audio";
        public const string DefaultVoicePathPrefix = "Voice";

        [Tooltip("Configuration of the resource loader used with audio (BGM and SFX) resources.")]
        public ResourceLoaderConfiguration AudioLoader = new() { PathPrefix = DefaultAudioPathPrefix };
        [Tooltip("Configuration of the resource loader used with voice resources.")]
        public ResourceLoaderConfiguration VoiceLoader = new() { PathPrefix = DefaultVoicePathPrefix };
        [Tooltip(nameof(IAudioPlayer) + " implementation responsible for playing audio clips.")]
        public string AudioPlayer = typeof(AudioPlayer).AssemblyQualifiedName;
        [Range(0f, 1f), Tooltip("Master volume to set when the game is first started.")]
        public float DefaultMasterVolume = 1f;
        [Range(0f, 1f), Tooltip("BGM volume to set when the game is first started.")]
        public float DefaultBgmVolume = 1f;
        [Range(0f, 1f), Tooltip("SFX volume to set when the game is first started.")]
        public float DefaultSfxVolume = 1f;
        [Range(0f, 1f), Tooltip("Voice volume to set when the game is first started.")]
        public float DefaultVoiceVolume = 1f;
        [Tooltip("When enabled, each [@print] command will attempt to play an associated voice clip.")]
        public bool EnableAutoVoicing;
        [Tooltip("Dictates how to handle concurrent voices playback:" +
                 "\n • Allow Overlap — Concurrent voices will be played without limitation." +
                 "\n • Prevent Overlap — Prevent concurrent voices playback by stopping any played voice clip before playing a new one." +
                 "\n • Prevent Character Overlap — Prevent concurrent voices playback per character; voices of different characters (auto voicing) and any number of [@voice] command are allowed to be played concurrently.")]
        public VoiceOverlapPolicy VoiceOverlapPolicy = VoiceOverlapPolicy.PreventOverlap;
        [Tooltip("Assign localization tags to allow selecting voice language in the game settings independently of the main localization.")]
        public List<string> VoiceLocales;
        [Tooltip("Default duration of the volume fade in/out when starting or stopping playing audio.")]
        public float DefaultFadeDuration = .35f;
        [Tooltip("Whether to play non-looped sound effects (SFX) while in skip mode. When disabled, will ignore [@sfx] commands without `loop!` while skipping.")]
        public bool PlaySfxWhileSkipping = true;

        [Header("Audio Mixer")]
        [Tooltip("Audio mixer to control audio groups. When not specified, will use a default one.")]
        public AudioMixer CustomAudioMixer;
        [Tooltip("Path of the mixer's group to control master volume.")]
        public string MasterGroupPath = "Master";
        [Tooltip("Name of the mixer's handle (exposed parameter) to control master volume.")]
        public string MasterVolumeHandleName = "Master Volume";
        [Tooltip("Path of the mixer's group to control volume of background music.")]
        public string BgmGroupPath = "Master/BGM";
        [Tooltip("Name of the mixer's handle (exposed parameter) to control background music volume.")]
        public string BgmVolumeHandleName = "BGM Volume";
        [Tooltip("Path of the mixer's group to control sound effects music volume.")]
        public string SfxGroupPath = "Master/SFX";
        [Tooltip("Name of the mixer's handle (exposed parameter) to control sound effects volume.")]
        public string SfxVolumeHandleName = "SFX Volume";
        [Tooltip("Path of the mixer's group to control voice volume.")]
        public string VoiceGroupPath = "Master/Voice";
        [Tooltip("Name of the mixer's handle (exposed parameter) to control voice volume.")]
        public string VoiceVolumeHandleName = "Voice Volume";
    }
}
