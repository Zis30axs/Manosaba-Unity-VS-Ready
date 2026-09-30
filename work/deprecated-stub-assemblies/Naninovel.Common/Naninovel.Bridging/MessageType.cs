using Il2CppDummyDll;

namespace Naninovel.Bridging
{
    [Token(Token = "0x20000CC")]
    public enum MessageType
    {
    	[Token(Token = "0x4000255")]
    	ServerAnnounced,
    	[Token(Token = "0x4000256")]
    	MetadataUpdated,
    	[Token(Token = "0x4000257")]
    	PlaybackStatusChanged,
    	[Token(Token = "0x4000258")]
    	GotoRequested
    }
}
