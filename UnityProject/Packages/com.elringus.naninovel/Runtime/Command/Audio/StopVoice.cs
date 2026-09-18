namespace Naninovel.Commands
{
    [Doc(
        @"
Stops playback of the currently played voice clip.",
        null,
        @"
; Given a voice is being played, stop it.
@stopVoice"
    )]
    public class StopVoice : AudioCommand
    {
        public override UniTask Execute (AsyncToken token = default)
        {
            AudioManager.StopVoice();
            return UniTask.CompletedTask;
        }
    }
}
