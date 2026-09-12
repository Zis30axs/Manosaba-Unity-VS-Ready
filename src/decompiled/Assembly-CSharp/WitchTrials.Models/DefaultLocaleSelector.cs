using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Localization;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using Newtonsoft.Json;

namespace WitchTrials.Models;

[Token(Token = "0x200016C")]
public class DefaultLocaleSelector : IDefaultLocaleSelector, IService
{
	[Token(Token = "0x200016D")]
	public class ConfigJson
	{
		[Token(Token = "0x40006E9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		[JsonProperty("available_text_lang")]
		public IList<string> AvailableTextLang;

		[Token(Token = "0x40006EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		[JsonProperty("unavailable_lang_fallback")]
		public IDictionary<string, string> UnavailableLangFallback;

		[Token(Token = "0x600062E")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public ConfigJson()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200016F")]
	[CompilerGenerated]
	private struct _003CLoadAsync_003Ed__9
{
		[Token(Token = "0x40006EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40006EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40006F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CancellationToken ct;

		[Token(Token = "0x40006F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public DefaultLocaleSelector _003C_003E4__this;

		[Token(Token = "0x40006F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private string _003Ckey_003E5__2;

		[Token(Token = "0x40006F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private string _003CjsonPath_003E5__3;

		[Token(Token = "0x40006F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private TaskAwaiter<string> _003C_003Eu__1;

		[Token(Token = "0x6000633")]
		[Address(RVA = "0x3982A0", Offset = "0x396CA0", VA = "0x1803982A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000634")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x170000FD")]
	public LocaleKind DefaultLocale
	{
		[Token(Token = "0x6000628")]
		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650", Slot = "4")]
		[CompilerGenerated]
		get
		{
			return default(LocaleKind);
		}
		[Token(Token = "0x6000629")]
		[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x170000FE")]
	public IList<string> AvailableTextLanguages
	{
		[Token(Token = "0x600062A")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "5")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x600062B")]
		[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x600062C")]
	[Address(RVA = "0x398000", Offset = "0x396A00", VA = "0x180398000")]
	[AsyncStateMachine(typeof(_003CLoadAsync_003Ed__9))]
	public UniTask LoadAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600062D")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public DefaultLocaleSelector()
	{
	}
}
