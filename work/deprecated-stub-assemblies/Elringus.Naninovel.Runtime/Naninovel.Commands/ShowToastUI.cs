using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x2000704")]
    [CommandAlias("toast")]
    public class ShowToastUI : Command, Command.IPreloadable, Command.ILocalizable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000705")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__5
    {
    		[Token(Token = "0x4001AD5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001AD6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001AD7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ShowToastUI _003C_003E4__this;

    		[Token(Token = "0x4001AD8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask<System.IDisposable>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6002178")]
    		[Address(RVA = "0x506AF0", Offset = "0x5054F0", VA = "0x180506AF0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6002179")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001AD2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	public LocalizableTextParameter Text;

    	[Token(Token = "0x4001AD3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	public StringParameter Appearance;

    	[Token(Token = "0x4001AD4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[ParameterAlias("time")]
    	public DecimalParameter Duration;

    	[Token(Token = "0x6002174")]
    	[Address(RVA = "0x506850", Offset = "0x505250", VA = "0x180506850", Slot = "16")]
    	public UniTask PreloadResources()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002175")]
    	[Address(RVA = "0x506910", Offset = "0x505310", VA = "0x180506910", Slot = "17")]
    	public void ReleaseResources()
    	{
    	}

    	[Token(Token = "0x6002176")]
    	[Address(RVA = "0x5069D0", Offset = "0x5053D0", VA = "0x1805069D0", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__5))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002177")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public ShowToastUI()
    	{
    	}
    }
}
