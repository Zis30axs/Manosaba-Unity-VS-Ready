using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Metadata;

namespace Naninovel.Commands;

[Token(Token = "0x20006A1")]
[CommandAlias("choice")]
[Branch(BranchTraits.Endpoint | BranchTraits.Nest | BranchTraits.Interactive | BranchTraits.Return, null, null)]
public class AddChoice : Command, Command.ILocalizable, Command.IPreloadable, Command.INestedHost
{
	[StructLayout(3)]
	[Token(Token = "0x20006A2")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__16
{
		[Token(Token = "0x4001967")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001968")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001969")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AddChoice _003C_003E4__this;

		[Token(Token = "0x400196A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x400196B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private IDisposable _003C__003E5__2;

		[Token(Token = "0x400196C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<IDisposable>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400196D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask<IChoiceHandlerActor>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6002042")]
		[Address(RVA = "0x4E5FD0", Offset = "0x4E49D0", VA = "0x1804E5FD0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6002043")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20006A3")]
	[CompilerGenerated]
	private struct _003CGetOrAddHandler_003Ed__17
{
		[Token(Token = "0x400196E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400196F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<IChoiceHandlerActor> _003C_003Et__builder;

		[Token(Token = "0x4001970")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AddChoice _003C_003E4__this;

		[Token(Token = "0x4001971")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x4001972")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<IChoiceHandlerActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6002044")]
		[Address(RVA = "0x4E6820", Offset = "0x4E5220", VA = "0x1804E6820", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6002045")]
		[Address(RVA = "0x4E6BD0", Offset = "0x4E55D0", VA = "0x1804E6BD0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20006A4")]
	[CompilerGenerated]
	private struct _003CPreloadResources_003Ed__13
{
		[Token(Token = "0x4001973")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001974")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001975")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AddChoice _003C_003E4__this;

		[Token(Token = "0x4001976")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4001977")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<IChoiceHandlerActor>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x4001978")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<Resource<UnityEngine.GameObject>>.Awaiter _003C_003Eu__3;

		[Token(Token = "0x6002046")]
		[Address(RVA = "0x4E6C00", Offset = "0x4E5600", VA = "0x1804E6C00", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6002047")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400195C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	public LocalizableTextParameter ChoiceSummary;

	[Token(Token = "0x400195D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterDefaultValue("false")]
	public BooleanParameter Lock;

	[Token(Token = "0x400195E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[ParameterAlias("button")]
	public StringParameter ButtonPath;

	[Token(Token = "0x400195F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[ParameterAlias("pos")]
	[VectorContext("X,Y", -1, null)]
	public DecimalListParameter ButtonPosition;

	[Token(Token = "0x4001960")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[ParameterAlias("handler")]
	[ActorContext("ChoiceHandlers", -1, null)]
	public StringParameter HandlerId;

	[Token(Token = "0x4001961")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[ParameterAlias("goto")]
	[EndpointContext(null)]
	public NamedStringParameter GotoPath;

	[Token(Token = "0x4001962")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[ParameterAlias("gosub")]
	[EndpointContext(null)]
	public NamedStringParameter GosubPath;

	[Token(Token = "0x4001963")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	[ParameterAlias("set")]
	[AssignmentContext(-1, null)]
	public StringParameter SetExpression;

	[Token(Token = "0x4001964")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	[ParameterAlias("play")]
	[ParameterDefaultValue("true")]
	public BooleanParameter AutoPlay;

	[Token(Token = "0x4001965")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	[ParameterAlias("show")]
	[ParameterDefaultValue("true")]
	public BooleanParameter ShowHandler;

	[Token(Token = "0x4001966")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	[ParameterAlias("time")]
	[ParameterDefaultValue("0.35")]
	public DecimalParameter Duration;

	[Token(Token = "0x170004C3")]
	protected IChoiceHandlerManager Handlers
	{
		[Token(Token = "0x6002039")]
		[Address(RVA = "0x4E4800", Offset = "0x4E3200", VA = "0x1804E4800")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600203A")]
	[Address(RVA = "0x4E4860", Offset = "0x4E3260", VA = "0x1804E4860", Slot = "19")]
	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__13))]
	public virtual UniTask PreloadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x600203B")]
	[Address(RVA = "0x4E4990", Offset = "0x4E3390", VA = "0x1804E4990", Slot = "20")]
	public virtual void ReleaseResources()
	{
	}

	[Token(Token = "0x600203C")]
	[Address(RVA = "0x4E4BA0", Offset = "0x4E35A0", VA = "0x1804E4BA0", Slot = "21")]
	public virtual int GetNextPlaybackIndex(ScriptPlaylist playlist, int playedIndex)
	{
		return default(int);
	}

	[Token(Token = "0x600203D")]
	[Address(RVA = "0x4E4FB0", Offset = "0x4E39B0", VA = "0x1804E4FB0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__16))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600203E")]
	[Address(RVA = "0x4E5150", Offset = "0x4E3B50", VA = "0x1804E5150", Slot = "22")]
	[AsyncStateMachine(typeof(_003CGetOrAddHandler_003Ed__17))]
	protected virtual UniTask<IChoiceHandlerActor> GetOrAddHandler(AsyncToken token)
	{
		return default(UniTask<IChoiceHandlerActor>);
	}

	[Token(Token = "0x600203F")]
	[Address(RVA = "0x4E5340", Offset = "0x4E3D40", VA = "0x1804E5340", Slot = "23")]
	protected virtual UniTask ShowHandlerActor(IChoiceHandlerActor handler, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002040")]
	[Address(RVA = "0x4E5560", Offset = "0x4E3F60", VA = "0x1804E5560", Slot = "24")]
	protected virtual ChoiceState CreateChoice()
	{
		return default(ChoiceState);
	}

	[Token(Token = "0x6002041")]
	[Address(RVA = "0x4E5E90", Offset = "0x4E4890", VA = "0x1804E5E90")]
	public AddChoice()
	{
	}
}
