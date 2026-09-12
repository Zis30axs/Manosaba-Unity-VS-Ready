using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace GigaCreation.NaninovelExtender.Localization;

[Token(Token = "0x2000006")]
[UsedImplicitly]
[InitializeAtRuntime(0, typeof(Naninovel.LocalizationManager))]
public class LocalizationManagerExtended : Naninovel.LocalizationManager
{
	[StructLayout(3)]
	[Token(Token = "0x2000009")]
	[CompilerGenerated]
	private struct _003CGetAvailableLocaleTagsAsync_003Ed__14
{
		[Token(Token = "0x400000F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000010")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<IEnumerable<string>> _003C_003Et__builder;

		[Token(Token = "0x4000011")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public LocalizationManagerExtended _003C_003E4__this;

		[Token(Token = "0x4000012")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Naninovel.UniTask<IReadOnlyCollection<Folder>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000015")]
		[Address(RVA = "0xD319D0", Offset = "0xD303D0", VA = "0x180D319D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000016")]
		[Address(RVA = "0xD31EF0", Offset = "0xD308F0", VA = "0x180D31EF0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200000A")]
	[CompilerGenerated]
	private struct _003CRetrieveAvailableLocales_003Ed__12
{
		[Token(Token = "0x4000013")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000014")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000015")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public LocalizationManagerExtended _003C_003E4__this;

		[Token(Token = "0x4000016")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private Naninovel.UniTask<IEnumerable<string>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000017")]
		[Address(RVA = "0xD31F20", Offset = "0xD30920", VA = "0x180D31F20", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000018")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200000B")]
	[CompilerGenerated]
	private struct _003CSelectLocale_003Ed__10
{
		[Token(Token = "0x4000017")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000018")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000019")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public LocalizationManagerExtended _003C_003E4__this;

		[Token(Token = "0x400001A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public LocaleKind locale;

		[Token(Token = "0x400001B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Naninovel.UniTask _003Ctask_003E5__2;

		[Token(Token = "0x400001C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400001D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000019")]
		[Address(RVA = "0xD324F0", Offset = "0xD30EF0", VA = "0x180D324F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600001A")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200000C")]
	[CompilerGenerated]
	private struct _003CSelectLocale_003Ed__11
{
		[Token(Token = "0x400001E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400001F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000020")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public LocalizationManagerExtended _003C_003E4__this;

		[Token(Token = "0x4000021")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string locale;

		[Token(Token = "0x4000022")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Naninovel.UniTask _003Ctask_003E5__2;

		[Token(Token = "0x4000023")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000024")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600001B")]
		[Address(RVA = "0xD329E0", Offset = "0xD313E0", VA = "0x180D329E0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600001C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000009")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private readonly List<string> _validLocales;

	[Token(Token = "0x400000A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private GigaCreation.Essentials.Localization.ILocalizationManager _localizationManager;

	[Token(Token = "0x400000B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private IDefaultLocaleSelector _defaultLocaleSelector;

	[Token(Token = "0x17000003")]
	public IReadOnlyCollection<string> ValidLocales
	{
		[Token(Token = "0x6000008")]
		[Address(RVA = "0x453470", Offset = "0x451E70", VA = "0x180453470", Slot = "36")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000004")]
	private GigaCreation.Essentials.Localization.ILocalizationManager LocalizationManager
	{
		[Token(Token = "0x6000009")]
		[Address(RVA = "0xD310E0", Offset = "0xD2FAE0", VA = "0x180D310E0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000005")]
	private IDefaultLocaleSelector DefaultLocaleSelector
	{
		[Token(Token = "0x600000A")]
		[Address(RVA = "0xD311A0", Offset = "0xD2FBA0", VA = "0x180D311A0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600000B")]
	[Address(RVA = "0xD31260", Offset = "0xD2FC60", VA = "0x180D31260")]
	public LocalizationManagerExtended(LocalizationConfiguration cfg, IResourceProviderManager resources)
	{
	}

	[Token(Token = "0x600000C")]
	[Address(RVA = "0xD31350", Offset = "0xD2FD50", VA = "0x180D31350", Slot = "37")]
	[AsyncStateMachine(typeof(_003CSelectLocale_003Ed__10))]
	public Naninovel.UniTask SelectLocale(LocaleKind locale)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x600000D")]
	[Address(RVA = "0xD31490", Offset = "0xD2FE90", VA = "0x180D31490", Slot = "30")]
	[AsyncStateMachine(typeof(_003CSelectLocale_003Ed__11))]
	public override Naninovel.UniTask SelectLocale(string locale)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x600000E")]
	[Address(RVA = "0xD31620", Offset = "0xD30020", VA = "0x180D31620", Slot = "33")]
	[AsyncStateMachine(typeof(_003CRetrieveAvailableLocales_003Ed__12))]
	protected override Naninovel.UniTask RetrieveAvailableLocales()
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x600000F")]
	[Address(RVA = "0xD31740", Offset = "0xD30140", VA = "0x180D31740", Slot = "34")]
	protected override string GetDefaultLocale()
	{
		return null;
	}

	[Token(Token = "0x6000010")]
	[Address(RVA = "0xD317A0", Offset = "0xD301A0", VA = "0x180D317A0")]
	[AsyncStateMachine(typeof(_003CGetAvailableLocaleTagsAsync_003Ed__14))]
	private Naninovel.UniTask<IEnumerable<string>> GetAvailableLocaleTagsAsync()
	{
		return default(Naninovel.UniTask<IEnumerable<string>>);
	}
}
