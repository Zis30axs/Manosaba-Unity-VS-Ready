namespace Naninovel.Commands
{
    [Doc(
        @"
Stops playing an SFX (sound effect) track with the specified name.",
        @"
When sound effect track name (SfxPath) is not specified, will stop all the currently played tracks.",
        @"
; Stop playing an SFX with the name 'Rain', fading-out for 15 seconds.
@stopSfx Rain fade:15",
        @"
; Stops all the currently played sound effect tracks.
@stopSfx"
    )]
    public class StopSfx : AudioCommand
    {
        [Doc("Path to the sound effect to stop.")]
        [ParameterAlias(NamelessParameterAlias), ResourceContext(AudioConfiguration.DefaultAudioPathPrefix)]
        public StringParameter SfxPath;
        [Doc("Duration of the volume fade-out before stopping playback, in seconds (0.35 by default).")]
        [ParameterAlias("fade"), ParameterDefaultValue("0.35")]
        public DecimalParameter FadeOutDuration;
        [Doc("Whether to wait for the SFX fade-out animation to finish before playing next command.")]
        public BooleanParameter Wait;

        public override UniTask Execute (AsyncToken token = default)
        {
            return WaitOrForget(Stop, Wait, token);
        }

        protected virtual async UniTask Stop (AsyncToken token)
        {
            var duration = Assigned(FadeOutDuration) ? FadeOutDuration.Value : AudioManager.Configuration.DefaultFadeDuration;
            if (Assigned(SfxPath)) await AudioManager.StopSfx(SfxPath, duration, token);
            else await AudioManager.StopAllSfx(duration, token);
        }
    }
}
