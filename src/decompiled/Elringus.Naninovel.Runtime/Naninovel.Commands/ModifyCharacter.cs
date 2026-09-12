using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x200067C")]
[CommandAlias("char")]
[ActorContext("Characters", -1, "Id")]
[ConstantContext("Poses/Characters/{:Id??:IdAndAppearance[0]}+Poses/Characters/*", -1, "Pose")]
public class ModifyCharacter : ModifyOrthoActor<ICharacterActor, CharacterState, CharacterMetadata, CharactersConfiguration, ICharacterManager>
{
	[StructLayout(3)]
	[Token(Token = "0x200067D")]
	[CompilerGenerated]
	private struct _003CApplyLookDirectionModification_003Ed__17
{
		[Token(Token = "0x40018C9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40018CA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40018CB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ModifyCharacter _003C_003E4__this;

		[Token(Token = "0x40018CC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Tween tween;

		[Token(Token = "0x40018CD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public ICharacterActor actor;

		[Token(Token = "0x40018CE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public AsyncToken token;

		[Token(Token = "0x40018CF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001FD3")]
		[Address(RVA = "0x4D9720", Offset = "0x4D8120", VA = "0x1804D9720", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001FD4")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200067E")]
	[CompilerGenerated]
	private struct _003CApplyModifications_003Ed__15
{
		[Token(Token = "0x40018D0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40018D1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40018D2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ModifyCharacter _003C_003E4__this;

		[Token(Token = "0x40018D3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public ICharacterActor actor;

		[Token(Token = "0x40018D4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public EasingType easingType;

		[Token(Token = "0x40018D5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken token;

		[Token(Token = "0x40018D6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private PooledObject<List<UniTask>> _003C__003E5__2;

		[Token(Token = "0x40018D7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001FD5")]
		[Address(RVA = "0x4D9AC0", Offset = "0x4D84C0", VA = "0x1804D9AC0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001FD6")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200067F")]
	[CompilerGenerated]
	private struct _003CModify_003Ed__14
{
		[Token(Token = "0x40018D8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40018D9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40018DA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ModifyCharacter _003C_003E4__this;

		[Token(Token = "0x40018DB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40018DC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001FD7")]
		[Address(RVA = "0x4DA420", Offset = "0x4D8E20", VA = "0x1804DA420", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001FD8")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40018C5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
	[ParameterAlias("")]
	[RequiredParameter]
	[ActorContext("Characters", 0, null)]
	[AppearanceContext(1, null, null)]
	public NamedStringParameter IdAndAppearance;

	[Token(Token = "0x40018C6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
	[ParameterAlias("look")]
	[ConstantContext(typeof(CharacterLookDirection), -1, null)]
	public StringParameter LookDirection;

	[Token(Token = "0x40018C7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
	[ParameterAlias("avatar")]
	public StringParameter AvatarTexturePath;

	[Token(Token = "0x40018C8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	[IgnoreParameter(null)]
	public BooleanParameter IsGenericPrefix;

	[Token(Token = "0x170004BA")]
	protected override bool AllowPreload
	{
		[Token(Token = "0x6001FC6")]
		[Address(RVA = "0x4D88A0", Offset = "0x4D72A0", VA = "0x1804D88A0", Slot = "30")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x170004BB")]
	protected override string AssignedId
	{
		[Token(Token = "0x6001FC7")]
		[Address(RVA = "0x4D88F0", Offset = "0x4D72F0", VA = "0x1804D88F0", Slot = "18")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004BC")]
	protected override string AlternativeAppearance
	{
		[Token(Token = "0x6001FC8")]
		[Address(RVA = "0x4D8970", Offset = "0x4D7370", VA = "0x1804D8970", Slot = "29")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004BD")]
	protected virtual CharacterLookDirection? AssignedLookDirection
	{
		[Token(Token = "0x6001FC9")]
		[Address(RVA = "0x4D8A40", Offset = "0x4D7440", VA = "0x1804D8A40", Slot = "46")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004BE")]
	protected CharacterLookDirection? PosedLookDirection
	{
		[Token(Token = "0x6001FCA")]
		[Address(RVA = "0x4D8BD0", Offset = "0x4D75D0", VA = "0x1804D8BD0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001FCB")]
	[Address(RVA = "0x4D8C70", Offset = "0x4D7670", VA = "0x1804D8C70", Slot = "34")]
	[AsyncStateMachine(typeof(_003CModify_003Ed__14))]
	protected override UniTask Modify(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001FCC")]
	[Address(RVA = "0x4D8DF0", Offset = "0x4D77F0", VA = "0x1804D8DF0", Slot = "35")]
	[AsyncStateMachine(typeof(_003CApplyModifications_003Ed__15))]
	protected override UniTask ApplyModifications(ICharacterActor actor, EasingType easingType, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001FCD")]
	[Address(RVA = "0x4D9000", Offset = "0x4D7A00", VA = "0x1804D9000", Slot = "47")]
	protected virtual bool ShouldAutoArrange(ICharacterActor actor)
	{
		return default(bool);
	}

	[Token(Token = "0x6001FCE")]
	[Address(RVA = "0x4D92E0", Offset = "0x4D7CE0", VA = "0x1804D92E0", Slot = "48")]
	[AsyncStateMachine(typeof(_003CApplyLookDirectionModification_003Ed__17))]
	protected virtual UniTask ApplyLookDirectionModification(ICharacterActor actor, Tween tween, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001FCF")]
	[Address(RVA = "0x4D94F0", Offset = "0x4D7EF0", VA = "0x1804D94F0", Slot = "49")]
	protected virtual CharacterLookDirection? ParseLookDirection(string value)
	{
		return null;
	}

	[Token(Token = "0x6001FD0")]
	[Address(RVA = "0x4D9600", Offset = "0x4D8000", VA = "0x1804D9600")]
	public ModifyCharacter()
	{
	}
}
