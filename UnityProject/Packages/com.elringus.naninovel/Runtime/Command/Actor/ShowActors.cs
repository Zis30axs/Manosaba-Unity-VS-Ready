using System.Linq;

namespace Naninovel.Commands
{
    [Doc(@"
Shows (makes visible) actors (character, background, text printer, choice handler, etc) with the specified IDs.
In case multiple actors with the same ID found (eg, a character and a printer), will affect only the first found one.",
        null,
        @"
; Given an actor with ID 'Smoke' is hidden, reveal it over 3 seconds.
@show Smoke time:3",
        @"
; Show 'Kohaku' and 'Yuko' actors.
@show Kohaku,Yuko"
    )]
    [CommandAlias("show")]
    public class ShowActors : Command
    {
        [Doc("IDs of the actors to show.")]
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
            return WaitOrForget(Show, Wait, token);
        }

        protected virtual async UniTask Show (AsyncToken token)
        {
            using var _ = ListPool<IActorManager>.Rent(out var managers);
            Engine.FindAllServices<IActorManager, StringListParameter>(managers, ActorIds,
                static (manager, actorIds) => actorIds.Any(id => manager.ActorExists(id)));
            using var __ = ListPool<UniTask>.Rent(out var tasks);
            foreach (var actorId in ActorIds)
                if (managers.FirstOrDefault(m => m.ActorExists(actorId)) is { } manager)
                    tasks.Add(manager.GetActor(actorId).ChangeVisibility(true, new(GetDuration(manager), GetEasing(manager), complete: !Lazy), token));
                else Err($"Failed to show '{actorId}' actor: can't find any managers with '{actorId}' actor.");
            await UniTask.WhenAll(tasks);
        }

        protected virtual float GetDuration (IActorManager manager)
        {
            return Assigned(Duration) ? Duration.Value : manager.ActorManagerConfiguration.DefaultDuration;
        }

        protected virtual EasingType GetEasing (IActorManager manager)
        {
            return manager.ActorManagerConfiguration.DefaultEasing;
        }
    }
}
