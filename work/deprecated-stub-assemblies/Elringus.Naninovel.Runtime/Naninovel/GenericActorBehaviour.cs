using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;

namespace Naninovel
{
    [Token(Token = "0x2000082")]
    public abstract class GenericActorBehaviour : MonoBehaviour
    {
    	[Serializable]
    	[Token(Token = "0x2000083")]
    	private class AppearanceChangedEvent : UnityEvent<string>
    	{
    		[Token(Token = "0x6000255")]
    		[Address(RVA = "0x45C780", Offset = "0x45B180", VA = "0x18045C780")]
    		public AppearanceChangedEvent()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x2000084")]
    	private class VisibilityChangedEvent : UnityEvent<bool>
    	{
    		[Token(Token = "0x6000256")]
    		[Address(RVA = "0x45C7C0", Offset = "0x45B1C0", VA = "0x18045C7C0")]
    		public VisibilityChangedEvent()
    		{
    		}
    	}

    	[Serializable]
    	[Token(Token = "0x2000085")]
    	private class TintColorChangedEvent : UnityEvent<Color>
    	{
    		[Token(Token = "0x6000257")]
    		[Address(RVA = "0x45C800", Offset = "0x45B200", VA = "0x18045C800")]
    		public TintColorChangedEvent()
    		{
    		}
    	}

    	[Token(Token = "0x40001D8")]
    	[FieldOffset(Offset = "0x38")]
    	[Tooltip("Invoked when appearance of the actor is changed.")]
    	[SerializeField]
    	private AppearanceChangedEvent onAppearanceChanged;

    	[Token(Token = "0x40001D9")]
    	[FieldOffset(Offset = "0x40")]
    	[Tooltip("Invoked when visibility of the actor is changed.")]
    	[SerializeField]
    	private VisibilityChangedEvent onVisibilityChanged;

    	[Token(Token = "0x40001DA")]
    	[FieldOffset(Offset = "0x48")]
    	[Tooltip("Invoked when tint color of the actor is changed.")]
    	[SerializeField]
    	private TintColorChangedEvent onTintColorChanged;

    	[Token(Token = "0x1400000B")]
    	public event Action<string> OnAppearanceChanged
    	{
    		[Token(Token = "0x600024B")]
    		[Address(RVA = "0x45BFA0", Offset = "0x45A9A0", VA = "0x18045BFA0")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x600024C")]
    		[Address(RVA = "0x45C0B0", Offset = "0x45AAB0", VA = "0x18045C0B0")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x1400000C")]
    	public event Action<bool> OnVisibilityChanged
    	{
    		[Token(Token = "0x600024D")]
    		[Address(RVA = "0x45C1C0", Offset = "0x45ABC0", VA = "0x18045C1C0")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x600024E")]
    		[Address(RVA = "0x45C2D0", Offset = "0x45ACD0", VA = "0x18045C2D0")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x1400000D")]
    	public event Action<Color> OnTintColorChanged
    	{
    		[Token(Token = "0x600024F")]
    		[Address(RVA = "0x45C3E0", Offset = "0x45ADE0", VA = "0x18045C3E0")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6000250")]
    		[Address(RVA = "0x45C4F0", Offset = "0x45AEF0", VA = "0x18045C4F0")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x6000251")]
    	[Address(RVA = "0x45C600", Offset = "0x45B000", VA = "0x18045C600")]
    	public void InvokeAppearanceChangedEvent(string value)
    	{
    	}

    	[Token(Token = "0x6000252")]
    	[Address(RVA = "0x45C670", Offset = "0x45B070", VA = "0x18045C670")]
    	public void InvokeVisibilityChangedEvent(bool value)
    	{
    	}

    	[Token(Token = "0x6000253")]
    	[Address(RVA = "0x45C6F0", Offset = "0x45B0F0", VA = "0x18045C6F0")]
    	public void InvokeTintColorChangedEvent(Color value)
    	{
    	}

    	[Token(Token = "0x6000254")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	protected GenericActorBehaviour()
    	{
    	}
    }
}
