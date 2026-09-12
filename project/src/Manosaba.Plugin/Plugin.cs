using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace Manosaba.Plugin;

/// <summary>
/// manosaba 插件骨架（BepInEx 6 IL2CPP）。
///   * 构建：dotnet build src\Manosaba.Plugin\Manosaba.Plugin.csproj
///   * 部署：构建后自动复制到 &lt;游戏&gt;\BepInEx\plugins\
///   * 启动：VS Code → Run and Debug → "Launch game (BepInEx)"，或直接运行 manosaba.exe
///
/// 注意：方法体在 IL2CPP 里是原生代码，改写逻辑要用 Harmony 补丁（见 PatchExample 注释）。
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

        // Harmony 补丁示例（取消注释并改成你想挂的方法）：
        // var h = new Harmony(Guid);
        // h.PatchAll(typeof(PatchExample));
    }
}

// [HarmonyPatch(typeof(WitchTrials.Views.SomeClass), "SomeMethod")]
// internal static class PatchExample
// {
//     [HarmonyPrefix]
//     private static bool Prefix() => true;      // 返回 false 表示跳过原方法
// }
