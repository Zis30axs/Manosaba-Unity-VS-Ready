namespace Naninovel
{
    /// <summary>
    /// Implementation is able to create <see cref="Script"/> asset from text string.
    /// </summary>
    public interface IScriptParser
    {
        /// <summary>
        /// Creates a new script asset instance by parsing specified script text.
        /// </summary>
        /// <param name="scriptPath">Unique (project-wide) local resource path of the script asset.</param>
        /// <param name="scriptText">The script text to parse.</param>
        /// <param name="options">Optional configuration of the parse behaviour.</param>
        Script ParseText (string scriptPath, string scriptText, ParseOptions options = default);
    }
}
