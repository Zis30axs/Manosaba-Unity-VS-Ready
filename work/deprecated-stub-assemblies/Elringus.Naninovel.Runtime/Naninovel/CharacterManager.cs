using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000035")]
    [InitializeAtRuntime(0, null)]
    public class CharacterManager : OrthoActorManager<ICharacterActor, CharacterState, CharacterMetadata, CharactersConfiguration>
    {
    	[Serializable]
    	[Token(Token = "0x2000036")]
    	public new class GameState
    	{
    		[Token(Token = "0x40000B5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public SerializableLiteralStringMap CharIdToAvatarPathMap;

    		[Token(Token = "0x60000DF")]
    		[Address(RVA = "0x450F10", Offset = "0x44F910", VA = "0x180450F10")]
    		public GameState()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000039")]
    	[CompilerGenerated]
    	private struct _003CArrangeCharacters_003Ed__21
    {
    		[Token(Token = "0x40000BA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000BB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000BC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CharacterManager _003C_003E4__this;

    		[Token(Token = "0x40000BD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public Tween tween;

    		[Token(Token = "0x40000BE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x40000BF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		public bool lookAtOrigin;

    		[Token(Token = "0x40000C0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private PooledObject<List<UniTask>> _003C__003E5__2;

    		[Token(Token = "0x40000C1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60000E6")]
    		[Address(RVA = "0x451200", Offset = "0x44FC00", VA = "0x180451200", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000E7")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200003A")]
    	[CompilerGenerated]
    	private struct _003CConstructActor_003Ed__22
    {
    		[Token(Token = "0x40000C2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000C3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<ICharacterActor> _003C_003Et__builder;

    		[Token(Token = "0x40000C4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CharacterManager _003C_003E4__this;

    		[Token(Token = "0x40000C5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string actorId;

    		[Token(Token = "0x40000C6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<ICharacterActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60000E8")]
    		[Address(RVA = "0x451B30", Offset = "0x450530", VA = "0x180451B30", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000E9")]
    		[Address(RVA = "0x451FB0", Offset = "0x4509B0", VA = "0x180451FB0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200003B")]
    	[CompilerGenerated]
    	private struct _003CInitializeService_003Ed__10
    {
    		[Token(Token = "0x40000C7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000C8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000C9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CharacterManager _003C_003E4__this;

    		[Token(Token = "0x40000CA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40000CB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask<IReadOnlyCollection<Resource<Texture2D>>>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x40000CC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<Resource>.Awaiter _003C_003Eu__3;

    		[Token(Token = "0x60000EA")]
    		[Address(RVA = "0x451FE0", Offset = "0x4509E0", VA = "0x180451FE0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000EB")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200003C")]
    	[CompilerGenerated]
    	private struct _003CLoadServiceState_003Ed__13
    {
    		[Token(Token = "0x40000CD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000CE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000CF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CharacterManager _003C_003E4__this;

    		[Token(Token = "0x40000D0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public GameStateMap stateMap;

    		[Token(Token = "0x40000D1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60000EC")]
    		[Address(RVA = "0x452830", Offset = "0x451230", VA = "0x180452830", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000ED")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40000B1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private readonly ITextManager docs;

    	[Token(Token = "0x40000B2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private readonly ITextPrinterManager printers;

    	[Token(Token = "0x40000B3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	private readonly SerializableLiteralStringMap avatarPathByCharId;

    	[Token(Token = "0x40000B4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	private ResourceLoader<Texture2D> avatarTextureLoader;

    	[Token(Token = "0x14000003")]
    	public event Action<CharacterAvatarChangedArgs> OnCharacterAvatarChanged
    	{
    		[Token(Token = "0x60000C8")]
    		[Address(RVA = "0x44DD00", Offset = "0x44C700", VA = "0x18044DD00", Slot = "48")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60000C9")]
    		[Address(RVA = "0x44DE10", Offset = "0x44C810", VA = "0x18044DE10", Slot = "49")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x60000CA")]
    	[Address(RVA = "0x44DF20", Offset = "0x44C920", VA = "0x18044DF20")]
    	public CharacterManager(CharactersConfiguration config = default, CameraConfiguration cameraConfig = default, ITextManager docs = default, ITextPrinterManager printers = default)
    	{
    	}

    	[Token(Token = "0x60000CB")]
    	[Address(RVA = "0x44E0C0", Offset = "0x44CAC0", VA = "0x18044E0C0", Slot = "29")]
    	public override void ResetService()
    	{
    	}

    	[Token(Token = "0x60000CC")]
    	[Address(RVA = "0x44E170", Offset = "0x44CB70", VA = "0x18044E170", Slot = "28")]
    	[AsyncStateMachine(typeof(_003CInitializeService_003Ed__10))]
    	public override UniTask InitializeService()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60000CD")]
    	[Address(RVA = "0x44E2A0", Offset = "0x44CCA0", VA = "0x18044E2A0", Slot = "30")]
    	public override void DestroyService()
    	{
    	}

    	[Token(Token = "0x60000CE")]
    	[Address(RVA = "0x44E3E0", Offset = "0x44CDE0", VA = "0x18044E3E0", Slot = "31")]
    	public override void SaveServiceState(GameStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x60000CF")]
    	[Address(RVA = "0x44E6C0", Offset = "0x44D0C0", VA = "0x18044E6C0", Slot = "32")]
    	[AsyncStateMachine(typeof(_003CLoadServiceState_003Ed__13))]
    	public override UniTask LoadServiceState(GameStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60000D0")]
    	[Address(RVA = "0x44E840", Offset = "0x44D240", VA = "0x18044E840", Slot = "58")]
    	public virtual bool AvatarTextureExists(string avatarTexturePath)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60000D1")]
    	[Address(RVA = "0x44E870", Offset = "0x44D270", VA = "0x18044E870", Slot = "59")]
    	public virtual void RemoveAvatarTextureFor(string characterId)
    	{
    	}

    	[Token(Token = "0x60000D2")]
    	[Address(RVA = "0x44E940", Offset = "0x44D340", VA = "0x18044E940", Slot = "60")]
    	public virtual Texture2D GetAvatarTextureFor(string characterId)
    	{
    		return null;
    	}

    	[Token(Token = "0x60000D3")]
    	[Address(RVA = "0x44EA50", Offset = "0x44D450", VA = "0x18044EA50", Slot = "61")]
    	public virtual string GetAvatarTexturePathFor(string characterId)
    	{
    		return null;
    	}

    	[Token(Token = "0x60000D4")]
    	[Address(RVA = "0x44EB70", Offset = "0x44D570", VA = "0x18044EB70", Slot = "62")]
    	public virtual void SetAvatarTexturePathFor(string characterId, string avatarTexturePath)
    	{
    	}

    	[Token(Token = "0x60000D5")]
    	[Address(RVA = "0x44EEC0", Offset = "0x44D8C0", VA = "0x18044EEC0", Slot = "63")]
    	public virtual string GetAuthorName(string characterId)
    	{
    		return null;
    	}

    	[Token(Token = "0x60000D6")]
    	[Address(RVA = "0x44F250", Offset = "0x44DC50", VA = "0x18044F250", Slot = "64")]
    	public virtual CharacterLookDirection LookAtOriginDirection(float xPos)
    	{
    		return default(CharacterLookDirection);
    	}

    	[Token(Token = "0x60000D7")]
    	[Address(RVA = "0x44F2D0", Offset = "0x44DCD0", VA = "0x18044F2D0", Slot = "65")]
    	[AsyncStateMachine(typeof(_003CArrangeCharacters_003Ed__21))]
    	public virtual UniTask ArrangeCharacters(bool lookAtOrigin, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60000D8")]
    	[Address(RVA = "0x44F490", Offset = "0x44DE90", VA = "0x18044F490", Slot = "41")]
    	[AsyncStateMachine(typeof(_003CConstructActor_003Ed__22))]
    	protected override UniTask<ICharacterActor> ConstructActor(string actorId)
    	{
    		return default(UniTask<ICharacterActor>);
    	}

    	[Token(Token = "0x60000D9")]
    	[Address(RVA = "0x44F680", Offset = "0x44E080", VA = "0x18044F680", Slot = "66")]
    	protected virtual void HandleAuthorHighlighting(PrintMessageArgs args)
    	{
    	}

    	[Token(Token = "0x60000DA")]
    	[Address(RVA = "0x450540", Offset = "0x44EF40", VA = "0x180450540", Slot = "67")]
    	protected virtual void ApplyPose(ICharacterActor actor, string poseName, Tween tween)
    	{
    	}
    }
}
