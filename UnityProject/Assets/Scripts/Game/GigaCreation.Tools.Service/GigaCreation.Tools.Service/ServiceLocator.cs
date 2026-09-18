using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.Service
{
    [Token(Token = "0x2000005")]
    public static class ServiceLocator
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200000C")]
    	[CompilerGenerated]
    	private struct _003CUnregisterAsync_003Ed__3<TService> where TService : class, IService
    {
    		[Token(Token = "0x400000D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400000E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400000F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public TService service;

    		[Token(Token = "0x4000010")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private KeyValuePair<Type, IService> _003CregisteredService_003E5__2;

    		[Token(Token = "0x4000011")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private ValueTaskAwaiter _003C_003Eu__1;

    		[Token(Token = "0x4000012")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000018")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000019")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000006")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private static readonly Dictionary<Type, IService> s_services;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD531E0, 215 B (the static constructor, absent from the
    	// stub: without it `s_services` stays null and every other member dies on a null dereference).
    	//
    	// Native evidence (tools/unity-rebuild/out/logs/disas_servicelocator_cctor_0xD531E0.txt):
    	//   1. `METHODINFO Dictionary<Type, IService>..ctor()` / `TYPE Dictionary<Type, IService>_TypeInfo`
    	//      / `TYPE ServiceLocator_TypeInfo` are the only runtime-metadata initializations.
    	//   2. `il2cpp_codegen_object_new(TYPE Dictionary<Type, IService>_TypeInfo)` then
    	//      `Dictionary<__Il2CppFullySharedGenericType, __Il2CppFullySharedGenericType>$$.ctor`
    	//      -> `new Dictionary<Type, IService>()` (generic body, fully-shared instantiation).
    	//   3. stored through the statics block of its own type:
    	//      `mov rcx,[ServiceLocator_TypeInfo]; mov rdx,[rcx+0xB8]; mov [rdx],rbx`
    	//      (+0xB8 = Il2CppClass::static_fields; offset 0 = s_services, the only static field).
    	//   4. the trailing `bts rcx,r8` / `lock cmpxchg [rdx]` loop is the IL2CPP GC write barrier for
    	//      that static reference store - codegen scaffolding, not game logic.
    	//
    	// TODO(evidence): the native `.cctor` cannot distinguish an explicit static constructor from a
    	// field initializer (`private static readonly ... s_services = new Dictionary<...>();`), because
    	// Roslyn compiles both into this same `.cctor`. The explicit constructor is kept because the
    	// task's premise is that the .cctor is the place the native code initializes it.
    	static ServiceLocator()
    	{
    		s_services = new Dictionary<Type, IService>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x752130, 430 B (shared-generic body: the `<object>`
    	// instantiation below is the body every reference-type instantiation executes)
    	//
    	// Native evidence (tools/unity-rebuild/out/logs/disas_servicelocator_register_0x752130.txt):
    	//   1. `test rdi,rdi; je ...` (rdi = the `service` argument) -> `Debug.LogError(STR 'It does not
    	//      allow to register null.')`; the literal goes straight to LogError, no concatenation.
    	//   2. typeof(TService): rgctx[1] (the Il2CppType* of TService) -> `Class::FromIl2CppType`
    	//      (0x29F650; it switches on Il2CppType->type at +0xA and, for a class/valuetype, reads
    	//      Il2CppType->data.klass at +0x0) -> `lea rcx,[klass+0x20]` = Il2CppClass::byval_arg ->
    	//      `Reflection::GetTypeObject` (0x299D20: cache probe 0x29D960, else object_new of TYPE
    	//      data_30D77F0 with the Il2CppType* stored at +0x10) => a System.Type.
    	//      This helper chain is the canonical shared-generic codegen of `typeof(T)`, and the result
    	//      is used directly as the Dictionary key - so the KEY IS typeof(TService). There is no
    	//      `Object.GetType` (no klass->byval_arg/GetTypeObject on the service instance) in this body.
    	//   3. `METHODINFO Dictionary<Type, IService>.TryAdd()` + `Dictionary<..>$$TryAdd(key, value)`
    	//      (rdx = that Type, r8 = the service) - TryAdd, not Add/ContainsKey+Add.
    	//   4. `test al,al; jne <epilogue>`: on false (already present) a virtual call through
    	//      klass+0x1B8 / +0x1C0 is made on the Type instance. Vtable base is klass+0x138 with a
    	//      16-byte stride, so 0x1B8 = slot 8 = MemberInfo.get_Name (System.Type's own Slot 3
    	//      ToString would be 0x168; Type.IsAssignableFrom's Slot 24 is 0x2B8, which the
    	//      `<Get>b__0` lambda below really does call - that anchor pins the base/stride).
    	//      The name is then concatenated with `String$$Concat` (sig: Concat(string, string)) and
    	//      passed to `Debug.LogWarning`.
    	[Token(Token = "0x6000003")]
    	public static void Register<TService>(TService service) where TService : class, IService
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x752130
    		if (service == null)
    		{
    			UnityEngine.Debug.LogError("It does not allow to register null.");
    			return;
    		}

    		Type type = typeof(TService);
    		if (!s_services.TryAdd(type, service))
    		{
    			UnityEngine.Debug.LogWarning("A service of the same type is already registered: " + type.Name);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (the `IDisposable`/`IAsyncDisposable`/`IUniTaskAsyncDisposable`
    	// null-guard spelling is `is T x`, i.e. isinst + null test) | RVA: 0x752790, 1684 B
    	//
    	// Native evidence:
    	//   tools/unity-rebuild/out/logs/disas_servicelocator_unregister_0x752790.txt (body)
    	//   tools/unity-rebuild/out/logs/disas_servicelocator_pred_unregister_b0_0x7D9B50.txt (lambda)
    	//
    	//   1. `il2cpp_codegen_object_new` of rgctx[0] = `<>c__DisplayClass2_0<TService>`, then
    	//      `[rax+0x10] = service` (+ write barrier) - the closure field declared in dump.cs as
    	//      `public TService service; // 0x0`.
    	//   2. `TYPE System.Func<KeyValuePair<Type, IService>, bool>_TypeInfo` +
    	//      `System.Func<..>$$.ctor(displayClass, rgctx[3])` + `Enumerable$$FirstOrDefault<KVP<Type,IService>>`
    	//      over the statics block's dictionary -> the linear scan is
    	//      `s_services.FirstOrDefault(pair => ...)`.
    	//   3. the predicate body (0x7D9B50) is exactly System.Object::Equals(object, object): get_Value(),
    	//      `cmp rcx,rdx; je true`, `test rcx,rcx; je false`, `test rdx,rdx; je false`, then a virtual
    	//      dispatch through klass+0x138 = vtable slot 0 = Object.Equals(object) with rdx = the captured
    	//      service. So it is `object.Equals(pair.Value, service)` and NOT `pair.Value == service`
    	//      (a `==` on these operands would compile to a plain reference compare) and not a null-unsafe
    	//      `pair.Value.Equals(service)` (that would throw on a null registration instead of returning false).
    	//   4. the returned KVP is copied out (double memcpy through the fully-shared-generic size helper
    	//      0x306510) and `get_Key`'s result is null-tested -> `if (registeredService.Key == null)`:
    	//      on null, `service.GetType()` (klass->byval_arg + Reflection::GetTypeObject; note this one
    	//      IS the instance GetType, unlike every key computation) is formatted into
    	//      STR 'The passed service is not registered: {0}' (ParamsArray + String$$FormatHelper) and
    	//      passed to `Debug.LogWarning`; the method then returns.
    	//   5. `METHODINFO Dictionary<Type, IService>.Remove()` with rdx = the KVP key.
    	//   6. three consecutive `il2cpp isinst` (0x298020 = IsInst(object, klass)) + null test + interface
    	//      dispatch, taking the get_Value() result each time:
    	//        System.IDisposable_TypeInfo            -> interface call thunk 0x22E0  => Dispose()
    	//        System.IAsyncDisposable_TypeInfo       -> thunk 0x3490, ValueTask ignored
    	//        Cysharp...IUniTaskAsyncDisposable      -> thunk 0x3260, UniTask ignored
    	//      The two DisposeAsync results are NOT awaited and NOT observed here (no GetAwaiter /
    	//      IsCompleted / GetResult anywhere in this body) - that is the game's behaviour; see the
    	//      NOTE below, which is a finding and deliberately not "fixed".
    	[Token(Token = "0x6000004")]
    	public static void Unregister<TService>(TService service) where TService : class, IService
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x752790
    		KeyValuePair<Type, IService> registeredService = s_services.FirstOrDefault(pair => object.Equals(pair.Value, service));
    		if (registeredService.Key == null)
    		{
    			UnityEngine.Debug.LogWarning($"The passed service is not registered: {service.GetType()}");
    			return;
    		}

    		s_services.Remove(registeredService.Key);

    		// NOTE(quirk, reproduced as-is): both DisposeAsync results are discarded here, so an
    		// asynchronous teardown started by this method is never observed. UnregisterAsync is the
    		// variant that awaits them.
    		if (registeredService.Value is IDisposable disposable)
    		{
    			disposable.Dispose();
    		}

    		if (registeredService.Value is IAsyncDisposable asyncDisposable)
    		{
    			asyncDisposable.DisposeAsync();
    		}

    		if (registeredService.Value is IUniTaskAsyncDisposable uniTaskAsyncDisposable)
    		{
    			uniTaskAsyncDisposable.DisposeAsync();
    		}
    	}

    	[Token(Token = "0x6000005")]
    	[AsyncStateMachine(typeof(_003CUnregisterAsync_003Ed__3<>))]
    	public static UniTask UnregisterAsync<TService>(TService service) where TService : class, IService
    	{
    		// The recovered body lives in UnregisterAsyncCore: writing it *here* as an async method would
    		// make the compiler emit a nested state machine whose name collides with the decompiled
    		// `_003CUnregisterAsync_003Ed__3<TService>` retained above as evidence-bearing metadata.
    		return UnregisterAsyncCore(service);
    	}

    	// RECONSTRUCTED | Confidence: A (structure and every call/field offset pinned; the `object.Equals`
    	// spelling of the predicate is B, see Unregister) | RVA: 0x752E30 (kickoff, 325 B) +
    	// 0xB9F8B0 (state machine `<UnregisterAsync>d__3<object>.MoveNext`, 2094 B)
    	//
    	// Native evidence:
    	//   tools/unity-rebuild/out/logs/disas_servicelocator_unregisterasync_kickoff_0x752E30.txt
    	//   tools/unity-rebuild/out/logs/disas_servicelocator_unregisterasync_movenext_0xB9F8B0.txt
    	//   tools/unity-rebuild/out/logs/disas_servicelocator_pred_unregisterasync_b0_0x7DB2C0.txt
    	//
    	// Kickoff: zero the stack state machine, `state = -1`, `service` stored (with barrier),
    	// `AsyncUniTaskMethodBuilder$$Start<..>(ref stateMachine, rgctx[2] = MoveNext)` then
    	// `<builder>.get_Task()` - i.e. a plain `async UniTask` method, nothing else.
    	//
    	// MoveNext (state layout: +0x00 state, +0x08 builder, +0x18 service, +0x20 <registeredService>,
    	// +0x30 ValueTaskAwaiter <>u__1, +0x40 UniTask.Awaiter <>u__2 - exactly the struct kept above):
    	//   1. `<>c__DisplayClass3_0<TService>` + `Func<KVP,bool>` ctor with the `<UnregisterAsync>b__0`
    	//      MethodInfo (rgctx[5]) + `Enumerable$$FirstOrDefault` -> same scan as Unregister; the
    	//      predicate at 0x7DB2C0 is byte-for-byte the same Object.Equals(object, object) body, so
    	//      `pair => object.Equals(pair.Value, service)`. The KVP is hoisted into <registeredService>.
    	//   2. `KVP.get_Key` + null test -> on null: `service.GetType()` (display class .service ->
    	//      klass->byval_arg -> Reflection::GetTypeObject) -> `String$$Format(STR 'The passed service is
    	//      not registered: {0}', type)` -> `Debug.LogWarning`, then the builder's SetResult path.
    	//   3. `Dictionary<Type, IService>.Remove(KVP.get_Key())`.
    	//   4. `KVP.get_Value` -> isinst IDisposable (0x298020) -> interface call thunk 0x22E0 => Dispose().
    	//   5. `KVP.get_Value` -> isinst System.IAsyncDisposable -> thunk 0x3490 => `DisposeAsync()`
    	//      returning a ValueTask (TYPE System.Threading.Tasks.ValueTask_TypeInfo); then
    	//      `ValueTask.GetAwaiter` (0x3323C0 = IsCompleted), on false `state = 0`,
    	//      `<>u__1 = awaiter`, `AwaitOnCompleted<ValueTaskAwaiter,..>`, and on resume
    	//      `ValueTaskAwaiter.GetResult` (0x332470) -> `await asyncDisposable.DisposeAsync()`.
    	//   6. `KVP.get_Value` -> isinst Cysharp.Threading.Tasks.IUniTaskAsyncDisposable -> thunk 0x3260
    	//      => `DisposeAsync()` returning a UniTask (TYPE UniTask_TypeInfo); `GetAwaiter` (0x3314B0 =
    	//      IsCompleted), on false `state = 1`, `<>u__2 = awaiter`, `AwaitOnCompleted<..>`, on resume
    	//      `UniTask.Awaiter.GetResult` (0x31D0) -> `await uniTaskAsyncDisposable.DisposeAsync()`.
    	//   7. epilogue: state = -2, <registeredService> cleared, builder completion (interface thunk 0x22E0
    	//      on IStateMachineRunnerPromise with the SetResult slot).
    	//   dump.cs confirms the two interfaces' shapes: System.IAsyncDisposable.DisposeAsync() -> ValueTask,
    	//   Cysharp.Threading.Tasks.IUniTaskAsyncDisposable.DisposeAsync() -> UniTask.
    	private static async UniTask UnregisterAsyncCore<TService>(TService service) where TService : class, IService
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x752E30 / 0xB9F8B0
    		KeyValuePair<Type, IService> registeredService = s_services.FirstOrDefault(pair => object.Equals(pair.Value, service));
    		if (registeredService.Key == null)
    		{
    			UnityEngine.Debug.LogWarning($"The passed service is not registered: {service.GetType()}");
    			return;
    		}

    		s_services.Remove(registeredService.Key);

    		if (registeredService.Value is IDisposable disposable)
    		{
    			disposable.Dispose();
    		}

    		if (registeredService.Value is IAsyncDisposable asyncDisposable)
    		{
    			await asyncDisposable.DisposeAsync();
    		}

    		if (registeredService.Value is IUniTaskAsyncDisposable uniTaskAsyncDisposable)
    		{
    			await uniTaskAsyncDisposable.DisposeAsync();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x751D40, 533 B
    	//
    	// Native evidence:
    	//   tools/unity-rebuild/out/logs/disas_servicelocator_isregistered_0x751D40.txt
    	//   tools/unity-rebuild/out/logs/disas_servicelocator_pred_isregistered_b0_0x7DBBE0.txt
    	//
    	//   1. typeof(TService) through the very same rgctx[2] -> Class::FromIl2CppType (0x29F650) ->
    	//      klass->byval_arg -> Reflection::GetTypeObject (0x299D20) chain as Register, stored into
    	//      `<>c__DisplayClass4_0<TService>.type` (dump.cs declares that field as `Type type`).
    	//   2. `METHODINFO Dictionary<Type, IService>.ContainsKey()` + `Dictionary<..>$$ContainsKey(key)`
    	//      -> `test al,al; jne <return true>`.
    	//   3. `test dil,dil; je <return false>` (dil = includeDerivedType), then
    	//      `TYPE System.Func<KVP<Type,IService>,bool>_TypeInfo` + Func ctor (rgctx[3] = `<IsRegistered>b__0`)
    	//      + `Enumerable$$Any<KVP<Type,IService>>`.
    	//   4. the predicate (0x7DBBE0) reads `this.type` ([rcx+0x10]), calls `KVP.get_Key`, and dispatches
    	//      virtually through klass+0x2B8 = vtable slot 24 = System.Type::IsAssignableFrom(Type)
    	//      (dump.cs: `public virtual bool IsAssignableFrom(Type c) // Slot: 24`; 0x138 + 24*16 = 0x2B8)
    	//      with rdx = the pair key -> `type.IsAssignableFrom(pair.Key)`.
    	[Token(Token = "0x6000006")]
    	public static bool IsRegistered<TService>(bool includeDerivedType = false) where TService : class, IService
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x751D40
    		Type type = typeof(TService);
    		if (s_services.ContainsKey(type))
    		{
    			return true;
    		}

    		if (includeDerivedType)
    		{
    			return s_services.Any(pair => type.IsAssignableFrom(pair.Key));
    		}

    		return false;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x751F60, 453 B
    	//
    	// Native evidence:
    	//   tools/unity-rebuild/out/logs/disas_servicelocator_isregistered_service_0x751F60.txt
    	//   tools/unity-rebuild/out/logs/disas_servicelocator_pred_isregistered_service_b0_0x7DCD80.txt
    	//
    	//   1. `<>c__DisplayClass5_0<TService>` with `[rax+0x10] = service` (dump.cs: `TService service`).
    	//   2. `METHODINFO Dictionary<Type, IService>.ContainsValue()` + `Dictionary<..>$$ContainsValue(service)`
    	//      -> membership of that very INSTANCE, not a key lookup.
    	//   3. `includeDerivedType` gate then `Enumerable$$Any<KVP<Type,IService>>` with `<IsRegistered>b__0`.
    	//   4. the predicate (0x7DCD80) does `mov rcx,[this+0x10]` (the captured service), null-checks it
    	//      (NRE if null), takes `mov rcx,[rcx]; add rcx,0x20` (klass->byval_arg) + Reflection::GetTypeObject
    	//      = `service.GetType()` - the INSTANCE GetType, unlike the type-parameter overload above -
    	//      then `KVP.get_Key` and the same slot 24 dispatch (klass+0x2B8) with the receiver being that
    	//      runtime type -> `service.GetType().IsAssignableFrom(pair.Key)`.
    	[Token(Token = "0x6000007")]
    	public static bool IsRegistered<TService>(TService service, bool includeDerivedType = false) where TService : class, IService
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x751F60
    		if (s_services.ContainsValue(service))
    		{
    			return true;
    		}

    		if (includeDerivedType)
    		{
    			return s_services.Any(pair => service.GetType().IsAssignableFrom(pair.Key));
    		}

    		return false;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x751910, 1072 B
    	//
    	// Native evidence (tools/unity-rebuild/out/logs/disas_servicelocator_get_0x751910.txt):
    	//   1. `<>c__DisplayClass6_0<TService>` (il2cpp_codegen_object_new of rgctx[0]) with
    	//      `[+0x10] = <typeof(TService)>` - the typeof chain is rgctx[2] -> Class::FromIl2CppType
    	//      (0x29F650) -> `[klass+0x20]` (byval_arg) -> Reflection::GetTypeObject (0x299D20);
    	//      dump.cs declares the closure field as `Type type`.
    	//   2. `METHODINFO Dictionary<Type, IService>.TryGetValue()` with the key taken from that closure
    	//      field -> `s_services.TryGetValue(typeof(TService), out ...)`.
    	//   3. on a hit: `rgctx[3]` (Il2CppClass* of TService) + `Class::IsAssignableFrom` (0x2A06D0, the
    	//      internal sibling of the exported il2cpp_class_is_has_parent/assignable pair) with the
    	//      instance's klass, plus an interface branch ending in 0x247C80 (IsInst) and, finally,
    	//      `cmp rbx,[il2cpp_defaults.object_class (data_30D7660)]; cmove rdi,rsi` (= "isinst to object
    	//      always succeeds") - together this is the ldc+isinst lowering of `as TService`; the result is
    	//      returned directly, so a failed cast returns null with NO error message.
    	//   4. on a miss with includeDerivedType: `Func<KVP<Type,IService>,bool>` ctor (rgctx[4] =
    	//      `<Get>b__0`) + `Enumerable$$FirstOrDefault<KVP<Type,IService>>` -> the predicate (0x7DDC10)
    	//      is `this.type.IsAssignableFrom(KVP.get_Key())` (slot 24 = 0x2B8, as in IsRegistered).
    	//      The resulting `.Value` is copied out and null-tested; non-null is returned through
    	//      `il2cpp isinst` (0x298020) as TService.
    	//   5. the shared failure tail is reached from (a) a miss without includeDerivedType and (b) a
    	//      null scan result: `type.Name` (virtual slot 8 = klass+0x1B8 = MemberInfo.get_Name) is
    	//      concatenated with STR 'A service of the given type is not registered: ' through
    	//      `String$$Concat` (sig Concat(string, string)) and passed to `Debug.LogError`, then null
    	//      is returned (`xor eax,eax`).
    	//   NOTE(quirk, reproduced as-is): the failed-cast path of (3) returns null *silently* - it does
    	//   not fall into the LogError tail, while the scan path of (4) does.
    	[Token(Token = "0x6000008")]
    	public static TService Get<TService>(bool includeDerivedType = false) where TService : class, IService
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x751910
    		Type type = typeof(TService);
    		if (s_services.TryGetValue(type, out IService registeredService))
    		{
    			return registeredService as TService;
    		}

    		if (includeDerivedType)
    		{
    			IService derivedService = s_services.FirstOrDefault(pair => type.IsAssignableFrom(pair.Key)).Value;
    			if (derivedService != null)
    			{
    				return derivedService as TService;
    			}
    		}

    		UnityEngine.Debug.LogError("A service of the given type is not registered: " + type.Name);
    		return null;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x7522E0, 1188 B
    	//
    	// Native evidence (tools/unity-rebuild/out/logs/disas_servicelocator_tryget_0x7522E0.txt):
    	//   Same three-step shape as Get (its MethodInfo table names only Dictionary.TryGetValue,
    	//   Enumerable.FirstOrDefault, Func<KVP<Type,IService>,bool>, KVP.get_Value and ServiceLocator -
    	//   there is NO UnityEngine.Debug reference, so this member never logs):
    	//   1. typeof(TService) -> `<>c__DisplayClass7_0<TService>` field `Type type`.
    	//   2. `TryGetValue(type, out local)`; on a hit the value is cast with the inlined `as TService`
    	//      lowering and stored into the out parameter: `mov [rsi],rdi` (rsi = out TService*), followed
    	//      by the IL2CPP write-barrier sequence for that shared-generic store - the second
    	//      assignability/isinst block (0x7526C0..0x752728) is that barrier's reference-type test, its
    	//      result is discarded and it cannot throw - then `al = 1`. So the member returns TRUE even if
    	//      that cast produced null.
    	//   3. on a miss with includeDerivedType: `FirstOrDefault(pair => type.IsAssignableFrom(pair.Key))`
    	//      (predicate 0x7DE8F0, slot 24 at klass+0x2B8). A non-null `.Value` is `as TService`-cast
    	//      (0x298020), stored into the out parameter (with the 0x2460D0 barrier helper) and TRUE is
    	//      returned; a null `.Value` falls into the false tail.
    	//   4. false tail: `xor eax,eax; mov [rsi],rax` -> out parameter set to null, return false.
    	//   NOTE(quirk, reproduced as-is): unlike Get, TryGet does not distinguish "found but not a
    	//   TService" (true + null out) from "not found" (false + null out) in its return value.
    	[Token(Token = "0x6000009")]
    	public static bool TryGet<TService>(out TService service, bool includeDerivedType = false) where TService : class, IService
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x7522E0
    		Type type = typeof(TService);
    		if (s_services.TryGetValue(type, out IService registeredService))
    		{
    			service = registeredService as TService;
    			return true;
    		}

    		if (includeDerivedType)
    		{
    			IService derivedService = s_services.FirstOrDefault(pair => type.IsAssignableFrom(pair.Key)).Value;
    			if (derivedService != null)
    			{
    				service = derivedService as TService;
    				return true;
    			}
    		}

    		service = null;
    		return false;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD53160, 122 B
    	//
    	// Native evidence (tools/unity-rebuild/out/logs/disas_servicelocator_clearservices_0xD53160.txt):
    	//   1. only two metadata initializations: `METHODINFO Dictionary<Type, IService>.Clear()` and
    	//      `TYPE ServiceLocator_TypeInfo`.
    	//   2. the statics block is read exactly like everywhere else
    	//      (`[ServiceLocator_TypeInfo+0xB8] -> [0]` = s_services), with the usual
    	//      `cmp dword ptr [klass+0xE4],0` / 0x24CE60 class-initialization guard before it - the guard
    	//      is what makes the static constructor run before this reads s_services.
    	//   3. `test rcx,rcx; je -> THROW NullReferenceException` then a tail `jmp` to
    	//      `Dictionary<..>$$Clear` => `s_services.Clear();`.
    	//      The dictionary is CLEARED, never re-created, so this hook only drops state that would
    	//      otherwise survive a domain reload (`RuntimeInitializeOnLoadMethod(SubsystemRegistration)`).
    	[Token(Token = "0x600000A")]
    	[Address(RVA = "0xD53160", Offset = "0xD51B60", VA = "0x180D53160")]
    	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    	private static void ClearServices()
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD53160
    		s_services.Clear();
    	}
    }
}
