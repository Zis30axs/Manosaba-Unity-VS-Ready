using System;

namespace Naninovel
{
    /// <summary>
    /// Handles pre-/loading and unloading resources associated with scenario scripts.
    /// </summary>
    public interface IScriptLoader : IEngineService
    {
        /// <summary>
        /// Event invoked when script load progress is changed, in 0.0 to 1.0 range.
        /// </summary>
        event Action<float> OnLoadProgress;

        /// <summary>
        /// Loads resources associated with specified local script path and unloads resources associated
        /// with the previously loaded scripts in accordance with <see cref="ResourcePolicy"/>.
        /// </summary>
        /// <param name="playlist">Local resource path of the script to preload.</param>
        /// <param name="startIndex">Playlist index of the script to start loading from.</param>
        UniTask Load (string scriptPath, int startIndex = 0);
    }
}
