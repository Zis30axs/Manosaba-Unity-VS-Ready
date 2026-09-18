using System.Text;
using Naninovel.Metadata;
using UnityEngine;

namespace Naninovel.Commands
{
    [Doc(
        @"
Adds a [choice](/guide/choices) option to a choice handler with the specified ID (or default one).",
        @"
When nesting commands under the choice, `goto`, `gosub`, `set` and `play` parameters are ignored.",
        @"
; Print the text, then immediately show choices and stop script execution.
Continue executing this script or ...?[< skip!]
@choice ""Continue""
@choice ""Load another script from start"" goto:Another
@choice ""Load another script from \""Label\"" label"" goto:Another.Label
@choice ""Goto to \""Sub\"" subroutine in another script"" gosub:Another.Sub
@stop",
        @"
; You can also set custom variables based on choices.
@choice ""I'm humble, one is enough..."" set:score++
@choice ""Two, please."" set:score=score+2
@choice ""I'll take the entire stock!"" set:karma--;score=999",
        @"
; Play a sound effect and arrange characters when choice is picked.
@choice Arrange
    @sfx Click
    @arrange k.10,y.55",
        @"
; Print a text line corresponding to the picked choice.
@choice ""Ask about color""
    What's your favorite color?
@choice ""Ask about age""
    How old are you?
@choice ""Keep silent""
    ...
@stop",
        @"
; Make choice disabled/locked when 'score' variable is below 10.
@choice ""Secret option"" lock:{score<10}"
    )]
    [CommandAlias("choice"), Branch(BranchTraits.Interactive | BranchTraits.Nest | BranchTraits.Return | BranchTraits.Endpoint)]
    public class AddChoice : Command, Command.ILocalizable, Command.IPreloadable, Command.INestedHost
    {
        [Doc("Text to show for the choice. When the text contain spaces, wrap it in double quotes (`\"`). " +
             "In case you wish to include the double quotes in the text itself, escape them.")]
        [ParameterAlias(NamelessParameterAlias)]
        public LocalizableTextParameter ChoiceSummary;
        [Doc("Whether the choice should be disabled or otherwise not accessible for player to pick; " +
             "see [choice docs](/guide/choices#locked-choice) for more info. Disabled by default.")]
        [ParameterDefaultValue("false")]
        public BooleanParameter Lock = false;
        [Doc("Path (relative to a `Resources` folder) to a [button prefab](/guide/choices#choice-button) representing the choice. " +
             "The prefab should have a `ChoiceHandlerButton` component attached to the root object. " +
             "Will use a default button when not specified.")]
        [ParameterAlias("button")]
        public StringParameter ButtonPath;
        [Doc("Local position of the choice button inside the choice handler (if supported by the handler implementation).")]
        [ParameterAlias("pos"), VectorContext("X,Y")]
        public DecimalListParameter ButtonPosition;
        [Doc("ID of the choice handler to add choice for. Will use a default handler if not specified.")]
        [ParameterAlias("handler"), ActorContext(ChoiceHandlersConfiguration.DefaultPathPrefix)]
        public StringParameter HandlerId;
        [Doc("Path to go when the choice is selected by user; see [@goto] command for the path format. " +
             "Ignored when nesting commands under the choice.")]
        [ParameterAlias("goto"), EndpointContext]
        public NamedStringParameter GotoPath;
        [Doc("Path to a subroutine to go when the choice is selected by user; see [@gosub] command for the path format. " +
             "When `goto` is assigned this parameter will be ignored. Ignored when nesting commands under the choice.")]
        [ParameterAlias("gosub"), EndpointContext]
        public NamedStringParameter GosubPath;
        [Doc("Set expression to execute when the choice is selected by user; see [@set] command for syntax reference. " +
             "Ignored when nesting commands under the choice.")]
        [ParameterAlias("set"), AssignmentContext]
        public StringParameter SetExpression;
        [Doc("Whether to automatically continue playing script from the next line, " +
             "when neither `goto` nor `gosub` parameters are specified. " +
             "Has no effect in case the script is already playing when the choice is processed. " +
             "Ignored when nesting commands under the choice.")]
        [ParameterAlias("play"), ParameterDefaultValue("true")]
        public BooleanParameter AutoPlay = true;
        [Doc("Whether to also show choice handler the choice is added for; enabled by default.")]
        [ParameterAlias("show"), ParameterDefaultValue("true")]
        public BooleanParameter ShowHandler = true;
        [Doc("Duration (in seconds) of the fade-in (reveal) animation.")]
        [ParameterAlias("time"), ParameterDefaultValue("0.35")]
        public DecimalParameter Duration;

