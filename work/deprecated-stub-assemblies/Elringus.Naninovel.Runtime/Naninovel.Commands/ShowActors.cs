using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x2000685")]
    [CommandAlias("show")]
    public class ShowActors : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000689")]
    	[CompilerGenerated]
    	private struct _003CShow_003Ed__5
    {
    		[Token(Token = "0x40018ED")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40018EE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40018EF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ShowActors _003C_003E4__this;

    		[Token(Token = "0x40018F0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x40018F1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private PooledObject<List<IActorManager>> _003C__003E5__2;

    		[Token(Token = "0x40018F2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private PooledObject<List<UniTask>> _003C___003E5__3;

    		[Token(Token = "0x40018F3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001FF7")]
    		[Address(RVA = "0x4DBF90", Offset = "0x4DA990", VA = "0x1804DBF90", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001FF8")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40018E5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	[ActorContext("*", -1, null)]
    	public StringListParameter ActorIds;

    	[Token(Token = "0x40018E6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterAlias("time")]
    	[ParameterDefaultValue("0.35")]
    	public DecimalParameter Duration;

    	[Token(Token = "0x40018E7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[ParameterDefaultValue("false")]
    	public BooleanParameter Lazy;

    	[Token(Token = "0x40018E8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	public BooleanParameter Wait;

    	[Token(Token = "0x6001FEB")]
    	[Address(RVA = "0x4DB8A0", Offset = "0x4DA2A0", VA = "0x1804DB8A0", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001FEC")]
    	[Address(RVA = "0x4DB950", Offset = "0x4DA350", VA = "0x1804DB950", Slot = "16")]
    	[AsyncStateMachine(typeof(_003CShow_003Ed__5))]
    	protected virtual UniTask Show(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001FED")]
    	[Address(RVA = "0x4DBAF0", Offset = "0x4DA4F0", VA = "0x1804DBAF0", Slot = "17")]
    	protected virtual float GetDuration(IActorManager manager)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x6001FEE")]
    	[Address(RVA = "0x4DBBE0", Offset = "0x4DA5E0", VA = "0x1804DBBE0", Slot = "18")]
    	protected virtual EasingType GetEasing(IActorManager manager)
    	{
    		return default(EasingType);
    	}

    	[Token(Token = "0x6001FEF")]
    	[Address(RVA = "0x4D6090", Offset = "0x4D4A90", VA = "0x1804D6090")]
    	public ShowActors()
    	{
    	}
    }
}
