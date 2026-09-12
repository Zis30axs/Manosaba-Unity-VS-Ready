using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000088")]
[ActorResources(typeof(GenericCharacterBehaviour), false)]
public class GenericCharacter : GenericActor<GenericCharacterBehaviour, CharacterMetadata>, ICharacterActor, IActor, LipSync.IReceiver
{
	[StructLayout(3)]
	[Token(Token = "0x2000089")]
	[CompilerGenerated]
	private struct _003CChangeLookDirection_003Ed__9
{
		[Token(Token = "0x40001DD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40001DE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40001DF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GenericCharacter _003C_003E4__this;

		[Token(Token = "0x40001E0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CharacterLookDirection lookDirection;

		[Token(Token = "0x40001E1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public Tween tween;

		[Token(Token = "0x40001E2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken token;

		[Token(Token = "0x40001E3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000264")]
		[Address(RVA = "0x45D040", Offset = "0x45BA40", VA = "0x18045D040", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000265")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200008A")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__6
{
		[Token(Token = "0x40001E4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40001E5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40001E6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GenericCharacter _003C_003E4__this;

		[Token(Token = "0x40001E7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000266")]
		[Address(RVA = "0x45D3A0", Offset = "0x45BDA0", VA = "0x18045D3A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000267")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40001DB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
	private CharacterLipSyncer lipSyncer;

	[Token(Token = "0x40001DC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
	private CharacterLookDirection lookDirection;

	[Token(Token = "0x17000063")]
	public CharacterLookDirection LookDirection
	{
		[Token(Token = "0x600025A")]
		[Address(RVA = "0x45C8C0", Offset = "0x45B2C0", VA = "0x18045C8C0", Slot = "65")]
		get
		{
			return default(CharacterLookDirection);
		}
		[Token(Token = "0x600025B")]
		[Address(RVA = "0x45C8D0", Offset = "0x45B2D0", VA = "0x18045C8D0", Slot = "66")]
		set
		{
		}
	}

	[Token(Token = "0x600025C")]
	[Address(RVA = "0x45C8F0", Offset = "0x45B2F0", VA = "0x18045C8F0")]
	public GenericCharacter(string id, CharacterMetadata meta, EmbeddedAppearanceLoader<GameObject> loader)
	{
	}

	[Token(Token = "0x600025D")]
	[Address(RVA = "0x45C970", Offset = "0x45B370", VA = "0x18045C970", Slot = "41")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__6))]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x600025E")]
	[Address(RVA = "0x45CA90", Offset = "0x45B490", VA = "0x18045CA90", Slot = "48")]
	public override void Dispose()
	{
	}

	[Token(Token = "0x600025F")]
	[Address(RVA = "0x45CB20", Offset = "0x45B520", VA = "0x18045CB20", Slot = "68")]
	public void AllowLipSync(bool active)
	{
	}

	[Token(Token = "0x6000260")]
	[Address(RVA = "0x45CB40", Offset = "0x45B540", VA = "0x18045CB40", Slot = "67")]
	[AsyncStateMachine(typeof(_003CChangeLookDirection_003Ed__9))]
	public UniTask ChangeLookDirection(CharacterLookDirection lookDirection, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000261")]
	[Address(RVA = "0x45CD00", Offset = "0x45B700", VA = "0x18045CD00", Slot = "69")]
	protected virtual void SetLookDirection(CharacterLookDirection lookDirection)
	{
	}

	[Token(Token = "0x6000262")]
	[Address(RVA = "0x45CDB0", Offset = "0x45B7B0", VA = "0x18045CDB0", Slot = "70")]
	protected virtual Quaternion LookDirectionToRotation(CharacterLookDirection lookDirection)
	{
		return default(Quaternion);
	}
}
