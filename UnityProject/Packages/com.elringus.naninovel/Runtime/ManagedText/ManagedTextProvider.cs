using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Naninovel
{
    /// <summary>
    /// Provides managed text values to game objects via Unity events.
    /// When attached to a managed UI, text printer or choice handler
    /// generated managed text documents will automatically include corresponding record.
    /// </summary>
    public class ManagedTextProvider : MonoBehaviour
    {
        [Serializable]
        private class ValueChangedEvent : UnityEvent<string> { }

        public string Document => string.IsNullOrWhiteSpace(document) ? ManagedTextPaths.Default : document;
        public string Key => string.IsNullOrWhiteSpace(key) ? gameObject.name : key;
        public string DefaultValue => defaultValue;

        [FormerlySerializedAs("category"), Tooltip("Local resource path of the managed text document, which contains tracked managed text record.")]
        [SerializeField] private string document;
        [Tooltip("ID of the tracked managed text record; when not specified (empty), will use name of the game object to which the component is attached.")]
        [SerializeField] private string key;
        [Tooltip("Default value to use when the tracked record is missing.")]
        [SerializeField] private string defaultValue;
        [Tooltip("Invoked when value of the tracked managed text record is changed (eg, when switching localization); also invoked when the engine is initialized.")]
        [SerializeField] private ValueChangedEvent onValueChanged;

        private ILocalizationManager l10n;
        private ITextManager docs;

        private void OnEnable ()
        {
            if (Engine.Initialized) HandleEngineInitialized();
            else Engine.OnInitializationFinished += HandleEngineInitialized;
        }

        private void OnDisable ()
        {
            if (l10n != null)
                l10n.OnLocaleChanged -= HandleLocalizationChanged;
            Engine.OnInitializationFinished -= HandleEngineInitialized;
        }

        private void OnDestroy ()
        {
            docs?.DocumentLoader?.ReleaseAll(this);
        }

        private void HandleEngineInitialized ()
        {
            Engine.OnInitializationFinished -= HandleEngineInitialized;

            docs = Engine.GetServiceOrErr<ITextManager>();
            l10n = Engine.GetServiceOrErr<ILocalizationManager>();
            l10n.OnLocaleChanged += HandleLocalizationChanged;

            InvokeValueChanged();
        }

        private void HandleLocalizationChanged (LocaleChangedArgs _) => InvokeValueChanged();

        private async void InvokeValueChanged ()
        {
            if (!docs.DocumentLoader.IsLoaded(Document))
                await docs.DocumentLoader.Load(Document, this);
            var value = docs.GetRecordValue(Key, Document);
            if (string.IsNullOrEmpty(value)) value = DefaultValue;
            onValueChanged?.Invoke(value);
        }
    }
}
