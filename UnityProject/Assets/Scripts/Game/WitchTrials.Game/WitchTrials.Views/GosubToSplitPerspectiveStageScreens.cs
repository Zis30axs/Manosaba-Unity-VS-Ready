using System.Runtime.InteropServices;
using System.Text;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Commands;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000047")]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [CommandAlias("gosubSplitStageScreen")]
    public class GosubToSplitPerspectiveStageScreens : Command
    {
    	[Token(Token = "0x40000D2")]
    	private const string SubroutineScriptPath = "System/System_Subroutine";

    	[Token(Token = "0x40000D3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	public IntegerParameter Index;

    	[Token(Token = "0x40000D4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("look")]
    	[RequiredParameter]
    	public StringParameter LookCharacterId;

    	[Token(Token = "0x40000D5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("comp")]
    	public StringParameter Composition;

    	[Token(Token = "0x40000D6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("rollDir")]
    	public StringParameter RollDirection;

    	[Token(Token = "0x40000D7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("params")]
    	public StringListParameter LabelNameParams;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x31E250, 2338 B
    	//
    	// Confirms the calling-convention hypothesis recorded for
    	// `GosubToModifyPerspectiveStageCamera::Execute` (this same round,
    	// dump.cs priority table #25): same "read a per-character variable,
    	// build a subroutine-label suffix, republish two fixed-name
    	// variables, Gosub to `System/System_Subroutine`, don't forward
    	// `token`" shape -- `Assigned(...)`-guarded `Composition`/
    	// `RollDirection` with the same "Center"/"NoRoll" defaults (each
    	// re-verified independently, not assumed carried over: same
    	// `ICommandParameter.get_HasValue()` interface-dispatch guard, same
    	// `StringParameter.op_Implicit` re-fetch), the same
    	// `foreach (NullableString in LabelNameParams)` append loop, the same
    	// `Warn("Variable " + name + " does not exist.")` failure path, the
    	// same `NamedString`/`Gosub` construction, the same unforwarded
    	// `default(AsyncToken)` tail call. Three concrete differences from
    	// `GosubToModifyPerspectiveStageCamera`, each confirmed rather than
    	// assumed identical:
    	//   - This class has no `ZoomLevel` field -- confirmed from this
    	//     class's OWN field layout, not an unread dump.cs class body:
    	//     `LabelNameParams` sits at `+0x50` here (where
    	//     `GosubToModifyPerspectiveStageCamera.ZoomLevel` sat), and there
    	//     is no `"-Lvl"` + zoom-level append at all. The suffix is
    	//     `"SplitScreen-<comp>-<rollDir>[-<param>...]"`, not
    	//     `"ModifyCamera-...-Lvl<n>[-<param>...]"`.
    	//   - The per-character variable name uses `Naninovel.StringUtils.
    	//     FirstToLower(LookCharacterId)` (dump.cs confirms this exact
    	//     `FirstToLower(string)` overload; `LookCharacterId`'s implicit
    	//     `StringParameter -> string` conversion is what supplies the
    	//     `string` argument, confirmed by independently disassembling
    	//     `StringParameter.op_Implicit` and matching it byte-for-byte
    	//     against what's inlined here) instead of `LookCharacterId.Value.
    	//     ToLowerInvariant()` -- lowercasing only the first character
    	//     (e.g. "Ann" -> "annIdx", but "AnnSmith" -> "annSmithIdx" here
    	//     vs. "annsmithIdx" for `ModifyCamera`'s `ToLowerInvariant`), not
    	//     the whole string.
    	//   - Critically, THIS difference isn't just cosmetic: `Naninovel.
    	//     StringUtils.FirstToLower` is a plain static method that accepts
    	//     `null`, so there is NO throw-on-unassigned guard here at all --
    	//     `LookCharacterId.Value` (a `.Value` property access, which
    	//     `ModifyCamera` uses and which DOES throw NullReferenceException
    	//     when unassigned, per that class's own byte-verified
    	//     `.Value`-guard) is the WRONG read for this class: this
    	//     function's only throws are on the enumerator/Gosub paths
    	//     (`0x31EB5B`/`0x31EB61`/`0x31EB6D`), not on an unassigned
    	//     `LookCharacterId`. An unassigned `LookCharacterId` here silently
    	//     produces `"Idx"` as the variable name (empty first segment),
    	//     not a crash -- despite `LookCharacterId` being
    	//     `[RequiredParameter]` on both classes, only `ModifyCamera`
    	//     actually enforces that at the byte level for this particular
    	//     read.
    	//
    	// Checked against the shipped script corpus (`res/Assets/#WitchTrials/
    	// Scenarios/**/*.asset.json`): every `look:` value ever passed to
    	// either `gosubModifyCamera` or `gosubSplitStageScreen` across the
    	// whole corpus is `Ema` or `Sherry` -- both single-capital names, so
    	// `ToLowerInvariant()` and `FirstToLower()` happen to agree on every
    	// value actually used. The `ToLowerInvariant`/`FirstToLower`
    	// divergence is a real inconsistency between these two commands'
    	// implementations, but it is INERT in this game's shipped content,
    	// not a live bug.
    	[Token(Token = "0x60000EA")]
    	[Address(RVA = "0x31E250", Offset = "0x31CC50", VA = "0x18031E250", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		int index = ((int?)Index).Value;
    		string targetCharIdxVarName = StringUtils.FirstToLower(LookCharacterId) + "Idx";
    		ICustomVariableManager customVariableManager = Engine.GetServiceOrErr<ICustomVariableManager>();
    		if (!customVariableManager.TryGetVariableValue<int>(targetCharIdxVarName, out int targetCharIdx))
    		{
    			Warn("Variable " + targetCharIdxVarName + " does not exist.");
    			return default(UniTask);
    		}
    		StringBuilder stringBuilder = new StringBuilder("SplitScreen");
    		string composition = (Assigned(Composition) && !string.IsNullOrWhiteSpace(Composition.Value)) ? (string)Composition : "Center";
    		stringBuilder.Append("-").Append(composition);
    		string rollDirection = (Assigned(RollDirection) && !string.IsNullOrWhiteSpace(RollDirection.Value)) ? (string)RollDirection : "NoRoll";
    		stringBuilder.Append("-").Append(rollDirection);
    		if (Assigned(LabelNameParams))
    		{
    			foreach (NullableString labelNameParam in LabelNameParams)
    			{
    				string value = (labelNameParam != null && labelNameParam.HasValue) ? labelNameParam.Value : null;
    				stringBuilder.Append("-").Append(value);
    			}
    		}
    		customVariableManager.TrySetVariableValue("cameraIdx", index);
    		customVariableManager.TrySetVariableValue("targetCharIdx", targetCharIdx);
    		Gosub gosub = new Gosub
    		{
    			Path = new NamedString(SubroutineScriptPath, stringBuilder.ToString())
    		};
    		return gosub.Execute(default(AsyncToken));
    	}

    	[Token(Token = "0x60000EB")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public GosubToSplitPerspectiveStageScreens()
    	{
    	}
    }
}
