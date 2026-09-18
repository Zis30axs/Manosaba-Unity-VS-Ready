using System.Collections.Generic;
using System.Linq;
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel.FX
{
    public class Blur : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IParameterized, DestroySpawned.IAwaitable
    {
        public interface IBlurable
        {
            UniTask Blur (float intensity, Tween tween, AsyncToken token = default);
        }

        protected string ActorId { get; private set; }
        protected float Intensity { get; private set; }
        protected float Duration { get; private set; }
        protected float StopDuration { get; private set; }

        [SerializeField] private string defaultActorId = "MainBackground";
        [SerializeField] private float defaultIntensity = .5f;
        [SerializeField] private float defaultDuration = 1f;

        public virtual void SetSpawnParameters (IReadOnlyList<string> parameters, bool asap)
        {
            ActorId = parameters?.ElementAtOrDefault(0) ?? defaultActorId;
            Intensity = Mathf.Abs(parameters?.ElementAtOrDefault(1)?.AsInvariantFloat() ?? defaultIntensity);
            Duration = asap ? 0 : Mathf.Abs(parameters?.ElementAtOrDefault(2)?.AsInvariantFloat() ?? defaultDuration);
        }

        public async UniTask AwaitSpawn (AsyncToken token = default)
        {
            var actor = FindActor(ActorId);
            if (actor is null) return;
            var duration = token.Completed ? 0 : Duration;
            await actor.Blur(Intensity, new(duration, EasingType.SmoothStep), token);
        }

        public void SetDestroyParameters (IReadOnlyList<string> parameters)
        {
            StopDuration = Mathf.Abs(parameters?.ElementAtOrDefault(0)?.AsInvariantFloat() ?? defaultDuration);
        }

        public async UniTask AwaitDestroy (AsyncToken token = default)
        {
            var actor = FindActor(ActorId);
            if (actor is null) return;
            var duration = token.Completed ? 0 : StopDuration;
            await actor.Blur(0, new(duration, EasingType.SmoothStep), token);
        }

        private void OnDestroy () // Required to disable the effect on rollback.
        {
            FindActor(ActorId, false)?.Blur(0, new(0));
        }

        private static IBlurable FindActor (string actorId, bool logError = true)
        {
            var manager = Engine.FindService<IActorManager, string>(actorId,
                static (manager, actorId) => manager.ActorExists(actorId));
            if (manager is null)
            {
                if (logError) Engine.Err($"Failed to apply blur effect: Can't find '{actorId}' actor");
                return null;
            }
            var blurable = manager.GetActor(actorId) as IBlurable;
            if (blurable is null)
            {
                if (logError) Engine.Err($"Failed to apply blur effect: '{actorId}' actor doesn't support blur effect.");
                return null;
            }
            return blurable;
        }
    }
}
