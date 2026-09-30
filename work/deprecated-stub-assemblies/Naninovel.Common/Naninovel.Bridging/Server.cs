using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Bridging
{
    [Token(Token = "0x20000D0")]
    public class Server
    {
    	[Token(Token = "0x4000260")]
    	[FieldOffset(Offset = "0x10")]
    	[CompilerGenerated]
    	private ISerializer _003Cserde_003EP;

    	[Token(Token = "0x4000262")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly Transport transport;

    	[Token(Token = "0x14000004")]
    	public event Action<PlaybackSpot>? OnGotoRequested
    	{
    		[Token(Token = "0x600042A")]
    		[Address(RVA = "0x1104F70", Offset = "0x1103970", VA = "0x181104F70")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x600042B")]
    		[Address(RVA = "0x1105080", Offset = "0x1103A80", VA = "0x181105080")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x6000429")]
    	[Address(RVA = "0x1104E20", Offset = "0x1103820", VA = "0x181104E20")]
    	public Server(IFiles files, ISerializer serde)
    	{
    	}

    	[Token(Token = "0x600042C")]
    	[Address(RVA = "0x1105190", Offset = "0x1103B90", VA = "0x181105190")]
    	public void Start(ServerInfo info)
    	{
    	}

    	[Token(Token = "0x600042D")]
    	[Address(RVA = "0x1105330", Offset = "0x1103D30", VA = "0x181105330")]
    	public void NotifyMetadataUpdated()
    	{
    	}

    	[Token(Token = "0x600042E")]
    	[Address(RVA = "0x1105390", Offset = "0x1103D90", VA = "0x181105390")]
    	public void NotifyPlaybackStatusChanged(PlaybackStatus status)
    	{
    	}

    	[Token(Token = "0x600042F")]
    	[Address(RVA = "0x11054D0", Offset = "0x1103ED0", VA = "0x1811054D0")]
    	private void HandleMessage(Message message)
    	{
    	}
    }
}
