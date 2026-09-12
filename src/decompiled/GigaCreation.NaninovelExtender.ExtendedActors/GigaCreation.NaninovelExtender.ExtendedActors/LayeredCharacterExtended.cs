using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x200002C")]
[UsedImplicitly]
[ActorResources(typeof(LayeredCharacterBehaviour), false)]
public class LayeredCharacterExtended : LayeredCharacter, IMultipliableActor, IPivotChangeableActor, IMaskabilityChangeableActor
{
	[StructLayout(3)]
	[Token(Token = "0x200002D")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__13
{
		[Token(Token = "0x4000099")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400009A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400009B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public LayeredCharacterExtended _003C_003E4__this;

		[Token(Token = "0x400009C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000095")]
		[Address(RVA = "0xD1D690", Offset = "0xD1C090", VA = "0x180D1D690", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000096")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x1700000D")]
	public string SubId
	{
		[Token(Token = "0x6000089")]
		[Address(RVA = "0xD1CE80", Offset = "0xD1B880", VA = "0x180D1CE80", Slot = "74")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700000E")]
	public Vector2 Pivot
	{
		[Token(Token = "0x600008A")]
		[Address(RVA = "0xD1CE90", Offset = "0xD1B890", VA = "0x180D1CE90", Slot = "75")]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x600008B")]
		[Address(RVA = "0xD1CED0", Offset = "0xD1B8D0", VA = "0x180D1CED0", Slot = "76")]
		set
		{
		}
	}

	[Token(Token = "0x1700000F")]
	public bool Masked
	{
		[Token(Token = "0x600008C")]
		[Address(RVA = "0xD1CF30", Offset = "0xD1B930", VA = "0x180D1CF30", Slot = "78")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x600008D")]
		[Address(RVA = "0xD1D030", Offset = "0xD1BA30", VA = "0x180D1D030", Slot = "79")]
		set
		{
		}
	}

	[Token(Token = "0x600008E")]
	[Address(RVA = "0x461C60", Offset = "0x460660", VA = "0x180461C60")]
	public LayeredCharacterExtended(string id, CharacterMetadata meta, EmbeddedAppearanceLoader<GameObject> loader)
	{
	}

	[Token(Token = "0x600008F")]
	[Address(RVA = "0xD1D140", Offset = "0xD1BB40", VA = "0x180D1D140")]
	public LayeredCharacterExtended(string actorId, string subId, CharacterMetadata meta, EmbeddedAppearanceLoader<GameObject> loader)
	{
	}

	[Token(Token = "0x6000090")]
	[Address(RVA = "0xD1D250", Offset = "0xD1BC50", VA = "0x180D1D250", Slot = "77")]
	public void ChangePivot(float? x, float? y)
	{
	}

	[Token(Token = "0x6000091")]
	[Address(RVA = "0xD1D030", Offset = "0xD1BA30", VA = "0x180D1D030", Slot = "80")]
	public void ChangeMaskability(bool enable)
	{
	}

	[Token(Token = "0x6000092")]
	[Address(RVA = "0xD1D570", Offset = "0xD1BF70", VA = "0x180D1D570", Slot = "41")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__13))]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000093")]
	[Address(RVA = "0xD1CF30", Offset = "0xD1B930", VA = "0x180D1CF30")]
	private bool IsMasked()
	{
		return default(bool);
	}
}
