using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x2000061")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("badId")]
public class SetBadId : Command
{
	[Token(Token = "0x4000143")]
	private const string CommonBadEndIdVarName = "commonBadEndId";

	[Token(Token = "0x4000144")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	[RequiredParameter]
	public IntegerParameter CommonBadEndId;

	[Token(Token = "0x4000145")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private ICustomVariableManager _naniVarManager;

	[Token(Token = "0x1700001C")]
	private ICustomVariableManager NaniVarManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x325A40, 186 B (`??= Engine.GetServiceOrErr<T>()`, +0x38)
		[Token(Token = "0x600013B")]
		[Address(RVA = "0x325A40", Offset = "0x324440", VA = "0x180325A40")]
		get
		{
			return _naniVarManager ??= Engine.GetServiceOrErr<ICustomVariableManager>();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x325B00, 622 B (getter inlined; `(uint)(id - 1) > 12` range test; Command slot 7 `Warn`)
	// Valid ids 1..13 are stored zero-padded ("01".."13") as a string variable.
	[Token(Token = "0x600013C")]
	[Address(RVA = "0x325B00", Offset = "0x324500", VA = "0x180325B00", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		int commonBadEndId = CommonBadEndId;
		if (commonBadEndId >= 1 && commonBadEndId <= 13)
		{
			NaniVarManager.TrySetVariableValue(CommonBadEndIdVarName, string.Format("{0:D2}", commonBadEndId));
		}
		else
		{
			Warn(string.Format("共通バッドエンド ID が不正です：{0}", commonBadEndId));
		}
		return UniTask.CompletedTask;
	}

	[Token(Token = "0x600013D")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public SetBadId()
	{
	}
}
