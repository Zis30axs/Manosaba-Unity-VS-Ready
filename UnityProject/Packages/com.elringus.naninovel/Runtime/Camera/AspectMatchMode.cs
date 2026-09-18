
namespace Naninovel
{
    /// <summary>
    /// Controls the mode in which to match the camera size.
    /// </summary>
    public enum AspectMatchMode
    {
        /// <summary>
        /// Crop to ensure no 'black bars' are visible.
        /// </summary>
        Crop,
        /// <summary>
        /// Fit without cropping, but 'black bars' will appear.
        /// </summary>
        Fit,
        /// <summary>
        /// Match either width or height with a custom ratio.
        /// </summary>
        Custom,
        /// <summary>
        /// Don't perform any matching.
        /// </summary>
        Disable
    }
}

