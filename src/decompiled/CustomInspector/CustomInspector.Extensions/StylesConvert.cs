using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector.Extensions;

[Token(Token = "0x2000081")]
public static class StylesConvert
{
	[Token(Token = "0x60001F5")]
	[Address(RVA = "0x4407D0", Offset = "0x43F1D0", VA = "0x1804407D0")]
	public static Color ToColor(this FixedColor c)
	{
		return default(Color);
	}

	[Token(Token = "0x60001F6")]
	[Address(RVA = "0x440A20", Offset = "0x43F420", VA = "0x180440A20")]
	public static TextAnchor AlignmentToAnchor(TextAlignment textAlignment)
	{
		return default(TextAnchor);
	}

	[Token(Token = "0x60001F7")]
	[Address(RVA = "0x440AD0", Offset = "0x43F4D0", VA = "0x180440AD0")]
	public static InternalLabelStyle ToInteralStyle(this LabelStyle labelStyle)
	{
		return default(InternalLabelStyle);
	}

	[Token(Token = "0x60001F8")]
	[Address(RVA = "0x440BA0", Offset = "0x43F5A0", VA = "0x180440BA0")]
	public static string ToInternalIconName(this InspectorIcon icon)
	{
		return null;
	}

	[Token(Token = "0x60001F9")]
	[Address(RVA = "0x440C00", Offset = "0x43F600", VA = "0x180440C00")]
	public static string ToInternalIconName(string iconName)
	{
		return null;
	}

	[Token(Token = "0x60001FA")]
	[Address(RVA = "0x444670", Offset = "0x443070", VA = "0x180444670")]
	public static float ToButtonHeight(Size size)
	{
		return default(float);
	}

	[Token(Token = "0x60001FB")]
	[Address(RVA = "0x444750", Offset = "0x443150", VA = "0x180444750")]
	public static float ToButtonWidth(float availableWidth, GUIContent buttonLabel, Size size)
	{
		return default(float);
	}
}
