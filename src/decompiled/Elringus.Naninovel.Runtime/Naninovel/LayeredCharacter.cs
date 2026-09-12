using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x200009C")]
[ActorResources(typeof(LayeredCharacterBehaviour), false)]
public class LayeredCharacter : LayeredActor<LayeredCharacterBehaviour, CharacterMetadata>, ICharacterActor, IActor, LipSync.IReceiver
{
	[StructLayout(3)]
	[Token(Token = "0x200009D")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__5
{
		[Token(Token = "0x4000233")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000234")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000235")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public LayeredCharacter _003C_003E4__this;

		[Token(Token = "0x4000236")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60002D2")]
		[Address(RVA = "0x461F90", Offset = "0x460990", VA = "0x180461F90", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60002D3")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000232")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
	private CharacterLipSyncer lipSyncer;

	[Token(Token = "0x17000073")]
	public CharacterLookDirection LookDirection
	{
		[Token(Token = "0x60002CA")]
		[Address(RVA = "0x461B60", Offset = "0x460560", VA = "0x180461B60", Slot = "70")]
		get
		{
			return default(CharacterLookDirection);
		}
		[Token(Token = "0x60002CB")]
		[Address(RVA = "0x461BE0", Offset = "0x4605E0", VA = "0x180461BE0", Slot = "71")]
		set
		{
		}
	}

	[Token(Token = "0x60002CC")]
	[Address(RVA = "0x461C60", Offset = "0x460660", VA = "0x180461C60")]
	public LayeredCharacter(string id = default, CharacterMetadata meta = default, EmbeddedAppearanceLoader<GameObject> loader = default)
	{
	}

	[Token(Token = "0x60002CD")]
	[Address(RVA = "0x461CE0", Offset = "0x4606E0", VA = "0x180461CE0", Slot = "41")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__5))]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60002CE")]
	[Address(RVA = "0x461E00", Offset = "0x460800", VA = "0x180461E00", Slot = "72")]
	public UniTask ChangeLookDirection(CharacterLookDirection lookDirection, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60002CF")]
	[Address(RVA = "0x461ED0", Offset = "0x4608D0", VA = "0x180461ED0", Slot = "48")]
	public override void Dispose()
	{
	}

	[Token(Token = "0x60002D0")]
	[Address(RVA = "0x461F30", Offset = "0x460930", VA = "0x180461F30", Slot = "73")]
	public void AllowLipSync(bool active)
	{
	}
}
