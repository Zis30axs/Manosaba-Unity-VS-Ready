using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Resolves <see cref="EngineTypes"/> and caches them via <see cref="TypeCache"/>.
    /// </summary>
    internal static class TypesResolver
    {
        internal static void Resolve ()
        {
            var types = new EngineTypes();
            SetField(types, nameof(EngineTypes.Commands), ResolveDerived<Command>());
            SetField(types, nameof(EngineTypes.Configurations), ResolveDerived<Configuration>());
            SetField(types, nameof(EngineTypes.ResourceProviders), ResolveDerived<IResourceProvider>());
            SetField(types, nameof(EngineTypes.ActorImplementations), ResolveDerived<IActor>());
            SetField(types, nameof(EngineTypes.CustomActorMetadata), ResolveDerived<CustomMetadata>());
            SetField(types, nameof(EngineTypes.InitializeAtRuntime), ResolveAttributed<InitializeAtRuntimeAttribute>());
            SetField(types, nameof(EngineTypes.DontReset), ResolveAttributed<Commands.Goto.DontResetAttribute>());
            SetField(types, nameof(EngineTypes.ExpressionFunctionHosts), ResolveWithAttributedMethods<ExpressionFunctionAttribute>());
            SetField(types, nameof(EngineTypes.ConsoleCommandHosts), ResolveWithAttributedMethods<ConsoleCommandAttribute>());
            SetField(types, nameof(EngineTypes.ManagedTextFieldHosts), ResolveWithAttributedFields<ManagedTextAttribute>());
            PreloadEditorHack(types);
            WriteCacheAsset(types);
        }

        private static void SetField (object instance, string fieldName, object value)
        {
            fieldName = fieldName.FirstToLower();
            var field = instance.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
                        ?? throw new Error($"Failed to cache engine types: missing '{fieldName}' field.");
            field.SetValue(instance, value);
        }

        private static string[] ResolveDerived<T> () =>
            UnityEditor.TypeCache.GetTypesDerivedFrom<T>()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .Select(t => t.AssemblyQualifiedName)
                .OrderBy(t => t)
                .ToArray();

        private static string[] ResolveAttributed<T> () where T : Attribute =>
            UnityEditor.TypeCache.GetTypesWithAttribute<T>()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
                .Select(t => t.AssemblyQualifiedName)
                .OrderBy(t => t)
                .ToArray();

        private static string[] ResolveWithAttributedMethods<T> () where T : Attribute =>
            UnityEditor.TypeCache.GetMethodsWithAttribute<T>()
                .Where(t => t.IsStatic && t.DeclaringType != null && (!t.DeclaringType.IsAbstract || t.DeclaringType.IsSealed) && !t.DeclaringType.IsGenericType)
                .Select(t => t.DeclaringType.AssemblyQualifiedName)
                .Distinct()
                .OrderBy(t => t)
                .ToArray();

        private static string[] ResolveWithAttributedFields<T> () where T : Attribute =>
            UnityEditor.TypeCache.GetFieldsWithAttribute<T>()
                .Where(t => t.IsStatic && t.DeclaringType != null && (!t.DeclaringType.IsAbstract || t.DeclaringType.IsSealed) && !t.DeclaringType.IsGenericType)
                .Select(t => t.DeclaringType.AssemblyQualifiedName)
                .Distinct()
                .OrderBy(t => t)
                .ToArray();

        /// A workaround due to a Unity design flow; see <see cref="TypeCache"/> for more info.
        private static void PreloadEditorHack (EngineTypes types)
        {
            typeof(TypeCache).GetField("PRELOADED_BY_EDITOR", BindingFlags.NonPublic | BindingFlags.Static)!.SetValue(null, types);
        }

        private static void WriteCacheAsset (EngineTypes types)
        {
            var path = PathUtils.Combine(PackagePath.TransientAssetPath, $"Resources/{TypeCache.ResourcePath}.json");
            var json = JsonUtility.ToJson(types);
            IOUtils.EnsureDirectories(path);
            File.WriteAllText(path, json, Encoding.UTF8);
        }
    }
}
