using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.AddressablesUtils;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using TMPro;
using UniRx;
using UnityEngine;

namespace GigaCreation.Essentials.Localization
{
    [Token(Token = "0x200000E")]
    public class LocalizationManager : ILocalizationManager, IService, ILocalizationProvider, IFontProvider
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000010")]
    	[CompilerGenerated]
    	private struct _003CChangeLocaleAsync_003Ed__17
    {
    		[Token(Token = "0x4000020")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000021")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000022")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public LocalizationManager _003C_003E4__this;

    		[Token(Token = "0x4000023")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public LocaleKind locale;

    		[Token(Token = "0x4000024")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000025")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<(TMP_FontAsset, Material[])>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600002D")]
    		[Address(RVA = "0xCD25C0", Offset = "0xCD0FC0", VA = "0x180CD25C0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600002E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000011")]
    	[CompilerGenerated]
    	private struct _003CGetOrLoadFontAssetAsync_003Ed__18
    {
    		[Token(Token = "0x4000026")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000027")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<TMP_FontAsset> _003C_003Et__builder;

    		[Token(Token = "0x4000028")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public LocalizationManager _003C_003E4__this;

    		[Token(Token = "0x4000029")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public LocaleKind locale;

    		[Token(Token = "0x400002A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public CancellationToken ct;

    		[Token(Token = "0x400002B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<TMP_FontAsset>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600002F")]
    		[Address(RVA = "0xCD29F0", Offset = "0xCD13F0", VA = "0x180CD29F0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000030")]
    		[Address(RVA = "0xCD2EF0", Offset = "0xCD18F0", VA = "0x180CD2EF0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000012")]
    	[CompilerGenerated]
    	private struct _003CGetOrLoadFontMaterialsAsync_003Ed__19
    {
    		[Token(Token = "0x400002C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400002D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<Material[]> _003C_003Et__builder;

    		[Token(Token = "0x400002E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public LocalizationManager _003C_003E4__this;

    		[Token(Token = "0x400002F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000030")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public LocaleKind locale;

    		[Token(Token = "0x4000031")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<Material[]>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000031")]
    		[Address(RVA = "0xCD2F20", Offset = "0xCD1920", VA = "0x180CD2F20", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000032")]
    		[Address(RVA = "0xCD3480", Offset = "0xCD1E80", VA = "0x180CD3480", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000018")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly IDictionary<LocaleKind, LocaleDataItem> _localeData;

    	[Token(Token = "0x4000019")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly ISubject<LocaleKind> _localeChangedSubject;

    	[Token(Token = "0x400001A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private IAddressableAssetLoader _addressableAssetLoader;

    	private readonly LocaleKind _sourceLocale;
    	private LocaleKind _currentLocale;
    	private TMP_FontAsset _currentFontAsset;

    	[Token(Token = "0x1700000C")]
    	public LocaleKind SourceLocale
    	{
    		[Token(Token = "0x6000020")]
    		[Address(RVA = "0x4403C0", Offset = "0x43EDC0", VA = "0x1804403C0", Slot = "5")]
    		[CompilerGenerated]
    		get
    		{
    			return _sourceLocale;
    		}
    	}

    	[Token(Token = "0x1700000D")]
    	public LocaleKind CurrentLocale
    	{
    		[Token(Token = "0x6000021")]
    		[Address(RVA = "0x8940D0", Offset = "0x892AD0", VA = "0x1808940D0", Slot = "6")]
    		[CompilerGenerated]
    		get
    		{
    			return _currentLocale;
    		}
    		[Token(Token = "0x6000022")]
    		[Address(RVA = "0x8940E0", Offset = "0x892AE0", VA = "0x1808940E0")]
    		[CompilerGenerated]
    		private set
    		{
    			_currentLocale = value;
    		}
    	}

    	[Token(Token = "0x1700000E")]
    	public TMP_FontAsset CurrentFontAsset
    	{
    		[Token(Token = "0x6000023")]
    		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0", Slot = "7")]
    		[CompilerGenerated]
    		get
    		{
    			return _currentFontAsset;
    		}
    		[Token(Token = "0x6000024")]
    		[Address(RVA = "0x58DF10", Offset = "0x58C910", VA = "0x18058DF10")]
    		[CompilerGenerated]
    		private set
    		{
    			_currentFontAsset = value;
    		}
    	}

    	[Token(Token = "0x1700000F")]
    	private IAddressableAssetLoader AddressableAssetLoader
    	{
    		[Token(Token = "0x6000025")]
    		[Address(RVA = "0xCD1CF0", Offset = "0xCD06F0", VA = "0x180CD1CF0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000026")]
    	[Address(RVA = "0xCD1DB0", Offset = "0xCD07B0", VA = "0x180CD1DB0")]
    	public LocalizationManager(LocaleKind sourceLocale = default, IDictionary<LocaleKind, LocaleDataItem> localeData = default)
    	{
    		_localeData = localeData;
    		_localeChangedSubject = new Subject<LocaleKind>();
    		_sourceLocale = sourceLocale;
    		CurrentLocale = sourceLocale;
    	}

    	[Token(Token = "0x6000027")]
    	[Address(RVA = "0xCD1EE0", Offset = "0xCD08E0", VA = "0x180CD1EE0", Slot = "4")]
    	[AsyncStateMachine(typeof(_003CChangeLocaleAsync_003Ed__17))]
    	public UniTask ChangeLocaleAsync(LocaleKind locale, [Optional] CancellationToken ct)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000028")]
    	[Address(RVA = "0xCD20A0", Offset = "0xCD0AA0", VA = "0x180CD20A0", Slot = "9")]
    	[AsyncStateMachine(typeof(_003CGetOrLoadFontAssetAsync_003Ed__18))]
    	public UniTask<TMP_FontAsset> GetOrLoadFontAssetAsync(LocaleKind locale, [Optional] CancellationToken ct)
    	{
    		return default(UniTask<TMP_FontAsset>);
    	}

    	[Token(Token = "0x6000029")]
    	[Address(RVA = "0xCD22B0", Offset = "0xCD0CB0", VA = "0x180CD22B0", Slot = "10")]
    	[AsyncStateMachine(typeof(_003CGetOrLoadFontMaterialsAsync_003Ed__19))]
    	public UniTask<Material[]> GetOrLoadFontMaterialsAsync(LocaleKind locale, [Optional] CancellationToken ct)
    	{
    		return default(UniTask<Material[]>);
    	}

    	[Token(Token = "0x600002A")]
    	[Address(RVA = "0xCD24C0", Offset = "0xCD0EC0", VA = "0x180CD24C0", Slot = "8")]
    	public IObservable<LocaleKind> OnLocaleChangedAsObservable()
    	{
    		return _localeChangedSubject;
    	}
    }
}
