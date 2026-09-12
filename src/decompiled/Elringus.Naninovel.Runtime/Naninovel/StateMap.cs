using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000494")]
public class StateMap : ISerializationCallbackReceiver
{
	[Token(Token = "0x400113B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	protected readonly Dictionary<string, object> ObjectMap;

	[Token(Token = "0x400113C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	[SerializeField]
	private SerializableLiteralStringMap objectJsonMap;

	[Token(Token = "0x60016A8")]
	public static TMap With<TMap>(params (object state, string id)[] records) where TMap : StateMap, new()
	{
		return null;
	}

	[Token(Token = "0x60016A9")]
	[Address(RVA = "0x5E7140", Offset = "0x5E5B40", VA = "0x1805E7140", Slot = "6")]
	public virtual void OnBeforeSerialize()
	{
	}

	[Token(Token = "0x60016AA")]
	[Address(RVA = "0x5E7380", Offset = "0x5E5D80", VA = "0x1805E7380", Slot = "7")]
	public virtual void OnAfterDeserialize()
	{
	}

	[Token(Token = "0x60016AB")]
	public void SetState<TState>(TState state, [Optional] string instanceId) where TState : class, new()
	{
	}

	[Token(Token = "0x60016AC")]
	public TState GetState<TState>([Optional] string instanceId) where TState : class, new()
	{
		return null;
	}

	[Token(Token = "0x60016AD")]
	[Address(RVA = "0x5E76C0", Offset = "0x5E60C0", VA = "0x1805E76C0")]
	public StateMap()
	{
	}
}
