using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Effects;

[Token(Token = "0x200000F")]
public class MaskingRectangle : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IParameterized, DestroySpawned.IAwaitable
{
	[StructLayout(3)]
	[Token(Token = "0x2000010")]
	[CompilerGenerated]
	private struct _003CAwaitDestroy_003Ed__18
{
		[Token(Token = "0x4000059")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400005A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400005B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public MaskingRectangle _003C_003E4__this;

		[Token(Token = "0x400005C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x400005D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600002F")]
		[Address(RVA = "0xD09DF0", Offset = "0xD087F0", VA = "0x180D09DF0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000030")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000011")]
	[CompilerGenerated]
	private struct _003CAwaitSpawn_003Ed__16
{
		[Token(Token = "0x400005E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400005F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000060")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public MaskingRectangle _003C_003E4__this;

		[Token(Token = "0x4000061")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x4000062")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000031")]
		[Address(RVA = "0xD0A2B0", Offset = "0xD08CB0", VA = "0x180D0A2B0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000032")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400004B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private Transform _maskTransform;

	[Token(Token = "0x400004C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private SpriteRenderer _borderSpriteRenderer;

	[Token(Token = "0x400004D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[Header("Parameters")]
	[SerializeField]
	private float _defaultWidth;

	[Token(Token = "0x400004E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
	[SerializeField]
	private float _defaultHeight;

	[Token(Token = "0x400004F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private Color _defaultColor;

	[Token(Token = "0x4000050")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[SerializeField]
	private float _defaultDuration;

	[Token(Token = "0x4000051")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x4C")]
	[SerializeField]
	private EasingType _easingType;

	[Token(Token = "0x4000052")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private readonly Tweener<FloatTween> _tweener;

	[Token(Token = "0x4000053")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private float _width;

	[Token(Token = "0x4000054")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x5C")]
	private float _height;

	[Token(Token = "0x4000055")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private bool _calculateWidthBasedOnCameraHeight;

	[Token(Token = "0x4000056")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x64")]
	private float _duration;

	[Token(Token = "0x4000057")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private Color _color;

	[Token(Token = "0x4000058")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	private float _stopDuration;

	[Token(Token = "0x6000028")]
	[Address(RVA = "0xD08FF0", Offset = "0xD079F0", VA = "0x180D08FF0")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000029")]
	[Address(RVA = "0xD09000", Offset = "0xD07A00", VA = "0x180D09000", Slot = "4")]
	public void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
	{
	}

	[Token(Token = "0x600002A")]
	[Address(RVA = "0xD096B0", Offset = "0xD080B0", VA = "0x180D096B0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__16))]
	public UniTask AwaitSpawn([Optional] AsyncToken asyncToken)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600002B")]
	[Address(RVA = "0xD09830", Offset = "0xD08230", VA = "0x180D09830", Slot = "6")]
	public void SetDestroyParameters(IReadOnlyList<string> parameters)
	{
	}

	[Token(Token = "0x600002C")]
	[Address(RVA = "0xD09A00", Offset = "0xD08400", VA = "0x180D09A00", Slot = "7")]
	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__18))]
	public UniTask AwaitDestroy([Optional] AsyncToken asyncToken)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600002D")]
	[Address(RVA = "0xD09B80", Offset = "0xD08580", VA = "0x180D09B80")]
	private void SetAlpha(float alpha)
	{
	}

	[Token(Token = "0x600002E")]
	[Address(RVA = "0xD09D10", Offset = "0xD08710", VA = "0x180D09D10")]
	public MaskingRectangle()
	{
	}
}
