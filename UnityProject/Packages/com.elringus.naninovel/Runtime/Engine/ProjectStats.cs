using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Information about the Naninovel project resolved on build and when
    /// running 'Naninovel/Show Project Stats'.
    /// </summary>
    public class ProjectStats : ScriptableObject
    {
        /// <summary>
        /// Asset path relative to a 'Resources' folder, w/o the file extension.
        /// </summary>
        public const string ResourcesPath = "Naninovel/ProjectStats";

        /// <summary>
        /// Total number of scenario script assets under the scenario root.
        /// </summary>
        public int TotalScriptsCount;
        /// <summary>
        /// Total number of commands in all the scenario scripts.
        /// </summary>
        public int TotalCommandCount;
        /// <summary>
        /// Total number of words in 'natural' text (directly shown to player,
        /// such as printed messages and choices) in all the scenario scripts.
        /// </summary>
        public int TotalWordsCount;

        /// <summary>
        /// Loads existing or creates new default instance of the asset.
        /// </summary>
        public static ProjectStats GetOrDefault ()
        {
            return Resources.Load<ProjectStats>(ResourcesPath) ?? CreateInstance<ProjectStats>();
        }
    }
}
