using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.AddressablesUtils;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WitchTrials.Models;

[Serializable]
[Token(Token = "0x20001A1")]
public sealed class AuthorTextBuilder
{
	[Serializable]
	[Token(Token = "0x20001A2")]
	public sealed class Settings
	{
		[Token(Token = "0x4000791")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		[Header("Assets")]
		[SerializeField]
		private AssetReference _characterDataAsset;

		[Token(Token = "0x4000792")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		[SerializeField]
		private AssetReference _authorDataAsset;

		[Token(Token = "0x4000793")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		[Header("Parameters")]
		[SerializeField]
		private float _firstInitialFontSize;

		[Token(Token = "0x4000794")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		[SerializeField]
		private float _firstBodyFontSize;

		[Token(Token = "0x4000795")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		[SerializeField]
		private float _secondInitialFontSize;

		[Token(Token = "0x4000796")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		[SerializeField]
		private float _secondBodyFontSize;

		[Token(Token = "0x600072A")]
		[Address(RVA = "0x3A4810", Offset = "0x3A3210", VA = "0x1803A4810")]
		public void InjectTo(AuthorTextBuilder writer)
		{
		}

		[Token(Token = "0x600072B")]
		[Address(RVA = "0x3A4900", Offset = "0x3A3300", VA = "0x1803A4900")]
		public Settings()
		{
		}
	}

	[Token(Token = "0x20001A3")]
	private record FullName
	{
		[Token(Token = "0x17000131")]
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[Token(Token = "0x600072D")]
			[Address(RVA = "0x3A4920", Offset = "0x3A3320", VA = "0x1803A4920", Slot = "5")]
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 get (shared reference-field getter, +0x10); setter shares a generic store body
		[Token(Token = "0x17000132")]
		public string Name
		{
			[Token(Token = "0x600072E")]
			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
			[CompilerGenerated]
			get;
			[Token(Token = "0x600072F")]
			[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
			[CompilerGenerated]
			set;
		}

		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 get (shared reference-field getter, +0x18); setter shares a generic store body
		[Token(Token = "0x17000133")]
		public string FamilyName
		{
			[Token(Token = "0x6000730")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
			[CompilerGenerated]
			get;
			[Token(Token = "0x6000731")]
			[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
			[CompilerGenerated]
			set;
		}

		// RECONSTRUCTED | Confidence: A | RVA: 0x36C0D0 (shared two-reference-store body: +0x10, +0x18)
		[Token(Token = "0x600072C")]
		[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
		public FullName([NotNull] string Name, [NotNull] string FamilyName)
		{
			this.Name = Name;
			this.FamilyName = FamilyName;
		}

		[Token(Token = "0x6000732")]
		[Address(RVA = "0x3A49A0", Offset = "0x3A33A0", VA = "0x1803A49A0", Slot = "3")]
		[CompilerGenerated]
		public override string ToString()
		{
			return null;
		}

		[Token(Token = "0x6000733")]
		[Address(RVA = "0x3A4AE0", Offset = "0x3A34E0", VA = "0x1803A4AE0", Slot = "6")]
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			return default(bool);
		}

		[Token(Token = "0x6000736")]
		[Address(RVA = "0x3A4BC0", Offset = "0x3A35C0", VA = "0x1803A4BC0", Slot = "2")]
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return default(int);
		}

		[Token(Token = "0x6000738")]
		[Address(RVA = "0x3A4D70", Offset = "0x3A3770", VA = "0x1803A4D70", Slot = "7")]
		[CompilerGenerated]
		public virtual bool Equals(FullName? other)
		{
			return default(bool);
		}

		// RECONSTRUCTED | Confidence: A | RVA: 0x36C8D0 (shared record copy-ctor body, two reference fields)
		[Token(Token = "0x600073A")]
		[Address(RVA = "0x36C8D0", Offset = "0x36B2D0", VA = "0x18036C8D0")]
		[CompilerGenerated]
		protected FullName(FullName original)
		{
			Name = original.Name;
			FamilyName = original.FamilyName;
		}

		// RECONSTRUCTED | Confidence: A | RVA: 0x36C9B0 (shared Deconstruct body, two reference fields)
		[Token(Token = "0x600073B")]
		[Address(RVA = "0x36C9B0", Offset = "0x36B3B0", VA = "0x18036C9B0")]
		[CompilerGenerated]
		public void Deconstruct(out string Name, out string FamilyName)
		{
			Name = this.Name;
			FamilyName = this.FamilyName;
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001A6")]
	[CompilerGenerated]
	private struct _003CInitializeAsync_003Ed__21
{
		[Token(Token = "0x40007A7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40007A8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40007A9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AuthorTextBuilder _003C_003E4__this;

		[Token(Token = "0x40007AA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x40007AB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600074B")]
		[Address(RVA = "0x3A5C40", Offset = "0x3A4640", VA = "0x1803A5C40", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600074C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001A7")]
	[CompilerGenerated]
	private struct _003CLoadDataAsync_003Ed__26
{
		[Token(Token = "0x40007AC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40007AD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40007AE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AuthorTextBuilder _003C_003E4__this;

		[Token(Token = "0x40007AF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x40007B0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<(CharacterData, AuthorData)>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600074D")]
		[Address(RVA = "0x3A6090", Offset = "0x3A4A90", VA = "0x1803A6090", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600074E")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000785")]
	private const string ColorTemplateLiteral = "%COLOR%";

	[Token(Token = "0x4000786")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private readonly StringBuilder _builder;

	[Token(Token = "0x4000787")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private IAddressableAssetLoader _addressableAssetLoader;

	[Token(Token = "0x4000788")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private ILocalizationProvider _localizationProvider;

	[Token(Token = "0x4000789")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private AssetReference _characterDataAsset;

	[Token(Token = "0x400078A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private AssetReference _authorDataAsset;

	[Token(Token = "0x400078B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private float _firstBodyFontSize;

	[Token(Token = "0x400078C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
	private float _firstInitialFontSize;

	[Token(Token = "0x400078D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private float _secondBodyFontSize;

	[Token(Token = "0x400078E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
	private float _secondInitialFontSize;

	[Token(Token = "0x400078F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private IReadOnlyDictionary<string, IReadOnlyDictionary<LocaleKind, FullName>> _nameData;

	[Token(Token = "0x4000790")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private IReadOnlyDictionary<string, IReadOnlyDictionary<LocaleKind, string>> _authorData;

	[Token(Token = "0x1700012D")]
	private static string ColorClosingTag
	{
		[Token(Token = "0x600071D")]
		[Address(RVA = "0x3A33F0", Offset = "0x3A1DF0", VA = "0x1803A33F0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700012E")]
	private static string SizeClosingTag
	{
		[Token(Token = "0x600071E")]
		[Address(RVA = "0x3A3430", Offset = "0x3A1E30", VA = "0x1803A3430")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700012F")]
	private IAddressableAssetLoader AddressableAssetLoader
	{
		[Token(Token = "0x600071F")]
		[Address(RVA = "0x3A3470", Offset = "0x3A1E70", VA = "0x1803A3470")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000130")]
	private ILocalizationProvider LocalizationProvider
	{
		[Token(Token = "0x6000720")]
		[Address(RVA = "0x3A3530", Offset = "0x3A1F30", VA = "0x1803A3530")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000721")]
	[Address(RVA = "0x3A35F0", Offset = "0x3A1FF0", VA = "0x1803A35F0")]
	public AuthorTextBuilder(Settings settings)
	{
	}

	[Token(Token = "0x6000722")]
	[Address(RVA = "0x3A3780", Offset = "0x3A2180", VA = "0x1803A3780")]
	[AsyncStateMachine(typeof(_003CInitializeAsync_003Ed__21))]
	public UniTask InitializeAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000723")]
	[Address(RVA = "0x3A3920", Offset = "0x3A2320", VA = "0x1803A3920")]
	public bool TryBuildAuthorText(string authorId, Color? color, out string text)
	{
		text = default;
		text = default;
		return default(bool);
	}

	[Token(Token = "0x6000724")]
	[Address(RVA = "0x3A4110", Offset = "0x3A2B10", VA = "0x1803A4110")]
	public string BuildTaggedSimpleName([NotNull] string nameText, string htmlStringRgb)
	{
		return null;
	}

	[Token(Token = "0x6000725")]
	[Address(RVA = "0x3A4190", Offset = "0x3A2B90", VA = "0x1803A4190")]
	public string BuildTaggedFullName([NotNull] string firstText, [NotNull] string lastText, string htmlStringRgb)
	{
		return null;
	}

	[Token(Token = "0x6000726")]
	[Address(RVA = "0x3A4240", Offset = "0x3A2C40", VA = "0x1803A4240")]
	private void AppendTaggedName(StringBuilder builder, [NotNull] string nameText, bool isFirst, string htmlStringRgb)
	{
	}

	[Token(Token = "0x6000727")]
	[Address(RVA = "0x3A4550", Offset = "0x3A2F50", VA = "0x1803A4550")]
	[AsyncStateMachine(typeof(_003CLoadDataAsync_003Ed__26))]
	private UniTask LoadDataAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000728")]
	[Address(RVA = "0x3A4700", Offset = "0x3A3100", VA = "0x1803A4700")]
	private static string BuildColorOpeningTag(string htmlStringRGB)
	{
		return null;
	}

	[Token(Token = "0x6000729")]
	[Address(RVA = "0x3A4760", Offset = "0x3A3160", VA = "0x1803A4760")]
	private static string BuildSizeOpeningTag(float size)
	{
		return null;
	}
}
