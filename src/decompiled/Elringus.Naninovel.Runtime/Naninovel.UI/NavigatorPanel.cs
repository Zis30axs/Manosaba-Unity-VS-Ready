using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x2000583")]
public abstract class NavigatorPanel : CustomUI
{
	[StructLayout(3)]
	[Token(Token = "0x2000584")]
	[CompilerGenerated]
	private struct _003CAwake_003Ed__14
{
		[Token(Token = "0x400151E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400151F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001520")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public NavigatorPanel _003C_003E4__this;

		[Token(Token = "0x4001521")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<IReadOnlyCollection<string>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001B51")]
		[Address(RVA = "0x63A270", Offset = "0x638C70", VA = "0x18063A270", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001B52")]
		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400151A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	[SerializeField]
	private Transform buttonsContainer;

	[Token(Token = "0x400151B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	[SerializeField]
	private GameObject playButtonPrototype;

	[Token(Token = "0x170003DD")]
	protected virtual Transform ButtonsContainer
	{
		[Token(Token = "0x6001B43")]
		[Address(RVA = "0x33E0B0", Offset = "0x33CAB0", VA = "0x18033E0B0", Slot = "95")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003DE")]
	protected virtual GameObject PlayButtonPrototype
	{
		[Token(Token = "0x6001B44")]
		[Address(RVA = "0x49D2A0", Offset = "0x49BCA0", VA = "0x18049D2A0", Slot = "96")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003DF")]
	protected virtual IScriptPlayer Player
	{
		[Token(Token = "0x6001B45")]
		[Address(RVA = "0x49D2B0", Offset = "0x49BCB0", VA = "0x18049D2B0", Slot = "97")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6001B46")]
		[Address(RVA = "0x639A40", Offset = "0x638440", VA = "0x180639A40")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x170003E0")]
	protected virtual IScriptManager ScriptManager
	{
		[Token(Token = "0x6001B47")]
		[Address(RVA = "0x49D250", Offset = "0x49BC50", VA = "0x18049D250", Slot = "98")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6001B48")]
		[Address(RVA = "0x639AA0", Offset = "0x6384A0", VA = "0x180639AA0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x6001B49")]
	[Address(RVA = "0x639B00", Offset = "0x638500", VA = "0x180639B00", Slot = "4")]
	[AsyncStateMachine(typeof(_003CAwake_003Ed__14))]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001B4A")]
	[Address(RVA = "0x639C30", Offset = "0x638630", VA = "0x180639C30", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x6001B4B")]
	[Address(RVA = "0x639D00", Offset = "0x638700", VA = "0x180639D00", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x6001B4C")]
	protected abstract UniTask<IReadOnlyCollection<string>> LocateAllScriptPaths();

	[Token(Token = "0x6001B4D")]
	[Address(RVA = "0x639DF0", Offset = "0x6387F0", VA = "0x180639DF0", Slot = "100")]
	protected virtual void GenerateScriptButtons(IEnumerable<string> scriptPaths)
	{
	}

	[Token(Token = "0x6001B4E")]
	[Address(RVA = "0x63A1A0", Offset = "0x638BA0", VA = "0x18063A1A0")]
	private void HandlePlay(Script script)
	{
	}

	[Token(Token = "0x6001B4F")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	protected NavigatorPanel()
	{
	}
}
