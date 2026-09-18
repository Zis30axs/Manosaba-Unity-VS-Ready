using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Naninovel
{
    public static class ObjectUtils
    {
        /// <summary>
        /// Invokes <see cref="Object.Destroy(Object)"/> or <see cref="Object.DestroyImmediate(Object)"/>
        /// depending on whether the application is in play mode. Won't have effect if the object is not valid.
        /// </summary>
        public static void DestroyOrImmediate (Object obj)
        {
            if (!IsValid(obj)) return;

            if (Application.isPlaying)
                Object.Destroy(obj);
            else Object.DestroyImmediate(obj);
        }

        /// <summary>
        /// Invokes <see cref="DestroyOrImmediate(Object)"/> on each direct descendent of the specified transform.
        /// </summary>
        public static void DestroyAllChildren (Transform trs)
        {
            var childCount = trs.childCount;
            for (var i = 0; i < childCount; i++)
                DestroyOrImmediate(trs.GetChild(i).gameObject);
        }

        /// <summary>
        /// Asserts validity of all the required objects.
        /// </summary>
        /// <param name="requiredObjects">Objects to check for validity.</param>
        /// <returns>Whether all the required objects are valid.</returns>
        public static void AssertRequiredObjects (this Component component, params Object[] requiredObjects)
        {
            if (requiredObjects.Any(obj => !obj))
                throw new UnityException($"Unity object '{component}' is missing a required dependency. " +
                                         "Make sure all the required fields are assigned in the inspector and are pointing to valid objects.");
        }

        /// <summary>
        /// Invokes the specified action on each descendant (child of any level, recursively) and (optionally) on self.
        /// </summary>
        public static void ForEachDescendant (this GameObject gameObject, Action<GameObject> action, bool invokeOnSelf = true)
        {
            if (invokeOnSelf) action?.Invoke(gameObject);
            foreach (Transform childTransform in gameObject.transform)
                ForEachDescendant(childTransform.gameObject, action);
        }

        /// <summary>
        /// Checks if specified reference targets to a valid (not-destroyed) <see cref="UnityEngine.Object"/>.
        /// </summary>
        public static bool IsValid (object obj)
        {
            if (obj is Object unityObject)
                return unityObject;
            return false;
        }

        /// <summary>
        /// Checks whether the specified game object is currently edited in prefab isolation mode.
        /// Always returns false in case specified object is not valid and outside editor.
        /// </summary>
        public static bool IsEditedInPrefabMode (GameObject obj)
        {
            if (!IsValid(obj)) return false;
            #if UNITY_EDITOR
            return UnityEditor.SceneManagement.PrefabStageUtility.GetPrefabStage(obj);
            #else
            return false;
            #endif
        }

        /// <summary>
        /// Checks whether specified object is part of a prefab (original asset) and not an instance.
        /// Always returns false in case specified object is not valid and outside editor.
        /// </summary>
        public static bool IsPartOfPrefab (GameObject obj)
        {
            if (!IsValid(obj)) return false;
            #if UNITY_EDITOR
            return UnityEditor.PrefabUtility.IsPartOfPrefabAsset(obj);
            #else
            return false;
            #endif
        }

        /// <summary>
        /// When in editor and specified object is a valid project asset,
        /// returns asset's path formatted as hyperlink; otherwise, returns null.
        /// </summary>
        public static string BuildAssetLink (Object asset, int? line = null)
        {
            #if UNITY_EDITOR
            if (!asset) return null;
            return StringUtils.BuildAssetLink(UnityEditor.AssetDatabase.GetAssetPath(asset), line);
            #else
            return null;
            #endif
        }
    }
}
