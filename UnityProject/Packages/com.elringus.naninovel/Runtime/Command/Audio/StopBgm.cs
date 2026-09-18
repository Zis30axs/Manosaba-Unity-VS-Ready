namespace Naninovel.Commands
{
    [Doc(
        @"
Stops playing a BGM (background music) track with the specified name.",
        @"
When music track name (BgmPath) is not specified, will stop all the currently played tracks.",
        @"
; Fades-out 'Sanctuary' bgm track over 10 seconds and stops the playback.
@stopBgm Sanctuary fade:10",
        @"
; Stops all the currently played music tracks.
@stopBgm"
    )]
    public class StopBgm : AudioCommand
    {
        [Doc("Path to the music track to stop.")]
        [ParameterAlias(NamelessParameterAlias), ResourceContext(AudioConfiguration.DefaultAudioPathPrefix)]
        public StringParameter BgmPath;
        [Doc("Duration of the volume fade-out before stopping playback, in seconds (0.35 by default).")]
        [ParameterAlias("fade"), ParameterDefaultValue("0.35")]
        public DecimalParameter FadeOutDuration;
        [Doc("Whether to wait for the BGM fade-out animation to finish before playing next command.")]
        public BooleanParameter Wait;

        public override UniTask Execute (AsyncToken token = default)
        {
            return WaitOrForget(Stop, Wait, token);
        }

        protected virtual async UniTask Stop (AsyncToken token)
        {
            var duration = Assigned(FadeOutDuration) ? FadeOutDuration.Value : AudioManager.Configuration.DefaultFadeDuration;
            if (Assigned(BgmPath)) await AudioManager.StopBgm(BgmPath, duration, token);
            else await AudioManager.StopAllBgm(duration, token);
        }
    }
}
