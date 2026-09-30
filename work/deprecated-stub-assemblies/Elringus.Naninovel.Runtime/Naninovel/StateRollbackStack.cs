using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000495")]
    public class StateRollbackStack
    {
    	[Serializable]
    	[Token(Token = "0x2000496")]
    	private class SerializedStack
    	{
    		[Token(Token = "0x4001140")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public List<GameStateMap> List;

    		[Token(Token = "0x60016BB")]
    		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    		public SerializedStack(List<GameStateMap> list)
    		{
    		}
    	}

    	[Token(Token = "0x400113D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	public readonly int Capacity;

    	[Token(Token = "0x400113E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly LinkedList<GameStateMap> rollbackList;

    	[Token(Token = "0x400113F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private readonly Action<GameStateMap> onDrop;

    	[Token(Token = "0x17000330")]
    	public int Count
    	{
    		[Token(Token = "0x60016AE")]
    		[Address(RVA = "0x5E7880", Offset = "0x5E6280", VA = "0x1805E7880")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x60016AF")]
    	[Address(RVA = "0x5E78D0", Offset = "0x5E62D0", VA = "0x1805E78D0")]
    	public StateRollbackStack(int capacity, [Optional] Action<GameStateMap> onDrop)
    	{
    	}

    	[Token(Token = "0x60016B0")]
    	[Address(RVA = "0x5E79E0", Offset = "0x5E63E0", VA = "0x1805E79E0", Slot = "4")]
    	public IEnumerator<GameStateMap> GetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x60016B1")]
    	[Address(RVA = "0x5E7A80", Offset = "0x5E6480", VA = "0x1805E7A80", Slot = "5")]
    	private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x60016B2")]
    	[Address(RVA = "0x5E7B20", Offset = "0x5E6520", VA = "0x1805E7B20")]
    	public void Push(GameStateMap item)
    	{
    	}

    	[Token(Token = "0x60016B3")]
    	[Address(RVA = "0x5E7C60", Offset = "0x5E6660", VA = "0x1805E7C60")]
    	public GameStateMap Peek()
    	{
    		return null;
    	}

    	[Token(Token = "0x60016B4")]
    	[Address(RVA = "0x5E7CE0", Offset = "0x5E66E0", VA = "0x1805E7CE0")]
    	public GameStateMap Pop()
    	{
    		return null;
    	}

    	[Token(Token = "0x60016B5")]
    	[Address(RVA = "0x5E7DB0", Offset = "0x5E67B0", VA = "0x1805E7DB0")]
    	public GameStateMap Pop(Predicate<GameStateMap> predicate)
    	{
    		return null;
    	}

    	[Token(Token = "0x60016B6")]
    	[Address(RVA = "0x5E7EB0", Offset = "0x5E68B0", VA = "0x1805E7EB0")]
    	public bool Contains(Predicate<GameStateMap> predicate)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60016B7")]
    	[Address(RVA = "0x5E7F70", Offset = "0x5E6970", VA = "0x1805E7F70")]
    	public void Clear()
    	{
    	}

    	[Token(Token = "0x60016B8")]
    	[Address(RVA = "0x5E7FC0", Offset = "0x5E69C0", VA = "0x1805E7FC0")]
    	public void ForEach(Action<GameStateMap> action)
    	{
    	}

    	[Token(Token = "0x60016B9")]
    	[Address(RVA = "0x5E8100", Offset = "0x5E6B00", VA = "0x1805E8100")]
    	public void OverrideFromJson(string json, [Optional] Action<GameStateMap> @foreach)
    	{
    	}

    	[Token(Token = "0x60016BA")]
    	[Address(RVA = "0x5E82E0", Offset = "0x5E6CE0", VA = "0x1805E82E0")]
    	public string ToJson(int maxSize, [Optional] Predicate<GameStateMap> filter)
    	{
    		return null;
    	}
    }
}
