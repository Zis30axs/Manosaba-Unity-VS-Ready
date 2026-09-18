using System;
using System.Collections.Generic;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Types (both built-in and user-created) available for the engine at runtime when looking
    /// for available configurations, actor implementations, serialization handlers, etc.
    /// </summary>
    /// <remarks>
    /// Can be accessed both at runtime and in editor via <see cref="Engine.Types"/>.
    /// The types are resolved and assigned by TypeResolver in editor.
    /// Serialized and cached as runtime resource by <see cref="TypeCache"/>.
    /// </remarks>
    [Serializable]
    public class EngineTypes
    {
        /// <summary>
        /// Types derived from <see cref="Command"/>.
        /// </summary>
        public IReadOnlyCollection<Type> Commands => GetOrConstruct(nameof(commands), commands);
        /// <summary>
        /// Types derived from <see cref="Configuration"/>.
        /// </summary>
        public IReadOnlyCollection<Type> Configurations => GetOrConstruct(nameof(configurations), configurations);
        /// <summary>
        /// Types derived from <see cref="IResourceProvider"/>.
        /// </summary>
        public IReadOnlyCollection<Type> ResourceProviders => GetOrConstruct(nameof(resourceProviders), resourceProviders);
        /// <summary>
        /// Types derived from <see cref="IActor"/>.
        /// </summary>
        public IReadOnlyCollection<Type> ActorImplementations => GetOrConstruct(nameof(actorImplementations), actorImplementations);
        /// <summary>
        /// Types derived from <see cref="CustomMetadata"/>.
        /// </summary>
        public IReadOnlyCollection<Type> CustomActorMetadata => GetOrConstruct(nameof(customActorMetadata), customActorMetadata);
        /// <summary>
        /// Types with <see cref="InitializeAtRuntimeAttribute"/> attribute.
        /// </summary>
        public IReadOnlyCollection<Type> InitializeAtRuntime => GetOrConstruct(nameof(initializeAtRuntime), initializeAtRuntime);
        /// <summary>
        /// Types with <see cref="Commands.Goto.DontResetAttribute"/> attribute.
        /// </summary>
        public IReadOnlyCollection<Type> DontReset => GetOrConstruct(nameof(dontReset), dontReset);
        /// <summary>
        /// Types (static classes) that host methods with <see cref="ExpressionFunctionAttribute"/> attribute.
        /// </summary>
        public IReadOnlyCollection<Type> ExpressionFunctionHosts => GetOrConstruct(nameof(expressionFunctionHosts), expressionFunctionHosts);
        /// <summary>
        /// Types (static classes) that host methods with <see cref="ConsoleCommandAttribute"/> attribute.
        /// </summary>
        public IReadOnlyCollection<Type> ConsoleCommandHosts => GetOrConstruct(nameof(consoleCommandHosts), consoleCommandHosts);
        /// <summary>
        /// Types (static classes) that host fields with <see cref="ManagedTextAttribute"/> attribute.
        /// </summary>
        public IReadOnlyCollection<Type> ManagedTextFieldHosts => GetOrConstruct(nameof(managedTextFieldHosts), managedTextFieldHosts);

        [SerializeField] private string[] commands;
        [SerializeField] private string[] configurations;
        [SerializeField] private string[] resourceProviders;
        [SerializeField] private string[] actorImplementations;
        [SerializeField] private string[] customActorMetadata;
        [SerializeField] private string[] initializeAtRuntime;
        [SerializeField] private string[] dontReset;
        [SerializeField] private string[] expressionFunctionHosts;
        [SerializeField] private string[] consoleCommandHosts;
        [SerializeField] private string[] managedTextFieldHosts;

        private Dictionary<string, IReadOnlyCollection<Type>> constructed = new();

        private IReadOnlyCollection<Type> GetOrConstruct (string key, IReadOnlyCollection<string> typeNames)
        {
            if (constructed.TryGetValue(key, out var cached)) return cached;
            var types = new List<Type>(typeNames.Count);
            foreach (var typeName in typeNames)
                // check for null, as some types may not be available in build
                // (eg, mock commands under test assembly)
                if (Type.GetType(typeName) is { } type)
                    types.Add(type);
            return constructed[key] = types;
        }
    }
}
