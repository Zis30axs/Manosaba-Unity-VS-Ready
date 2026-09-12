using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20003C7")]
[InitializeAtRuntime(0, null)]
public class ResourceProviderManager : IResourceProviderManager, IEngineService<ResourceProviderConfiguration>, IEngineService, IHoldersTracker
{
	[Token(Token = "0x4000E36")]
	[FieldOffset(Offset = "0x20")]
	private readonly Dictionary<string, IResourceProvider> providersMap;

	[Token(Token = "0x4000E37")]
	[FieldOffset(Offset = "0x28")]
	private readonly Dictionary<UnityEngine.Object, HashSet<object>> holdersMap;

	[Token(Token = "0x17000277")]
	public virtual ResourceProviderConfiguration Configuration
	{
		[Token(Token = "0x6001243")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "17")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x14000063")]
	public event Action<string> OnProviderMessage
	{
		[Token(Token = "0x6001241")]
		[Address(RVA = "0x59B970", Offset = "0x59A370", VA = "0x18059B970", Slot = "4")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6001242")]
		[Address(RVA = "0x59BA80", Offset = "0x59A480", VA = "0x18059BA80", Slot = "5")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6001244")]
	[Address(RVA = "0x59BB90", Offset = "0x59A590", VA = "0x18059BB90")]
	public ResourceProviderManager(ResourceProviderConfiguration config)
	{
	}

	[Token(Token = "0x6001245")]
	[Address(RVA = "0x59BD60", Offset = "0x59A760", VA = "0x18059BD60", Slot = "18")]
	public virtual UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001246")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "19")]
	public virtual void ResetService()
	{
	}

	[Token(Token = "0x6001247")]
	[Address(RVA = "0x59BEB0", Offset = "0x59A8B0", VA = "0x18059BEB0", Slot = "20")]
	public virtual void DestroyService()
	{
	}

	[Token(Token = "0x6001248")]
	[Address(RVA = "0x59C070", Offset = "0x59AA70", VA = "0x18059C070", Slot = "21")]
	public virtual bool IsProviderInitialized(string providerType)
	{
		return default(bool);
	}

	[Token(Token = "0x6001249")]
	[Address(RVA = "0x59C0D0", Offset = "0x59AAD0", VA = "0x18059C0D0", Slot = "22")]
	public virtual IResourceProvider GetProvider(string providerType)
	{
		return null;
	}

	[Token(Token = "0x600124A")]
	[Address(RVA = "0x59C1C0", Offset = "0x59ABC0", VA = "0x18059C1C0", Slot = "23")]
	public virtual void GetProviders(IList<IResourceProvider> providers, IReadOnlyList<string> types)
	{
	}

	[Token(Token = "0x600124B")]
	[Address(RVA = "0x59C480", Offset = "0x59AE80", VA = "0x18059C480", Slot = "24")]
	public virtual int Hold(UnityEngine.Object obj, object holder)
	{
		return default(int);
	}

	[Token(Token = "0x600124C")]
	[Address(RVA = "0x59C610", Offset = "0x59B010", VA = "0x18059C610", Slot = "25")]
	public virtual int Release(UnityEngine.Object obj, object holder)
	{
		return default(int);
	}

	[Token(Token = "0x600124D")]
	[Address(RVA = "0x59C750", Offset = "0x59B150", VA = "0x18059C750", Slot = "26")]
	public virtual int CountHolders(UnityEngine.Object obj)
	{
		return default(int);
	}

	[Token(Token = "0x600124E")]
	[Address(RVA = "0x59C860", Offset = "0x59B260", VA = "0x18059C860", Slot = "27")]
	protected virtual IResourceProvider InitializeProjectProvider()
	{
		return null;
	}

	[Token(Token = "0x600124F")]
	[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "28")]
	protected virtual IResourceProvider InitializeGoogleDriveProvider()
	{
		return null;
	}

	[Token(Token = "0x6001250")]
	[Address(RVA = "0x59C8E0", Offset = "0x59B2E0", VA = "0x18059C8E0", Slot = "29")]
	protected virtual IResourceProvider InitializeLocalProvider()
	{
		return null;
	}

	[Token(Token = "0x6001251")]
	[Address(RVA = "0x59CA50", Offset = "0x59B450", VA = "0x18059CA50", Slot = "30")]
	protected virtual IResourceProvider InitializeAddressableProvider()
	{
		return null;
	}

	[Token(Token = "0x6001252")]
	[Address(RVA = "0x59CB70", Offset = "0x59B570", VA = "0x18059CB70", Slot = "31")]
	protected virtual IResourceProvider InitializeProvider(string providerType)
	{
		return null;
	}

	[Token(Token = "0x6001253")]
	[Address(RVA = "0x59D0D0", Offset = "0x59BAD0", VA = "0x18059D0D0")]
	private void HandleProviderMessage(IResourceProvider provider, string message)
	{
	}

	[Token(Token = "0x6001254")]
	[Address(RVA = "0x59D1A0", Offset = "0x59BBA0", VA = "0x18059D1A0")]
	private HashSet<object> GetHolders(UnityEngine.Object obj)
	{
		return null;
	}
}
