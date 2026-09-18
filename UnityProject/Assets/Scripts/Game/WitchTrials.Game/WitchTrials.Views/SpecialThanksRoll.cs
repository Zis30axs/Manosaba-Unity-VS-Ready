using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Localization;
using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace WitchTrials.Views
{
    [Token(Token = "0x20000B2")]
    public class SpecialThanksRoll : CreditRoll
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20000B3")]
    	[CompilerGenerated]
    	private struct _003CRebuildLayoutAsync_003Ed__9
    {
    		[Token(Token = "0x4000324")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000325")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000326")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public SpecialThanksRoll _003C_003E4__this;

    		[Token(Token = "0x4000327")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000328")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600028E")]
    		[Address(RVA = "0x34B1D0", Offset = "0x349BD0", VA = "0x18034B1D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600028F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400031F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[Space]
    	[SerializeField]
    	private TMP_Text[] _labelPrefabs;

    	[Token(Token = "0x4000320")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[SerializeField]
    	private Transform _labelContainer;

    	[Token(Token = "0x4000321")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[SerializeField]
    	private AutoLayoutSupporter _autoLayoutSupporter;

    	[Token(Token = "0x4000322")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	[SerializeField]
    	private int _maxLineCount;

    	[Token(Token = "0x4000323")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	private readonly List<TMP_Text> _labels = new List<TMP_Text>();

    	// RECONSTRUCTED | Confidence: A | RVA: 0x34A550, 167 B
    	[Token(Token = "0x6000288")]
    	[Address(RVA = "0x34A550", Offset = "0x348F50", VA = "0x18034A550", Slot = "4")]
    	protected override void Reset()
    	{
    		base.Reset();
    		_autoLayoutSupporter = GetComponentInChildren<AutoLayoutSupporter>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x34A600, 307 B
    	// base.EndToScroll() is inlined (`_canvas.enabled = false`); every label gets `SetText(string.Empty)`.
    	[Token(Token = "0x6000289")]
    	[Address(RVA = "0x34A600", Offset = "0x349000", VA = "0x18034A600", Slot = "6")]
    	protected override void EndToScroll()
    	{
    		base.EndToScroll();
    		foreach (TMP_Text label in _labels)
    		{
    			label.SetText(string.Empty);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x34A740, 598 B (List<T>.Clear inlined as sub_4AD0)
    	[Token(Token = "0x600028A")]
    	[Address(RVA = "0x34A740", Offset = "0x349140", VA = "0x18034A740")]
    	public void Clear()
    	{
    		foreach (TMP_Text label in _labels)
    		{
    			UnityEngine.Object.Destroy(label.gameObject);
    		}
    		_labels.Clear();
    	}

    	// RECONSTRUCTED | Confidence: A (behaviour), B (the dictionary's static type) | RVA: 0x34A9A0, 1415 B
    	// 
    	// - A label is instantiated from `_labelPrefabs[sizeLevel]` under `_labelContainer`; its sizeDelta.x is the line
    	//   width and the preferred width of "＿" is the name spacing.
    	// - Each name is looked up through IReadOnlyDictionary<LocaleKind, string> slot 2 (the indexer) with key 0
    	//   (LocaleKind.Ja); the interface call means the element's static type was IReadOnlyDictionary.
    	// - A name that fits is appended after a full-width space (only when the line is not empty). Otherwise a line break
    	//   is appended (StringBuilder.Clear/AppendLine inlined as Length = 0 and Append(Environment.NewLine)), and after
    	//   `_maxLineCount` lines the text moves to a fresh label. The break is appended even right after switching labels,
    	//   so continuation labels start with an empty line.
    	[Token(Token = "0x600028B")]
    	[Address(RVA = "0x34A9A0", Offset = "0x3493A0", VA = "0x18034A9A0")]
    	public void SetCreditTexts(int sizeLevel, Dictionary<LocaleKind, string>[] creditNames)
    	{
    		StringBuilder stringBuilder = new StringBuilder();
    		_labels.Add(UnityEngine.Object.Instantiate(_labelPrefabs[sizeLevel], _labelContainer));
    		float width = _labels[_labels.Count - 1].rectTransform.sizeDelta.x;
    		float spacing = _labels[_labels.Count - 1].GetPreferredValues("＿").x;
    		float lineWidth = 0f;
    		int lineCount = 0;
    		foreach (IReadOnlyDictionary<LocaleKind, string> creditName in creditNames)
    		{
    			string name = creditName[LocaleKind.Ja];
    			float nameWidth = _labels[_labels.Count - 1].GetPreferredValues(name).x;
    			if (width > nameWidth + lineWidth)
    			{
    				if (lineWidth > 0f)
    				{
    					stringBuilder.Append("\u3000");
    				}
    				lineWidth += nameWidth + spacing;
    			}
    			else
    			{
    				if (++lineCount >= _maxLineCount)
    				{
    					lineCount = 0;
    					_labels[_labels.Count - 1].SetText(stringBuilder.ToString());
    					_labels.Add(UnityEngine.Object.Instantiate(_labelPrefabs[sizeLevel], _labelContainer));
    					stringBuilder.Clear();
    				}
    				stringBuilder.AppendLine();
    				lineWidth = nameWidth;
    			}
    			stringBuilder.Append(name);
    		}
    		_labels[_labels.Count - 1].SetText(stringBuilder.ToString());
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x34AF30 (builder stub); MoveNext 0x34B1D0, 724 B
    	[Token(Token = "0x600028C")]
    	[Address(RVA = "0x34AF30", Offset = "0x349930", VA = "0x18034AF30")]
    	[AsyncStateMachine(typeof(_003CRebuildLayoutAsync_003Ed__9))]
    	public async UniTask RebuildLayoutAsync([Optional] CancellationToken ct)
    	{
    		await _autoLayoutSupporter.ExecuteRebuildingAsync(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x34B0D0, 242 B
    	// Only the `_labels` field initializer (new List<TMP_Text>, +0x60) runs before the base MonoBehaviour ctor.
    	[Token(Token = "0x600028D")]
    	[Address(RVA = "0x34B0D0", Offset = "0x349AD0", VA = "0x18034B0D0")]
    	public SpecialThanksRoll()
    	{
    	}
    }
}
