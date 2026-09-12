using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;

namespace GigaCreation.Essentials.Overlays.Models;

[Token(Token = "0x2000008")]
public class OverlayFader : IOverlayFader, IService
{
	[StructLayout(3)]
	[Token(Token = "0x2000009")]
	[CompilerGenerated]
	private struct _003CFadeInAsync_003Ed__8
{
		[Token(Token = "0x4000011")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000012")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000013")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public OverlayFader _003C_003E4__this;

		[Token(Token = "0x4000014")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float duration;

		[Token(Token = "0x4000015")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public Color? color;

		[Token(Token = "0x4000016")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public CancellationToken ct;

		[Token(Token = "0x4000017")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600001A")]
		[Address(RVA = "0xCD50A0", Offset = "0xCD3AA0", VA = "0x180CD50A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600001B")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200000A")]
	[CompilerGenerated]
	private struct _003CFadeOutAsync_003Ed__6
{
		[Token(Token = "0x4000018")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000019")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400001A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public OverlayFader _003C_003E4__this;

		[Token(Token = "0x400001B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float duration;

		[Token(Token = "0x400001C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public Color? color;

		[Token(Token = "0x400001D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public CancellationToken ct;

		[Token(Token = "0x400001E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600001C")]
		[Address(RVA = "0xCD5430", Offset = "0xCD3E30", VA = "0x180CD5430", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600001D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400000F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private readonly ISubject<FadeOverlayRequest> _requestSubject;

	[Token(Token = "0x17000002")]
	public UniTask FadeTask
	{
		[Token(Token = "0x6000012")]
		[Address(RVA = "0x331710", Offset = "0x330110", VA = "0x180331710")]
		[CompilerGenerated]
		private get
		{
			return default(UniTask);
		}
		[Token(Token = "0x6000013")]
		[Address(RVA = "0x59D3D0", Offset = "0x59BDD0", VA = "0x18059D3D0", Slot = "4")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x6000014")]
	[Address(RVA = "0xCD4AE0", Offset = "0xCD34E0", VA = "0x180CD4AE0", Slot = "5")]
	public void FadeOut(float duration, [Optional] Color? color)
	{
	}

	[Token(Token = "0x6000015")]
	[Address(RVA = "0xCD4B80", Offset = "0xCD3580", VA = "0x180CD4B80", Slot = "6")]
	[AsyncStateMachine(typeof(_003CFadeOutAsync_003Ed__6))]
	public UniTask FadeOutAsync(float duration, [Optional] Color? color, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000016")]
	[Address(RVA = "0xCD4D60", Offset = "0xCD3760", VA = "0x180CD4D60", Slot = "7")]
	public void FadeIn(float duration, [Optional] Color? color)
	{
	}

	[Token(Token = "0x6000017")]
	[Address(RVA = "0xCD4E00", Offset = "0xCD3800", VA = "0x180CD4E00", Slot = "8")]
	[AsyncStateMachine(typeof(_003CFadeInAsync_003Ed__8))]
	public UniTask FadeInAsync(float duration, [Optional] Color? color, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000018")]
	[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "9")]
	public IObservable<FadeOverlayRequest> OnRequestToFadeOverlayAsObservable()
	{
		return null;
	}

	[Token(Token = "0x6000019")]
	[Address(RVA = "0xCD4FE0", Offset = "0xCD39E0", VA = "0x180CD4FE0")]
	public OverlayFader()
	{
	}
}
