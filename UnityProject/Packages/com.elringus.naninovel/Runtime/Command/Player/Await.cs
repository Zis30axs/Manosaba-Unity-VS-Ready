using Naninovel.Metadata;

namespace Naninovel.Commands
{
    [Doc(
        @"
Holds script execution until all the nested async commands finished execution.
Useful for grouping multiple async commands to wait until they all finish
before proceeding with the script playback.",
        @"
The nested block is expected to always finish; don't nest any commands that could
navigate outside the nested block, as this may cause undefined behaviour.",
        @"
; Run nested lines in parallel and wait until they all are finished.
@await
    @back RainyScene
    @bgm RainAmbient
    @camera zoom:0.5 time:3
    @print ""It starts Raining..."" !waitInput
; Following line will execute after all the above is finished.
..."
    )]
    [RequireNested, Branch(BranchTraits.Nest | BranchTraits.Return)]
    public class Await : Command, Command.INestedHost
    {
        private bool initial;

        public virtual int GetNextPlaybackIndex (ScriptPlaylist playlist, int playedIndex)
        {
            if (playlist.IsEnteringNestedAt(playedIndex))
                return initial
                    ? playedIndex + 1
                    : playlist.SkipNestedAt(playedIndex, Indent);

            if (playlist.IsExitingNestedAt(playedIndex, Indent))
                return initial
                    ? playlist.IndexOf(this)
                    : playlist.ExitNestedAt(playedIndex, Indent);

            return playedIndex + 1;
        }

        public override async UniTask Execute (AsyncToken token = default)
        {
            var player = Engine.GetServiceOrErr<IScriptPlayer>();

            if (!initial)
            {
                initial = true;
                return;
            }

            try
            {
                while (player.ExecutingCommands.Count > 1 && token.EnsureNotCanceledOrCompleted())
                    await AsyncUtils.WaitEndOfFrame(token);
            }
            finally
            {
                initial = false;
                if (!token.Canceled && token.Completed)
                    player.Complete().Forget();
            }
        }
    }
}
