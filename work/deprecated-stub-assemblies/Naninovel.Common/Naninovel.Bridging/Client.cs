#nullable enable annotations
﻿using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Bridging
{
    [Token(Token = "0x20000C5")]
    public class Client
    {
    	[Token(Token = "0x4000245")]
    	[FieldOffset(Offset = "0x10")]
    	[CompilerGenerated]
    	private ISerializer _003Cserde_003EP;

    	[Token(Token = "0x4000249")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly Transport transport;

    	[Token(Token = "0x14000001")]
    	public event Action<ServerInfo>? OnServerAnnounced
    	{
    		[Token(Token = "0x60003FA")]
    		[Address(RVA = "0x1103920", Offset = "0x1102320", VA = "0x181103920")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60003FB")]
    		[Address(RVA = "0x1103A30", Offset = "0x1102430", VA = "0x181103A30")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000002")]
    	public event Action? OnMetadataUpdated
    	{
    		[Token(Token = "0x60003FC")]
    		[Address(RVA = "0x1103B40", Offset = "0x1102540", VA = "0x181103B40")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60003FD")]
    		[Address(RVA = "0x1103C30", Offset = "0x1102630", VA = "0x181103C30")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000003")]
    	public event Action<PlaybackStatus>? OnPlaybackStatusChanged
    	{
    		[Token(Token = "0x60003FE")]
    		[Address(RVA = "0x1103D20", Offset = "0x1102720", VA = "0x181103D20")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60003FF")]
    		[Address(RVA = "0x1103E30", Offset = "0x1102830", VA = "0x181103E30")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x60003F9")]
    	[Address(RVA = "0x11037E0", Offset = "0x11021E0", VA = "0x1811037E0")]
    	public Client(string id, IFiles files, ISerializer serde)
    	{
    	}

    	[Token(Token = "0x6000400")]
    	[Address(RVA = "0x1103F40", Offset = "0x1102940", VA = "0x181103F40")]
    	public void Start()
    	{
    	}

    	[Token(Token = "0x6000401")]
    	[Address(RVA = "0x1103FE0", Offset = "0x11029E0", VA = "0x181103FE0")]
    	public void RequestGoto(PlaybackSpot spot)
    	{
    	}

    	[Token(Token = "0x6000402")]
    	[Address(RVA = "0x1104120", Offset = "0x1102B20", VA = "0x181104120")]
    	private void HandleMessage(Message message)
    	{
    	}
    }
}
