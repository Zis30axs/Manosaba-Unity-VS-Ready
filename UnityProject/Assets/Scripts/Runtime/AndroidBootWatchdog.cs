// Development players on Android log what the boot is waiting for.
//
// A black screen on a device says nothing about where the start-up is stuck, and the managed log is empty when nothing throws.
// Every few seconds this reports the frame counter, whether Naninovel and Addressables have initialised, the loaded scenes and
// the number of live canvases/cameras, which is enough to tell "still loading" from "waiting for something that never completes".
#if UNITY_ANDROID && !UNITY_EDITOR && DEVELOPMENT_BUILD
using System;
using System.Collections;
using System.Linq;
using Naninovel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Manosaba.Integration
{
    public sealed class AndroidBootWatchdog : MonoBehaviour
    {
        private const float IntervalSeconds = 2f;
        private const int Reports = 1800;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Create()
        {
            Engine.OnInitializationStarted += () => Debug.Log("[AndroidBootWatchdog] naninovel initialization started");
            Engine.OnInitializationProgress += LogProgress;
            Engine.OnInitializationFinished += () => Debug.Log("[AndroidBootWatchdog] naninovel initialization finished");
            var host = new GameObject("AndroidBootWatchdog");
            DontDestroyOnLoad(host);
            host.AddComponent<AndroidBootWatchdog>();
        }

        private static float lastProgress = -1f;

        // Progress is .25 + .5 * (i / services) while service i initialises, so a stall names the service that never completes.
        private static void LogProgress(float progress)
        {
            if (Mathf.Approximately(progress, lastProgress)) return;
            lastProgress = progress;
            var services = Engine.Services.ToList();
            int index = services.Count == 0 ? -1 : Mathf.RoundToInt((progress - .25f) / .5f * services.Count);
            string service = index >= 0 && index < services.Count ? services[index].GetType().Name : "-";
            Debug.Log("[AndroidBootWatchdog] naninovel progress " + progress.ToString("0.000") + " -> service #" + index + "/" + services.Count + " " + service);
        }

        private IEnumerator Start()
        {
            var wait = new WaitForSecondsRealtime(IntervalSeconds);
            for (int i = 0; i < Reports; i++)
            {
                Report();
                yield return wait;
            }
        }

        private static void Report()
        {
            string scenes = string.Join(",", Enumerable.Range(0, SceneManager.sceneCount)
                .Select(index => SceneManager.GetSceneAt(index)).Select(scene => scene.name + "/" + scene.rootCount));
            string addressables;
            try
            {
                var init = Addressables.InitializeAsync(false);
                addressables = init.IsDone ? init.Status + (init.OperationException == null ? "" : " " + init.OperationException.Message) : "pending";
            }
            catch (Exception exception)
            {
                addressables = "error " + exception.Message;
            }
            int canvases = FindObjectsByType<Canvas>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Length;
            string audio = "";
            try
            {
                if (Engine.TryGetService<IAudioManager>(out IAudioManager manager) && manager.AudioMixer != null)
                {
                    manager.AudioMixer.GetFloat("BGM Pitch", out float bgm);
                    manager.AudioMixer.GetFloat("SFX Pitch", out float sfx);
                    manager.AudioMixer.GetFloat("Voice Pitch", out float voice);
                    audio = " pitch(bgm=" + bgm.ToString("0.##") + ",sfx=" + sfx.ToString("0.##") + ",voice=" + voice.ToString("0.##") + ")";
                }
            }
            catch (Exception)
            {
                audio = " pitch(unavailable)";
            }
            Debug.Log("[AndroidBootWatchdog] t=" + Time.realtimeSinceStartup.ToString("0.0") + " frame=" + Time.frameCount +
                      " naninovel(initialized=" + Engine.Initialized + ", initializing=" + Engine.Initializing + ")" +
                      " addressables=" + addressables + " scenes=" + scenes + " canvases=" + canvases +
                      " cameras=" + Camera.allCamerasCount + " managed=" + (GC.GetTotalMemory(false) >> 20) + "MB" +
                      audio + " timeScale=" + Time.timeScale.ToString("0.##") + " listenerPause=" + AudioListener.pause);
        }
    }
}
#endif
