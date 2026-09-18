using Naninovel.Metadata;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to provide various project-specific metadata
    /// for external tools (IDE extension, web editor, etc).
    /// </summary>
    /// <remarks>
    /// Implementation is expected to have parameterless constructor.
    /// When no custom implementation is found, default is executed.
    /// </remarks>
    public interface IMetadataProvider
    {
        /// <summary>
        /// Returns project metadata.
        /// </summary>
        Project GetMetadata ();
    }
}
