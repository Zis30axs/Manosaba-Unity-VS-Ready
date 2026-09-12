using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Core;

[Token(Token = "0x2000006")]
public class NaninovelManager : INaninovelManager, IService, INaninovelStateSaver
{
	[StructLayout(3)]
	[Token(Token = "0x2000007")]
	[CompilerGenerated]
	private struct _003CInitializeAsync_003Ed__8
{
		[Token(Token = "0x4000008")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000009")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400000A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CancellationToken ct;

		[Token(Token = "0x400000B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600000E")]
		[Address(RVA = "0xD02E10", Offset = "0xD01810", VA = "0x180D02E10", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600000F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000008")]
	[CompilerGenerated]
	private struct _003CInitializeNaninovelAsync_003Ed__10
{
		[Token(Token = "0x400000C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400000D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400000E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CancellationToken ct;

		[Token(Token = "0x400000F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private Naninovel.UniTask _003Ctask_003E5__2;

		[Token(Token = "0x4000010")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000010")]
		[Address(RVA = "0xD031E0", Offset = "0xD01BE0", VA = "0x180D031E0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000011")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000009")]
	[CompilerGenerated]
	private struct _003CSaveSettingsAsync_003Ed__9
{
		[Token(Token = "0x4000011")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000012")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000013")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public NaninovelManager _003C_003E4__this;

		[Token(Token = "0x4000014")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000015")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Naninovel.UniTask _003Ctask_003E5__2;

		[Token(Token = "0x4000016")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000012")]
		[Address(RVA = "0xD035A0", Offset = "0xD01FA0", VA = "0x180D035A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000013")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000006")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private IStateManager _naniStateManager;

	[Token(Token = "0x4000007")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private IScriptManager _naniScriptManager;

	[Token(Token = "0x17000002")]
	public virtual string StartGameScenarioName
	{
		[Token(Token = "0x6000006")]
		[Address(RVA = "0xD026C0", Offset = "0xD010C0", VA = "0x180D026C0", Slot = "7")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000003")]
	private IStateManager NaniStateManager
	{
		[Token(Token = "0x6000007")]
		[Address(RVA = "0xD027B0", Offset = "0xD011B0", VA = "0x180D027B0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000004")]
	private IScriptManager NaniScriptManager
	{
		[Token(Token = "0x6000008")]
		[Address(RVA = "0xD02870", Offset = "0xD01270", VA = "0x180D02870")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000009")]
	[Address(RVA = "0xD02930", Offset = "0xD01330", VA = "0x180D02930", Slot = "8")]
	[AsyncStateMachine(typeof(_003CInitializeAsync_003Ed__8))]
	public virtual Cysharp.Threading.Tasks.UniTask InitializeAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x600000A")]
	[Address(RVA = "0xD02A70", Offset = "0xD01470", VA = "0x180D02A70", Slot = "6")]
	[AsyncStateMachine(typeof(_003CSaveSettingsAsync_003Ed__9))]
	public Cysharp.Threading.Tasks.UniTask SaveSettingsAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x600000B")]
	[Address(RVA = "0xD02C20", Offset = "0xD01620", VA = "0x180D02C20")]
	[AsyncStateMachine(typeof(_003CInitializeNaninovelAsync_003Ed__10))]
	private static Cysharp.Threading.Tasks.UniTask InitializeNaninovelAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x600000C")]
	[Address(RVA = "0xD02D60", Offset = "0xD01760", VA = "0x180D02D60", Slot = "9")]
	protected virtual void ReleaseNaninovelServices()
	{
	}

	[Token(Token = "0x600000D")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public NaninovelManager()
	{
	}
}
