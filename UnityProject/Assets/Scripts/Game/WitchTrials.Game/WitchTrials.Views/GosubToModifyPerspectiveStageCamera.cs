using System.Runtime.InteropServices;
using System.Text;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Commands;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000046")]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [CommandAlias("gosubModifyCamera")]
    public class GosubToModifyPerspectiveStageCamera : Command
    {
    	[Token(Token = "0x40000CB")]
    	private const string SubroutineScriptPath = "System/System_Subroutine";

    	[Token(Token = "0x40000CC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	public IntegerParameter Index;

    	[Token(Token = "0x40000CD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("look")]
    	[RequiredParameter]
    	public StringParameter LookCharacterId;

    	[Token(Token = "0x40000CE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("comp")]
    	public StringParameter Composition;

    	[Token(Token = "0x40000CF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("rollDir")]
    	public StringParameter RollDirection;

    	[Token(Token = "0x40000D0")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("zoomLvl")]
    	public IntegerParameter ZoomLevel;

    	[Token(Token = "0x40000D1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("params")]
    	public StringListParameter LabelNameParams;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x31D870, 2520 B
    	//
    	// NOT an async state machine -- there is no `[AsyncStateMachine]`
    	// attribute and no nested `<Execute>d__` struct on this class. The
    	// disassembly confirms this directly: everything runs synchronously
    	// and the method ends with a plain tail call, `return gosub.Execute(
    	// default(AsyncToken));` -- the incoming `token` parameter is never
    	// read anywhere in the function body and a freshly zeroed struct is
    	// built on the stack for the call instead (`xorps`+`movdqa` zeroing
    	// exactly 16 bytes, which dump.cs confirms is `AsyncToken`'s full
    	// size: two `CancellationToken` fields at 0x0/0x8, 8 bytes each --
    	// not a partial zero-fill of a larger struct), so this command's own
    	// cancellation token is silently NOT forwarded to the `Gosub` it
    	// dispatches to.
    	//
    	// The whole method computes a dynamic subroutine-label SUFFIX by
    	// string-building from its own parameters, publishes two script
    	// variables, then hands off execution to a freshly-constructed
    	// `Gosub` targeting the fixed script "System/System_Subroutine" with
    	// that computed label. Each parameter's extraction was checked against
    	// its own required/optional-ness (per the `[RequiredParameter]`
    	// attributes on this class) rather than assumed uniform:
    	//   - `Index`/`LookCharacterId` are both `[RequiredParameter]`, and
    	//     both are accessed as plain, unguarded conversions/`.Value` reads
    	//     with NO defensive null/HasValue branch in the source (the
    	//     `((int?)Index).Value`/two-step-cast shape is only there to
    	//     dodge the same `IntegerParameter`/`Nullable<int>` CS0457
    	//     ambiguity as `ModifyDebatePrinter`'s `NullableFloat` casts, not
    	//     because the disassembly shows an `if`) -- the
    	//     `test al,al; je NullReferenceException`-shaped checks seen at
    	//     the call sites are `Nullable<T>.Value`'s OWN internal
    	//     throw-if-unassigned guard, fully inlined (same "the guard is the
    	//     callee's, not the caller's" pattern established for `TMP_Text.
    	//     set_alpha` in `AddText` this same round), not a source-level
    	//     `if`.
    	//   - `Composition`/`RollDirection`/`ZoomLevel`/`LabelNameParams` are
    	//     all optional (no `[RequiredParameter]`), and each is guarded by
    	//     `Command.Assigned(...)` (the interface-dispatched
    	//     `ICommandParameter.get_HasValue()` shape established this round
    	//     in `ModifyDebatePrinter`) with an explicit fallback default,
    	//     confirmed by three literal defaults read from the data section:
    	//     "Center", "NoRoll", and `1` (int) respectively (`ZoomLevel`'s
    	//     `((int?)ZoomLevel).Value` is the same CS0457 dodge as `Index`
    	//     above, not an extra guard).
    	//   - `targetCharIdxVarName = LookCharacterId.Value.ToLowerInvariant()
    	//     + "Idx"` is looked up via
    	//     `CustomVariableManagerExtensions.TryGetVariableValue<int>` (an
    	//     extension method on `ICustomVariableManager`, dump.cs confirms
    	//     the exact `<TValue>(manager, name, out value)` overload); on
    	//     failure this calls `Warn(...)` (`Command.Warn`, dump.cs Slot 7,
    	//     same identification as `ModifyDebatePrinter`) with the exact
    	//     literal-concatenated message read from the data section and
    	//     returns `default(UniTask)` WITHOUT ever building or running the
    	//     `Gosub` -- i.e. a missing per-character index variable silently
    	//     no-ops the whole command.
    	//   - The `foreach (NullableString label in LabelNameParams)` loop
    	//     (only entered when `Assigned(LabelNameParams)`) unwraps each
    	//     item defensively (`item != null && item.HasValue ? item.Value :
    	//     null`, matching how `AddText`'s own X/Y `ElementAtOrNull`
    	//     pattern this round distinguished a real ternary from an inlined
    	//     guard -- this one IS a real ternary, since a null/absent item
    	//     yields `null` rather than throwing) and appends `"-" + value`
    	//     for each; the compiler's standard `foreach`-over-`IEnumerable<T>`
    	//     dispose-in-finally scaffolding is present but not represented
    	//     here since a plain `foreach` already guarantees it. Writing this
    	//     `foreach` at all required a THIRD infrastructure fix this round:
    	//     the reference stub `Naninovel/ParameterList.cs` declared its
    	//     class with no base interfaces at all (`ParameterList<TItem> :
    	//     CommandParameter<List<TItem>>`), while dump.cs shows the real
    	//     type as `..., IEnumerable<TItem>, IEnumerable` -- the same class
    	//     of gap as the `UniTask.Awaiter` fix earlier this round. Fixed by
    	//     adding the two interfaces to the class declaration and
    	//     converting its two mangled-name placeholder methods
    	//     (`System_002ECollections_002E...GetEnumerator`) into real
    	//     explicit interface implementations (`IEnumerable.GetEnumerator`/
    	//     `IEnumerable<TItem>.GetEnumerator`) with the same bodies.
    	//   - After the optional-params section (which always runs regardless
    	//     of whether the loop above executed), `cameraIdx` is published as
    	//     `index` and `targetCharIdx` is re-published as the SAME value
    	//     just read for the lookup above -- i.e. the per-character-named
    	//     variable ("<lowercased look>Idx") is read once, then re-exposed
    	//     under two FIXED names the "ModifyCamera..." subroutine reads
    	//     directly, since that subroutine has no way to know which
    	//     specific character was targeted.
    	//   - `new Gosub { Path = new NamedString(SubroutineScriptPath,
    	//     stringBuilder.ToString()) }`: the allocated object is a
    	//     `Naninovel.NamedString` (dump.cs `TYPE Naninovel.NamedString_TypeInfo`
    	//     at the `il2cpp_codegen_object_new` call), NOT a raw
    	//     `Named<NullableString>` -- confirmed by independently
    	//     disassembling `NamedString.ctor(string name, string value)`
    	//     (RVA 0x525920) and finding it byte-for-byte identical (same
    	//     `data_30DBD5C` class-init flag, same `NullableString.op_Implicit`
    	//     call, same tail call into `Named<NullableString>..ctor`) to what
    	//     is inlined here. `NamedString` converts to `NamedStringParameter`
    	//     via its own `implicit operator` (dump.cs), so the assignment to
    	//     `Gosub.Path` needs no cast. `Gosub.Path` (dump.cs,
    	//     `NamedStringParameter`, `[RequiredParameter]`) confirms the
    	//     field; the object-initializer
    	//     ordering (allocate + base ctor first, THEN evaluate `ToString()`
    	//     and build the `Named<NullableString>`, THEN assign `.Path`) is an
    	//     exact match for how C# compiles `new T { Prop = expr }` overall,
    	//     not `new T(); t.Prop = expr;` written separately (both compile
    	//     the same, but this is the natural source form). `Gosub.Execute`
    	//     is dump.cs Slot 5 (`Naninovel.Commands.Gosub`, its own override
    	//     of `Command.Execute`) -- independently confirmed from dump.cs,
    	//     not inferred from any other class's Slot 5 (`Goto.Execute` being
    	//     Slot 5 too, from earlier this round, is simply the same base
    	//     `Command.Execute` position, not corroborating evidence for this
    	//     unrelated subclass).
    	[Token(Token = "0x60000E8")]
    	[Address(RVA = "0x31D870", Offset = "0x31C270", VA = "0x18031D870", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		int index = ((int?)Index).Value;
    		string targetCharIdxVarName = LookCharacterId.Value.ToLowerInvariant() + "Idx";
    		ICustomVariableManager customVariableManager = Engine.GetServiceOrErr<ICustomVariableManager>();
    		if (!customVariableManager.TryGetVariableValue<int>(targetCharIdxVarName, out int targetCharIdx))
    		{
    			Warn("Variable " + targetCharIdxVarName + " does not exist.");
    			return default(UniTask);
    		}
    		StringBuilder stringBuilder = new StringBuilder("ModifyCamera");
    		string composition = (Assigned(Composition) && !string.IsNullOrWhiteSpace(Composition.Value)) ? (string)Composition : "Center";
    		stringBuilder.Append("-").Append(composition);
    		string rollDirection = (Assigned(RollDirection) && !string.IsNullOrWhiteSpace(RollDirection.Value)) ? (string)RollDirection : "NoRoll";
    		stringBuilder.Append("-").Append(rollDirection);
    		stringBuilder.Append("-Lvl");
    		int zoomLevel = Assigned(ZoomLevel) ? ((int?)ZoomLevel).Value : 1;
    		stringBuilder.Append(zoomLevel);
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

    	[Token(Token = "0x60000E9")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public GosubToModifyPerspectiveStageCamera()
    	{
    	}
    }
}
