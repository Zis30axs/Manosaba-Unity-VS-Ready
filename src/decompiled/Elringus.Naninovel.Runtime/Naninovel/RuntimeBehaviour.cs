using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x200034C")]
public class RuntimeBehaviour : MonoBehaviour, IEngineBehaviour
{
	[Token(Token = "0x4000C4E")]
	[FieldOffset(Offset = "0x38")]
	private GameObject rootObject;

	[Token(Token = "0x4000C4F")]
	[FieldOffset(Offset = "0x40")]
	private MonoBehaviour monoBehaviour;

	[Token(Token = "0x14000050")]
	public event Action OnBehaviourUpdate
	{
		[Token(Token = "0x6000FD8")]
		[Address(RVA = "0x569D80", Offset = "0x568780", VA = "0x180569D80", Slot = "4")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000FD9")]
		[Address(RVA = "0x569E70", Offset = "0x568870", VA = "0x180569E70", Slot = "5")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x14000051")]
	public event Action OnBehaviourLateUpdate
	{
		[Token(Token = "0x6000FDA")]
		[Address(RVA = "0x569F60", Offset = "0x568960", VA = "0x180569F60", Slot = "6")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000FDB")]
		[Address(RVA = "0x56A050", Offset = "0x568A50", VA = "0x18056A050", Slot = "7")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x14000052")]
	public event Action OnBehaviourDestroy
	{
		[Token(Token = "0x6000FDC")]
		[Address(RVA = "0x56A140", Offset = "0x568B40", VA = "0x18056A140", Slot = "8")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000FDD")]
		[Address(RVA = "0x56A230", Offset = "0x568C30", VA = "0x18056A230", Slot = "9")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6000FDE")]
	[Address(RVA = "0x56A320", Offset = "0x568D20", VA = "0x18056A320")]
	public static RuntimeBehaviour Create(bool dontDestroyOnLoad = true)
	{
		return null;
	}

	[Token(Token = "0x6000FDF")]
	[Address(RVA = "0x347410", Offset = "0x345E10", VA = "0x180347410", Slot = "10")]
	public GameObject GetRootObject()
	{
		return null;
	}

	[Token(Token = "0x6000FE0")]
	[Address(RVA = "0x56A4F0", Offset = "0x568EF0", VA = "0x18056A4F0", Slot = "11")]
	public void AddChildObject(GameObject obj)
	{
	}

	[Token(Token = "0x6000FE1")]
	[Address(RVA = "0x56A560", Offset = "0x568F60", VA = "0x18056A560", Slot = "12")]
	public void Destroy()
	{
	}

	[Token(Token = "0x6000FE2")]
	[Address(RVA = "0x56A750", Offset = "0x569150", VA = "0x18056A750")]
	private void Update()
	{
	}

	[Token(Token = "0x6000FE3")]
	[Address(RVA = "0x56A770", Offset = "0x569170", VA = "0x18056A770")]
	private void LateUpdate()
	{
	}

	[Token(Token = "0x6000FE4")]
	[Address(RVA = "0x56A790", Offset = "0x569190", VA = "0x18056A790")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000FE5")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public RuntimeBehaviour()
	{
	}

	[Token(Token = "0x6000FE6")]
	[Address(RVA = "0x56A7B0", Offset = "0x5691B0", VA = "0x18056A7B0", Slot = "13")]
	private Coroutine Naninovel_002EIEngineBehaviour_002EStartCoroutine(IEnumerator routine)
	{
		return null;
	}

	[Token(Token = "0x6000FE7")]
	[Address(RVA = "0x56A7C0", Offset = "0x5691C0", VA = "0x18056A7C0", Slot = "14")]
	private void Naninovel_002EIEngineBehaviour_002EStopCoroutine(Coroutine routine)
	{
	}
}
