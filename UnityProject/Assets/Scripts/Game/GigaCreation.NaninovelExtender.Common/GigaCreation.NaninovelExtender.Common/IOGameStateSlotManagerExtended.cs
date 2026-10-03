using System.IO;
using System.Linq;
using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Common
{
    // NEW IN THE 2026-10-02 GAME UPDATE; see ISaveSlotManagerExtended.cs for why its absence stopped
    // Naninovel from initializing at all.  Base class and field/member layout come from the updated
    // dump (work/il2cpp_out/dump.cs: TypeDefIndex 17007); the three member bodies were recovered with
    // capstone disassembly (work/slot-disas/*.txt) and mirror the idiom of the shipped
    // `Naninovel.IOGameStateSlotManager` base
    // (UnityProject/Packages/com.elringus.naninovel/Runtime/State/IOGameStateSlotManager.cs).
    [Token(Token = "0x2000026")]
    public class IOGameStateSlotManagerExtended : IOGameStateSlotManager, ISaveSlotManagerExtended<GameStateMap>, ISaveSlotManager<GameStateMap>, ISaveSlotManager
    {
    	[Token(Token = "0x400007B")]
    	private readonly string _autoSavePattern;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD05860, 361 B | Slot 42
    	//
    	// Native evidence (work/slot-disas/getautosaveslotnames.txt): the receiver is the inlined-in-spirit
    	// `SaveDataPath` virtual (`mov rax,[rdx+0x388]`), then `_autoSavePattern` (+0x80) is passed as the
    	// search pattern to `System.IO.Directory.GetFiles` (0xD058F8) with the two remaining argument
    	// registers zeroed (= the `SearchOption.TopDirectoryOnly` default of the 4-argument overload, exactly
    	// as `IOGameStateSlotManager.AnySaveExists` calls it), and the result goes through the cached
    	// `<>O.<0>__GetFileNameWithoutExtension` lambda via `Enumerable.Select` + `Enumerable.ToArray`.
    	[Token(Token = "0x1700000C")]
    	public string[] AutoSaveSlotNames
    	{
    		[Token(Token = "0x6000060")]
    		[Address(RVA = "0xD05860", Offset = "0xD04060", VA = "0x180D05860", Slot = "42")]
    		get
    		{
    			return Directory.GetFiles(SaveDataPath, _autoSavePattern).Select(Path.GetFileNameWithoutExtension).ToArray();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD05A40, 328 B
     //
    	// Native evidence (work/slot-disas/ctor.txt): base ctor first (call 0x1805E0920 =
    	// `Naninovel.IOGameStateSlotManager..ctor`, with rdx = config / r8 = savesFolderPath forwarded),
    	// then a null check on config (0xD05A9B `test rbx,rbx` / `je` to the throw helper) and
    	// `_autoSavePattern (+0x80) = string.Format(config.QuickSaveSlotMask (+0x38), "*") + "." + <virtual>`
    	// (0xD05AEA String.FormatHelper with the format string read from +0x38, 0xD05B06 a vtable call at
    	// +0x338 -- `Extension` -- and 0xD05B19 String.Concat).  This is byte-for-byte the base class's own
    	// `string.Format(config.XxxSlotMask, "*") + $".{Extension}"` pattern.
    	[Token(Token = "0x6000061")]
    	[Address(RVA = "0xD05A40", Offset = "0xD04240", VA = "0x180D05A40")]
    	public IOGameStateSlotManagerExtended(StateConfiguration config, string savesFolderPath)
    		: base(config, savesFolderPath)
    	{
    		_autoSavePattern = string.Format(config.QuickSaveSlotMask, "*") + $".{Extension}";
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD05B90, 305 B | Slot 43
     //
    	// Native evidence (work/slot-disas/ensurecapacity.txt): `Directory.Exists(SaveDataPath)` bails out
    	// (0xD05C1F, `je` to the epilogue), then `Directory.GetFiles(SaveDataPath, _autoSavePattern,
    	// SearchOption.TopDirectoryOnly)`; the length check against the `capacity` argument (`cmp
    	// dword ptr [rax+0x18], esi` / `jl` at 0xD05C73) returns when there is room, otherwise the cached
    	// `<>O.<1>__GetFileName` lambda orders the *full paths* by file name (`Enumerable.OrderBy`, 0xD05CEC),
    	// `Enumerable.First` takes the oldest and `System.IO.File.Delete` (0xD05D0C) removes it.  Auto-save
    	// slot ids are the '{0:yyMMddHHmmssfff}' timestamps the state manager formats, so ordering by name
    	// is ordering by age.
    	[Token(Token = "0x6000062")]
    	[Address(RVA = "0xD05B90", Offset = "0xD04390", VA = "0x180D05B90", Slot = "43")]
    	public void EnsureCapacityForAutoSaveSlot(int capacity)
    	{
    		if (!Directory.Exists(SaveDataPath)) return;

    		string[] files = Directory.GetFiles(SaveDataPath, _autoSavePattern, SearchOption.TopDirectoryOnly);
    		if (files.Length < capacity) return;

    		File.Delete(files.OrderBy(Path.GetFileName).First());
    	}
    }
}
