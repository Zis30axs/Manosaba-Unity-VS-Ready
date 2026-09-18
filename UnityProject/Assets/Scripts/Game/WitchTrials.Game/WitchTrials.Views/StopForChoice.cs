using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Commands;
using UnityEngine;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000032")]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class StopForChoice : Stop
    {
    	[Token(Token = "0x4000089")]
    	private const string EnableDebugSkipVarName = "enableDebugSkip";

    	[Token(Token = "0x400008A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	// RECONSTRUCTED | Confidence: A | RVA: 0x319200 (.cctor, 268 B): the only static initializer
    	private static readonly string[] s_alwaysHandleChoiceIds = new string[2] { "0101Trial14_Choice003", "0102Trial14_Choice005" };

    	[Token(Token = "0x400008B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    	private static List<PlaybackSpot> s_chosenChoiceStates;

    	[Token(Token = "0x400008C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterDefaultValue("false")]
    	public BooleanParameter Clear = false;

    	// RECONSTRUCTED | Confidence: A (behaviour), B (the `?.EndsWith` tests written with explicit null checks) | RVA: 0x318730, 2409 B;
    	// <>c lambdas b__4_0 0x3193B0 (IActor slot 3 `Visible`), b__4_1 0x319400
    	// 
    	// Not async. `clear` resets the remembered choices. Otherwise, only with the `enableDebugSkip` custom variable, the
    	// first visible choice handler (IActorManager slot 0 `Actors`) auto-picks its first choice that is neither a `...Bad`
    	// button (ChoiceState.buttonPath, +0x30) nor already chosen (hostedAt, +0x08, remembered in s_chosenChoiceStates).
    	// The spot is remembered unless it is a `...Cancel` button or its summary (+0x20) carries one of the always-handle ids.
    	// IChoiceHandlerActor slot 0 `Choices`, slot 3 `HandleChoice`; Command slot 6 `Log`, slot 7 `Warn`. The debug path
    	// returns without the base Stop; `Summary.Parts[0].Id` throws for plain text (inlined LocalizableTextPart.Id).
    	[Token(Token = "0x60000AF")]
    	[Address(RVA = "0x318730", Offset = "0x317130", VA = "0x180318730", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		if (Clear)
    		{
    			s_chosenChoiceStates.Clear();
    			return UniTask.CompletedTask;
    		}
    		if (Engine.GetServiceOrErr<ICustomVariableManager>().TryGetVariableValue<bool>(EnableDebugSkipVarName, out bool enableDebugSkip) && enableDebugSkip)
    		{
    			IChoiceHandlerActor choiceHandler = Engine.GetServiceOrErr<IChoiceHandlerManager>().Actors.FirstOrDefault((IChoiceHandlerActor actor) => actor.Visible);
    			if (choiceHandler != null)
    			{
    				foreach (ChoiceState choice in choiceHandler.Choices)
    				{
    					if ((choice.ButtonPath != null && choice.ButtonPath.EndsWith("Bad")) || s_chosenChoiceStates.Contains(choice.HostedAt))
    					{
    						continue;
    					}
    					bool isCancel = choice.ButtonPath != null && choice.ButtonPath.EndsWith("Cancel");
    					if (!isCancel && !choice.Summary.Parts.Any((LocalizableTextPart part) => s_alwaysHandleChoiceIds.Contains(part.Id)))
    					{
    						s_chosenChoiceStates.Add(choice.HostedAt);
    					}
    					choiceHandler.HandleChoice(choice.Id);
    					Log("Auto choice: #" + choice.Summary.Parts[0].Id);
    					break;
    				}
    				return UniTask.CompletedTask;
    			}
    			Warn("@stopForChoice コマンドが実行されましたが、選択肢が表示されていません。");
    		}
    		return base.Execute(token);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3190A0, 231 B (runs at SubsystemRegistration; the list has no static initializer)
    	[Token(Token = "0x60000B0")]
    	[Address(RVA = "0x3190A0", Offset = "0x317AA0", VA = "0x1803190A0")]
    	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    	private static void ResetStaticFields()
    	{
    		s_chosenChoiceStates = new List<PlaybackSpot>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x319190, 112 B (`Clear = false` field initializer, then Command..ctor)
    	[Token(Token = "0x60000B1")]
    	[Address(RVA = "0x319190", Offset = "0x317B90", VA = "0x180319190")]
    	public StopForChoice()
    	{
    	}
    }
}
