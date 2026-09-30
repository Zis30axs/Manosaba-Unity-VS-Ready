using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x2000715")]
    public abstract class SpawnEffect : Command, Command.IPreloadable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000716")]
    	[CompilerGenerated]
    	private struct _003CPreloadResources_003Ed__8
    {
    		[Token(Token = "0x4001B0B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001B0C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001B0D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public SpawnEffect _003C_003E4__this;

    		[Token(Token = "0x4001B0E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60021B8")]
    		[Address(RVA = "0x50C3E0", Offset = "0x50ADE0", VA = "0x18050C3E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60021B9")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001B09")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	public BooleanParameter Wait;

    	[Token(Token = "0x170004ED")]
    	protected abstract string Path
    	{
    		[Token(Token = "0x60021A7")]
    		get;
    	}

    	[Token(Token = "0x170004EE")]
    	protected virtual bool DestroyWhen
    	{
    		[Token(Token = "0x60021A8")]
    		[Address(RVA = "0x43D680", Offset = "0x43C080", VA = "0x18043D680", Slot = "19")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170004EF")]
    	protected virtual ISpawnManager SpawnManager
    	{
    		[Token(Token = "0x60021A9")]
    		[Address(RVA = "0x50B870", Offset = "0x50A270", VA = "0x18050B870", Slot = "20")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60021AA")]
    	[Address(RVA = "0x50B8D0", Offset = "0x50A2D0", VA = "0x18050B8D0", Slot = "21")]
    	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__8))]
    	public virtual UniTask PreloadResources()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60021AB")]
    	[Address(RVA = "0x50B9F0", Offset = "0x50A3F0", VA = "0x18050B9F0", Slot = "22")]
    	public virtual void ReleaseResources()
    	{
    	}

    	[Token(Token = "0x60021AC")]
    	[Address(RVA = "0x50BA80", Offset = "0x50A480", VA = "0x18050BA80", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60021AD")]
    	protected abstract StringListParameter GetSpawnParameters();

    	[Token(Token = "0x60021AE")]
    	[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "24")]
    	protected virtual StringListParameter GetDestroyParameters()
    	{
    		return null;
    	}

    	[Token(Token = "0x60021AF")]
    	[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "25")]
    	protected virtual DecimalListParameter GetScenePosition()
    	{
    		return null;
    	}

    	[Token(Token = "0x60021B0")]
    	[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "26")]
    	protected virtual DecimalListParameter GetPosition()
    	{
    		return null;
    	}

    	[Token(Token = "0x60021B1")]
    	[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "27")]
    	protected virtual DecimalListParameter GetRotation()
    	{
    		return null;
    	}

    	[Token(Token = "0x60021B2")]
    	[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "28")]
    	protected virtual DecimalListParameter GetScale()
    	{
    		return null;
    	}

    	[Token(Token = "0x60021B3")]
    	[Address(RVA = "0x50C020", Offset = "0x50AA20", VA = "0x18050C020", Slot = "29")]
    	protected virtual string ToSpawnParam(StringParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x60021B4")]
    	[Address(RVA = "0x50C0B0", Offset = "0x50AAB0", VA = "0x18050C0B0", Slot = "30")]
    	protected virtual string ToSpawnParam(IntegerParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x60021B5")]
    	[Address(RVA = "0x50C1D0", Offset = "0x50ABD0", VA = "0x18050C1D0", Slot = "31")]
    	protected virtual string ToSpawnParam(DecimalParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x60021B6")]
    	[Address(RVA = "0x50C2F0", Offset = "0x50ACF0", VA = "0x18050C2F0", Slot = "32")]
    	protected virtual string ToSpawnParam(BooleanParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x60021B7")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	protected SpawnEffect()
    	{
    	}
    }
}
