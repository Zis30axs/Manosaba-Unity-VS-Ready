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

                    // Built-in references the inventory decodes: a canvas's m_Camera and plane distance (all four
                    // shipped canvases are Screen Space - Camera on MainCamera), and a camera's m_TargetTexture (the two
                    // stage cameras render into RenderTexture_1/_2, which the stage screens' RawImages show).
                    foreach (var node in nodes)
                    {
                        long pathId = (long)node["path_id"];
                        if (!componentsByOwner.TryGetValue(pathId, out var rows)) continue;
                        foreach (var row in rows)
                        {
                            var data = row["data"] as JObject;
                            if (data == null) continue;
                            string type = (string)row["type"];
                            if (type == "Canvas")
                            {
                                var canvas = created[pathId].GetComponent<Canvas>();
                                if (data["plane_distance"] != null) canvas.planeDistance = (float)data["plane_distance"];
                                long cameraPathId = (long?)data["world_camera_path_id"] ?? 0;
                                if (cameraPathId == 0) continue;
                                var camera = ComponentOn(created, ownerOf, cameraPathId, "Camera", result) as Camera;
                                if (camera == null) continue;
                                canvas.worldCamera = camera;
                                result.camera_links.Add("canvas '" + canvas.name + "'.worldCamera -> " + camera.name);
                            }
                            else if (type == "Camera" && data["target_texture"] is JObject texture)
                            {
                                var camera = created[pathId].GetComponent<Camera>();
                                var target = ResolveExternal(new JObject
                                {
                                    ["file"] = "m_FileID " + (int)texture["m_FileID"],
                                    ["path_id"] = texture["m_PathID"],
                                    ["type"] = texture["type"],
                                    ["name"] = texture["name"],
                                    ["descriptor"] = texture["descriptor"],
                                    ["error"] = texture["error"],
                                }, result, out string why) as RenderTexture;
                                if (target == null)
                                {
                                    result.deviations.Add("camera '" + camera.name + "'.targetTexture: " + why);
                                    continue;
                                }
                                camera.targetTexture = target;
                                result.camera_links.Add("camera '" + camera.name + "'.targetTexture -> " + target.name);
                            }
                        }
                    }
                    Camera mainCamera = created.Values.Select(go => go.GetComponent<Camera>())
                        .FirstOrDefault(c => c != null && c.CompareTag("MainCamera"));
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
                        if (ApplyValue(property, value, className + "." + fieldName, created, ownerOf, result))
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
        /// Applies one decoded value to its property, recursing the way the decoder walked it: a struct or
        /// [Serializable] class field by field (PerspectiveStagePresenter's stage list, a Button's colour block, a
        /// UnityEvent's persistent calls), an array element by element, an Addressables reference by its three
        /// strings.  Returns false - with the reason in the report - when the value cannot be applied faithfully.
        /// </summary>
        private static bool ApplyValue(SerializedProperty property, JObject value, string label,
                                       Dictionary<long, GameObject> created, Dictionary<long, long> ownerOf,
                                       BuildResult result)
        {
            switch ((string)value["kind"])
            {
                case "component_ref":
                {
                    if (property.propertyType != SerializedPropertyType.ObjectReference)
                    {
                        result.component_values_skipped.Add(label + " (decoded as a reference, the field is " +
                                                             property.propertyType + ")");
                        return false;
                    }
                    if (!TryResolveReference(created, ownerOf, value, result, out UnityEngine.Object target, out string why))
                    {
                        result.component_values_skipped.Add(label + " (" + why + ")");
                        return false;
                    }
                    property.objectReferenceValue = target;
                    return true;
                }
                case "struct":
                {
                    bool complete = true;
                    foreach (var field in (JObject)value["fields"])
                    {
                        SerializedProperty child = property.FindPropertyRelative(field.Key);
                        if (child == null)
                        {
                            result.component_values_skipped.Add(label + "." + field.Key + " (no such serialized field)");
                            complete = false;
                            continue;
                        }
                        complete &= ApplyValue(child, (JObject)field.Value, label + "." + field.Key, created, ownerOf, result);
                    }
                    return complete;
                }
                case "array":
                {
                    if (!property.isArray || property.propertyType == SerializedPropertyType.String)
                    {
                        result.component_values_skipped.Add(label + " (decoded as an array but the field is not an array)");
                        return false;
                    }
                    // All or nothing: an array with some links missing would silently change the scene's behaviour,
                    // which is worse than leaving it empty and saying so.
                    var items = (JArray)value["items"];
                    for (int index = 0; index < items.Count; index++)
                    {
                        string why = FirstUnresolved((JObject)items[index], created, ownerOf, result);
                        if (why != null)
                        {
                            result.component_values_skipped.Add(label + " (element " + index + ": " + why + ")");
                            return false;
                        }
                    }
                    property.arraySize = items.Count;
                    bool complete = true;
                    for (int index = 0; index < items.Count; index++)
                        complete &= ApplyValue(property.GetArrayElementAtIndex(index), (JObject)items[index],
                                               label + "[" + index + "]", created, ownerOf, result);
                    return complete;
                }
                case "asset_reference":
                {
                    string guid = (string)value["m_AssetGUID"] ?? "";
                    if (guid.Length > 0 && string.IsNullOrEmpty(AssetDatabase.GUIDToAssetPath(guid)))
                    {
                        result.component_values_skipped.Add(label + " (AssetReference " + guid + " is not in the project)");
                        return false;
                    }
                    property.FindPropertyRelative("m_AssetGUID").stringValue = guid;
                    property.FindPropertyRelative("m_SubObjectName").stringValue = (string)value["m_SubObjectName"] ?? "";
                    SerializedProperty subType = property.FindPropertyRelative("m_SubObjectType");
                    if (subType != null) subType.stringValue = (string)value["m_SubObjectType"] ?? "";
                    return true;
                }
                default:
                    if (AssignScalar(property, value)) return true;
                    result.component_values_skipped.Add(label + " (kind " + (string)value["kind"] + " not applied to " +
                                                         property.propertyType + ")");
                    return false;
            }
        }

        /// <summary>The first reference inside a decoded value that cannot be resolved, or null when all can.</summary>
        private static string FirstUnresolved(JObject value, Dictionary<long, GameObject> created,
                                              Dictionary<long, long> ownerOf, BuildResult result)
        {
            switch ((string)value["kind"])
            {
                case "component_ref":
                    return TryResolveReference(created, ownerOf, value, result, out _, out string why) ? null : why;
                case "struct":
                    foreach (var field in (JObject)value["fields"])
                    {
                        string nested = FirstUnresolved((JObject)field.Value, created, ownerOf, result);
                        if (nested != null) return field.Key + ": " + nested;
                    }
                    return null;
                case "array":
                    foreach (var item in (JArray)value["items"])
                    {
                        string nested = FirstUnresolved((JObject)item, created, ownerOf, result);
                        if (nested != null) return nested;
                    }
                    return null;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Resolves a decoded PPtr.  (0, 0) is a genuine null; m_FileID 0 goes through the shipped path_id -> rebuilt
        /// object mapping; m_FileID > 0 points into another shipped file and is bound to the project's copy of that
        /// object (see <see cref="ResolveExternal"/>).
        /// </summary>
        private static bool TryResolveReference(Dictionary<long, GameObject> created, Dictionary<long, long> ownerOf,
                                                JObject value, BuildResult result, out UnityEngine.Object target,
                                                out string why)
        {
            target = null;
            why = null;
            int fileId = (int)value["m_FileID"];
            long pathId = (long)value["m_PathID"];
            if (pathId == 0) return true;
            if (fileId != 0)
            {
                target = ResolveExternal(value["external"] as JObject, result, out why);
                return target != null;
            }
            target = ResolveReference(created, ownerOf, value);
            if (target == null) why = "target " + pathId + " (" + (string)value["target_class"] + ") unresolved";
            return target != null;
        }

        /// <summary>Resolves an in-scene PPtr through the shipped path_id -> rebuilt object mapping.</summary>
        private static UnityEngine.Object ResolveReference(Dictionary<long, GameObject> created,
                                                            Dictionary<long, long> ownerOf, JObject value)
        {
            if ((int)value["m_FileID"] != 0) return null;      // a different serialized file: not in this scene
            long targetPathId = (long)value["m_PathID"];
            string targetClass = (string)value["target_class"];
            // a GameObject reference (EventSystem.m_FirstSelected) names the object itself, not a component on it
            if (targetClass == "GameObject")
                return created.TryGetValue(targetPathId, out var gameObject) ? gameObject : null;
            long owner = ownerOf.TryGetValue(targetPathId, out long ownerPathId) ? ownerPathId : 0;
            if (owner == 0 || !created.TryGetValue(owner, out var host)) return null;
            Type type = FindType(targetClass);
            if (type == null) return null;
            return host.GetComponent(type);
        }

        private const string RenderTextureFolder = "Assets/GameContent/Uncategorized/RenderTexture";

        /// <summary>
        /// Binds a reference into another shipped file (decoded by type + name, e.g. sharedassets0's
        /// "BrandLogo_Acacia" Sprite) to the project's copy: exactly one asset of that type with that name, or
        /// nothing.  A RenderTexture is not an extracted asset, so it is authored from the shipped descriptor.
        /// </summary>
        private static UnityEngine.Object ResolveExternal(JObject external, BuildResult result, out string why)
        {
            why = null;
            if (external == null)
            {
                why = "external reference not decoded";
                return null;
            }
            string typeName = (string)external["type"];
            string name = (string)external["name"];
            JToken errorToken = external["error"];
            string error = errorToken == null || errorToken.Type == JTokenType.Null ? null : (string)errorToken;
            if (error != null || string.IsNullOrEmpty(typeName) || name == null)
            {
                why = "external " + (string)external["file"] + ":" + (long?)external["path_id"] + " unreadable (" +
                      (error ?? "no type or name decoded") + ")";
                return null;
            }
            if (typeName == "RenderTexture")
                return RenderTextureFor(name, external["descriptor"] as JObject, result, out why);

            Type type = typeName == "MonoBehaviour" ? FindType((string)external["script"]) : FindUnityType(typeName);
            if (type == null)
            {
                why = "external " + typeName + " '" + name + "': type " + ((string)external["script"] ?? typeName) +
                      " not found";
                return null;
            }
            // a name with path separators ("UI/Point") cannot be a search term; search by type alone then
            string filter = name.IndexOfAny(new[] { '/', '\\', ' ' }) < 0 ? name + " t:" + type.Name : "t:" + type.Name;
            var matches = new List<UnityEngine.Object>();
            foreach (string path in AssetDatabase.FindAssets(filter).Select(AssetDatabase.GUIDToAssetPath).Distinct())
            {
                foreach (UnityEngine.Object candidate in AssetDatabase.LoadAllAssetsAtPath(path))
                {
                    if (candidate != null && type.IsInstanceOfType(candidate) && candidate.name == name)
                        matches.Add(candidate);
                }
            }
            if (matches.Count != 1)
            {
                why = "external " + type.Name + " '" + name + "' (" + (string)external["file"] + "): " +
                      (matches.Count == 0 ? "no such asset in the project" : matches.Count + " assets share that name");
                return null;
            }
            result.external_references_bound.Add(type.Name + " '" + name + "' -> " + AssetDatabase.GetAssetPath(matches[0]));
            return matches[0];
        }

        private static Type FindUnityType(string typeName)
        {
            return FindType(typeName) ?? FindType("UnityEngine." + typeName);
        }

        /// <summary>
        /// The project's copy of a shipped RenderTexture: RenderTexture assets are not part of the extraction, so the
        /// texture is authored once with the shipped descriptor (size, formats, MSAA, mips, filtering, wrapping) and
        /// reused on later rebuilds - both the stage camera writing it and the RawImage showing it bind the same asset.
        /// </summary>
        private static UnityEngine.Object RenderTextureFor(string name, JObject descriptor, BuildResult result, out string why)
        {
            why = null;
            string path = RenderTextureFolder + "/" + name + ".renderTexture";
            var existing = AssetDatabase.LoadAssetAtPath<RenderTexture>(path);
            if (existing != null) return existing;
            if (descriptor == null)
            {
                why = "RenderTexture '" + name + "' has no decoded descriptor";
                return null;
            }
            var colorFormat = (UnityEngine.Experimental.Rendering.GraphicsFormat)(int)descriptor["color_format"];
            var depthFormat = (UnityEngine.Experimental.Rendering.GraphicsFormat)(int)descriptor["depth_stencil_format"];
            var texture = new RenderTexture((int)descriptor["width"], (int)descriptor["height"], colorFormat, depthFormat)
            {
                name = name,
                antiAliasing = Math.Max(1, (int)descriptor["anti_aliasing"]),
                dimension = (UnityEngine.Rendering.TextureDimension)(int)descriptor["dimension"],
                volumeDepth = Math.Max(1, (int)descriptor["volume_depth"]),
                useMipMap = (bool)descriptor["mip_map"],
                autoGenerateMips = (bool)descriptor["generate_mips"],
                filterMode = (FilterMode)(int)descriptor["filter_mode"],
                anisoLevel = (int)descriptor["aniso"],
                wrapModeU = (TextureWrapMode)(int)descriptor["wrap_u"],
                wrapModeV = (TextureWrapMode)(int)descriptor["wrap_v"],
            };
            Directory.CreateDirectory(RenderTextureFolder);
            AssetDatabase.CreateAsset(texture, path);
            result.render_textures_created.Add(path + " (" + descriptor["width"] + "x" + descriptor["height"] + " " +
                                               colorFormat + " / " + depthFormat + ", MSAA " + texture.antiAliasing + ")");
            return texture;
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
                {
                    var array = (JArray)value["value"];
                    property.vector2Value = new Vector2((float)array[0], (float)array[1]);
                    return true;
                }
                case SerializedPropertyType.Vector3:
                {
                    var array = (JArray)value["value"];
                    property.vector3Value = new Vector3((float)array[0], (float)array[1], (float)array[2]);
                    return true;
                }
                case SerializedPropertyType.Vector4:
                {
                    var array = (JArray)value["value"];
                    property.vector4Value = new Vector4((float)array[0], (float)array[1], (float)array[2], (float)array[3]);
                    return true;
                }
                case SerializedPropertyType.Quaternion:
                {
                    var array = (JArray)value["value"];
                    property.quaternionValue = new Quaternion((float)array[0], (float)array[1], (float)array[2], (float)array[3]);
                    return true;
                }
                case SerializedPropertyType.Rect:
                {
                    var array = (JArray)value["value"];
                    property.rectValue = new Rect((float)array[0], (float)array[1], (float)array[2], (float)array[3]);
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
            public List<string> external_references_bound = new List<string>();
            public List<string> render_textures_created = new List<string>();
            public List<string> camera_links = new List<string>();
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