        protected IChoiceHandlerManager Handlers => Engine.GetServiceOrErr<IChoiceHandlerManager>();

        public virtual async UniTask PreloadResources ()
        {
            await PreloadStaticTextResources(ChoiceSummary);

            if (Assigned(HandlerId) && !HandlerId.DynamicValue)
            {
                var handlerId = Assigned(HandlerId) ? HandlerId.Value : Handlers.Configuration.DefaultHandlerId;
                await Handlers.GetOrAddActor(handlerId);
            }

            if (Assigned(ButtonPath) && !ButtonPath.DynamicValue)
                await Handlers.ChoiceButtonLoader.Load(ButtonPath, this);
        }

        public virtual void ReleaseResources ()
        {
            ReleaseStaticTextResources(ChoiceSummary);

            if (Assigned(ButtonPath) && !ButtonPath.DynamicValue)
                Handlers.ChoiceButtonLoader.Release(ButtonPath, this);
        }

        public virtual int GetNextPlaybackIndex (ScriptPlaylist playlist, int playedIndex)
        {
            if (playlist.IsEnteringNestedAt(playedIndex))
                // Always skip nested callback; it's executed when (if) the choice is picked by the player.
                return playlist.SkipNestedAt(playedIndex, Indent);

            if (!playlist.IsExitingNestedAt(playedIndex, Indent))
                return playedIndex + 1;

            // Exiting the block: navigate to the spot which was assigned to continue playback when choice was picked.
            var continueAt = Handlers.PopPickedChoice(PlaybackSpot);
            if (!continueAt.Valid)
                throw new Error(Engine.FormatMessage("Choice callback has nowhere to return. Make sure playable line exists after the nested block.", PlaybackSpot));
            if (continueAt.ScriptPath != playlist.ScriptPath)
                throw new Error(Engine.FormatMessage("Choice callback from another script is not supported.", PlaybackSpot));
            return playlist.IndexOf(continueAt);
        }

        public override async UniTask Execute (AsyncToken token = default)
        {
            using var _ = await LoadDynamicTextResources(ChoiceSummary);
            var handler = await GetOrAddHandler(token);
            if (!handler.Visible && ShowHandler)
                ShowHandlerActor(handler, token).Forget();
            var choice = CreateChoice();
            handler.AddChoice(choice);
        }

        protected virtual async UniTask<IChoiceHandlerActor> GetOrAddHandler (AsyncToken token)
        {
            var handlerId = Assigned(HandlerId) ? HandlerId.Value : Handlers.Configuration.DefaultHandlerId;
            var handler = await Handlers.GetOrAddActor(handlerId);
            token.ThrowIfCanceled();
            return handler;
        }

        protected virtual UniTask ShowHandlerActor (IChoiceHandlerActor handler, AsyncToken token)
        {
            var duration = Assigned(Duration) ? Duration.Value : Handlers.Configuration.DefaultDuration;
            return handler.ChangeVisibility(true, new(duration), token: token);
        }

        protected virtual ChoiceState CreateChoice ()
        {
            var nested = Engine.GetServiceOrErr<IScriptPlayer>().IsEnteringNested();
            var builder = new StringBuilder();

            if (nested)
            {
                if (Assigned(GotoPath) || Assigned(GosubPath) || Assigned(SetExpression) || !AutoPlay)
                    Warn("Using goto, gosub, set and play parameters with nested commands in '@choice' is not supported. Parameters will be ignored.");
            }
            else
            {
                if (Assigned(SetExpression))
                    builder.AppendLine($"{Compiler.Syntax.CommandLine}{nameof(SetCustomVariable)} {SetExpression}");
                if (Assigned(GotoPath))
                    builder.AppendLine($"{Compiler.Syntax.CommandLine}{nameof(Goto)} {GotoPath.Name ?? string.Empty}{(GotoPath.NamedValue.HasValue ? $".{GotoPath.NamedValue.Value}" : string.Empty)}");
                else if (Assigned(GosubPath))
                    builder.AppendLine($"{Compiler.Syntax.CommandLine}{nameof(Gosub)} {GosubPath.Name ?? string.Empty}{(GosubPath.NamedValue.HasValue ? $".{GosubPath.NamedValue.Value}" : string.Empty)}");
            }

            var onSelectScript = builder.ToString().TrimFull();
            var buttonPos = Assigned(ButtonPosition) ? (Vector2?)ArrayUtils.ToVector2(ButtonPosition) : null;
            var autoPlay = AutoPlay && !Assigned(GotoPath) && !Assigned(GosubPath);

            return new(PlaybackSpot, nested, ChoiceSummary, Lock, ButtonPath, buttonPos, onSelectScript, autoPlay);
        }
    }
}
