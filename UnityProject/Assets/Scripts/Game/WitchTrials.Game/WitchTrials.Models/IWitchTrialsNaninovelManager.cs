using System;
using GigaCreation.NaninovelExtender.Core;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UniRx;

namespace WitchTrials.Models
{
    [Token(Token = "0x2000178")]
    public interface IWitchTrialsNaninovelManager : INaninovelManager, IService, INaninovelStateSaver, IDisposable
    {
    	[Token(Token = "0x1700010B")]
    	bool IsInDebate
    	{
    		[Token(Token = "0x600064F")]
    		get;
    	}

    	[Token(Token = "0x1700010C")]
    	bool IsPresentEvidenceMode
    	{
    		[Token(Token = "0x6000650")]
    		get;
    		[Token(Token = "0x6000651")]
    		set;
    	}

    	[Token(Token = "0x6000652")]
    	void SetTitleScreenShown(bool isIn);

    	[Token(Token = "0x6000653")]
    	void ChangeTrialMode(bool enable);

    	[Token(Token = "0x6000654")]
    	void SetTrialTimer(float time);

    	[Token(Token = "0x6000655")]
    	void ChangeDebateMode(bool enable);

    	[Token(Token = "0x6000656")]
    	void ChangeTrialPauseMode(bool enable, bool processTimer);

    	[Token(Token = "0x6000657")]
    	IObservable<Unit> OnEnterTitleScreenAsObservable();

    	[Token(Token = "0x6000658")]
    	IObservable<TimeSpan> OnRemainingTimeChangedAsObservable();

    	[Token(Token = "0x6000659")]
    	IObservable<bool> OnDebateSkippableChangedAsObservable();
    }
}
