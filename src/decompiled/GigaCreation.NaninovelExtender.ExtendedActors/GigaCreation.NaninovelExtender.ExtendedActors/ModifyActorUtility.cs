using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.AddressablesUtils;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x2000039")]
public class ModifyActorUtility<TActor, TManager> where TActor : class, IActor where TManager : class, IMultipliableActorManager<TActor>
{
	[StructLayout(3)]
	[Token(Token = "0x200003A")]
	[CompilerGenerated]
	private struct _003CGetDissolveTexture_003Ed__6
{
		[Token(Token = "0x40000BD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000BE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<Texture2D> _003C_003Et__builder;

		[Token(Token = "0x40000BF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ModifyActorUtility<TActor, TManager> _003C_003E4__this;

		[Token(Token = "0x40000C0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string name;

		[Token(Token = "0x40000C1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public CancellationToken ct;

		[Token(Token = "0x40000C2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Cysharp.Threading.Tasks.UniTask<Texture2D>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000C4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000C5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200003B")]
	[CompilerGenerated]
	private struct _003CGetOrAddActor_003Ed__9
{
		[Token(Token = "0x40000C3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000C4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<TActor> _003C_003Et__builder;

		[Token(Token = "0x40000C5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ModifyActorUtility<TActor, TManager> _003C_003E4__this;

		[Token(Token = "0x40000C6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string actorId;

		[Token(Token = "0x40000C7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string subId;

		[Token(Token = "0x40000C8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Naninovel.UniTask<TActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000C6")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000C7")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200003C")]
	[CompilerGenerated]
	private struct _003CPreloadResources_003Ed__7
{
		[Token(Token = "0x40000C9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000CA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000CB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ModifyActorUtility<TActor, TManager> _003C_003E4__this;

		[Token(Token = "0x40000CC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string actorId;

		[Token(Token = "0x40000CD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string subId;

		[Token(Token = "0x40000CE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string appearance;

		[Token(Token = "0x40000CF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public object holder;

		[Token(Token = "0x40000D0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Naninovel.UniTask<TActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40000D1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private Naninovel.UniTask<Resource>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60000C8")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000C9")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40000BA")]
	private const string AddressToDissolveTextures = "General/Transitions/";

	[Token(Token = "0x40000BB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly TManager _multipliableActorManager;

	[Token(Token = "0x40000BC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private IAddressableAssetLoader _addressableAssetLoader;

	[Token(Token = "0x17000013")]
	private IAddressableAssetLoader AddressableAssetLoader
	{
		[Token(Token = "0x60000BC")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60000BD")]
	public ModifyActorUtility(IActorManager manager)
	{
	}

	[Token(Token = "0x60000BE")]
	[AsyncStateMachine(typeof(ModifyActorUtility<, >._003CGetDissolveTexture_003Ed__6))]
	public Naninovel.UniTask<Texture2D> GetDissolveTexture(string name, [Optional] CancellationToken ct)
	{
		return default(Naninovel.UniTask<Texture2D>);
	}

	[Token(Token = "0x60000BF")]
	[AsyncStateMachine(typeof(ModifyActorUtility<, >._003CPreloadResources_003Ed__7))]
	public Naninovel.UniTask PreloadResources(string actorId, string subId, string appearance, object holder)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x60000C0")]
	public void ReleaseResources(string id, string appearance, object holder)
	{
	}

	[Token(Token = "0x60000C1")]
	[AsyncStateMachine(typeof(ModifyActorUtility<, >._003CGetOrAddActor_003Ed__9))]
	public Naninovel.UniTask<TActor> GetOrAddActor(string actorId, string subId)
	{
		return default(Naninovel.UniTask<TActor>);
	}

	[Token(Token = "0x60000C2")]
	public Transition BuildAppearanceTransition(string transition, DecimalListParameter paramList, Texture2D dissolveTexture)
	{
		return default(Transition);
	}

	[Token(Token = "0x60000C3")]
	public float?[] EvaluateWorldPosition(DecimalListParameter scenePosition, bool matchScreenWidth)
	{
		return null;
	}
}
