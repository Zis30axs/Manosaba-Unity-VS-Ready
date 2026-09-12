using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Naninovel;

[Token(Token = "0x2000249")]
public class AddressableResourceLoader<TResource> : LoadResourceRunner<TResource> where TResource : UnityEngine.Object
{
	[StructLayout(3)]
	[Token(Token = "0x200024A")]
	[CompilerGenerated]
	private struct _003CRun_003Ed__4
{
		[Token(Token = "0x40008F9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40008FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40008FB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AddressableResourceLoader<TResource> _003C_003E4__this;

		[Token(Token = "0x40008FC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private float _003CstartTime_003E5__2;

		[Token(Token = "0x40008FD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private AsyncOperationHandle<TResource> _003Ctask_003E5__3;

		[Token(Token = "0x40008FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000A28")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000A29")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40008F6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly List<IResourceLocation> locations;

	[Token(Token = "0x40008F7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly Action<string> logAction;

	[Token(Token = "0x40008F8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly string resourceAddress;

	[Token(Token = "0x6000A25")]
	public AddressableResourceLoader(AddressableResourceProvider provider, string resourcePath, List<IResourceLocation> locations, Action<string> logAction)
	{
	}

	[Token(Token = "0x6000A26")]
	[AsyncStateMachine(typeof(AddressableResourceLoader<>._003CRun_003Ed__4))]
	public override UniTask Run()
	{
		return default(UniTask);
	}
}
