using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel;

[Token(Token = "0x2000263")]
public class ProjectResourceProvider : ResourceProvider
{
	[Token(Token = "0x2000264")]
	public class TypeRedirector
	{
		[StructLayout(3)]
		[Token(Token = "0x2000265")]
		[CompilerGenerated]
		private struct _003CToSourceAsync_003Ed__10<TSource>
{
			[Token(Token = "0x4000948")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public int _003C_003E1__state;

			[Token(Token = "0x4000949")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public AsyncUniTaskMethodBuilder<TSource> _003C_003Et__builder;

			[Token(Token = "0x400094A")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public TypeRedirector _003C_003E4__this;

			[Token(Token = "0x400094B")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public object obj;

			[Token(Token = "0x400094C")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public string path;

			[Token(Token = "0x400094D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private UniTask<object>.Awaiter _003C_003Eu__1;

			[Token(Token = "0x6000AB6")]
			private void MoveNext()
			{
			}

			[Token(Token = "0x6000AB7")]
			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}
		}

		[Token(Token = "0x17000187")]
		public virtual Type SourceType
		{
			[Token(Token = "0x6000AB0")]
			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x17000188")]
		public virtual Type RedirectType
		{
			[Token(Token = "0x6000AB1")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "5")]
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x17000189")]
		public virtual IConverter RedirectToSourceConverter
		{
			[Token(Token = "0x6000AB2")]
			[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "6")]
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x6000AB3")]
		[Address(RVA = "0x38F3B0", Offset = "0x38DDB0", VA = "0x18038F3B0")]
		public TypeRedirector(Type sourceType, Type redirectType, IConverter redirectToSourceConverter)
		{
		}

		[Token(Token = "0x6000AB4")]
		[AsyncStateMachine(typeof(_003CToSourceAsync_003Ed__10<>))]
		public virtual UniTask<TSource> ToSourceAsync<TSource>(object obj, string path)
		{
			return default(UniTask<TSource>);
		}

		[Token(Token = "0x6000AB5")]
		public virtual TSource ToSource<TSource>(object obj, string path)
		{
			return default(TSource);
		}
	}

	[Token(Token = "0x4000943")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private readonly IReadOnlyDictionary<string, Type> projectResources;

	[Token(Token = "0x4000944")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private readonly Dictionary<Type, TypeRedirector> redirectors;

	[Token(Token = "0x17000186")]
	public string RootPath
	{
		[Token(Token = "0x6000AA6")]
		[Address(RVA = "0x453480", Offset = "0x451E80", VA = "0x180453480")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000AA7")]
	[Address(RVA = "0x5322D0", Offset = "0x530CD0", VA = "0x1805322D0")]
	public ProjectResourceProvider([Optional] string rootPath)
	{
	}

	[Token(Token = "0x6000AA8")]
	public override bool SupportsType<T>()
	{
		return default(bool);
	}

	[Token(Token = "0x6000AA9")]
	public virtual void AddRedirector<TSource, TRedirect>(IConverter<TRedirect, TSource> redirectToSourceConverter)
	{
	}

	[Token(Token = "0x6000AAA")]
	protected override LoadResourceRunner<T> CreateLoadResourceRunner<T>(string path)
	{
		return null;
	}

	[Token(Token = "0x6000AAB")]
	protected override LocateResourcesRunner<T> CreateLocateResourcesRunner<T>(string path)
	{
		return null;
	}

	[Token(Token = "0x6000AAC")]
	[Address(RVA = "0x532840", Offset = "0x531240", VA = "0x180532840", Slot = "39")]
	protected override LocateFoldersRunner CreateLocateFoldersRunner(string path)
	{
		return null;
	}

	[Token(Token = "0x6000AAD")]
	[Address(RVA = "0x532920", Offset = "0x531320", VA = "0x180532920", Slot = "40")]
	protected override void DisposeResource(Resource resource)
	{
	}

	[Token(Token = "0x6000AAE")]
	[Address(RVA = "0x532AC0", Offset = "0x5314C0", VA = "0x180532AC0", Slot = "51")]
	protected override bool AreTypesCompatible(Type sourceType, Type targetType)
	{
		return default(bool);
	}
}
