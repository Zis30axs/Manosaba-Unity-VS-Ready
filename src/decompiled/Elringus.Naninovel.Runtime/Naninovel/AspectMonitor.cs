using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000124")]
public class AspectMonitor
{
	[Token(Token = "0x4000498")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private readonly Timer timer;

	[Token(Token = "0x170000E9")]
	public float CurrentAspect
	{
		[Token(Token = "0x6000575")]
		[Address(RVA = "0x39FB70", Offset = "0x39E570", VA = "0x18039FB70")]
		[CompilerGenerated]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6000576")]
		[Address(RVA = "0x39FB80", Offset = "0x39E580", VA = "0x18039FB80")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x170000EA")]
	public float PreviousAspect
	{
		[Token(Token = "0x6000577")]
		[Address(RVA = "0x5104D0", Offset = "0x50EED0", VA = "0x1805104D0")]
		[CompilerGenerated]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6000578")]
		[Address(RVA = "0x5104E0", Offset = "0x50EEE0", VA = "0x1805104E0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x14000011")]
	public event Action<AspectMonitor> OnChanged
	{
		[Token(Token = "0x6000573")]
		[Address(RVA = "0x5102B0", Offset = "0x50ECB0", VA = "0x1805102B0")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000574")]
		[Address(RVA = "0x5103C0", Offset = "0x50EDC0", VA = "0x1805103C0")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6000579")]
	[Address(RVA = "0x5104F0", Offset = "0x50EEF0", VA = "0x1805104F0")]
	public AspectMonitor()
	{
	}

	[Token(Token = "0x600057A")]
	[Address(RVA = "0x5106D0", Offset = "0x50F0D0", VA = "0x1805106D0")]
	public void Start(float updateDelay = 0.5f, UnityEngine.Object target = default, AsyncToken token = default)
	{
	}

	[Token(Token = "0x600057B")]
	[Address(RVA = "0x510720", Offset = "0x50F120", VA = "0x180510720")]
	public void Stop()
	{
	}

	[Token(Token = "0x600057C")]
	[Address(RVA = "0x510780", Offset = "0x50F180", VA = "0x180510780")]
	private void Update()
	{
	}
}
