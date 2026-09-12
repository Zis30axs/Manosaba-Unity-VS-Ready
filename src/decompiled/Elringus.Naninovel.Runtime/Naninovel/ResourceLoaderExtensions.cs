using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000281")]
public static class ResourceLoaderExtensions
{
	[StructLayout(3)]
	[Token(Token = "0x2000282")]
	[CompilerGenerated]
	private struct _003CLoadOrErr_003Ed__0
{
		[Token(Token = "0x40009C9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40009CA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<Resource> _003C_003Et__builder;

		[Token(Token = "0x40009CB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public IResourceLoader loader;

		[Token(Token = "0x40009CC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string path;

		[Token(Token = "0x40009CD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public object holder;

		[Token(Token = "0x40009CE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<Resource>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000B34")]
		[Address(RVA = "0x5360F0", Offset = "0x534AF0", VA = "0x1805360F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000B35")]
		[Address(RVA = "0x536410", Offset = "0x534E10", VA = "0x180536410", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000283")]
	[CompilerGenerated]
	private struct _003CLoadOrErr_003Ed__6<TResource> where TResource : Object
{
		[Token(Token = "0x40009CF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40009D0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<Resource<TResource>> _003C_003Et__builder;

		[Token(Token = "0x40009D1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public IResourceLoader<TResource> loader;

		[Token(Token = "0x40009D2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string path;

		[Token(Token = "0x40009D3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public object holder;

		[Token(Token = "0x40009D4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<Resource<TResource>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000B36")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000B37")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x6000B2B")]
	[Address(RVA = "0x535AF0", Offset = "0x5344F0", VA = "0x180535AF0")]
	[AsyncStateMachine(typeof(_003CLoadOrErr_003Ed__0))]
	public static UniTask<Resource> LoadOrErr(this IResourceLoader loader, string path, [Optional][CanBeNull] object holder)
	{
		return default(UniTask<Resource>);
	}

	[Token(Token = "0x6000B2C")]
	[Address(RVA = "0x535D50", Offset = "0x534750", VA = "0x180535D50")]
	public static void Hold(this IResourceLoader loader, Resource resource, object holder)
	{
	}

	[Token(Token = "0x6000B2D")]
	[Address(RVA = "0x535E10", Offset = "0x534810", VA = "0x180535E10")]
	public static void Release(this IResourceLoader loader, Resource resource, object holder, bool unload = true)
	{
	}

	[Token(Token = "0x6000B2E")]
	[Address(RVA = "0x535ED0", Offset = "0x5348D0", VA = "0x180535ED0")]
	public static bool TryGetLoaded(this IResourceLoader loader, string path, out Object resource)
	{
		resource = default;
		resource = default;
		resource = default;
		resource = default;
		return default(bool);
	}

	[Token(Token = "0x6000B2F")]
	public static bool TryGetLoaded<TResource>(this IResourceLoader<TResource> loader, string path, out TResource resource) where TResource : Object
	{
		resource = default;
		resource = default;
		return default(bool);
	}

	[Token(Token = "0x6000B30")]
	[Address(RVA = "0x535FD0", Offset = "0x5349D0", VA = "0x180535FD0")]
	public static Object GetLoadedOrErr(this IResourceLoader loader, string path)
	{
		return null;
	}

	[Token(Token = "0x6000B31")]
	[AsyncStateMachine(typeof(_003CLoadOrErr_003Ed__6<>))]
	public static UniTask<Resource<TResource>> LoadOrErr<TResource>(this IResourceLoader<TResource> loader, string path, [Optional][CanBeNull] object holder) where TResource : Object
	{
		return default(UniTask<Resource<TResource>>);
	}

	[Token(Token = "0x6000B32")]
	public static TResource GetLoadedOrErr<TResource>(this IResourceLoader<TResource> loader, string path) where TResource : Object
	{
		return null;
	}

	[Token(Token = "0x6000B33")]
	[CanBeNull]
	public static TResource Juggle<TResource>(this IResourceLoader<TResource> loader, [CanBeNull] TResource from, [CanBeNull] TResource to, object holder) where TResource : Object
	{
		return null;
	}
}
