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
    [Serializable]
    [Token(Token = "0x2000075")]
    public class TextPrinterState : ActorState<ITextPrinterActor>
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000076")]
    	[CompilerGenerated]
    	private struct _003CApplyToActor_003Ed__10
    {
    		[Token(Token = "0x4000196")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000197")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000198")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TextPrinterState _003C_003E4__this;

    		[Token(Token = "0x4000199")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public ITextPrinterActor actor;

    		[Token(Token = "0x400019A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private PooledObject<List<UniTask>> _003C__003E5__2;

    		[Token(Token = "0x400019B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000206")]
    		[Address(RVA = "0x45AA00", Offset = "0x459400", VA = "0x18045AA00", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000207")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000193")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	[SerializeField]
    	private List<PrintedMessage> messages;

    	[Token(Token = "0x4000194")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	[SerializeField]
    	private List<MessageTemplate> templates;

    	[Token(Token = "0x4000195")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	[SerializeField]
    	private float revealProgress;

    	[Token(Token = "0x17000059")]
    	public List<PrintedMessage> Messages
    	{
    		[Token(Token = "0x60001FF")]
    		[Address(RVA = "0x453950", Offset = "0x452350", VA = "0x180453950")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700005A")]
    	public List<MessageTemplate> Templates
    	{
    		[Token(Token = "0x6000200")]
    		[Address(RVA = "0x45A0C0", Offset = "0x458AC0", VA = "0x18045A0C0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700005B")]
    	public float RevealProgress
    	{
    		[Token(Token = "0x6000201")]
    		[Address(RVA = "0x45A5A0", Offset = "0x458FA0", VA = "0x18045A5A0")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x6000202")]
    	[Address(RVA = "0x45A5B0", Offset = "0x458FB0", VA = "0x18045A5B0", Slot = "4")]
    	public override void OverwriteFromActor(ITextPrinterActor actor)
    	{
    	}

    	[Token(Token = "0x6000203")]
    	[Address(RVA = "0x45A6B0", Offset = "0x4590B0", VA = "0x18045A6B0", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CApplyToActor_003Ed__10))]
    	public override UniTask ApplyToActor(ITextPrinterActor actor)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000204")]
    	[Address(RVA = "0x45A830", Offset = "0x459230", VA = "0x18045A830")]
    	public TextPrinterState()
    	{
    	}
    }
}
