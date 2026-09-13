using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x20000F5")]
public class MapArea : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x20000F7")]
	[CompilerGenerated]
	private struct _003COnClickAsync_003Ed__9
{
		[Token(Token = "0x400049E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400049F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<MapAreaKind> _003C_003Et__builder;

		[Token(Token = "0x40004A0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public MapArea _003C_003E4__this;

		[Token(Token = "0x40004A1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x40004A2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60003E8")]
		[Address(RVA = "0x36E320", Offset = "0x36CD20", VA = "0x18036E320", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60003E9")]
		[Address(RVA = "0x36E680", Offset = "0x36D080", VA = "0x18036E680", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000499")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private MapAreaFocusButton _focusButton;

	[Token(Token = "0x400049A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private MapAreaLabel _areaLabel;

	[Token(Token = "0x400049B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[Header("Parameters")]
	[SerializeField]
	private MapAreaKind _areaKind;

	[Token(Token = "0x17000085")]
	// RECONSTRUCTED | Confidence: A | RVA: 0x36DB50 (`mov eax,[rcx+0x30]; ret`)
	public MapAreaKind AreaKind
	{
		[Token(Token = "0x60003DD")]
		[Address(RVA = "0x36DB50", Offset = "0x36C550", VA = "0x18036DB50")]
		get
		{
			return _areaKind;
		}
	}

	[Token(Token = "0x60003DE")]
	[Address(RVA = "0x36DB60", Offset = "0x36C560", VA = "0x18036DB60")]
	private void Reset()
	{
	}

	[Token(Token = "0x60003DF")]
	[Address(RVA = "0x36DC90", Offset = "0x36C690", VA = "0x18036DC90")]
	public void Show()
	{
	}

	[Token(Token = "0x60003E0")]
	[Address(RVA = "0x36DCC0", Offset = "0x36C6C0", VA = "0x18036DCC0")]
	public void Hide()
	{
	}

	[Token(Token = "0x60003E1")]
	[Address(RVA = "0x36DCF0", Offset = "0x36C6F0", VA = "0x18036DCF0")]
	public void ChangeFocusButtonActivity(bool enable)
	{
	}

	[Token(Token = "0x60003E2")]
	[Address(RVA = "0x36DD60", Offset = "0x36C760", VA = "0x18036DD60")]
	[AsyncStateMachine(typeof(_003COnClickAsync_003Ed__9))]
	public UniTask<MapAreaKind> OnClickAsync([Optional] CancellationToken ct)
	{
		return default(UniTask<MapAreaKind>);
	}

	[Token(Token = "0x60003E3")]
	[Address(RVA = "0x36DF50", Offset = "0x36C950", VA = "0x18036DF50")]
	public void ModifyInEditor(bool showFocusButton, float[] labelMaxWidths)
	{
	}

	[Token(Token = "0x60003E4")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public MapArea()
	{
	}
}
