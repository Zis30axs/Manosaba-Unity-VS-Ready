using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200034D")]
    public class RuntimeInitializer : MonoBehaviour
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000353")]
    	[CompilerGenerated]
    	private struct _003CAwake_003Ed__8
    {
    		[Token(Token = "0x4000C5E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000C5F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000C60")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public RuntimeInitializer _003C_003E4__this;

    		[Token(Token = "0x4000C61")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001000")]
    		[Address(RVA = "0x56B5E0", Offset = "0x569FE0", VA = "0x18056B5E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001001")]
    		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000354")]
    	[CompilerGenerated]
    	private struct _003CInitialize_003Ed__3
    {
    		[Token(Token = "0x4000C62")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000C63")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000C64")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public IConfigurationProvider configurationProvider;

    		[Token(Token = "0x4000C65")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public IEnumerable<ServiceInitializationData> customInitializationData;

    		[Token(Token = "0x4000C66")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public ITime time;

    		[Token(Token = "0x4000C67")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private ScriptableUIBehaviour _003CinitializationUI_003E5__2;

    		[Token(Token = "0x4000C68")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001002")]
    		[Address(RVA = "0x56B860", Offset = "0x56A260", VA = "0x18056B860", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001003")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000355")]
    	[CompilerGenerated]
    	private struct _003CPlayInitScript_003Ed__5
    {
    		[Token(Token = "0x4000C69")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000C6A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000C6B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		private IScriptPlayer _003Cplayer_003E5__2;

    		[Token(Token = "0x4000C6C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private InteractionBlocker _003C_003E7__wrap2;

    		[Token(Token = "0x4000C6D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000C6E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private YieldAwaitable.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6001004")]
    		[Address(RVA = "0x56DA20", Offset = "0x56C420", VA = "0x18056DA20", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001005")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000356")]
    	[CompilerGenerated]
    	private struct _003CPlayIntroMovie_003Ed__4
    {
    		[Token(Token = "0x4000C6F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000C70")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000C71")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001006")]
    		[Address(RVA = "0x56E420", Offset = "0x56CE20", VA = "0x18056E420", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001007")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000C50")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private bool initializeOnAwake;

    	[Token(Token = "0x4000C51")]
    	private const string initPrefabName = "EngineInitializationUI";

    	[Token(Token = "0x4000C52")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private static UniTaskCompletionSource initializeTCS;

    	[Token(Token = "0x6000FE8")]
    	[Address(RVA = "0x56A7D0", Offset = "0x5691D0", VA = "0x18056A7D0")]
    	[AsyncStateMachine(typeof(_003CInitialize_003Ed__3))]
    	public static UniTask Initialize([Optional] IConfigurationProvider configurationProvider, [Optional] IEnumerable<ServiceInitializationData> customInitializationData, [Optional] ITime time)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000FE9")]
    	[Address(RVA = "0x56A9C0", Offset = "0x5693C0", VA = "0x18056A9C0")]
    	[AsyncStateMachine(typeof(_003CPlayIntroMovie_003Ed__4))]
    	private static UniTask PlayIntroMovie()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000FEA")]
    	[Address(RVA = "0x56AA70", Offset = "0x569470", VA = "0x18056AA70")]
    	[AsyncStateMachine(typeof(_003CPlayInitScript_003Ed__5))]
    	private static UniTask PlayInitScript()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000FEB")]
    	[Address(RVA = "0x56AB30", Offset = "0x569530", VA = "0x18056AB30")]
    	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    	private static void DisposeTCS()
    	{
    	}

    	[Token(Token = "0x6000FEC")]
    	[Address(RVA = "0x56ABF0", Offset = "0x5695F0", VA = "0x18056ABF0")]
    	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    	private static void OnApplicationLoaded()
    	{
    	}

    	[Token(Token = "0x6000FED")]
    	[Address(RVA = "0x56AC60", Offset = "0x569660", VA = "0x18056AC60")]
    	[AsyncStateMachine(typeof(_003CAwake_003Ed__8))]
    	private void Awake()
    	{
    	}

    	[Token(Token = "0x6000FEE")]
    	[Address(RVA = "0x56AD90", Offset = "0x569790", VA = "0x18056AD90")]
    	public RuntimeInitializer()
    	{
    	}
    }
}
