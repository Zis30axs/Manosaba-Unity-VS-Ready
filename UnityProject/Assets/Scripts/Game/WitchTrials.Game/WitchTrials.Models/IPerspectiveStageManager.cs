using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models
{
    [Token(Token = "0x200018B")]
    public interface IPerspectiveStageManager : IPerspectiveStageLoader, IPerspectiveStageScreenModifier, IPerspectiveStageCameraModifier, ICourtModifier, IService
    {
    	[Token(Token = "0x1700011B")]
    	Vector3 StageOffset
    	{
    		[Token(Token = "0x60006B8")]
    		get;
    	}
    }
}
