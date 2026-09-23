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
    [Token(Token = "0x200000D")]
    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class InsideMultipleRectTransforms : MonoBehaviour
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200000F")]
    	[CompilerGenerated]
    	private struct _003CWaitForNextFrameAndAdjustAsync_003Ed__11
    {
    		[Token(Token = "0x4000036")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000037")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000038")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000039")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public InsideMultipleRectTransforms _003C_003E4__this;

    		[Token(Token = "0x400003A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600004D")]
    		[Address(RVA = "0xD58690", Offset = "0xD57090", VA = "0x180D58690", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600004E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000029")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private RectTransform _rectTransform;

    	[Token(Token = "0x400002A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Image _image;

    	[Token(Token = "0x400002B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[Space]
    	[SerializeField]
    	private List<RectTransform> _insideTargets;

    	[Token(Token = "0x400002C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[Header("Parameters")]
    	[SerializeField]
    	private bool _showBorder;

    	[Token(Token = "0x400002D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x39")]
    	[SerializeField]
    	private bool _setDirtyOnAdjust;

    	[Token(Token = "0x400002E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x3A")]
    	[Space]
    	[SerializeField]
    	private bool _adjustOnAwake;

    	[Token(Token = "0x17000008")]
    	public IList<RectTransform> InsideTargets
    	{
    		[Token(Token = "0x600003E")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3786A0, 5 B -- `mov rax, qword ptr [rcx + 0x30]; ret`: the backing field _insideTargets (+0x30 in dump.cs)
    		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0")]
    		get
    		{
    			return _insideTargets;
    		}
    	}

    	[Token(Token = "0x600003F")]
    	[Address(RVA = "0xD572F0", Offset = "0xD55CF0", VA = "0x180D572F0")]
    	private void Awake()
    	{
    	}

    	[Token(Token = "0x6000040")]
    	[Address(RVA = "0xD574F0", Offset = "0xD55EF0", VA = "0x180D574F0")]
    	private void OnValidate()
    	{
    	}

    	[Token(Token = "0x6000041")]
    	[Address(RVA = "0xD575E0", Offset = "0xD55FE0", VA = "0x180D575E0")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x6000042")]
    	[Address(RVA = "0xD57720", Offset = "0xD56120", VA = "0x180D57720")]
    	[AsyncStateMachine(typeof(_003CWaitForNextFrameAndAdjustAsync_003Ed__11))]
    	private UniTask WaitForNextFrameAndAdjustAsync([Optional] CancellationToken ct)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000043")]
    	[Address(RVA = "0xD578C0", Offset = "0xD562C0", VA = "0x180D578C0")]
    	public void Adjust()
    	{
    	}

    	[Token(Token = "0x6000044")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public InsideMultipleRectTransforms()
    	{
    	}
    }
}
