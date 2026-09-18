using Naninovel.Async;
using UnityEditor;

namespace Naninovel
{
    /// <summary>
    /// Initializes Naninovel systems in order when editor starts or recompiles scripts.
    /// </summary>
    [InitializeOnLoad]
    public static class InitializeOnLoad
    {
        static InitializeOnLoad ()
        {
            PlayerLoopHelper.InitOnEditor();
            TypesResolver.Resolve();
            Compiler.Initialize();
            EditorResources.InitializeEditorProvider();
            ScriptFileWatcher.Initialize();
            HotReloadService.Initialize();
            BuildProcessor.Initialize();
            AboutWindow.FirstTimeSetup();
            Engine.EditorTransientRoot = PackagePath.TransientDataPath;
            // Delay, as otherwise it throws when accessing engine config on first package import.
            EditorApplication.delayCall += BridgingService.RestartServer;
        }
    }
}
