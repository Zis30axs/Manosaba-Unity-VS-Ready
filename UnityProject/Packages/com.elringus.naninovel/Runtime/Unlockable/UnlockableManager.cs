using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel
{
    /// <inheritdoc cref="IUnlockableManager"/>
    [InitializeAtRuntime]
    public class UnlockableManager : IUnlockableManager, IStatefulService<GlobalStateMap>
    {
        /// <summary>
        /// Serializable dictionary, representing unlockable item ID to its unlocked state map.
        /// </summary>
        [Serializable]
        public class UnlockablesMap : SerializableMap<string, bool>
        {
            public UnlockablesMap () : base(StringComparer.OrdinalIgnoreCase) { }
            public UnlockablesMap (UnlockablesMap map) : base(map, StringComparer.OrdinalIgnoreCase) { }
        }

        [Serializable]
        public class GlobalState
        {
            public UnlockablesMap UnlockablesMap = new();
        }

        public event Action<UnlockableItemUpdatedArgs> OnItemUpdated;

        public virtual UnlockablesConfiguration Configuration { get; }
        public virtual IReadOnlyCollection<string> ItemIds => map.KeyCollection;

        private readonly UnlockablesMap map = new();
        private readonly IResourceProviderManager resources;
        private readonly ITextManager docs;

        public UnlockableManager (UnlockablesConfiguration config,
            IResourceProviderManager resources, ITextManager docs)
        {
            Configuration = config;
            this.resources = resources;
            this.docs = docs;
        }

        public virtual async UniTask InitializeService ()
        {
            await docs.DocumentLoader.Load(ManagedTextPaths.Tips, this);
            foreach (var id in await LocateUnlockableResources())
                map[id] = false;
            foreach (var id in GetAllTips())
                map[id] = false;
        }

        public virtual void ResetService () { }

        public virtual void DestroyService ()
        {
            docs.DocumentLoader.ReleaseAll(this);
        }

        public virtual void SaveServiceState (GlobalStateMap stateMap)
        {
            var globalState = new GlobalState {
                UnlockablesMap = new(map)
            };
            stateMap.SetState(globalState);
        }

        public virtual UniTask LoadServiceState (GlobalStateMap stateMap)
        {
            var state = stateMap.GetState<GlobalState>();
            if (state is null) return UniTask.CompletedTask;

            foreach (var kv in state.UnlockablesMap)
                map[kv.Key] = kv.Value;
            return UniTask.CompletedTask;
        }

        public virtual bool ItemUnlocked (string itemId)
        {
            if (string.IsNullOrEmpty(itemId)) throw new ArgumentNullException(nameof(itemId), "Can't get unlock status of item with empty ID.");
            return map.TryGetValue(itemId, out var item) && item;
        }

        public virtual void SetItemUnlocked (string itemId, bool unlocked)
        {
            if (string.IsNullOrEmpty(itemId)) throw new ArgumentNullException(nameof(itemId), "Can't set unlock status of item with empty ID.");

            if (unlocked && ItemUnlocked(itemId)) return;
            if (!unlocked && map.ContainsKey(itemId) && !ItemUnlocked(itemId)) return;

            var added = map.ContainsKey(itemId);
            map[itemId] = unlocked;
            OnItemUpdated?.Invoke(new(itemId, unlocked, added));
        }

        public virtual void UnlockItem (string itemId) => SetItemUnlocked(itemId, true);

        public virtual void LockItem (string itemId) => SetItemUnlocked(itemId, false);

        public virtual void UnlockAllItems ()
        {
            foreach (var itemId in map.Keys.ToArray())
                UnlockItem(itemId);
        }

        public virtual void LockAllItems ()
        {
            foreach (var itemId in map.Keys.ToArray())
                LockItem(itemId);
        }

        protected virtual UniTask<IReadOnlyCollection<string>> LocateUnlockableResources ()
        {
            var loader = Configuration.Loader.CreateFor<GameObject>(resources);
            return loader.Locate();
        }

        protected virtual IReadOnlyCollection<string> GetAllTips ()
        {
            return docs.GetDocument(ManagedTextPaths.Tips)?.Records
                .Select(r => $"{UI.TipsPanel.DefaultUnlockableIdPrefix}/{r.Key}").ToArray() ?? Array.Empty<string>();
        }
    }
}
