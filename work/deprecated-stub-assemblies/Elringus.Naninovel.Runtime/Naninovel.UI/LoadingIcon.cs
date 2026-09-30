using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x200055E")]
    public class LoadingIcon : MonoBehaviour
    {
    	[Token(Token = "0x200055F")]
    	[CompilerGenerated]
    	private sealed class _003CStart_003Ed__3
    {
    		[Token(Token = "0x4001488")]
    		[FieldOffset(Offset = "0x10")]
    		private int _003C_003E1__state;

    		[Token(Token = "0x4001489")]
    		[FieldOffset(Offset = "0x18")]
    		private object _003C_003E2__current;

    		[Token(Token = "0x400148A")]
    		[FieldOffset(Offset = "0x20")]
    		public LoadingIcon _003C_003E4__this;

    		[Token(Token = "0x170003C0")]
    		private object System_002ECollections_002EGeneric_002EIEnumerator_003CSystem_002EObject_003E_002ECurrent
    		{
    			[Token(Token = "0x6001AA3")]
    			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "4")]
    			[DebuggerHidden]
    			get
    			{
    				return null;
    			}
    		}

    		[Token(Token = "0x170003C1")]
    		private object System_002ECollections_002EIEnumerator_002ECurrent
    		{
    			[Token(Token = "0x6001AA5")]
    			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "7")]
    			[DebuggerHidden]
    			get
    			{
    				return null;
    			}
    		}

    		[Token(Token = "0x6001AA0")]
    		[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
    		[DebuggerHidden]
    		public _003CStart_003Ed__3(int _003C_003E1__state)
    		{
    		}

    		[Token(Token = "0x6001AA1")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void System_002EIDisposable_002EDispose()
    		{
    		}

    		[Token(Token = "0x6001AA2")]
    		[Address(RVA = "0x62D5C0", Offset = "0x62BFC0", VA = "0x18062D5C0", Slot = "6")]
    		private bool MoveNext()
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x6001AA4")]
    		[Address(RVA = "0x62DC70", Offset = "0x62C670", VA = "0x18062DC70", Slot = "8")]
    		[DebuggerHidden]
    		private void System_002ECollections_002EIEnumerator_002EReset()
    		{
    		}
    	}

    	[Token(Token = "0x4001485")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private float animationSpeed;

    	[Token(Token = "0x4001486")]
    	[FieldOffset(Offset = "0x24")]
    	[SerializeField]
    	private float animationDelay;

    	[Token(Token = "0x4001487")]
    	[FieldOffset(Offset = "0x28")]
    	private WaitForSeconds waitForDelay;

    	[Token(Token = "0x6001A9E")]
    	[Address(RVA = "0x62D4C0", Offset = "0x62BEC0", VA = "0x18062D4C0")]
    	[IteratorStateMachine(typeof(_003CStart_003Ed__3))]
    	private IEnumerator Start()
    	{
    		return null;
    	}

    	[Token(Token = "0x6001A9F")]
    	[Address(RVA = "0x62D560", Offset = "0x62BF60", VA = "0x18062D560")]
    	public LoadingIcon()
    	{
    	}
    }
}
