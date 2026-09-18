using System.Linq;
using UnityEngine;

namespace Naninovel.Commands
{
    [Doc(
        @"
Arranges specified characters by X-axis.
When no parameters specified, will execute an auto-arrange evenly distributing visible characters by X-axis.",
        null,
        @"
; Evenly distribute all the visible characters.
@arrange",
        @"
; Place character with ID 'Jenna' 15%, 'Felix' 50% and 'Mia' 85% away
; from the left border of the scene.
@arrange Jenna.15,Felix.50,Mia.85"
    )]
    [CommandAlias("arrange")]
    public class ArrangeCharacters : Command
    {
        [Doc("A collection of character ID to scene X-axis position (relative to the left scene border, in percents) named values. " +
             "Position 0 relates to the left border and 100 to the right border of the scene; 50 is the center.")]
        [ParameterAlias(NamelessParameterAlias), ActorContext(CharactersConfiguration.DefaultPathPrefix, 0)]
        public NamedDecimalListParameter CharacterPositions;
        [Doc("When performing auto-arrange, controls whether to also make the characters look at the scene origin (enabled by default).")]
        [ParameterAlias("look"), ParameterDefaultValue("true")]
        public BooleanParameter LookAtOrigin = true;
        [Doc(SharedDocs.DurationParameter)]
        [ParameterAlias("time"), ParameterDefaultValue("0.35")]
        public DecimalParameter Duration;
        [Doc(SharedDocs.WaitParameter)]
        public BooleanParameter Wait;

        protected ICharacterManager Characters => Engine.GetServiceOrErr<ICharacterManager>();

        public override UniTask Execute (AsyncToken token = default)
        {
            return WaitOrForget(Arrange, Wait, token);
        }

        protected virtual async UniTask Arrange (AsyncToken token)
        {
            var duration = Assigned(Duration) ? Duration.Value : Characters.ActorManagerConfiguration.DefaultDuration;
            var easing = Characters.ActorManagerConfiguration.DefaultEasing;
            var tween = new Tween(duration, easing);

            // When positions are not specified execute auto arrange.
            if (!Assigned(CharacterPositions))
            {
                await WaitOrForget(token => Characters.ArrangeCharacters(LookAtOrigin, tween, token), Wait, token);
                return;
            }

            using var _ = ListPool<UniTask>.Rent(out var tasks);
            foreach (var actorPos in CharacterPositions)
            {
                if (!actorPos.HasValue) continue;

                var actor = Characters.Actors.FirstOrDefault(a => a.Id.EqualsFastIgnoreCase(actorPos.Name));
                var posX = actorPos.NamedValue / 100f; // Implementation is expecting local scene pos, not percents.
                if (actor is null)
                {
                    Warn($"Actor '{actorPos.Name}' not found while executing arranging task.");
                    continue;
                }
                var newPosX = Engine.GetConfiguration<CameraConfiguration>().SceneToWorldSpace(new Vector2(posX, 0)).x;
                var newDir = Characters.LookAtOriginDirection(newPosX);
                tasks.Add(actor.ChangeLookDirection(newDir, tween, token));
                tasks.Add(actor.ChangePositionX(newPosX, tween, token));
            }

            // Sorting by z in order of declaration (first is bottom).
            var declaredActorIds = CharacterPositions
                .Where(a => !string.IsNullOrEmpty(a?.Value?.Name))
                .Select(a => a.Name).Reverse().ToList();
            for (int i = 0; i < declaredActorIds.Count - 1; i++)
            {
                var currentActor = Characters.Actors.FirstOrDefault(a => a.Id.EqualsFastIgnoreCase(declaredActorIds[i]));
                var nextActor = Characters.Actors.FirstOrDefault(a => a.Id.EqualsFastIgnoreCase(declaredActorIds[i + 1]));
                if (currentActor is null || nextActor is null) continue;

                if (currentActor.Position.z > nextActor.Position.z)
                {
                    var lowerZPos = nextActor.Position.z;
                    var higherZPos = currentActor.Position.z;

                    nextActor.ChangePositionZ(higherZPos);
                    currentActor.ChangePositionZ(lowerZPos);
                }
            }

            await UniTask.WhenAll(tasks);
        }
    }
}
