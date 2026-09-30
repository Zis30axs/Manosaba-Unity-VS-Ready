using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    [Token(Token = "0x200054C")]
    [RequireComponent(typeof(CanvasGroup))]
    public class GameSettingsMenu : CustomUI, ISettingsUI, IManagedUI
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200054D")]
    	[CompilerGenerated]
    	private struct _003CInitialize_003Ed__9
    {
    		[Token(Token = "0x4001463")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001464")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001465")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GameSettingsMenu _003C_003E4__this;

    		[Token(Token = "0x4001466")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask<Resource>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001A54")]
    		[Address(RVA = "0x629590", Offset = "0x627F90", VA = "0x180629590", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001A55")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200054E")]
    	[CompilerGenerated]
    	private struct _003CSaveSettingsAndHide_003Ed__10
    {
    		[Token(Token = "0x4001467")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001468")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001469")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GameSettingsMenu _003C_003E4__this;

    		[Token(Token = "0x400146A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private InteractionBlocker _003C_003E7__wrap1;

    		[Token(Token = "0x400146B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001A56")]
    		[Address(RVA = "0x6299F0", Offset = "0x6283F0", VA = "0x1806299F0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001A57")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001460")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
    	[Tooltip("Toggles representing menu tabs. Expected in left to right order.")]
    	[SerializeField]
    	private Toggle[] tabs;

    	[Token(Token = "0x4001461")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
    	private readonly List<TMP_Dropdown> dropdowns;

    	[Token(Token = "0x4001462")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
    	private int tabIndex;

    	[Token(Token = "0x170003B7")]
    	protected virtual Toggle[] Tabs
    	{
    		[Token(Token = "0x6001A4A")]
    		[Address(RVA = "0x33E0B0", Offset = "0x33CAB0", VA = "0x18033E0B0", Slot = "95")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003B8")]
    	private IStateManager state
    	{
    		[Token(Token = "0x6001A4B")]
    		[Address(RVA = "0x628CA0", Offset = "0x6276A0", VA = "0x180628CA0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003B9")]
    	private ITextManager docs
    	{
    		[Token(Token = "0x6001A4C")]
    		[Address(RVA = "0x628D00", Offset = "0x627700", VA = "0x180628D00")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6001A4D")]
    	[Address(RVA = "0x628D60", Offset = "0x627760", VA = "0x180628D60", Slot = "83")]
    	[AsyncStateMachine(typeof(_003CInitialize_003Ed__9))]
    	public override UniTask Initialize()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001A4E")]
    	[Address(RVA = "0x628E80", Offset = "0x627880", VA = "0x180628E80", Slot = "96")]
    	[AsyncStateMachine(typeof(_003CSaveSettingsAndHide_003Ed__10))]
    	public virtual UniTask SaveSettingsAndHide()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001A4F")]
    	[Address(RVA = "0x628FA0", Offset = "0x6279A0", VA = "0x180628FA0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001A50")]
    	[Address(RVA = "0x629070", Offset = "0x627A70", VA = "0x180629070", Slot = "8")]
    	protected override void OnDestroy()
    	{
    	}

    	[Token(Token = "0x6001A51")]
    	[Address(RVA = "0x629150", Offset = "0x627B50", VA = "0x180629150", Slot = "97")]
    	protected virtual void HandleCancelInput()
    	{
    	}

    	[Token(Token = "0x6001A52")]
    	[Address(RVA = "0x629350", Offset = "0x627D50", VA = "0x180629350", Slot = "98")]
    	protected virtual void HandleTabInput(float value)
    	{
    	}

    	[Token(Token = "0x6001A53")]
    	[Address(RVA = "0x6294C0", Offset = "0x627EC0", VA = "0x1806294C0")]
    	public GameSettingsMenu()
    	{
    	}
    }
}
