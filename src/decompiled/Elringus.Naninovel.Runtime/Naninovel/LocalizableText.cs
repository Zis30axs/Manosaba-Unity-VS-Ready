using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000383")]
public struct LocalizableText : IEquatable<LocalizableText>
{
	[StructLayout(3)]
	[Token(Token = "0x2000385")]
	[CompilerGenerated]
	private struct _003CLoad_003Ed__15
{
		[Token(Token = "0x4000D15")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000D16")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000D17")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public LocalizableText _003C_003E4__this;

		[Token(Token = "0x4000D18")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public object holder;

		[Token(Token = "0x4000D19")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001119")]
		[Address(RVA = "0x584500", Offset = "0x582F00", VA = "0x180584500", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600111A")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000D11")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	public static readonly LocalizableText Empty;

	[Token(Token = "0x4000D12")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[SerializeField]
	private LocalizableTextPart[] parts;

	[Token(Token = "0x1700024E")]
	public IReadOnlyList<LocalizableTextPart> Parts
	{
		[Token(Token = "0x6001102")]
		[Address(RVA = "0x5822C0", Offset = "0x580CC0", VA = "0x1805822C0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700024F")]
	public bool IsEmpty
	{
		[Token(Token = "0x6001103")]
		[Address(RVA = "0x582310", Offset = "0x580D10", VA = "0x180582310")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6001104")]
	[Address(RVA = "0x3912B0", Offset = "0x38FCB0", VA = "0x1803912B0")]
	public LocalizableText(LocalizableTextPart[] parts)
	{
	}

	[Token(Token = "0x6001105")]
	[Address(RVA = "0x582330", Offset = "0x580D30", VA = "0x180582330")]
	public static implicit operator LocalizableText(string plainText)
	{
		return default(LocalizableText);
	}

	[Token(Token = "0x6001106")]
	[Address(RVA = "0x582530", Offset = "0x580F30", VA = "0x180582530")]
	public static implicit operator string(LocalizableText text)
	{
		return null;
	}

	[Token(Token = "0x6001107")]
	[Address(RVA = "0x582630", Offset = "0x581030", VA = "0x180582630")]
	public static bool operator ==(LocalizableText left, LocalizableText right)
	{
		return default(bool);
	}

	[Token(Token = "0x6001108")]
	[Address(RVA = "0x582690", Offset = "0x581090", VA = "0x180582690")]
	public static bool operator !=(LocalizableText left, LocalizableText right)
	{
		return default(bool);
	}

	[Token(Token = "0x6001109")]
	[Address(RVA = "0x5826F0", Offset = "0x5810F0", VA = "0x1805826F0")]
	public static LocalizableText operator +(LocalizableText a, LocalizableText b)
	{
		return default(LocalizableText);
	}

	[Token(Token = "0x600110A")]
	[Address(RVA = "0x582E30", Offset = "0x581830", VA = "0x180582E30")]
	public static LocalizableText FromPlainText(string plainText)
	{
		return default(LocalizableText);
	}

	[Token(Token = "0x600110B")]
	[Address(RVA = "0x582FC0", Offset = "0x5819C0", VA = "0x180582FC0")]
	public static LocalizableText FromTemplate(string template, string replace, LocalizableText replacement)
	{
		return default(LocalizableText);
	}

	[Token(Token = "0x600110C")]
	[Address(RVA = "0x583590", Offset = "0x581F90", VA = "0x180583590")]
	public static LocalizableText Join(string separator, IReadOnlyList<LocalizableText> values)
	{
		return default(LocalizableText);
	}

	[Token(Token = "0x600110D")]
	[Address(RVA = "0x583A40", Offset = "0x582440", VA = "0x180583A40")]
	[AsyncStateMachine(typeof(_003CLoad_003Ed__15))]
	public UniTask Load([Optional] object holder)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600110E")]
	[Address(RVA = "0x583BC0", Offset = "0x5825C0", VA = "0x180583BC0")]
	public void Hold(object holder)
	{
	}

	[Token(Token = "0x600110F")]
	[Address(RVA = "0x583CA0", Offset = "0x5826A0", VA = "0x180583CA0")]
	public void Release(object holder)
	{
	}

	[Token(Token = "0x6001110")]
	[Address(RVA = "0x583D80", Offset = "0x582780", VA = "0x180583D80")]
	public LocalizableText Juggle(LocalizableText to, object holder)
	{
		return default(LocalizableText);
	}

	[Token(Token = "0x6001111")]
	[Address(RVA = "0x583E30", Offset = "0x582830", VA = "0x180583E30", Slot = "3")]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x6001112")]
	[Address(RVA = "0x583F40", Offset = "0x582940", VA = "0x180583F40", Slot = "4")]
	public bool Equals(LocalizableText other)
	{
		return default(bool);
	}

	[Token(Token = "0x6001113")]
	[Address(RVA = "0x584050", Offset = "0x582A50", VA = "0x180584050", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x6001114")]
	[Address(RVA = "0x5840F0", Offset = "0x582AF0", VA = "0x1805840F0", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}
}
