using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000452")]
public class ScriptPlaylist
{
	[StructLayout(3)]
	[Token(Token = "0x2000459")]
	[CompilerGenerated]
	private struct _003CLoadResources_003Ed__16
{
		[Token(Token = "0x400102D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400102E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400102F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ScriptPlaylist _003C_003E4__this;

		[Token(Token = "0x4001030")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600157F")]
		[Address(RVA = "0x5D2FB0", Offset = "0x5D19B0", VA = "0x1805D2FB0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001580")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200045A")]
	[CompilerGenerated]
	private struct _003CLoadResources_003Ed__17
{
		[Token(Token = "0x4001031")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001032")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001033")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public Action<float> onProgress;

		[Token(Token = "0x4001034")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public ScriptPlaylist _003C_003E4__this;

		[Token(Token = "0x4001035")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public int startCommandIndex;

		[Token(Token = "0x4001036")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public int endCommandIndex;

		[Token(Token = "0x4001037")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001581")]
		[Address(RVA = "0x5D32B0", Offset = "0x5D1CB0", VA = "0x1805D32B0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001582")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400101C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	[SerializeField]
	private string scriptPath;

	[Token(Token = "0x400101D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	[SerializeReference]
	private List<Command> commands;

	[Token(Token = "0x170002F9")]
	public string ScriptPath
	{
		[Token(Token = "0x6001554")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170002FA")]
	public int Count
	{
		[Token(Token = "0x6001555")]
		[Address(RVA = "0x5D0FF0", Offset = "0x5CF9F0", VA = "0x1805D0FF0", Slot = "5")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x170002FB")]
	public Command this[int index]
	{
		[Token(Token = "0x6001557")]
		[Address(RVA = "0x5D1040", Offset = "0x5CFA40", VA = "0x1805D1040", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001556")]
	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
	public ScriptPlaylist(string scriptPath, List<Command> commands)
	{
	}

	[Token(Token = "0x6001558")]
	[Address(RVA = "0x5D10B0", Offset = "0x5CFAB0", VA = "0x1805D10B0", Slot = "6")]
	public IEnumerator<Command> GetEnumerator()
	{
		return null;
	}

	[Token(Token = "0x6001559")]
	[Address(RVA = "0x5D1140", Offset = "0x5CFB40", VA = "0x1805D1140", Slot = "7")]
	private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
	{
		return null;
	}

	[Token(Token = "0x600155A")]
	[Address(RVA = "0x5D1150", Offset = "0x5CFB50", VA = "0x1805D1150")]
	public Command Find(Predicate<Command> predicate)
	{
		return null;
	}

	[Token(Token = "0x600155B")]
	[Address(RVA = "0x5D11C0", Offset = "0x5CFBC0", VA = "0x1805D11C0")]
	public int FindIndex(Predicate<Command> predicate)
	{
		return default(int);
	}

	[Token(Token = "0x600155C")]
	[Address(RVA = "0x5D1240", Offset = "0x5CFC40", VA = "0x1805D1240")]
	public List<Command> GetRange(int index, int count)
	{
		return null;
	}

	[Token(Token = "0x600155D")]
	[Address(RVA = "0x5D12B0", Offset = "0x5CFCB0", VA = "0x1805D12B0")]
	public bool IsIndexValid(int index)
	{
		return default(bool);
	}

	[Token(Token = "0x600155E")]
	[Address(RVA = "0x5D1330", Offset = "0x5CFD30", VA = "0x1805D1330")]
	public int FindIndexAfter(int afterIndex, Predicate<Command> predicate)
	{
		return default(int);
	}

	[Token(Token = "0x600155F")]
	[Address(RVA = "0x5D1400", Offset = "0x5CFE00", VA = "0x1805D1400")]
	[AsyncStateMachine(typeof(_003CLoadResources_003Ed__16))]
	public UniTask LoadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001560")]
	[Address(RVA = "0x5D1520", Offset = "0x5CFF20", VA = "0x1805D1520")]
	[AsyncStateMachine(typeof(_003CLoadResources_003Ed__17))]
	public UniTask LoadResources(int startCommandIndex, int endCommandIndex, [Optional] Action<float> onProgress)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001561")]
	[Address(RVA = "0x5D16D0", Offset = "0x5D00D0", VA = "0x1805D16D0")]
	public void ReleaseResources()
	{
	}

	[Token(Token = "0x6001562")]
	[Address(RVA = "0x5D1730", Offset = "0x5D0130", VA = "0x1805D1730")]
	public void ReleaseResources(int startCommandIndex, int endCommandIndex)
	{
	}

	[Token(Token = "0x6001563")]
	[Address(RVA = "0x5D1A00", Offset = "0x5D0400", VA = "0x1805D1A00")]
	public Command GetCommandByIndex(int index)
	{
		return null;
	}

	[Token(Token = "0x6001564")]
	[Address(RVA = "0x5D1A80", Offset = "0x5D0480", VA = "0x1805D1A80")]
	public Command GetCommandByLine(int lineIndex, int inlineIndex)
	{
		return null;
	}

	[Token(Token = "0x6001565")]
	[Address(RVA = "0x5D1B80", Offset = "0x5D0580", VA = "0x1805D1B80")]
	public Command GetCommandAfterLine(int lineIndex, int inlineIndex)
	{
		return null;
	}

	[Token(Token = "0x6001566")]
	[Address(RVA = "0x5D1C70", Offset = "0x5D0670", VA = "0x1805D1C70")]
	public Command GetCommandBeforeLine(int lineIndex, int inlineIndex)
	{
		return null;
	}

	[Token(Token = "0x6001567")]
	[Address(RVA = "0x5D1D60", Offset = "0x5D0760", VA = "0x1805D1D60")]
	public Command GetFirstCommand()
	{
		return null;
	}

	[Token(Token = "0x6001568")]
	[Address(RVA = "0x5D1DB0", Offset = "0x5D07B0", VA = "0x1805D1DB0")]
	public Command GetLastCommand()
	{
		return null;
	}

	[Token(Token = "0x6001569")]
	[Address(RVA = "0x5D1E00", Offset = "0x5D0800", VA = "0x1805D1E00")]
	public int IndexOf(PlaybackSpot spot)
	{
		return default(int);
	}

	[Token(Token = "0x600156A")]
	[Address(RVA = "0x5D1F60", Offset = "0x5D0960", VA = "0x1805D1F60")]
	public int GetIndexByLine(int lineIndex, int inlineIndex)
	{
		return default(int);
	}

	[Token(Token = "0x600156B")]
	[Address(RVA = "0x5D1FF0", Offset = "0x5D09F0", VA = "0x1805D1FF0")]
	public int GetNestedHostIndex(int nestedIndex, [Optional] int? hostIndent)
	{
		return default(int);
	}

	[Token(Token = "0x600156C")]
	[Address(RVA = "0x5D2320", Offset = "0x5D0D20", VA = "0x1805D2320")]
	public Command.INestedHost GetNestedHost(int nestedIndex, [Optional] int? hostIndent)
	{
		return null;
	}

	[Token(Token = "0x600156D")]
	[Address(RVA = "0x5D23F0", Offset = "0x5D0DF0", VA = "0x1805D23F0")]
	public bool IsEnteringNestedAt(int index)
	{
		return default(bool);
	}

	[Token(Token = "0x600156E")]
	[Address(RVA = "0x5D2460", Offset = "0x5D0E60", VA = "0x1805D2460")]
	public bool IsExitingNestedAt(int index, int hostIndent)
	{
		return default(bool);
	}

	[Token(Token = "0x600156F")]
	[Address(RVA = "0x5D24D0", Offset = "0x5D0ED0", VA = "0x1805D24D0")]
	public int GetNestedExitIndexAt(int index, int hostIndent)
	{
		return default(int);
	}

	[Token(Token = "0x6001570")]
	[Address(RVA = "0x5D25B0", Offset = "0x5D0FB0", VA = "0x1805D25B0")]
	public int ExitNestedAt(int nestedIndex, int hostIndent)
	{
		return default(int);
	}

	[Token(Token = "0x6001571")]
	[Address(RVA = "0x5D2680", Offset = "0x5D1080", VA = "0x1805D2680")]
	public int SkipNestedAt(int nestedIndex, int hostIndent)
	{
		return default(int);
	}

	[Token(Token = "0x6001572")]
	[Address(RVA = "0x5D26C0", Offset = "0x5D10C0", VA = "0x1805D26C0")]
	public int MoveAt(int index)
	{
		return default(int);
	}
}
