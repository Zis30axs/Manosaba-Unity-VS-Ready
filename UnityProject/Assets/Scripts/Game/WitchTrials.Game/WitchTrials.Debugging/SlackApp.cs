using GigaCreation.Tools.Service;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.WebRequestUtils;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace WitchTrials.Debugging
{
    [Token(Token = "0x20001B6")]
    public class SlackApp : MonoBehaviour
    {
    	[Token(Token = "0x20001B7")]
    	public class UploadUrlExternalJson
    	{
    		[Token(Token = "0x400081A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		[JsonProperty("upload_url")]
    		public string UploadUrl;

    		[Token(Token = "0x400081B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		[JsonProperty("file_id")]
    		public string FileId;

    		[Token(Token = "0x6000794")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public UploadUrlExternalJson()
    		{
    		}
    	}

    	[Token(Token = "0x20001B8")]
    	public class CompleteUploadExternalJson
    	{
    		[Token(Token = "0x400081C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		[JsonProperty("id")]
    		public string Id;

    		[Token(Token = "0x6000795")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public CompleteUploadExternalJson()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20001BA")]
    	[CompilerGenerated]
    	private struct _003CCompleteUploadAsync_003Ed__12
    {
    		[Token(Token = "0x400081F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000820")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000821")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public string[] fileIds;

    		[Token(Token = "0x4000822")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public SlackApp _003C_003E4__this;

    		[Token(Token = "0x4000823")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string message;

    		[Token(Token = "0x4000824")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000825")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UnityWebRequest _003Crequest3_003E5__2;

    		[Token(Token = "0x4000826")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask<string>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000799")]
    		[Address(RVA = "0x3AFDA0", Offset = "0x3AE7A0", VA = "0x1803AFDA0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600079A")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20001BB")]
    	[CompilerGenerated]
    	private struct _003CDeleteMessageAsync_003Ed__14
    {
    		[Token(Token = "0x4000827")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000828")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000829")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public string deletePostUrl;

    		[Token(Token = "0x400082A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public SlackApp _003C_003E4__this;

    		[Token(Token = "0x400082B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x400082C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UnityWebRequest _003Crequest2_003E5__2;

    		[Token(Token = "0x400082D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<string>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600079B")]
    		[Address(RVA = "0x3B06F0", Offset = "0x3AF0F0", VA = "0x1803B06F0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600079C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20001BC")]
    	[CompilerGenerated]
    	private struct _003CUploadFileAsync_003Ed__10
    {
    		[Token(Token = "0x400082E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400082F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<string> _003C_003Et__builder;

    		[Token(Token = "0x4000830")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public SlackApp _003C_003E4__this;

    		[Token(Token = "0x4000831")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string text;

    		[Token(Token = "0x4000832")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public string filename;

    		[Token(Token = "0x4000833")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public string snippetType;

    		[Token(Token = "0x4000834")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000835")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask<string>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600079D")]
    		[Address(RVA = "0x3B0E30", Offset = "0x3AF830", VA = "0x1803B0E30", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600079E")]
    		[Address(RVA = "0x3B11A0", Offset = "0x3AFBA0", VA = "0x1803B11A0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20001BD")]
    	[CompilerGenerated]
    	private struct _003CUploadFileAsync_003Ed__11
    {
    		[Token(Token = "0x4000836")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000837")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<string> _003C_003Et__builder;

    		[Token(Token = "0x4000838")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public SlackApp _003C_003E4__this;

    		[Token(Token = "0x4000839")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string filename;

    		[Token(Token = "0x400083A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public byte[] binary;

    		[Token(Token = "0x400083B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public string snippetType;

    		[Token(Token = "0x400083C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		public CancellationToken ct;

    		[Token(Token = "0x400083D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UnityWebRequest _003Crequest1_003E5__2;

    		[Token(Token = "0x400083E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UploadUrlExternalJson _003CuploadUrlExternalJson_003E5__3;

    		[Token(Token = "0x400083F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private UnityWebRequest _003Crequest2_003E5__4;

    		[Token(Token = "0x4000840")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    		private UniTask<string>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600079F")]
    		[Address(RVA = "0x3B11D0", Offset = "0x3AFBD0", VA = "0x1803B11D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60007A0")]
    		[Address(RVA = "0x3B1EC0", Offset = "0x3B08C0", VA = "0x1803B1EC0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20001BE")]
    	[CompilerGenerated]
    	private struct _003CUploadFileAsync_003Ed__9
    {
    		[Token(Token = "0x4000841")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000842")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<string> _003C_003Et__builder;

    		[Token(Token = "0x4000843")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public SlackApp _003C_003E4__this;

    		[Token(Token = "0x4000844")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public byte[] binary;

    		[Token(Token = "0x4000845")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public string filename;

    		[Token(Token = "0x4000846")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000847")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask<string>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60007A1")]
    		[Address(RVA = "0x3B1EF0", Offset = "0x3B08F0", VA = "0x1803B1EF0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60007A2")]
    		[Address(RVA = "0x3B2230", Offset = "0x3B0C30", VA = "0x1803B2230", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000813")]
    	private const string GetUploadUrlExternalApi = "https://slack.com/api/files.getUploadURLExternal";

    	[Token(Token = "0x4000814")]
    	private const string CompleteUploadExternalApi = "https://slack.com/api/files.completeUploadExternal";

    	[Token(Token = "0x4000815")]
    	private const string DeleteMessageApi = "https://slack.com/api/chat.delete";

    	[Token(Token = "0x4000816")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private string _token;

    	[Token(Token = "0x4000817")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private string _channelId;

    	[Token(Token = "0x4000818")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[Space]
    	[UsedImplicitly]
    	[SerializeField]
    	private string _deletePostUrl;

    	[Token(Token = "0x4000819")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private IWebRequestManager _webRequestManager;

    	[Token(Token = "0x17000143")]
    	private IWebRequestManager WebRequestManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3AEE10
    		// Lazy cache in +0x38 via `ServiceLocator.Get<IWebRequestManager>(true)`.
    		[Token(Token = "0x600078C")]
    		[Address(RVA = "0x3AEE10", Offset = "0x3AD810", VA = "0x1803AEE10")]
    		get
    		{
    			return _webRequestManager ??= ServiceLocator.Get<IWebRequestManager>(true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3AEED0 (stub); MoveNext 0x3B1EF0
    	// Forwards to the private overload with `snippetType` null.
    	[Token(Token = "0x600078D")]
    	[Address(RVA = "0x3AEED0", Offset = "0x3AD8D0", VA = "0x1803AEED0")]
    	[AsyncStateMachine(typeof(_003CUploadFileAsync_003Ed__9))]
    	public async UniTask<string> UploadFileAsync(byte[] binary, string filename, [Optional] CancellationToken ct)
    	{
    		return await UploadFileAsync(binary, filename, null, ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3AF180 (stub); MoveNext 0x3B0E30
    	// `Encoding.UTF8` + vtable slot 18 `GetBytes(string)`, then the private overload.
    	[Token(Token = "0x600078E")]
    	[Address(RVA = "0x3AF180", Offset = "0x3ADB80", VA = "0x1803AF180")]
    	[AsyncStateMachine(typeof(_003CUploadFileAsync_003Ed__10))]
    	public async UniTask<string> UploadFileAsync(string text, string filename, string snippetType, [Optional] CancellationToken ct)
    	{
    		return await UploadFileAsync(Encoding.UTF8.GetBytes(text), filename, snippetType, ct);
    	}

    	// RECONSTRUCTED | Confidence: A (showErrorDialog argument: B) | RVA: 0x3AF480 (stub); MoveNext 0x3B11D0, 3301 B
    	// 
    	// Slack external upload, step 1: form token/filename/length (+ snippet_type when non-null), POST to
    	// GetUploadUrlExternalApi (hoisted `request1`), Newtonsoft `DeserializeObject<UploadUrlExternalJson>`.
    	// Step 2: form token + `AddBinaryData("file", binary)`, POST to `UploadUrl` (`request2`). Both requests
    	// are disposed on every exit (nested usings); returns `FileId`. The token is the serialized `_token`
    	// field; no credential is present in code.
    	// The request goes through IWebRequestManager slot 0 `SendWebRequestAsync(UnityWebRequest, int timeout,
    	// bool showErrorDialog, CancellationToken)` via thunk sub_8570, which passes timeout 5 (the default);
    	// the dialog flag is written with its default (B).
    	[Token(Token = "0x600078F")]
    	[Address(RVA = "0x3AF480", Offset = "0x3ADE80", VA = "0x1803AF480")]
    	[AsyncStateMachine(typeof(_003CUploadFileAsync_003Ed__11))]
    	private async UniTask<string> UploadFileAsync(byte[] binary, string filename, string snippetType, [Optional] CancellationToken ct)
    	{
    		WWWForm form = new WWWForm();
    		form.AddField("token", _token);
    		form.AddField("filename", filename);
    		form.AddField("length", binary.Length.ToString());
    		if (snippetType != null)
    		{
    			form.AddField("snippet_type", snippetType);
    		}
    		using UnityWebRequest request1 = UnityWebRequest.Post(GetUploadUrlExternalApi, form);
    		UploadUrlExternalJson uploadUrlExternalJson = JsonConvert.DeserializeObject<UploadUrlExternalJson>(await WebRequestManager.SendWebRequestAsync(request1, ct: ct));
    		WWWForm uploadForm = new WWWForm();
    		uploadForm.AddField("token", _token);
    		uploadForm.AddBinaryData("file", binary);
    		using UnityWebRequest request2 = UnityWebRequest.Post(uploadUrlExternalJson.UploadUrl, uploadForm);
    		await WebRequestManager.SendWebRequestAsync(request2, ct: ct);
    		return uploadUrlExternalJson.FileId;
    	}

    	// RECONSTRUCTED | Confidence: A (showErrorDialog argument: B) | RVA: 0x3AF780 (stub); MoveNext 0x3AFDA0; lambda <>c.b__12_0 0x3AFD00
    	// 
    	// `JsonConvert.SerializeObject(fileIds.Select(id => new CompleteUploadExternalJson { Id = id }).ToList())`,
    	// form token/files/channel_id (`_channelId`)/initial_comment, POST to CompleteUploadExternalApi
    	// (`request3`), and `Debug.Log` of the response.
    	// The request goes through IWebRequestManager slot 0 `SendWebRequestAsync(UnityWebRequest, int timeout,
    	// bool showErrorDialog, CancellationToken)` via thunk sub_8570, which passes timeout 5 (the default);
    	// the dialog flag is written with its default (B).
    	[Token(Token = "0x6000790")]
    	[Address(RVA = "0x3AF780", Offset = "0x3AE180", VA = "0x1803AF780")]
    	[AsyncStateMachine(typeof(_003CCompleteUploadAsync_003Ed__12))]
    	public async UniTask CompleteUploadAsync(string[] fileIds, string message, [Optional] CancellationToken ct)
    	{
    		string files = JsonConvert.SerializeObject(fileIds.Select((string id) => new CompleteUploadExternalJson
    		{
    			Id = id
    		}).ToList());
    		WWWForm form = new WWWForm();
    		form.AddField("token", _token);
    		form.AddField("files", files);
    		form.AddField("channel_id", _channelId);
    		form.AddField("initial_comment", message);
    		using UnityWebRequest request3 = UnityWebRequest.Post(CompleteUploadExternalApi, form);
    		UnityEngine.Debug.Log(await WebRequestManager.SendWebRequestAsync(request3, ct: ct));
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3AF9F0, 82 B
    	[Token(Token = "0x6000791")]
    	[Address(RVA = "0x3AF9F0", Offset = "0x3AE3F0", VA = "0x1803AF9F0")]
    	public void DeleteMessage(string deletePostUrl)
    	{
    		DeleteMessageAsync(deletePostUrl, destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A (showErrorDialog argument: B) | RVA: 0x3AFA50 (stub); MoveNext 0x3B06F0
    	// 
    	// The message timestamp is recovered from a Slack permalink: last '/' segment, drop the leading 'p',
    	// insert '.' at index 10. Form token/channel/ts, POST to DeleteMessageApi (`request2`), log the response.
    	// The request goes through IWebRequestManager slot 0 `SendWebRequestAsync(UnityWebRequest, int timeout,
    	// bool showErrorDialog, CancellationToken)` via thunk sub_8570, which passes timeout 5 (the default);
    	// the dialog flag is written with its default (B).
    	[Token(Token = "0x6000792")]
    	[Address(RVA = "0x3AFA50", Offset = "0x3AE450", VA = "0x1803AFA50")]
    	[AsyncStateMachine(typeof(_003CDeleteMessageAsync_003Ed__14))]
    	public async UniTask DeleteMessageAsync(string deletePostUrl, [Optional] CancellationToken ct)
    	{
    		string ts = deletePostUrl.Split('/').Last().Substring(1).Insert(10, ".");
    		WWWForm form = new WWWForm();
    		form.AddField("token", _token);
    		form.AddField("channel", _channelId);
    		form.AddField("ts", ts);
    		using UnityWebRequest request2 = UnityWebRequest.Post(DeleteMessageApi, form);
    		UnityEngine.Debug.Log(await WebRequestManager.SendWebRequestAsync(request2, ct: ct));
    	}

    	[Token(Token = "0x6000793")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public SlackApp()
    	{
    	}
    }
}
