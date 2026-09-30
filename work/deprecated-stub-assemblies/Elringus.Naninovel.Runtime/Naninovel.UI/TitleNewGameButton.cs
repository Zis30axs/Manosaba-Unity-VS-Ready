using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x20005CE")]
    public class TitleNewGameButton : ScriptableButton
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20005D0")]
    	[CompilerGenerated]
    	private struct _003CAwake_003Ed__6
    {
    		[Token(Token = "0x400164A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400164B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400164C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public TitleNewGameButton _003C_003E4__this;

    		[Token(Token = "0x400164D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<string>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001D13")]
    		[Address(RVA = "0x4ACC10", Offset = "0x4AB610", VA = "0x1804ACC10", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001D14")]
    		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005D1")]
    	[CompilerGenerated]
    	private struct _003COnButtonClick_003Ed__8
    {
    		[Token(Token = "0x400164E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400164F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001650")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public TitleNewGameButton _003C_003E4__this;

    		[Token(Token = "0x4001651")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4001652")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private IDisposable _003C_003E7__wrap1;

    		[Token(Token = "0x4001653")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask<IDisposable>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6001D15")]
    		[Address(RVA = "0x4AD170", Offset = "0x4ABB70", VA = "0x1804AD170", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001D16")]
    		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005D2")]
    	[CompilerGenerated]
    	private struct _003CPlayTitleNewGame_003Ed__9
    {
    		[Token(Token = "0x4001654")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001655")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001656")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TitleNewGameButton _003C_003E4__this;

    		[Token(Token = "0x4001657")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private string _003CscriptPath_003E5__2;

    		[Token(Token = "0x4001658")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask<Resource<Script>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4001659")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6001D17")]
    		[Address(RVA = "0x4ADBF0", Offset = "0x4AC5F0", VA = "0x1804ADBF0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001D18")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005D3")]
    	[CompilerGenerated]
    	private struct _003CResolveStartScriptPath_003Ed__10
    {
    		[Token(Token = "0x400165A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400165B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<string> _003C_003Et__builder;

    		[Token(Token = "0x400165C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public IScriptManager scripts;

    		[Token(Token = "0x400165D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask<System.Collections.Generic.IReadOnlyCollection<string>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001D19")]
    		[Address(RVA = "0x4AE520", Offset = "0x4ACF20", VA = "0x1804AE520", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001D1A")]
    		[Address(RVA = "0x4AE9F0", Offset = "0x4AD3F0", VA = "0x1804AE9F0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001642")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
    	[Tooltip("Services to exclude from state reset when starting a new game.")]
    	[SerializeField]
    	private string[] excludeFromReset;

    	[Token(Token = "0x4001643")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
    	private string startScriptPath;

    	[Token(Token = "0x4001644")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
    	private TitleMenu titleMenu;

    	[Token(Token = "0x4001645")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
    	private IScriptPlayer player;

    	[Token(Token = "0x4001646")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
    	private IStateManager state;

    	[Token(Token = "0x4001647")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
    	private IScriptManager scripts;

    	[Token(Token = "0x6001D07")]
    	[Address(RVA = "0x4AC4B0", Offset = "0x4AAEB0", VA = "0x1804AC4B0", Slot = "4")]
    	[AsyncStateMachine(typeof(_003CAwake_003Ed__6))]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001D08")]
    	[Address(RVA = "0x4AC5E0", Offset = "0x4AAFE0", VA = "0x1804AC5E0", Slot = "6")]
    	protected override void Start()
    	{
    	}

    	[Token(Token = "0x6001D09")]
    	[Address(RVA = "0x4AC630", Offset = "0x4AB030", VA = "0x1804AC630", Slot = "62")]
    	[AsyncStateMachine(typeof(_003COnButtonClick_003Ed__8))]
    	protected override void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x6001D0A")]
    	[Address(RVA = "0x4AC760", Offset = "0x4AB160", VA = "0x1804AC760", Slot = "63")]
    	[AsyncStateMachine(typeof(_003CPlayTitleNewGame_003Ed__9))]
    	protected virtual UniTask PlayTitleNewGame()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001D0B")]
    	[Address(RVA = "0x4AC890", Offset = "0x4AB290", VA = "0x1804AC890", Slot = "64")]
    	[AsyncStateMachine(typeof(_003CResolveStartScriptPath_003Ed__10))]
    	protected virtual UniTask<string> ResolveStartScriptPath(IScriptManager scripts)
    	{
    		return default(UniTask<string>);
    	}

    	[Token(Token = "0x6001D0C")]
    	[Address(RVA = "0x4ACA20", Offset = "0x4AB420", VA = "0x1804ACA20")]
    	public TitleNewGameButton()
    	{
    	}
    }
}
