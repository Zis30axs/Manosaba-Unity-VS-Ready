using System.Collections.Generic;
using System.Reflection;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000361")]
    public static class ExpressionFunctions
    {
    	[Token(Token = "0x600102C")]
    	[Address(RVA = "0x571E90", Offset = "0x570890", VA = "0x180571E90")]
    	[ExpressionFunction("random")]
    	public static int Random(int min, int max)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x600102D")]
    	[Address(RVA = "0x571EF0", Offset = "0x5708F0", VA = "0x180571EF0")]
    	[ExpressionFunction("random")]
    	public static double Random(double min, double max)
    	{
    		return default(double);
    	}

    	[Token(Token = "0x600102E")]
    	[Address(RVA = "0x571F10", Offset = "0x570910", VA = "0x180571F10")]
    	[ExpressionFunction("random")]
    	public static string Random(params string[] args)
    	{
    		return null;
    	}

    	[Token(Token = "0x600102F")]
    	[Address(RVA = "0x571F60", Offset = "0x570960", VA = "0x180571F60")]
    	[ExpressionFunction("calculateProgress")]
    	public static float CalculateProgress()
    	{
    		return default(float);
    	}

    	[Token(Token = "0x6001030")]
    	[Address(RVA = "0x572100", Offset = "0x570B00", VA = "0x180572100")]
    	[ExpressionFunction("isUnlocked")]
    	public static bool IsUnlocked([ResourceContext("Unlockables", -1, null)] string id)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001031")]
    	[Address(RVA = "0x5721A0", Offset = "0x570BA0", VA = "0x1805721A0")]
    	[ExpressionFunction("hasPlayed")]
    	public static bool HasPlayed()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001032")]
    	[Address(RVA = "0x5722C0", Offset = "0x570CC0", VA = "0x1805722C0")]
    	[ExpressionFunction("hasPlayed")]
    	public static bool HasPlayed([ResourceContext("Scripts", -1, null)] string scriptPath)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001033")]
    	[Address(RVA = "0x572360", Offset = "0x570D60", VA = "0x180572360")]
    	[ExpressionFunction("getName")]
    	public static string GetName([ActorContext("Characters", -1, null)] string characterId)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001034")]
    	[Address(RVA = "0x572400", Offset = "0x570E00", VA = "0x180572400")]
    	[ExpressionFunction("pow")]
    	public static double Pow(double num, double pow)
    	{
    		return default(double);
    	}

    	[Token(Token = "0x6001035")]
    	[Address(RVA = "0x572420", Offset = "0x570E20", VA = "0x180572420")]
    	[ExpressionFunction("sqrt")]
    	public static double Sqrt(double num)
    	{
    		return default(double);
    	}

    	[Token(Token = "0x6001036")]
    	[Address(RVA = "0x572450", Offset = "0x570E50", VA = "0x180572450")]
    	[ExpressionFunction("cos")]
    	public static double Cos(double num)
    	{
    		return default(double);
    	}

    	[Token(Token = "0x6001037")]
    	[Address(RVA = "0x572470", Offset = "0x570E70", VA = "0x180572470")]
    	[ExpressionFunction("sin")]
    	public static double Sin(double num)
    	{
    		return default(double);
    	}

    	[Token(Token = "0x6001038")]
    	[Address(RVA = "0x572490", Offset = "0x570E90", VA = "0x180572490")]
    	[ExpressionFunction("log")]
    	public static double Log(double num)
    	{
    		return default(double);
    	}

    	[Token(Token = "0x6001039")]
    	[Address(RVA = "0x5724B0", Offset = "0x570EB0", VA = "0x1805724B0")]
    	[ExpressionFunction("abs")]
    	public static double Abs(double num)
    	{
    		return default(double);
    	}

    	[Token(Token = "0x600103A")]
    	[Address(RVA = "0x5724D0", Offset = "0x570ED0", VA = "0x1805724D0")]
    	[ExpressionFunction("max")]
    	public static double Max(params double[] nums)
    	{
    		return default(double);
    	}

    	[Token(Token = "0x600103B")]
    	[Address(RVA = "0x572730", Offset = "0x571130", VA = "0x180572730")]
    	[ExpressionFunction("min")]
    	public static double Min(params double[] nums)
    	{
    		return default(double);
    	}

    	[Token(Token = "0x600103C")]
    	[Address(RVA = "0x572990", Offset = "0x571390", VA = "0x180572990")]
    	[ExpressionFunction("round")]
    	public static double Round(double num)
    	{
    		return default(double);
    	}

    	[Token(Token = "0x600103D")]
    	[Address(RVA = "0x5729B0", Offset = "0x5713B0", VA = "0x1805729B0")]
    	[ExpressionFunction("approx")]
    	public static bool Approximately(double a, double b)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600103E")]
    	[Address(RVA = "0x5729C0", Offset = "0x5713C0", VA = "0x1805729C0")]
    	public static List<ExpressionFunction> Resolve()
    	{
    		return null;
    	}

    	[Token(Token = "0x600103F")]
    	[Address(RVA = "0x572D10", Offset = "0x571710", VA = "0x180572D10")]
    	private static ExpressionFunction BuildFunction(MethodInfo method, ExpressionFunctionAttribute fn)
    	{
    		return default(ExpressionFunction);
    	}
    }
}
