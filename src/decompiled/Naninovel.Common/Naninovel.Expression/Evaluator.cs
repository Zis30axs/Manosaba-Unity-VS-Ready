using Il2CppDummyDll;

namespace Naninovel.Expression;

[Token(Token = "0x2000093")]
public class Evaluator
{
	[Token(Token = "0x40001E8")]
	[FieldOffset(Offset = "0x10")]
	private readonly ResolveVariable resolveVar;

	[Token(Token = "0x40001E9")]
	[FieldOffset(Offset = "0x18")]
	private readonly ResolveFunction resolveFn;

	[Token(Token = "0x600034A")]
	[Address(RVA = "0x36C8D0", Offset = "0x36B2D0", VA = "0x18036C8D0")]
	public Evaluator(EvaluateOptions options)
	{
	}

	[Token(Token = "0x600034B")]
	public TResult Evaluate<TResult>(IExpression exp) where TResult : notnull
	{
		return default(TResult);
	}

	[Token(Token = "0x600034C")]
	[Address(RVA = "0x10F6BD0", Offset = "0x10F55D0", VA = "0x1810F6BD0")]
	public IOperand Evaluate(IExpression exp)
	{
		return null;
	}
}
