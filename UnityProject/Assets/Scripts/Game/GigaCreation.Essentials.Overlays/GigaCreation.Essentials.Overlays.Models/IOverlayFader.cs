#nullable enable annotations
﻿using System;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Overlays.Models
{
    [Token(Token = "0x2000007")]
    public interface IOverlayFader : IService
    {
    	[Token(Token = "0x17000001")]
    	UniTask FadeTask
    	{
    		[Token(Token = "0x600000C")]
    		set;
    	}

    	[Token(Token = "0x600000D")]
    	void FadeOut(float duration, [Optional] Color? color);

    	[Token(Token = "0x600000E")]
    	UniTask FadeOutAsync(float duration, [Optional] Color? color, [Optional] CancellationToken ct);

    	[Token(Token = "0x600000F")]
    	void FadeIn(float duration, [Optional] Color? color);

    	[Token(Token = "0x6000010")]
    	UniTask FadeInAsync(float duration, [Optional] Color? color, [Optional] CancellationToken ct);

    	[Token(Token = "0x6000011")]
    	IObservable<FadeOverlayRequest> OnRequestToFadeOverlayAsObservable();
    }
}
