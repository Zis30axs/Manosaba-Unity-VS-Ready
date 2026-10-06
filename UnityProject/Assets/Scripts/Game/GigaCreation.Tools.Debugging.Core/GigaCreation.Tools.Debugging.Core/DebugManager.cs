using System;
using System.Collections.Generic;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UniRx;

namespace GigaCreation.Tools.Debugging.Core
{
    [Token(Token = "0x2000004")]
    public class DebugManager : IDebugManager, IService, IDisposable
    {
    	[Token(Token = "0x4000006")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly ReactiveProperty<bool> _isDebugMode;

    	[Token(Token = "0x17000001")]
    	public IReactiveProperty<bool> IsDebugMode
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 -- shared trivial getter: mov rax, [rcx+0x10] (`_isDebugMode`)
    		[Token(Token = "0x6000003")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
    		get
    		{
    			return _isDebugMode;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD42010, 209 B -- new ReactiveProperty<bool>(initialMode) (0x1808C4A20) stored to +0x10
    	// Annotated disassembly: work/title-path/disas/dm_01_ctor.txt
    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xD42010", Offset = "0xD40A10", VA = "0x180D42010")]
    	// PERMANENT DEBUG MODE (project decision): the debug side bar must never disappear, so debug mode starts on whatever
    	// `initialMode` says and every attempt to turn it off (DisableDebugModeButton, the presenter's inspector flag) is
    	// ignored.  UniRx's `Value` setter publishes the assigned value whenever the comparer calls it a change, so the
    	// comparer treats every `false` as "no change"; re-setting `true` from a subscriber would instead let later subscribers
    	// (DebugDisplay) receive the stale `false` after the corrective `true`.
    	private sealed class AlwaysOnReactiveProperty : ReactiveProperty<bool>
    	{
    		private sealed class IgnoreFalse : IEqualityComparer<bool>
    		{
    			public bool Equals(bool current, bool incoming)
    			{
    				return !incoming || current == incoming;
    			}

    			public int GetHashCode(bool value)
    			{
    				return value.GetHashCode();
    			}
    		}

    		private static readonly IEqualityComparer<bool> Comparer = new IgnoreFalse();

    		protected override IEqualityComparer<bool> EqualityComparer
    		{
    			get
    			{
    				return Comparer;
    			}
    		}

    		public AlwaysOnReactiveProperty() : base(true)
    		{
    		}
    	}

    	public DebugManager(bool initialMode)
    	{
    		_isDebugMode = new AlwaysOnReactiveProperty();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD420F0, 69 B -- tail call ReactiveProperty<bool>.Dispose on +0x10
    	// Annotated disassembly: work/title-path/disas/dm_02_Dispose.txt
    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xD420F0", Offset = "0xD40AF0", VA = "0x180D420F0", Slot = "5")]
    	public void Dispose()
    	{
    		_isDebugMode.Dispose();
    	}
    }
}
