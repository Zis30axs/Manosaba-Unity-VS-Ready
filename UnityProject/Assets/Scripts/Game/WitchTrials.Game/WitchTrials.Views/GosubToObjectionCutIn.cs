using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Commands;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000060")]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [CommandAlias("gosubCutIn")]
    public class GosubToObjectionCutIn : Command
    {
    	[Token(Token = "0x400013D")]
    	private const string SubroutineScriptPath = "System/System_Subroutine";

    	[Token(Token = "0x400013E")]
    	private const string ShowObjectionCutInLabelName = "EndDebate-CutIn";

    	[Token(Token = "0x400013F")]
    	private const string ObjectionSpawnPathVarName = "objectionCutInSpawnPath";

    	[Token(Token = "0x4000140")]
    	private const string ObjectionIndexVarName = "objectionCutInIndex";

    	[Token(Token = "0x4000141")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	public StringParameter CutInKind;

    	[Token(Token = "0x4000142")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[RequiredParameter]
    	public IntegerParameter Index;

    	// NEW IN THE 2026-10-02 GAME UPDATE.  The shipped `gosubCutIn` commands serialise these four
    	// fields, but this checkout's pre-update reconstruction declares neither, so the incremental sync
    	// rejected every script that uses them ("VoicePath: no serialised field") and retained the old asset.
    	// TODO(evidence): the updated build also rebases this type onto `AudioCommand` and adds
    	// `Command.IPreloadable.PreloadResources()` plus a `HasVoice` property; those are not reconstructed
    	// here, and the RVAs recorded in this file still belong to the pre-update layout.
    	[Token(Token = "0x400014F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	public StringParameter VoicePath;

    	[Token(Token = "0x4000150")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	public DecimalParameter Volume;

    	[Token(Token = "0x4000151")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	public StringParameter GroupPath;

    	[Token(Token = "0x4000152")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	public StringParameter AuthorId;

    	// RECONSTRUCTED | Confidence: A (structure), B (Index's exact nullable typing)
    	// RVA: 0x325750
    	//
    	// Native evidence:
    	//   - Engine.GetServiceOrErr<ICustomVariableManager>()
    	//   - TrySetVariableValue(ObjectionSpawnPathVarName, string.Format("ObjectionCutIn_{0}", CutInKind))
    	//     -- the format string "ObjectionCutIn_{0}" is confirmed directly (STR literal);
    	//     CutInKind is passed as the object argument to string.Format, which calls its ToString().
    	//   - TrySetVariableValue(ObjectionIndexVarName, <Index as int?>) -- Confidence B on the
    	//     exact type: disassembly clearly shows IntegerParameter -> Nullable<int>.op_Implicit,
    	//     but the METHODINFO for the TrySetVariableValue call itself displays generic arg
    	//     `<int>` rather than `<Nullable<int>>`, which is not fully resolved here.
    	//   - `new Gosub { Path = new NamedString(SubroutineScriptPath, ShowObjectionCutInLabelName) }`:
    	//     the object_new(NamedString) followed immediately by a direct call to the *base*
    	//     Named<NullableString> constructor (not NamedString's own 2-string-arg overload) is
    	//     that constructor's own trivial `: base(name, value)` body inlined at the call site --
    	//     same "small ctor gets inlined" pattern documented in earlier commits.
    	//   - `gosub.Execute(token)` is a raw vtable dispatch through Gosub's own overridden
    	//     Execute(AsyncToken) -- called with a freshly zeroed AsyncToken rather than the
    	//     original `token` parameter (which this method never otherwise reads), hence `default`
    	//     below rather than forwarding `token`.
    	[Token(Token = "0x6000139")]
    	[Address(RVA = "0x325750", Offset = "0x324150", VA = "0x180325750", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		ICustomVariableManager customVariableManager = Engine.GetServiceOrErr<ICustomVariableManager>();
    		customVariableManager.TrySetVariableValue(ObjectionSpawnPathVarName, string.Format("ObjectionCutIn_{0}", CutInKind));
    		customVariableManager.TrySetVariableValue(ObjectionIndexVarName, (int?)Index);
    		Gosub gosub = new Gosub
    		{
    			Path = new NamedString(SubroutineScriptPath, ShowObjectionCutInLabelName)
    		};
    		return gosub.Execute(default(AsyncToken));
    	}

    	[Token(Token = "0x600013A")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public GosubToObjectionCutIn()
    	{
    	}
    }
}
