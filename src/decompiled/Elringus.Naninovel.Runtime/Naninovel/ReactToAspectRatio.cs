using System;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;

namespace Naninovel;

[Token(Token = "0x20004C1")]
public class ReactToAspectRatio : MonoBehaviour
{
	[Serializable]
	[Token(Token = "0x20004C2")]
	private class ThresholdReachedEvent : UnityEvent<bool>
	{
		[Token(Token = "0x60017C3")]
		[Address(RVA = "0x5F9FC0", Offset = "0x5F89C0", VA = "0x1805F9FC0")]
		public ThresholdReachedEvent()
		{
		}
	}

	[Token(Token = "0x4001211")]
	[FieldOffset(Offset = "0x20")]
	[Tooltip("When scene aspect ratio (width divided by height) goes above or below the value, the event will be invoked.")]
	[SerializeField]
	private float aspectThreshold;

	[Token(Token = "0x4001212")]
	[FieldOffset(Offset = "0x24")]
	[Tooltip("How frequently update the values, in seconds.")]
	[Range(0f, 1f)]
	[SerializeField]
	private float updateDelay;

	[Token(Token = "0x4001213")]
	[FieldOffset(Offset = "0x28")]
	[Tooltip("Invoked when scene aspect ratio (width divided by height) is changed and become either equal or above (true) or below (false) specified threshold.")]
	[SerializeField]
	private ThresholdReachedEvent onThresholdReached;

	[Token(Token = "0x4001214")]
	[FieldOffset(Offset = "0x30")]
	private AspectMonitor aspectMonitor;

	[Token(Token = "0x60017BF")]
	[Address(RVA = "0x5F9BC0", Offset = "0x5F85C0", VA = "0x1805F9BC0")]
	private void Awake()
	{
	}

	[Token(Token = "0x60017C0")]
	[Address(RVA = "0x5F9D10", Offset = "0x5F8710", VA = "0x1805F9D10")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x60017C1")]
	[Address(RVA = "0x5F9EF0", Offset = "0x5F88F0", VA = "0x1805F9EF0")]
	private void HandleAspectChanged(AspectMonitor monitor)
	{
	}

	[Token(Token = "0x60017C2")]
	[Address(RVA = "0x5F9F60", Offset = "0x5F8960", VA = "0x1805F9F60")]
	public ReactToAspectRatio()
	{
	}
}
