using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorInternal;

namespace Naninovel
{
    /// <summary>
    /// Uses file system watchers to track changes to `.nani` files in the project directory.
    /// </summary>
    public static class ScriptFileWatcher
    {
        /// <summary>
        /// Invoked when a <see cref="Script"/> asset is created or modified; returns modified script asset path.
        /// </summary>
        public static event Action<string> OnModified;

        private static readonly ConcurrentQueue<string> modifiedScriptPaths = new();
        private static readonly ConcurrentStack<FileSystemWatcher> runningWatchers = new();

        /// <summary>
        /// Start the watchers if the option is enabled in the script configuration.
        /// Will also stop any watchers that are already running.
        /// </summary>
        public static void Initialize ()
        {
            StopWatching();
            var config = Configuration.GetOrDefault<ScriptsConfiguration>();
            if (config.WatchScripts) StartWatching(config);
        }

        /// <summary>
        /// Adds directories to watch unless they're already watched.
        /// </summary>
        /// <param name="directoryPaths">Full paths to the directories to watch.</param>
        public static void AddWatchedDirectories (IEnumerable<string> directoryPaths)
        {
            foreach (var path in directoryPaths)
                if (!runningWatchers.Any(w => PathUtils.AbsoluteToAssetPath(w.Path) == PathUtils.AbsoluteToAssetPath(path)))
                    WatchDirectory(path);
        }

        private static void StartWatching (ScriptsConfiguration config)
        {
            EditorApplication.update += Update;
            foreach (var path in FindDirectoriesWithScripts(config))
                WatchDirectory(path);
        }

        private static void StopWatching ()
        {
            EditorApplication.update -= Update;
            foreach (var watcher in runningWatchers)
                watcher?.Dispose();
            runningWatchers.Clear();
        }

        private static void Update ()
        {
            if (modifiedScriptPaths.Count == 0) return;
            if (!modifiedScriptPaths.TryDequeue(out var fullPath)) return;
            if (!File.Exists(fullPath)) return;

            var assetPath = PathUtils.AbsoluteToAssetPath(fullPath);
            AssetDatabase.ImportAsset(assetPath);
            OnModified?.Invoke(assetPath);

            // Required to rebuild script when editor is not in focus, because script view
            // delays rebuild, but delayed call is not invoked while editor is not in focus.
            if (!InternalEditorUtility.isApplicationActive)
                EditorApplication.delayCall?.Invoke();
        }

        private static IReadOnlyCollection<string> FindDirectoriesWithScripts (ScriptsConfiguration config)
        {
            var scenarioRoot = PackagePath.ScriptsRoot;
            if (!Directory.Exists(scenarioRoot)) return Array.Empty<string>();
            var result = new List<string>();
            if (ContainsScripts(scenarioRoot)) result.Add(scenarioRoot);
            foreach (var path in Directory.GetDirectories(scenarioRoot, "*", SearchOption.AllDirectories))
                if (ContainsScripts(path))
                    result.Add(path);
            return result;

            bool ContainsScripts (string path) => Directory.GetFiles(path, "*.nani", SearchOption.TopDirectoryOnly).Length > 0;
        }

        private static void WatchDirectory (string path)
        {
            Task.Run(AddWatcher).ContinueWith(DisposeWatcher, TaskScheduler.FromCurrentSynchronizationContext());

            FileSystemWatcher AddWatcher ()
            {
                var watcher = CreateWatcher(path);
                runningWatchers.Push(watcher);
                return watcher;
            }
        }

        private static FileSystemWatcher CreateWatcher (string path)
        {
            var watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.IncludeSubdirectories = false;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.nani";
            watcher.Changed += (_, e) => modifiedScriptPaths.Enqueue(e.FullPath);
            watcher.EnableRaisingEvents = true;
            return watcher;
        }

        private static void DisposeWatcher (Task<FileSystemWatcher> startTask)
        {
            try
            {
                var watcher = startTask.Result;
                AppDomain.CurrentDomain.DomainUnload += (_, __) => { watcher.Dispose(); };
            }
            catch (Exception e)
            {
                Engine.Err($"Failed to stop script file watcher: {e.Message}");
            }
        }
    }
}
