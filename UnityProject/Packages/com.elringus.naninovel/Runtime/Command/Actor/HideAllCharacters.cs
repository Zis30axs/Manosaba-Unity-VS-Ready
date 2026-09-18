using System.Linq;

namespace Naninovel.Commands
{
    [Doc(
        @"
Hides all the visible characters on scene.",
        null,
        @"
; Hide all the visible character actors on scene.
@hideChars"
    )]
    [CommandAlias("hideChars")]
    public class HideAllCharacters : Command
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
            return WaitOrForget(Hide, Wait, token);
        }

        protected virtual async UniTask Hide (AsyncToken token)
        {
            var manager = Engine.GetServiceOrErr<ICharacterManager>();
            var duration = Assigned(Duration) ? Duration.Value : manager.ActorManagerConfiguration.DefaultDuration;
            var easing = manager.ActorManagerConfiguration.DefaultEasing;
            var tween = new Tween(duration, easing, complete: !Lazy);
            await UniTask.WhenAll(manager.Actors.Select(a => a.ChangeVisibility(false, tween, token)));
        }
    }
}
