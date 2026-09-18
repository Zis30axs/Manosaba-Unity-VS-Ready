using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x200003E")]
    public class Underlay : MonoBehaviour
    {
    	[Token(Token = "0x4000105")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private Graphic _graphic;

    	[Token(Token = "0x4000106")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Button _button;

    	[Token(Token = "0x6000120")]
    	[Address(RVA = "0xCEA0D0", Offset = "0xCE8AD0", VA = "0x180CEA0D0")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x6000121")]
    	[Address(RVA = "0xCEA200", Offset = "0xCE8C00", VA = "0x180CEA200")]
    	public void SetRaycastTargetEnabled(bool enable)
    	{
    	}

    	[Token(Token = "0x6000122")]
    	[Address(RVA = "0xCEA300", Offset = "0xCE8D00", VA = "0x180CEA300")]
    	public void SetInteractable(bool enable)
    	{
    	}

    	[Token(Token = "0x6000123")]
    	[Address(RVA = "0xCEA400", Offset = "0xCE8E00", VA = "0x180CEA400")]
    	public bool TryGetClickTask(out UniTask task, [Optional] CancellationToken ct)
    	{
    		task = default;
    		task = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6000124")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public Underlay()
    	{
    	}
    }
}
