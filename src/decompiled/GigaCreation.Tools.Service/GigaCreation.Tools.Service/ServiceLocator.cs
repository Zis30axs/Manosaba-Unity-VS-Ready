using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.Service;

[Token(Token = "0x2000005")]
public static class ServiceLocator
{
	[StructLayout(3)]
	[Token(Token = "0x200000C")]
	[CompilerGenerated]
	private struct _003CUnregisterAsync_003Ed__3<TService> where TService : class, IService
{
		[Token(Token = "0x400000D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400000E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400000F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public TService service;

		[Token(Token = "0x4000010")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private KeyValuePair<Type, IService> _003CregisteredService_003E5__2;

		[Token(Token = "0x4000011")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ValueTaskAwaiter _003C_003Eu__1;

		[Token(Token = "0x4000012")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000018")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000019")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000006")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly Dictionary<Type, IService> s_services;

	[Token(Token = "0x6000003")]
	public static void Register<TService>(TService service) where TService : class, IService
	{
	}

	[Token(Token = "0x6000004")]
	public static void Unregister<TService>(TService service) where TService : class, IService
	{
	}

	[Token(Token = "0x6000005")]
	[AsyncStateMachine(typeof(_003CUnregisterAsync_003Ed__3<>))]
	public static UniTask UnregisterAsync<TService>(TService service) where TService : class, IService
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000006")]
	public static bool IsRegistered<TService>(bool includeDerivedType = false) where TService : class, IService
	{
		return default(bool);
	}

	[Token(Token = "0x6000007")]
	public static bool IsRegistered<TService>(TService service, bool includeDerivedType = false) where TService : class, IService
	{
		return default(bool);
	}

	[Token(Token = "0x6000008")]
	public static TService Get<TService>(bool includeDerivedType = false) where TService : class, IService
	{
		return null;
	}

	[Token(Token = "0x6000009")]
	public static bool TryGet<TService>(out TService service, bool includeDerivedType = false) where TService : class, IService
	{
		service = default;
		service = default;
		return default(bool);
	}

	[Token(Token = "0x600000A")]
	[Address(RVA = "0xD53160", Offset = "0xD51B60", VA = "0x180D53160")]
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void ClearServices()
	{
	}
}
