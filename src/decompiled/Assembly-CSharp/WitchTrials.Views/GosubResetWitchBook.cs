using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using Naninovel.Metadata;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000069")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[Branch(BranchTraits.Endpoint | BranchTraits.Return, null, null)]
[CommandAlias("gosubResetBook")]
public class GosubResetWitchBook : Gosub
{
	[StructLayout(3)]
	[Token(Token = "0x200006D")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__4
{
		[Token(Token = "0x4000169")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400016A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400016B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GosubResetWitchBook _003C_003E4__this;

		[Token(Token = "0x400016C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x400016D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000160")]
		[Address(RVA = "0x329A90", Offset = "0x328490", VA = "0x180329A90", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000161")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200006E")]
	[CompilerGenerated]
	private struct _003CExportScriptFile_003Ed__7
{
		[Token(Token = "0x400016E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400016F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000170")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GosubResetWitchBook _003C_003E4__this;

		[Token(Token = "0x4000171")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private StreamWriter _003Cwriter_003E5__2;

		[Token(Token = "0x4000172")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private object _003C_003E7__wrap2;

		[Token(Token = "0x4000173")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private int _003C_003E7__wrap3;

		[Token(Token = "0x4000174")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private TaskAwaiter _003C_003Eu__1;

		[Token(Token = "0x4000175")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Dictionary<WitchBookCategory, IdVersionPair[]>.Enumerator _003C_003E7__wrap4;

		[Token(Token = "0x4000176")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
		private WitchBookCategory _003Ccategory_003E5__6;

		[Token(Token = "0x4000177")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
		private IdVersionPair[] _003C_003E7__wrap6;

		[Token(Token = "0x4000178")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
		private int _003C_003E7__wrap7;

		[Token(Token = "0x4000179")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
		private ValueTaskAwaiter _003C_003Eu__2;

		[Token(Token = "0x6000162")]
		[Address(RVA = "0x32A300", Offset = "0x328D00", VA = "0x18032A300", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000163")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400015F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static Dictionary<WitchBookCategory, IdVersionPair[]> s_prevEvidences;

	[Token(Token = "0x4000160")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public IntegerParameter Version;

	[Token(Token = "0x4000161")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public BooleanParameter IsDebugDirectJump;

	[Token(Token = "0x4000162")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private ScenarioInfo _info;

	[Token(Token = "0x6000150")]
	[Address(RVA = "0x328530", Offset = "0x326F30", VA = "0x180328530", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__4))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000151")]
	[Address(RVA = "0x3286B0", Offset = "0x3270B0", VA = "0x1803286B0", Slot = "17")]
	protected override bool TryGetScriptPathAndLabel(out string scriptPath, out string label)
	{
		scriptPath = default;
		label = default;
		scriptPath = default;
		label = default;
		return default(bool);
	}

	[Token(Token = "0x6000152")]
	[Address(RVA = "0x328D20", Offset = "0x327720", VA = "0x180328D20")]
	private static bool CheckEvidenceState()
	{
		return default(bool);
	}

	[Token(Token = "0x6000153")]
	[Address(RVA = "0x329170", Offset = "0x327B70", VA = "0x180329170")]
	[AsyncStateMachine(typeof(_003CExportScriptFile_003Ed__7))]
	private UniTask ExportScriptFile()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000154")]
	[Address(RVA = "0x3292C0", Offset = "0x327CC0", VA = "0x1803292C0")]
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void ResetStaticFields()
	{
	}

	[Token(Token = "0x6000155")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public GosubResetWitchBook()
	{
	}
}
