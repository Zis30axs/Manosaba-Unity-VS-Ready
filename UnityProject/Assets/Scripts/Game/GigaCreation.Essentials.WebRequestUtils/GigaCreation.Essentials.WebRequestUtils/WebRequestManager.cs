using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UniRx;
using UnityEngine.Networking;

namespace GigaCreation.Essentials.WebRequestUtils
{
    [Token(Token = "0x2000007")]
    public class WebRequestManager : IWebRequestManager, IService
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000008")]
    	[CompilerGenerated]
    	private struct _003CSendWebRequestAsync_003Ed__2
    {
    		[Token(Token = "0x4000012")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000013")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<string> _003C_003Et__builder;

    		[Token(Token = "0x4000014")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public int timeout;

    		[Token(Token = "0x4000015")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000016")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public UnityWebRequest request;

    		[Token(Token = "0x4000017")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public bool showErrorDialog;

    		[Token(Token = "0x4000018")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		public WebRequestManager _003C_003E4__this;

    		[Token(Token = "0x4000019")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private CancellationTokenSource _003CtimeoutCts_003E5__2;

    		[Token(Token = "0x400001A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private object _003C_003E7__wrap2;

    		[Token(Token = "0x400001B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private int _003C_003E7__wrap3;

    		[Token(Token = "0x400001C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    		private CancellationTokenSource _003ClinkedCts_003E5__5;

    		[Token(Token = "0x400001D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    		private UniTask<UnityWebRequest>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400001E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600000C")]
    		[Address(RVA = "0xCF20B0", Offset = "0xCF0AB0", VA = "0x180CF20B0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600000D")]
    		[Address(RVA = "0xCF3B50", Offset = "0xCF2550", VA = "0x180CF3B50", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000009")]
    	[CompilerGenerated]
    	private struct _003CSendWebRequestAsync_003Ed__3
    {
    		[Token(Token = "0x400001F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000020")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<string> _003C_003Et__builder;

    		[Token(Token = "0x4000021")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string uri;

    		[Token(Token = "0x4000022")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public WebRequestManager _003C_003E4__this;

    		[Token(Token = "0x4000023")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public int timeout;

    		[Token(Token = "0x4000024")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
    		public bool showErrorDialog;

    		[Token(Token = "0x4000025")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000026")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UnityWebRequest _003Crequest_003E5__2;

    		[Token(Token = "0x4000027")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask<string>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600000E")]
    		[Address(RVA = "0xCF3B80", Offset = "0xCF2580", VA = "0x180CF3B80", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600000F")]
    		[Address(RVA = "0xCF41A0", Offset = "0xCF2BA0", VA = "0x180CF41A0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCF1F70: new Subject<WebRequestErrorData>() -> +0x10
    	[Token(Token = "0x4000010")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly ISubject<WebRequestErrorData> _showErrorDialogSubject = new Subject<WebRequestErrorData>();

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCF1F70: new WebRequestErrorData() -> +0x18
    	[Token(Token = "0x4000011")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly WebRequestErrorData _errorData = new WebRequestErrorData();

    	[Token(Token = "0x6000008")]
    	[Address(RVA = "0xCF1A90", Offset = "0xCF0490", VA = "0x180CF1A90", Slot = "4")]
    	[AsyncStateMachine(typeof(_003CSendWebRequestAsync_003Ed__2))]
    	public UniTask<string> SendWebRequestAsync(UnityWebRequest request, int timeout = 5, bool showErrorDialog = true, CancellationToken ct = default)
    	{
    		return default(UniTask<string>);
    	}

    	[Token(Token = "0x6000009")]
    	[Address(RVA = "0xCF1D00", Offset = "0xCF0700", VA = "0x180CF1D00", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CSendWebRequestAsync_003Ed__3))]
    	public UniTask<string> SendWebRequestAsync(string uri, int timeout = 5, bool showErrorDialog = true, CancellationToken ct = default)
    	{
    		return default(UniTask<string>);
    	}

    	[Token(Token = "0x600000A")]
    	[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "6")]
    	public IObservable<WebRequestErrorData> OnRequestToShowErrorDialogAsObservable()
    	{
    		return null;
    	}

    	[Token(Token = "0x600000B")]
    	[Address(RVA = "0xCF1F70", Offset = "0xCF0970", VA = "0x180CF1F70")]
    	public WebRequestManager()
    	{
    	}
    }
}
