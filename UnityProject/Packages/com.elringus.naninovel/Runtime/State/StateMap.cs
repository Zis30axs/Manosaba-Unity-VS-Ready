using System;
using System.Collections.Generic;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents a map of serializable objects indexed by their assembly-qualified type names.
    /// </summary>
    [Serializable]
    public class StateMap : ISerializationCallbackReceiver
    {
        protected readonly Dictionary<string, object> ObjectMap = new(StringComparer.Ordinal);

        [SerializeField] private SerializableLiteralStringMap objectJsonMap = new();

        /// <summary>
        /// Creates new state map instance with the specified records.
        /// </summary>
        public static TMap With<TMap> (params (object state, string id)[] records) where TMap : StateMap, new()
        {
            var map = new TMap();
            foreach (var (state, id) in records)
                map.SetState(state, id);
            return map;
        }

        public virtual void OnBeforeSerialize ()
        {
            objectJsonMap.Clear();
            foreach (var kv in ObjectMap)
                objectJsonMap.Add(kv.Key, JsonUtility.ToJson(kv.Value));
        }

        public virtual void OnAfterDeserialize ()
        {
            ObjectMap.Clear();
            foreach (var kv in objectJsonMap)
            {
                var type = Type.GetType(kv.Key);
                if (type is null) continue; // Could (rarely) happen when we change asmdefs for naninovel sources.
                ObjectMap[kv.Key] = JsonUtility.FromJson(kv.Value, type);
            }
        }

        /// <summary>
        /// Stores the specified serializable object in the map using the object's type as the key.
        /// </summary>
        /// <typeparam name="TState">Type of the serialized state.</typeparam>
        /// <param name="state">State object to serialize.</param>
        /// <param name="instanceId">Optional instance ID, use when storing multiple objects of equal type.</param>
        public void SetState<TState> (TState state, string instanceId = default) where TState : class, new()
        {
            var key = state.GetType().AssemblyQualifiedName;
            if (!string.IsNullOrEmpty(instanceId))
                key += $", InstanceID={instanceId}";
            ObjectMap[key] = state;
        }

        /// <summary>
        /// Attempts to retrieve a serializable object with the specified type from the map. 
        /// Will return null when no objects of the type is contained in the map.
        /// </summary>
        /// <typeparam name="TState">Type of the serialized state.</typeparam>
        /// <param name="instanceId">Optional instance ID, use when storing multiple objects of equal type.</param>
        /// <returns>Deserialized state object or null, when none if found.</returns>
        public TState GetState<TState> (string instanceId = default) where TState : class, new()
        {
            var key = typeof(TState).AssemblyQualifiedName;
            if (!string.IsNullOrEmpty(instanceId))
                key += $", InstanceID={instanceId}";
            return ObjectMap.TryGetValue(key, out var state) ? state as TState : null;
        }
    }
}
