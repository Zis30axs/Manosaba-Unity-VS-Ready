using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000471")]
public class GameStateMap : StateMap
{
	[Token(Token = "0x4001095")]
	private const string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";

	[Token(Token = "0x4001096")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private PlaybackSpot playbackSpot;

	[Token(Token = "0x4001097")]
	[FieldOffset(Offset = "0x40")]
	[SerializeField]
	private bool playerRollbackAllowed;

	[Token(Token = "0x4001098")]
	[FieldOffset(Offset = "0x41")]
	[SerializeField]
	private bool forcedSerialize;

	[Token(Token = "0x4001099")]
	[FieldOffset(Offset = "0x48")]
	[SerializeField]
	private string saveDateTime;

	[Token(Token = "0x400109A")]
	[FieldOffset(Offset = "0x50")]
	[SerializeField]
	private string thumbnailBase64;

	[Token(Token = "0x400109B")]
	[FieldOffset(Offset = "0x58")]
	[SerializeField]
	private string rollbackStackJson;

	[Token(Token = "0x17000308")]
	public PlaybackSpot PlaybackSpot
	{
		[Token(Token = "0x60015D7")]
		[Address(RVA = "0x468B10", Offset = "0x467510", VA = "0x180468B10")]
		get
		{
			return default(PlaybackSpot);
		}
		[Token(Token = "0x60015D8")]
		[Address(RVA = "0x5DA060", Offset = "0x5D8A60", VA = "0x1805DA060")]
		set
		{
		}
	}

	[Token(Token = "0x17000309")]
	public DateTime SaveDateTime
	{
		[Token(Token = "0x60015D9")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		[CompilerGenerated]
		get
		{
			return default(DateTime);
		}
		[Token(Token = "0x60015DA")]
		[Address(RVA = "0x5DA0C0", Offset = "0x5D8AC0", VA = "0x1805DA0C0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x1700030A")]
	public Texture2D Thumbnail
	{
		[Token(Token = "0x60015DB")]
		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x60015DC")]
		[Address(RVA = "0x486D40", Offset = "0x485740", VA = "0x180486D40")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x1700030B")]
	public bool PlayerRollbackAllowed
	{
		[Token(Token = "0x60015DD")]
		[Address(RVA = "0x456360", Offset = "0x454D60", VA = "0x180456360")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x60015DE")]
		[Address(RVA = "0x5DA0D0", Offset = "0x5D8AD0", VA = "0x1805DA0D0")]
		set
		{
		}
	}

	[Token(Token = "0x1700030C")]
	public bool ForcedSerialize
	{
		[Token(Token = "0x60015DF")]
		[Address(RVA = "0x5DA0E0", Offset = "0x5D8AE0", VA = "0x1805DA0E0")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x60015E0")]
		[Address(RVA = "0x5DA0F0", Offset = "0x5D8AF0", VA = "0x1805DA0F0")]
		set
		{
		}
	}

	[Token(Token = "0x1700030D")]
	public string RollbackStackJson
	{
		[Token(Token = "0x60015E1")]
		[Address(RVA = "0x453950", Offset = "0x452350", VA = "0x180453950")]
		get
		{
			return null;
		}
		[Token(Token = "0x60015E2")]
		[Address(RVA = "0x458180", Offset = "0x456B80", VA = "0x180458180")]
		set
		{
		}
	}

	[Token(Token = "0x60015E3")]
	[Address(RVA = "0x5DA100", Offset = "0x5D8B00", VA = "0x1805DA100")]
	public static GameStateMap With(params (object state, string id)[] records)
	{
		return null;
	}

	[Token(Token = "0x60015E4")]
	[Address(RVA = "0x5DA140", Offset = "0x5D8B40", VA = "0x1805DA140", Slot = "6")]
	public override void OnBeforeSerialize()
	{
	}

	[Token(Token = "0x60015E5")]
	[Address(RVA = "0x5DA4E0", Offset = "0x5D8EE0", VA = "0x1805DA4E0", Slot = "7")]
	public override void OnAfterDeserialize()
	{
	}

	[Token(Token = "0x60015E6")]
	[Address(RVA = "0x5DA780", Offset = "0x5D9180", VA = "0x1805DA780")]
	public void AllowPlayerRollback()
	{
	}

	[Token(Token = "0x60015E7")]
	[Address(RVA = "0x5DA790", Offset = "0x5D9190", VA = "0x1805DA790")]
	public void ForceSerialize()
	{
	}

	[Token(Token = "0x60015E8")]
	[Address(RVA = "0x5DA7A0", Offset = "0x5D91A0", VA = "0x1805DA7A0")]
	private Texture2D GetThumbnail()
	{
		return null;
	}

	[Token(Token = "0x60015E9")]
	[Address(RVA = "0x5DA960", Offset = "0x5D9360", VA = "0x1805DA960")]
	public GameStateMap()
	{
	}
}
