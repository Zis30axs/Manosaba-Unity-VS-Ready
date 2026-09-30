using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel.FX
{
    [Token(Token = "0x20005FA")]
    public abstract class ShakeTransform : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20005FB")]
    	[CompilerGenerated]
    	private struct _003CAwaitSpawn_003Ed__83
    {
    		[Token(Token = "0x4001751")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001752")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001753")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ShakeTransform _003C_003E4__this;

    		[Token(Token = "0x4001754")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001755")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private int _003Ci_003E5__2;

    		[Token(Token = "0x4001756")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4001757")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private YieldAwaitable.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6001E1C")]
    		[Address(RVA = "0x4C2890", Offset = "0x4C1290", VA = "0x1804C2890", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001E1D")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005FC")]
    	[CompilerGenerated]
    	private struct _003CLoopRoutine_003Ed__88
    {
    		[Token(Token = "0x4001758")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001759")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400175A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public ShakeTransform _003C_003E4__this;

    		[Token(Token = "0x400175B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AsyncToken token;

    		[Token(Token = "0x400175C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001E1E")]
    		[Address(RVA = "0x4C32A0", Offset = "0x4C1CA0", VA = "0x1804C32A0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001E1F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005FD")]
    	[CompilerGenerated]
    	private struct _003CMove_003Ed__86
    {
    		[Token(Token = "0x400175D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400175E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400175F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ShakeTransform _003C_003E4__this;

    		[Token(Token = "0x4001760")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public Vector3 position;

    		[Token(Token = "0x4001761")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    		public float duration;

    		[Token(Token = "0x4001762")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x4001763")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001E20")]
    		[Address(RVA = "0x4C3600", Offset = "0x4C2000", VA = "0x1804C3600", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001E21")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005FE")]
    	[CompilerGenerated]
    	private struct _003CShakeSequence_003Ed__85
    {
    		[Token(Token = "0x4001764")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001765")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001766")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ShakeTransform _003C_003E4__this;

    		[Token(Token = "0x4001767")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001768")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Vector3 _003Camplitude_003E5__2;

    		[Token(Token = "0x4001769")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
    		private float _003Cduration_003E5__3;

    		[Token(Token = "0x400176A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001E22")]
    		[Address(RVA = "0x4C3B70", Offset = "0x4C2570", VA = "0x1804C3B70", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001E23")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400174A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
    	[SerializeField]
    	private int defaultShakesCount;

    	[Token(Token = "0x400174B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x84")]
    	[SerializeField]
    	private float defaultShakeDuration;

    	[Token(Token = "0x400174C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
    	[SerializeField]
    	private float defaultDurationVariation;

    	[Token(Token = "0x400174D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x8C")]
    	[SerializeField]
    	private float defaultShakeAmplitude;

    	[Token(Token = "0x400174E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
    	[SerializeField]
    	private float defaultAmplitudeVariation;

    	[Token(Token = "0x400174F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x94")]
    	[SerializeField]
    	private bool defaultShakeHorizontally;

    	[Token(Token = "0x4001750")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x95")]
    	[SerializeField]
    	private bool defaultShakeVertically;

    	[Token(Token = "0x1700046C")]
    	public virtual string SpawnedPath
    	{
    		[Token(Token = "0x6001DED")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "6")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001DEE")]
    		[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700046D")]
    	public virtual string ObjectName
    	{
    		[Token(Token = "0x6001DEF")]
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060", Slot = "7")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001DF0")]
    		[Address(RVA = "0x486D40", Offset = "0x485740", VA = "0x180486D40")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700046E")]
    	public virtual int ShakesCount
    	{
    		[Token(Token = "0x6001DF1")]
    		[Address(RVA = "0x36DB50", Offset = "0x36C550", VA = "0x18036DB50", Slot = "8")]
    		[CompilerGenerated]
    		get
    		{
    			return default(int);
    		}
    		[Token(Token = "0x6001DF2")]
    		[Address(RVA = "0x4C0D40", Offset = "0x4BF740", VA = "0x1804C0D40")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700046F")]
    	public virtual float ShakeDuration
    	{
    		[Token(Token = "0x6001DF3")]
    		[Address(RVA = "0x4C0D50", Offset = "0x4BF750", VA = "0x1804C0D50", Slot = "9")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001DF4")]
    		[Address(RVA = "0x4C0D60", Offset = "0x4BF760", VA = "0x1804C0D60")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000470")]
    	public virtual float DurationVariation
    	{
    		[Token(Token = "0x6001DF5")]
    		[Address(RVA = "0x3786B0", Offset = "0x3770B0", VA = "0x1803786B0", Slot = "10")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001DF6")]
    		[Address(RVA = "0x4C0D70", Offset = "0x4BF770", VA = "0x1804C0D70")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000471")]
    	public virtual float ShakeAmplitude
    	{
    		[Token(Token = "0x6001DF7")]
    		[Address(RVA = "0x4C0D80", Offset = "0x4BF780", VA = "0x1804C0D80", Slot = "11")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001DF8")]
    		[Address(RVA = "0x4C0D90", Offset = "0x4BF790", VA = "0x1804C0D90")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000472")]
    	public virtual float AmplitudeVariation
    	{
    		[Token(Token = "0x6001DF9")]
    		[Address(RVA = "0x4C0DA0", Offset = "0x4BF7A0", VA = "0x1804C0DA0", Slot = "12")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001DFA")]
    		[Address(RVA = "0x4C0DB0", Offset = "0x4BF7B0", VA = "0x1804C0DB0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000473")]
    	public virtual bool ShakeHorizontally
    	{
    		[Token(Token = "0x6001DFB")]
    		[Address(RVA = "0x4C0DC0", Offset = "0x4BF7C0", VA = "0x1804C0DC0", Slot = "13")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6001DFC")]
    		[Address(RVA = "0x4C0DD0", Offset = "0x4BF7D0", VA = "0x1804C0DD0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000474")]
    	public virtual bool ShakeVertically
    	{
    		[Token(Token = "0x6001DFD")]
    		[Address(RVA = "0x4C0DE0", Offset = "0x4BF7E0", VA = "0x1804C0DE0", Slot = "14")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6001DFE")]
    		[Address(RVA = "0x4C0DF0", Offset = "0x4BF7F0", VA = "0x1804C0DF0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000475")]
    	protected virtual int DefaultShakesCount
    	{
    		[Token(Token = "0x6001DFF")]
    		[Address(RVA = "0x4C0E00", Offset = "0x4BF800", VA = "0x1804C0E00", Slot = "15")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x17000476")]
    	protected virtual float DefaultShakeDuration
    	{
    		[Token(Token = "0x6001E00")]
    		[Address(RVA = "0x4C0E10", Offset = "0x4BF810", VA = "0x1804C0E10", Slot = "16")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x17000477")]
    	protected virtual float DefaultDurationVariation
    	{
    		[Token(Token = "0x6001E01")]
    		[Address(RVA = "0x4C0E20", Offset = "0x4BF820", VA = "0x1804C0E20", Slot = "17")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x17000478")]
    	protected virtual float DefaultShakeAmplitude
    	{
    		[Token(Token = "0x6001E02")]
    		[Address(RVA = "0x4C0E30", Offset = "0x4BF830", VA = "0x1804C0E30", Slot = "18")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x17000479")]
    	protected virtual float DefaultAmplitudeVariation
    	{
    		[Token(Token = "0x6001E03")]
    		[Address(RVA = "0x4C0E40", Offset = "0x4BF840", VA = "0x1804C0E40", Slot = "19")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x1700047A")]
    	protected virtual bool DefaultShakeHorizontally
    	{
    		[Token(Token = "0x6001E04")]
    		[Address(RVA = "0x4C0E50", Offset = "0x4BF850", VA = "0x1804C0E50", Slot = "20")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700047B")]
    	protected virtual bool DefaultShakeVertically
    	{
    		[Token(Token = "0x6001E05")]
    		[Address(RVA = "0x4C0E60", Offset = "0x4BF860", VA = "0x1804C0E60", Slot = "21")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700047C")]
    	protected virtual ISpawnManager SpawnManager
    	{
    		[Token(Token = "0x6001E06")]
    		[Address(RVA = "0x4C0E70", Offset = "0x4BF870", VA = "0x1804C0E70", Slot = "22")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700047D")]
    	protected virtual Vector3 DeltaPos
    	{
    		[Token(Token = "0x6001E07")]
    		[Address(RVA = "0x4C0ED0", Offset = "0x4BF8D0", VA = "0x1804C0ED0", Slot = "23")]
    		[CompilerGenerated]
    		get
    		{
    			return default(Vector3);
    		}
    		[Token(Token = "0x6001E08")]
    		[Address(RVA = "0x4C0EF0", Offset = "0x4BF8F0", VA = "0x1804C0EF0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700047E")]
    	protected virtual Vector3 InitialPos
    	{
    		[Token(Token = "0x6001E09")]
    		[Address(RVA = "0x4C0F00", Offset = "0x4BF900", VA = "0x1804C0F00", Slot = "24")]
    		[CompilerGenerated]
    		get
    		{
    			return default(Vector3);
    		}
    		[Token(Token = "0x6001E0A")]
    		[Address(RVA = "0x4C0F20", Offset = "0x4BF920", VA = "0x1804C0F20")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700047F")]
    	protected virtual Transform ShakenTransform
    	{
    		[Token(Token = "0x6001E0B")]
    		[Address(RVA = "0x45A0C0", Offset = "0x458AC0", VA = "0x18045A0C0", Slot = "25")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001E0C")]
    		[Address(RVA = "0x4C0F30", Offset = "0x4BF930", VA = "0x1804C0F30")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000480")]
    	protected virtual bool Loop
    	{
    		[Token(Token = "0x6001E0D")]
    		[Address(RVA = "0x4C0F90", Offset = "0x4BF990", VA = "0x1804C0F90", Slot = "26")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6001E0E")]
    		[Address(RVA = "0x4C0FA0", Offset = "0x4BF9A0", VA = "0x1804C0FA0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000481")]
    	protected virtual Tweener<VectorTween> PositionTweener
    	{
    		[Token(Token = "0x6001E0F")]
    		[Address(RVA = "0x478200", Offset = "0x476C00", VA = "0x180478200", Slot = "27")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000482")]
    	protected virtual CancellationTokenSource CTS
    	{
    		[Token(Token = "0x6001E10")]
    		[Address(RVA = "0x478210", Offset = "0x476C10", VA = "0x180478210", Slot = "28")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001E11")]
    		[Address(RVA = "0x4C0FB0", Offset = "0x4BF9B0", VA = "0x1804C0FB0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x6001E12")]
    	[Address(RVA = "0x4C1010", Offset = "0x4BFA10", VA = "0x1804C1010", Slot = "29")]
    	public virtual void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
    	{
    	}

    	[Token(Token = "0x6001E13")]
    	[Address(RVA = "0x4C1CA0", Offset = "0x4C06A0", VA = "0x1804C1CA0", Slot = "30")]
    	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__83))]
    	public virtual UniTask AwaitSpawn([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001E14")]
    	protected abstract Transform GetShakenTransform();

    	[Token(Token = "0x6001E15")]
    	[Address(RVA = "0x4BFE70", Offset = "0x4BE870", VA = "0x1804BFE70", Slot = "32")]
    	[AsyncStateMachine(typeof(_003CShakeSequence_003Ed__85))]
    	protected virtual UniTask ShakeSequence(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001E16")]
    	[Address(RVA = "0x4C1E40", Offset = "0x4C0840", VA = "0x1804C1E40", Slot = "33")]
    	[AsyncStateMachine(typeof(_003CMove_003Ed__86))]
    	protected virtual UniTask Move(Vector3 position, float duration, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001E17")]
    	[Address(RVA = "0x4C2000", Offset = "0x4C0A00", VA = "0x1804C2000", Slot = "34")]
    	protected virtual void OnDestroy()
    	{
    	}

    	[Token(Token = "0x6001E18")]
    	[Address(RVA = "0x4C2350", Offset = "0x4C0D50", VA = "0x1804C2350", Slot = "35")]
    	[AsyncStateMachine(typeof(_003CLoopRoutine_003Ed__88))]
    	protected virtual UniTaskVoid LoopRoutine(AsyncToken token)
    	{
    		return default(UniTaskVoid);
    	}

    	[Token(Token = "0x6001E19")]
    	[Address(RVA = "0x4C24D0", Offset = "0x4C0ED0", VA = "0x1804C24D0", Slot = "36")]
    	protected virtual AsyncToken InitializeCTS(AsyncToken token)
    	{
    		return default(AsyncToken);
    	}

    	[Token(Token = "0x6001E1A")]
    	[Address(RVA = "0x4C26B0", Offset = "0x4C10B0", VA = "0x1804C26B0")]
    	protected ShakeTransform()
    	{
    	}
    }
}
