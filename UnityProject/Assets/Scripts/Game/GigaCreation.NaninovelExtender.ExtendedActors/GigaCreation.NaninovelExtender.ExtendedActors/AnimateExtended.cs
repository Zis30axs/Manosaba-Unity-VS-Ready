#nullable enable annotations
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.FX;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x200004B")]
    public class AnimateExtended : Animate
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200004E")]
    	[CompilerGenerated]
    	private struct _003CAnimateKey_003Ed__7
    {
    		[Token(Token = "0x400011F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000120")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000121")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AnimateExtended _003C_003E4__this;

    		[Token(Token = "0x4000122")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public int keyIndex;

    		[Token(Token = "0x4000123")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public IActor actor;

    		[Token(Token = "0x4000124")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x4000125")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600010F")]
    		[Address(RVA = "0xD28B70", Offset = "0xD27570", VA = "0x180D28B70", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000110")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000116")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
    	private readonly List<string> _easingTypeNameForPosX;

    	[Token(Token = "0x4000117")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
    	private readonly List<string> _easingTypeNameForPosY;

    	[Token(Token = "0x4000118")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
    	private readonly IList<UniTask> _tasks;

    	[Token(Token = "0x60000FF")]
    	[Address(RVA = "0xD27530", Offset = "0xD25F30", VA = "0x180D27530", Slot = "26")]
    	public override void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
    	{
    	}

    	[Token(Token = "0x6000100")]
    	[Address(RVA = "0xD27760", Offset = "0xD26160", VA = "0x180D27760")]
    	private void ParseAdditionalParameters(IEnumerable<string> parameters)
    	{
    	}

    	[Token(Token = "0x6000101")]
    	[Address(RVA = "0xD27EF0", Offset = "0xD268F0", VA = "0x180D27EF0")]
    	private static bool TryParseParameter(string parameter, out string[] keys)
    	{
    		keys = default;
    		keys = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6000102")]
    	private void AssignKeys<T>(IReadOnlyList<string> keys, List<T> parameter, [Optional] Func<string, T> parseKey)
    	{
    	}

    	[Token(Token = "0x6000103")]
    	[Address(RVA = "0xD28160", Offset = "0xD26B60", VA = "0x180D28160", Slot = "28")]
    	[AsyncStateMachine(typeof(_003CAnimateKey_003Ed__7))]
    	protected override UniTask AnimateKey(IActor actor, int keyIndex, AsyncToken asyncToken)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000104")]
    	[Address(RVA = "0xD28360", Offset = "0xD26D60", VA = "0x180D28360")]
    	private static EasingType? ParseEasing(IReadOnlyList<string> easingTypeName, int keyIndex)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000105")]
    	[Address(RVA = "0xD28540", Offset = "0xD26F40", VA = "0x180D28540")]
    	public AnimateExtended()
    	{
    	}
    }
}
