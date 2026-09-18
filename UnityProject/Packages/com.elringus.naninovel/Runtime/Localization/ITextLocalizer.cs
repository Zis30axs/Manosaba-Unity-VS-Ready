namespace Naninovel
{
    /// <summary>
    /// Allows resolving localized strings associated with <see cref="LocalizableText"/>.
    /// </summary>
    public interface ITextLocalizer : IEngineService
    {
        /// <summary>
        /// Preloads resources required to resolve localization for specified text.
        /// When <paramref name="holder"/> is specified, will as well <see cref="Hold"/> the resources.
        /// </summary>
        UniTask Load (LocalizableText text, object holder = null);
        /// <summary>
        /// Holds resources required to resolve localization for specified text.
        /// </summary>
        void Hold (LocalizableText text, object holder);
        /// <summary>
        /// Releases and unloads (when no other holders) resources required to resolve localization for specified text.
        /// </summary>
        void Release (LocalizableText text, object holder);
        /// <summary>
        /// Returns localized string (translation) associated with the specified localizable text.
        /// </summary>
        /// <remarks>
        /// Make sure to <see cref="Load"/> the localizable text reference before using this method. 
        /// </remarks>
        string Resolve (LocalizableText text);
    }
}
