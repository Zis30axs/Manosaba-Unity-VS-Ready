using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x200025D")]
public class LocalResourceProvider : ResourceProvider
{
	[Token(Token = "0x4000934")]
	[FieldOffset(Offset = "0x58")]
	private readonly Dictionary<Type, List<IConverter>> converters;

	[Token(Token = "0x17000184")]
	public string RootPath
	{
		[Token(Token = "0x6000A8F")]
		[Address(RVA = "0x453480", Offset = "0x451E80", VA = "0x180453480")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000A90")]
	[Address(RVA = "0x531230", Offset = "0x52FC30", VA = "0x180531230")]
	public LocalResourceProvider(string rootPath)
	{
	}

	[Token(Token = "0x6000A91")]
	public override bool SupportsType<T>()
	{
		return default(bool);
	}

	[Token(Token = "0x6000A92")]
	public virtual void AddConverter<T>(IRawConverter<T> converter)
	{
	}

	[Token(Token = "0x6000A93")]
	protected override LoadResourceRunner<T> CreateLoadResourceRunner<T>(string path)
	{
		return null;
	}

	[Token(Token = "0x6000A94")]
	protected override LocateResourcesRunner<T> CreateLocateResourcesRunner<T>(string path)
	{
		return null;
	}

	[Token(Token = "0x6000A95")]
	[Address(RVA = "0x531D30", Offset = "0x530730", VA = "0x180531D30", Slot = "39")]
	protected override LocateFoldersRunner CreateLocateFoldersRunner(string path)
	{
		return null;
	}

	[Token(Token = "0x6000A96")]
	[Address(RVA = "0x531E00", Offset = "0x530800", VA = "0x180531E00", Slot = "40")]
	protected override void DisposeResource(Resource resource)
	{
	}

	[Token(Token = "0x6000A97")]
	protected virtual IEnumerable<IRawConverter<T>> ResolveConverters<T>()
	{
		return null;
	}
}
