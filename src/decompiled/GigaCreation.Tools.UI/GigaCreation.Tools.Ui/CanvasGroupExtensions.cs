using Il2CppDummyDll;
using JetBrains.Annotations;
using UnityEngine;

namespace GigaCreation.Tools.Ui;

[Token(Token = "0x2000004")]
[UsedImplicitly(ImplicitUseTargetFlags.Members)]
public static class CanvasGroupExtensions
{
	[Token(Token = "0x6000003")]
	[Address(RVA = "0xD53430", Offset = "0xD51E30", VA = "0x180D53430")]
	public static void Present(this CanvasGroup self)
	{
	}

	[Token(Token = "0x6000004")]
	[Address(RVA = "0xD53480", Offset = "0xD51E80", VA = "0x180D53480")]
	public static void Dismiss(this CanvasGroup self)
	{
	}

	[Token(Token = "0x6000005")]
	[Address(RVA = "0xD534C0", Offset = "0xD51EC0", VA = "0x180D534C0")]
	public static void Enable(this CanvasGroup self)
	{
	}

	[Token(Token = "0x6000006")]
	[Address(RVA = "0xD534F0", Offset = "0xD51EF0", VA = "0x180D534F0")]
	public static void Disable(this CanvasGroup self)
	{
	}
}
