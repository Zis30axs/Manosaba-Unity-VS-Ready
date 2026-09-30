#nullable enable annotations
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    [Token(Token = "0x20004F8")]
    public class BacklogPanel : CustomUI, IBacklogUI, IManagedUI, ILocalizableUI
    {
    	[Serializable]
    	[Token(Token = "0x20004F9")]
    	public new class GameState
    	{
    		[Token(Token = "0x4001363")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public List<BacklogMessage> Messages;

    		[Token(Token = "0x60018E3")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public GameState()
    		{
    		}
    	}

    	[Token(Token = "0x20004FB")]
    	[CompilerGenerated]
    	private sealed class _003C_003Ec__DisplayClass46_0
    	{
    		[Token(Token = "0x4001366")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public HashSet<LocalizableText> deferRelease;

    		[Token(Token = "0x4001367")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public BacklogPanel _003C_003E4__this;

    		[Token(Token = "0x60018E7")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public _003C_003Ec__DisplayClass46_0()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20004FD")]
    	[CompilerGenerated]
    	private struct _003CDeserializeState_003Ed__46
    {
    		[Token(Token = "0x400136D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400136E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400136F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public BacklogPanel _003C_003E4__this;

    		[Token(Token = "0x4001370")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public GameStateMap stateMap;

    		[Token(Token = "0x4001371")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private _003C_003Ec__DisplayClass46_0 _003C_003E8__1;

    		[Token(Token = "0x4001372")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60018EB")]
    		[Address(RVA = "0x610C30", Offset = "0x60F630", VA = "0x180610C30", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60018EC")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20004FE")]
    	[CompilerGenerated]
    	private struct _003CScrollToBottom_003Ed__51
    {
    		[Token(Token = "0x4001373")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001374")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001375")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public BacklogPanel _003C_003E4__this;

    		[Token(Token = "0x4001376")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60018ED")]
    		[Address(RVA = "0x6115A0", Offset = "0x60FFA0", VA = "0x1806115A0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60018EE")]
    		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001353")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
    	[SerializeField]
    	private RectTransform messagesContainer;

    	[Token(Token = "0x4001354")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
    	[SerializeField]
    	private ScrollRect scrollRect;

    	[Token(Token = "0x4001355")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
    	[SerializeField]
    	private BacklogMessageUI messagePrefab;

    	[Token(Token = "0x4001356")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
    	[Tooltip("How many messages should the backlog keep.")]
    	[SerializeField]
    	private int capacity;

    	[Token(Token = "0x4001357")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xFC")]
    	[Tooltip("How many messages should the backlog keep when saving the game.")]
    	[SerializeField]
    	private int saveCapacity;

    	[Token(Token = "0x4001358")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
    	[Tooltip("Whether to add choices summary to the log.")]
    	[SerializeField]
    	private bool addChoices;

    	[Token(Token = "0x4001359")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
    	[Tooltip("Template to use for selected choice summary. %SUMMARY% will be replaced with the actual choice summary.")]
    	[SerializeField]
    	private string selectedChoiceTemplate;

    	[Token(Token = "0x400135A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
    	[Tooltip("Template to use for other (not selected) choice summary. %SUMMARY% will be replaced with the actual choice summary.")]
    	[SerializeField]
    	private string otherChoiceTemplate;

    	[Token(Token = "0x400135B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x118")]
    	[Tooltip("String added between consequent choices.")]
    	[SerializeField]
    	private string choiceSeparator;

    	[Token(Token = "0x400135C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x120")]
    	[Tooltip("Whether to allow replaying voices associated with the backlogged messages.")]
    	[SerializeField]
    	private bool allowReplayVoice;

    	[Token(Token = "0x400135D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x121")]
    	[Tooltip("Whether to allow rolling back to playback spots associated with the backlogged messages.")]
    	[SerializeField]
    	private bool allowRollback;

    	[Token(Token = "0x400135E")]
    	private const string choiceTemplateLiteral = "%SUMMARY%";

    	[Token(Token = "0x400135F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x128")]
    	private readonly LinkedList<BacklogMessageUI> messages;

    	[Token(Token = "0x4001360")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x130")]
    	private readonly Stack<BacklogMessageUI> messagesPool;

    	[Token(Token = "0x4001361")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x138")]
    	private readonly List<LocalizableText> formatPool;

    	[Token(Token = "0x4001362")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x140")]
    	private IStateManager stateManager;

    	[Token(Token = "0x17000385")]
    	protected virtual BacklogMessageUI LastMessage
    	{
    		[Token(Token = "0x60018C7")]
    		[Address(RVA = "0x60E260", Offset = "0x60CC60", VA = "0x18060E260", Slot = "100")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000386")]
    	protected virtual RectTransform MessagesContainer
    	{
    		[Token(Token = "0x60018C8")]
    		[Address(RVA = "0x33E0B0", Offset = "0x33CAB0", VA = "0x18033E0B0", Slot = "101")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000387")]
    	protected virtual ScrollRect ScrollRect
    	{
    		[Token(Token = "0x60018C9")]
    		[Address(RVA = "0x49D2A0", Offset = "0x49BCA0", VA = "0x18049D2A0", Slot = "102")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000388")]
    	protected virtual BacklogMessageUI MessagePrefab
    	{
    		[Token(Token = "0x60018CA")]
    		[Address(RVA = "0x49D2B0", Offset = "0x49BCB0", VA = "0x18049D2B0", Slot = "103")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000389")]
    	protected virtual int Capacity
    	{
    		[Token(Token = "0x60018CB")]
    		[Address(RVA = "0x60E310", Offset = "0x60CD10", VA = "0x18060E310", Slot = "104")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x1700038A")]
    	protected virtual int SaveCapacity
    	{
    		[Token(Token = "0x60018CC")]
    		[Address(RVA = "0x60E330", Offset = "0x60CD30", VA = "0x18060E330", Slot = "105")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x1700038B")]
    	protected virtual bool AddChoices
    	{
    		[Token(Token = "0x60018CD")]
    		[Address(RVA = "0x60E340", Offset = "0x60CD40", VA = "0x18060E340", Slot = "106")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700038C")]
    	protected virtual bool AllowReplayVoice
    	{
    		[Token(Token = "0x60018CE")]
    		[Address(RVA = "0x60E350", Offset = "0x60CD50", VA = "0x18060E350", Slot = "107")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700038D")]
    	protected virtual bool AllowRollback
    	{
    		[Token(Token = "0x60018CF")]
    		[Address(RVA = "0x60E360", Offset = "0x60CD60", VA = "0x18060E360", Slot = "108")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700038E")]
    	protected virtual string ChoiceSeparator
    	{
    		[Token(Token = "0x60018D0")]
    		[Address(RVA = "0x34DB60", Offset = "0x34C560", VA = "0x18034DB60", Slot = "109")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60018D1")]
    	[Address(RVA = "0x33E9B0", Offset = "0x33D3B0", VA = "0x18033E9B0", Slot = "83")]
    	public override UniTask Initialize()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60018D2")]
    	[Address(RVA = "0x60E370", Offset = "0x60CD70", VA = "0x18060E370", Slot = "110")]
    	public virtual void AddMessage(LocalizableText text, [Optional] string actorId, [Optional] PlaybackSpot? rollbackSpot, [Optional] string voicePath)
    	{
    	}

    	[Token(Token = "0x60018D3")]
    	[Address(RVA = "0x60E530", Offset = "0x60CF30", VA = "0x18060E530", Slot = "111")]
    	public virtual void AppendMessage(LocalizableText text, [Optional] string voicePath)
    	{
    	}

    	[Token(Token = "0x60018D4")]
    	[Address(RVA = "0x60E6D0", Offset = "0x60D0D0", VA = "0x18060E6D0", Slot = "112")]
    	public virtual void AddChoice(IReadOnlyList<BacklogChoice> choices)
    	{
    	}

    	[Token(Token = "0x60018D5")]
    	[Address(RVA = "0x60E7C0", Offset = "0x60D1C0", VA = "0x18060E7C0", Slot = "113")]
    	public virtual void Clear()
    	{
    	}

    	[Token(Token = "0x60018D6")]
    	[Address(RVA = "0x60E7E0", Offset = "0x60D1E0", VA = "0x18060E7E0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x60018D7")]
    	[Address(RVA = "0x60E9C0", Offset = "0x60D3C0", VA = "0x18060E9C0", Slot = "114")]
    	protected virtual void SpawnMessage(BacklogMessage message)
    	{
    	}

    	[Token(Token = "0x60018D8")]
    	[Address(RVA = "0x60EDD0", Offset = "0x60D7D0", VA = "0x18060EDD0", Slot = "55")]
    	protected override void HandleVisibilityChanged(bool visible)
    	{
    	}

    	[Token(Token = "0x60018D9")]
    	[Address(RVA = "0x60EE50", Offset = "0x60D850", VA = "0x18060EE50", Slot = "87")]
    	protected override void SerializeState(GameStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x60018DA")]
    	[Address(RVA = "0x60F1D0", Offset = "0x60DBD0", VA = "0x18060F1D0", Slot = "88")]
    	[AsyncStateMachine(typeof(_003CDeserializeState_003Ed__46))]
    	protected override UniTask DeserializeState(GameStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60018DB")]
    	[Address(RVA = "0x60F350", Offset = "0x60DD50", VA = "0x18060F350", Slot = "115")]
    	protected virtual void ClearMessages([Optional] HashSet<LocalizableText> deferRelease)
    	{
    	}

    	[Token(Token = "0x60018DC")]
    	[Address(RVA = "0x60F6C0", Offset = "0x60E0C0", VA = "0x18060F6C0", Slot = "116")]
    	protected virtual PlaybackSpot ProcessRollbackSpot(PlaybackSpot? spot)
    	{
    		return default(PlaybackSpot);
    	}

    	[Token(Token = "0x60018DD")]
    	[Address(RVA = "0x60FC40", Offset = "0x60E640", VA = "0x18060FC40", Slot = "117")]
    	protected virtual LocalizableText FormatChoices(IReadOnlyList<BacklogChoice> choices)
    	{
    		return default(LocalizableText);
    	}

    	[Token(Token = "0x60018DE")]
    	[Address(RVA = "0x60FF50", Offset = "0x60E950", VA = "0x18060FF50", Slot = "118")]
    	protected virtual LocalizableText FormatChoice(BacklogChoice choice)
    	{
    		return default(LocalizableText);
    	}

    	[Token(Token = "0x60018DF")]
    	[Address(RVA = "0x60FFF0", Offset = "0x60E9F0", VA = "0x18060FFF0", Slot = "119")]
    	[AsyncStateMachine(typeof(_003CScrollToBottom_003Ed__51))]
    	protected virtual void ScrollToBottom()
    	{
    	}

    	[Token(Token = "0x60018E0")]
    	[Address(RVA = "0x610120", Offset = "0x60EB20", VA = "0x180610120", Slot = "92")]
    	protected override GameObject FindFocusObject()
    	{
    		return null;
    	}

    	[Token(Token = "0x60018E1")]
    	[Address(RVA = "0x610240", Offset = "0x60EC40", VA = "0x180610240")]
    	public BacklogPanel()
    	{
    	}
    }
}
