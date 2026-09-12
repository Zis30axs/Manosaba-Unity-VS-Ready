using System;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Serializable]
[Token(Token = "0x2000062")]
public class SerializableInterface<T> where T : class
{
	[Token(Token = "0x400016A")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private MonoBehaviour serializedReference;

	[Token(Token = "0x400016B")]
	[FieldOffset(Offset = "0x0")]
	private T castedReference;

	[Token(Token = "0x17000020")]
	public T Value
	{
		[Token(Token = "0x600012A")]
		get
		{
			return null;
		}
		[Token(Token = "0x600012B")]
		set
		{
		}
	}

	[Token(Token = "0x600012C")]
	public SerializableInterface()
	{
	}

	[Token(Token = "0x600012D")]
	public SerializableInterface(MonoBehaviour value)
	{
	}

	[Token(Token = "0x600012E")]
	private void UnityEngine_002EISerializationCallbackReceiver_002EOnBeforeSerialize()
	{
	}

	[Token(Token = "0x600012F")]
	private void UnityEngine_002EISerializationCallbackReceiver_002EOnAfterDeserialize()
	{
	}
}
