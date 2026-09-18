using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Naninovel
{
    public static class ActorImplementations
    {
        private static readonly List<Type> implementationTypes = new();
        private static readonly Dictionary<string, Type> implementationToCustomDataType = new();
        private static readonly Dictionary<string, ActorResourcesAttribute> implementationToResourcesAttribute = new();

        static ActorImplementations ()
        {
            foreach (var type in Engine.Types.ActorImplementations)
                AddImplementationType(type);
            foreach (var type in Engine.Types.CustomActorMetadata)
                AddCustomDataType(type);
        }

        public static Type[] GetAllImplementations ()
        {
            return implementationTypes.ToArray();
        }

        public static Type[] GetImplementations<TActor> () where TActor : IActor
        {
            return implementationTypes.Where(t => typeof(TActor).IsAssignableFrom(t)).ToArray();
        }

        public static bool TryGetCustomDataType (string implementation, out Type type)
        {
            return implementationToCustomDataType.TryGetValue(implementation, out type);
        }

        public static bool TryGetResourcesAttribute (string implementation, out ActorResourcesAttribute attribute)
        {
            return implementationToResourcesAttribute.TryGetValue(implementation, out attribute);
        }

        private static void AddImplementationType (Type type)
        {
            implementationTypes.Add(type);
            if (type.GetCustomAttribute<ActorResourcesAttribute>() is { } resourcesAttribute)
                implementationToResourcesAttribute[type.AssemblyQualifiedName] = resourcesAttribute;
        }

        private static void AddCustomDataType (Type type)
        {
            var customMetaImplType = default(string);
            var curType = type;
            while (curType.BaseType != null)
            {
                if (curType.IsGenericType && curType.GetGenericTypeDefinition() == typeof(CustomMetadata<>))
                {
                    customMetaImplType = curType.GetGenericArguments()[0].AssemblyQualifiedName;
                    break;
                }
                curType = curType.BaseType;
            }
            if (!string.IsNullOrEmpty(customMetaImplType))
                implementationToCustomDataType[customMetaImplType] = type;
        }
    }
}
