using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.ManagedText;

namespace Naninovel;

[Token(Token = "0x20003A7")]
public class ManagedTextFieldAssigner
{
	[Token(Token = "0x20003A8")]
	private struct ManagedField
	{
		[Token(Token = "0x4000D9A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public FieldInfo Info;

		[Token(Token = "0x4000D9B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public ManagedTextAttribute Attribute;
	}

	[StructLayout(3)]
	[Token(Token = "0x20003A9")]
	[CompilerGenerated]
	private struct _003CAssign_003Ed__5
{
		[Token(Token = "0x4000D9C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000D9D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000D9E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ManagedTextFieldAssigner _003C_003E4__this;

		[Token(Token = "0x4000D9F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60011B7")]
		[Address(RVA = "0x5929C0", Offset = "0x5913C0", VA = "0x1805929C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60011B8")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20003AA")]
	[CompilerGenerated]
	private struct _003CAssignField_003Ed__8
{
		[Token(Token = "0x4000DA0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000DA1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000DA2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ManagedField field;

		[Token(Token = "0x4000DA3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public ManagedTextFieldAssigner _003C_003E4__this;

		[Token(Token = "0x4000DA4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private string _003CdocumentPath_003E5__2;

		[Token(Token = "0x4000DA5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<Resource>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60011B9")]
		[Address(RVA = "0x593170", Offset = "0x591B70", VA = "0x180593170", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60011BA")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000D97")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private readonly List<UniTask> tasks;

	[Token(Token = "0x4000D98")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private readonly List<ManagedField> fields;

	[Token(Token = "0x4000D99")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private readonly ITextManager docs;

	[Token(Token = "0x60011B1")]
	[Address(RVA = "0x592240", Offset = "0x590C40", VA = "0x180592240")]
	public ManagedTextFieldAssigner(ITextManager docs)
	{
	}

	[Token(Token = "0x60011B2")]
	[Address(RVA = "0x592410", Offset = "0x590E10", VA = "0x180592410")]
	[AsyncStateMachine(typeof(_003CAssign_003Ed__5))]
	public UniTask Assign()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60011B3")]
	[Address(RVA = "0x592530", Offset = "0x590F30", VA = "0x180592530")]
	private void Reset()
	{
	}

	[Token(Token = "0x60011B4")]
	[Address(RVA = "0x5925E0", Offset = "0x590FE0", VA = "0x1805925E0")]
	private IReadOnlyCollection<ManagedField> GetManagedFields(Type hostType)
	{
		return null;
	}

	[Token(Token = "0x60011B5")]
	[Address(RVA = "0x5927E0", Offset = "0x5911E0", VA = "0x1805927E0")]
	[AsyncStateMachine(typeof(_003CAssignField_003Ed__8))]
	private UniTask AssignField(ManagedField field)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60011B6")]
	[Address(RVA = "0x592970", Offset = "0x591370", VA = "0x180592970")]
	private string GetValueWithFallback(ManagedTextRecord record)
	{
		return null;
	}
}
