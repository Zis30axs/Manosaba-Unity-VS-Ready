using UnityEngine;

namespace Naninovel.Commands
{
    [Doc(@"
Slides (moves between two positions) an actor (character, background, text printer or choice handler) with the specified ID and optionally changes actor visibility and appearance.
Can be used instead of multiple [@char] or [@back] commands to reveal or hide an actor with a slide animation.",
        @"
Be aware, that this command searches for an existing actor with the specified ID over all the actor managers,
and in case multiple actors with the same ID exist (eg, a character and a text printer), this will affect only the first found one.
Make sure the actor exist on scene before referencing it with this command;
eg, if it's a character, you can add it on scene imperceptibly to player with `@char CharID visible:false time:0`.",
        @"
; Given 'Jenna' actor is not visible, reveal it with an 'Angry' appearance
; and slide to the center from either left or right border of the scene.
@slide Jenna.Angry to:50",
        @"
; Given 'Sheba' actor is currently visible,
; hide and slide it out of the scene over the left border.
@slide Sheba to:-10 !visible",
        @"
; Slide 'Mia' actor from left-center side of the scene to the right-bottom
; over 5 seconds using 'EaseOutBounce' animation easing.
@slide Sheba from:15,50 to:85,0 time:5 easing:EaseOutBounce"
    )]
    [CommandAlias("slide")]
    public class SlideActor : Command
    {
        [Doc("ID of the actor to slide and (optionally) appearance to set.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter, ActorContext(index: 0), AppearanceContext(1)]
        public NamedStringParameter IdAndAppearance;
        [Doc("Position in scene space to slide the actor from (slide start position). " +
             "Described as follows: `0,0` is the bottom left, `50,50` is the center and `100,100` is the top right corner of the scene; Z-component (depth) is in world space. " +
             "When not specified, will use current actor position in case it's visible and a random off-scene position otherwise (could slide-in from left or right borders).")]
        [ParameterAlias("from"), VectorContext("X,Y,Z")]
        public DecimalListParameter FromPosition;
        [Doc("Position in scene space to slide the actor to (slide finish position).")]
        [ParameterAlias("to"), VectorContext("X,Y,Z"), RequiredParameter]
        public DecimalListParameter ToPosition;
        [Doc("Change visibility status of the actor (show or hide). When not set and target actor is hidden, will still automatically show it.")]
        public BooleanParameter Visible;
        [Doc(SharedDocs.EasingParameter)]
        [ParameterAlias("easing"), ConstantContext(typeof(EasingType))]
        public StringParameter EasingTypeName;
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
            return WaitOrForget(Slide, Wait, token);
        }

        protected virtual async UniTask Slide (AsyncToken token)
        {
            var actorId = IdAndAppearance.Name;
            var manager = Engine.FindService<IActorManager, string>(actorId,
                static (manager, actorId) => manager.ActorExists(actorId));

            if (manager is null)
            {
                Err($"Can't find a manager with '{actorId}' actor.");
                return;
            }

            using var _ = ListPool<UniTask>.Rent(out var tasks);

            var cfg = Engine.GetConfiguration<CameraConfiguration>();
            var actor = manager.GetActor(actorId);

            var fromPos = new Vector3(
                FromPosition?.ElementAtOrNull(0)?.HasValue ?? false ? cfg.SceneToWorldSpace(new Vector2(FromPosition[0] / 100f, 0)).x :
                actor.Visible ? actor.Position.x : cfg.SceneToWorldSpace(new Vector2(Random.value > .5f ? -.1f : 1.1f, 0)).x,
                FromPosition?.ElementAtOrNull(1)?.HasValue ?? false ? cfg.SceneToWorldSpace(new Vector2(0, FromPosition[1] / 100f)).y : actor.Position.y,
                FromPosition?.ElementAtOrNull(2) ?? actor.Position.z);

            var toPos = new Vector3(
                ToPosition.ElementAtOrNull(0)?.HasValue ?? false ? cfg.SceneToWorldSpace(new Vector2(ToPosition[0] / 100f, 0)).x : actor.Position.x,
                ToPosition.ElementAtOrNull(1)?.HasValue ?? false ? cfg.SceneToWorldSpace(new Vector2(0, ToPosition[1] / 100f)).y : actor.Position.y,
                ToPosition.ElementAtOrNull(2) ?? actor.Position.z);

            var duration = Assigned(Duration) ? Duration.Value : manager.ActorManagerConfiguration.DefaultDuration;
            var easingType = manager.ActorManagerConfiguration.DefaultEasing;
            if (Assigned(EasingTypeName) && !ParseUtils.TryConstantParameter(EasingTypeName, out easingType))
                Warn($"Failed to parse '{EasingTypeName}' easing.");
            var tween = new Tween(duration, easingType, complete: !Lazy);

            actor.Position = fromPos;

            if (!actor.Visible)
            {
                if (IdAndAppearance.NamedValue.HasValue)
                    actor.Appearance = IdAndAppearance.NamedValue;
                Visible = true;
            }
            else if (IdAndAppearance.NamedValue.HasValue)
                tasks.Add(actor.ChangeAppearance(IdAndAppearance.NamedValue, tween, token: token));

            if (Assigned(Visible)) tasks.Add(actor.ChangeVisibility(Visible, tween, token));

            tasks.Add(actor.ChangePosition(toPos, tween, token));

            await UniTask.WhenAll(tasks);
        }
    }
}
