using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Commands;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x2000032")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
public class StopForChoice : Stop
{
	[Token(Token = "0x4000089")]
	private const string EnableDebugSkipVarName = "enableDebugSkip";

	[Token(Token = "0x400008A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly string[] s_alwaysHandleChoiceIds;

	[Token(Token = "0x400008B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
	private static List<PlaybackSpot> s_chosenChoiceStates;

	[Token(Token = "0x400008C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterDefaultValue("false")]
	public BooleanParameter Clear;

	[Token(Token = "0x60000AF")]
	[Address(RVA = "0x318730", Offset = "0x317130", VA = "0x180318730", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000B0")]
	[Address(RVA = "0x3190A0", Offset = "0x317AA0", VA = "0x1803190A0")]
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void ResetStaticFields()
	{
	}

	[Token(Token = "0x60000B1")]
	[Address(RVA = "0x319190", Offset = "0x317B90", VA = "0x180319190")]
	public StopForChoice()
	{
	}
}
