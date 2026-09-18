#nullable enable annotations
﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.NaninovelExtender.Effects
{
    [Token(Token = "0x2000012")]
    public class Mattes : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IParameterized, DestroySpawned.IAwaitable
    {
    	[Token(Token = "0x2000013")]
    	private enum Direction
    	{
    		[Token(Token = "0x4000079")]
    		Top,
    		[Token(Token = "0x400007A")]
    		Right,
    		[Token(Token = "0x400007B")]
    		Bottom,
    		[Token(Token = "0x400007C")]
    		Left
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000017")]
    	[CompilerGenerated]
    	private struct _003CAwaitDestroy_003Ed__30
    {
    		[Token(Token = "0x4000081")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000082")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000083")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Mattes _003C_003E4__this;

    		[Token(Token = "0x4000084")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x4000085")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x4000086")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000045")]
    		[Address(RVA = "0xD0C690", Offset = "0xD0B090", VA = "0x180D0C690", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000046")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000018")]
    	[CompilerGenerated]
    	private struct _003CAwaitSpawn_003Ed__28
    {
    		[Token(Token = "0x4000087")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000088")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000089")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Mattes _003C_003E4__this;

    		[Token(Token = "0x400008A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x400008B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x400008C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000047")]
    		[Address(RVA = "0xD0D240", Offset = "0xD0BC40", VA = "0x180D0D240", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000048")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000063")]
    	internal const float ZOffset = 91f;

    	[Token(Token = "0x4000064")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private Canvas _canvas;

    	[Token(Token = "0x4000065")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private RectTransform[] _rectTransforms;

    	[Token(Token = "0x4000066")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private Image[] _images;

    	[Token(Token = "0x4000067")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[Header("Parameters")]
    	[SerializeField]
    	private float _defaultChangeWidthDuration;

    	[Token(Token = "0x4000068")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
    	[SerializeField]
    	private float _defaultChangeColorDuration;

    	[Token(Token = "0x4000069")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[SerializeField]
    	private Color _defaultColor;

    	[Token(Token = "0x400006A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[SerializeField]
    	private EasingType _defaultEasingType;

    	[Token(Token = "0x400006B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private readonly IList<Tweener<FloatTween>> _changeWidthTweeners;

    	[Token(Token = "0x400006C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	private readonly IList<Tweener<ColorTween>> _changeColorTweeners;

    	[Token(Token = "0x400006D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	private readonly IList<UniTask> _tweenTasks;

    	[Token(Token = "0x400006E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	private ISpawnManager _spawnManager;

    	[Token(Token = "0x400006F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    	private Camera _camera;

    	[Token(Token = "0x4000070")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
    	private float?[] _widths;

    	[Token(Token = "0x4000071")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
    	private float _positionZ;

    	[Token(Token = "0x4000072")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x8C")]
    	private Color? _color;

    	[Token(Token = "0x4000073")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
    	private float _changeWidthDuration;

    	[Token(Token = "0x4000074")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA4")]
    	private float _changeColorDuration;

    	[Token(Token = "0x4000075")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
    	private EasingType _easingType;

    	[Token(Token = "0x4000076")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xAC")]
    	private float _stopDuration;

    	[Token(Token = "0x4000077")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
    	private CancellationTokenSource _cts;

    	[Token(Token = "0x17000004")]
    	private ISpawnManager SpawnManager
    	{
    		[Token(Token = "0x6000033")]
    		[Address(RVA = "0xD0AA40", Offset = "0xD09440", VA = "0x180D0AA40")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000005")]
    	private Camera Camera
    	{
    		[Token(Token = "0x6000034")]
    		[Address(RVA = "0xD0AB00", Offset = "0xD09500", VA = "0x180D0AB00")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000035")]
    	[Address(RVA = "0xD0ABF0", Offset = "0xD095F0", VA = "0x180D0ABF0")]
    	private void Awake()
    	{
    	}

    	[Token(Token = "0x6000036")]
    	[Address(RVA = "0xD0ACF0", Offset = "0xD096F0", VA = "0x180D0ACF0")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x6000037")]
    	[Address(RVA = "0xD0AD90", Offset = "0xD09790", VA = "0x180D0AD90", Slot = "4")]
    	public void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
    	{
    	}

    	[Token(Token = "0x6000038")]
    	[Address(RVA = "0xD0B5C0", Offset = "0xD09FC0", VA = "0x180D0B5C0", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__28))]
    	public UniTask AwaitSpawn([Optional] AsyncToken asyncToken)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000039")]
    	[Address(RVA = "0xD0B740", Offset = "0xD0A140", VA = "0x180D0B740", Slot = "6")]
    	public void SetDestroyParameters(IReadOnlyList<string> parameters)
    	{
    	}

    	[Token(Token = "0x600003A")]
    	[Address(RVA = "0xD0B910", Offset = "0xD0A310", VA = "0x180D0B910", Slot = "7")]
    	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__30))]
    	public UniTask AwaitDestroy([Optional] AsyncToken asyncToken)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600003B")]
    	[Address(RVA = "0xD0BA90", Offset = "0xD0A490", VA = "0x180D0BA90")]
    	private void ClearAllTweeners()
    	{
    	}

    	[Token(Token = "0x600003C")]
    	[Address(RVA = "0xD0BEF0", Offset = "0xD0A8F0", VA = "0x180D0BEF0")]
    	private float GetCurrentWidth(Direction direction)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x600003D")]
    	[Address(RVA = "0xD0C0A0", Offset = "0xD0AAA0", VA = "0x180D0C0A0")]
    	private void SetWidth(float value, Direction direction)
    	{
    	}

    	[Token(Token = "0x600003E")]
    	[Address(RVA = "0xD0C2A0", Offset = "0xD0ACA0", VA = "0x180D0C2A0")]
    	public Mattes()
    	{
    	}
    }
}
