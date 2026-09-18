using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.General
{
    [Token(Token = "0x2000005")]
    public class DestroySelfIfAlreadyExists : MonoBehaviour
    {
    	[Token(Token = "0x2000006")]
    	private enum DestroyTarget
    	{
    		[Token(Token = "0x400000A")]
    		Component,
    		[Token(Token = "0x400000B")]
    		GameObject
    	}

    	[Token(Token = "0x4000006")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private Component _targetComponent;

    	[Token(Token = "0x4000007")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private DestroyTarget _destroyTarget;

    	[Token(Token = "0x17000001")]
    	private bool WillBeDestroyed
    	{
    		[Token(Token = "0x6000004")]
    		[Address(RVA = "0x4B99C0", Offset = "0x4B83C0", VA = "0x1804B99C0")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000005")]
    		[Address(RVA = "0x4B99D0", Offset = "0x4B83D0", VA = "0x1804B99D0")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0xD49980", Offset = "0xD48380", VA = "0x180D49980")]
    	private void Awake()
    	{
    	}

    	[Token(Token = "0x6000007")]
    	[Address(RVA = "0xD49EB0", Offset = "0xD488B0", VA = "0x180D49EB0")]
    	private void OnValidate()
    	{
    	}

    	[Token(Token = "0x6000008")]
    	[Address(RVA = "0xD4A140", Offset = "0xD48B40", VA = "0x180D4A140")]
    	private void DestroySelf()
    	{
    	}

    	[Token(Token = "0x6000009")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DestroySelfIfAlreadyExists()
    	{
    	}
    }
}
