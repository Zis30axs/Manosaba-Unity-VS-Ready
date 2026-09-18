using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="IEngineBehaviour"/> implementation for <see cref="EditorApplication"/> environment.
    /// Behaviour will be destroyed when play mode state changes.
    /// </summary>
    public class EditorBehaviour : IEngineBehaviour
    {
        public event Action OnBehaviourUpdate;
        public event Action OnBehaviourLateUpdate;
        public event Action OnBehaviourDestroy;

        private readonly GameObject rootObject;

        public EditorBehaviour ()
        {
            rootObject = new("Naninovel<Editor>");
            rootObject.hideFlags = HideFlags.DontSaveInEditor;

            EditorApplication.update += OnBehaviourUpdate.SafeInvoke;
            EditorApplication.update += OnBehaviourLateUpdate.SafeInvoke;
            EditorApplication.playModeStateChanged += HandlePlayModeStateChanged;
        }

        public GameObject GetRootObject () => rootObject;

        public void AddChildObject (GameObject obj)
        {
            obj.transform.SetParent(rootObject.transform);
        }

        public void Destroy ()
        {
            EditorApplication.update -= OnBehaviourUpdate.SafeInvoke;
            EditorApplication.update -= OnBehaviourLateUpdate.SafeInvoke;
            EditorApplication.playModeStateChanged -= HandlePlayModeStateChanged;

            UnityEngine.Object.DestroyImmediate(rootObject);
        }

        public Coroutine StartCoroutine (IEnumerator routine)
        {
            throw new NotImplementedException();
        }

        public void StopCoroutine (Coroutine routine)
        {
            throw new NotImplementedException();
        }

        private void HandlePlayModeStateChanged (PlayModeStateChange change)
        {
            OnBehaviourDestroy.SafeInvoke();
            Destroy();
        }
    }
}
