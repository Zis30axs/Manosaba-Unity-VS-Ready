using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel
{
    [Token(Token = "0x200044D")]
    public static class ScriptPlayerExtensions
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200044E")]
    	[CompilerGenerated]
    	private struct _003CLoadAndPlay_003Ed__0
    {
    		[Token(Token = "0x4001001")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001002")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001003")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public string scriptPath;

    		[Token(Token = "0x4001004")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public int playlistIndex;

    		[Token(Token = "0x4001005")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public IScriptPlayer player;

    		[Token(Token = "0x4001006")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600154C")]
    		[Address(RVA = "0x5CF3B0", Offset = "0x5CDDB0", VA = "0x1805CF3B0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600154D")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200044F")]
    	[CompilerGenerated]
    	private struct _003CLoadAndPlayAtLabel_003Ed__2
    {
    		[Token(Token = "0x4001007")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001008")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001009")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public string scriptPath;

    		[Token(Token = "0x400100A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public IScriptPlayer player;

    		[Token(Token = "0x400100B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string label;

    		[Token(Token = "0x400100C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<Resource<Script>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400100D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600154E")]
    		[Address(RVA = "0x5CF6E0", Offset = "0x5CE0E0", VA = "0x1805CF6E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600154F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000450")]
    	[CompilerGenerated]
    	private struct _003CLoadAndPlayAtLine_003Ed__1
    {
    		[Token(Token = "0x400100E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400100F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001010")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public string scriptPath;

    		[Token(Token = "0x4001011")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public IScriptPlayer player;

    		[Token(Token = "0x4001012")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public int lineIndex;

    		[Token(Token = "0x4001013")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    		public int inlineIndex;

    		[Token(Token = "0x4001014")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<Resource<Script>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4001015")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6001550")]
    		[Address(RVA = "0x5CFF00", Offset = "0x5CE900", VA = "0x1805CFF00", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001551")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000451")]
    	[CompilerGenerated]
    	private struct _003CPlayTransient_003Ed__7
    {
    		[Token(Token = "0x4001016")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001017")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001018")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ScriptPlaylist playlist;

    		[Token(Token = "0x4001019")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x400101A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400101B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private IEnumerator<Command> _003C_003E7__wrap1;

    		[Token(Token = "0x6001552")]
    		[Address(RVA = "0x5D0540", Offset = "0x5CEF40", VA = "0x1805D0540", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001553")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x6001542")]
    	[Address(RVA = "0x5CE2C0", Offset = "0x5CCCC0", VA = "0x1805CE2C0")]
    	[AsyncStateMachine(typeof(_003CLoadAndPlay_003Ed__0))]
    	public static UniTask LoadAndPlay(this IScriptPlayer player, string scriptPath, int playlistIndex = 0)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001543")]
    	[Address(RVA = "0x5CE450", Offset = "0x5CCE50", VA = "0x1805CE450")]
    	[AsyncStateMachine(typeof(_003CLoadAndPlayAtLine_003Ed__1))]
    	public static UniTask LoadAndPlayAtLine(this IScriptPlayer player, string scriptPath, int lineIndex, int inlineIndex)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001544")]
    	[Address(RVA = "0x5CE600", Offset = "0x5CD000", VA = "0x1805CE600")]
    	[AsyncStateMachine(typeof(_003CLoadAndPlayAtLabel_003Ed__2))]
    	public static UniTask LoadAndPlayAtLabel(this IScriptPlayer player, string scriptPath, string label)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001545")]
    	[Address(RVA = "0x5CE7F0", Offset = "0x5CD1F0", VA = "0x1805CE7F0")]
    	public static void PlayAtLine(this IScriptPlayer player, string scriptPath, int lineIndex, int inlineIndex)
    	{
    	}

    	[Token(Token = "0x6001546")]
    	[Address(RVA = "0x5CE9B0", Offset = "0x5CD3B0", VA = "0x1805CE9B0")]
    	public static void PlayAtLabel(this IScriptPlayer player, string scriptPath, string label)
    	{
    	}

    	[Token(Token = "0x6001547")]
    	[Address(RVA = "0x5CEBB0", Offset = "0x5CD5B0", VA = "0x1805CEBB0")]
    	public static void ResumeAtLine(this IScriptPlayer player, int lineIndex, int inlineIndex = 0)
    	{
    	}

    	[Token(Token = "0x6001548")]
    	[Address(RVA = "0x5CEE50", Offset = "0x5CD850", VA = "0x1805CEE50")]
    	public static void ResumeAtLabel(this IScriptPlayer player, string label)
    	{
    	}

    	[Token(Token = "0x6001549")]
    	[Address(RVA = "0x5CF110", Offset = "0x5CDB10", VA = "0x1805CF110")]
    	[AsyncStateMachine(typeof(_003CPlayTransient_003Ed__7))]
    	public static UniTask PlayTransient(this IScriptPlayer _, ScriptPlaylist playlist, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600154A")]
    	[Address(RVA = "0x5CF290", Offset = "0x5CDC90", VA = "0x1805CF290")]
    	public static UniTask PlayTransient(this IScriptPlayer player, string scriptName, string scriptText, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600154B")]
    	[Address(RVA = "0x5CF2F0", Offset = "0x5CDCF0", VA = "0x1805CF2F0")]
    	public static bool IsEnteringNested(this IScriptPlayer player)
    	{
    		return default(bool);
    	}
    }
}
