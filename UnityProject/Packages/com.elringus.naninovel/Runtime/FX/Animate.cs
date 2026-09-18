using Naninovel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Naninovel.FX
{
    /// <summary>
    /// Used by <see cref="AnimateActor"/> command.
    /// </summary>
    public class Animate : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable
    {
        protected virtual string ActorId { get; private set; }
        protected virtual bool Loop { get; private set; }
        protected virtual List<string> Appearance { get; } = new();
        protected virtual List<string> Transition { get; } = new();
        protected virtual List<bool?> Visibility { get; } = new();
        protected virtual List<float?> PositionX { get; } = new();
        protected virtual List<float?> PositionY { get; } = new();
        protected virtual List<float?> PositionZ { get; } = new();
        protected virtual List<float?> RotationZ { get; } = new();
        protected virtual List<Vector3?> Scale { get; } = new();
        protected virtual List<string> TintColor { get; } = new();
        protected virtual List<string> EasingTypeName { get; } = new();
        protected virtual List<float?> Duration { get; } = new();

        protected virtual int KeyCount { get; private set; }
        protected virtual string SpawnedPath { get; private set; }
        protected virtual EasingType DefaultEasing { get; private set; }
        protected virtual ISpawnManager SpawnManager => Engine.GetServiceOrErr<ISpawnManager>();
        protected virtual List<UniTask> tasks { get; } = new();
        protected virtual CameraConfiguration CameraConfig => Engine.GetServiceOrErr<ICameraManager>().Configuration;
        protected virtual CancellationTokenSource CTS { get; private set; }

        public virtual void SetSpawnParameters (IReadOnlyList<string> parameters, bool asap)
        {
            SpawnedPath = gameObject.name;
            KeyCount = 1 + parameters.Max(s => string.IsNullOrEmpty(s) ? 0 : s.Count(c => AnimateActor.KeyDelimiters.Contains(c)));
            for (int paramIdx = 0; paramIdx < 13; paramIdx++)
                ParseParameter(paramIdx, parameters);
            FillMissingDurations();
        }

        public virtual async UniTask AwaitSpawn (AsyncToken token = default)
        {
            var manager = Engine.FindService<IActorManager, string>(ActorId,
                static (manager, actorId) => manager.ActorExists(actorId));
            if (manager is null)
            {
                Engine.Warn($"Can't find a manager with '{ActorId}' actor to apply '{SpawnedPath}' command.");
                return;
            }
            token = InitializeCTS(token);
            DefaultEasing = manager.ActorManagerConfiguration.DefaultEasing;
            var actor = manager.GetActor(ActorId);

            if (Loop) LoopRoutine(actor, token).Forget();
            else
            {
                for (int keyIdx = 0; keyIdx < KeyCount; keyIdx++)
                    await AnimateKey(actor, keyIdx, token);
                if (SpawnManager.IsSpawned(SpawnedPath))
                    SpawnManager.DestroySpawned(SpawnedPath);
            }
        }

        protected virtual async UniTask AnimateKey (IActor actor, int keyIndex, AsyncToken token)
        {
            tasks.Clear();

            if (!Duration.IsIndexValid(keyIndex)) return;

            var duration = Duration[keyIndex] ?? 0f;
            var easingType = DefaultEasing;
            if (EasingTypeName.ElementAtOrDefault(keyIndex) != null && !Enum.TryParse(EasingTypeName[keyIndex], true, out easingType))
                Engine.Warn($"Failed to parse '{EasingTypeName}' easing.");
            var tween = new Tween(duration, easingType);

            if (Appearance.ElementAtOrDefault(keyIndex) != null)
            {
                var transitionName = !string.IsNullOrEmpty(Transition.ElementAtOrDefault(keyIndex)) ? Transition[keyIndex] : TransitionUtils.DefaultTransition;
                var transition = new Transition(transitionName);
                tasks.Add(actor.ChangeAppearance(Appearance[keyIndex], tween, transition, token));
            }

            if (Visibility.ElementAtOrDefault(keyIndex).HasValue)
                tasks.Add(actor.ChangeVisibility(Visibility[keyIndex] ?? false, tween, token));

            if (PositionX.ElementAtOrDefault(keyIndex).HasValue || PositionY.ElementAtOrDefault(keyIndex).HasValue || PositionZ.ElementAtOrDefault(keyIndex).HasValue)
                tasks.Add(actor.ChangePosition(new(
                    PositionX.ElementAtOrDefault(keyIndex) ?? actor.Position.x,
                    PositionY.ElementAtOrDefault(keyIndex) ?? actor.Position.y,
                    PositionZ.ElementAtOrDefault(keyIndex) ?? actor.Position.z), tween, token));

            if (RotationZ.ElementAtOrDefault(keyIndex).HasValue)
                tasks.Add(actor.ChangeRotationZ(RotationZ[keyIndex] ?? 0f, tween, token));

            if (Scale.ElementAtOrDefault(keyIndex).HasValue)
                tasks.Add(actor.ChangeScale(Scale[keyIndex] ?? Vector3.one, tween, token));

            if (TintColor.ElementAtOrDefault(keyIndex) != null)
            {
                if (ColorUtility.TryParseHtmlString(TintColor[keyIndex], out var color))
                    tasks.Add(actor.ChangeTintColor(color, tween, token));
                else Engine.Warn($"Failed to parse '{TintColor}' color to apply tint animation for '{actor.Id}' actor. See the API docs for supported color formats.");
            }

            await UniTask.WhenAll(tasks);

            token.ThrowIfCanceled(gameObject);
        }

        protected virtual Vector3? ParseScale (string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            if (!value.Contains(','))
            {
                var uniformScale = value.AsInvariantFloat();
                if (uniformScale is null) return null;
                return Vector3.one * uniformScale;
            }
            var values = value.Split(',');
            return new Vector3(
                values.ElementAtOrDefault(0)?.AsInvariantFloat() ?? 1,
                values.ElementAtOrDefault(1)?.AsInvariantFloat() ?? 1,
                values.ElementAtOrDefault(2)?.AsInvariantFloat() ?? 1);
        }

        protected virtual void ParseParameter (int paramIdx, IEnumerable<string> parameters)
        {
            var keys = parameters.ElementAtOrDefault(paramIdx)?.Split(AnimateActor.KeyDelimiters);
            if (keys is null || keys.Length == 0 || keys.All(s => s == string.Empty)) return;

            if (paramIdx == 0) ActorId = keys.ElementAtOrDefault(0);
            if (paramIdx == 1) Loop = bool.Parse(keys.ElementAtOrDefault(0) ?? "false");
            if (paramIdx == 2) AssignKeys(Appearance);
            if (paramIdx == 3) AssignKeys(Transition);
            if (paramIdx == 4) AssignKeys(Visibility, k => bool.TryParse(k, out var result) ? result : null);
            if (paramIdx == 5) AssignKeys(PositionX, k => CameraConfig.SceneToWorldSpace(new Vector2((k.AsInvariantFloat() ?? 0) / 100f, 0)).x);
            if (paramIdx == 6) AssignKeys(PositionY, k => CameraConfig.SceneToWorldSpace(new Vector2(0, (k.AsInvariantFloat() ?? 0) / 100f)).y);
            if (paramIdx == 7) AssignKeys(PositionZ, k => k.AsInvariantFloat());
            if (paramIdx == 8) AssignKeys(RotationZ, k => k.AsInvariantFloat());
            if (paramIdx == 9) AssignKeys(Scale, ParseScale);
            if (paramIdx == 10) AssignKeys(TintColor);
            if (paramIdx == 11) AssignKeys(EasingTypeName);
            if (paramIdx == 12) AssignKeys(Duration, k => k.AsInvariantFloat());

            void AssignKeys<T> (List<T> parameter, Func<string, T> parseKey = default)
            {
                var defaultKeys = Enumerable.Repeat<T>(default, KeyCount);
                parameter.AddRange(defaultKeys);
                for (int keyIdx = 0; keyIdx < keys.Length; keyIdx++)
                    if (!string.IsNullOrEmpty(keys[keyIdx]))
                        parameter[keyIdx] = parseKey is null ? (T)(object)keys[keyIdx] : parseKey(keys[keyIdx]);
            }
        }

        protected virtual void FillMissingDurations ()
        {
            var lastDuration = 0f;
            for (int keyIdx = 0; keyIdx < KeyCount; keyIdx++)
                if (!Duration.IsIndexValid(keyIdx)) continue;
                else if (!Duration[keyIdx].HasValue)
                    Duration[keyIdx] = lastDuration;
                else lastDuration = Duration[keyIdx].Value;
        }

        protected virtual async UniTaskVoid LoopRoutine (IActor actor, AsyncToken token)
        {
            while (Loop && Application.isPlaying && token.EnsureNotCanceledOrCompleted())
                for (int keyIdx = 0; keyIdx < KeyCount; keyIdx++)
                {
                    await AnimateKey(actor, keyIdx, token);
                    if (!token.EnsureNotCanceledOrCompleted()) break;
                }
        }

        protected virtual void OnDestroy ()
        {
            Loop = false;
            CTS?.Cancel();
            CTS?.Dispose();

            // The following is possible to prevent unpredicted state mutations,
            // though it's hardly worth all the complications:
            //   1. Add transient actor state (per each parameter), that is not serialized.
            //   2. When starting animation set real state to the last key frame.
            //   3. When any "normal" command modifies a property that has a transient state -- remove the transient effect.

            if (Engine.Initialized && SpawnManager.IsSpawned(SpawnedPath))
                SpawnManager.DestroySpawned(SpawnedPath);
        }

        protected virtual AsyncToken InitializeCTS (AsyncToken token)
        {
            CTS?.Cancel();
            CTS?.Dispose();
            CTS = CancellationTokenSource.CreateLinkedTokenSource(token.CancellationToken);
            return new(CTS.Token, token.CompletionToken);
        }
    }
}
