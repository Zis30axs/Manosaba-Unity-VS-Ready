using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Naninovel
{
    [Token(Token = "0x20004B2")]
    public class BacklogMessageUI : ScriptableUIBehaviour
    {
    	[Serializable]
    	[Token(Token = "0x20004B3")]
    	private class OnMessageChangedEvent : UnityEvent<string>
    	{
    		[Token(Token = "0x6001788")]
    		[Address(RVA = "0x5F6170", Offset = "0x5F4B70", VA = "0x1805F6170")]
    		public OnMessageChangedEvent()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x20004B4")]
    	private class OnAuthorChangedEvent : UnityEvent<string>
    	{
    		[Token(Token = "0x6001789")]
    		[Address(RVA = "0x5F61B0", Offset = "0x5F4BB0", VA = "0x1805F61B0")]
    		public OnAuthorChangedEvent()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20004B6")]
    	[CompilerGenerated]
    	private struct _003CHandlePlayVoiceButtonClicked_003Ed__43
    {
    		[Token(Token = "0x40011E1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40011E2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40011E3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public BacklogMessageUI _003C_003E4__this;

    		[Token(Token = "0x40011E4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600178C")]
    		[Address(RVA = "0x5F6320", Offset = "0x5F4D20", VA = "0x1805F6320", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600178D")]
    		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20004B7")]
    	[CompilerGenerated]
    	private struct _003CHandleRollbackButtonClicked_003Ed__44
    {
    		[Token(Token = "0x40011E5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40011E6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40011E7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public BacklogMessageUI _003C_003E4__this;

    		[Token(Token = "0x40011E8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<bool>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600178E")]
    		[Address(RVA = "0x5F6760", Offset = "0x5F5160", VA = "0x1805F6760", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600178F")]
    		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40011DB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
    	[Tooltip("Panel hosting author name text (optional). When assigned will be de-activated based on whether author is assigned.")]
    	[SerializeField]
    	private GameObject authorPanel;

    	[Token(Token = "0x40011DC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
    	[Tooltip("Button to replay voice associated with the message (optional).")]
    	[SerializeField]
    	private Button playVoiceButton;

    	[Token(Token = "0x40011DD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
    	[Tooltip("Button to perform rollback to the moment the messages was added (optional).")]
    	[SerializeField]
    	private Button rollbackButton;

    	[Token(Token = "0x40011DE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
    	[SerializeField]
    	private OnMessageChangedEvent onMessageChanged;

    	[Token(Token = "0x40011DF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
    	[SerializeField]
    	private OnAuthorChangedEvent onAuthorChanged;

    	[Token(Token = "0x17000357")]
    	public virtual LocalizableText Text
    	{
    		[Token(Token = "0x600176E")]
    		[Address(RVA = "0x468E80", Offset = "0x467880", VA = "0x180468E80", Slot = "59")]
    		[CompilerGenerated]
    		get
    		{
    			return default(LocalizableText);
    		}
    		[Token(Token = "0x600176F")]
    		[Address(RVA = "0x468E90", Offset = "0x467890", VA = "0x180468E90")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000358")]
    	public virtual string AuthorId
    	{
    		[Token(Token = "0x6001770")]
    		[Address(RVA = "0x46D4E0", Offset = "0x46BEE0", VA = "0x18046D4E0", Slot = "60")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001771")]
    		[Address(RVA = "0x46D4F0", Offset = "0x46BEF0", VA = "0x18046D4F0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000359")]
    	public virtual PlaybackSpot RollbackSpot
    	{
    		[Token(Token = "0x6001772")]
    		[Address(RVA = "0x5F4820", Offset = "0x5F3220", VA = "0x1805F4820", Slot = "61")]
    		[CompilerGenerated]
    		get
    		{
    			return default(PlaybackSpot);
    		}
    		[Token(Token = "0x6001773")]
    		[Address(RVA = "0x5F4830", Offset = "0x5F3230", VA = "0x1805F4830")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700035A")]
    	public virtual List<string> VoicePaths
    	{
    		[Token(Token = "0x6001774")]
    		[Address(RVA = "0x33B470", Offset = "0x339E70", VA = "0x18033B470", Slot = "62")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700035B")]
    	public virtual GameObject AuthorPanel
    	{
    		[Token(Token = "0x6001775")]
    		[Address(RVA = "0x471780", Offset = "0x470180", VA = "0x180471780", Slot = "63")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700035C")]
    	public virtual Button PlayVoiceButton
    	{
    		[Token(Token = "0x6001776")]
    		[Address(RVA = "0x4717B0", Offset = "0x4701B0", VA = "0x1804717B0", Slot = "64")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700035D")]
    	public virtual Button RollbackButton
    	{
    		[Token(Token = "0x6001777")]
    		[Address(RVA = "0x492CE0", Offset = "0x4916E0", VA = "0x180492CE0", Slot = "65")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700035E")]
    	private IAudioManager audioManager
    	{
    		[Token(Token = "0x6001778")]
    		[Address(RVA = "0x5F4890", Offset = "0x5F3290", VA = "0x1805F4890")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700035F")]
    	private IStateManager stateManager
    	{
    		[Token(Token = "0x6001779")]
    		[Address(RVA = "0x5F48F0", Offset = "0x5F32F0", VA = "0x1805F48F0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000360")]
    	private ICharacterManager charManager
    	{
    		[Token(Token = "0x600177A")]
    		[Address(RVA = "0x5F4950", Offset = "0x5F3350", VA = "0x1805F4950")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x600177B")]
    	[Address(RVA = "0x5F49B0", Offset = "0x5F33B0", VA = "0x1805F49B0", Slot = "66")]
    	public virtual BacklogMessage GetState()
    	{
    		return default(BacklogMessage);
    	}

    	[Token(Token = "0x600177C")]
    	[Address(RVA = "0x5F4B10", Offset = "0x5F3510", VA = "0x1805F4B10", Slot = "67")]
    	public virtual void Initialize(BacklogMessage message)
    	{
    	}

    	[Token(Token = "0x600177D")]
    	[Address(RVA = "0x5F4BD0", Offset = "0x5F35D0", VA = "0x1805F4BD0", Slot = "68")]
    	public virtual void Append(LocalizableText text, [Optional] string voicePath)
    	{
    	}

    	[Token(Token = "0x600177E")]
    	[Address(RVA = "0x5F4DD0", Offset = "0x5F37D0", VA = "0x1805F4DD0", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x600177F")]
    	[Address(RVA = "0x5F5040", Offset = "0x5F3A40", VA = "0x1805F5040", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x6001780")]
    	[Address(RVA = "0x5F5320", Offset = "0x5F3D20", VA = "0x1805F5320", Slot = "69")]
    	protected virtual void SetText(LocalizableText text)
    	{
    	}

    	[Token(Token = "0x6001781")]
    	[Address(RVA = "0x5F5400", Offset = "0x5F3E00", VA = "0x1805F5400", Slot = "70")]
    	protected virtual void SetAuthor(string authorId)
    	{
    	}

    	[Token(Token = "0x6001782")]
    	[Address(RVA = "0x5F56A0", Offset = "0x5F40A0", VA = "0x1805F56A0", Slot = "71")]
    	protected virtual void SetVoice(IReadOnlyList<string> voicePaths)
    	{
    	}

    	[Token(Token = "0x6001783")]
    	[Address(RVA = "0x5F5890", Offset = "0x5F4290", VA = "0x1805F5890", Slot = "72")]
    	protected virtual void SetRollbackSpot(PlaybackSpot spot)
    	{
    	}

    	[Token(Token = "0x6001784")]
    	[Address(RVA = "0x5F5C30", Offset = "0x5F4630", VA = "0x1805F5C30", Slot = "73")]
    	[AsyncStateMachine(typeof(_003CHandlePlayVoiceButtonClicked_003Ed__43))]
    	protected virtual void HandlePlayVoiceButtonClicked()
    	{
    	}

    	[Token(Token = "0x6001785")]
    	[Address(RVA = "0x5F5D60", Offset = "0x5F4760", VA = "0x1805F5D60", Slot = "74")]
    	[AsyncStateMachine(typeof(_003CHandleRollbackButtonClicked_003Ed__44))]
    	protected virtual void HandleRollbackButtonClicked()
    	{
    	}

    	[Token(Token = "0x6001786")]
    	[Address(RVA = "0x33E4C0", Offset = "0x33CEC0", VA = "0x18033E4C0")]
    	public BacklogMessageUI()
    	{
    	}
    }
}
