using BepInEx;
using BepInEx.Unity.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using WitchTrials.Debugging;
using WitchTrials.Models;
using WitchTrials.Views;

namespace Manosaba.Plugin;

/// <summary>
/// manosaba 插件骨架（BepInEx 6.0.0-be.788+，Il2CppInterop 后端，CoreCLR 宿主）。
///   * 构建：dotnet build src\Manosaba.Plugin\Manosaba.Plugin.csproj
///   * 部署：构建后自动复制到 &lt;游戏&gt;\BepInEx\plugins\
///   * 启动：VS Code → Run and Debug → "Launch game (BepInEx)"，或直接运行 manosaba.exe
///
/// 已验证 2026-09-13：装 BepInEx（tools/install_bepinex.ps1）→ 启动游戏 → 本插件成功加载，
/// LogOutput.log 里能看到 "Loading [Manosaba Plugin 1.0.0]" 到 "Chainloader startup complete"
/// 全程无异常。见 work/reversing/README.md「运行时验证通道」一节。
///
/// 注意：方法体在 IL2CPP 里是原生代码，`BepInEx\interop\*.dll` 里看到的是 Il2CppInterop
/// 生成的调用封装（marshal 参数 -> il2cpp_runtime_invoke -> marshal 返回值），
/// **不是**反编译出来的真实逻辑——那些方法体仍然只能通过 Ghidra/IDA/capstone 静态分析
/// （见 RECONSTRUCTION.md）复原。这条 Harmony 通道的价值是反过来的：
/// 用它验证"已经静态复原的方法"在真实运行时是否表现一致（打印真实参数/字段值），
/// 而不是用它替代静态复原。
/// </summary>
[BepInPlugin(Guid, "Manosaba Plugin", "1.0.0")]
public class Plugin : BasePlugin
{
    public const string Guid = "com.example.manosaba.plugin";

    internal static ManualLogSource Log;

    public override void Load()
    {
        Log = base.Log;
        Log.LogInfo("Manosaba.Plugin loaded.");

        // 游戏自己的程序集已经由 BepInEx 生成代理，可以直接用类型名：
        // var gl = new GameObject("ManosabaPlugin");
        // Object.DontDestroyOnLoad(gl);

        var h = new Harmony(Guid);
        h.PatchAll(typeof(TrialStateManagerVerification));
        h.PatchAll(typeof(GameInitializerVerification));
    }
}

/// <summary>
/// 验证 RECONSTRUCTION.md 里 TrialStateManager 的静态复原结果（Confidence A 的几个方法）。
/// Postfix 打印真实参数和复原时用到的字段，能直接和静态分析里写的注释对照。
/// 想验证其他方法时复制这个模式即可：换 [HarmonyPatch] 的类型/方法名，
/// 在 Postfix 里读 __instance 的字段（Traverse 或反射，因为字段是 private）。
/// </summary>
[HarmonyPatch(typeof(TrialStateManager), nameof(TrialStateManager.InitializeService))]
internal static class TrialStateManagerVerification
{
    // InitializeService 是 Naninovel IEngineService 生命周期方法，引擎启动时自动调用一次，
    // 不需要玩家做任何操作就能触发——用它做首个验证目标，比要等玩家进庭审的
    // RegisterCurrentStage 更适合"启动就看结果"。
    //
    // RECONSTRUCTION.md 对 RVA 0x39D3A0 的声称：
    //   _stageManager = ServiceLocator.Get<IPerspectiveStageManager>(includeDerivedType: true);
    //   _cameraStates = new List<CameraState>();
    //   _screenStates = new List<ScreenState>();
    [HarmonyPostfix]
    private static void Postfix(TrialStateManager __instance)
    {
        // NOTE: Il2CppInterop wraps every IL2CPP field (private or not) as a public C#
        // property with hand-written get/set doing pointer arithmetic into native
        // memory -- there is no real backing field, so Traverse.Field("_x") silently
        // finds nothing and .GetValue() returns null without throwing. First attempt
        // at this used Traverse.Field and printed "null" for all three; that was a bug
        // in the verification code, not evidence against the reconstruction. Direct
        // property access is both simpler and correct here.
        var stageManager = __instance._stageManager;
        var cameraStates = __instance._cameraStates;
        var screenStates = __instance._screenStates;
        Plugin.Log.LogInfo(
            $"[verify] InitializeService() -> _stageManager={(stageManager == null ? "null" : stageManager.GetType().Name)} "
            + $"_cameraStates={(cameraStates == null ? "null" : "non-null")} "
            + $"_screenStates={(screenStates == null ? "null" : "non-null")}");
    }
}

/// <summary>
/// 验证 RECONSTRUCTION.md 里 GameInitializer::Start（游戏启动入口）的静态复原结果。
/// RVA 0x30FB70 声称：
///   _steamManager.gameObject.SetActive(!DebugOptions.IsDebugMode || DebugOptions.EnableSteamworks);
///   InitializeAsync(destroyCancellationToken).Forget();
/// 这条 Start() 是私有方法，Harmony 一样能挂（GetMethod 时带 NonPublic）。
/// </summary>
[HarmonyPatch(typeof(GameInitializer), "Start")]
internal static class GameInitializerVerification
{
    [HarmonyPostfix]
    private static void Postfix(GameInitializer __instance)
    {
        bool isDebugMode = DebugOptions.IsDebugMode;
        bool enableSteamworks = DebugOptions.EnableSteamworks;
        bool expected = !isDebugMode || enableSteamworks;
        var steamManager = __instance._steamManager;
        bool? actualActive = steamManager?.gameObject?.activeSelf;
        Plugin.Log.LogInfo(
            $"[verify] GameInitializer.Start() -> IsDebugMode={isDebugMode} EnableSteamworks={enableSteamworks} "
            + $"expectedSteamManagerActive={expected} actualSteamManagerActive={actualActive} "
            + $"match={(actualActive.HasValue && actualActive.Value == expected)}");
    }
}
