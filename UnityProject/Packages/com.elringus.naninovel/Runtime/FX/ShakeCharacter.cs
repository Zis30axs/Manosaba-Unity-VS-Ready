using System.Linq;
using UnityEngine;

namespace Naninovel.FX
{
    /// <summary>
    /// Shakes a <see cref="ICharacterActor"/> with specified name or a random visible one.
    /// </summary>
    public class ShakeCharacter : ShakeTransform
    {
        [SerializeField] private bool preventPositiveYOffset = true;

        protected override Transform GetShakenTransform ()
        {
            var manager = Engine.GetServiceOrErr<ICharacterManager>();
            var id = string.IsNullOrEmpty(ObjectName) ? manager.Actors.FirstOrDefault(a => a.Visible)?.Id : ObjectName;
            if (id is null || !manager.ActorExists(id))
                throw new Error($"Failed to shake character with '{id}' ID: actor not found.");
            return (manager.GetActor(id) as MonoBehaviourActor<CharacterMetadata>)?.Transform;
        }

        protected override async UniTask ShakeSequence (AsyncToken token)
        {
            if (!preventPositiveYOffset)
            {
                await base.ShakeSequence(token);
                return;
            }

            var amplitude = DeltaPos + DeltaPos * Random.Range(-AmplitudeVariation, AmplitudeVariation);
            var duration = ShakeDuration + ShakeDuration * Random.Range(-DurationVariation, DurationVariation);

            await Move(InitialPos - amplitude * .5f, duration * .5f, token);
            await Move(InitialPos, duration * .5f, token);
        }
    }
}
