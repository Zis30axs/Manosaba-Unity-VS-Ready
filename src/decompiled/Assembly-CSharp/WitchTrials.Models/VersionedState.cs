using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Serializable]
[Token(Token = "0x200019C")]
public class VersionedState
{
	[Token(Token = "0x400077D")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private List<IdVersionPair> _list;

	[Token(Token = "0x17000129")]
	public IReadOnlyCollection<IdVersionPair> ActiveItems
	{
		[Token(Token = "0x6000707")]
		[Address(RVA = "0x3A2300", Offset = "0x3A0D00", VA = "0x1803A2300")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000708")]
	[Address(RVA = "0x3A24F0", Offset = "0x3A0EF0", VA = "0x1803A24F0")]
	public VersionedState Clone()
	{
		return null;
	}

	[Token(Token = "0x6000709")]
	[Address(RVA = "0x3A2560", Offset = "0x3A0F60", VA = "0x1803A2560")]
	public void ResetToDefault()
	{
	}

	[Token(Token = "0x600070A")]
	[Address(RVA = "0x3A25B0", Offset = "0x3A0FB0", VA = "0x1803A25B0")]
	public void Copy(VersionedState from)
	{
	}

	[Token(Token = "0x600070B")]
	[Address(RVA = "0x3A2820", Offset = "0x3A1220", VA = "0x1803A2820")]
	public void SetVersion(string id, int version)
	{
	}

	[Token(Token = "0x600070C")]
	[Address(RVA = "0x3A2A40", Offset = "0x3A1440", VA = "0x1803A2A40")]
	public bool Validate(IdVersionPair pair)
	{
		return default(bool);
	}

	[Token(Token = "0x600070D")]
	[Address(RVA = "0x3A2AA0", Offset = "0x3A14A0", VA = "0x1803A2AA0")]
	public bool IsNull()
	{
		return default(bool);
	}

	[Token(Token = "0x600070E")]
	[Address(RVA = "0x3A2AB0", Offset = "0x3A14B0", VA = "0x1803A2AB0")]
	public VersionedState()
	{
	}
}
