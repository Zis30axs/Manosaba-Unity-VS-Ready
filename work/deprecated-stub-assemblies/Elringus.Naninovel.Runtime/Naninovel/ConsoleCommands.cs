using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200031B")]
    public static class ConsoleCommands
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200031E")]
    	[CompilerGenerated]
    	private struct _003CRewind_003Ed__6
    {
    		[Token(Token = "0x4000BBE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000BBF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000BC0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public int line;

    		[Token(Token = "0x4000BC1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private IScriptPlayer _003Cplayer_003E5__2;

    		[Token(Token = "0x4000BC2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<bool>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000EF4")]
    		[Address(RVA = "0x55EED0", Offset = "0x55D8D0", VA = "0x18055EED0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000EF5")]
    		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x6000EE5")]
    	[Address(RVA = "0x55D940", Offset = "0x55C340", VA = "0x18055D940")]
    	[ConsoleCommand("nav")]
    	public static void ToggleScriptNavigator()
    	{
    	}

    	[Token(Token = "0x6000EE6")]
    	[Address(RVA = "0x55DB00", Offset = "0x55C500", VA = "0x18055DB00")]
    	[ConsoleCommand("debug")]
    	public static void ToggleDebugInfoGUI()
    	{
    	}

    	[Token(Token = "0x6000EE7")]
    	[Address(RVA = "0x55DB10", Offset = "0x55C510", VA = "0x18055DB10")]
    	[ConsoleCommand("var")]
    	public static void ToggleCustomVariableGUI()
    	{
    	}

    	[Token(Token = "0x6000EE8")]
    	[Address(RVA = "0x55DB60", Offset = "0x55C560", VA = "0x18055DB60")]
    	[ConsoleCommand(null)]
    	public static void Play()
    	{
    	}

    	[Token(Token = "0x6000EE9")]
    	[Address(RVA = "0x55DBF0", Offset = "0x55C5F0", VA = "0x18055DBF0")]
    	[ConsoleCommand(null)]
    	public static void PlayScript(string name)
    	{
    	}

    	[Token(Token = "0x6000EEA")]
    	[Address(RVA = "0x55DC70", Offset = "0x55C670", VA = "0x18055DC70")]
    	[ConsoleCommand(null)]
    	public static void Stop()
    	{
    	}

    	[Token(Token = "0x6000EEB")]
    	[Address(RVA = "0x55DD00", Offset = "0x55C700", VA = "0x18055DD00")]
    	[AsyncStateMachine(typeof(_003CRewind_003Ed__6))]
    	[ConsoleCommand(null)]
    	public static void Rewind(int line)
    	{
    	}

    	[Token(Token = "0x6000EEC")]
    	[Address(RVA = "0x55DDE0", Offset = "0x55C7E0", VA = "0x18055DDE0")]
    	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    	private static void SetupDevelopmentConsole()
    	{
    	}

    	[Token(Token = "0x6000EED")]
    	[Address(RVA = "0x55DEB0", Offset = "0x55C8B0", VA = "0x18055DEB0")]
    	private static string ProcessCommandInput(string input)
    	{
    		return null;
    	}
    }
}
