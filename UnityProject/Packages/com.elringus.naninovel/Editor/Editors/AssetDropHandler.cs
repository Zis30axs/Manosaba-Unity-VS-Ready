using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    public class AssetDropHandler
    {
        public Type TypeConstraint { get; set; }
        public string DropMessage { get; set; } = "Drop the assets or folders here to add the resources in batch.";

        private readonly Action<DroppedAsset[]> onDrop;
        private readonly List<DroppedAsset> droppedAssets = new();

        public AssetDropHandler (Action<DroppedAsset[]> onDrop)
        {
            this.onDrop = onDrop;
        }

        public bool CanHandleDraggedObjects ()
        {
            if (DragAndDrop.objectReferences.Length == 0) return false;

            foreach (var obj in DragAndDrop.objectReferences)
                if (!CanHandle(obj))
                    return false;
            return true;
        }

        public bool CanHandle (UnityEngine.Object obj)
        {
            return obj && (TypeConstraint is null ||
                           obj.GetType() == TypeConstraint ||
                           ProjectWindowUtil.IsFolder(obj.GetInstanceID()));
        }

        public void DrawDropArea (Rect rect)
        {
            rect.height += 20;
            rect.y += 5;
            GUILayoutUtility.GetRect(rect.width, 30);
            EditorGUI.HelpBox(rect, DropMessage, MessageType.Info);

            if (rect.Contains(Event.current.mousePosition))
                DragAndDrop.visualMode = DragAndDropVisualMode.Link;

            if (Event.current.type == EventType.DragPerform && rect.Contains(Event.current.mousePosition))
                ProcessDroppedObjects();
        }

        private void ProcessDroppedObjects ()
        {
            droppedAssets.Clear();
            foreach (var obj in DragAndDrop.objectReferences)
                ProcessDroppedObject(obj);
            DragAndDrop.AcceptDrag();
            onDrop(droppedAssets.ToArray());
        }

        private void ProcessDroppedObject (UnityEngine.Object obj, string relativePath = null)
        {
            if (!CanHandle(obj)) return;
            if (ProjectWindowUtil.IsFolder(obj.GetInstanceID())) ProcessDroppedFolder(obj);
            else
            {
                AssetDatabase.TryGetGUIDAndLocalFileIdentifier(obj, out var guid, out long _);
                if (string.IsNullOrEmpty(guid)) return;
                droppedAssets.Add(new(obj, guid, relativePath));
            }
        }

        private void ProcessDroppedFolder (UnityEngine.Object folderObj)
        {
            var folderPath = AssetDatabase.GetAssetPath(folderObj);
            var guids = AssetDatabase.FindAssets(null, new[] { folderPath }).DistinctBy(p => p);
            var objects = new Dictionary<string, UnityEngine.Object>();
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (AssetDatabase.IsValidFolder(assetPath)) continue;
                var assetObj = AssetDatabase.LoadAssetAtPath(assetPath, TypeConstraint ?? typeof(UnityEngine.Object));
                if (assetObj is null || !CanHandle(assetObj)) continue;
                var relativeName = folderObj.name + assetPath.GetAfterFirst(folderPath).GetBeforeLast(".");
                objects.Add(relativeName, assetObj);
            }

            foreach (var kv in objects)
                ProcessDroppedObject(kv.Value, kv.Key);
        }
    }
}
