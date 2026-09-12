using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000260")]
public class ProjectResourceLoader<TResource> : LoadResourceRunner<TResource> where TResource : UnityEngine.Object
{
	[StructLayout(3)]
	[Token(Token = "0x2000261")]
	[CompilerGenerated]
	private struct _003CRun_003Ed__6
{
		[Token(Token = "0x400093B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400093C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400093D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ProjectResourceLoader<TResource> _003C_003E4__this;

		[Token(Token = "0x400093E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private float _003CstartTime_003E5__2;

		[Token(Token = "0x400093F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UnityAsyncExtensions.ResourceRequestAwaiter _003C_003Eu__1;

		[Token(Token = "0x4000940")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TResource>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000AA1")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000AA2")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000939")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly Action<string> logAction;

	[Token(Token = "0x400093A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly ProjectResourceProvider.TypeRedirector redirector;

	[Token(Token = "0x17000185")]
	public virtual string RootPath
	{
		[Token(Token = "0x6000A9E")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000A9F")]
	public ProjectResourceLoader(IResourceProvider provider, string rootPath, string resourcePath, ProjectResourceProvider.TypeRedirector redirector, Action<string> logAction)
	{
	}

	[Token(Token = "0x6000AA0")]
	[AsyncStateMachine(typeof(ProjectResourceLoader<>._003CRun_003Ed__6))]
	public override UniTask Run()
	{
		return default(UniTask);
	}
}
