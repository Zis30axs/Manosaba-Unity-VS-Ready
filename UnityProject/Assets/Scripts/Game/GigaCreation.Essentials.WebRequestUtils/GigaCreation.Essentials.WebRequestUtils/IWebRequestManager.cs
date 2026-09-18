using System;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine.Networking;

namespace GigaCreation.Essentials.WebRequestUtils
{
    [Token(Token = "0x2000004")]
    public interface IWebRequestManager : IService
    {
    	[Token(Token = "0x6000003")]
    	UniTask<string> SendWebRequestAsync(UnityWebRequest request, int timeout = 5, bool showErrorDialog = true, CancellationToken ct = default);

    	[Token(Token = "0x6000004")]
    	UniTask<string> SendWebRequestAsync(string uri, int timeout = 5, bool showErrorDialog = true, CancellationToken ct = default);

    	[Token(Token = "0x6000005")]
    	IObservable<WebRequestErrorData> OnRequestToShowErrorDialogAsObservable();
    }
}
