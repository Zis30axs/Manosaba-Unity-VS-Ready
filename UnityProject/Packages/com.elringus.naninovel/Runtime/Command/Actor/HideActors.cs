using System.Linq;

namespace Naninovel.Commands
{
    [Doc(
        @"
Hides actors (character, background, text printer, choice handler) with the specified IDs.
In case multiple actors with the same ID found (eg, a character and a printer), will affect only the first found one.",
        null,
        @"
; Given an actor with ID 'Smoke' is visible, hide it over 3 seconds.
@hide Smoke time:3",
        @"
; Hide 'Kohaku' and 'Yuko' actors.
@hide Kohaku,Yuko"
    )]
    [CommandAlias("hide")]
    public class HideActors : Command
    {
        [Doc("IDs of the actors to hide.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter, ActorContext]
        public StringListParameter ActorIds;
        [Doc(SharedDocs.DurationParameter)]
        [ParameterAlias("time"), ParameterDefaultValue("0.35")]
        public DecimalParameter Duration;
        [Doc(SharedDocs.LazyParameter)]
        [ParameterDefaultValue("false")]
        public BooleanParameter Lazy = false;
        [Doc(SharedDocs.WaitParameter)]
        public BooleanParameter Wait;

        public override UniTask Execute (AsyncToken token = default)
        {
            return WaitOrForget(Hide, Wait, token);
        }

        protected virtual async UniTask Hide (AsyncToken token)
        {
            using var _ = ListPool<IActorManager>.Rent(out var managers);
            Engine.FindAllServices(managers, ActorIds,
                static (manager, actorIds) => actorIds.Any(id => manager.ActorExists(id)));
            using var __ = ListPool<UniTask>.Rent(out var tasks);
            foreach (var actorId in ActorIds)
                if (managers.FirstOrDefault(m => m.ActorExists(actorId)) is { } manager)
                    tasks.Add(HideInManager(actorId, manager, token));
                else Err($"Failed to hide '{actorId}' actor: can't find any managers with '{actorId}' actor.");
            await UniTask.WhenAll(tasks);
        }

        protected virtual async UniTask HideInManager (string actorId, IActorManager manager, AsyncToken token)
        {
            var actor = manager.GetActor(actorId);
            var duration = Assigned(Duration) ? Duration.Value : manager.ActorManagerConfiguration.DefaultDuration;
            var easing = manager.ActorManagerConfiguration.DefaultEasing;
            await actor.ChangeVisibility(false, new(duration, easing, complete: !Lazy), token);
        }
    }
}
