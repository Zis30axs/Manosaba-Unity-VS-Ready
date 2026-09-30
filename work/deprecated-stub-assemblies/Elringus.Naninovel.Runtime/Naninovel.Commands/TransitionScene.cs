using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Metadata;
using Naninovel.UI;
using UnityEngine;

namespace Naninovel.Commands
{
    [Token(Token = "0x200071E")]
    [CommandAlias("trans")]
    [RequireNested]
    [Branch(BranchTraits.Nest | BranchTraits.Return, null, null)]
    public class TransitionScene : Command, Command.INestedHost, Command.IPreloadable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200071F")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__12
    {
    		[Token(Token = "0x4001B2F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001B30")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001B31")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TransitionScene _003C_003E4__this;

    		[Token(Token = "0x4001B32")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001B33")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60021E4")]
    		[Address(RVA = "0x50E6E0", Offset = "0x50D0E0", VA = "0x18050E6E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60021E5")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000720")]
    	[CompilerGenerated]
    	private struct _003CFinishTransition_003Ed__14
    {
    		[Token(Token = "0x4001B34")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001B35")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001B36")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TransitionScene _003C_003E4__this;

    		[Token(Token = "0x4001B37")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001B38")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private IDisposable _003C__003E5__2;

    		[Token(Token = "0x4001B39")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60021E6")]
    		[Address(RVA = "0x50EA80", Offset = "0x50D480", VA = "0x18050EA80", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60021E7")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000721")]
    	[CompilerGenerated]
    	private struct _003CPreloadResources_003Ed__9
    {
    		[Token(Token = "0x4001B3A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001B3B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001B3C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TransitionScene _003C_003E4__this;

    		[Token(Token = "0x4001B3D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private ResourceRequest _003Cloader_003E5__2;

    		[Token(Token = "0x4001B3E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UnityAsyncExtensions.ResourceRequestAwaiter _003C_003Eu__1;

    		[Token(Token = "0x60021E8")]
    		[Address(RVA = "0x50F1B0", Offset = "0x50DBB0", VA = "0x18050F1B0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60021E9")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001B28")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[ConstantContext(typeof(TransitionType), -1, null)]
    	public StringParameter Transition;

    	[Token(Token = "0x4001B29")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterAlias("params")]
    	public DecimalListParameter TransitionParams;

    	[Token(Token = "0x4001B2A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[ParameterAlias("dissolve")]
    	public StringParameter DissolveTexturePath;

    	[Token(Token = "0x4001B2B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[ParameterAlias("easing")]
    	[ConstantContext(typeof(EasingType), -1, null)]
    	public StringParameter EasingTypeName;

    	[Token(Token = "0x4001B2C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[ParameterAlias("time")]
    	[ParameterDefaultValue("0.35")]
    	public DecimalParameter Duration;

    	[Token(Token = "0x4001B2D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private bool running;

    	[Token(Token = "0x4001B2E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	private Texture2D preloadedDissolveTexture;

    	[Token(Token = "0x170004F9")]
    	private IScriptPlayer player
    	{
    		[Token(Token = "0x60021D9")]
    		[Address(RVA = "0x50D8F0", Offset = "0x50C2F0", VA = "0x18050D8F0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60021DA")]
    	[Address(RVA = "0x50D950", Offset = "0x50C350", VA = "0x18050D950", Slot = "19")]
    	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__9))]
    	public virtual UniTask PreloadResources()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60021DB")]
    	[Address(RVA = "0x50DA80", Offset = "0x50C480", VA = "0x18050DA80", Slot = "20")]
    	public virtual void ReleaseResources()
    	{
    	}

    	[Token(Token = "0x60021DC")]
    	[Address(RVA = "0x50DAE0", Offset = "0x50C4E0", VA = "0x18050DAE0", Slot = "21")]
    	public virtual int GetNextPlaybackIndex(ScriptPlaylist playlist, int playedIndex)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x60021DD")]
    	[Address(RVA = "0x50DC70", Offset = "0x50C670", VA = "0x18050DC70", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__12))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60021DE")]
    	[Address(RVA = "0x50DDF0", Offset = "0x50C7F0", VA = "0x18050DDF0", Slot = "22")]
    	protected virtual UniTask StartTransition()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60021DF")]
    	[Address(RVA = "0x50DE60", Offset = "0x50C860", VA = "0x18050DE60", Slot = "23")]
    	[AsyncStateMachine(typeof(_003CFinishTransition_003Ed__14))]
    	protected virtual UniTask FinishTransition(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60021E0")]
    	[Address(RVA = "0x50DFE0", Offset = "0x50C9E0", VA = "0x18050DFE0", Slot = "24")]
    	protected virtual EasingType ResolveEasing()
    	{
    		return default(EasingType);
    	}

    	[Token(Token = "0x60021E1")]
    	[Address(RVA = "0x50E180", Offset = "0x50CB80", VA = "0x18050E180", Slot = "25")]
    	protected virtual Transition ResolveTransition()
    	{
    		return default(Transition);
    	}

    	[Token(Token = "0x60021E2")]
    	[Address(RVA = "0x50E590", Offset = "0x50CF90", VA = "0x18050E590", Slot = "26")]
    	protected virtual ISceneTransitionUI GetTransitionUI()
    	{
    		return null;
    	}

    	[Token(Token = "0x60021E3")]
    	[Address(RVA = "0x50E660", Offset = "0x50D060", VA = "0x18050E660")]
    	public TransitionScene()
    	{
    	}
    }
}
