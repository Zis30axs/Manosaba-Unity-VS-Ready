using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x200004F")]
public class ChoiceHandlerState : ActorState<IChoiceHandlerActor>
{
	[StructLayout(3)]
	[Token(Token = "0x2000050")]
	[CompilerGenerated]
	private struct _003CApplyToActor_003Ed__4
{
		[Token(Token = "0x4000107")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000108")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000109")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ChoiceHandlerState _003C_003E4__this;

		[Token(Token = "0x400010A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public IChoiceHandlerActor actor;

		[Token(Token = "0x400010B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private PooledObject<List<ChoiceState>> _003C__003E5__2;

		[Token(Token = "0x400010C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400010D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private List<ChoiceState>.Enumerator _003C_003E7__wrap2;

		[Token(Token = "0x400010E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
		private ChoiceState _003Cchoice_003E5__4;

		[Token(Token = "0x6000128")]
		[Address(RVA = "0x4553F0", Offset = "0x453DF0", VA = "0x1804553F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000129")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000106")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[SerializeField]
	private List<ChoiceState> choices;

	[Token(Token = "0x17000022")]
	public List<ChoiceState> Choices
	{
		[Token(Token = "0x6000123")]
		[Address(RVA = "0x455000", Offset = "0x453A00", VA = "0x180455000")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000124")]
	[Address(RVA = "0x455080", Offset = "0x453A80", VA = "0x180455080", Slot = "4")]
	public override void OverwriteFromActor(IChoiceHandlerActor actor)
	{
	}

	[Token(Token = "0x6000125")]
	[Address(RVA = "0x455130", Offset = "0x453B30", VA = "0x180455130", Slot = "5")]
	[AsyncStateMachine(typeof(_003CApplyToActor_003Ed__4))]
	public override UniTask ApplyToActor(IChoiceHandlerActor actor)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000126")]
	[Address(RVA = "0x4552C0", Offset = "0x453CC0", VA = "0x1804552C0")]
	public ChoiceHandlerState()
	{
	}
}
