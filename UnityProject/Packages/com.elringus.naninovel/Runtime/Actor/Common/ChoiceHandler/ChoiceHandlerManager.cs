using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel
{
    /// <inheritdoc cref="IChoiceHandlerManager"/>
    [InitializeAtRuntime]
    public class ChoiceHandlerManager : ActorManager<IChoiceHandlerActor, ChoiceHandlerState, ChoiceHandlerMetadata, ChoiceHandlersConfiguration>, IChoiceHandlerManager
    {
        [Serializable]
        public new class GameState
        {
            public PickedChoice[] PickedChoices;
        }

        [Serializable]
        public struct PickedChoice
        {
            public PlaybackSpot HostedAt;
            public PlaybackSpot PickedAt;
        }

        public virtual IResourceLoader<GameObject> ChoiceButtonLoader => buttonLoader;

        protected virtual Dictionary<PlaybackSpot, PlaybackSpot> PickedChoices { get; } = new();

        private readonly IResourceProviderManager resources;
        private readonly ILocalizationManager l10n;

        private LocalizableResourceLoader<GameObject> buttonLoader;

        public ChoiceHandlerManager (ChoiceHandlersConfiguration config, IResourceProviderManager resources, ILocalizationManager l10n)
            : base(config)
        {
            this.resources = resources;
            this.l10n = l10n;
        }

        public override async UniTask InitializeService ()
        {
            await base.InitializeService();
            buttonLoader = Configuration.ChoiceButtonLoader.CreateLocalizableFor<GameObject>(resources, l10n);
        }

        public override void DestroyService ()
        {
            base.DestroyService();
            ChoiceButtonLoader?.ReleaseAll(this);
        }

        public override void SaveServiceState (GameStateMap stateMap)
        {
            base.SaveServiceState(stateMap);
            var state = new GameState {
                PickedChoices = PickedChoices.Select(kv => new PickedChoice {
                    HostedAt = kv.Key,
                    PickedAt = kv.Value
                }).ToArray()
            };
            stateMap.SetState(state);
        }

        public override UniTask LoadServiceState (GameStateMap stateMap)
        {
            var task = base.LoadServiceState(stateMap);
            var state = stateMap.GetState<GameState>() ?? new GameState();

            PickedChoices.Clear();
            foreach (var picked in state.PickedChoices)
                PickedChoices[picked.HostedAt] = picked.PickedAt;

            return task;
        }

        public virtual void PushPickedChoice (PlaybackSpot hostedAt, PlaybackSpot continueAt)
        {
            PickedChoices[hostedAt] = continueAt;
        }

        public virtual PlaybackSpot PopPickedChoice (PlaybackSpot hostedAt)
        {
            return PickedChoices.TryGetValue(hostedAt, out var pickedAt) ? pickedAt :
                throw new Error($"Failed to get picked choice for host at {hostedAt}");
        }
    }
}
