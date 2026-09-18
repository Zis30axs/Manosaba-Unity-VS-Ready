namespace Naninovel
{
    /// <summary>
    /// Provides implementations of the built-in debug console commands (editor-only).
    /// </summary>
    public static class ConsoleCommands
    {
        [ConsoleCommand]
        public static void Reload ()
        {
            HotReloadService.ReloadPlayedScript().Forget();
        }
    }
}
