namespace Naninovel
{
    /// <summary>
    /// Local resource paths of default managed text documents.
    /// </summary>
    public static class ManagedTextPaths
    {
        /// <summary>
        /// Local resource path of a managed text document for arbitrary (uncategorized) records.
        /// </summary>
        public const string Default = "Uncategorized";
        /// <summary>
        /// Local resource path of a managed text document for script constants (accessed via 't_name` in script expressions).
        /// </summary>
        public const string ScriptConstants = "Script";
        /// <summary>
        /// Local resource path of a managed text document for localization tags (language names).
        /// </summary>
        public const string Locales = "Locales";
        /// <summary>
        /// Local resource path of a managed text document for character display names.
        /// </summary>
        public const string DisplayNames = "CharacterNames";
        /// <summary>
        /// Local resource path of a managed text document for unlockable tips documents.
        /// </summary>
        public const string Tips = "Tips";
        /// <summary>
        /// Local resource path prefix of all the script localization documents.
        /// </summary>
        public const string ScriptLocalizationPrefix = "Scripts";
    }
}
