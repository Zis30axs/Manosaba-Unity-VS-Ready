using System;
using System.Collections;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000345")]
public interface IEngineBehaviour
{
	[Token(Token = "0x1400004D")]
	event Action OnBehaviourUpdate;

	[Token(Token = "0x1400004E")]
	event Action OnBehaviourLateUpdate;

	[Token(Token = "0x1400004F")]
	event Action OnBehaviourDestroy;

	[Token(Token = "0x6000FC5")]
	GameObject GetRootObject();

	[Token(Token = "0x6000FC6")]
	void AddChildObject(GameObject obj);

	[Token(Token = "0x6000FC7")]
	void Destroy();

	[Token(Token = "0x6000FC8")]
	Coroutine StartCoroutine(IEnumerator routine);

	[Token(Token = "0x6000FC9")]
	void StopCoroutine(Coroutine routine);
}
