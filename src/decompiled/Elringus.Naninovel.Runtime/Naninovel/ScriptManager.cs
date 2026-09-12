using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000424")]
[InitializeAtRuntime(0, null)]
public class ScriptManager : IScriptManager, IEngineService<ScriptsConfiguration>, IEngineService
{
	[Token(Token = "0x4000F23")]
	[FieldOffset(Offset = "0x20")]
	private readonly IResourceProviderManager providers;

	[Token(Token = "0x4000F24")]
	[FieldOffset(Offset = "0x28")]
	private ResourceLoader<Script> scriptLoader;

	[Token(Token = "0x4000F25")]
	[FieldOffset(Offset = "0x30")]
	private ResourceLoader<Script> extScriptLoader;

	[Token(Token = "0x170002CE")]
	public virtual ScriptsConfiguration Configuration
	{
		[Token(Token = "0x6001448")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "11")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002CF")]
	public IResourceLoader<Script> ScriptLoader
	{
		[Token(Token = "0x6001449")]
		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002D0")]
	public IResourceLoader<Script> ExternalScriptLoader
	{
		[Token(Token = "0x600144A")]
		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0", Slot = "5")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002D1")]
	public virtual int TotalCommandsCount
	{
		[Token(Token = "0x600144B")]
		[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590", Slot = "12")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
		[Token(Token = "0x600144C")]
		[Address(RVA = "0x39B780", Offset = "0x39A180", VA = "0x18039B780")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x600144D")]
	[Address(RVA = "0x5BAC90", Offset = "0x5B9690", VA = "0x1805BAC90")]
	public ScriptManager(ScriptsConfiguration config, IResourceProviderManager providers)
	{
	}

	[Token(Token = "0x600144E")]
	[Address(RVA = "0x5BAD40", Offset = "0x5B9740", VA = "0x1805BAD40", Slot = "13")]
	public virtual UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x600144F")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "14")]
	public virtual void ResetService()
	{
	}

	[Token(Token = "0x6001450")]
	[Address(RVA = "0x5BAF50", Offset = "0x5B9950", VA = "0x1805BAF50", Slot = "15")]
	public virtual void DestroyService()
	{
	}
}
