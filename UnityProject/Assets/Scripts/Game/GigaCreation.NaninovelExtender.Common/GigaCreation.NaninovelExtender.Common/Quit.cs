using UnityEngine;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Commands;

namespace GigaCreation.NaninovelExtender.Common
{
    [Token(Token = "0x2000009")]
    [UsedImplicitly]
    public class Quit : Stop
    {
    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF8EF0, 112 B -- Application.Quit() (0x181FDA9D0), then the Stop command's own
    	//   Execute (Naninovel.Commands.Stop$$Execute, 0x1804F4C50) with the token
    	// Annotated disassembly: work/title-path/disas/quit_01_Execute.txt
    	[Token(Token = "0x6000010")]
    	[Address(RVA = "0xCF8EF0", Offset = "0xCF78F0", VA = "0x180CF8EF0", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		Application.Quit();
    		return base.Execute(token);
    	}

    	[Token(Token = "0x6000011")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public Quit()
    	{
    	}
    }
}
