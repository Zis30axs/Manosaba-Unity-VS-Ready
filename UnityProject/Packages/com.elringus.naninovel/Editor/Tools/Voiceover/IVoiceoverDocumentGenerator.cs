namespace Naninovel
{
    public interface IVoiceoverDocumentGenerator
    {
        void GenerateVoiceoverDocument (ScriptPlaylist list, string locale, string outDir);
    }
}
