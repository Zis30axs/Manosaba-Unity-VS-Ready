using System.Linq;
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
		public IgnoreSetting[] IgnoreSettings = Array.Empty<IgnoreSetting>();

		// RECONSTRUCTED | Confidence: A (behaviour), B (`== null` spelling of the inlined Unity null test) | RVA: 0x30E290, 616 B;
		// <>c__DisplayClass1_0.<CanPropagate>b__0 0x30E590 (captures `worker`, +0x10)
		// 
		// - Propagation never goes back to the owner itself (plain reference compare, before anything else).
		// - `owner as PointerStayingSpriteSwapper` is an exact-class test (the class is sealed). A non-sprite-swapper owner, or
		//   one with no matching IgnoreSetting, propagates to everything.
		// - The lambda is IgnoreSetting.IsOwner inlined (+0x10 `_owner` compared with `worker`, UnityEngine.Object equality inlined (both-null / m_CachedPtr checks));
		//   the final test is IgnoreSetting.CanPropagate inlined (`!Contains` over `_ignores`, +0x18).
		[Token(Token = "0x6000037")]
		[Address(RVA = "0x30E290", Offset = "0x30CC90", VA = "0x18030E290")]
		public bool CanPropagate(IPointerStaying owner, IPointerStaying target)
		{
			if (owner == target)
			{
				return false;
			}
			PointerStayingSpriteSwapper worker = owner as PointerStayingSpriteSwapper;
			if (worker == null)
			{
				return true;
			}
			IgnoreSetting ignoreSetting = IgnoreSettings.FirstOrDefault((IgnoreSetting setting) => setting.IsOwner(worker));
			if (ignoreSetting == null)
			{
				return true;
			}
			return ignoreSetting.CanPropagate(target as PointerStayingSpriteSwapper);
		}

		// RECONSTRUCTED | Confidence: A | RVA: 0x30E500, 135 B (only the `Array.Empty<IgnoreSetting>()` field initializer, +0x10)
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

		// RECONSTRUCTED | Confidence: A | RVA: 0x30E6D0, 307 B (UnityEngine.Object equality inlined (both-null / m_CachedPtr checks))
		[Token(Token = "0x600003B")]
		[Address(RVA = "0x30E6D0", Offset = "0x30D0D0", VA = "0x18030E6D0")]
		public bool IsOwner(PointerStayingSpriteSwapper owner)
		{
			return _owner == owner;
		}

		// RECONSTRUCTED | Confidence: A | RVA: 0x30E810, 81 B (Enumerable.Contains, result negated)
		[Token(Token = "0x600003C")]
		[Address(RVA = "0x30E810", Offset = "0x30D210", VA = "0x18030E810")]
		public bool CanPropagate(PointerStayingSpriteSwapper target)
		{
			return !_ignores.Contains(target);
		}

		// RECONSTRUCTED | Confidence: A | RVA: 0x30C000 (the shared empty body; no field initializers)
		[Token(Token = "0x600003D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public IgnoreSetting()
		{
		}
	}

	[Token(Token = "0x400001D")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private BlockList _blockList = new BlockList();

	[Token(Token = "0x400001E")]
	[FieldOffset(Offset = "0x28")]
	private IPointerStaying[] _pointerStayings;

	// RECONSTRUCTED | Confidence: A | RVA: 0x30DDB0, 437 B
	// Component.GetComponentsInChildren<T>(includeInactive: true) inlined (gameObject + dl=1); each child registers via
	// IPointerStaying slot 2 `RegisterTo(this)`. The search includes this GameObject, so a swapper on it also registers.
	[Token(Token = "0x6000032")]
	[Address(RVA = "0x30DDB0", Offset = "0x30C7B0", VA = "0x18030DDB0")]
	private void Awake()
	{
		_pointerStayings = GetComponentsInChildren<IPointerStaying>(includeInactive: true);
		IPointerStaying[] pointerStayings = _pointerStayings;
		foreach (IPointerStaying pointerStaying in pointerStayings)
		{
			pointerStaying.RegisterTo(this);
		}
	}

	// RECONSTRUCTED | Confidence: A (behaviour), B (the private CanPropagate call is inlined, so it may have been written
	// as `_blockList.CanPropagate` directly) | RVA: 0x30DF70 | IPointerStaying slot 0 `DoPointerEnter`
	// Every registered pointer-staying element, the owner included, is offered the event; BlockList filters the owner out.
	[Token(Token = "0x6000033")]
	[Address(RVA = "0x30DF70", Offset = "0x30C970", VA = "0x18030DF70")]
	public void OnPointerEnter(PointerEventData eventData, IPointerStaying owner)
	{
		IPointerStaying[] pointerStayings = _pointerStayings;
		foreach (IPointerStaying pointerStaying in pointerStayings)
		{
			if (CanPropagate(owner, pointerStaying))
			{
				pointerStaying.DoPointerEnter(eventData);
			}
		}
	}

	// RECONSTRUCTED | Confidence: A (behaviour), B (the private CanPropagate call is inlined, so it may have been written
	// as `_blockList.CanPropagate` directly) | RVA: 0x30E040 | IPointerStaying slot 1 `DoPointerExit`
	// Every registered pointer-staying element, the owner included, is offered the event; BlockList filters the owner out.
	[Token(Token = "0x6000034")]
	[Address(RVA = "0x30E040", Offset = "0x30CA40", VA = "0x18030E040")]
	public void OnPointerExit(PointerEventData eventData, IPointerStaying owner)
	{
		IPointerStaying[] pointerStayings = _pointerStayings;
		foreach (IPointerStaying pointerStaying in pointerStayings)
		{
			if (CanPropagate(owner, pointerStaying))
			{
				pointerStaying.DoPointerExit(eventData);
			}
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x30E110, 30 B (tail jump to BlockList.CanPropagate)
	[Token(Token = "0x6000035")]
	[Address(RVA = "0x30E110", Offset = "0x30CB10", VA = "0x18030E110")]
	private bool CanPropagate(IPointerStaying owner, IPointerStaying other)
	{
		return _blockList.CanPropagate(owner, other);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x30E130, 346 B
	// Only the `_blockList = new BlockList()` field initializer (BlockList ctor inlined) before the MonoBehaviour ctor.
	[Token(Token = "0x6000036")]
	[Address(RVA = "0x30E130", Offset = "0x30CB30", VA = "0x18030E130")]
	public PointerStayingPropagator()
	{
	}
}
