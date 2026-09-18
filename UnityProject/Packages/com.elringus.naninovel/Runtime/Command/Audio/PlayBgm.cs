namespace Naninovel.Commands
{
    [Doc(@"
Plays or modifies currently played [BGM (background music)](/guide/audio#background-music) track with the specified name.",
        @"
Music tracks are looped by default.
When music track name (BgmPath) is not specified, will affect all the currently played tracks.
When invoked for a track that is already playing, the playback won't be affected (track won't start playing from the start),
but the specified parameters (volume and whether the track is looped) will be applied.",
        @"
; Starts playing a music track with the name 'Sanctuary' in a loop.
@bgm Sanctuary",
        @"
; Same as above, but fades-in the volume over 10 seconds and plays once.
@bgm Sanctuary fade:10 !loop",
        @"
; Changes volume of all the played music tracks to 50% over 2.5 seconds
; and makes them play in a loop.
@bgm volume:0.5 loop! time:2.5",
        @"
; Plays 'BattleThemeIntro' once, then loops 'BattleThemeMain'.
@bgm BattleThemeMain intro:BattleThemeIntro"
        )]
    [CommandAlias("bgm")]
    public class PlayBgm : AudioCommand, Command.IPreloadable
    {
        [Doc("Path to the music track to play.")]
        [ParameterAlias(NamelessParameterAlias), ResourceContext(AudioConfiguration.DefaultAudioPathPrefix)]
        public StringParameter BgmPath;
        [Doc("Path to the intro music track to play once before the main track (not affected by the loop parameter).")]
        [ParameterAlias("intro"), ResourceContext(AudioConfiguration.DefaultAudioPathPrefix)]
        public StringParameter IntroBgmPath;
        [Doc("Volume of the music track.")]
        [ParameterDefaultValue("1")]
        public DecimalParameter Volume = 1f;
        [Doc("Whether to play the track from beginning when it finishes.")]
        [ParameterDefaultValue("true")]
        public BooleanParameter Loop = true;
        [Doc("Duration of the volume fade-in when starting playback, in seconds (0.0 by default); doesn't have effect when modifying a playing track.")]
        [ParameterAlias("fade"), ParameterDefaultValue("0")]
        public DecimalParameter FadeInDuration = 0f;
        [Doc("Audio mixer [group path](https://docs.unity3d.com/ScriptReference/Audio.AudioMixer.FindMatchingGroups) that should be used when playing the audio.")]
        [ParameterAlias("group")]
        public StringParameter GroupPath;
        [Doc("Duration (in seconds) of the modification.")]
        [ParameterAlias("time"), ParameterDefaultValue("0.35")]
        public DecimalParameter Duration;
        [Doc("Whether to wait for the BGM fade animation to finish before playing next command.")]
        public BooleanParameter Wait;

        public async UniTask PreloadResources ()
        {
            if (!Assigned(BgmPath) || BgmPath.DynamicValue) return;
            await AudioManager.AudioLoader.Load(BgmPath, this);

            if (!Assigned(IntroBgmPath) || IntroBgmPath.DynamicValue) return;
            await AudioManager.AudioLoader.Load(IntroBgmPath, this);
        }

        public void ReleaseResources ()
        {
            if (!Assigned(BgmPath) || BgmPath.DynamicValue) return;
            AudioManager?.AudioLoader?.Release(BgmPath, this);

            if (!Assigned(IntroBgmPath) || IntroBgmPath.DynamicValue) return;
            AudioManager?.AudioLoader?.Release(IntroBgmPath, this);
        }

        public override UniTask Execute (AsyncToken token = default)
        {
            return WaitOrForget(Play, Wait, token);
        }

        protected virtual async UniTask Play (AsyncToken token)
        {
            var duration = Assigned(Duration) ? Duration.Value : AudioManager.Configuration.DefaultFadeDuration;
            if (Assigned(BgmPath)) await PlayOrModifyTrack(AudioManager, BgmPath, Volume, Loop, duration, FadeInDuration, IntroBgmPath, GroupPath, token);
            else
            {
                using var _ = SetPool<string>.Rent(out var paths);
                using var __ = ListPool<UniTask>.Rent(out var tasks);
                AudioManager.GetPlayedBgm(paths);
                foreach (var path in paths)
                    tasks.Add(PlayOrModifyTrack(AudioManager, path, Volume, Loop, duration, FadeInDuration, IntroBgmPath, null, token));
                await UniTask.WhenAll(tasks);
            }
        }

        protected virtual UniTask PlayOrModifyTrack (IAudioManager manager, string path, float volume, bool loop, float time, float fade, string introPath, string group, AsyncToken token)
        {
            if (manager.IsBgmPlaying(path)) return manager.ModifyBgm(path, volume, loop, time, token);
            return manager.PlayBgm(path, volume, fade, loop, introPath, group, token);
        }
    }
}
