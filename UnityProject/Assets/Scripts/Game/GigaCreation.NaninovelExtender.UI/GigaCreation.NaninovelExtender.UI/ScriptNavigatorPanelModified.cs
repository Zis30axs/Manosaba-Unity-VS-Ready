using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.UniTasks;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Token(Token = "0x2000013")]
    public class ScriptNavigatorPanelModified : ScriptNavigatorPanel
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000014")]
    	[CompilerGenerated]
    	private struct _003CGenerateScriptButtonsAsync_003Ed__6
    {
    		[Token(Token = "0x400003E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400003F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000040")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ScriptNavigatorPanelModified _003C_003E4__this;

    		[Token(Token = "0x4000041")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public IEnumerable<string> scriptNames;

    		[Token(Token = "0x4000042")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private IEnumerator<string> _003C_003E7__wrap1;

    		[Token(Token = "0x4000043")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private string _003CscriptName_003E5__3;

    		[Token(Token = "0x4000044")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private Cysharp.Threading.Tasks.UniTask<GameObject>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000049")]
    		[Address(RVA = "0xD38BA0", Offset = "0xD375A0", VA = "0x180D38BA0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600004A")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000015")]
    	[CompilerGenerated]
    	private struct _003CInitialize_003Ed__4
    {
    		[Token(Token = "0x4000045")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000046")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public Naninovel.Async.CompilerServices.AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000047")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ScriptNavigatorPanelModified _003C_003E4__this;

    		[Token(Token = "0x4000048")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000049")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private Cysharp.Threading.Tasks.UniTask<GameObject>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600004B")]
    		[Address(RVA = "0xD39380", Offset = "0xD37D80", VA = "0x180D39380", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600004C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400003A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
    	[Header("Assets - ScriptNavigatorPanel Modified")]
    	[SerializeField]
    	private AssetReferenceGameObject _headerPrefab;

    	[Token(Token = "0x400003B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
    	[SerializeField]
    	private AssetReferenceGameObject _playButtonPrefab;

    	[Token(Token = "0x400003C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
    	[Header("References - ScriptNavigatorPanel Modified")]
    	[SerializeField]
    	private Transform _headerParent;

    	[Token(Token = "0x400003D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x118")]
    	private readonly SimpleWaitable _initialized;

    	[Token(Token = "0x6000043")]
    	[Address(RVA = "0xD385E0", Offset = "0xD36FE0", VA = "0x180D385E0", Slot = "83")]
    	[AsyncStateMachine(typeof(_003CInitialize_003Ed__4))]
    	public override Naninovel.UniTask Initialize()
    	{
    		return default(Naninovel.UniTask);
    	}

    	[Token(Token = "0x6000044")]
    	[Address(RVA = "0xD38710", Offset = "0xD37110", VA = "0x180D38710", Slot = "100")]
    	protected override void GenerateScriptButtons(IEnumerable<string> scriptNames)
    	{
    	}

    	[Token(Token = "0x6000045")]
    	[Address(RVA = "0xD388C0", Offset = "0xD372C0", VA = "0x180D388C0")]
    	[AsyncStateMachine(typeof(_003CGenerateScriptButtonsAsync_003Ed__6))]
    	private Cysharp.Threading.Tasks.UniTask GenerateScriptButtonsAsync(IEnumerable<string> scriptNames)
    	{
    		return default(Cysharp.Threading.Tasks.UniTask);
    	}

    	[Token(Token = "0x6000046")]
    	[Address(RVA = "0xD38A70", Offset = "0xD37470", VA = "0x180D38A70")]
    	public ScriptNavigatorPanelModified()
    	{
    	}
    }
}
