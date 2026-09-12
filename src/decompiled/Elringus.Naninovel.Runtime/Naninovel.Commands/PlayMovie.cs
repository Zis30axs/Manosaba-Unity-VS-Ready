using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;

namespace Naninovel.Commands;

[Token(Token = "0x20006DA")]
[CommandAlias("movie")]
public class PlayMovie : Command, Command.IPreloadable
{
	[StructLayout(3)]
	[Token(Token = "0x20006DB")]
	[CompilerGenerated]
	private struct _003CPlay_003Ed__9
{
		[Token(Token = "0x4001A23")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001A24")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001A25")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PlayMovie _003C_003E4__this;

		[Token(Token = "0x4001A26")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001A27")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private IMovieUI _003CmovieUI_003E5__2;

		[Token(Token = "0x4001A28")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private InteractionBlocker _003Cblocker_003E5__3;

		[Token(Token = "0x4001A29")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private float _003CfadeDuration_003E5__4;

		[Token(Token = "0x4001A2A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4001A2B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private UniTask<UnityEngine.Texture>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x4001A2C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
		private YieldAwaitable.Awaiter _003C_003Eu__3;

		[Token(Token = "0x60020E6")]
		[Address(RVA = "0x4F7830", Offset = "0x4F6230", VA = "0x1804F7830", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60020E7")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20006DC")]
	[CompilerGenerated]
	private struct _003CPreloadResources_003Ed__6
{
		[Token(Token = "0x4001A2D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001A2E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001A2F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PlayMovie _003C_003E4__this;

		[Token(Token = "0x4001A30")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60020E8")]
		[Address(RVA = "0x4F86D0", Offset = "0x4F70D0", VA = "0x1804F86D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60020E9")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001A1F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[RequiredParameter]
	[ResourceContext("Movies", -1, null)]
	public StringParameter MovieName;

	[Token(Token = "0x4001A20")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterAlias("time")]
	public DecimalParameter Duration;

	[Token(Token = "0x4001A21")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[ParameterAlias("block")]
	public BooleanParameter BlockInteraction;

	[Token(Token = "0x4001A22")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	public BooleanParameter Wait;

	[Token(Token = "0x170004CE")]
	protected virtual IMoviePlayer Player
	{
		[Token(Token = "0x60020E0")]
		[Address(RVA = "0x4F7360", Offset = "0x4F5D60", VA = "0x1804F7360", Slot = "18")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60020E1")]
	[Address(RVA = "0x4F73C0", Offset = "0x4F5DC0", VA = "0x1804F73C0", Slot = "16")]
	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__6))]
	public UniTask PreloadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60020E2")]
	[Address(RVA = "0x4F74E0", Offset = "0x4F5EE0", VA = "0x1804F74E0", Slot = "17")]
	public void ReleaseResources()
	{
	}

	[Token(Token = "0x60020E3")]
	[Address(RVA = "0x4F75D0", Offset = "0x4F5FD0", VA = "0x1804F75D0", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60020E4")]
	[Address(RVA = "0x4F7680", Offset = "0x4F6080", VA = "0x1804F7680", Slot = "19")]
	[AsyncStateMachine(typeof(_003CPlay_003Ed__9))]
	protected virtual UniTask Play(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60020E5")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public PlayMovie()
	{
	}
}
