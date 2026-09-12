using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.BuildTimestampDisplay;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Debugging;

[Token(Token = "0x20001B1")]
public class DebugReporter : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x20001B2")]
	[CompilerGenerated]
	private struct _003CCaptureScreenshotAsJpegAsync_003Ed__15
{
		[Token(Token = "0x40007F6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40007F7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<byte[]> _003C_003Et__builder;

		[Token(Token = "0x40007F8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x40007F9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000784")]
		[Address(RVA = "0x3ABDA0", Offset = "0x3AA7A0", VA = "0x1803ABDA0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000785")]
		[Address(RVA = "0x3AC250", Offset = "0x3AAC50", VA = "0x1803AC250", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001B3")]
	[CompilerGenerated]
	private struct _003CUploadAsync_003Ed__13
{
		[Token(Token = "0x40007FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40007FB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40007FC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public DebugReporter _003C_003E4__this;

		[Token(Token = "0x40007FD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x40007FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private string _003CtimeStamp_003E5__2;

		[Token(Token = "0x40007FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private string _003CcsvFileName_003E5__3;

		[Token(Token = "0x4000800")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private string _003CjpgFileName_003E5__4;

		[Token(Token = "0x4000801")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private string _003Ccsv_003E5__5;

		[Token(Token = "0x4000802")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private byte[] _003Cjpg_003E5__6;

		[Token(Token = "0x4000803")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private float _003CstoredTimeScale_003E5__7;

		[Token(Token = "0x4000804")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x54")]
		private int _003CresultIndex_003E5__8;

		[Token(Token = "0x4000805")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private UniTask<byte[]>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000806")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
		private UniTask<int>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x4000807")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
		private string _003CpostMessage_003E5__9;

		[Token(Token = "0x4000808")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
		private string[] _003CslackFileIds_003E5__10;

		[Token(Token = "0x4000809")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
		private string[] _003C_003E7__wrap10;

		[Token(Token = "0x400080A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
		private UniTask<string>.Awaiter _003C_003Eu__3;

		[Token(Token = "0x400080B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
		private UniTask.Awaiter _003C_003Eu__4;

		[Token(Token = "0x400080C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
		private string _003ClogPath_003E5__12;

		[Token(Token = "0x6000786")]
		[Address(RVA = "0x3AC280", Offset = "0x3AAC80", VA = "0x1803AC280", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000787")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20001B4")]
	[CompilerGenerated]
	private struct _003CWaitForClickResultButtonAsync_003Ed__14
{
		[Token(Token = "0x400080D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400080E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<int> _003C_003Et__builder;

		[Token(Token = "0x400080F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000810")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public DebugReporter _003C_003E4__this;

		[Token(Token = "0x4000811")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x4000812")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<int>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000788")]
		[Address(RVA = "0x3AE400", Offset = "0x3ACE00", VA = "0x1803AE400", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000789")]
		[Address(RVA = "0x3AEDE0", Offset = "0x3AD7E0", VA = "0x1803AEDE0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40007EB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("Assets")]
	[SerializeField]
	private BuildTimestamp _buildTimestamp;

	[Token(Token = "0x40007EC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[Header("References")]
	[SerializeField]
	private SlackApp _slackApp;

	[Token(Token = "0x40007ED")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Button _uploadButton;

	[Token(Token = "0x40007EE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private CanvasGroup _resultWindowCanvasGroup;

	[Token(Token = "0x40007EF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[SerializeField]
	private TMP_Text _resultHeadingLabel;

	[Token(Token = "0x40007F0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[SerializeField]
	private TMP_Text _resultDescriptionLabel;

	[Token(Token = "0x40007F1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[SerializeField]
	private Button _cancelButton;

	[Token(Token = "0x40007F2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[SerializeField]
	private Button _postToSlackButton;

	[Token(Token = "0x40007F3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[SerializeField]
	private Button _saveToLocalButton;

	[Token(Token = "0x40007F4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	[Header("Parameters")]
	[SerializeField]
	private string _format;

	[Token(Token = "0x40007F5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	[SerializeField]
	private float _utcOffsetHours;

	[Token(Token = "0x600077B")]
	[Address(RVA = "0x3AA620", Offset = "0x3A9020", VA = "0x1803AA620")]
	private void Start()
	{
	}

	[Token(Token = "0x600077C")]
	[Address(RVA = "0x3AA750", Offset = "0x3A9150", VA = "0x1803AA750")]
	private void Reset()
	{
	}

	[Token(Token = "0x600077D")]
	[Address(RVA = "0x3AA7F0", Offset = "0x3A91F0", VA = "0x1803AA7F0")]
	[AsyncStateMachine(typeof(_003CUploadAsync_003Ed__13))]
	private UniTask UploadAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600077E")]
	[Address(RVA = "0x3AA990", Offset = "0x3A9390", VA = "0x1803AA990")]
	[AsyncStateMachine(typeof(_003CWaitForClickResultButtonAsync_003Ed__14))]
	private UniTask<int> WaitForClickResultButtonAsync([Optional] CancellationToken ct)
	{
		return default(UniTask<int>);
	}

	[Token(Token = "0x600077F")]
	[Address(RVA = "0x3AAB80", Offset = "0x3A9580", VA = "0x1803AAB80")]
	[AsyncStateMachine(typeof(_003CCaptureScreenshotAsJpegAsync_003Ed__15))]
	private static UniTask<byte[]> CaptureScreenshotAsJpegAsync([Optional] CancellationToken ct)
	{
		return default(UniTask<byte[]>);
	}

	[Token(Token = "0x6000780")]
	[Address(RVA = "0x3AAD20", Offset = "0x3A9720", VA = "0x1803AAD20")]
	private string BuildLogAsCsv()
	{
		return null;
	}

	[Token(Token = "0x6000781")]
	[Address(RVA = "0x3AB700", Offset = "0x3AA100", VA = "0x1803AB700")]
	private string BuildPostMessage()
	{
		return null;
	}

	[Token(Token = "0x6000782")]
	[Address(RVA = "0x3ABB30", Offset = "0x3AA530", VA = "0x1803ABB30")]
	public DebugReporter()
	{
	}
}
