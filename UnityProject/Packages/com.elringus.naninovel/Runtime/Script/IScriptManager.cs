namespace Naninovel
{
    /// <summary>
    /// Manages scenario scripts.
    /// </summary>
    public interface IScriptManager : IEngineService<ScriptsConfiguration>
    {
        /// <summary>
        /// Manages scenario script resources.
        /// </summary>
        IResourceLoader<Script> ScriptLoader { get; }
        /// <summary>
        /// Manages external scenario script resources (community modding feature),
        /// when <see cref="ScriptsConfiguration.EnableCommunityModding"/> is enabled.
        /// </summary>
        IResourceLoader<Script> ExternalScriptLoader { get; }
        /// <summary>
        /// Total number of commands existing in all the available scenario scripts.
        /// </summary>
        /// <remarks>
        /// Updated on build and when invoking 'Naninovel/Show Project Stats' via editor menu.
        /// </remarks>
        int TotalCommandsCount { get; }
    }
}
