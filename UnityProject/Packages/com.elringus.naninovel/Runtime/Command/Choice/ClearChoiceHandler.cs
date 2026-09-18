namespace Naninovel.Commands
{
    [Doc(
        @"
Removes all the choice options in the choice handler with the specified ID (or in default one, when ID is not specified;
or in all the existing handlers, when `*` is specified as ID) and (optionally) hides it (them).",
        null,
        @"
; Give the player 2 seconds to pick a choice.
# Start
You have 2 seconds to respond![< skip!]
@choice Cats goto:.PickedChoice
@choice Dogs goto:.PickedChoice
@wait 2
@clearChoice
Too late!
@stop
# PickedChoice
Good!"
    )]
    [CommandAlias("clearChoice")]
    public class ClearChoiceHandler : Command
    {
        [Doc("ID of the choice handler to clear. Will use a default handler if not specified. " +
             "Specify `*` to clear all the existing handlers.")]
        [ParameterAlias(NamelessParameterAlias), ActorContext(ChoiceHandlersConfiguration.DefaultPathPrefix)]
        public StringParameter HandlerId;
        [Doc("Whether to also hide the affected choice handlers.")]
        [ParameterDefaultValue("true")]
        public BooleanParameter Hide = true;

        public override UniTask Execute (AsyncToken token = default)
        {
            var choiceManager = Engine.GetServiceOrErr<IChoiceHandlerManager>();

            if (Assigned(HandlerId) && HandlerId == "*")
            {
                foreach (var handler in choiceManager.Actors)
                {
                    RemoveAllChoices(handler);
                    if (Hide) handler.Visible = false;
                }
                return UniTask.CompletedTask;
            }

            var handlerId = Assigned(HandlerId) ? HandlerId.Value : choiceManager.Configuration.DefaultHandlerId;
            if (!choiceManager.ActorExists(handlerId))
            {
                Warn($"Failed to clear `{handlerId}` choice handler: handler actor with the specified ID doesn't exist.");
                return UniTask.CompletedTask;
            }

            var choiceHandler = choiceManager.GetActor(handlerId);
            RemoveAllChoices(choiceHandler);
            if (Hide) choiceHandler.Visible = false;
            return UniTask.CompletedTask;
        }

        private static void RemoveAllChoices (IChoiceHandlerActor handler)
        {
            using var _ = ListPool<ChoiceState>.Rent(out var choices);
            choices.ReplaceWith(handler.Choices);
            foreach (var choice in choices)
                handler.RemoveChoice(choice.Id);
        }
    }
}
