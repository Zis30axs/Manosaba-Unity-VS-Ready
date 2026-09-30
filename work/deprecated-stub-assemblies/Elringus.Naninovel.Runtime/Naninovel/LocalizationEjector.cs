using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000389")]
    public class LocalizationEjector
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200038B")]
    	[CompilerGenerated]
    	private struct _003CEjectScripts_003Ed__5
    {
    		[Token(Token = "0x4000D2D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000D2E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000D2F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public LocalizationEjector _003C_003E4__this;

    		[Token(Token = "0x4000D30")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string outDir;

    		[Token(Token = "0x4000D31")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask<System.Collections.Generic.IReadOnlyCollection<Resource<Script>>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001138")]
    		[Address(RVA = "0x586B50", Offset = "0x585550", VA = "0x180586B50", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001139")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200038C")]
    	[CompilerGenerated]
    	private struct _003CEjectText_003Ed__6
    {
    		[Token(Token = "0x4000D32")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000D33")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000D34")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public LocalizationEjector _003C_003E4__this;

    		[Token(Token = "0x4000D35")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string outDir;

    		[Token(Token = "0x4000D36")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private LocalizableResourceLoader<TextAsset> _003CtextLoader_003E5__2;

    		[Token(Token = "0x4000D37")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<System.Collections.Generic.IReadOnlyCollection<Resource<TextAsset>>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600113A")]
    		[Address(RVA = "0x587180", Offset = "0x585B80", VA = "0x180587180", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600113B")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000D27")]
    	private const string ejectArg = "-nani-eject";

    	[Token(Token = "0x4000D28")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly IResourceProviderManager providers;

    	[Token(Token = "0x4000D29")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly ILocalizationManager l10n;

    	[Token(Token = "0x600112D")]
    	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
    	public LocalizationEjector(IResourceProviderManager providers, ILocalizationManager l10n)
    	{
    	}

    	[Token(Token = "0x600112E")]
    	[Address(RVA = "0x57C100", Offset = "0x57AB00", VA = "0x18057C100")]
    	public static bool IsEjectionRequested()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600112F")]
    	[Address(RVA = "0x585870", Offset = "0x584270", VA = "0x180585870")]
    	[AsyncStateMachine(typeof(_003CEjectScripts_003Ed__5))]
    	public UniTask EjectScripts(string outDir)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001130")]
    	[Address(RVA = "0x5859F0", Offset = "0x5843F0", VA = "0x1805859F0")]
    	[AsyncStateMachine(typeof(_003CEjectText_003Ed__6))]
    	public UniTask EjectText(string outDir)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001131")]
    	[Address(RVA = "0x585B70", Offset = "0x584570", VA = "0x180585B70")]
    	private void EjectScript(Script script, string outDir)
    	{
    	}

    	[Token(Token = "0x6001132")]
    	[Address(RVA = "0x585EB0", Offset = "0x5848B0", VA = "0x180585EB0")]
    	private void EjectText(string sourceText, string documentPath, string outDir)
    	{
    	}

    	[Token(Token = "0x6001133")]
    	[Address(RVA = "0x586390", Offset = "0x584D90", VA = "0x180586390")]
    	private string ResolveEjectionLocale()
    	{
    		return null;
    	}
    }
}
