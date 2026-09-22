using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;

namespace GigaCreation.Tools.Debugging.Core
{
    [Token(Token = "0x2000005")]
    public class DebugPresenter : MonoBehaviour
    {
    	[Token(Token = "0x4000007")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private bool _forceReleaseBuild;

    	[Token(Token = "0x4000008")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private BoolReactiveProperty _isDebugMode;

    	[Token(Token = "0x4000009")]
    	[FieldOffset(Offset = "0x30")]
    	private IDebugManager _debugManager;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD42140, 832 B
    	// - Debug.isDebugBuild (icall through data_30E6790) false, or `_forceReleaseBuild` (+0x20) -> Destroy(this) (0x180D4245E):
    	//   a release build registers no IDebugManager, which is what DestroySelfIfReleaseBuild and DebugDisplay test.
    	// - An IDebugManager already registered (ServiceLocator.TryGet into +0x30): FindObjectsByType<DebugPresenter>(1, 0)
    	//   -- a second presenter destroys itself (0x180D423EC), the only one links its flags (0x180D42400).
    	// - Otherwise +0x30 = new DebugManager(_isDebugMode (+0x28).Value) (ReactiveProperty<bool>.get_Value, ctor inlined),
    	//   LinkDebugModeFlags, then ServiceLocator.Register<IDebugManager> (tail call 0x180752130).
    	// Annotated disassembly: work/title-path/disas/dp_01_Awake.txt
    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0xD42140", Offset = "0xD40B40", VA = "0x180D42140")]
    	private void Awake()
    	{
    		if (!Debug.isDebugBuild || _forceReleaseBuild)
    		{
    			Destroy(this);
    			return;
    		}
    		if (ServiceLocator.TryGet(out _debugManager))
    		{
    			if (FindObjectsByType<DebugPresenter>(FindObjectsInactive.Include, FindObjectsSortMode.None).Length != 1)
    			{
    				Destroy(this);
    				return;
    			}
    			LinkDebugModeFlags(_debugManager);
    			return;
    		}
    		_debugManager = new DebugManager(_isDebugMode.Value);
    		LinkDebugModeFlags(_debugManager);
    		ServiceLocator.Register(_debugManager);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD42480, 104 B -- ServiceLocator.Unregister<IDebugManager>(+0x30), tail call
    	// Annotated disassembly: work/title-path/disas/dp_02_OnApplicationQuit.txt
    	[Token(Token = "0x6000007")]
    	[Address(RVA = "0xD42480", Offset = "0xD40E80", VA = "0x180D42480")]
    	private void OnApplicationQuit()
    	{
    		ServiceLocator.Unregister(_debugManager);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD424F0, 639 B -- two-way link through <>c__DisplayClass5_0 {this, debugManager}:
    	//   IDebugManager.IsDebugMode (slot 0) -> b__0 (0xD42780: `_isDebugMode` (+0x28).Value = x), AddTo(this);
    	//   `_isDebugMode`.SkipLatestValueOnSubscribe() -> b__1 (0xD427F0: debugManager.IsDebugMode.Value = x, IReactiveProperty
    	//   slot 1 set_Value), AddTo(this)
    	// Annotated disassembly: work/title-path/disas/dp_03_LinkDebugModeFlags, dp_04_lambda0, dp_05_lambda1.txt
    	[Token(Token = "0x6000008")]
    	[Address(RVA = "0xD424F0", Offset = "0xD40EF0", VA = "0x180D424F0")]
    	private void LinkDebugModeFlags(IDebugManager debugManager)
    	{
    		debugManager.IsDebugMode.Subscribe((bool x) => _isDebugMode.Value = x).AddTo(this);
    		_isDebugMode.SkipLatestValueOnSubscribe().Subscribe((bool x) => debugManager.IsDebugMode.Value = x).AddTo(this);
    	}

    	[Token(Token = "0x6000009")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DebugPresenter()
    	{
    	}
    }
}
