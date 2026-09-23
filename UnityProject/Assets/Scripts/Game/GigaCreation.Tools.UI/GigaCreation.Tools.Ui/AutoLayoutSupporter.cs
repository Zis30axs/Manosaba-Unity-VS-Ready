using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Tools.Ui
{
    [Token(Token = "0x2000007")]
    public class AutoLayoutSupporter : MonoBehaviour
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000009")]
    	[CompilerGenerated]
    	private struct _003CExecuteRebuildingAsync_003Ed__11
    {
    		[Token(Token = "0x400000F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000010")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000011")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AutoLayoutSupporter _003C_003E4__this;

    		[Token(Token = "0x4000012")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000013")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600002E")]
    		[Address(RVA = "0xD558A0", Offset = "0xD542A0", VA = "0x180D558A0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600002F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000A")]
    	[CompilerGenerated]
    	private struct _003CMarkAllRectTransformsForRebuildAsync_003Ed__13
    {
    		[Token(Token = "0x4000014")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000015")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000016")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AutoLayoutSupporter _003C_003E4__this;

    		[Token(Token = "0x4000017")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000018")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private RectTransform[] _003C_003E7__wrap1;

    		[Token(Token = "0x4000019")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private int _003C_003E7__wrap2;

    		[Token(Token = "0x400001A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000030")]
    		[Address(RVA = "0xD55CF0", Offset = "0xD546F0", VA = "0x180D55CF0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000031")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000B")]
    	[CompilerGenerated]
    	private struct _003CRebuildLayoutAsync_003Ed__12
    {
    		[Token(Token = "0x400001B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400001C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400001D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AutoLayoutSupporter _003C_003E4__this;

    		[Token(Token = "0x400001E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x400001F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000032")]
    		[Address(RVA = "0xD56210", Offset = "0xD54C10", VA = "0x180D56210", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000033")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000006")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private ContentSizeFitter[] _contentSizeFitters;

    	[Token(Token = "0x4000007")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private LayoutGroup[] _layoutGroups;

    	[Token(Token = "0x4000008")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private RectTransform[] _rectTransforms;

    	[Token(Token = "0x4000009")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private bool _isDestroying;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xBDA9F0 (shared 5 B body) -- _isDestroying (+0x38) = true
    	// Annotated disassembly: work/title-path/disas/als_0xBDA9F0.txt
    	[Token(Token = "0x600001C")]
    	[Address(RVA = "0xBDA9F0", Offset = "0xBD93F0", VA = "0x180BDA9F0")]
    	private void OnDestroy()
    	{
    		_isDestroying = true;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD541F0, 238 B -- UpdateReferences(); then tail jump
    	//   SetComponentsEnabled(false, _contentSizeFitters (+0x20), _layoutGroups (+0x28))
    	// Annotated disassembly: work/title-path/disas/als_0xD541F0.txt
    	[Token(Token = "0x600001D")]
    	[Address(RVA = "0xD541F0", Offset = "0xD52BF0", VA = "0x180D541F0")]
    	private void Reset()
    	{
    		UpdateReferences();
    		SetComponentsEnabled(false, _contentSizeFitters, _layoutGroups);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD542E0, 228 B -- tail jump SetComponentsEnabled(true (mov cl, 1),
    	//   _contentSizeFitters, _layoutGroups)
    	// Annotated disassembly: work/title-path/disas/als_0xD542E0.txt
    	[Token(Token = "0x600001E")]
    	[Address(RVA = "0xD542E0", Offset = "0xD52CE0", VA = "0x180D542E0")]
    	public void EnableLayoutComponents()
    	{
    		SetComponentsEnabled(true, _contentSizeFitters, _layoutGroups);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD543D0, 228 B -- tail jump SetComponentsEnabled(false, _contentSizeFitters,
    	//   _layoutGroups)
    	// Annotated disassembly: work/title-path/disas/als_0xD543D0.txt
    	[Token(Token = "0x600001F")]
    	[Address(RVA = "0xD543D0", Offset = "0xD52DD0", VA = "0x180D543D0")]
    	[UsedImplicitly]
    	public void DisableLayoutComponents()
    	{
    		SetComponentsEnabled(false, _contentSizeFitters, _layoutGroups);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD544C0, 190 B -- UpdateReferences() on every
    	//   GetComponentsInChildren<AutoLayoutSupporter>(true) (includeInactive: mov dl, 1)
    	// Annotated disassembly: work/title-path/disas/als_0xD544C0.txt
    	[Token(Token = "0x6000020")]
    	[Address(RVA = "0xD544C0", Offset = "0xD52EC0", VA = "0x180D544C0")]
    	public void UpdateReferencesInChildren()
    	{
    		AutoLayoutSupporter[] componentsInChildren = GetComponentsInChildren<AutoLayoutSupporter>(true);
    		for (int i = 0; i < componentsInChildren.Length; i++)
    		{
    			componentsInChildren[i].UpdateReferences();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD54580, 1964 B -- _contentSizeFitters = GetComponentsInChildren<ContentSizeFitter>(true)
    	//   .ToArray(); _layoutGroups = GetComponentsInChildren<LayoutGroup>(true).ToArray(); _rectTransforms (+0x30) =
    	//   the fitters' transforms as RectTransform (<>c.b__9_0 0xD55730) Concat the groups' (b__9_1 0xD557A0), Distinct,
    	//   OrderByDescending(rt => rt.GetComponentsInParent<Transform>(true).Length) (b__9_2 0xD55810: deepest first),
    	//   ToArray
    	// Annotated disassembly: work/title-path/disas/als_0xD54580.txt, als_0xD55730.txt, als_0xD557A0.txt, als_0xD55810.txt
    	[Token(Token = "0x6000021")]
    	[Address(RVA = "0xD54580", Offset = "0xD52F80", VA = "0x180D54580")]
    	private void UpdateReferences()
    	{
    		_contentSizeFitters = GetComponentsInChildren<ContentSizeFitter>(true).ToArray();
    		_layoutGroups = GetComponentsInChildren<LayoutGroup>(true).ToArray();
    		_rectTransforms = _contentSizeFitters.Select((ContentSizeFitter fitter) => fitter.transform as RectTransform)
    			.Concat(_layoutGroups.Select((LayoutGroup group) => group.transform as RectTransform))
    			.Distinct()
    			.OrderByDescending((RectTransform rt) => rt.GetComponentsInParent<Transform>(true).Length)
    			.ToArray();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD54D30, 71 B -- ExecuteRebuildingAsync(destroyCancellationToken).Forget()
    	// Annotated disassembly: work/title-path/disas/als_0xD54D30.txt
    	[Token(Token = "0x6000022")]
    	[Address(RVA = "0xD54D30", Offset = "0xD53730", VA = "0x180D54D30")]
    	public void ExecuteRebuilding()
    	{
    		ExecuteRebuildingAsync(base.destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD54D80 (kickoff) / 0xD558A0 (<ExecuteRebuildingAsync>d__11.MoveNext, 1102 B):
    	//   nothing once the component is being destroyed (_isDestroying +0x38, 0x180D5593F); UpdateReferencesInChildren()
    	//   (inlined), then await RebuildLayoutAsync(ct). Body in ExecuteRebuildingAsyncCore.
    	// Annotated disassembly: work/title-path/disas/als_0xD558A0.txt
    	[Token(Token = "0x6000023")]
    	[Address(RVA = "0xD54D80", Offset = "0xD53780", VA = "0x180D54D80")]
    	[AsyncStateMachine(typeof(_003CExecuteRebuildingAsync_003Ed__11))]
    	[UsedImplicitly]
    	public UniTask ExecuteRebuildingAsync([Optional] CancellationToken ct)
    	{
    		return ExecuteRebuildingAsyncCore(ct);
    	}

    	private async UniTask ExecuteRebuildingAsyncCore(CancellationToken ct)
    	{
    		// 0x180D5593F: the shipped code skips the rebuild while the component is being destroyed
    		if (_isDestroying)
    		{
    			return;
    		}
    		UpdateReferencesInChildren();
    		await RebuildLayoutAsync(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD54F20 (kickoff) / 0xD56210 (<RebuildLayoutAsync>d__12.MoveNext, 1471 B):
    	//   EnableLayoutComponents() (inlined, cl = 1); await MarkAllRectTransformsForRebuildAsync(ct);
    	//   DisableLayoutComponents() (inlined, ecx = 0). Body in RebuildLayoutAsyncCore.
    	// Annotated disassembly: work/title-path/disas/als_0xD56210.txt
    	[Token(Token = "0x6000024")]
    	[Address(RVA = "0xD54F20", Offset = "0xD53920", VA = "0x180D54F20")]
    	[AsyncStateMachine(typeof(_003CRebuildLayoutAsync_003Ed__12))]
    	private UniTask RebuildLayoutAsync([Optional] CancellationToken ct)
    	{
    		return RebuildLayoutAsyncCore(ct);
    	}

    	private async UniTask RebuildLayoutAsyncCore(CancellationToken ct)
    	{
    		EnableLayoutComponents();
    		await MarkAllRectTransformsForRebuildAsync(ct);
    		DisableLayoutComponents();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD550C0 (kickoff) / 0xD55CF0 (<MarkAllRectTransformsForRebuildAsync>d__13.MoveNext,
    	//   1300 B): foreach over _rectTransforms (<>7__wrap1 / <>7__wrap2): a live one is
    	//   LayoutRebuilder.MarkLayoutForRebuild(rt) and then awaited: in play mode UniTask.WaitForEndOfFrame(this, ct),
    	//   otherwise UniTask.Delay(TimeSpan.FromSeconds(0.1) (data 0x2AE2888), DelayType.Realtime (2),
    	//   PlayerLoopTiming.Update (8), ct); a destroyed one logs Debug.LogWarning("RectTransform is null: " + rt.name,
    	//   rt). Body in MarkAllRectTransformsForRebuildAsyncCore.
    	// Annotated disassembly: work/title-path/disas/als_0xD55CF0.txt
    	[Token(Token = "0x6000025")]
    	[Address(RVA = "0xD550C0", Offset = "0xD53AC0", VA = "0x180D550C0")]
    	[AsyncStateMachine(typeof(_003CMarkAllRectTransformsForRebuildAsync_003Ed__13))]
    	private UniTask MarkAllRectTransformsForRebuildAsync([Optional] CancellationToken ct)
    	{
    		return MarkAllRectTransformsForRebuildAsyncCore(ct);
    	}

    	private async UniTask MarkAllRectTransformsForRebuildAsyncCore(CancellationToken ct)
    	{
    		RectTransform[] rectTransforms = _rectTransforms;
    		foreach (RectTransform rectTransform in rectTransforms)
    		{
    			if (rectTransform != null)
    			{
    				LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
    				await (Application.isPlaying ? UniTask.WaitForEndOfFrame(this, ct) : UniTask.Delay(TimeSpan.FromSeconds(0.1), DelayType.Realtime, PlayerLoopTiming.Update, ct));
    			}
    			else
    			{
    				UnityEngine.Debug.LogWarning("RectTransform is null: " + rectTransform.name, rectTransform);
    			}
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD55270, 1054 B -- foreach behaviour in behaviours.SelectMany(<>c.b__14_0
    	//   0x3133E0: e => e): a live one gets enabled = enable, a destroyed one logs
    	//   Debug.LogWarning("Behaviour is null: " + behaviour.name, behaviour)
    	// Annotated disassembly: work/title-path/disas/als_0xD55270.txt, als_0x3133E0.txt
    	[Token(Token = "0x6000026")]
    	[Address(RVA = "0xD55270", Offset = "0xD53C70", VA = "0x180D55270")]
    	private static void SetComponentsEnabled(bool enable, params IEnumerable<Behaviour>[] behaviours)
    	{
    		foreach (Behaviour item in behaviours.SelectMany((IEnumerable<Behaviour> enumerable) => enumerable))
    		{
    			if (item != null)
    			{
    				item.enabled = enable;
    			}
    			else
    			{
    				UnityEngine.Debug.LogWarning("Behaviour is null: " + item.name, item);
    			}
    		}
    	}

    	[Token(Token = "0x6000027")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public AutoLayoutSupporter()
    	{
    	}
    }
}
