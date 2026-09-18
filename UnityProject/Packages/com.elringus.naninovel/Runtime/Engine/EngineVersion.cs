using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Stores engine version and build number.
    /// </summary>
    public class EngineVersion : ScriptableObject
    {
        /// <summary>
        /// Version identifier of the engine release.
        /// </summary>
        public string Version => engineVersion;
        /// <summary>
        /// Whether the release is in a preview stage.
        /// </summary>
        public bool Preview => preview;
        /// <summary>
        /// Date and time the release was built.
        /// </summary>
        public string Build => buildDate;

        [SerializeField] private string engineVersion = string.Empty;
        [SerializeField] private bool preview;
        [SerializeField, ReadOnly] private string buildDate = string.Empty;

        public static EngineVersion LoadFromResources ()
        {
            const string assetPath = nameof(EngineVersion);
            return Engine.LoadInternalResource<EngineVersion>(assetPath);
        }

        public string BuildVersionTag ()
        {
            return $"{Version}.{Build.Replace("-", "")[2..]}";
        }
    }
}
