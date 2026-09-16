using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using WitchTrials.Models;

namespace WitchTrials.Presenters;

[Token(Token = "0x2000149")]
public class DefaultLocalePresenter : IDisposable
{
	[StructLayout(3)]
	[Token(Token = "0x200014A")]
	[CompilerGenerated]
	private struct _003CLoadAsync_003Ed__3
{
		[Token(Token = "0x4000657")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000658")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000659")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public DefaultLocalePresenter _003C_003E4__this;

		[Token(Token = "0x400065A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x400065B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600057E")]
		[Address(RVA = "0x38D3C0", Offset = "0x38BDC0", VA = "0x18038D3C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600057F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000656")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private readonly DefaultLocaleSelector _defaultLocaleSelector;

	// RECONSTRUCTED | Confidence: A | RVA: 0x38D0E0
	//
	// Native evidence:
	//   0x38D0ED  rbx = rcx (__this)
	//   0x38D0F2..0x38D125  one-time type-init guard for
	//             DefaultLocaleSelector_TypeInfo,
	//             MethodInfo ServiceLocator.Register<IDefaultLocaleSelector>(),
	//             ServiceLocator_TypeInfo
	//   0x38D12C  rcx = DefaultLocaleSelector_TypeInfo
	//   0x38D133  call il2cpp_codegen_object_new   -> `new DefaultLocaleSelector()`
	//   0x38D13F  [rbx + 0x10] = rax  ->  this._defaultLocaleSelector
	//             (offset 0x10 confirmed against dump.cs: the only instance field,
	//             declared `private readonly DefaultLocaleSelector`), followed by the
	//             GC write barrier at 0x38D143..0x38D17F (card table data_30D91C4).
	//   0x38D188  rbx = [rbx + 0x10]  ->  reload this._defaultLocaleSelector
	//   0x38D19A  rdx = MethodInfo ServiceLocator.Register<IDefaultLocaleSelector>()
	//   0x38D1A9  jmp -> GigaCreation.Tools.Service.ServiceLocator$$Register<object>
	//             tail call, rcx = the reloaded field.
	//             ServiceLocator.Register<TService>(TService service) (ServiceLocator.cs:62)
	//             is the matching overload, so the field is the argument.
	//
	// Note this is the exact structural twin of GameInitializer::Awake (RVA 0x30FA80):
	// same fold pattern, same ServiceLocator.Register target (0x180752130), different
	// field offset (0x10 vs 0x40).
	[Token(Token = "0x600057B")]
	[Address(RVA = "0x38D0E0", Offset = "0x38BAE0", VA = "0x18038D0E0")]
	public DefaultLocalePresenter()
	{
		_defaultLocaleSelector = new DefaultLocaleSelector();
		GigaCreation.Tools.Service.ServiceLocator.Register<IDefaultLocaleSelector>(_defaultLocaleSelector);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x38D1B0
	//
	// Native evidence:
	//   0x38D1C4..0x38D1E4  one-time type-init guard for
	//             MethodInfo ServiceLocator.Unregister<IDefaultLocaleSelector>()
	//             and ServiceLocator_TypeInfo
	//   0x38D1F2  rbx = [rbx + 0x10]  ->  this._defaultLocaleSelector
	//   0x38D1FF  call interface/virtual dispatch resolve (constraint check)
	//   0x38D204  rdx = MethodInfo ServiceLocator.Unregister<IDefaultLocaleSelector>()
	//   0x38D20B  rcx = rbx (the field)
	//   0x38D213  jmp -> GigaCreation.Tools.Service.ServiceLocator$$Unregister<object>
	//             (the same 0x180752790 target as GameInitializer::OnDestroy)
	//
	// Recovered behaviour: symmetric teardown of the constructor.
	[Token(Token = "0x600057C")]
	[Address(RVA = "0x38D1B0", Offset = "0x38BBB0", VA = "0x18038D1B0", Slot = "4")]
	public void Dispose()
	{
		GigaCreation.Tools.Service.ServiceLocator.Unregister<IDefaultLocaleSelector>(_defaultLocaleSelector);
	}

	[Token(Token = "0x600057D")]
	[Address(RVA = "0x38D220", Offset = "0x38BC20", VA = "0x18038D220")]
	[AsyncStateMachine(typeof(_003CLoadAsync_003Ed__3))]
	public UniTask LoadAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}
}
