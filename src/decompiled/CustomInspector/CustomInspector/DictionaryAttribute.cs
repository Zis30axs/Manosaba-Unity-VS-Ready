using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000072")]
[Conditional("UNITY_EDITOR")]
public class DictionaryAttribute : PropertyAttribute
{
	[Token(Token = "0x400018B")]
	[FieldOffset(Offset = "0x18")]
	public string keyLabel;

	[Token(Token = "0x400018C")]
	[FieldOffset(Offset = "0x20")]
	public string valueLabel;

	[Token(Token = "0x400018D")]
	[FieldOffset(Offset = "0x28")]
	public readonly float keySize;

	[Token(Token = "0x400018E")]
	public const float defaultKeySize = 0.4f;

	[Token(Token = "0x600016F")]
	[Address(RVA = "0x4406F0", Offset = "0x43F0F0", VA = "0x1804406F0")]
	public DictionaryAttribute(float keySize = 0.4f)
	{
	}
}
