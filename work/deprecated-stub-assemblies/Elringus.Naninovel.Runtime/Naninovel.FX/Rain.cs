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
    [Token(Token = "0x20005F1")]
    [RequireComponent(typeof(ParticleSystem))]
    public class Rain : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IParameterized, DestroySpawned.IAwaitable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20005F2")]
    	[CompilerGenerated]
    	private struct _003CAwaitDestroy_003Ed__27
    {
    		[Token(Token = "0x4001728")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001729")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400172A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Rain _003C_003E4__this;

    		[Token(Token = "0x400172B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x400172C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001DDA")]
    		[Address(RVA = "0x4BEB00", Offset = "0x4BD500", VA = "0x1804BEB00", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001DDB")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005F3")]
    	[CompilerGenerated]
    	private struct _003CAwaitSpawn_003Ed__25
    {
    		[Token(Token = "0x400172D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400172E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400172F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Rain _003C_003E4__this;

    		[Token(Token = "0x4001730")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001731")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001DDC")]
    		[Address(RVA = "0x4BEFE0", Offset = "0x4BD9E0", VA = "0x1804BEFE0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001DDD")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400171C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    	[SerializeField]
    	private float defaultIntensity;

    	[Token(Token = "0x400171D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private float defaultFadeInTime;

    	[Token(Token = "0x400171E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
    	[SerializeField]
    	private float defaultFadeOutTime;

    	[Token(Token = "0x400171F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private float defaultVelocityX;

    	[Token(Token = "0x4001720")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
    	[SerializeField]
    	private float defaultVelocityY;

    	[Token(Token = "0x4001721")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private static readonly int tintColorId;

    	[Token(Token = "0x4001722")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	private readonly Tweener<FloatTween> intensityTweener;

    	[Token(Token = "0x4001723")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private ParticleSystem particles;

    	[Token(Token = "0x4001724")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private ParticleSystem.EmissionModule emissionModule;

    	[Token(Token = "0x4001725")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private ParticleSystem.VelocityOverLifetimeModule velocityModule;

    	[Token(Token = "0x4001726")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	private Material particlesMaterial;

    	[Token(Token = "0x4001727")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	private Color tintColor;

    	[Token(Token = "0x17000469")]
    	protected float Intensity
    	{
    		[Token(Token = "0x6001DCB")]
    		[Address(RVA = "0x4B8670", Offset = "0x4B7070", VA = "0x1804B8670")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001DCC")]
    		[Address(RVA = "0x4B8680", Offset = "0x4B7080", VA = "0x1804B8680")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700046A")]
    	protected float FadeInTime
    	{
    		[Token(Token = "0x6001DCD")]
    		[Address(RVA = "0x4B8690", Offset = "0x4B7090", VA = "0x1804B8690")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001DCE")]
    		[Address(RVA = "0x4B86A0", Offset = "0x4B70A0", VA = "0x1804B86A0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700046B")]
    	protected float FadeOutTime
    	{
    		[Token(Token = "0x6001DCF")]
    		[Address(RVA = "0x398CF0", Offset = "0x3976F0", VA = "0x180398CF0")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001DD0")]
    		[Address(RVA = "0x3A6BA0", Offset = "0x3A55A0", VA = "0x1803A6BA0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x6001DD1")]
    	[Address(RVA = "0x4BD760", Offset = "0x4BC160", VA = "0x1804BD760", Slot = "8")]
    	public virtual void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
    	{
    	}

    	[Token(Token = "0x6001DD2")]
    	[Address(RVA = "0x4BDE60", Offset = "0x4BC860", VA = "0x1804BDE60", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__25))]
    	public UniTask AwaitSpawn([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001DD3")]
    	[Address(RVA = "0x4BDFE0", Offset = "0x4BC9E0", VA = "0x1804BDFE0", Slot = "6")]
    	public void SetDestroyParameters(IReadOnlyList<string> parameters)
    	{
    	}

    	[Token(Token = "0x6001DD4")]
    	[Address(RVA = "0x4BE1B0", Offset = "0x4BCBB0", VA = "0x1804BE1B0", Slot = "7")]
    	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__27))]
    	public UniTask AwaitDestroy([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001DD5")]
    	[Address(RVA = "0x4BE330", Offset = "0x4BCD30", VA = "0x1804BE330")]
    	private void Awake()
    	{
    	}

    	[Token(Token = "0x6001DD6")]
    	[Address(RVA = "0x4BE7A0", Offset = "0x4BD1A0", VA = "0x1804BE7A0")]
    	private void SetIntensity(float value)
    	{
    	}

    	[Token(Token = "0x6001DD7")]
    	[Address(RVA = "0x4BE800", Offset = "0x4BD200", VA = "0x1804BE800")]
    	private void SetTintOpacity(float value)
    	{
    	}

    	[Token(Token = "0x6001DD8")]
    	[Address(RVA = "0x4BE9A0", Offset = "0x4BD3A0", VA = "0x1804BE9A0")]
    	public Rain()
    	{
    	}
    }
}
