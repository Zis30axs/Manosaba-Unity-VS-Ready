using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel
{
    [Token(Token = "0x200006C")]
    [InitializeAtRuntime(0, null)]
    public class TextPrinterManager : OrthoActorManager<ITextPrinterActor, TextPrinterState, TextPrinterMetadata, TextPrintersConfiguration>
    {
    	[Serializable]
    	[Token(Token = "0x200006D")]
    	public class Settings
    	{
    		[Token(Token = "0x4000169")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public float BaseRevealSpeed;

    		[Token(Token = "0x400016A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
    		public float BaseAutoDelay;

    		[Token(Token = "0x60001EF")]
    		[Address(RVA = "0x458D10", Offset = "0x457710", VA = "0x180458D10")]
    		public Settings()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x200006E")]
    	public new class GameState
    	{
    		[Token(Token = "0x400016B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public string DefaultPrinterId;

    		[Token(Token = "0x60001F0")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public GameState()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200006F")]
    	[CompilerGenerated]
    	private struct _003CInitializeService_003Ed__22
    {
    		[Token(Token = "0x400016C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400016D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400016E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TextPrinterManager _003C_003E4__this;

    		[Token(Token = "0x400016F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60001F1")]
    		[Address(RVA = "0x458D20", Offset = "0x457720", VA = "0x180458D20", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60001F2")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000070")]
    	[CompilerGenerated]
    	private struct _003CLoadServiceState_003Ed__27
    {
    		[Token(Token = "0x4000170")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000171")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000172")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TextPrinterManager _003C_003E4__this;

    		[Token(Token = "0x4000173")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public GameStateMap stateMap;

    		[Token(Token = "0x4000174")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60001F3")]
    		[Address(RVA = "0x459040", Offset = "0x457A40", VA = "0x180459040", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60001F4")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000071")]
    	[CompilerGenerated]
    	private struct _003CPrint_003Ed__28
    {
    		[Token(Token = "0x4000175")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000176")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000177")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TextPrinterManager _003C_003E4__this;

    		[Token(Token = "0x4000178")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string printerId;

    		[Token(Token = "0x4000179")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AsyncToken token;

    		[Token(Token = "0x400017A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public PrintedMessage message;

    		[Token(Token = "0x400017B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		public bool append;

    		[Token(Token = "0x400017C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x54")]
    		public float speed;

    		[Token(Token = "0x400017D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private ITextPrinterActor _003Cprinter_003E5__2;

    		[Token(Token = "0x400017E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    		private UniTask<ITextPrinterActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400017F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60001F5")]
    		[Address(RVA = "0x4593F0", Offset = "0x457DF0", VA = "0x1804593F0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60001F6")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000168")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	private readonly IScriptPlayer scriptPlayer;

    	[Token(Token = "0x17000054")]
    	public virtual string DefaultPrinterId
    	{
    		[Token(Token = "0x60001DF")]
    		[Address(RVA = "0x453950", Offset = "0x452350", VA = "0x180453950", Slot = "61")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60001E0")]
    		[Address(RVA = "0x458180", Offset = "0x456B80", VA = "0x180458180", Slot = "62")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000055")]
    	public virtual float BaseRevealSpeed
    	{
    		[Token(Token = "0x60001E1")]
    		[Address(RVA = "0x4581E0", Offset = "0x456BE0", VA = "0x1804581E0", Slot = "63")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x60001E2")]
    		[Address(RVA = "0x4581F0", Offset = "0x456BF0", VA = "0x1804581F0", Slot = "64")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000056")]
    	public virtual float BaseAutoDelay
    	{
    		[Token(Token = "0x60001E3")]
    		[Address(RVA = "0x458200", Offset = "0x456C00", VA = "0x180458200", Slot = "65")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x60001E4")]
    		[Address(RVA = "0x458210", Offset = "0x456C10", VA = "0x180458210", Slot = "66")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x14000009")]
    	public event Action<PrintMessageArgs> OnPrintStarted
    	{
    		[Token(Token = "0x60001DB")]
    		[Address(RVA = "0x457D40", Offset = "0x456740", VA = "0x180457D40", Slot = "50")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60001DC")]
    		[Address(RVA = "0x457E50", Offset = "0x456850", VA = "0x180457E50", Slot = "51")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x1400000A")]
    	public event Action<PrintMessageArgs> OnPrintFinished
    	{
    		[Token(Token = "0x60001DD")]
    		[Address(RVA = "0x457F60", Offset = "0x456960", VA = "0x180457F60", Slot = "52")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60001DE")]
    		[Address(RVA = "0x458070", Offset = "0x456A70", VA = "0x180458070", Slot = "53")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x60001E5")]
    	[Address(RVA = "0x458220", Offset = "0x456C20", VA = "0x180458220")]
    	public TextPrinterManager(TextPrintersConfiguration config, CameraConfiguration cameraConfig, IScriptPlayer scriptPlayer)
    	{
    	}

    	[Token(Token = "0x60001E6")]
    	[Address(RVA = "0x4582E0", Offset = "0x456CE0", VA = "0x1804582E0", Slot = "28")]
    	[AsyncStateMachine(typeof(_003CInitializeService_003Ed__22))]
    	public override UniTask InitializeService()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60001E7")]
    	[Address(RVA = "0x458400", Offset = "0x456E00", VA = "0x180458400", Slot = "29")]
    	public override void ResetService()
    	{
    	}

    	[Token(Token = "0x60001E8")]
    	[Address(RVA = "0x458480", Offset = "0x456E80", VA = "0x180458480", Slot = "67")]
    	public virtual void SaveServiceState(SettingsStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x60001E9")]
    	[Address(RVA = "0x4585C0", Offset = "0x456FC0", VA = "0x1804585C0", Slot = "68")]
    	public virtual UniTask LoadServiceState(SettingsStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60001EA")]
    	[Address(RVA = "0x4586E0", Offset = "0x4570E0", VA = "0x1804586E0", Slot = "31")]
    	public override void SaveServiceState(GameStateMap stateMap)
    	{
    	}

    	[Token(Token = "0x60001EB")]
    	[Address(RVA = "0x458890", Offset = "0x457290", VA = "0x180458890", Slot = "32")]
    	[AsyncStateMachine(typeof(_003CLoadServiceState_003Ed__27))]
    	public override UniTask LoadServiceState(GameStateMap stateMap)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60001EC")]
    	[Address(RVA = "0x458A10", Offset = "0x457410", VA = "0x180458A10", Slot = "69")]
    	[AsyncStateMachine(typeof(_003CPrint_003Ed__28))]
    	public virtual UniTask Print(string printerId, PrintedMessage message, bool append = false, float speed = 1f, AsyncToken token = default)
    	{
    		return default(UniTask);
    	}
    }
}
