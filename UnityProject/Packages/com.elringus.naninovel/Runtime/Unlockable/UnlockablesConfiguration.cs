using UnityEngine;

namespace Naninovel
{
    [EditInProjectSettings]
    public class UnlockablesConfiguration : Configuration
    {
        public const string DefaultPathPrefix = "Unlockables";

        [Tooltip("Configuration of the resource loader used with unlockable resources.")]
        public ResourceLoaderConfiguration Loader = new() { PathPrefix = DefaultPathPrefix };
    }
}
