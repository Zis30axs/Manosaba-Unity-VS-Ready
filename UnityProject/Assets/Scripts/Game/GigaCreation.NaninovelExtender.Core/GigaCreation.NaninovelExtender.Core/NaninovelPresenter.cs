using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Core
{
    [Token(Token = "0x200000A")]
    public class NaninovelPresenter : MonoBehaviour
    {
    	[Token(Token = "0x4000017")]
    	[FieldOffset(Offset = "0x20")]
    	private INaninovelManager _naninovelManager;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD03A20, 206 B -- _naninovelManager (+0x20) = new NaninovelManager(); tail jump
    	//   ServiceLocator.Register<INaninovelManager>(_naninovelManager).  (WitchTrialsNaninovelPresenter overrides this
    	//   without calling base, so the shipped game registers its own manager instead.)
    	// Annotated disassembly: work/title-path/disas/np_0xD03A20.txt
    	[Token(Token = "0x6000014")]
    	[Address(RVA = "0xD03A20", Offset = "0xD02420", VA = "0x180D03A20", Slot = "4")]
    	protected virtual void Awake()
    	{
    		_naninovelManager = new NaninovelManager();
    		ServiceLocator.Register(_naninovelManager);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD03AF0, 104 B -- tail jump ServiceLocator.Unregister<INaninovelManager>(_naninovelManager)
    	// Annotated disassembly: work/title-path/disas/np_0xD03AF0.txt
    	[Token(Token = "0x6000015")]
    	[Address(RVA = "0xD03AF0", Offset = "0xD024F0", VA = "0x180D03AF0", Slot = "5")]
    	protected virtual void OnDestroy()
    	{
    		ServiceLocator.Unregister(_naninovelManager);
    	}

    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public NaninovelPresenter()
    	{
    	}
    }
}
