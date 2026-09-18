// Rebuilds the shipped boot scene (`manosaba_Data/level0`) as a real, editable Unity scene.
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.Level0SceneBuilder.Run -logFile -
//
// Why not import the shipped file: it is a player-build scene, so it has no type tree, and Unity's own importer
// aborts on it ("is corrupted! [Position out of bounds!]" - see Level0SceneProbe.cs, which records that
// experiment).  The scene therefore has to be *rebuilt*, and the input is the byte-level decode produced by
// tools/unity-rebuild/read_level0_scene_inventory.py:
//
//   objects   142        (33 GameObject / 50 MonoBehaviour / 13 Transform / 20 RectTransform / 12 CanvasRenderer
//                         / 4 Canvas / 4 CanvasGroup / 3 Camera + settings objects)
//   hierarchy 33 nodes   (10 roots: SplashScreen, PerspectiveStage, LetterBox, ForefrontOverlay,
//                         GameInitializer, SteamManager, Presenters, Debug, MainCamera, EventSystem)
//   values    44 rows    (TRS for every Transform/RectTransform, plus Canvas/CanvasGroup/Camera settings)
//
// What this builder does, in order: create the GameObjects with the decoded names, restore the parent/child
// edges and sibling order, add the same components (built-ins directly, MonoBehaviours by resolved type name),
// copy the decoded values, then restore the entry component's serialized references from the decode in
// decode_level0_boot.py.  Everything it cannot do is *reported*, never silently dropped - the scene is a
// reconstruction and the report is where its deviations live.
//
// Writes tools/unity-rebuild/out/unity/level0_scene_build.json (exit 0 when the scene was built).
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manosaba.EditorTools
{
    public static class Level0SceneBuilder
    {
        private const string InventoryRelativePath = "../tools/unity-rebuild/out/unity/level0_scene_inventory.json";
        private const string BootReferencesRelativePath = "../tools/unity-rebuild/out/unity/level0_boot_references.json";
        private const string ComponentValuesRelativePath = "../tools/unity-rebuild/out/unity/level0_component_values.json";
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/level0_scene_build.json";
        private const string GamePrefabPath = "Assets/GameContent/#WitchTrials/Prefabs/General/Core/Game.prefab";

        // The gate scripts drive Assets/Scenes/Boot.unity, so the reconstruction is written there: the scene the
        // project boots from *is* the rebuilt level0 (the asset file name differs from the shipped one).
        private const string ScenePath = "Assets/Scenes/Boot.unity";

        [MenuItem("Manosaba/Rebuild Level0 Boot Scene")]
        public static void Run()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute()
        {
            var result = new BuildResult { generated = DateTime.UtcNow.ToString("o") };
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));

            try
            {
                string inventoryPath = Path.GetFullPath(Path.Combine(projectRoot, InventoryRelativePath));
                if (!File.Exists(inventoryPath))
                {
                    result.status = "BLOCKED";
                    result.error = "inventory not found: " + inventoryPath +
                                   " (run tools/unity-rebuild/read_level0_scene_inventory.py --write)";
                }
                else if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    result.status = "BLOCKED";
                    result.error = "a modified scene could not be saved";
                }
                else
                {
                    var inventory = JObject.Parse(File.ReadAllText(inventoryPath));
                    JArray nodes = (JArray)inventory["hierarchy"]["nodes"];
                    JArray objects = (JArray)inventory["objects"];

                    // row lookups: a component's owner comes from the decoded hierarchy (node.components), so
                    // ownership is read rather than inferred from creation order
                    var ownerOf = new Dictionary<long, long>();
                    foreach (var node in nodes)
                    {
                        long go = (long)node["path_id"];
                        var nodeComponents = node["components"] as JArray;
                        if (nodeComponents == null) continue;
                        foreach (var component in nodeComponents) ownerOf[(long)component["path_id"]] = go;
                    }

                    var componentsByOwner = new Dictionary<long, List<JToken>>();
                    var transformsByOwner = new Dictionary<long, JToken>();
                    foreach (var row in objects)
                    {
                        string type = (string)row["type"];
                        if (type == "GameObject") continue;
                        if (!ownerOf.TryGetValue((long)row["path_id"], out long owner)) continue;   // scene settings etc.
                        if (type == "Transform" || type == "RectTransform")
                        {
                            transformsByOwner[owner] = row;
                            continue;   // created together with the GameObject
                        }
                        if (!componentsByOwner.TryGetValue(owner, out var list))
                            componentsByOwner[owner] = list = new List<JToken>();
                        list.Add(row);
                    }

                    Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
                    var created = new Dictionary<long, GameObject>();

                    // --- 1. GameObjects (a RectTransform must exist from birth; it cannot be added later) -----
                    var goRows = objects.Where(r => (string)r["type"] == "GameObject")
                        .ToDictionary(r => (long)r["path_id"], r => r);
                    foreach (var node in nodes)
                    {
                        long pathId = (long)node["path_id"];
                        string name = ((string)node["name"]) ?? ("node" + pathId);
                        bool isUi = transformsByOwner.TryGetValue(pathId, out var tr) &&
                                    (string)tr["type"] == "RectTransform";
                        var go = isUi ? new GameObject(name, typeof(RectTransform)) : new GameObject(name);
                        created[pathId] = go;
                        result.created_paths.Add((string)node["path"]);

                        // tag / layer / active state, so "MainCamera" really is Camera.main and inactive
                        // authored objects stay inactive
                        if (goRows.TryGetValue(pathId, out var goRow))
                        {
                            string tag = (string)goRow["tag"];
                            if (!string.IsNullOrEmpty(tag) && tag != "Untagged")
                            {
                                try { go.tag = tag; }
                                catch (Exception) { result.component_errors.Add("tag '" + tag + "' is not defined in this project"); }
                            }
                            if (goRow["layer"] != null) go.layer = (int)goRow["layer"];
                            if (goRow["active"] != null && !(bool)goRow["active"]) go.SetActive(false);
                        }
                    }

                    // --- 2. parenting and sibling order ------------------------------------------------------
                    foreach (var node in nodes)
                    {
                        long pathId = (long)node["path_id"];
                        long parentId = (long)node["parent"];
                        if (parentId == 0 || !created.ContainsKey(parentId)) continue;
                        created[pathId].transform.SetParent(created[parentId].transform, false);
                        result.parent_edges++;
                    }
                    foreach (var node in nodes)
                    {
                        var children = node["children"] as JArray;
                        if (children == null || children.Count == 0) continue;
                        int index = 0;
                        foreach (var child in children)
                        {
                            long childId = (long)child;
                            if (created.ContainsKey(childId))
                                created[childId].transform.SetSiblingIndex(index++);
                        }
                    }

                    // --- 3. components ----------------------------------------------------------------------
                    foreach (var node in nodes)
                    {
                        long pathId = (long)node["path_id"];
                        GameObject go = created[pathId];
                        if (!componentsByOwner.TryGetValue(pathId, out var rows)) continue;
                        foreach (var row in rows)
                        {
                            string typeName = (string)row["type"];
                            string script = (string)row["script"];
                            Type type = typeName == "MonoBehaviour" ? FindType(script) : FindType(typeName);
                            if (type == null)
                            {
                                string missing = typeName == "MonoBehaviour" ? script : typeName;
                                result.missing_types.Add(missing + " @" + (string)node["path"]);
                                continue;
                            }
                            try
                            {
                                go.AddComponent(type);
                                result.components_added++;
                            }
                            catch (Exception ex)
                            {
                                result.component_errors.Add(type.Name + " @" + (string)node["path"] + ": " + ex.Message);
                            }
                        }
                    }

                    // --- 4. serialized values (needs the components to exist) --------------------------------
                    foreach (var node in nodes)
                    {
                        long pathId = (long)node["path_id"];
                        GameObject go = created[pathId];
                        if (transformsByOwner.TryGetValue(pathId, out var trRow) && trRow["data"] != null)
                            ApplyTransform(go.transform, (JObject)trRow["data"], result);
                        if (!componentsByOwner.TryGetValue(pathId, out var rows)) continue;
                        foreach (var row in rows)
                        {
                            if (row["data"] == null) continue;
                            ApplyComponent(go, (string)row["type"], (JObject)row["data"], result);
                        }
                    }

                    // --- 4b. decoded component values (the shipped scene's serialized fields) -----------------
                    ApplyDecodedValues(created, ownerOf, projectRoot, result);

                    // Screen-space-camera canvases need a camera; the shipped scene's m_Camera PPtr is not part of
                    // the decode, so bind the scene's main camera (tag or name) and report it as a stated deviation.
                    Camera mainCamera = created.Values.Select(go => go.GetComponent<Camera>())
                        .FirstOrDefault(c => c != null && c.CompareTag("MainCamera"))
                        ?? created.Values.Select(go => go.GetComponent<Camera>())
                            .FirstOrDefault(c => c != null && c.gameObject.name == "MainCamera");
                    foreach (var canvas in created.Values.Select(go => go.GetComponent<Canvas>()).Where(c => c != null))
                    {
                        if (canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera == null && mainCamera != null)
                        {
                            canvas.worldCamera = mainCamera;
                            result.deviations.Add("canvas '" + canvas.name +
                                                  "': worldCamera bound to the scene's MainCamera (m_Camera PPtr not decoded)");
                        }
                    }
                    result.main_camera = mainCamera == null ? null : mainCamera.gameObject.name;

                    // --- 5. the entry component's serialized references --------------------------------------
                    WireEntryComponent(created, ownerOf, projectRoot, result);

                    // --- 6. the integration probe (this is how the gates observe the boot chain) -------------
                    if (!created.Values.Any(go => go.GetComponent("ManosabaBoot") != null))
                    {
                        Type boot = FindType("Manosaba.Integration.ManosabaBoot");
                        if (boot == null) result.missing_types.Add("Manosaba.Integration.ManosabaBoot");
                        else
                        {
                            var host = new GameObject("ManosabaBoot");
                            host.AddComponent(boot);
                            result.probe_object_added = true;
                        }
                    }

                    // --- 7. save + build settings ------------------------------------------------------------
                    Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(ScenePath)));
                    EditorSceneManager.MarkSceneDirty(scene);
                    EditorSceneManager.SaveScene(scene, ScenePath);
                    AssetDatabase.SaveAssets();

                    var scenes = EditorBuildSettings.scenes.Where(s => s.path != ScenePath).ToList();
                    scenes.Insert(0, new EditorBuildSettingsScene(ScenePath, true));
                    EditorBuildSettings.scenes = scenes.ToArray();
                    result.build_scenes = EditorBuildSettings.scenes.Select(s => (s.enabled ? "+" : "-") + s.path).ToList();

                    result.root_count = scene.rootCount;
                    result.gameobject_count = created.Count;
                    result.status = "OK";
                }
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[Level0SceneBuilder] " + ex);
            }

            Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            File.WriteAllText(outPath, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
            Debug.Log("[Level0SceneBuilder] " + result.status + " objects=" + result.gameobject_count +
                      " components=" + result.components_added + " missing_types=" + result.missing_types.Count +
                      " wrote " + outPath);
            return result.status == "OK" ? 0 : 1;
        }

        /// <summary>The owning GameObject path_id of a component, from the decoded hierarchy (never guessed).</summary>
        private static long OwnerOf(Dictionary<long, long> ownerOf, long componentPathId, BuildResult result,
                                    string what)
        {
            if (ownerOf.TryGetValue(componentPathId, out long owner)) return owner;
            result.deviations.Add(what + ": component " + componentPathId + " has no owner in the decoded hierarchy");
            return 0;
        }

        private static void ApplyTransform(Transform transform, JObject data, BuildResult result)
        {
            transform.localPosition = ReadVector3(data["local_position"], Vector3.zero);
            transform.localRotation = ReadQuaternion(data["local_rotation"], Quaternion.identity);
            transform.localScale = ReadVector3(data["local_scale"], Vector3.one);
            if (transform is RectTransform rect)
            {
                rect.anchorMin = ReadVector2(data["anchor_min"], rect.anchorMin);
                rect.anchorMax = ReadVector2(data["anchor_max"], rect.anchorMax);
                rect.anchoredPosition = ReadVector2(data["anchored_position"], rect.anchoredPosition);
                rect.sizeDelta = ReadVector2(data["size_delta"], rect.sizeDelta);
                rect.pivot = ReadVector2(data["pivot"], rect.pivot);
            }
            result.transforms_applied++;
        }

        private static void ApplyComponent(GameObject go, string typeName, JObject data, BuildResult result)
        {
            Type type = typeName == "MonoBehaviour" ? null : FindType(typeName);
            if (type == null) return;
            var component = go.GetComponent(type);
            if (component == null) return;
            if (component is Canvas canvas)
            {
                canvas.renderMode = (RenderMode)(int)data["render_mode"];
                canvas.sortingOrder = (int)data["sorting_order"];
                result.canvas_states_applied++;
            }
            else if (component is CanvasGroup group)
            {
                group.alpha = (float)data["alpha"];
                group.interactable = (bool)data["interactable"];
                group.blocksRaycasts = (bool)data["blocks_raycasts"];
                group.ignoreParentGroups = (bool)data["ignore_parent_groups"];
                result.canvas_group_states_applied++;
            }
            else if (component is Camera camera)
            {
                camera.orthographic = (bool)data["orthographic"];
                camera.orthographicSize = (float)data["orthographic_size"];
                camera.fieldOfView = (float)data["field_of_view"];
                camera.depth = (float)data["depth"];
                camera.nearClipPlane = (float)data["near_clip"];
                camera.farClipPlane = (float)data["far_clip"];
                camera.clearFlags = (CameraClearFlags)(int)data["clear_flags"];
                camera.cullingMask = (int)data["culling_mask"];
                var colour = (JArray)data["background_color"];
                camera.backgroundColor = new Color((float)colour[0], (float)colour[1], (float)colour[2], (float)colour[3]);
                result.camera_states_applied++;
            }
        }

        /// <summary>
        /// Restores what decode_level0_boot.py recovered from the shipped scene: the entry component's three
        /// serialized references and its instance name.  SerializedObject is used because the fields are private
        /// and one of them is a nested Addressables AssetReference.
        /// </summary>
        private static void WireEntryComponent(Dictionary<long, GameObject> created, Dictionary<long, long> ownerOf,
                                               string projectRoot, BuildResult result)
        {
            string refsPath = Path.GetFullPath(Path.Combine(projectRoot, BootReferencesRelativePath));
            if (!File.Exists(refsPath))
            {
                result.deviations.Add("boot references not decoded (" + refsPath + "): entry references left empty");
                return;
            }
            var refs = JObject.Parse(File.ReadAllText(refsPath));
            Type entryType = FindType("WitchTrials.Views.GameInitializer");
            if (entryType == null)
            {
                result.missing_types.Add("WitchTrials.Views.GameInitializer (cannot wire references)");
                return;
            }

            // The decode names the entry GameObject directly (its m_GameObject), so this cannot pick up a second
            // GameInitializer by accident.
            long entryOwner = (long)refs["m_GameObject"]["m_PathID"];
            var entry = created.TryGetValue(entryOwner, out var entryGo) ? entryGo.GetComponent(entryType) : null;
            if (entry == null)
            {
                result.deviations.Add("no GameInitializer component in the rebuilt scene: references not wired");
                return;
            }

            long steamPathId = (long)refs["_steamManager"]["m_PathID"];
            long splashPathId = (long)refs["_splashScreen"]["m_PathID"];
            UnityEngine.Object steam = ComponentOn(created, ownerOf, steamPathId, "SteamManager", result);
            UnityEngine.Object splash = ComponentOn(created, ownerOf, splashPathId, "SplashScreen", result);
            string guid = AssetDatabase.AssetPathToGUID(GamePrefabPath);
            if (string.IsNullOrEmpty(guid))
                result.deviations.Add("prefab GUID lookup failed for " + GamePrefabPath + " (run the asset import first)");

            var so = new SerializedObject(entry);
            so.FindProperty("_steamManager").objectReferenceValue = steam;
            so.FindProperty("_splashScreen").objectReferenceValue = splash;
            so.FindProperty("_gamePrefabInstanceName").stringValue = (string)refs["_gamePrefabInstanceName"];
            if (!string.IsNullOrEmpty(guid))
            {
                SerializedProperty assetGuid = so.FindProperty("_gamePrefab.m_AssetGUID");
                if (assetGuid != null) assetGuid.stringValue = guid;
                else result.deviations.Add("_gamePrefab.m_AssetGUID not found on the AssetReference");
            }
            so.ApplyModifiedPropertiesWithoutUndo();

            // Steam: the manager is a real component from the shipped scene and it is wired, but activating it in
            // a harness without a running Steam client makes Steamworks.NET initialise natively and kill the
            // editor (measured in an earlier round).  The GameObject is therefore left inactive by default; the
            // reference itself stays intact so the boot chain can still be measured, and
            // MANOSABA_GAMESTART_WIRE_STEAM=1 activates it for runs on a machine with Steam.
            if (steam is Component steamComponent && steamComponent.gameObject.activeSelf)
            {
                if (Environment.GetEnvironmentVariable("MANOSABA_GAMESTART_WIRE_STEAM") == "1")
                {
                    result.deviations.Add("SteamManager: GameObject left ACTIVE (MANOSABA_GAMESTART_WIRE_STEAM=1)");
                }
                else
                {
                    steamComponent.gameObject.SetActive(false);
                    result.deviations.Add("SteamManager: GameObject deactivated (no Steam client in the harness; " +
                                          "the reference is still wired). Set MANOSABA_GAMESTART_WIRE_STEAM=1 to keep it active.");
                }
            }

            result.entry_wired = new List<string>
            {
                "_steamManager = scene object " + steamPathId + " (" + (steam == null ? "MISSING" : steam.GetType().Name) + ")",
                "_splashScreen = scene object " + splashPathId + " (" + (splash == null ? "MISSING" : splash.GetType().Name) + ")",
                "_gamePrefab.m_AssetGUID = " + guid + "  (shipped GUID " + (string)refs["_gamePrefab_m_AssetGUID"] + ")",
                "_gamePrefabInstanceName = '" + (string)refs["_gamePrefabInstanceName"] + "'",
            };
        }

        /// <summary>
        /// Applies the field values recovered from the shipped scene by
        /// tools/unity-rebuild/decode_level0_component_values.py - the serialized references that make the
        /// rebuilt scene behave like the original (SplashScreen's canvas group and whole-screen button,
        /// OverlayPresenter's overlay, the stage containers' cameras, ...).
        ///
        /// Only entries the decoder could walk to the exact end of the object are applied: a partial walk may
        /// have mis-mapped a field type, and half-applied data is worse than none.  Every reference is resolved
        /// by the target class the decoder read from the *target's own* m_Script, so a mismatch is caught here
        /// rather than becoming a silently wrong link.
        /// </summary>
        private static void ApplyDecodedValues(Dictionary<long, GameObject> created, Dictionary<long, long> ownerOf,
                                               string projectRoot, BuildResult result)
        {
            string valuesPath = Path.GetFullPath(Path.Combine(projectRoot, ComponentValuesRelativePath));
            if (!File.Exists(valuesPath))
            {
                result.deviations.Add("component values not decoded (" + valuesPath + "): scene fields left at defaults");
                return;
            }
            var document = JObject.Parse(File.ReadAllText(valuesPath));
            foreach (var entry in (JArray)document["classes"])
            {
                long componentPathId = (long)entry["path_id"];
                string className = (string)entry["class"];
                string status = (string)entry["status"];
                if (status != "OK")
                {
                    result.component_values_skipped.Add(className + " (" + status + ")");
                    continue;
                }
                long owner = ownerOf.TryGetValue(componentPathId, out long ownerPathId) ? ownerPathId : 0;
                Type componentType = FindType(className);
                if (owner == 0 || componentType == null || !created.TryGetValue(owner, out var host))
                {
                    result.component_values_skipped.Add(className + " (no rebuilt component to apply to)");
                    continue;
                }
                var component = host.GetComponent(componentType);
                if (component == null)
                {
                    result.component_values_skipped.Add(className + " (component missing on object " + owner + ")");
                    continue;
                }

                var serialized = new SerializedObject(component);
                foreach (var field in (JObject)entry["values"])
                {
                    string fieldName = field.Key;   // the decoder writes field name -> value object
                    var value = (JObject)field.Value;
                    SerializedProperty property = serialized.FindProperty(fieldName);
                    if (property == null)
                    {
                        result.component_values_skipped.Add(className + "." + fieldName + " (no such serialized field)");
                        continue;
                    }
                    try
                    {
                        if ((string)value["kind"] == "component_ref")
                        {
                            UnityEngine.Object target = ResolveReference(created, ownerOf, value);
                            if (target == null)
                            {
                                result.component_values_skipped.Add(className + "." + fieldName +
                                                                     " (target " + (long)value["m_PathID"] + " unresolved)");
                                continue;
                            }
                            property.objectReferenceValue = target;
                        }
                        else if ((string)value["kind"] == "array")
                        {
                            if (!ApplyReferenceArray(property, created, ownerOf, value, className + "." + fieldName, result))
                                continue;
                        }
                        else if (!AssignScalar(property, value))
                        {
                            result.component_values_skipped.Add(className + "." + fieldName + " (kind " +
                                                                 (string)value["kind"] + " not applied)");
                            continue;
                        }
                        result.component_values_applied++;
                    }
                    catch (Exception ex)
                    {
                        result.component_values_skipped.Add(className + "." + fieldName + ": " + ex.Message);
                    }
                }
                serialized.ApplyModifiedPropertiesWithoutUndo();
            }
        }

        /// <summary>
        /// Applies an array of object references (e.g. StageCameraController's `_cameraContainers`).  The whole
        /// array is written only when *every* element resolves: a partially filled array would silently change the
        /// scene's behaviour, which is worse than leaving the field empty and saying so.
        /// </summary>
        private static bool ApplyReferenceArray(SerializedProperty property, Dictionary<long, GameObject> created,
                                                Dictionary<long, long> ownerOf, JObject value, string label,
                                                BuildResult result)
        {
            if (property.propertyType != SerializedPropertyType.Generic || !property.isArray)
            {
                result.component_values_skipped.Add(label + " (decoded as an array but the field is not an array)");
                return false;
            }
            var items = (JArray)value["items"];
            var resolved = new List<UnityEngine.Object>(items.Count);
            foreach (var item in items)
            {
                var element = (JObject)item;
                UnityEngine.Object target = (string)element["kind"] == "component_ref"
                    ? ResolveReference(created, ownerOf, element) : null;
                if (target == null)
                {
                    result.component_values_skipped.Add(label + " (element " + resolved.Count + " unresolved)");
                    return false;
                }
                resolved.Add(target);
            }
            property.arraySize = resolved.Count;
            for (int index = 0; index < resolved.Count; index++)
                property.GetArrayElementAtIndex(index).objectReferenceValue = resolved[index];
            return true;   // the caller counts the applied value
        }

        /// <summary>Resolves a decoded PPtr through the shipped path_id -> rebuilt component mapping.</summary>
        private static UnityEngine.Object ResolveReference(Dictionary<long, GameObject> created,
                                                            Dictionary<long, long> ownerOf, JObject value)
        {
            if ((int)value["m_FileID"] != 0) return null;      // a different serialized file: not in this scene
            long targetPathId = (long)value["m_PathID"];
            string targetClass = (string)value["target_class"];
            long owner = ownerOf.TryGetValue(targetPathId, out long ownerPathId) ? ownerPathId : 0;
            if (owner == 0 || !created.TryGetValue(owner, out var host)) return null;
            Type type = FindType(targetClass);
            if (type == null) return null;
            return host.GetComponent(type);
        }

        private static bool AssignScalar(SerializedProperty property, JObject value)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Float:
                    property.floatValue = (float)value["value"]; return true;
                case SerializedPropertyType.Integer:
                case SerializedPropertyType.Enum:
                case SerializedPropertyType.LayerMask:
                    property.intValue = (int)value["value"]; return true;
                case SerializedPropertyType.Boolean:
                    property.boolValue = (int)value["value"] != 0; return true;
                case SerializedPropertyType.String:
                    property.stringValue = (string)value["value"] ?? ""; return true;
                case SerializedPropertyType.Color:
                {
                    var colour = (JArray)value["value"];
                    property.colorValue = new Color((float)colour[0], (float)colour[1], (float)colour[2], (float)colour[3]);
                    return true;
                }
                case SerializedPropertyType.Vector2:
                case SerializedPropertyType.Vector3:
                case SerializedPropertyType.Vector4:
                case SerializedPropertyType.Quaternion:
                {
                    var array = (JArray)value["value"];
                    property.vector4Value = new Vector4((float)array[0], (float)array[1],
                                                        array.Count > 2 ? (float)array[2] : 0f,
                                                        array.Count > 3 ? (float)array[3] : 0f);
                    return true;
                }
                default:
                    return false;
            }
        }

        /// <summary>Resolves a shipped component path_id to the component rebuilt for it: the hierarchy decode says which
        /// GameObject owns it, and the expected type is checked on that object.
        /// </summary>
        private static UnityEngine.Object ComponentOn(Dictionary<long, GameObject> created,
                                                      Dictionary<long, long> ownerOf, long componentPathId,
                                                      string expectedTypeName, BuildResult result)
        {
            long owner = OwnerOf(ownerOf, componentPathId, result, expectedTypeName);
            if (owner == 0 || !created.TryGetValue(owner, out var go)) return null;
            var component = go.GetComponent(expectedTypeName);
            if (component == null)
                result.deviations.Add(expectedTypeName + ": object " + owner + " has no " + expectedTypeName +
                                      " component after the build");
            return component;
        }

        /// <summary>
        /// UnityPy names built-in components without a namespace ("Canvas", "Camera"), so a plain
        /// <c>Assembly.GetType</c> lookup cannot find them.  Namespaced names (game scripts, "UnityEngine.UI.Image",
        /// ...) are looked up in the loaded assemblies as before.
        /// </summary>
        private static readonly Type[] BuiltinCandidates =
        {
            typeof(Canvas), typeof(CanvasGroup), typeof(CanvasRenderer), typeof(Camera), typeof(Light),
            typeof(AudioSource), typeof(AudioListener), typeof(Animator), typeof(SpriteRenderer),
            typeof(MeshRenderer), typeof(MeshFilter), typeof(SkinnedMeshRenderer), typeof(LineRenderer),
            typeof(TrailRenderer), typeof(ParticleSystem), typeof(ParticleSystemRenderer), typeof(ReflectionProbe),
            typeof(LightProbeGroup), typeof(Terrain), typeof(UnityEngine.Video.VideoPlayer),
            typeof(Rigidbody), typeof(Rigidbody2D),
            typeof(BoxCollider), typeof(BoxCollider2D), typeof(CircleCollider2D), typeof(CapsuleCollider),
            typeof(SphereCollider), typeof(MeshCollider), typeof(LODGroup), typeof(OcclusionArea),
            typeof(OcclusionPortal), typeof(WindZone), typeof(RectTransform), typeof(Transform),
        };

        private static Dictionary<string, Type> builtinsByName;

        private static Type FindType(string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return null;

            if (!fullName.Contains("."))
            {
                if (builtinsByName == null)
                    builtinsByName = BuiltinCandidates.GroupBy(t => t.Name)
                        .ToDictionary(g => g.Key, g => g.First());
                if (builtinsByName.TryGetValue(fullName, out Type builtin)) return builtin;
            }

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type type = null;
                try { type = assembly.GetType(fullName, false); }
                catch (Exception) { /* dynamic assemblies can refuse lookups */ }
                if (type != null) return type;
            }
            return null;
        }

        private static Vector3 ReadVector3(JToken token, Vector3 fallback)
        {
            var array = token as JArray;
            if (array == null || array.Count < 3) return fallback;
            return new Vector3((float)array[0], (float)array[1], (float)array[2]);
        }

        private static Vector2 ReadVector2(JToken token, Vector2 fallback)
        {
            var array = token as JArray;
            if (array == null || array.Count < 2) return fallback;
            return new Vector2((float)array[0], (float)array[1]);
        }

        private static Quaternion ReadQuaternion(JToken token, Quaternion fallback)
        {
            var array = token as JArray;
            if (array == null || array.Count < 4) return fallback;
            return new Quaternion((float)array[0], (float)array[1], (float)array[2], (float)array[3]);
        }

        [Serializable]
        public class BuildResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string error;
            public int gameobject_count;
            public int root_count;
            public int parent_edges;
            public int components_added;
            public int transforms_applied;
            public int canvas_states_applied;
            public int canvas_group_states_applied;
            public int camera_states_applied;
            public bool probe_object_added;
            public int component_values_applied;
            public List<string> component_values_skipped = new List<string>();
            public string main_camera;
            public List<string> created_paths = new List<string>();
            public List<string> entry_wired = new List<string>();
            public List<string> missing_types = new List<string>();
            public List<string> component_errors = new List<string>();
            public List<string> deviations = new List<string>();
            public List<string> build_scenes = new List<string>();
        }
    }
}
