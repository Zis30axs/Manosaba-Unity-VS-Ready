using Il2CppDummyDll;
using Naninovel.UI;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x20000B8")]
public class ExecutionButtonUi : CustomUI, IExecutionButtonUi, IManagedUI
{
	[Token(Token = "0x400034B")]
	[FieldOffset(Offset = "0xE0")]
	[SerializeField]
	private ExecutionButtonScreen _executionButtonScreen;

	[Token(Token = "0x60002A2")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public ExecutionButtonUi()
	{
	}
}
