using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Ui;
using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x2000112")]
public class WitchBookItemList : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x2000113")]
	[CompilerGenerated]
	private struct _003CExecuteRebuildingAsync_003Ed__11
{
		[Token(Token = "0x4000520")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000521")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000522")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public WitchBookItemList _003C_003E4__this;

		[Token(Token = "0x4000523")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000524")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000487")]
		[Address(RVA = "0x378990", Offset = "0x377390", VA = "0x180378990", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000488")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400051C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private RectTransform _contentRoot;

	[Token(Token = "0x400051D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private AutoLayoutSupporter _autoLayoutSupporter;

	[Token(Token = "0x400051E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Pagination _pagination;

	[Token(Token = "0x400051F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[Header("Parameters")]
	[SerializeField]
	private float _itemInterval;

	[Token(Token = "0x17000094")]
	public RectTransform ContentRoot
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x377910 (generic reference-field getter)
		// 292 project-wide methods fold to this same native body; the offset determines
		// which field each one reads. Here that is `_contentRoot` (+0x20, RectTransform).
		[Token(Token = "0x6000481")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		get
		{
			return _contentRoot;
		}
	}

	[Token(Token = "0x17000095")]
	public Pagination Pagination
	{
		[Token(Token = "0x6000482")]
		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000096")]
	public float ItemInterval
	{
		[Token(Token = "0x6000483")]
		[Address(RVA = "0x3786B0", Offset = "0x3770B0", VA = "0x1803786B0")]
		get
		{
			return default(float);
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3786C0, 292 B
	// `_autoLayoutSupporter (+0x28) = GetComponentInChildren<AutoLayoutSupporter>(); _pagination (+0x30) = GetComponentInChildren<Pagination>()`.
	[Token(Token = "0x6000484")]
	[Address(RVA = "0x3786C0", Offset = "0x3770C0", VA = "0x1803786C0")]
	private void Reset()
	{
		_autoLayoutSupporter = GetComponentInChildren<AutoLayoutSupporter>();
		_pagination = GetComponentInChildren<Pagination>();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3787F0, 401 B (entry) + MoveNext 0x378990, 724 B
	// A single await: `_autoLayoutSupporter (+0x28).ExecuteRebuildingAsync(ct)`. The awaiter is at +0x28 and
	// IUniTaskSource status/GetResult go through the interface thunks.
	[Token(Token = "0x6000485")]
	[Address(RVA = "0x3787F0", Offset = "0x3771F0", VA = "0x1803787F0")]
	[AsyncStateMachine(typeof(_003CExecuteRebuildingAsync_003Ed__11))]
	public async UniTask ExecuteRebuildingAsync([Optional] CancellationToken ct)
	{
		await _autoLayoutSupporter.ExecuteRebuildingAsync(ct);
	}

	[Token(Token = "0x6000486")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public WitchBookItemList()
	{
	}
}
