using System;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WitchTrials.Views;

[Token(Token = "0x2000011")]
public sealed class PointerStayingPropagator : MonoBehaviour
{
	[Serializable]
	[Token(Token = "0x2000012")]
	public class BlockList
	{
		[Token(Token = "0x400001F")]
		[FieldOffset(Offset = "0x10")]
		[SerializeField]
		public IgnoreSetting[] IgnoreSettings;

		[Token(Token = "0x6000037")]
		[Address(RVA = "0x30E290", Offset = "0x30CC90", VA = "0x18030E290")]
		public bool CanPropagate(IPointerStaying owner, IPointerStaying target)
		{
			return default(bool);
		}

		[Token(Token = "0x6000038")]
		[Address(RVA = "0x30E500", Offset = "0x30CF00", VA = "0x18030E500")]
		public BlockList()
		{
		}
	}

	[Serializable]
	[Token(Token = "0x2000014")]
	public class IgnoreSetting
	{
		[Token(Token = "0x4000021")]
		[FieldOffset(Offset = "0x10")]
		[SerializeField]
		private PointerStayingSpriteSwapper _owner;

		[Token(Token = "0x4000022")]
		[FieldOffset(Offset = "0x18")]
		[SerializeField]
		private PointerStayingSpriteSwapper[] _ignores;

		[Token(Token = "0x600003B")]
		[Address(RVA = "0x30E6D0", Offset = "0x30D0D0", VA = "0x18030E6D0")]
		public bool IsOwner(PointerStayingSpriteSwapper owner)
		{
			return default(bool);
		}

		[Token(Token = "0x600003C")]
		[Address(RVA = "0x30E810", Offset = "0x30D210", VA = "0x18030E810")]
		public bool CanPropagate(PointerStayingSpriteSwapper target)
		{
			return default(bool);
		}

		[Token(Token = "0x600003D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public IgnoreSetting()
		{
		}
	}

	[Token(Token = "0x400001D")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private BlockList _blockList;

	[Token(Token = "0x400001E")]
	[FieldOffset(Offset = "0x28")]
	private IPointerStaying[] _pointerStayings;

	[Token(Token = "0x6000032")]
	[Address(RVA = "0x30DDB0", Offset = "0x30C7B0", VA = "0x18030DDB0")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000033")]
	[Address(RVA = "0x30DF70", Offset = "0x30C970", VA = "0x18030DF70")]
	public void OnPointerEnter(PointerEventData eventData, IPointerStaying owner)
	{
	}

	[Token(Token = "0x6000034")]
	[Address(RVA = "0x30E040", Offset = "0x30CA40", VA = "0x18030E040")]
	public void OnPointerExit(PointerEventData eventData, IPointerStaying owner)
	{
	}

	[Token(Token = "0x6000035")]
	[Address(RVA = "0x30E110", Offset = "0x30CB10", VA = "0x18030E110")]
	private bool CanPropagate(IPointerStaying owner, IPointerStaying other)
	{
		return default(bool);
	}

	[Token(Token = "0x6000036")]
	[Address(RVA = "0x30E130", Offset = "0x30CB30", VA = "0x18030E130")]
	public PointerStayingPropagator()
	{
	}
}
