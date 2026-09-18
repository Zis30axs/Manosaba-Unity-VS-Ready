using System;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace Naninovel.UI
{
    public class LoadLoggerScrollRect : ScriptableUIComponent<ScrollRect>
    {
        protected virtual TMP_Text LoggerText => loggerText;
        protected virtual TMP_Text MemoryUsageText => memoryUsageText;
        protected virtual bool LogEnabled => providerManager?.Configuration.LogResourceLoading ?? false;

        [SerializeField] private TMP_Text loggerText;
        [SerializeField] private TMP_Text memoryUsageText;

        private IResourceProviderManager providerManager;

        protected override void Awake ()
        {
            base.Awake();
            this.AssertRequiredObjects(loggerText);

            providerManager = Engine.GetServiceOrErr<IResourceProviderManager>();
            loggerText.text = string.Empty;
        }

        protected override void Start ()
        {
            base.Start();

            if (memoryUsageText) memoryUsageText.gameObject.SetActive(LogEnabled);
            if (LogEnabled) UpdateMemoryUsage();
        }

        protected override void OnEnable ()
        {
            base.OnEnable();

            if (!LogEnabled) return;
            providerManager.OnProviderMessage += LogResourceProviderMessage;
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();

            if (providerManager != null)
                providerManager.OnProviderMessage -= LogResourceProviderMessage;
        }

        protected virtual void LogResourceProviderMessage (string message)
        {
            Debug.Log($"<color=lightblue>{message}</color>");
            Append($"<color=lightblue>{message}</color>");
            UpdateMemoryUsage();
        }

        protected virtual void Append (string message)
        {
            EnsureCapacity(message);
            loggerText.text += message;
            loggerText.text += Environment.NewLine;
            UIComponent.verticalNormalizedPosition = 0;
        }

        protected virtual void UpdateMemoryUsage ()
        {
            if (!memoryUsageText) return;
            var size = StringUtils.FormatFileSize(Profiler.GetTotalAllocatedMemoryLong());
            memoryUsageText.text = $"<b>Total memory used: {size}</b>";
        }

        /// <summary>
        /// UI.Text has char limit (depends on vertex count per char, 65k verts is the limit).
        /// This will remove older messages to ensure it has capacity to accomodate the new one.
        /// </summary>
        protected virtual void EnsureCapacity (string message)
        {
            while (!HasEnoughCapacity())
                loggerText.text = loggerText.text.GetAfterFirst(Environment.NewLine);

            bool HasEnoughCapacity () => loggerText.text.Length + message.Length < 10000;
        }
    }
}
