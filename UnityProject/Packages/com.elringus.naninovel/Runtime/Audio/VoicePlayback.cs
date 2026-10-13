namespace Naninovel
{
    /// <summary>One voice in an explicitly concurrent, jointly managed playback group.</summary>
    public readonly struct VoicePlayback
    {
        public string Path { get; }
        public float Volume { get; }
        public string Group { get; }
        public string AuthorId { get; }

        public VoicePlayback(string path, float volume = 1f, string group = null, string authorId = null)
        {
            Path = path;
            Volume = volume;
            Group = group;
            AuthorId = authorId;
        }
    }
}
