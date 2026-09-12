using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel.FX;

[Token(Token = "0x20005DE")]
public class Animate : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable
{
	[StructLayout(3)]
	[Token(Token = "0x20005E1")]
	[CompilerGenerated]
	private struct _003CAnimateKey_003Ed__66
{
		[Token(Token = "0x400169D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400169E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400169F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public Animate _003C_003E4__this;

		[Token(Token = "0x40016A0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public int keyIndex;

		[Token(Token = "0x40016A1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public IActor actor;

		[Token(Token = "0x40016A2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken token;

		[Token(Token = "0x40016A3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001D77")]
		[Address(RVA = "0x4B4910", Offset = "0x4B3310", VA = "0x1804B4910", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001D78")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20005E2")]
	[CompilerGenerated]
	private struct _003CAwaitSpawn_003Ed__65
{
		[Token(Token = "0x40016A4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40016A5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40016A6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public Animate _003C_003E4__this;

		[Token(Token = "0x40016A7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40016A8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private IActor _003Cactor_003E5__2;

		[Token(Token = "0x40016A9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private int _003CkeyIdx_003E5__3;

		[Token(Token = "0x40016AA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001D79")]
		[Address(RVA = "0x4B64B0", Offset = "0x4B4EB0", VA = "0x1804B64B0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001D7A")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20005E3")]
	[CompilerGenerated]
	private struct _003CLoopRoutine_003Ed__70
{
		[Token(Token = "0x40016AB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40016AC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40016AD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public Animate _003C_003E4__this;

		[Token(Token = "0x40016AE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public IActor actor;

		[Token(Token = "0x40016AF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40016B0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private int _003CkeyIdx_003E5__2;

		[Token(Token = "0x40016B1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001D7B")]
		[Address(RVA = "0x4B6D30", Offset = "0x4B5730", VA = "0x1804B6D30", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001D7C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x17000444")]
	protected virtual string ActorId
	{
		[Token(Token = "0x6001D45")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "6")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6001D46")]
		[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000445")]
	protected virtual bool Loop
	{
		[Token(Token = "0x6001D47")]
		[Address(RVA = "0x456330", Offset = "0x454D30", VA = "0x180456330", Slot = "7")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6001D48")]
		[Address(RVA = "0x4B1840", Offset = "0x4B0240", VA = "0x1804B1840")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000446")]
	protected virtual List<string> Appearance
	{
		[Token(Token = "0x6001D49")]
		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0", Slot = "8")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000447")]
	protected virtual List<string> Transition
	{
		[Token(Token = "0x6001D4A")]
		[Address(RVA = "0x347410", Offset = "0x345E10", VA = "0x180347410", Slot = "9")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000448")]
	protected virtual List<bool?> Visibility
	{
		[Token(Token = "0x6001D4B")]
		[Address(RVA = "0x44CE10", Offset = "0x44B810", VA = "0x18044CE10", Slot = "10")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000449")]
	protected virtual List<float?> PositionX
	{
		[Token(Token = "0x6001D4C")]
		[Address(RVA = "0x453470", Offset = "0x451E70", VA = "0x180453470", Slot = "11")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700044A")]
	protected virtual List<float?> PositionY
	{
		[Token(Token = "0x6001D4D")]
		[Address(RVA = "0x453480", Offset = "0x451E80", VA = "0x180453480", Slot = "12")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700044B")]
	protected virtual List<float?> PositionZ
	{
		[Token(Token = "0x6001D4E")]
		[Address(RVA = "0x453950", Offset = "0x452350", VA = "0x180453950", Slot = "13")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700044C")]
	protected virtual List<float?> RotationZ
	{
		[Token(Token = "0x6001D4F")]
		[Address(RVA = "0x45A0C0", Offset = "0x458AC0", VA = "0x18045A0C0", Slot = "14")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700044D")]
	protected virtual List<Vector3?> Scale
	{
		[Token(Token = "0x6001D50")]
		[Address(RVA = "0x398D30", Offset = "0x397730", VA = "0x180398D30", Slot = "15")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700044E")]
	protected virtual List<string> TintColor
	{
		[Token(Token = "0x6001D51")]
		[Address(RVA = "0x478200", Offset = "0x476C00", VA = "0x180478200", Slot = "16")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700044F")]
	protected virtual List<string> EasingTypeName
	{
		[Token(Token = "0x6001D52")]
		[Address(RVA = "0x478210", Offset = "0x476C10", VA = "0x180478210", Slot = "17")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000450")]
	protected virtual List<float?> Duration
	{
		[Token(Token = "0x6001D53")]
		[Address(RVA = "0x468E80", Offset = "0x467880", VA = "0x180468E80", Slot = "18")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000451")]
	protected virtual int KeyCount
	{
		[Token(Token = "0x6001D54")]
		[Address(RVA = "0x4B1850", Offset = "0x4B0250", VA = "0x1804B1850", Slot = "19")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
		[Token(Token = "0x6001D55")]
		[Address(RVA = "0x4B1860", Offset = "0x4B0260", VA = "0x1804B1860")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000452")]
	protected virtual string SpawnedPath
	{
		[Token(Token = "0x6001D56")]
		[Address(RVA = "0x468E20", Offset = "0x467820", VA = "0x180468E20", Slot = "20")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6001D57")]
		[Address(RVA = "0x4B1870", Offset = "0x4B0270", VA = "0x1804B1870")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000453")]
	protected virtual EasingType DefaultEasing
	{
		[Token(Token = "0x6001D58")]
		[Address(RVA = "0x4B18D0", Offset = "0x4B02D0", VA = "0x1804B18D0", Slot = "21")]
		[CompilerGenerated]
		get
		{
			return default(EasingType);
		}
		[Token(Token = "0x6001D59")]
		[Address(RVA = "0x4B18E0", Offset = "0x4B02E0", VA = "0x1804B18E0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000454")]
	protected virtual ISpawnManager SpawnManager
	{
		[Token(Token = "0x6001D5A")]
		[Address(RVA = "0x4B18F0", Offset = "0x4B02F0", VA = "0x1804B18F0", Slot = "22")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000455")]
	protected virtual List<UniTask> tasks
	{
		[Token(Token = "0x6001D5B")]
		[Address(RVA = "0x33B470", Offset = "0x339E70", VA = "0x18033B470", Slot = "23")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000456")]
	protected virtual CameraConfiguration CameraConfig
	{
		[Token(Token = "0x6001D5C")]
		[Address(RVA = "0x4B1950", Offset = "0x4B0350", VA = "0x1804B1950", Slot = "24")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000457")]
	protected virtual CancellationTokenSource CTS
	{
		[Token(Token = "0x6001D5D")]
		[Address(RVA = "0x471780", Offset = "0x470180", VA = "0x180471780", Slot = "25")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6001D5E")]
		[Address(RVA = "0x4B19D0", Offset = "0x4B03D0", VA = "0x1804B19D0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x6001D5F")]
	[Address(RVA = "0x4B1A30", Offset = "0x4B0430", VA = "0x1804B1A30", Slot = "26")]
	public virtual void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
	{
	}

	[Token(Token = "0x6001D60")]
	[Address(RVA = "0x4B1C90", Offset = "0x4B0690", VA = "0x1804B1C90", Slot = "27")]
	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__65))]
	public virtual UniTask AwaitSpawn([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001D61")]
	[Address(RVA = "0x4B1E20", Offset = "0x4B0820", VA = "0x1804B1E20", Slot = "28")]
	[AsyncStateMachine(typeof(_003CAnimateKey_003Ed__66))]
	protected virtual UniTask AnimateKey(IActor actor, int keyIndex, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001D62")]
	[Address(RVA = "0x4B2020", Offset = "0x4B0A20", VA = "0x1804B2020", Slot = "29")]
	protected virtual Vector3? ParseScale(string value)
	{
		return null;
	}

	[Token(Token = "0x6001D63")]
	[Address(RVA = "0x4B27A0", Offset = "0x4B11A0", VA = "0x1804B27A0", Slot = "30")]
	protected virtual void ParseParameter(int paramIdx, IEnumerable<string> parameters)
	{
	}

	[Token(Token = "0x6001D64")]
	[Address(RVA = "0x4B3130", Offset = "0x4B1B30", VA = "0x1804B3130", Slot = "31")]
	protected virtual void FillMissingDurations()
	{
	}

	[Token(Token = "0x6001D65")]
	[Address(RVA = "0x4B33A0", Offset = "0x4B1DA0", VA = "0x1804B33A0", Slot = "32")]
	[AsyncStateMachine(typeof(_003CLoopRoutine_003Ed__70))]
	protected virtual UniTaskVoid LoopRoutine(IActor actor, AsyncToken token)
	{
		return default(UniTaskVoid);
	}

	[Token(Token = "0x6001D66")]
	[Address(RVA = "0x4B3580", Offset = "0x4B1F80", VA = "0x1804B3580", Slot = "33")]
	protected virtual void OnDestroy()
	{
	}

	[Token(Token = "0x6001D67")]
	[Address(RVA = "0x4B3730", Offset = "0x4B2130", VA = "0x1804B3730", Slot = "34")]
	protected virtual AsyncToken InitializeCTS(AsyncToken token)
	{
		return default(AsyncToken);
	}

	[Token(Token = "0x6001D68")]
	[Address(RVA = "0x4B3910", Offset = "0x4B2310", VA = "0x1804B3910")]
	public Animate()
	{
	}
}
