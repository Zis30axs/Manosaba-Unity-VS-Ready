using System.Collections.Generic;

namespace Naninovel
{
    /// <summary>
    /// Parameters required for engine initialization.
    /// </summary>
    public class EngineParams
    {
        /// <summary>
        /// Resolves <see cref="Configuration"/> objects.
        /// </summary>
        public IConfigurationProvider ConfigurationProvider { get; set; }
        /// <summary>
        /// Proxy <see cref="MonoBehaviour"/> to be used by the engine.
        /// </summary>
        public IEngineBehaviour Behaviour { get; set; }
        /// <summary>
        /// Time service to be used by the engine.
        /// </summary>
        public ITime Time { get; set; }
        /// <summary>
        /// List of engine services, in order of initialization.
        /// </summary>
        public IList<IEngineService> Services { get; set; }
    }
}
