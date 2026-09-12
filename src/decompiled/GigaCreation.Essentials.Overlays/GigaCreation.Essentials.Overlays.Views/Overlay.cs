using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Overlays.Models;
using Il2CppDummyDll;
using LitMotion;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Overlays.Views;

[Token(Token = "0x2000004")]
public class Overlay : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x2000005")]
	[CompilerGenerated]
	private struct _003CFadeAsync_003Ed__3
{
		[Token(Token = "0x4000008")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000009")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400000A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public Overlay _003C_003E4__this;

		[Token(Token = "0x400000B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public FadeOverlayRequest request;

		[Token(Token = "0x400000C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000006")]
		[Address(RVA = "0xCD41D0", Offset = "0xCD2BD0", VA = "0x180CD41D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000007")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000006")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[SerializeField]
	private Image _image;

	[Token(Token = "0x4000007")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private MotionHandle _motion;

	[Token(Token = "0x6000003")]
	[Address(RVA = "0xCD3F90", Offset = "0xCD2990", VA = "0x180CD3F90")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000004")]
	[Address(RVA = "0xCD4030", Offset = "0xCD2A30", VA = "0x180CD4030")]
	[AsyncStateMachine(typeof(_003CFadeAsync_003Ed__3))]
	public UniTask FadeAsync(FadeOverlayRequest request)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000005")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public Overlay()
	{
	}
}
