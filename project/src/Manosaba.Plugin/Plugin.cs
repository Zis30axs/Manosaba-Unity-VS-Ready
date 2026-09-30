using System;
using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;
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
///
/// 关于字段访问方式（2026-09-30 修订）：
///   下面读字段一律走 <see cref="Probe"/> 反射，而不是直接写 `__instance._stageManager`。
///   原因是这个工程要在两种引用程序集下都能编译：
///     1. 游戏里 BepInEx 生成的 Il2CppInterop 代理程序集 —— 它把每个 IL2CPP 字段
///        （无论 private 与否）包成**公有属性**，所以 `__instance._stageManager` 能用；
///     2. 仓库自带的 Il2CppDumper 引用存根 src/il2cpp/DummyDll —— 那里 `_stageManager`
///        仍是**私有字段**，直接点出来是 CS0122/CS1061，编译不过。
///   反射在两种情况下都能拿到值，于是同一份源码在"装了游戏的本机"和"干净克隆"下
///   都能编译；运行时行为不变。
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
/// 反射读取 IL2CPP 字段/静态成员的辅助方法。
/// 代理程序集里字段是属性、DummyDll 里字段是私有字段，两种都要能读到，
/// 且静态成员可能声明在基类上（如 DebugOptions.IsDebugMode 继承自 DebugOptionsBase），
/// 所以要沿基类链查找。
/// </summary>
internal static class Probe
{
    private const BindingFlags AnyInstance = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    private const BindingFlags AnyStatic = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

    /// <summary>读实例成员：先找属性（代理程序集），再找字段（DummyDll），沿基类链查找。</summary>
    internal static object Member(object instance, string name)
    {
        if (instance == null) return null;
        for (var t = instance.GetType(); t != null; t = t.BaseType)
        {
            var p = t.GetProperty(name, AnyInstance);
            if (p != null) return p.GetValue(instance);
            var f = t.GetField(name, AnyInstance);
            if (f != null) return f.GetValue(instance);
        }
        return null;
    }

    /// <summary>读静态成员：同样先属性后字段，沿基类链查找。</summary>
    internal static object Static(Type type, string name)
    {
        for (var t = type; t != null; t = t.BaseType)
        {
            var p = t.GetProperty(name, AnyStatic);
            if (p != null) return p.GetValue(null);
            var f = t.GetField(name, AnyStatic);
            if (f != null) return f.GetValue(null);
        }
        return null;
    }

    /// <summary>把成员值格式化成日志友好的字符串；读不到时明确写出 &lt;not found&gt; 而不是伪装成 null。</summary>
    internal static string Describe(object value)
    {
        if (value == null) return "null";
        if (value is bool b) return b ? "true" : "false";
        return value.GetType().Name;
    }
}

/// <summary>
/// 验证 RECONSTRUCTION.md 里 TrialStateManager 的静态复原结果（Confidence A 的几个方法）。
/// Postfix 打印真实参数和复原时用到的字段，能直接和静态分析里写的注释对照。
/// 想验证其他方法时复制这个模式即可：换 [HarmonyPatch] 的类型/方法名，
/// 在 Postfix 里读 __instance 的字段（见 Probe 的说明）。
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
        // NOTE: 早先这里写成 `__instance._stageManager` 直接取属性，在装了游戏、
        // BepInEx 生成了代理程序集的本机上能编译；但干净克隆里只有 DummyDll，
        // 字段是 private，同样一行会 CS1061 编译失败——整个解决方案跟着挂。
        // 改走 Probe 反射后两种引用下都能编译。运行时值不变。
        //
        // 另外注意：不要用 Harmony 的 Traverse.Field("_x")，代理程序集里根本没有真实
        // 后备字段，Traverse 会静默返回 null 而不报错（当初踩过这个坑，不是复原结论有误）。
        var stageManager = Probe.Member(__instance, "_stageManager");
        var cameraStates = Probe.Member(__instance, "_cameraStates");
        var screenStates = Probe.Member(__instance, "_screenStates");
        Plugin.Log.LogInfo(
            $"[verify] InitializeService() -> _stageManager={Probe.Describe(stageManager)} "
            + $"_cameraStates={Probe.Describe(cameraStates)} "
            + $"_screenStates={Probe.Describe(screenStates)}");
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
        // DebugOptions.IsDebugMode / EnableSteamworks 同样是"运行时存在、DummyDll 里没有"
        // 的静态成员（DummyDll 里连 IsDebugMode 这个名字都搜不到），所以也走反射。
        bool isDebugMode = Probe.Static(typeof(DebugOptions), "IsDebugMode") as bool? ?? false;
        bool enableSteamworks = Probe.Static(typeof(DebugOptions), "EnableSteamworks") as bool? ?? false;
        bool expected = !isDebugMode || enableSteamworks;
        var steamManager = Probe.Member(__instance, "_steamManager");
        // steamManager 是 IL2CPP 对象，.gameObject 也是代理属性，这里同样反射取。
        var gameObject = Probe.Member(steamManager, "gameObject");
        bool? actualActive = Probe.Member(gameObject, "activeSelf") as bool?;
        Plugin.Log.LogInfo(
            $"[verify] GameInitializer.Start() -> IsDebugMode={isDebugMode} EnableSteamworks={enableSteamworks} "
            + $"expectedSteamManagerActive={expected} actualSteamManagerActive={actualActive} "
            + $"match={(actualActive.HasValue && actualActive.Value == expected)}");
    }
}
