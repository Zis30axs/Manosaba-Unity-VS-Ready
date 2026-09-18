using System;
using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Audio
{
    [Serializable]
    [Token(Token = "0x2000008")]
    public sealed class ResponseSfx : ResponseSfxBase
    {
    	[Token(Token = "0x400000D")]
    	[FieldOffset(Offset = "0x20")]
    	[ResourcePopup("Audio")]
    	[SerializeField]
    	private string _path;

    	[Token(Token = "0x17000002")]
    	public string Path
    	{
    		// RECONSTRUCTED | Confidence: A (by field identity) | RVA: 0x377910, folded
    		//
    		// The dump body was `return null;`.  RVA 0x377910 sits in the identical-code-folding region (the
    		// same address Il2CppDumper reports for dozens of unrelated trivial getters), so the bytes do not
    		// name the field -- but the class declares exactly one string member, `_path` at +0x20
    		// (`[FieldOffset(Offset = "0x20")] [ResourcePopup("Audio")] [SerializeField] private string _path`),
    		// and `AudioManagerExtended.InitializeService` calls this getter as the element selector of a
    		// `ToDictionary<ResponseSfx, ResponseKind, string>` whose values are SFX resource paths.  A stub
    		// returning null would instead build a map of nulls (see the ToDictionary note in
    		// AudioManagerExtended.InitializeService), so the field read is the only body consistent with both.
    		[Token(Token = "0x600000F")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		get
    		{
    			return _path;
    		}
    	}

    	[Token(Token = "0x6000010")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public ResponseSfx()
    	{
    	}
    }
}
