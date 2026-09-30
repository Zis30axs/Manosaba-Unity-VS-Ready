using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel
{
    [Token(Token = "0x200039F")]
    [InitializeAtRuntime(0, null)]
    public class TextLocalizer : ITextLocalizer, IEngineService
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20003A0")]
    	[CompilerGenerated]
    	private struct _003CHandleLocaleChanged_003Ed__20
    {
    		[Token(Token = "0x4000D7F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000D80")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000D81")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TextLocalizer _003C_003E4__this;

    		[Token(Token = "0x4000D82")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public LocaleChangedArgs args;

    		[Token(Token = "0x4000D83")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60011A3")]
    		[Address(RVA = "0x5907F0", Offset = "0x58F1F0", VA = "0x1805907F0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60011A4")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20003A1")]
    	[CompilerGenerated]
    	private struct _003CLoad_003Ed__10
    {
    		[Token(Token = "0x4000D84")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000D85")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000D86")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public LocalizableText text;

    		[Token(Token = "0x4000D87")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public object holder;

    		[Token(Token = "0x4000D88")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public TextLocalizer _003C_003E4__this;

    		[Token(Token = "0x4000D89")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private PooledObject<HashSet<string>> _003C__003E5__2;

    		[Token(Token = "0x4000D8A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private PooledObject<List<UniTask>> _003C___003E5__3;

    		[Token(Token = "0x4000D8B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60011A5")]
    		[Address(RVA = "0x590C50", Offset = "0x58F650", VA = "0x180590C50", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60011A6")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20003A2")]
    	[CompilerGenerated]
    	private struct _003CLoadL10nDocumentForScript_003Ed__14
    {
    		[Token(Token = "0x4000D8C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000D8D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000D8E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public string scriptPath;

    		[Token(Token = "0x4000D8F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public TextLocalizer _003C_003E4__this;

    		[Token(Token = "0x4000D90")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public LocalizableTextHolder holder;

    		[Token(Token = "0x4000D91")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<Resource>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60011A7")]
    		[Address(RVA = "0x591620", Offset = "0x590020", VA = "0x180591620", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60011A8")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000D79")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly HashSet<LocalizableTextHolder> holders;

    	[Token(Token = "0x4000D7A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly StringBuilder builder;

    	[Token(Token = "0x4000D7B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private readonly ITextManager docs;

    	[Token(Token = "0x4000D7C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private readonly IScriptManager scripts;

    	[Token(Token = "0x4000D7D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	private readonly ILocalizationManager l10n;

    	[Token(Token = "0x4000D7E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private readonly ICommunityLocalization communityL10n;

    	[Token(Token = "0x6001192")]
    	[Address(RVA = "0x58E0A0", Offset = "0x58CAA0", VA = "0x18058E0A0")]
    	public TextLocalizer(ITextManager docs, IScriptManager scripts, ILocalizationManager l10n, ICommunityLocalization communityL10n)
    	{
    	}

    	[Token(Token = "0x6001193")]
    	[Address(RVA = "0x58E350", Offset = "0x58CD50", VA = "0x18058E350", Slot = "11")]
    	public virtual UniTask InitializeService()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001194")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "12")]
    	public virtual void ResetService()
    	{
    	}

    	[Token(Token = "0x6001195")]
    	[Address(RVA = "0x58E420", Offset = "0x58CE20", VA = "0x18058E420", Slot = "13")]
    	public virtual void DestroyService()
    	{
    	}

    	[Token(Token = "0x6001196")]
    	[Address(RVA = "0x58E560", Offset = "0x58CF60", VA = "0x18058E560", Slot = "14")]
    	[AsyncStateMachine(typeof(_003CLoad_003Ed__10))]
    	public virtual UniTask Load(LocalizableText text, object holder)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001197")]
    	[Address(RVA = "0x58E750", Offset = "0x58D150", VA = "0x18058E750", Slot = "15")]
    	public virtual void Hold(LocalizableText text, object holder)
    	{
    	}

    	[Token(Token = "0x6001198")]
    	[Address(RVA = "0x58EBB0", Offset = "0x58D5B0", VA = "0x18058EBB0", Slot = "16")]
    	public virtual void Release(LocalizableText text, object holder)
    	{
    	}

    	[Token(Token = "0x6001199")]
    	[Address(RVA = "0x58F010", Offset = "0x58DA10", VA = "0x18058F010", Slot = "17")]
    	public virtual string Resolve(LocalizableText text)
    	{
    		return null;
    	}

    	[Token(Token = "0x600119A")]
    	[Address(RVA = "0x58F660", Offset = "0x58E060", VA = "0x18058F660", Slot = "18")]
    	[AsyncStateMachine(typeof(_003CLoadL10nDocumentForScript_003Ed__14))]
    	protected virtual UniTask LoadL10nDocumentForScript(string scriptPath, LocalizableTextHolder holder)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600119B")]
    	[Address(RVA = "0x58F850", Offset = "0x58E250", VA = "0x18058F850", Slot = "19")]
    	protected virtual string Resolve(string id, string scriptPath)
    	{
    		return null;
    	}

    	[Token(Token = "0x600119C")]
    	[Address(RVA = "0x58F8D0", Offset = "0x58E2D0", VA = "0x18058F8D0", Slot = "20")]
    	protected virtual string ResolveFromScript(string id, string scriptPath)
    	{
    		return null;
    	}

    	[Token(Token = "0x600119D")]
    	[Address(RVA = "0x58FDA0", Offset = "0x58E7A0", VA = "0x18058FDA0", Slot = "21")]
    	protected virtual string ResolveFromDocument(string id, string scriptPath)
    	{
    		return null;
    	}

    	[Token(Token = "0x600119E")]
    	[Address(RVA = "0x590290", Offset = "0x58EC90", VA = "0x180590290", Slot = "22")]
    	protected virtual HashSet<string> CollectScriptPaths(LocalizableText text, HashSet<string> into)
    	{
    		return null;
    	}

    	[Token(Token = "0x600119F")]
    	[Address(RVA = "0x590550", Offset = "0x58EF50", VA = "0x180590550", Slot = "23")]
    	protected virtual bool UsingL10nDocuments()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60011A0")]
    	[Address(RVA = "0x5905C0", Offset = "0x58EFC0", VA = "0x1805905C0", Slot = "24")]
    	[AsyncStateMachine(typeof(_003CHandleLocaleChanged_003Ed__20))]
    	protected virtual UniTask HandleLocaleChanged(LocaleChangedArgs args)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60011A1")]
    	[Address(RVA = "0x590740", Offset = "0x58F140", VA = "0x180590740")]
    	private static string ToL10nPath(string scriptPath)
    	{
    		return null;
    	}
    }
}
