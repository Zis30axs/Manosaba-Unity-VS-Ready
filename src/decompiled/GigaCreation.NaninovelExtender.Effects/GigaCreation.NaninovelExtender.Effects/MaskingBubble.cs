using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using YieldAwaitable = Naninovel.YieldAwaitable;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;
using UnityEngine.Playables;

namespace GigaCreation.NaninovelExtender.Effects;

[Token(Token = "0x200000C")]
public class MaskingBubble : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IAwaitable
{
	[StructLayout(3)]
	[Token(Token = "0x200000D")]
	[CompilerGenerated]
	private struct _003CAwaitDestroy_003Ed__9
{
		[Token(Token = "0x4000041")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000042")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000043")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public MaskingBubble _003C_003E4__this;

		[Token(Token = "0x4000044")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x4000045")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000024")]
		[Address(RVA = "0xD083E0", Offset = "0xD06DE0", VA = "0x180D083E0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000025")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200000E")]
	[CompilerGenerated]
	private struct _003CAwaitSpawn_003Ed__8
{
		[Token(Token = "0x4000046")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000047")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000048")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public MaskingBubble _003C_003E4__this;

		[Token(Token = "0x4000049")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x400004A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000026")]
		[Address(RVA = "0xD087A0", Offset = "0xD071A0", VA = "0x180D087A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000027")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400003B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[SerializeField]
	private Transform _transform;

	[Token(Token = "0x400003C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private PlayableDirector _presentPlayableDirector;

	[Token(Token = "0x400003D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private PlayableDirector _dismissPlayableDirector;

	[Token(Token = "0x400003E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private SpriteRenderer[] _spriteRenderers;

	[Token(Token = "0x400003F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private bool _flip;

	[Token(Token = "0x4000040")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x41")]
	private bool _asap;

	[Token(Token = "0x600001F")]
	[Address(RVA = "0xD07EB0", Offset = "0xD068B0", VA = "0x180D07EB0")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000020")]
	[Address(RVA = "0xD07FB0", Offset = "0xD069B0", VA = "0x180D07FB0", Slot = "4")]
	public void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
	{
	}

	[Token(Token = "0x6000021")]
	[Address(RVA = "0xD080C0", Offset = "0xD06AC0", VA = "0x180D080C0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__8))]
	public UniTask AwaitSpawn([Optional] AsyncToken asyncToken)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000022")]
	[Address(RVA = "0xD08250", Offset = "0xD06C50", VA = "0x180D08250", Slot = "6")]
	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__9))]
	public UniTask AwaitDestroy([Optional] AsyncToken asyncToken)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000023")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public MaskingBubble()
	{
	}
}
