using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel.FX;

[Token(Token = "0x2000602")]
[RequireComponent(typeof(ParticleSystem))]
public class SunShafts : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IParameterized, DestroySpawned.IAwaitable
{
	[StructLayout(3)]
	[Token(Token = "0x2000603")]
	[CompilerGenerated]
	private struct _003CAwaitDestroy_003Ed__23
{
		[Token(Token = "0x400178C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400178D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400178E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SunShafts _003C_003E4__this;

		[Token(Token = "0x400178F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001790")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001E45")]
		[Address(RVA = "0x4C6C90", Offset = "0x4C5690", VA = "0x1804C6C90", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001E46")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000604")]
	[CompilerGenerated]
	private struct _003CAwaitSpawn_003Ed__21
{
		[Token(Token = "0x4001791")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001792")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001793")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SunShafts _003C_003E4__this;

		[Token(Token = "0x4001794")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001795")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001E47")]
		[Address(RVA = "0x4C7170", Offset = "0x4C5B70", VA = "0x1804C7170", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001E48")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001784")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
	[SerializeField]
	private float defaultIntensity;

	[Token(Token = "0x4001785")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private float defaultFadeInTime;

	[Token(Token = "0x4001786")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
	[SerializeField]
	private float defaultFadeOutTime;

	[Token(Token = "0x4001787")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly int tintColorId;

	[Token(Token = "0x4001788")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private readonly Tweener<FloatTween> intensityTweener;

	[Token(Token = "0x4001789")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private ParticleSystem particles;

	[Token(Token = "0x400178A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private Material particlesMaterial;

	[Token(Token = "0x400178B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private Color tintColor;

	[Token(Token = "0x17000486")]
	protected float Intensity
	{
		[Token(Token = "0x6001E37")]
		[Address(RVA = "0x4B8670", Offset = "0x4B7070", VA = "0x1804B8670")]
		[CompilerGenerated]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6001E38")]
		[Address(RVA = "0x4B8680", Offset = "0x4B7080", VA = "0x1804B8680")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000487")]
	protected float FadeInTime
	{
		[Token(Token = "0x6001E39")]
		[Address(RVA = "0x4B8690", Offset = "0x4B7090", VA = "0x1804B8690")]
		[CompilerGenerated]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6001E3A")]
		[Address(RVA = "0x4B86A0", Offset = "0x4B70A0", VA = "0x1804B86A0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000488")]
	protected float FadeOutTime
	{
		[Token(Token = "0x6001E3B")]
		[Address(RVA = "0x398CF0", Offset = "0x3976F0", VA = "0x180398CF0")]
		[CompilerGenerated]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6001E3C")]
		[Address(RVA = "0x3A6BA0", Offset = "0x3A55A0", VA = "0x1803A6BA0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x6001E3D")]
	[Address(RVA = "0x4C5EF0", Offset = "0x4C48F0", VA = "0x1804C5EF0", Slot = "8")]
	public virtual void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
	{
	}

	[Token(Token = "0x6001E3E")]
	[Address(RVA = "0x4C6230", Offset = "0x4C4C30", VA = "0x1804C6230", Slot = "5")]
	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__21))]
	public UniTask AwaitSpawn([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001E3F")]
	[Address(RVA = "0x4C63B0", Offset = "0x4C4DB0", VA = "0x1804C63B0", Slot = "6")]
	public void SetDestroyParameters(IReadOnlyList<string> parameters)
	{
	}

	[Token(Token = "0x6001E40")]
	[Address(RVA = "0x4C6580", Offset = "0x4C4F80", VA = "0x1804C6580", Slot = "7")]
	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__23))]
	public UniTask AwaitDestroy([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001E41")]
	[Address(RVA = "0x4C6700", Offset = "0x4C5100", VA = "0x1804C6700")]
	private void Awake()
	{
	}

	[Token(Token = "0x6001E42")]
	[Address(RVA = "0x4C69C0", Offset = "0x4C53C0", VA = "0x1804C69C0")]
	private void SetIntensity(float value)
	{
	}

	[Token(Token = "0x6001E43")]
	[Address(RVA = "0x4C6B40", Offset = "0x4C5540", VA = "0x1804C6B40")]
	public SunShafts()
	{
	}
}
