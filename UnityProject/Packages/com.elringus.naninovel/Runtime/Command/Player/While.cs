using Naninovel.Metadata;

namespace Naninovel.Commands
{
    [Doc(
        @"
Executes nested lines in a loop, as long as specified conditional expression resolves to `true`.",
        null,
        @"
; Guess the number game.
@set number=random(1,100);answer=0
@while answer!=number
    @input answer summary:""Guess a number between 1 and 100""
    @stop
    @if answer<number
        Wrong, too low.
    @else if:answer>number
        Wrong, too high.
    @else
        Correct!"
    )]
    [RequireNested, Branch(BranchTraits.Nest | BranchTraits.Return), IgnoreParameter(nameof(ConditionalExpression))]
    public class While : Command, Command.INestedHost
    {
        [Doc("A [script expression](/guide/script-expressions), which should return a boolean value " +
             "determining whether the associated nested block should continue executing in loop.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter, ConditionContext]
        public StringParameter Expression;

        public override bool ShouldExecute => true;

        public virtual int GetNextPlaybackIndex (ScriptPlaylist playlist, int playedIndex)
        {
            if (playlist.IsEnteringNestedAt(playedIndex))
                return ExpressionEvaluator.Evaluate<bool>(Expression, Err)
                    ? playedIndex + 1
                    : playlist.SkipNestedAt(playedIndex, Indent);
            if (playlist.IsExitingNestedAt(playedIndex, Indent))
                return playlist.IndexOf(this);
            return playedIndex + 1;
        }

        public override UniTask Execute (AsyncToken token = default)
        {
            if (Assigned(ConditionalExpression))
                Warn("Parameter 'if' in '@while' command is ignored; use nameless parameter for the condition instead.");
            return UniTask.CompletedTask;
        }
    }
}
