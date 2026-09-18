using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Naninovel.UI
{
    public class CustomVariableGUI : MonoBehaviour
    {
        private class Record
        {
            public string Name, Value, EditedValue;
            public bool Changed => !Value?.Equals(EditedValue, StringComparison.Ordinal) ?? false;

            public Record (string name, string value)
            {
                Name = name;
                Value = EditedValue = value;
            }
        }

        private const float width = 400;
        private const int windowId = 1;

        private static Rect windowRect = new(Screen.width - width, 0, width, Screen.height * .85f);
        private static Vector2 scrollPos;
        private static bool show;
        private static string search;
        private static CustomVariableGUI instance;

        private readonly SortedList<string, Record> records = new();
        private ICustomVariableManager variableManager;
        private IStateManager stateManager;

        public static void Toggle ()
        {
            if (!instance) instance = Engine.CreateObject<CustomVariableGUI>(nameof(CustomVariableGUI));
            show = !show;
            if (show) instance.UpdateRecords();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetOnPlayMode ()
        {
            show = false;
        }

        private void Awake ()
        {
            variableManager = Engine.GetServiceOrErr<ICustomVariableManager>();
            stateManager = Engine.GetServiceOrErr<IStateManager>();
        }

        private void OnEnable ()
        {
            variableManager.OnVariableUpdated += HandleVariableUpdated;
            stateManager.OnGameLoadFinished += HandleGameLoadFinished;
            stateManager.OnResetFinished += UpdateRecords;
            stateManager.OnRollbackFinished += UpdateRecords;
        }

        private void OnDisable ()
        {
            if (variableManager != null)
                variableManager.OnVariableUpdated -= HandleVariableUpdated;
            if (variableManager != null)
            {
                stateManager.OnGameLoadFinished -= HandleGameLoadFinished;
                stateManager.OnResetFinished -= UpdateRecords;
                stateManager.OnRollbackFinished -= UpdateRecords;
            }
        }

        private void OnGUI ()
        {
            if (!show) return;

            windowRect = GUI.Window(windowId, windowRect, DrawWindow, "Custom Variables");
        }

        private void DrawWindow (int windowId)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Search: ", GUILayout.Width(50));
            search = GUILayout.TextField(search);
            GUILayout.EndHorizontal();

            scrollPos = GUILayout.BeginScrollView(scrollPos);

            foreach (var record in records)
            {
                if (!string.IsNullOrEmpty(search) && !record.Key.StartsWith(search, StringComparison.OrdinalIgnoreCase)) continue;

                GUILayout.BeginHorizontal();
                GUILayout.TextField(record.Key, GUILayout.Width(width / 2f - 15));
                record.Value.EditedValue = GUILayout.TextField(record.Value.EditedValue);
                if (record.Value.Changed && GUILayout.Button("SET", GUILayout.Width(50)))
                    SetEditedValue(record.Value.Name, record.Value.EditedValue);
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();

            if (GUILayout.Button("Close Window")) show = false;

            GUI.DragWindow();
        }

        private void SetEditedValue (string name, string editedValue)
        {
            if (editedValue.StartsWithFast("\"") && editedValue.EndsWithFast("\""))
                variableManager.SetVariableValue(name, new(editedValue.Substring(1, editedValue.Length - 2)));
            else if (bool.TryParse(editedValue, out var boo))
                variableManager.SetVariableValue(name, new(boo));
            else if (ParseUtils.TryInvariantFloat(editedValue, out var num))
                variableManager.SetVariableValue(name, new(num));
        }

        private string ToRecordValue (CustomVariableValue value)
        {
            if (value.Type == CustomVariableValueType.String) return $"{value.String}";
            if (value.Type == CustomVariableValueType.Boolean) return value.Boolean.ToString().ToLowerInvariant();
            return value.Number.ToString(CultureInfo.InvariantCulture);
        }

        private void HandleVariableUpdated (CustomVariableUpdatedArgs args)
        {
            if (!show) return;

            if (args.Value == null)
            {
                if (records.ContainsKey(args.Name))
                    records.Remove(args.Name);
                return;
            }

            var value = ToRecordValue(args.Value.Value);
            if (records.ContainsKey(args.Name))
            {
                records[args.Name].Value = value;
                records[args.Name].EditedValue = value;
                return;
            }

            records.Add(args.Name, new(args.Name, value));
        }

        private void HandleGameLoadFinished (GameSaveLoadArgs obj) => UpdateRecords();

        private void UpdateRecords ()
        {
            if (!show) return;

            records.Clear();
            foreach (var variable in variableManager.Variables)
                records.Add(variable.Name, new(variable.Name, ToRecordValue(variable.Value)));
        }
    }
}
