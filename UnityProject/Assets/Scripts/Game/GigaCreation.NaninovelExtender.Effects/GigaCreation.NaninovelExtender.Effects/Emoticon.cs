using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using YieldAwaitable = Naninovel.YieldAwaitable;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;
using UnityEngine.Playables;

namespace GigaCreation.NaninovelExtender.Effects
{
    [Token(Token = "0x200000A")]
    public class Emoticon : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200000B")]
    	[CompilerGenerated]
    	private struct _003CAwaitSpawn_003Ed__21
    {
    		[Token(Token = "0x4000036")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000037")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000038")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Emoticon _003C_003E4__this;

    		[Token(Token = "0x4000039")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x400003A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600001D")]
    		[Address(RVA = "0xD075F0", Offset = "0xD05FF0", VA = "0x180D075F0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600001E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000028")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private Transform _transform;

    	[Token(Token = "0x4000029")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private PlayableDirector _playableDirector;

    	[Token(Token = "0x400002A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private AudioSource _audioSource;

    	[Token(Token = "0x400002B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private SpriteRenderer[] _spriteRenderers;

    	[Token(Token = "0x400002C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[Header("Parameters")]
    	[SerializeField]
    	private bool _hiddenAtAwake;

    	[Token(Token = "0x400002D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private IScriptPlayer _naniScriptPlayer;

    	[Token(Token = "0x400002E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private IAudioManager _naniAudioManager;

    	[Token(Token = "0x400002F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private Vector3 _initialPosition;

    	[Token(Token = "0x4000030")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	private string _spawnedPath;

    	[Token(Token = "0x4000031")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	private Vector3 _position;

    	[Token(Token = "0x4000032")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x7C")]
    	private float _scale;

    	[Token(Token = "0x4000033")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
    	private bool _flip;

    	[Token(Token = "0x4000034")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x81")]
    	private bool _silent;

    	[Token(Token = "0x4000035")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x82")]
    	private bool _asap;

    	[Token(Token = "0x17000002")]
    	private IScriptPlayer NaniScriptPlayer
    	{
    		[Token(Token = "0x6000015")]
    		[Address(RVA = "0xD06490", Offset = "0xD04E90", VA = "0x180D06490")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000003")]
    	private IAudioManager NaniAudioManager
    	{
    		[Token(Token = "0x6000016")]
    		[Address(RVA = "0xD06550", Offset = "0xD04F50", VA = "0x180D06550")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000017")]
    	[Address(RVA = "0xD06610", Offset = "0xD05010", VA = "0x180D06610")]
    	private void Awake()
    	{
    	}

    	[Token(Token = "0x6000018")]
    	[Address(RVA = "0xD06970", Offset = "0xD05370", VA = "0x180D06970")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x6000019")]
    	[Address(RVA = "0xD06B80", Offset = "0xD05580", VA = "0x180D06B80", Slot = "4")]
    	public void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
    	{
    	}

    	[Token(Token = "0x600001A")]
    	[Address(RVA = "0xD073A0", Offset = "0xD05DA0", VA = "0x180D073A0", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__21))]
    	public UniTask AwaitSpawn([Optional] AsyncToken asyncToken)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600001B")]
    	[Address(RVA = "0xD07530", Offset = "0xD05F30", VA = "0x180D07530")]
    	private void DestroyIfSpawned()
    	{
    	}

    	[Token(Token = "0x600001C")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public Emoticon()
    	{
    	}
    }
}
