namespace Naninovel
{
    /// <summary>
    /// Scope of custom variable lifetime.
    /// </summary>
    public enum CustomVariableScope
    {
        /// <summary>
        /// The variable lives inside local game session and resets
        /// when new game is started.
        /// </summary>
        Local,
        /// <summary>
        /// The variable lives across game sessions and never resets.
        /// </summary>
        Global
    }
}
