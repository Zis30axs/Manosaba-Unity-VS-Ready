namespace Naninovel.Commands
{
    [Doc(
        @"
Plays an [SFX (sound effect)](/guide/audio#sound-effects) track with the specified name.
Unlike [@sfx] command, the clip is played with minimum delay and is not serialized with the game state (won't be played after loading a game, even if it was played when saved).
The command can be used to play various transient audio clips, such as UI-related sounds (eg, on button click with [`Play Script` component](/guide/user-interface#play-script-on-unity-event)).",
        null,
        @"
; Plays an SFX with the name 'Click' once.
@sfxFast Click",
        @"
; Same as above, but allow concurrent playbacks of the same clip.
@sfxFast Click !restart"
    )]
    [CommandAlias("sfxFast")]
    public class PlaySfxFast : AudioCommand, Command.IPreloadable
    {
        [Doc("Path to the sound effect asset to play.")]
        [ParameterAlias(NamelessParameterAlias), ResourceContext(AudioConfiguration.DefaultAudioPathPrefix)]
        public StringParameter SfxPath;
        [Doc("Volume of the sound effect.")]
        [ParameterDefaultValue("1")]
        public DecimalParameter Volume = 1f;
        [Doc("Whether to start playing the audio from start in case it's already playing.")]
        [ParameterDefaultValue("true")]
        public BooleanParameter Restart = true;
        [Doc("Whether to allow playing multiple instances of the same clip; has no effect when `restart` is enabled.")]
        [ParameterDefaultValue("true")]
        public BooleanParameter Additive = true;
        [Doc("Audio mixer [group path](https://docs.unity3d.com/ScriptReference/Audio.AudioMixer.FindMatchingGroups) that should be used when playing the audio.")]
        [ParameterAlias("group")]
        public StringParameter GroupPath;
        [Doc(SharedDocs.WaitParameter)]
        public BooleanParameter Wait;

        public async UniTask PreloadResources ()
        {
            if (!Assigned(SfxPath) || SfxPath.DynamicValue) return;
            await AudioManager.AudioLoader.Load(SfxPath, this);
        }

        public void ReleaseResources ()
        {
            if (!Assigned(SfxPath) || SfxPath.DynamicValue) return;
            AudioManager?.AudioLoader?.Release(SfxPath, this);
        }

        public override async UniTask Execute (AsyncToken token = default)
        {
            var path = SfxPath.Value;
            var wait = Assigned(Wait) && Wait.Value;
            await AudioManager.PlaySfxFast(path, Volume, GroupPath, Restart, Additive);
            while (wait && AudioManager.IsSfxPlaying(path))
                await AsyncUtils.WaitEndOfFrame();
        }
    }
}
