using Naninovel.Metadata;

namespace Naninovel.Commands
{
    [Doc(
        @"
Allows grouping commands inside nested block.",
        null,
        @"
; Random command chooses one of the nested lines, but ignores children
; of its nested lines. Group command used here to group multiple lines,
; so that random command will actually execute multiple lines.
@random
    @group
        @back tint:red
        Paint it red.
    @group
        @back tint:black
        Paint it black."
    )]
    [RequireNested, Branch(BranchTraits.Nest | BranchTraits.Return)]
    public class Group : Command, Command.INestedHost
    {
        public virtual int GetNextPlaybackIndex (ScriptPlaylist playlist, int playedIndex)
        {
            if (playlist.IsEnteringNestedAt(playedIndex))
                return ShouldExecute ? playedIndex + 1 : playlist.SkipNestedAt(playedIndex, Indent);
            if (playlist.IsExitingNestedAt(playedIndex, Indent))
                return playlist.ExitNestedAt(playedIndex, Indent);
            return playedIndex + 1;
        }

        public override UniTask Execute (AsyncToken token = default)
        {
            return UniTask.CompletedTask;
        }
    }
}
