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
    [Token(Token = "0x2000047")]
    [InitializeAtRuntime(0, null)]
    public class ChoiceHandlerManager : ActorManager<IChoiceHandlerActor, ChoiceHandlerState, ChoiceHandlerMetadata, ChoiceHandlersConfiguration>
    {
    	[Serializable]
    	[Token(Token = "0x2000048")]
    	public new class GameState
    	{
    		[Token(Token = "0x40000F7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public PickedChoice[] PickedChoices;

    		[Token(Token = "0x6000115")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public GameState()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x2000049")]
    	public struct PickedChoice
    	{
    		[Token(Token = "0x40000F8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public PlaybackSpot HostedAt;

    		[Token(Token = "0x40000F9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public PlaybackSpot PickedAt;
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200004B")]
    	[CompilerGenerated]
    	private struct _003CInitializeService_003Ed__11
    {
    		[Token(Token = "0x40000FC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000FD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000FE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ChoiceHandlerManager _003C_003E4__this;

    		[Token(Token = "0x40000FF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000119")]
    		[Address(RVA = "0x454580", Offset = "0x452F80", VA = "0x180454580", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600011A")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40000F4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private readonly IResourceProviderManager resources;

    	[Token(Token = "0x40000F5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private readonly ILocalizationManager l10n;

    	[Token(Token = "0x40000F6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private LocalizableResourceLoader<GameObject> buttonLoader;

    	[Token(Token = "0x1700001E")]
    	public virtual IResourceLoader<GameObject> ChoiceButtonLoader
    	{
    		[Token(Token = "0x600010B")]
    		[Address(RVA = "0x453950", Offset = "0x452350", VA = "0x180453950", Slot = "46")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700001F")]
    	protected virtual Dictionary<PlaybackSpot, PlaybackSpot> PickedChoices
    	{
    		[Token(Token = "0x600010C")]
    		[Address(RVA = "0x44CE10", Offset = "0x44B810", VA = "0x18044CE10", Slot = "47")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x600010D")]
    	[Address(RVA = "0x453960", Offset = "0x452360", VA = "0x180453960")]
    	public ChoiceHandlerManager(ChoiceHandlersConfiguration config = default, IResourceProviderManager resources = default, ILocalizationManager l10n = default)
    	{
    	}

    	[Token(Token = "0x600010E")]
    	[Address(RVA = "0x453B10", Offset = "0x452510", VA = "0x180453B10", Slot = "28")]
    	[AsyncStateMachine(typeof(_003CInitializeService_003Ed__11))]
    	public override UniTask InitializeService()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600010F")]
    	[Address(RVA = "0x453C30", Offset = "0x452630", VA = "0x180453C30", Slot = "30")]
    	public override void DestroyService()
    	{
    	}

    	[Token(Token = "0x6000110")]
    	[Address(RVA = "0x453CC0", Offset = "0x4526C0", VA = "0x180453CC0", Slot = "31")]
    	public override void SaveServiceState(GameStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x6000111")]
    	[Address(RVA = "0x453FE0", Offset = "0x4529E0", VA = "0x180453FE0", Slot = "32")]
    	public override UniTask LoadServiceState(GameStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000112")]
    	[Address(RVA = "0x4541A0", Offset = "0x452BA0", VA = "0x1804541A0", Slot = "48")]
    	public virtual void PushPickedChoice(PlaybackSpot hostedAt, PlaybackSpot continueAt)
    	{
    	}

    	[Token(Token = "0x6000113")]
    	[Address(RVA = "0x454240", Offset = "0x452C40", VA = "0x180454240", Slot = "49")]
    	public virtual PlaybackSpot PopPickedChoice(PlaybackSpot hostedAt)
    	{
    		return default(PlaybackSpot);
    	}
    }
}
