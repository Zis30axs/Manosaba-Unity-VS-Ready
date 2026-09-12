using System;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.NaninovelExtender.Ui;

[Token(Token = "0x2000012")]
public class ScriptNavigatorHeader : MonoBehaviour
{
	[Token(Token = "0x4000039")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private Button _syncButton;

	[Token(Token = "0x6000041")]
	[Address(RVA = "0x33A4B0", Offset = "0x338EB0", VA = "0x18033A4B0")]
	public IObservable<Unit> OnClickSyncButtonAsObservable()
	{
		return null;
	}

	[Token(Token = "0x6000042")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public ScriptNavigatorHeader()
	{
	}
}
