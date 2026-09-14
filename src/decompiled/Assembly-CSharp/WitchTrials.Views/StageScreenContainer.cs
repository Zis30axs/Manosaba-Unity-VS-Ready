using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using LitMotion;
using LitMotion.Extensions;
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

	// RECONSTRUCTED | Confidence: A | RVA: 0x387110, 292 B
	// Editor-only wiring: `GetComponent<CanvasGroup>()` -> +0x20, then
	// `GetComponentInChildren<Quadrilateral>()` -> +0x28.
	[Token(Token = "0x6000544")]
	[Address(RVA = "0x387110", Offset = "0x385B10", VA = "0x180387110")]
	private void Reset()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
		_quadrilateral = GetComponentInChildren<Quadrilateral>();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x387240, 479 B (entry) + MoveNext 0x387680
	// Single await: `LMotion.Create<float,...>(_canvasGroup.alpha, alpha,
	// duration)` (no ease), `LitMotionUGUIExtensions.BindToAlpha(_canvasGroup)`,
	// `AddTo(this)`, `ToUniTask(ct)`.
	[Token(Token = "0x6000545")]
	[Address(RVA = "0x387240", Offset = "0x385C40", VA = "0x180387240")]
	[AsyncStateMachine(typeof(_003CModifyAlphaAsync_003Ed__4))]
	public async UniTask ModifyAlphaAsync(float alpha, float duration, [Optional] CancellationToken ct)
	{
		await LMotion.Create(_canvasGroup.alpha, alpha, duration)
			.BindToAlpha(_canvasGroup)
			.AddTo(this)
			.ToUniTask(ct);
	}

	// RECONSTRUCTED | Confidence: A (behavior), B (`MathF.Tan` spelling) | RVA: 0x387420, 595 B
	// - The split line's horizontal lean is `tan(roll * Deg2Rad) / AspectRatio *
	//   0.5f`. The constants are rdata 0x2AE25B4 = 0.0174533, 0x2AE28C4 =
	//   1.7777778 (the `AspectRatio` const) and 0x2AE2728 = 0.5.
	// - The helper `sub_302800` is float-argument libm `tan`: its tiny-angle term
	//   is x + x^3 * (1/3) (double at 0x2AE7718) and its medium path is a
	//   rational P/Q. It is called directly rather than through the double
	//   `Mathf.Tan` wrapper, hence the `MathF.Tan` spelling (B).
	// - `top = normalizedPosX + lean`, `bottom = normalizedPosX - lean`, then a
	//   4-element `Vector2[]` in TL, TR, BR, BL order.
	// - Left (0): (min(top, 0), 1), (top, 1), (bottom, 0), (min(bottom, 0), 0).
	//   The outer corners stretch past the left edge when the line leaves the
	//   screen.
	// - Right (1): (top, 1), (max(top, 1), 1), (max(bottom, 1), 0), (bottom, 0).
	// - Any other value: the full screen from the Vector2 statics `up` (+0x10),
	//   `one` (+0x08), `right` (+0x28), `zero` (+0x00).
	// - Finally a tail jump to `Quadrilateral.SetUvs(Vector2[])`.
	// The min/max are inlined compares (`Mathf.Min`/`Mathf.Max` spelling B).
	[Token(Token = "0x6000546")]
	[Address(RVA = "0x387420", Offset = "0x385E20", VA = "0x180387420")]
	public void ModifyQuadrilateral(float normalizedPosX, float roll, ScreenSplitDirection direction)
	{
		// `Mathf.Deg2Rad` is a const in Unity (inlined to the rdata value); the
		// reference stub omits it, so the literal is written out.
		float lean = MathF.Tan(roll * 0.017453292f) / AspectRatio * 0.5f;
		float top = normalizedPosX + lean;
		float bottom = normalizedPosX - lean;
		Vector2[] uvs = new Vector2[4];
		switch (direction)
		{
		case ScreenSplitDirection.Left:
			uvs[0] = new Vector2(Mathf.Min(top, 0f), 1f);
			uvs[1] = new Vector2(top, 1f);
			uvs[2] = new Vector2(bottom, 0f);
			uvs[3] = new Vector2(Mathf.Min(bottom, 0f), 0f);
			break;
		case ScreenSplitDirection.Right:
			uvs[0] = new Vector2(top, 1f);
			uvs[1] = new Vector2(Mathf.Max(top, 1f), 1f);
			uvs[2] = new Vector2(Mathf.Max(bottom, 1f), 0f);
			uvs[3] = new Vector2(bottom, 0f);
			break;
		default:
			uvs[0] = Vector2.up;
			uvs[1] = Vector2.one;
			uvs[2] = Vector2.right;
			uvs[3] = Vector2.zero;
			break;
		}
		_quadrilateral.SetUvs(uvs);
	}

	[Token(Token = "0x6000547")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public StageScreenContainer()
	{
	}
}
