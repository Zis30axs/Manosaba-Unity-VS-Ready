using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Naninovel
{
    internal static class CommandDatabase
    {
        public static IReadOnlyDictionary<string, MethodInfo> Registered => methodInfoCache;

        private static Dictionary<string, MethodInfo> methodInfoCache;

        public static void ExecuteCommand (string methodName, params string[] args)
        {
            if (methodInfoCache == null || !methodInfoCache.TryGetValue(methodName, out var methodInfo))
            {
                Debug.LogWarning($"UnityConsole: Command '{methodName}' is not registered in the database.");
                return;
            }
            var parametersInfo = methodInfo.GetParameters();
            if (parametersInfo.Length != args.Length)
            {
                Debug.LogWarning($"UnityConsole: Command '{methodName}' requires {parametersInfo.Length} args, while {args.Length} were specified.");
                return;
            }
            var parameters = new object[parametersInfo.Length];
            for (int i = 0; i < args.Length; i++)
                parameters[i] = Convert.ChangeType(args[i], parametersInfo[i].ParameterType, System.Globalization.CultureInfo.InvariantCulture);
            methodInfo.Invoke(null, parameters);
        }

        internal static void RegisterCommands (Dictionary<string, MethodInfo> commands = null)
        {
            methodInfoCache = commands ?? AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.IsDynamic)
                .SelectMany(assembly => assembly.GetExportedTypes())
                .SelectMany(type => type.GetMethods(BindingFlags.Static | BindingFlags.Public))
                .Where(method => method.GetCustomAttribute<ConsoleCommandAttribute>() != null)
                .ToDictionary(method => method.GetCustomAttribute<ConsoleCommandAttribute>().Alias ?? method.Name, StringComparer.OrdinalIgnoreCase);
        }
    }
}
