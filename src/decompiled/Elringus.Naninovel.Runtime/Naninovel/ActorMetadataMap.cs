using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x200001E")]
public abstract class ActorMetadataMap
{
	[Token(Token = "0x1700000C")]
	public abstract int Length
	{
		[Token(Token = "0x6000071")]
		get;
	}

	[Token(Token = "0x6000072")]
	public abstract ActorMetadata GetMetadata(string id);

	[Token(Token = "0x6000073")]
	public abstract List<string> GetAllIds();

	[Token(Token = "0x6000074")]
	public abstract int GetMetaIndex(string id);

	[Token(Token = "0x6000075")]
	public abstract bool ContainsId(string id);

	[Token(Token = "0x6000076")]
	public abstract void RemoveRecord(string id);

	[Token(Token = "0x6000077")]
	public abstract void RemoveAllRecords();

	[Token(Token = "0x6000078")]
	public abstract void MoveRecord(string sourceId, string destinationId);

	[Token(Token = "0x6000079")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	protected ActorMetadataMap()
	{
	}
}
[Serializable]
[Token(Token = "0x200001F")]
public abstract class ActorMetadataMap<TMeta> : ActorMetadataMap where TMeta : ActorMetadata
{
	[Token(Token = "0x4000087")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private string[] ids;

	[Token(Token = "0x4000088")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private TMeta[] metas;

	[Token(Token = "0x1700000D")]
	public override int Length
	{
		[Token(Token = "0x600007A")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x1700000E")]
	public TMeta this[string id]
	{
		[Token(Token = "0x600007D")]
		get
		{
			return null;
		}
		[Token(Token = "0x600007E")]
		set
		{
		}
	}

	[Token(Token = "0x600007B")]
	protected ActorMetadataMap()
	{
	}

	[Token(Token = "0x600007C")]
	protected ActorMetadataMap(IDictionary<string, TMeta> dictionary)
	{
	}

	[Token(Token = "0x600007F")]
	public Dictionary<string, TMeta> ToDictionary()
	{
		return null;
	}

	[Token(Token = "0x6000080")]
	public override ActorMetadata GetMetadata(string id)
	{
		return null;
	}

	[Token(Token = "0x6000081")]
	public override List<string> GetAllIds()
	{
		return null;
	}

	[Token(Token = "0x6000082")]
	public List<TMeta> GetAllMetas()
	{
		return null;
	}

	[Token(Token = "0x6000083")]
	public override int GetMetaIndex(string id)
	{
		return default(int);
	}

	[Token(Token = "0x6000084")]
	public override bool ContainsId(string id)
	{
		return default(bool);
	}

	[Token(Token = "0x6000085")]
	public TMeta GetMetaById(string id)
	{
		return null;
	}

	[Token(Token = "0x6000086")]
	public void AddRecord(string id, TMeta meta)
	{
	}

	[Token(Token = "0x6000087")]
	public override void RemoveRecord(string id)
	{
	}

	[Token(Token = "0x6000088")]
	public override void RemoveAllRecords()
	{
	}

	[Token(Token = "0x6000089")]
	public override void MoveRecord(string sourceId, string destinationId)
	{
	}
}
