using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x200026A")]
public static class ProviderListExtensions
{
	[StructLayout(3)]
	[Token(Token = "0x200026F")]
	[CompilerGenerated]
	private struct _003CLoadResourceAsync_003Ed__3<T> where T : Object
{
		[Token(Token = "0x400095B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400095C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<Resource<T>> _003C_003Et__builder;

		[Token(Token = "0x400095D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public IList<IResourceProvider> providers;

		[Token(Token = "0x400095E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string path;

		[Token(Token = "0x400095F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<Resource<T>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000960")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private IEnumerator<IResourceProvider> _003C_003E7__wrap1;

		[Token(Token = "0x4000961")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private IResourceProvider _003Cprovider_003E5__3;

		[Token(Token = "0x4000962")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<bool>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000AD4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000AD5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000270")]
	[CompilerGenerated]
	private struct _003CLoadResourcesAsync_003Ed__4<T> where T : Object
{
		[Token(Token = "0x4000963")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000964")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<IReadOnlyCollection<Resource<T>>> _003C_003Et__builder;

		[Token(Token = "0x4000965")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public IList<IResourceProvider> providers;

		[Token(Token = "0x4000966")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string path;

		[Token(Token = "0x4000967")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private List<Resource<T>> _003Cresources_003E5__2;

		[Token(Token = "0x4000968")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private List<Resource<T>> _003C_003E7__wrap2;

		[Token(Token = "0x4000969")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<IReadOnlyCollection<Resource<T>>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400096A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private IEnumerator<IResourceProvider> _003C_003E7__wrap3;

		[Token(Token = "0x400096B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private IResourceProvider _003Cprovider_003E5__5;

		[Token(Token = "0x400096C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<IReadOnlyCollection<string>>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x400096D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private IEnumerator<string> _003C_003E7__wrap5;

		[Token(Token = "0x400096E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<Resource<T>>.Awaiter _003C_003Eu__3;

		[Token(Token = "0x6000AD6")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000AD7")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000271")]
	[CompilerGenerated]
	private struct _003CLocateFolders_003Ed__6
{
		[Token(Token = "0x400096F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000970")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<IReadOnlyCollection<Folder>> _003C_003Et__builder;

		[Token(Token = "0x4000971")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public IList<IResourceProvider> providers;

		[Token(Token = "0x4000972")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string path;

		[Token(Token = "0x4000973")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private List<Folder> _003Cresult_003E5__2;

		[Token(Token = "0x4000974")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private IEnumerator<IResourceProvider> _003C_003E7__wrap2;

		[Token(Token = "0x4000975")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask<IReadOnlyCollection<Folder>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000AD8")]
		[Address(RVA = "0x5344E0", Offset = "0x532EE0", VA = "0x1805344E0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000AD9")]
		[Address(RVA = "0x534F80", Offset = "0x533980", VA = "0x180534F80", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000272")]
	[CompilerGenerated]
	private struct _003CLocateResourcesAsync_003Ed__5<T> where T : Object
{
		[Token(Token = "0x4000976")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000977")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<IReadOnlyCollection<string>> _003C_003Et__builder;

		[Token(Token = "0x4000978")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public IList<IResourceProvider> providers;

		[Token(Token = "0x4000979")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string path;

		[Token(Token = "0x400097A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private List<string> _003Cresult_003E5__2;

		[Token(Token = "0x400097B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private IEnumerator<IResourceProvider> _003C_003E7__wrap2;

		[Token(Token = "0x400097C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<IReadOnlyCollection<string>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000ADA")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000ADB")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000273")]
	[CompilerGenerated]
	private struct _003CResourceExistsAsync_003Ed__7<T> where T : Object
{
		[Token(Token = "0x400097D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400097E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

		[Token(Token = "0x400097F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public IList<IResourceProvider> providers;

		[Token(Token = "0x4000980")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string path;

		[Token(Token = "0x4000981")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private IEnumerator<IResourceProvider> _003C_003E7__wrap1;

		[Token(Token = "0x4000982")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<bool>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000ADC")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000ADD")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x6000ABF")]
	[Address(RVA = "0x533590", Offset = "0x531F90", VA = "0x180533590")]
	public static bool AnyIsLoading(this IList<IResourceProvider> providers)
	{
		return default(bool);
	}

	[Token(Token = "0x6000AC0")]
	[Address(RVA = "0x533710", Offset = "0x532110", VA = "0x180533710")]
	public static IReadOnlyCollection<Resource> GetLoadedResources(this IList<IResourceProvider> providers)
	{
		return null;
	}

	[Token(Token = "0x6000AC1")]
	public static Resource<T> GetLoadedResourceOrNull<T>(this IList<IResourceProvider> providers, string path) where T : Object
	{
		return null;
	}

	[Token(Token = "0x6000AC2")]
	[AsyncStateMachine(typeof(_003CLoadResourceAsync_003Ed__3<>))]
	public static UniTask<Resource<T>> LoadResourceAsync<T>(this IList<IResourceProvider> providers, string path) where T : Object
	{
		return default(UniTask<Resource<T>>);
	}

	[Token(Token = "0x6000AC3")]
	[AsyncStateMachine(typeof(_003CLoadResourcesAsync_003Ed__4<>))]
	public static UniTask<IReadOnlyCollection<Resource<T>>> LoadResourcesAsync<T>(this IList<IResourceProvider> providers, string path) where T : Object
	{
		return default(UniTask<IReadOnlyCollection<Resource<T>>>);
	}

	[Token(Token = "0x6000AC4")]
	[AsyncStateMachine(typeof(_003CLocateResourcesAsync_003Ed__5<>))]
	public static UniTask<IReadOnlyCollection<string>> LocateResourcesAsync<T>(this IList<IResourceProvider> providers, string path) where T : Object
	{
		return default(UniTask<IReadOnlyCollection<string>>);
	}

	[Token(Token = "0x6000AC5")]
	[Address(RVA = "0x533900", Offset = "0x532300", VA = "0x180533900")]
	[AsyncStateMachine(typeof(_003CLocateFolders_003Ed__6))]
	public static UniTask<IReadOnlyCollection<Folder>> LocateFolders(this IList<IResourceProvider> providers, string path)
	{
		return default(UniTask<IReadOnlyCollection<Folder>>);
	}

	[Token(Token = "0x6000AC6")]
	[AsyncStateMachine(typeof(_003CResourceExistsAsync_003Ed__7<>))]
	public static UniTask<bool> ResourceExistsAsync<T>(this IList<IResourceProvider> providers, string path) where T : Object
	{
		return default(UniTask<bool>);
	}

	[Token(Token = "0x6000AC7")]
	[Address(RVA = "0x533AF0", Offset = "0x5324F0", VA = "0x180533AF0")]
	public static void UnloadResource(this IList<IResourceProvider> providers, string path)
	{
	}

	[Token(Token = "0x6000AC8")]
	[Address(RVA = "0x533D60", Offset = "0x532760", VA = "0x180533D60")]
	public static void UnloadResources(this IList<IResourceProvider> providers)
	{
	}

	[Token(Token = "0x6000AC9")]
	[Address(RVA = "0x533FC0", Offset = "0x5329C0", VA = "0x180533FC0")]
	public static bool ResourceLoaded(this IList<IResourceProvider> providers, string path)
	{
		return default(bool);
	}

	[Token(Token = "0x6000ACA")]
	[Address(RVA = "0x534160", Offset = "0x532B60", VA = "0x180534160")]
	public static bool ResourceLoading(this IList<IResourceProvider> providers, string path)
	{
		return default(bool);
	}
}
