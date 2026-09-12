using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.ManagedText;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20003B0")]
[InitializeAtRuntime(0, null)]
public class TextManager : ITextManager, IEngineService<ManagedTextConfiguration>, IEngineService
{
	[StructLayout(3)]
	[Token(Token = "0x20003B1")]
	[CompilerGenerated]
	private struct _003CHandleLocaleChanged_003Ed__19
{
		[Token(Token = "0x4000DC1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000DC2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000DC3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public TextManager _003C_003E4__this;

		[Token(Token = "0x4000DC4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60011DC")]
		[Address(RVA = "0x595980", Offset = "0x594380", VA = "0x180595980", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60011DD")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000DBA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private readonly IResourceProviderManager resources;

	[Token(Token = "0x4000DBB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private readonly ILocalizationManager l10n;

	[Token(Token = "0x4000DBC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private readonly ICommunityLocalization communityL10n;

	[Token(Token = "0x4000DBD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private readonly ScriptLocalizationParser l10nParser;

	[Token(Token = "0x4000DBE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private readonly ManagedTextFieldAssigner fieldAssigner;

	[Token(Token = "0x4000DBF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private readonly Dictionary<string, ManagedTextDocument> docByPath;

	[Token(Token = "0x4000DC0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private IResourceLoader<TextAsset> textLoader;

	[Token(Token = "0x1700026A")]
	public virtual ManagedTextConfiguration Configuration
	{
		[Token(Token = "0x60011D0")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "10")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700026B")]
	public virtual IResourceLoader DocumentLoader
	{
		[Token(Token = "0x60011D1")]
		[Address(RVA = "0x453470", Offset = "0x451E70", VA = "0x180453470", Slot = "11")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60011D2")]
	[Address(RVA = "0x594AF0", Offset = "0x5934F0", VA = "0x180594AF0")]
	public TextManager(ManagedTextConfiguration config, IResourceProviderManager resources, ILocalizationManager l10n, ICommunityLocalization communityL10n)
	{
	}

	[Token(Token = "0x60011D3")]
	[Address(RVA = "0x595010", Offset = "0x593A10", VA = "0x180595010", Slot = "12")]
	public virtual UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60011D4")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "13")]
	public virtual void ResetService()
	{
	}

	[Token(Token = "0x60011D5")]
	[Address(RVA = "0x595280", Offset = "0x593C80", VA = "0x180595280", Slot = "14")]
	public virtual void DestroyService()
	{
	}

	[Token(Token = "0x60011D6")]
	[Address(RVA = "0x595450", Offset = "0x593E50", VA = "0x180595450", Slot = "15")]
	public virtual ManagedTextDocument GetDocument(string documentPath)
	{
		return null;
	}

	[Token(Token = "0x60011D7")]
	[Address(RVA = "0x5954B0", Offset = "0x593EB0", VA = "0x1805954B0", Slot = "16")]
	protected virtual void HandleTextLoaded(Resource<TextAsset> resource)
	{
	}

	[Token(Token = "0x60011D8")]
	[Address(RVA = "0x5955C0", Offset = "0x593FC0", VA = "0x1805955C0", Slot = "17")]
	protected virtual void HandleTextUnloaded(Resource<TextAsset> resource)
	{
	}

	[Token(Token = "0x60011D9")]
	[Address(RVA = "0x595650", Offset = "0x594050", VA = "0x180595650", Slot = "18")]
	[AsyncStateMachine(typeof(_003CHandleLocaleChanged_003Ed__19))]
	protected virtual UniTask HandleLocaleChanged(LocaleChangedArgs _)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60011DA")]
	[Address(RVA = "0x595770", Offset = "0x594170", VA = "0x180595770", Slot = "19")]
	protected virtual ManagedTextDocument ParseDocument(string text, string documentPath)
	{
		return null;
	}

	[Token(Token = "0x60011DB")]
	[Address(RVA = "0x595830", Offset = "0x594230", VA = "0x180595830", Slot = "20")]
	protected virtual bool IsScriptL10nDocument(string documentPath)
	{
		return default(bool);
	}
}
