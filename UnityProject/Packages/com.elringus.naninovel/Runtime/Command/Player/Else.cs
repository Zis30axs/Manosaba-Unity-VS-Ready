using Naninovel.Metadata;

namespace Naninovel.Commands
{
    [Doc(
        @"
Marks a branch of a conditional execution block,
which is executed in case condition of the opening [@if] and preceding [@else] (if any) commands are not met.
For usage examples see [conditional execution](/guide/naninovel-scripts#conditional-execution) guide."
    )]
    [Branch(BranchTraits.Nest | BranchTraits.Return, nameof(BeginIf))]
    public class Else : Command, Command.INestedHost
    {
        public override bool ShouldExecute => true;

        public virtual int GetNextPlaybackIndex (ScriptPlaylist playlist, int playedIndex)
        {
            if (playlist.IsExitingNestedAt(playedIndex, Indent))
                return playlist.ExitNestedAt(playedIndex, Indent);
            return playedIndex + 1;
        }

        public override UniTask Execute (AsyncToken token = default)
        {
            // Opening @if block decides which one of the associated conditional branches is executed and 
            // navigates to the first line under the branch (not this command); so, if we're executing
            // this command, we either just finished executed previous if/else branch or got out of @goto;
            // in any case, we just have to get out of the current conditional block (any command with same
            // or lower indent level, except @else).

            BeginIf.HandleConditionalBlock(this);

            return UniTask.CompletedTask;
        }
    }
}
