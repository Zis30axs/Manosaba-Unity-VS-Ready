using System;
using System.Collections.Generic;
using System.Reflection;
using Naninovel.UI;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Provides implementations of the built-in debug console commands.
    /// </summary>
    public static class ConsoleCommands
    {
        [ConsoleCommand("nav")]
        public static void ToggleScriptNavigator ()
        {
            if (!Engine.GetServiceOrErr<IUIManager>().TryGetUI<IScriptNavigatorUI>(out var nav)) return;
            if (nav.Visible) nav.Hide();
            else nav.Show();
        }

        [ConsoleCommand("debug")]
        public static void ToggleDebugInfoGUI () => DebugInfoGUI.Toggle();

        [ConsoleCommand("var")]
        public static void ToggleCustomVariableGUI () => CustomVariableGUI.Toggle();

        #if UNITY_GOOGLE_DRIVE_AVAILABLE
        [ConsoleCommand("purge")]
        public static void PurgeCache ()
        {
            var manager = Engine.GetService<IResourceProviderManager>();
            if (manager is null)
            {
                Engine.Err("Failed to retrieve provider manager.");
                return;
            }
            var googleDriveProvider = manager.GetProvider(ResourceProviderConfiguration.GoogleDriveTypeName) as GoogleDriveResourceProvider;
            if (googleDriveProvider is null)
            {
                Engine.Err("Failed to retrieve google drive provider.");
                return;
            }
            googleDriveProvider.PurgeCache();
        }
        #endif

        [ConsoleCommand]
        public static void Play () => Engine.GetService<IScriptPlayer>()?.Resume();

        [ConsoleCommand]
        public static void PlayScript (string name) => Engine.GetService<IScriptPlayer>()?.LoadAndPlay(name);

        [ConsoleCommand]
        public static void Stop () => Engine.GetService<IScriptPlayer>()?.Stop();

        [ConsoleCommand]
        public static async void Rewind (int line)
        {
            line = Mathf.Clamp(line, 1, int.MaxValue);
            var player = Engine.GetServiceOrErr<IScriptPlayer>();
            var ok = await player.Rewind(line - 1);
            if (!ok) Engine.Warn($"Failed to rewind to line #{line} of script '{player.PlayedScript.Path}'. Make sure the line exists in the script and it's playable (either a command or a generic text line). When rewinding forward, '@stop' commands can prevent reaching the target line. When rewinding backward the target line should've been previously played and be kept in the rollback stack (capacity controlled by '{nameof(StateConfiguration.StateRollbackSteps)}' property in state configuration).");
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void SetupDevelopmentConsole ()
        {
            Engine.OnDestroyed -= ConsoleGUI.Destroy;
            Engine.OnDestroyed += ConsoleGUI.Destroy;
            Engine.OnInitializationFinished -= OnInitializationFinished;
            Engine.OnInitializationFinished += OnInitializationFinished;
            if (Engine.Initialized) OnInitializationFinished();

            void OnInitializationFinished ()
            {
                if (!Engine.Configuration.EnableDevelopmentConsole) return;
                if (Engine.Configuration.DebugOnlyConsole && !Debug.isDebugBuild) return;

                var input = Engine.GetService<IInputManager>().GetToggleConsole();
                if (input != null) input.OnEnd += ConsoleGUI.Toggle;

                ConsoleGUI.OnShow = () => Engine.GetService<IUIManager>()?.GetUI<IClickThroughPanel>()?.Show(false, null);
                ConsoleGUI.OnHide = () => Engine.GetService<IUIManager>()?.GetUI<IClickThroughPanel>()?.Hide();
                ConsoleGUI.ToggleKey = KeyCode.None;
                ConsoleGUI.Initialize(FindCommands());
                InputPreprocessor.AddPreprocessor(ProcessCommandInput);
            }

            Dictionary<string, MethodInfo> FindCommands ()
            {
                var commands = new Dictionary<string, MethodInfo>(StringComparer.OrdinalIgnoreCase);
                foreach (var type in Engine.Types.ConsoleCommandHosts)
                {
                    var methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public);
                    for (int i = 0; i < methods.Length; i++)
                    {
                        var method = methods[i];
                        var attr = method.GetCustomAttribute<ConsoleCommandAttribute>();
                        if (attr is null) continue;
                        commands[attr.Alias ?? method.Name] = method;
                    }
                }
                return commands;
            }
        }

        private static string ProcessCommandInput (string input)
        {
            if (input is null || !input.StartsWithFast(Compiler.Syntax.CommandLine)) return input;
            Engine.GetServiceOrErr<IScriptPlayer>().PlayTransient("Console", input).Forget();
            return null;
        }
    }
}
