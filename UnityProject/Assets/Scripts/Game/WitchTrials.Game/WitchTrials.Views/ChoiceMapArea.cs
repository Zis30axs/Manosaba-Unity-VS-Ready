using FastEnumUtility;
using GigaCreation.NaninovelExtender.Ui;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000066")]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [CommandAlias("map")]
    public class ChoiceMapArea : ShowWitchBookUi
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000067")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__3
    {
    		[Token(Token = "0x4000156")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000157")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000158")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ChoiceMapArea _003C_003E4__this;

    		[Token(Token = "0x4000159")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x400015A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private IReadOnlyList<MapAreaKind> _003CrestrictionAreas_003E5__2;

    		[Token(Token = "0x400015B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400015C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private Naninovel.UniTask<MapAreaKind>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x400015D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__3;

    		[Token(Token = "0x600014B")]
    		[Address(RVA = "0x327440", Offset = "0x325E40", VA = "0x180327440", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600014C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000153")]
    	private const string ChosenAreaIdVarName = "chosenAreaId";

    	[Token(Token = "0x4000154")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[RequiredParameter]
    	[ParameterAlias("")]
    	public StringListParameter MapAreaNames;

    	[Token(Token = "0x4000155")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("flag")]
    	public StringParameter FlagVariableName;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x326EA0 (builder stub); MoveNext 0x327440
    	// 
    	// Same shape as ChoiceEvidence.Execute (`@evidence`), with a map-area choice in its place:
    	// - IWitchTrialsNaninovelManager slot 2 set_IsPresentEvidenceMode(true).
    	// - A one-element `WitchBookCategory[]` holding 2 (Map), created before
    	//   GetRestrictionAreas. The result is hoisted to +0x30 as `IReadOnlyList<MapAreaKind>`.
    	// - Count == 0 -> Command slot 7 `Warn(...)` and SetResult. This comes before
    	//   ShowWitchBook, so the book is never shown and IsPresentEvidenceMode stays true.
    	// - `ShowWitchBook(Local (edx=0), categories, restrictionAreas, token)`, awaited as Naninovel.UniTask.
    	// - IsEnabledDebugSkip ? `restrictionAreas[0]` (IReadOnlyList get_Item) : IWitchBookUi slot 5
    	//   `ChoiceAreaAsync(token)`, awaited as Naninovel.UniTask<MapAreaKind>.
    	// - `TrySetVariableValue<string>("chosenAreaId", FastToString)`; DismissAsync (Cysharp awaiter);
    	//   IsPresentEvidenceMode = false; debug skip -> `IScriptPlayer.SetSkipEnabled(true)`.
    	[Token(Token = "0x6000148")]
    	[Address(RVA = "0x326EA0", Offset = "0x3258A0", VA = "0x180326EA0", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__3))]
    	public override async Naninovel.UniTask Execute([Optional] AsyncToken token)
    	{
    		WitchTrialsNaninovelManager.IsPresentEvidenceMode = true;
    		WitchBookCategory[] categories = new WitchBookCategory[1] { WitchBookCategory.Map };
    		IReadOnlyList<MapAreaKind> restrictionAreas = GetRestrictionAreas();
    		if (restrictionAreas.Count == 0)
    		{
    			Warn("No map areas are available for the current flag.");
    			return;
    		}
    		await ShowWitchBook(WitchBookPresentationMode.Local, categories, restrictionAreas, token);
    		MapAreaKind chosenArea = IsEnabledDebugSkip ? restrictionAreas[0] : await WitchBookUi.ChoiceAreaAsync(token);
    		NaniVarManager.TrySetVariableValue(ChosenAreaIdVarName, chosenArea.FastToString());
    		await WitchBookUi.DismissAsync(null, token);
    		WitchTrialsNaninovelManager.IsPresentEvidenceMode = false;
    		if (IsEnabledDebugSkip)
    		{
    			Engine.GetServiceOrErr<IScriptPlayer>().SetSkipEnabled(true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (bit test / flag spelling: B) | RVA: 0x327030
    	// 
    	// - Capacity is `MapAreaNames.Length` inlined (HasValue slot 0x1C8 ? Value.Count : 0).
    	// - `flag` (rsp+0x60) is TryGetVariableValue<int> on the `FlagVariableName` string (inlined StringParameter
    	//   implicit), guarded by Assigned (ICommandParameter slot 0). It is zeroed on either failure.
    	// - Per index: the NullableString -> string implicit is inlined, then FastEnum.TryParse<MapAreaKind>
    	//   (TryParseInternal, ignoreCase false).
    	//   - Success: `test flag, 1 << i; jg skip` -> the area is added only when its bit is NOT set, so
    	//     the flag variable is a mask of areas to exclude.
    	//   - Failure: Warn(string.Format("Invalid map area name: {0}.", item)), where the format argument
    	//     is the raw NullableString object.
    	[Token(Token = "0x6000149")]
    	[Address(RVA = "0x327030", Offset = "0x325A30", VA = "0x180327030")]
    	private List<MapAreaKind> GetRestrictionAreas()
    	{
    		List<MapAreaKind> areas = new List<MapAreaKind>(MapAreaNames.Length);
    		int flag = Assigned(FlagVariableName) && NaniVarManager.TryGetVariableValue<int>(FlagVariableName, out int value) ? value : 0;
    		for (int i = 0; i < MapAreaNames.Length; i++)
    		{
    			if (FastEnum.TryParse<MapAreaKind>(MapAreaNames[i], out MapAreaKind areaKind))
    			{
    				if ((flag & (1 << i)) <= 0)
    				{
    					areas.Add(areaKind);
    				}
    			}
    			else
    			{
    				Warn(string.Format("Invalid map area name: {0}.", MapAreaNames[i]));
    			}
    		}
    		return areas;
    	}

    	[Token(Token = "0x600014A")]
    	[Address(RVA = "0x326000", Offset = "0x324A00", VA = "0x180326000")]
    	public ChoiceMapArea()
    	{
    	}
    }
}
