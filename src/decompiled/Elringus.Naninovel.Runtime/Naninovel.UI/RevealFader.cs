using System.Collections.Generic;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x20005B0")]
public class RevealFader : TextRevealEffect
{
	[Token(Token = "0x40015DA")]
	[FieldOffset(Offset = "0x28")]
	[Tooltip("How long to stretch fade gradient, by character.")]
	[Range(0f, 100f)]
	[SerializeField]
	private float length;

	[Token(Token = "0x40015DB")]
	[FieldOffset(Offset = "0x2C")]
	[Tooltip("When below 1, will modify opacity of the text before the last character from which reveal started.")]
	[Range(0f, 1f)]
	[SerializeField]
	private float slackOpacity;

	[Token(Token = "0x40015DC")]
	[FieldOffset(Offset = "0x30")]
	[Tooltip("Duration (in seconds) of fading slack text to the target opacity.")]
	[Range(0f, 3f)]
	[SerializeField]
	private float slackDuration;

	[Token(Token = "0x40015DD")]
	[FieldOffset(Offset = "0x34")]
	[Tooltip("Whether to respect <alpha> tags or otherwise custom opacity levels assigned to specific text characters. When enabled, text reveal effect will limit max opacity to the specified level. Be aware, that this involves additional work inside the reveal loop and affects performance.")]
	[SerializeField]
	private bool supportCustomOpacity;

	[Token(Token = "0x40015DE")]
	[FieldOffset(Offset = "0x38")]
	private readonly Dictionary<int, byte> customOpacity;

	[Token(Token = "0x6001C55")]
	[Address(RVA = "0x49F790", Offset = "0x49E190", VA = "0x18049F790")]
	private void OnEnable()
	{
	}

	[Token(Token = "0x6001C56")]
	[Address(RVA = "0x49F900", Offset = "0x49E300", VA = "0x18049F900")]
	private void OnDisable()
	{
	}

	[Token(Token = "0x6001C57")]
	[Address(RVA = "0x49FB30", Offset = "0x49E530", VA = "0x18049FB30")]
	private void Update()
	{
	}

	[Token(Token = "0x6001C58")]
	[Address(RVA = "0x49FDD0", Offset = "0x49E7D0", VA = "0x18049FDD0")]
	private void FadeCharacter(TMP_CharacterInfo info, byte opacity)
	{
	}

	[Token(Token = "0x6001C59")]
	[Address(RVA = "0x4A0090", Offset = "0x49EA90", VA = "0x1804A0090")]
	private byte EvaluateOpacity(int charIndex)
	{
		return default(byte);
	}

	[Token(Token = "0x6001C5A")]
	[Address(RVA = "0x4A03B0", Offset = "0x49EDB0", VA = "0x1804A03B0")]
	private bool IsSlack(int charIndex)
	{
		return default(bool);
	}

	[Token(Token = "0x6001C5B")]
	[Address(RVA = "0x4A0440", Offset = "0x49EE40", VA = "0x1804A0440")]
	private void HandleTextChanged(Object obj)
	{
	}

	[Token(Token = "0x6001C5C")]
	[Address(RVA = "0x4A0590", Offset = "0x49EF90", VA = "0x1804A0590")]
	private void ResolveCustomOpacity()
	{
	}

	[Token(Token = "0x6001C5D")]
	[Address(RVA = "0x4A0830", Offset = "0x49F230", VA = "0x1804A0830")]
	public RevealFader()
	{
	}
}
