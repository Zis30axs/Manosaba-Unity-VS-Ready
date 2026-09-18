namespace Naninovel
{
    /// <summary>
    /// Used by the internal Naninovel systems to log messages.
    /// </summary>
    public interface ILogger
    {
        void Log (string message);
        void Warn (string message);
        void Err (string message);
    }
}
