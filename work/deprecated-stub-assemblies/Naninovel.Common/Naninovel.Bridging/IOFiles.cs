#nullable enable annotations
﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Il2CppDummyDll;

namespace Naninovel.Bridging
{
    [Token(Token = "0x20000C8")]
    [ExcludeFromCodeCoverage]
    public class IOFiles : IFiles, IDisposable
    {
    	[Token(Token = "0x400024A")]
    	[FieldOffset(Offset = "0x10")]
    	[CompilerGenerated]
    	private string _003Cdir_003EP;

    	[Token(Token = "0x400024B")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly SynchronizationContext ctx;

    	[Token(Token = "0x400024C")]
    	[FieldOffset(Offset = "0x20")]
    	private FileSystemWatcher? watcher;

    	[Token(Token = "0x600040B")]
    	[Address(RVA = "0x1104250", Offset = "0x1102C50", VA = "0x181104250")]
    	public IOFiles(string dir)
    	{
    	}

    	[Token(Token = "0x600040C")]
    	[Address(RVA = "0x1104320", Offset = "0x1102D20", VA = "0x181104320", Slot = "4")]
    	public Task<string> Read(string name)
    	{
    		return null;
    	}

    	[Token(Token = "0x600040D")]
    	[Address(RVA = "0x11043F0", Offset = "0x1102DF0", VA = "0x1811043F0", Slot = "5")]
    	public Task Write(string name, string content)
    	{
    		return null;
    	}

    	[Token(Token = "0x600040E")]
    	[Address(RVA = "0x11044A0", Offset = "0x1102EA0", VA = "0x1811044A0", Slot = "6")]
    	public Task<IReadOnlyCollection<string>> List()
    	{
    		return null;
    	}

    	[Token(Token = "0x600040F")]
    	[Address(RVA = "0x1104800", Offset = "0x1103200", VA = "0x181104800", Slot = "7")]
    	public void Watch(HandleFileChange handler)
    	{
    	}

    	[Token(Token = "0x6000410")]
    	[Address(RVA = "0x1104BC0", Offset = "0x11035C0", VA = "0x181104BC0", Slot = "8")]
    	public void Dispose()
    	{
    	}
    }
}
