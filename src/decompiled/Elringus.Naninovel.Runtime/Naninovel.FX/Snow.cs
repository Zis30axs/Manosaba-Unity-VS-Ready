using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel.FX;

[Token(Token = "0x20005FF")]
[RequireComponent(typeof(ParticleSystem))]
public class Snow : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IParameterized, DestroySpawned.IAwaitable
{
	[StructLayout(3)]
	[Token(Token = "0x2000600")]
	[CompilerGenerated]
	private struct _003CAwaitDestroy_003Ed__24
{
		[Token(Token = "0x4001777")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001778")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001779")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public Snow _003C_003E4__this;

		[Token(Token = "0x400177A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x400177B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001E33")]
		[Address(RVA = "0x4C54E0", Offset = "0x4C3EE0", VA = "0x1804C54E0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001E34")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000601")]
	[CompilerGenerated]
	private struct _003CAwaitSpawn_003Ed__22
{
		[Token(Token = "0x400177C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400177D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400177E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public Snow _003C_003E4__this;

		[Token(Token = "0x400177F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001780")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001E35")]
		[Address(RVA = "0x4C59C0", Offset = "0x4C43C0", VA = "0x1804C59C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001E36")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400176E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
	[SerializeField]
	private float defaultIntensity;

	[Token(Token = "0x400176F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private float defaultFadeInTime;

	[Token(Token = "0x4001770")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
	[SerializeField]
	private float defaultFadeOutTime;

	[Token(Token = "0x4001771")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly int tintColorId;

	[Token(Token = "0x4001772")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private readonly Tweener<FloatTween> intensityTweener;

	[Token(Token = "0x4001773")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private ParticleSystem particles;

	[Token(Token = "0x4001774")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private ParticleSystem.EmissionModule emissionModule;

	[Token(Token = "0x4001775")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private Material particlesMaterial;

	[Token(Token = "0x4001776")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private Color tintColor;

	[Token(Token = "0x17000483")]
	protected float Intensity
	{
		[Token(Token = "0x6001E24")]
		[Address(RVA = "0x4B8670", Offset = "0x4B7070", VA = "0x1804B8670")]
		[CompilerGenerated]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6001E25")]
		[Address(RVA = "0x4B8680", Offset = "0x4B7080", VA = "0x1804B8680")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000484")]
	protected float FadeInTime
	{
		[Token(Token = "0x6001E26")]
		[Address(RVA = "0x4B8690", Offset = "0x4B7090", VA = "0x1804B8690")]
		[CompilerGenerated]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6001E27")]
		[Address(RVA = "0x4B86A0", Offset = "0x4B70A0", VA = "0x1804B86A0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000485")]
	protected float FadeOutTime
	{
		[Token(Token = "0x6001E28")]
		[Address(RVA = "0x398CF0", Offset = "0x3976F0", VA = "0x180398CF0")]
		[CompilerGenerated]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6001E29")]
		[Address(RVA = "0x3A6BA0", Offset = "0x3A55A0", VA = "0x1803A6BA0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x6001E2A")]
	[Address(RVA = "0x4C45C0", Offset = "0x4C2FC0", VA = "0x1804C45C0", Slot = "8")]
	public virtual void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
	{
	}

	[Token(Token = "0x6001E2B")]
	[Address(RVA = "0x4C4900", Offset = "0x4C3300", VA = "0x1804C4900", Slot = "5")]
	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__22))]
	public UniTask AwaitSpawn([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001E2C")]
	[Address(RVA = "0x4C4A80", Offset = "0x4C3480", VA = "0x1804C4A80", Slot = "6")]
	public void SetDestroyParameters(IReadOnlyList<string> parameters)
	{
	}

	[Token(Token = "0x6001E2D")]
	[Address(RVA = "0x4C4C50", Offset = "0x4C3650", VA = "0x1804C4C50", Slot = "7")]
	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__24))]
	public UniTask AwaitDestroy([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001E2E")]
	[Address(RVA = "0x4C4DD0", Offset = "0x4C37D0", VA = "0x1804C4DD0")]
	private void Awake()
	{
	}

	[Token(Token = "0x6001E2F")]
	[Address(RVA = "0x4C5190", Offset = "0x4C3B90", VA = "0x1804C5190")]
	private void SetRateOverTime(float value)
	{
	}

	[Token(Token = "0x6001E30")]
	[Address(RVA = "0x4C51F0", Offset = "0x4C3BF0", VA = "0x1804C51F0")]
	private void SetTintOpacity(float value)
	{
	}

	[Token(Token = "0x6001E31")]
	[Address(RVA = "0x4C5390", Offset = "0x4C3D90", VA = "0x1804C5390")]
	public Snow()
	{
	}
}
