namespace Naninovel.Commands
{
    /// <summary>
    /// A base implementation for the audio-related commands.
    /// </summary>
    public abstract class AudioCommand : Command
    {
        protected IAudioManager AudioManager => Engine.GetServiceOrErr<IAudioManager>();
    }
}
