using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace GigaCreation.NaninovelExtender.Common
{
    [Token(Token = "0x200000B")]
    [UsedImplicitly]
    [CommandAlias("setFlag")]
    public class SetCustomVariableAsFlag : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200000C")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__9
    {
    		[Token(Token = "0x4000020")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000021")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000022")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public SetCustomVariableAsFlag _003C_003E4__this;

    		[Token(Token = "0x4000023")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000017")]
    		[Address(RVA = "0xCF93B0", Offset = "0xCF7DB0", VA = "0x180CF93B0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000018")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000D")]
    	[CompilerGenerated]
    	private struct _003CSetAndSaveFlagAsync_003Ed__10
    {
    		[Token(Token = "0x4000024")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000025")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000026")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public SetCustomVariableAsFlag _003C_003E4__this;

    		[Token(Token = "0x4000027")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string variableName;

    		[Token(Token = "0x4000028")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public int bit;

    		[Token(Token = "0x4000029")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    		public BitwiseOperator op;

    		[Token(Token = "0x400002A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000019")]
    		[Address(RVA = "0xCF97B0", Offset = "0xCF81B0", VA = "0x180CF97B0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600001A")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400001B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly]
    	[RequiredParameter]
    	[ParameterAlias("")]
    	public IntegerParameter Number;

    	[Token(Token = "0x400001C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[UsedImplicitly]
    	[ParameterAlias("var")]
    	public StringParameter VariableName;

    	[Token(Token = "0x400001D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[UsedImplicitly]
    	[ParameterAlias("op")]
    	public StringParameter BitwiseOp;

    	[Token(Token = "0x400001E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private IStateManager _naniStateManager;

    	[Token(Token = "0x400001F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private ICustomVariableManager _naniVarManager;

    	[Token(Token = "0x17000001")]
    	protected IStateManager NaniStateManager
    	{
    		[Token(Token = "0x6000012")]
    		[Address(RVA = "0xCF8F60", Offset = "0xCF7960", VA = "0x180CF8F60")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000002")]
    	protected ICustomVariableManager NaniVarManager
    	{
    		[Token(Token = "0x6000013")]
    		[Address(RVA = "0xCF9020", Offset = "0xCF7A20", VA = "0x180CF9020")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000014")]
    	[Address(RVA = "0xCF90E0", Offset = "0xCF7AE0", VA = "0x180CF90E0", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__9))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000015")]
    	[Address(RVA = "0xCF9200", Offset = "0xCF7C00", VA = "0x180CF9200")]
    	[AsyncStateMachine(typeof(_003CSetAndSaveFlagAsync_003Ed__10))]
    	protected UniTask SetAndSaveFlagAsync(string variableName, int bit, BitwiseOperator op)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public SetCustomVariableAsFlag()
    	{
    	}
    }
}
