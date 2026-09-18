using Naninovel.Metadata;

namespace Naninovel.Commands
{
    [Doc(
        @"
Marks the beginning of a conditional execution block.
Nested lines are considered body of the block and will be executed only in case
the conditional nameless parameter is evaluated to true.
See [conditional execution](/guide/naninovel-scripts#conditional-execution) guide for more info.",
        null,
        @"
; Print text line(s) depending on ""score"" variable:
;   ""You've failed. Try again!"" - when score is below 6.
;   ""You've passed the test."" and ""Brilliant!"" - when score is above 8.
;   ""You've passed the test."" and ""Impressive!"" - when score is above 7.
;   ""You've passed the test."" and ""Good job!"" - otherwise.
@if score>6
    You've passed the test.
    @if score>8
        Brilliant!
    @else if:score>7
        Impressive!
    @else
        Good job!
@else
    You've failed. Try again!",
        @"
; Print text line depending on ""score"" variable:
;   ""Test result: Failed."" - when score is below 6.
;   ""Test result: Perfect!"" - when score is above above 8.
;   ""Test result: Passed."" - otherwise.
Test result:[if score>8] Perfect![else if:score>6] Passed.[else] Failed.[endif]"
    )]
    [CommandAlias("if"), Branch(BranchTraits.Nest | BranchTraits.Return | BranchTraits.Switch)]
    [IgnoreParameter(nameof(ConditionalExpression))]
    public class BeginIf : Command, Command.INestedHost
    {
        [Doc("A [script expression](/guide/script-expressions), which should return a boolean value " +
             "determining whether the associated nested block will be executed.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter, ConditionContext]
        public StringParameter Expression;

        public override bool ShouldExecute => true;

        public virtual int GetNextPlaybackIndex (ScriptPlaylist playlist, int playedIndex)
        {
            if (playlist.IsExitingNestedAt(playedIndex, Indent))
                return playlist.ExitNestedAt(playedIndex, Indent);
            return playedIndex + 1;
        }

        public override UniTask Execute (AsyncToken token = default)
        {
            if (Assigned(ConditionalExpression))
                Warn("Parameter 'if' in '@if' command is ignored; use nameless parameter for the condition instead.");

            if (ExpressionEvaluator.Evaluate<bool>(Expression, Err))
                return UniTask.CompletedTask;

            HandleConditionalBlock(this);

            return UniTask.CompletedTask;
        }

        /// <summary>
        /// When invoked while entering a conditional block, will navigate the playback to the appropriate command.
        /// </summary>
        public static void HandleConditionalBlock (Command ifOrElse)
        {
            var player = Engine.GetServiceOrErr<IScriptPlayer>();
            var index = player.IsEnteringNested() ?
                GetNextIndexWithNested(player, ifOrElse) :
                GetNextIndexWithEndIf(player, ifOrElse is Else);
            if (!player.Playlist.IsIndexValid(index))
                throw new Error(Engine.FormatMessage("Conditional block under '@if' command doesn't end with a playable command; playback will be stopped.", ifOrElse.PlaybackSpot));
            player.Resume(index);
        }

        private static int GetNextIndexWithNested (IScriptPlayer player, Command ifOrElse)
        {
            // When invoked from else, always exit (inner else is entered when this is invoked from if).
            var exit = ifOrElse is Else;
            var index = player.Playlist.FindIndexAfter(player.PlayedIndex, cmd =>
                cmd.Indent <= ifOrElse.Indent &&
                (!exit && IsTruthyElse(cmd) || !exit && cmd is not Else || exit));
            if (!exit && player.Playlist.GetCommandByIndex(index) is Else) // ^ entering truthy else here.
                return index + 1;
            if (player.Playlist.GetCommandByIndex(index - 1) is Else el) // exiting out of nested into outer conditional
                return GetNextIndexWithNested(player, el);
            return player.Playlist.MoveAt(index - 1);
        }

        private static int GetNextIndexWithEndIf (IScriptPlayer player, bool gettingOut)
        {
            var depth = 0; // Depth of the conditional block (changes upon getting in our out of the nested if blocks).
            for (var i = player.PlayedIndex + 1; i < player.Playlist.Count; i++)
            {
                var command = player.Playlist[i];

                if (command is BeginIf)
                {
                    depth++;
                    continue;
                }
                if (depth != 0 && command is EndIf)
                {
                    depth--;
                    continue;
                }
                if (depth != 0) continue;

                if (command is EndIf || (!gettingOut && IsTruthyElse(command)))
                    return i + 1;
            }
            return -1;
        }

        private static bool IsTruthyElse (Command cmd)
        {
            if (cmd is not Else el) return false;
            if (!Assigned(el.ConditionalExpression)) return true;
            return ExpressionEvaluator.Evaluate<bool>(el.ConditionalExpression, el.Err);
        }
    }
}
