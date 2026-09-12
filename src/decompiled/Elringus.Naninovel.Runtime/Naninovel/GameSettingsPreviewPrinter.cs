using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel;

[Token(Token = "0x20004C3")]
public class GameSettingsPreviewPrinter : ScriptableUIBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x20004C5")]
	[CompilerGenerated]
	private struct _003CRevealTextOverTime_003Ed__11
{
		[Token(Token = "0x400121C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400121D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400121E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GameSettingsPreviewPrinter _003C_003E4__this;

		[Token(Token = "0x400121F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float revealDelay;

		[Token(Token = "0x4001220")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken token;

		[Token(Token = "0x4001221")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private float _003CwaitUntilTime_003E5__2;

		[Token(Token = "0x4001222")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<bool>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4001223")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private YieldAwaitable.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60017CC")]
		[Address(RVA = "0x5FAAA0", Offset = "0x5F94A0", VA = "0x1805FAAA0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60017CD")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001216")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	[Tooltip("A component implementing `IRevealableText` interface for displaying the preview text.")]
	[SerializeField]
	private Graphic revealableText;

	[Token(Token = "0x4001217")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	private TextRevealer revealer;

	[Token(Token = "0x4001218")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	private CancellationTokenSource revealCTS;

	[Token(Token = "0x4001219")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
	private ITextPrinterManager printMng;

	[Token(Token = "0x400121A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
	private IScriptPlayer player;

	[Token(Token = "0x1700036A")]
	protected virtual IRevealableText RevealableText
	{
		[Token(Token = "0x60017C4")]
		[Address(RVA = "0x468E80", Offset = "0x467880", VA = "0x180468E80", Slot = "59")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x60017C5")]
		[Address(RVA = "0x468E90", Offset = "0x467890", VA = "0x180468E90")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x60017C6")]
	[Address(RVA = "0x5FA000", Offset = "0x5F8A00", VA = "0x1805FA000", Slot = "60")]
	public virtual void StartPrinting()
	{
	}

	[Token(Token = "0x60017C7")]
	[Address(RVA = "0x5FA300", Offset = "0x5F8D00", VA = "0x1805FA300", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x60017C8")]
	[Address(RVA = "0x5FA690", Offset = "0x5F9090", VA = "0x1805FA690", Slot = "61")]
	[AsyncStateMachine(typeof(_003CRevealTextOverTime_003Ed__11))]
	protected virtual UniTask RevealTextOverTime(float revealDelay, CancellationToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60017C9")]
	[Address(RVA = "0x5FA840", Offset = "0x5F9240", VA = "0x1805FA840", Slot = "62")]
	protected virtual void HandleRootVisibilityChanged(bool visible)
	{
	}

	[Token(Token = "0x60017CA")]
	[Address(RVA = "0x5FA870", Offset = "0x5F9270", VA = "0x1805FA870", Slot = "63")]
	protected virtual float GetDelayForText(string text)
	{
		return default(float);
	}

	[Token(Token = "0x60017CB")]
	[Address(RVA = "0x38A020", Offset = "0x388A20", VA = "0x18038A020")]
	public GameSettingsPreviewPrinter()
	{
	}
}
