using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Metadata;

namespace Naninovel.Commands;

[Token(Token = "0x20006C6")]
[Branch(BranchTraits.Endpoint, null, null)]
public class Goto : Command
{
	[Token(Token = "0x20006C7")]
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class DontResetAttribute : Attribute
	{
		[Token(Token = "0x60020B0")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public DontResetAttribute()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20006C9")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__15
{
		[Token(Token = "0x40019E4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40019E5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40019E6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public Goto _003C_003E4__this;

		[Token(Token = "0x40019E7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40019E8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private string _003CscriptPath_003E5__2;

		[Token(Token = "0x40019E9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private string _003Clabel_003E5__3;

		[Token(Token = "0x40019EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private IDisposable _003C_003E7__wrap3;

		[Token(Token = "0x40019EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask<IDisposable>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40019EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60020B3")]
		[Address(RVA = "0x4F2410", Offset = "0x4F0E10", VA = "0x1804F2410", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60020B4")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20006CA")]
	[CompilerGenerated]
	private struct _003CNavigateOtherScript_003Ed__20
{
		[Token(Token = "0x40019ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40019EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40019EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public string label;

		[Token(Token = "0x40019F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Goto _003C_003E4__this;

		[Token(Token = "0x40019F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string scriptPath;

		[Token(Token = "0x40019F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60020B5")]
		[Address(RVA = "0x4F2E70", Offset = "0x4F1870", VA = "0x1804F2E70", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60020B6")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40019DB")]
	public const string ResetAllFlag = "*";

	[Token(Token = "0x40019DC")]
	public const string NoResetFlag = "-";

	[Token(Token = "0x40019DD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[RequiredParameter]
	[EndpointContext(null)]
	public NamedStringParameter Path;

	[Token(Token = "0x40019DE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterAlias("reset")]
	public StringListParameter ResetState;

	[Token(Token = "0x40019DF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	public BooleanParameter Hold;

	[Token(Token = "0x40019E0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	public BooleanParameter Release;

	[Token(Token = "0x170004C8")]
	protected IScriptManager Scripts
	{
		[Token(Token = "0x60020A3")]
		[Address(RVA = "0x4F0F60", Offset = "0x4EF960", VA = "0x1804F0F60")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004C9")]
	protected IScriptPlayer Player
	{
		[Token(Token = "0x60020A4")]
		[Address(RVA = "0x4F0FC0", Offset = "0x4EF9C0", VA = "0x1804F0FC0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004CA")]
	protected IScriptLoader Loader
	{
		[Token(Token = "0x60020A5")]
		[Address(RVA = "0x4F1020", Offset = "0x4EFA20", VA = "0x1804F1020")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004CB")]
	protected ResourcePolicy Policy
	{
		[Token(Token = "0x60020A6")]
		[Address(RVA = "0x4F1080", Offset = "0x4EFA80", VA = "0x1804F1080")]
		get
		{
			return default(ResourcePolicy);
		}
	}

	[Token(Token = "0x60020A7")]
	[Address(RVA = "0x4F10F0", Offset = "0x4EFAF0", VA = "0x1804F10F0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__15))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60020A8")]
	[Address(RVA = "0x4F1280", Offset = "0x4EFC80", VA = "0x1804F1280", Slot = "16")]
	protected virtual bool TryGetScriptPathAndLabel(out string scriptPath, [MaybeNull] out string label)
	{
		scriptPath = default;
		label = default;
		scriptPath = default;
		label = default;
		return default(bool);
	}

	[Token(Token = "0x60020A9")]
	[Address(RVA = "0x4F1650", Offset = "0x4F0050", VA = "0x1804F1650", Slot = "17")]
	protected virtual bool ShouldNavigatePlayedScript(string scriptPath)
	{
		return default(bool);
	}

	[Token(Token = "0x60020AA")]
	[Address(RVA = "0x4F1880", Offset = "0x4F0280", VA = "0x1804F1880", Slot = "18")]
	protected virtual void NavigatePlayedScript([MaybeNull] string label)
	{
	}

	[Token(Token = "0x60020AB")]
	[Address(RVA = "0x4F1B10", Offset = "0x4F0510", VA = "0x1804F1B10", Slot = "19")]
	protected virtual bool ShouldMaskWithLoadingScreen()
	{
		return default(bool);
	}

	[Token(Token = "0x60020AC")]
	[Address(RVA = "0x4F1D20", Offset = "0x4F0720", VA = "0x1804F1D20", Slot = "20")]
	[AsyncStateMachine(typeof(_003CNavigateOtherScript_003Ed__20))]
	protected virtual UniTask NavigateOtherScript(string scriptPath, [MaybeNull] string label)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60020AD")]
	[Address(RVA = "0x4F1F00", Offset = "0x4F0900", VA = "0x1804F1F00", Slot = "21")]
	protected virtual bool ShouldResetAll()
	{
		return default(bool);
	}

	[Token(Token = "0x60020AE")]
	[Address(RVA = "0x4F20E0", Offset = "0x4F0AE0", VA = "0x1804F20E0", Slot = "22")]
	protected virtual bool ShouldResetSpecific()
	{
		return default(bool);
	}

	[Token(Token = "0x60020AF")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public Goto()
	{
	}
}
