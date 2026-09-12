using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000258")]
public class LocalResourceLoader<TResource> : LoadResourceRunner<TResource> where TResource : UnityEngine.Object
{
	[StructLayout(3)]
	[Token(Token = "0x200025A")]
	[CompilerGenerated]
	private struct _003CRun_003Ed__7
{
		[Token(Token = "0x4000926")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000927")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000928")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public LocalResourceLoader<TResource> _003C_003E4__this;

		[Token(Token = "0x4000929")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private float _003CstartTime_003E5__2;

		[Token(Token = "0x400092A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private string _003CfilePath_003E5__3;

		[Token(Token = "0x400092B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private IRawConverter<TResource> _003CselectedConverter_003E5__4;

		[Token(Token = "0x400092C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private IEnumerator<IRawConverter<TResource>> _003C_003E7__wrap4;

		[Token(Token = "0x400092D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<byte[]>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400092E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TResource>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000A86")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000A87")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000921")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly Action<string> logAction;

	[Token(Token = "0x4000922")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly IEnumerable<IRawConverter<TResource>> converters;

	[Token(Token = "0x4000923")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private byte[] rawData;

	[Token(Token = "0x17000182")]
	public virtual string RootPath
	{
		[Token(Token = "0x6000A80")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000A81")]
	public LocalResourceLoader(IResourceProvider provider, string rootPath, string resourcePath, IEnumerable<IRawConverter<TResource>> converters, Action<string> logAction)
	{
	}

	[Token(Token = "0x6000A82")]
	[AsyncStateMachine(typeof(LocalResourceLoader<>._003CRun_003Ed__7))]
	public override UniTask Run()
	{
		return default(UniTask);
	}
}
