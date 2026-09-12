using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x200013B")]
public class StageScreenContainer : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x200013C")]
	[CompilerGenerated]
	private struct _003CModifyAlphaAsync_003Ed__4
{
		[Token(Token = "0x4000613")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000614")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000615")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public StageScreenContainer _003C_003E4__this;

		[Token(Token = "0x4000616")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float alpha;

		[Token(Token = "0x4000617")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public float duration;

		[Token(Token = "0x4000618")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x4000619")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000548")]
		[Address(RVA = "0x387680", Offset = "0x386080", VA = "0x180387680", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000549")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000610")]
	private const float AspectRatio = 1.7777778f;

	[Token(Token = "0x4000611")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[SerializeField]
	private CanvasGroup _canvasGroup;

	[Token(Token = "0x4000612")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Quadrilateral _quadrilateral;

	[Token(Token = "0x6000544")]
	[Address(RVA = "0x387110", Offset = "0x385B10", VA = "0x180387110")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000545")]
	[Address(RVA = "0x387240", Offset = "0x385C40", VA = "0x180387240")]
	[AsyncStateMachine(typeof(_003CModifyAlphaAsync_003Ed__4))]
	public UniTask ModifyAlphaAsync(float alpha, float duration, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000546")]
	[Address(RVA = "0x387420", Offset = "0x385E20", VA = "0x180387420")]
	public void ModifyQuadrilateral(float normalizedPosX, float roll, ScreenSplitDirection direction)
	{
	}

	[Token(Token = "0x6000547")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public StageScreenContainer()
	{
	}
}
