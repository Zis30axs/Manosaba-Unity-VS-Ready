using GigaCreation.Essentials.Platforms;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.BuildTimestampDisplay;
using Il2CppDummyDll;
using System;
using TMPro;
using UniRx;
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
	private string _format = "yyyy/MM/dd HH:mm:ss";

	[Token(Token = "0x40007F5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	[SerializeField]
	private float _utcOffsetHours;

	// RECONSTRUCTED | Confidence: A | RVA: 0x3AA620, 297 B; lambda <Start>b__11_0 0x3ABC00 (instance method, no display class)
	// 
	// Result window hidden (alpha 0, not interactable, no raycasts), then
	// `_uploadButton.onClick.AsObservable().Subscribe(...).AddTo(this)`. The lambda starts
	// `UploadAsync(destroyCancellationToken)` (builder stub inlined) and Cysharp-`Forget()`s it.
	[Token(Token = "0x600077B")]
	[Address(RVA = "0x3AA620", Offset = "0x3A9020", VA = "0x1803AA620")]
	private void Start()
	{
		_resultWindowCanvasGroup.alpha = 0f;
		_resultWindowCanvasGroup.interactable = false;
		_resultWindowCanvasGroup.blocksRaycasts = false;
		_uploadButton.onClick.AsObservable().Subscribe(delegate
		{
			UploadAsync(destroyCancellationToken).Forget();
		}).AddTo(this);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3AA750
	[Token(Token = "0x600077C")]
	[Address(RVA = "0x3AA750", Offset = "0x3A9150", VA = "0x1803AA750")]
	private void Reset()
	{
		_uploadButton = GetComponent<Button>();
	}

	// RECONSTRUCTED | Confidence: A (overall), B (unnamed reused local `screenshotPath`) | RVA: 0x3AA7F0 (stub); MoveNext 0x3AC280, 8576 B
	// 
	// - Hoisted locals: timeStamp +0x28, csvFileName +0x30, jpgFileName +0x38, csv +0x40, jpg +0x48,
	//   storedTimeScale +0x50, resultIndex +0x54, postMessage +0x80, slackFileIds +0x88 (+0x90 is the
	//   spilled array for `slackFileIds[i] = await ...`), logPath +0xC0.
	// - `Time.timeScale` (icalls 0x2608FD0 / 0x2608FF8) is saved and set to 0 while the result window
	//   (alpha 1, raycasts, interactable) waits for a button. `_resultDescriptionLabel` gets `string.Empty`.
	// - The two branches are separate `if`s (after the Slack branch control re-tests `resultIndex == 2`).
	//   - 1 = Slack: jpg then csv (snippet type 'csv') uploads, CompleteUploadAsync, a Concat4 file list,
	//     `UniTask.Delay(3 s, DelayType.UnscaledDeltaTime (r8d=1, overload 0x180D710), Update, ct)`.
	//   - 2 = desktop: `Environment.GetFolderPath(Desktop)` / productName + '_デバッグ情報' / timeStamp;
	//     the screenshot path reuses the postMessage slot (+0x80), so its source name is lost.
	//     Directory created if missing; `await (WriteAllBytesAsync(...).AsUniTask(),
	//     WriteAllTextAsync(...).AsUniTask())` via the ValueTuple GetAwaiter; then
	//     `UniTask.Delay(3 s, ignoreTimeScale: true (bool overload 0x180D530), Update, ct)`.
	// - `AsUniTask` is called statically: extension lookup would also bind UniTask's ValueTask overloads,
	//   whose mscorlib type reference the DummyDll set cannot resolve (CS7069). Same call target.
	// - Close: alpha 0, blocksRaycasts false, timeScale restored. No try/finally: an exception leaves the
	//   game paused.
	[Token(Token = "0x600077D")]
	[Address(RVA = "0x3AA7F0", Offset = "0x3A91F0", VA = "0x1803AA7F0")]
	[AsyncStateMachine(typeof(_003CUploadAsync_003Ed__13))]
	private async UniTask UploadAsync([Optional] CancellationToken ct)
	{
		string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
		string csvFileName = "Log_" + timeStamp + ".csv";
		string jpgFileName = "Screenshot_" + timeStamp + ".jpg";
		string csv = BuildLogAsCsv();
		byte[] jpg = await CaptureScreenshotAsJpegAsync(ct);
		float storedTimeScale = Time.timeScale;
		Time.timeScale = 0f;
		_resultHeadingLabel.SetText("デバッグ情報ファイルを生成しました。");
		_resultDescriptionLabel.SetText(string.Empty);
		_resultWindowCanvasGroup.alpha = 1f;
		_resultWindowCanvasGroup.blocksRaycasts = true;
		_resultWindowCanvasGroup.interactable = true;
		int resultIndex = await WaitForClickResultButtonAsync(ct);
		_resultWindowCanvasGroup.interactable = false;
		if (resultIndex == 1)
		{
			_resultHeadingLabel.SetText("ファイルをアップロード中……");
			string postMessage = BuildPostMessage();
			string[] slackFileIds = new string[2];
			slackFileIds[0] = await _slackApp.UploadFileAsync(jpg, jpgFileName, ct);
			slackFileIds[1] = await _slackApp.UploadFileAsync(csv, csvFileName, "csv", ct);
			await _slackApp.CompleteUploadAsync(slackFileIds, postMessage, ct);
			_resultHeadingLabel.SetText("Slack に以下のファイルを投稿しました。");
			_resultDescriptionLabel.SetText("- " + jpgFileName + "\n- " + csvFileName);
			await UniTask.Delay(TimeSpan.FromSeconds(3.0), DelayType.UnscaledDeltaTime, PlayerLoopTiming.Update, ct);
		}
		if (resultIndex == 2)
		{
			string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Application.productName + "_デバッグ情報", timeStamp);
			string screenshotPath = Path.Combine(directory, jpgFileName);
			string logPath = Path.Combine(directory, csvFileName);
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}
			await (Cysharp.Threading.Tasks.UniTaskExtensions.AsUniTask(File.WriteAllBytesAsync(screenshotPath, jpg, ct)), Cysharp.Threading.Tasks.UniTaskExtensions.AsUniTask(File.WriteAllTextAsync(logPath, csv, ct)));
			_resultHeadingLabel.SetText("デスクトップに以下のファイルを保存しました。");
			_resultDescriptionLabel.SetText("- " + screenshotPath + "\n- " + logPath);
			await UniTask.Delay(TimeSpan.FromSeconds(3.0), ignoreTimeScale: true, PlayerLoopTiming.Update, ct);
		}
		_resultWindowCanvasGroup.alpha = 0f;
		_resultWindowCanvasGroup.blocksRaycasts = false;
		Time.timeScale = storedTimeScale;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3AA990 (stub); MoveNext 0x3AE400
	// 
	// `using linkedCts = CreateLinkedTokenSource(ct)` (params overload, 1-element array). Three inlined
	// `OnClickAsync` handlers (Cancel +0x50 = 0, PostToSlack +0x58 = 1, SaveToLocal +0x60 = 2) go into
	// `UniTask.WhenAny(params UniTask[])` (WhenAnyPromise inlined). Then inlined `Cancel()` and the index.
	[Token(Token = "0x600077E")]
	[Address(RVA = "0x3AA990", Offset = "0x3A9390", VA = "0x1803AA990")]
	[AsyncStateMachine(typeof(_003CWaitForClickResultButtonAsync_003Ed__14))]
	private async UniTask<int> WaitForClickResultButtonAsync([Optional] CancellationToken ct)
	{
		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
		int resultIndex = await UniTask.WhenAny(_cancelButton.OnClickAsync(linkedCts.Token), _postToSlackButton.OnClickAsync(linkedCts.Token), _saveToLocalButton.OnClickAsync(linkedCts.Token));
		linkedCts.Cancel();
		return resultIndex;
	}

	// RECONSTRUCTED | Confidence: A (`CaptureScreenshotAsTexture()` default-argument spelling: B) | RVA: 0x3AAB80 (stub); MoveNext 0x3ABDA0
	// 
	// `UniTask.WaitForEndOfFrame(ct)` (async overload, its builder stub inlined), then icall
	// `ScreenCapture::CaptureScreenshotAsTexture_Injected(1, LeftEye (1))`, `EncodeToJPG(75)`, `Object.Destroy`.
	[Token(Token = "0x600077F")]
	[Address(RVA = "0x3AAB80", Offset = "0x3A9580", VA = "0x1803AAB80")]
	[AsyncStateMachine(typeof(_003CCaptureScreenshotAsJpegAsync_003Ed__15))]
	private static async UniTask<byte[]> CaptureScreenshotAsJpegAsync([Optional] CancellationToken ct)
	{
		await UniTask.WaitForEndOfFrame(ct);
		Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
		byte[] jpg = texture.EncodeToJPG(75);
		UnityEngine.Object.Destroy(texture);
		return jpg;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3AAD20, 2523 B
	// 
	// Two-column CSV built with chained `Append(key).Append(",").AppendLine(value)` (AppendLine(string)
	// inlined as Append + Append(NewLine)). Values: DateTime.Now in `_format`, '---' separators,
	// BuildTimestamp.ToString(_format, (double)_utcOffsetHours), Application.version,
	// Platform.CurrentPlatform.ToName(), Screen.currentResolution (icall 0x25F3CA0).ToString().
	// Engine not initialized -> single line; otherwise playback spot (fallback 'No playback spot.') and the
	// printed message through inlined DebugInfoBuilder.Quote (fallback 'No message.').
	[Token(Token = "0x6000780")]
	[Address(RVA = "0x3AAD20", Offset = "0x3A9720", VA = "0x1803AAD20")]
	private string BuildLogAsCsv()
	{
		StringBuilder builder = new StringBuilder();
		builder.Append("Log timestamp").Append(",").AppendLine(DateTime.Now.ToString(_format));
		builder.Append("---").Append(",").AppendLine("---");
		builder.Append("Build timestamp").Append(",").AppendLine(_buildTimestamp.ToString(_format, _utcOffsetHours));
		builder.Append("Version").Append(",").AppendLine(Application.version);
		builder.Append("Platform").Append(",").AppendLine(Platform.CurrentPlatform.ToName());
		builder.Append("Resolution").Append(",").AppendLine(Screen.currentResolution.ToString());
		builder.Append("---").Append(",").AppendLine("---");
		if (!Naninovel.Engine.Initialized)
		{
			builder.AppendLine("Novel engine is not initialized.");
		}
		else
		{
			string spot = DebugInfoBuilder.TryGetPlaybackSpot(out string playbackSpot) ? playbackSpot : "No playback spot.";
			builder.Append("Playback spot").Append(",").AppendLine(spot);
			string message = DebugInfoBuilder.TryGetPrintedMessage(out string printedMessage) ? DebugInfoBuilder.Quote(printedMessage) : "No message.";
			builder.Append("Printed message").Append(",").AppendLine(message);
		}
		return builder.ToString();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3AB700, 1067 B
	// 
	// `new StringBuilder("テストプレイ報告 ")`, then AppendLine(DateTime.Now in `_format`), the playback
	// spot if any, and the printed message fenced with ``` if any (all AppendLine(string) inlined).
	[Token(Token = "0x6000781")]
	[Address(RVA = "0x3AB700", Offset = "0x3AA100", VA = "0x1803AB700")]
	private string BuildPostMessage()
	{
		StringBuilder builder = new StringBuilder("テストプレイ報告 ");
		builder.AppendLine(DateTime.Now.ToString(_format));
		if (DebugInfoBuilder.TryGetPlaybackSpot(out string spot))
		{
			builder.AppendLine(spot);
		}
		if (DebugInfoBuilder.TryGetPrintedMessage(out string message))
		{
			builder.AppendLine("```");
			builder.AppendLine(message);
			builder.AppendLine("```");
		}
		return builder.ToString();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3ABB30, 194 B
	// Only the field initializer `_format = "yyyy/MM/dd HH:mm:ss"` (+0x68), then the MonoBehaviour ctor.
	[Token(Token = "0x6000782")]
	[Address(RVA = "0x3ABB30", Offset = "0x3AA530", VA = "0x1803ABB30")]
	public DebugReporter()
	{
	}
}
