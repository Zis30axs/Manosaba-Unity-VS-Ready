using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel.FX
{
    [Token(Token = "0x20005E4")]
    public class Blur : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IParameterized, DestroySpawned.IAwaitable
    {
    	[Token(Token = "0x20005E5")]
    	public interface IBlurable
    	{
    		[Token(Token = "0x6001D8C")]
    		UniTask Blur(float intensity, Tween tween, [Optional] AsyncToken token);
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005E7")]
    	[CompilerGenerated]
    	private struct _003CAwaitDestroy_003Ed__23
    {
    		[Token(Token = "0x40016BB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40016BC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40016BD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Blur _003C_003E4__this;

    		[Token(Token = "0x40016BE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x40016BF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001D90")]
    		[Address(RVA = "0x4B7F30", Offset = "0x4B6930", VA = "0x1804B7F30", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001D91")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005E8")]
    	[CompilerGenerated]
    	private struct _003CAwaitSpawn_003Ed__21
    {
    		[Token(Token = "0x40016C0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40016C1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40016C2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Blur _003C_003E4__this;

    		[Token(Token = "0x40016C3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x40016C4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001D92")]
    		[Address(RVA = "0x4B82D0", Offset = "0x4B6CD0", VA = "0x1804B82D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001D93")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40016B6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private string defaultActorId;

    	[Token(Token = "0x40016B7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[SerializeField]
    	private float defaultIntensity;

    	[Token(Token = "0x40016B8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
    	[SerializeField]
    	private float defaultDuration;

    	[Token(Token = "0x17000458")]
    	protected string ActorId
    	{
    		[Token(Token = "0x6001D7D")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001D7E")]
    		[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000459")]
    	protected float Intensity
    	{
    		[Token(Token = "0x6001D7F")]
    		[Address(RVA = "0x398CF0", Offset = "0x3976F0", VA = "0x180398CF0")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001D80")]
    		[Address(RVA = "0x3A6BA0", Offset = "0x3A55A0", VA = "0x1803A6BA0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700045A")]
    	protected float Duration
    	{
    		[Token(Token = "0x6001D81")]
    		[Address(RVA = "0x398D00", Offset = "0x397700", VA = "0x180398D00")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001D82")]
    		[Address(RVA = "0x4B70F0", Offset = "0x4B5AF0", VA = "0x1804B70F0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700045B")]
    	protected float StopDuration
    	{
    		[Token(Token = "0x6001D83")]
    		[Address(RVA = "0x398D10", Offset = "0x397710", VA = "0x180398D10")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001D84")]
    		[Address(RVA = "0x4B7100", Offset = "0x4B5B00", VA = "0x1804B7100")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x6001D85")]
    	[Address(RVA = "0x4B7110", Offset = "0x4B5B10", VA = "0x1804B7110", Slot = "8")]
    	public virtual void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
    	{
    	}

    	[Token(Token = "0x6001D86")]
    	[Address(RVA = "0x4B74E0", Offset = "0x4B5EE0", VA = "0x1804B74E0", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__21))]
    	public UniTask AwaitSpawn([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001D87")]
    	[Address(RVA = "0x4B7660", Offset = "0x4B6060", VA = "0x1804B7660", Slot = "6")]
    	public void SetDestroyParameters(IReadOnlyList<string> parameters)
    	{
    	}

    	[Token(Token = "0x6001D88")]
    	[Address(RVA = "0x4B7830", Offset = "0x4B6230", VA = "0x1804B7830", Slot = "7")]
    	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__23))]
    	public UniTask AwaitDestroy([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001D89")]
    	[Address(RVA = "0x4B79B0", Offset = "0x4B63B0", VA = "0x1804B79B0")]
    	private void OnDestroy()
    	{
    	}

    	[Token(Token = "0x6001D8A")]
    	[Address(RVA = "0x4B7AB0", Offset = "0x4B64B0", VA = "0x1804B7AB0")]
    	private static IBlurable FindActor(string actorId, bool logError = true)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001D8B")]
    	[Address(RVA = "0x4B7D60", Offset = "0x4B6760", VA = "0x1804B7D60")]
    	public Blur()
    	{
    	}
    }
}
