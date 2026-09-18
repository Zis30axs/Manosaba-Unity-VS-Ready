using System.Linq;

namespace Naninovel.Commands
{
    [Doc(
        @"
Hides all the actors (characters, backgrounds, text printers, choice handlers) on scene.",
        null,
        @"
; Hide all the visible actors (chars, backs, printers, etc) on scene.
@hideAll"
    )]
    [CommandAlias("hideAll")]
    public class HideAllActors : Command
    {
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
            return WaitOrForget(token => UniTask.WhenAll(Engine.Services.OfType<IActorManager>()
                .Select(m => HideManagedActors(m, token))), Wait, token);
        }

        private UniTask HideManagedActors (IActorManager manager, AsyncToken token)
        {
            var duration = Assigned(Duration) ? Duration.Value : manager.ActorManagerConfiguration.DefaultDuration;
            var easing = manager.ActorManagerConfiguration.DefaultEasing;
            var tween = new Tween(duration, easing, complete: !Lazy);
            return UniTask.WhenAll(manager.Actors.Select(a => a.ChangeVisibility(false, tween, token)));
        }
    }
}
