using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000335")]
public static class Engine
{
	[Token(Token = "0x200033B")]
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass70_0<T> where T : UnityEngine.Object
	{
		[Token(Token = "0x4000C11")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int? layer;

		[Token(Token = "0x6000F99")]
		public _003C_003Ec__DisplayClass70_0()
		{
		}

		[Token(Token = "0x6000F9A")]
		internal void _003CInstantiate_003Eb__0(GameObject obj)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200033F")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__54
{
		[Token(Token = "0x4000C17")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000C18")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000C19")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public EngineParams @params;

		[Token(Token = "0x4000C1A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000C1B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private int _003Ci_003E5__2;

		[Token(Token = "0x6000F9F")]
		[Address(RVA = "0x568310", Offset = "0x566D10", VA = "0x180568310", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000FA0")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000340")]
	[CompilerGenerated]
	private struct _003CInstantiate_003Ed__70<T> where T : UnityEngine.Object
{
		[Token(Token = "0x4000C1C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000C1D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<T> _003C_003Et__builder;

		[Token(Token = "0x4000C1E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int? layer;

		[Token(Token = "0x4000C1F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string name;

		[Token(Token = "0x4000C20")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T prototype;

		[Token(Token = "0x4000C21")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Transform parent;

		[Token(Token = "0x4000C22")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x4000C23")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private _003C_003Ec__DisplayClass70_0<T> _003C_003E8__1;

		[Token(Token = "0x4000C24")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private AsyncInstantiateOperation<T> _003Chandle_003E5__2;

		[Token(Token = "0x4000C25")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000FA1")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000FA2")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000BFC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private static readonly List<UnityEngine.Object> objects;

	[Token(Token = "0x4000BFD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private static readonly List<IEngineService> services;

	[Token(Token = "0x4000BFE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private static readonly Dictionary<Type, IEngineService> cachedGetServiceResults;

	[Token(Token = "0x4000BFF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private static readonly List<Func<UniTask>> preInitializationTasks;

	[Token(Token = "0x4000C00")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private static readonly List<Func<UniTask>> postInitializationTasks;

	[Token(Token = "0x4000C01")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private static IConfigurationProvider configurationProvider;

	[Token(Token = "0x4000C02")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	private static UniTaskCompletionSource initializeTCS;

	[Token(Token = "0x4000C03")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	private static CancellationTokenSource destroyCTS;

	[Token(Token = "0x4000C04")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	private static ILogger logger;

	[Token(Token = "0x4000C05")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private static string editorTransientRoot;

	[Token(Token = "0x1700020E")]
	public static EngineTypes Types
	{
		[Token(Token = "0x6000F5D")]
		[Address(RVA = "0x564A80", Offset = "0x563480", VA = "0x180564A80")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700020F")]
	public static EngineConfiguration Configuration
	{
		[Token(Token = "0x6000F5E")]
		[Address(RVA = "0x564AE0", Offset = "0x5634E0", VA = "0x180564AE0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000F5F")]
		[Address(RVA = "0x564B40", Offset = "0x563540", VA = "0x180564B40")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000210")]
	public static IEngineBehaviour Behaviour
	{
		[Token(Token = "0x6000F60")]
		[Address(RVA = "0x564C00", Offset = "0x563600", VA = "0x180564C00")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000F61")]
		[Address(RVA = "0x564C60", Offset = "0x563660", VA = "0x180564C60")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000211")]
	public static ITime Time
	{
		[Token(Token = "0x6000F62")]
		[Address(RVA = "0x564D20", Offset = "0x563720", VA = "0x180564D20")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000F63")]
		[Address(RVA = "0x564D80", Offset = "0x563780", VA = "0x180564D80")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000212")]
	public static IReadOnlyCollection<IEngineService> Services
	{
		[Token(Token = "0x6000F64")]
		[Address(RVA = "0x564E40", Offset = "0x563840", VA = "0x180564E40")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000213")]
	public static GameObject RootObject
	{
		[Token(Token = "0x6000F65")]
		[Address(RVA = "0x564EA0", Offset = "0x5638A0", VA = "0x180564EA0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000214")]
	public static bool Initialized
	{
		[Token(Token = "0x6000F66")]
		[Address(RVA = "0x564F60", Offset = "0x563960", VA = "0x180564F60")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000215")]
	public static bool Initializing
	{
		[Token(Token = "0x6000F67")]
		[Address(RVA = "0x5650D0", Offset = "0x563AD0", VA = "0x1805650D0")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000216")]
	public static CancellationToken DestroyToken
	{
		[Token(Token = "0x6000F68")]
		[Address(RVA = "0x565240", Offset = "0x563C40", VA = "0x180565240")]
		get
		{
			return default(CancellationToken);
		}
	}

	[Token(Token = "0x17000217")]
	public static string EditorTransientRoot
	{
		[Token(Token = "0x6000F69")]
		[Address(RVA = "0x565380", Offset = "0x563D80", VA = "0x180565380")]
		get
		{
			return null;
		}
		[Token(Token = "0x6000F6A")]
		[Address(RVA = "0x565420", Offset = "0x563E20", VA = "0x180565420")]
		set
		{
		}
	}

	[Token(Token = "0x14000049")]
	public static event Action OnInitializationStarted
	{
		[Token(Token = "0x6000F55")]
		[Address(RVA = "0x563FC0", Offset = "0x5629C0", VA = "0x180563FC0")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000F56")]
		[Address(RVA = "0x564110", Offset = "0x562B10", VA = "0x180564110")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400004A")]
	public static event Action OnInitializationFinished
	{
		[Token(Token = "0x6000F57")]
		[Address(RVA = "0x564260", Offset = "0x562C60", VA = "0x180564260")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000F58")]
		[Address(RVA = "0x5643B0", Offset = "0x562DB0", VA = "0x1805643B0")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400004B")]
	public static event Action<float> OnInitializationProgress
	{
		[Token(Token = "0x6000F59")]
		[Address(RVA = "0x564500", Offset = "0x562F00", VA = "0x180564500")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000F5A")]
		[Address(RVA = "0x564670", Offset = "0x563070", VA = "0x180564670")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400004C")]
	public static event Action OnDestroyed
	{
		[Token(Token = "0x6000F5B")]
		[Address(RVA = "0x5647E0", Offset = "0x5631E0", VA = "0x1805647E0")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000F5C")]
		[Address(RVA = "0x564930", Offset = "0x563330", VA = "0x180564930")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6000F6B")]
	[Address(RVA = "0x5654E0", Offset = "0x563EE0", VA = "0x1805654E0")]
	public static void AddPreInitializationTask(Func<UniTask> task)
	{
	}

	[Token(Token = "0x6000F6C")]
	[Address(RVA = "0x565570", Offset = "0x563F70", VA = "0x180565570")]
	public static void RemovePreInitializationTask(Func<UniTask> task)
	{
	}

	[Token(Token = "0x6000F6D")]
	[Address(RVA = "0x565600", Offset = "0x564000", VA = "0x180565600")]
	public static void AddPostInitializationTask(Func<UniTask> task)
	{
	}

	[Token(Token = "0x6000F6E")]
	[Address(RVA = "0x565690", Offset = "0x564090", VA = "0x180565690")]
	public static void RemovePostInitializationTask(Func<UniTask> task)
	{
	}

	[Token(Token = "0x6000F6F")]
	[Address(RVA = "0x565720", Offset = "0x564120", VA = "0x180565720")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__54))]
	public static UniTask Initialize(EngineParams @params)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000F70")]
	[Address(RVA = "0x565840", Offset = "0x564240", VA = "0x180565840")]
	public static void Reset()
	{
	}

	[Token(Token = "0x6000F71")]
	[Address(RVA = "0x5659F0", Offset = "0x5643F0", VA = "0x1805659F0")]
	public static void Reset(IReadOnlyCollection<Type> exclude)
	{
	}

	[Token(Token = "0x6000F72")]
	[Address(RVA = "0x565CE0", Offset = "0x5646E0", VA = "0x180565CE0")]
	public static void Reset(params Type[] exclude)
	{
	}

	[Token(Token = "0x6000F73")]
	[Address(RVA = "0x565D30", Offset = "0x564730", VA = "0x180565D30")]
	public static void Destroy()
	{
	}

	[Token(Token = "0x6000F74")]
	public static T GetConfiguration<T>() where T : Configuration
	{
		return null;
	}

	[Token(Token = "0x6000F75")]
	[Address(RVA = "0x5666B0", Offset = "0x5650B0", VA = "0x1805666B0")]
	public static Configuration GetConfiguration(Type type)
	{
		return null;
	}

	[Token(Token = "0x6000F76")]
	[CanBeNull]
	public static TService GetService<TService>() where TService : class, IEngineService
	{
		return null;
	}

	[Token(Token = "0x6000F77")]
	public static bool TryGetService<TService>(out TService result) where TService : class, IEngineService
	{
		result = default;
		return default(bool);
	}

	[Token(Token = "0x6000F78")]
	public static TService GetServiceOrErr<TService>() where TService : class, IEngineService
	{
		return null;
	}

	[Token(Token = "0x6000F79")]
	[Address(RVA = "0x5667E0", Offset = "0x5651E0", VA = "0x1805667E0")]
	[CanBeNull]
	public static IEngineService GetService(Type serviceType)
	{
		return null;
	}

	[Token(Token = "0x6000F7A")]
	[CanBeNull]
	public static TService FindService<TService>(Predicate<TService> predicate) where TService : class, IEngineService
	{
		return null;
	}

	[Token(Token = "0x6000F7B")]
	[CanBeNull]
	public static TService FindService<TService, TState>(TState state, Func<TService, TState, bool> predicate) where TService : class, IEngineService
	{
		return null;
	}

	[Token(Token = "0x6000F7C")]
	public static void FindAllServices<TService>(ICollection<TService> services) where TService : class, IEngineService
	{
	}

	[Token(Token = "0x6000F7D")]
	public static void FindAllServices<TService>(ICollection<TService> services, Predicate<TService> predicate) where TService : class, IEngineService
	{
	}

	[Token(Token = "0x6000F7E")]
	public static void FindAllServices<TService, TState>(ICollection<TService> services, TState state, Func<TService, TState, bool> predicate) where TService : class, IEngineService
	{
	}

	[Token(Token = "0x6000F7F")]
	[AsyncStateMachine(typeof(_003CInstantiate_003Ed__70<>))]
	public static UniTask<T> Instantiate<T>(T prototype, [Optional] string name, [Optional] int? layer, [Optional] Transform parent, [Optional] AsyncToken token) where T : UnityEngine.Object
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x6000F80")]
	[Address(RVA = "0x5669B0", Offset = "0x5653B0", VA = "0x1805669B0")]
	public static GameObject CreateObject([Optional] string name, [Optional] int? layer, [Optional] Transform parent, params Type[] components)
	{
		return null;
	}

	[Token(Token = "0x6000F81")]
	public static T CreateObject<T>([Optional] string name, [Optional] int? layer, [Optional] Transform parent) where T : Component
	{
		return null;
	}

	[Token(Token = "0x6000F82")]
	[Address(RVA = "0x566EA0", Offset = "0x5658A0", VA = "0x180566EA0")]
	public static GameObject FindObject(string name)
	{
		return null;
	}

	[Token(Token = "0x6000F83")]
	public static T LoadInternalResource<T>(string relativePath) where T : UnityEngine.Object
	{
		return null;
	}

	[Token(Token = "0x6000F84")]
	[Address(RVA = "0x567130", Offset = "0x565B30", VA = "0x180567130")]
	public static void UseLogger(ILogger logger)
	{
	}

	[Token(Token = "0x6000F85")]
	[Address(RVA = "0x5671F0", Offset = "0x565BF0", VA = "0x1805671F0")]
	public static void Log(string message, [Optional] PlaybackSpot? spot)
	{
	}

	[Token(Token = "0x6000F86")]
	[Address(RVA = "0x5672C0", Offset = "0x565CC0", VA = "0x1805672C0")]
	public static void Warn(string message, [Optional] PlaybackSpot? spot)
	{
	}

	[Token(Token = "0x6000F87")]
	[Address(RVA = "0x567390", Offset = "0x565D90", VA = "0x180567390")]
	public static void Err(string message, [Optional] PlaybackSpot? spot)
	{
	}

	[Token(Token = "0x6000F88")]
	[Address(RVA = "0x567460", Offset = "0x565E60", VA = "0x180567460")]
	internal static string FormatMessage(string message, PlaybackSpot? spot)
	{
		return null;
	}

	[Token(Token = "0x6000F89")]
	[Address(RVA = "0x567780", Offset = "0x566180", VA = "0x180567780")]
	[RuntimeInitializeOnLoadMethod]
	private static void CheckUnityVersion()
	{
	}
}
