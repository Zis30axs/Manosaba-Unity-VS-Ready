// Creates the materials the shipped build used but the extraction could not place ("Uncategorized"), at the
// path the plan names, so the 34 dangling `CustomSpriteMaterial` references in the imported Naninovel
// configuration assets have something to point at.
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.MaterialImporter.Run -logFile -
//
// Why
// ---
// `NaninovelConfigBuilder` closed 163 of the 200 PPtr nodes in the shipped configuration objects and left 37
// unbound; 34 of those 37 name just two objects, `Material "MaskableTransparent"` (shipped resources.assets
// path_id 2, 25 references) and `Material "MaskableTransparentToned_1"` (path_id 1, 9 references), reached
// through `BackgroundsConfiguration.Metadata.metas[0..2].CustomSpriteMaterial` and
// `CharactersConfiguration.Metadata.metas[*].CustomSpriteMaterial`.  Those materials are the ones Naninovel's
// actors render with: `TransitionalRenderer.CreateFor` passes the reference to
// `TransitionalSpriteRenderer.Initialize` (`Runtime/Transition/TransitionalRenderer.cs:82`), which uses it in
// place of its own default (`TransitionalSpriteRenderer.cs:72`:
// `SpriteMaterial = customSpriteMaterial ? new(customSpriteMaterial) : new(Shader.Find("Hidden/Naninovel/Transparent"))`).
// Leaving them null is therefore a rendering difference, not a cosmetic one.
//
// The input is `tools/unity-rebuild/out/unity/material_import_plan.json`, written by the pure-Python tool
// `tools/unity-rebuild/plan_material_import.py`, which reads the seven AssetRipper typetree dumps under
// `res/Uncategorized/Material/` (no asset path could be derived for them, hence "Uncategorized") and resolves
// everything that *can* be resolved without Unity: each material's shader pointer, the shader's recorded name,
// the property list from `m_SavedProperties`, the render queue / keywords / flags, and which already-imported
// content points at the material.
//
// What this tool does, per plan row
// ---------------------------------
//   1. find the shader by the name the plan recorded, among the project's own shader assets only
//      (`AssetDatabase.FindAssets("<name> t:Shader")` + an exact `Shader.name` comparison, so a prefix, a
//      case difference or a look-alike name can never be picked).  A name that matches nothing, or matches
//      more than one asset, is reported as SHADER_NOT_FOUND / SHADER_AMBIGUOUS and *nothing is created*:
//      substituting another shader would produce a material that renders differently while looking fixed.
//   2. load the asset at the plan's target path and reuse it, or create it - so a second run updates in place
//      instead of duplicating.  An existing asset that is not a Material is never overwritten.
//   3. apply every property the plan lists through `Material.Set*` by kind: `texenv` (texture + scale +
//      offset), `float`, `int`, `color`.  A `texenv` whose recorded texture the plan could not resolve still
//      gets its scale/offset (those are known even when the texture is not) and is reported with the plan's
//      reason; a texture that resolves is loaded and assigned.  An unknown kind, or a kind that does not
//      match the shader's own property type, is reported with its reason and skipped - never guessed.
//   4. set the name, the render queue (only when the shipped value is not -1, which means "use the shader's
//      Queue tag"), the keywords and the flags the plan records.
//   5. *close the dangling references* (`BindRecordedReferences`): for every material that was created or
//      updated, read the plan's `referenced_by.configuration_needs_binding` list and assign the created
//      material to exactly those `SerializedObject` properties on exactly those assets, under the same
//      conditions the configuration importer used when it left them null - the field path comes from that
//      importer's own report, the match is by exact material name and type, and a field whose recorded target
//      is a different name is never touched.  This is the step that turns 34 nulls into 34 bound references.
//
// Where the assets go, and why
// ----------------------------
// `Assets/GameContent/Uncategorized/Material/<Name>.mat`.  `a13_import_assets.py:dest_for` writes every
// imported object under `Assets/GameContent/<asset_path minus "Assets/">`, which is why the objects the
// extractor could not place already live in `Assets/GameContent/Uncategorized/{Font,Sprite,TextAsset,
// Texture2D}`; an uncategorized *material* belongs in the matching `Material` subfolder.  Nothing in this
// project looks a material up by path - the configuration holds object references, not resource keys - so the
// folder is an organisation choice, made to match the importer's existing convention, and the plan records it
// per material (`target_asset_path`).
//
// Exit codes: 0 = every planned material was created or updated, 1 = PARTIAL/ERROR (something was created but
// something else failed), 2 = BLOCKED (no plan input, or not one material could be created - which is what
// happens today, because all seven materials' shaders are absent from this project; each row says so).
// This mirrors the repo's other batchmode tools (NaninovelConfigBuilder.cs, Level0SceneBuilder.cs).
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public static class MaterialImporter
    {
        private const string InputRelativePath = "../tools/unity-rebuild/out/unity/material_import_plan.json";
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/material_import.json";

        /// <summary>Where a material goes when the plan does not say (it always does; this is the fallback and
        /// it is the same folder, so the two can never disagree silently).</summary>
        private const string DefaultFolder = "Assets/GameContent/Uncategorized/Material";

        private const int MaxMessagesPerRow = 200;

        [MenuItem("Manosaba/Import Uncategorized Materials")]
        public static void Run ()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute ()
        {
            var result = new ImportResult { generated = DateTime.UtcNow.ToString("o") };
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string inputPath = Path.GetFullPath(Path.Combine(projectRoot, InputRelativePath));
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));
            result.input = inputPath;

            int exitCode = 1;
            try
            {
                if (!File.Exists(inputPath))
                {
                    result.status = "BLOCKED";
                    result.error = "plan not found: " + inputPath +
                                   " (run tools/unity-rebuild/plan_material_import.py --write)";
                    exitCode = 2;
                }
                else
                {
                    result.input_bytes = new FileInfo(inputPath).Length;
                    JObject plan = JObject.Parse(File.ReadAllText(inputPath));
                    JArray materials = plan["materials"] as JArray;
                    if (materials == null || materials.Count == 0)
                    {
                        result.status = "BLOCKED";
                        result.error = "the plan contains no `materials` array: " + inputPath;
                        exitCode = 2;
                    }
                    else
                    {
                        result.plan_generated = (string)plan["generated"];
                        result.plan_status = (string)plan["status"];
                        result.target_folder = DefaultFolder;
                        result.target_folder_reason =
                            "a13_import_assets.py:dest_for writes imported objects under " +
                            "Assets/GameContent/<asset_path minus 'Assets/'>, so the objects the extractor could " +
                            "not place are already in Assets/GameContent/Uncategorized/{Font,Sprite,TextAsset," +
                            "Texture2D}; an uncategorized material goes in the matching Material subfolder. No " +
                            "engine lookup reads this path - the configuration holds object references - so this " +
                            "is the importer's convention, not an engine requirement.";
                        result.materials_total = materials.Count;

                        shaderCache = new Dictionary<string, Shader>(StringComparer.Ordinal);

                        var byName = new Dictionary<string, MaterialRow>(StringComparer.Ordinal);
                        foreach (JToken token in materials)
                        {
                            var row = new MaterialRow();
                            result.rows.Add(row);
                            try
                            {
                                ImportOne((JObject)token, row, result);
                            }
                            catch (Exception ex)
                            {
                                row.status = "ERROR";
                                row.reason = ex.GetType().Name + ": " + ex.Message;
                                row.notes.Add(ex.ToString());
                            }
                            // An exact-name match is the only rule used anywhere in this pass; a second row with
                            // the same name (the extraction carries two `Sprites-Default` objects) simply cannot
                            // claim the same asset, and that is recorded rather than resolved by preference.
                            if (!string.IsNullOrEmpty(row.material_name) && row.created_or_updated)
                            {
                                if (byName.ContainsKey(row.material_name))
                                    row.notes.Add("another plan row already created a material named '" +
                                                  row.material_name + "'; the reference-binding step uses that " +
                                                  "asset for this name");
                                else byName[row.material_name] = row;
                            }
                        }

                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();

                        BindRecordedReferences(plan, byName, result);
                        Tally(result);
                        result.limitations = BuildLimitations(result);
                        if (result.created_or_updated == 0)
                        {
                            result.status = "BLOCKED";
                            exitCode = 2;
                        }
                        else if (result.failed > 0 || result.partial > 0 || result.references_failed > 0)
                        {
                            result.status = "PARTIAL";
                            exitCode = 1;
                        }
                        else
                        {
                            result.status = "OK";
                            exitCode = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[MaterialImporter] " + ex);
                exitCode = 1;
            }

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));
                File.WriteAllText(outPath, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
            }
            catch (Exception ex)
            {
                Debug.LogError("[MaterialImporter] failed to write " + outPath + ": " + ex.Message);
            }

            Debug.Log("[MaterialImporter] " + result.status +
                      " materials=" + result.materials_total +
                      " created=" + result.created + " updated=" + result.updated + " failed=" + result.failed +
                      " properties_applied=" + result.properties_applied +
                      " properties_skipped=" + result.properties_skipped +
                      " references_bound=" + result.references_bound +
                      " references_failed=" + result.references_failed +
                      " wrote " + outPath);
            return exitCode;
        }

        // ------------------------------------------------------------------------------------------------------------
        // One planned material
        // ------------------------------------------------------------------------------------------------------------

        private static void ImportOne (JObject planned, MaterialRow row, ImportResult result)
        {
            row.source_file = (string)planned["source_file"];
            row.material_name = (string)planned["name"] ?? "";
            row.shipped_path_id = ReadLong(planned["shipped_path_id"]);
            row.shipped_shader_name = (string)planned["shipped_shader_name"];
            row.asset_path = (string)planned["target_asset_path"];
            row.plan_status = (string)planned["status"];

            if (string.IsNullOrEmpty(row.asset_path))
            {
                row.status = "FAILED";
                row.reason = "the plan row carries no target_asset_path";
                return;
            }

            // --- 1. the shader --------------------------------------------------------------------------
            var resolution = planned["shader_resolution"] as JObject;
            string state = resolution == null ? null : (string)resolution["state"];
            string recordedReason = resolution == null ? "the plan row carries no shader_resolution" :
                (string)resolution["reason"];
            if (state == "ambiguous_in_project")
            {
                row.status = "SHADER_AMBIGUOUS";
                row.reason = recordedReason + " (candidates: " +
                             string.Join(", ", ReadStrings(resolution["asset_paths"]).ToArray()) + ")";
                result.skipped_materials++;
                return;
            }
            if (state != "found" || string.IsNullOrEmpty(row.shipped_shader_name))
            {
                row.status = "SHADER_NOT_FOUND";
                row.reason = recordedReason;
                var lookalikes = ReadStrings(resolution == null ? null : resolution["closest_project_names"]);
                if (lookalikes.Count > 0)
                    row.notes.Add("shaders whose names resemble this one exist but are NOT substituted: " +
                                  string.Join(", ", lookalikes.ToArray()));
                result.shader_not_found++;
                result.skipped_materials++;
                return;
            }
            Shader shader = FindShaderByName(row.shipped_shader_name);
            if (shader == null)
            {
                row.status = "SHADER_NOT_FOUND";
                row.reason = "the plan recorded '" + row.shipped_shader_name + "' as present (" +
                             ((string)resolution["asset_path"]) + "), but no loaded shader asset declares that " +
                             "exact name at run time; nothing was created rather than substituting one";
                result.shader_not_found++;
                result.skipped_materials++;
                return;
            }
            row.shader_used = shader.name;
            row.shader_asset_path = AssetDatabase.GetAssetPath(shader);

            // --- 2. create or load (idempotency) ---------------------------------------------------------
            Material material = AssetDatabase.LoadAssetAtPath<Material>(row.asset_path);
            bool createdNow = false;
            if (material == null)
            {
                string absolute = AbsoluteAssetPath(row.asset_path);
                if (absolute != null && File.Exists(absolute))
                {
                    row.status = "FAILED";
                    row.reason = "a file already exists at " + row.asset_path +
                                 " but does not load as a Material; refusing to overwrite it";
                    result.failed++;
                    return;
                }
                EnsureFolder(Path.GetDirectoryName(row.asset_path).Replace('\\', '/'));
                material = new Material(shader);
                if (material == null)
                {
                    row.status = "FAILED";
                    row.reason = "new Material(shader) returned null";
                    result.failed++;
                    return;
                }
                if (!string.IsNullOrEmpty(row.material_name)) material.name = row.material_name;
                createdNow = true;
            }
            else if (material.shader != shader)
            {
                // Reusing the asset is only safe when it really is the material the plan describes; a different
                // shader means the asset is not this material, and re-pointing it would silently change it.
                row.status = "FAILED";
                row.reason = "the existing asset " + row.asset_path + " uses shader '" +
                             (material.shader == null ? "<null>" : material.shader.name) + "', not '" +
                             shader.name + "'; refusing to re-point an existing material";
                result.failed++;
                return;
            }

            // --- 3. properties ---------------------------------------------------------------------------
            JArray properties = planned["properties"] as JArray;
            if (properties != null)
            {
                foreach (JToken token in properties)
                {
                    var property = token as JObject;
                    if (property == null) continue;
                    ApplyProperty(material, property, row);
                }
            }
            else
            {
                row.notes.Add("the plan row carries no properties array: the material keeps the shader's own " +
                              "defaults for every property");
            }

            // --- 4. state ---------------------------------------------------------------------------------
            var stateNode = planned["state"] as JObject;
            if (stateNode != null)
            {
                foreach (string keyword in ReadStrings(stateNode["m_ValidKeywords"]))
                {
                    material.EnableKeyword(keyword);
                    row.keywords_enabled.Add(keyword);
                }
                foreach (string keyword in ReadStrings(stateNode["m_InvalidKeywords"]))
                {
                    if (string.IsNullOrEmpty(keyword)) continue;
                    row.notes.Add("the shipped material lists '" + keyword + "' in m_InvalidKeywords (a keyword " +
                                  "the shader no longer declares); nothing was enabled for it");
                }
                if (stateNode["m_EnableInstancingVariants"] != null &&
                    (bool)stateNode["m_EnableInstancingVariants"])
                {
                    material.enableInstancing = true;
                    row.notes.Add("m_EnableInstancingVariants = true applied through Material.enableInstancing");
                }
                if (stateNode["m_DoubleSidedGI"] != null && (bool)stateNode["m_DoubleSidedGI"])
                    row.notes.Add("m_DoubleSidedGI = true has no Material API and was not applied (the shipped " +
                                  "material sets it)");
                var tags = stateNode["m_StringTagMap"] as JArray;
                if (tags != null && tags.Count > 0)
                    row.notes.Add("m_StringTagMap " + tags.ToString(Newtonsoft.Json.Formatting.None) +
                                  " is render-pipeline bookkeeping; Material exposes no API for it and the " +
                                  "shader's own tags supply the RenderType, so it was not applied");
                var buildStacks = stateNode["m_BuildTextureStacks"] as JArray;
                if (buildStacks != null && buildStacks.Count > 0)
                    row.notes.Add("m_BuildTextureStacks is non-empty (" + buildStacks.Count + ") and is not " +
                                  "applied by this pass");
            }

            long customQueue = ReadLong(planned["render_queue"] == null
                ? null : planned["render_queue"]["m_CustomRenderQueue"], long.MinValue);
            if (customQueue != long.MinValue && customQueue != -1)
            {
                material.renderQueue = (int)customQueue;
                row.render_queue = (int)customQueue;
                row.notes.Add("renderQueue set from the shipped m_CustomRenderQueue " + customQueue);
            }
            else
            {
                row.render_queue = material.renderQueue;
                row.notes.Add("the shipped m_CustomRenderQueue is -1, i.e. 'use the shader's Queue tag'; " +
                              "renderQueue was left at the value the shader gives it (" + material.renderQueue +
                              ") rather than written as a guessed number");
            }

            // --- 5. save ---------------------------------------------------------------------------------
            EditorUtility.SetDirty(material);
            if (createdNow) AssetDatabase.CreateAsset(material, row.asset_path);

            row.created_or_updated = true;
            if (createdNow) result.created++; else result.updated++;
            if (!createdNow)
                row.notes.Add("the asset already existed and was re-applied in place (idempotent re-run)");
            // A material that exists but whose property list did not fully apply is not the same result as a
            // clean one, and the difference must survive into the status rather than only into the row's reasons.
            if (row.properties_skipped > 0) row.status = "PARTIAL";
            else row.status = createdNow ? "CREATED" : "UPDATED";
        }

        /// <summary>
        /// Apply one planned property by kind.  Nothing is dropped silently: every row that does not reach the
        /// material appears in `properties_skipped_list` with its reason.
        /// </summary>
        private static void ApplyProperty (Material material, JObject property, MaterialRow row)
        {
            string name = (string)property["name"];
            string kind = (string)property["kind"];
            if (string.IsNullOrEmpty(name))
            {
                Skip(row, name, kind, "the plan row carries no property name: " + property.ToString(Newtonsoft.Json.Formatting.None));
                return;
            }
            row.properties_total++;
            if (!material.HasProperty(name))
            {
                Skip(row, name, kind, "the shader '" + row.shader_used + "' declares no property with this name " +
                                      "(Material.HasProperty is false), so the value cannot be applied and the " +
                                      "shader's own default stands");
                return;
            }

            switch (kind)
            {
                case "texenv":
                {
                    var textureRecord = property["texture"] as JObject;
                    string textureState = textureRecord == null ? null : (string)textureRecord["resolution"];
                    string texturePath = textureRecord == null ? null : (string)textureRecord["asset_path"];
                    if (textureState == "resolved" && !string.IsNullOrEmpty(texturePath))
                    {
                        var texture = AssetDatabase.LoadAssetAtPath<Texture>(texturePath);
                        if (texture == null)
                        {
                            Skip(row, name, kind, "the plan resolved the texture to " + texturePath +
                                                  " but it does not load as a Texture");
                        }
                        else
                        {
                            material.SetTexture(name, texture);
                            row.Apply(name, kind, "texture " + texturePath);
                        }
                    }
                    else if (textureState == "null_reference")
                    {
                        material.SetTexture(name, null);
                        row.Apply(name, kind, "null (the shipped material binds no texture here)");
                    }
                    else
                    {
                        Skip(row, name, kind, "the texture was not assigned: " +
                                              (textureRecord == null ? "the plan row carries no texture record" :
                                               (string)textureRecord["reason"]));
                    }

                    // scale/offset are known even when the texture is not, and Unity keeps them per property
                    // name, so they are applied either way.
                    var scale = property["scale"] as JArray;
                    var offset = property["offset"] as JArray;
                    if (scale != null && scale.Count == 2)
                        material.SetTextureScale(name, new Vector2(ReadFloat(scale[0]), ReadFloat(scale[1])));
                    if (offset != null && offset.Count == 2)
                        material.SetTextureOffset(name, new Vector2(ReadFloat(offset[0]), ReadFloat(offset[1])));
                    return;
                }

                case "float":
                {
                    var value = property["value"];
                    if (value == null || value.Type == JTokenType.Null)
                    {
                        Skip(row, name, kind, "the plan row carries no value");
                        return;
                    }
                    material.SetFloat(name, ReadFloat(value));
                    row.Apply(name, kind, ReadFloat(value).ToString("R"));
                    return;
                }

                case "int":
                {
                    var value = property["value"];
                    if (value == null || value.Type == JTokenType.Null)
                    {
                        Skip(row, name, kind, "the plan row carries no value");
                        return;
                    }
                    long asLong = ReadLong(value, long.MinValue);
                    if (asLong == long.MinValue)
                    {
                        Skip(row, name, kind, "the value is not an integer: " + value.ToString());
                        return;
                    }
                    material.SetInteger(name, (int)asLong);
                    row.Apply(name, kind, asLong.ToString());
                    return;
                }

                case "color":
                {
                    var value = property["value"] as JObject;
                    if (value == null)
                    {
                        Skip(row, name, kind, "the plan row carries no {r,g,b,a} value");
                        return;
                    }
                    float r = ReadFloat(value["r"]), g = ReadFloat(value["g"]),
                          b = ReadFloat(value["b"]), a = ReadFloat(value["a"], 1f);
                    if (float.IsInfinity(r) || float.IsInfinity(g) || float.IsInfinity(b) || float.IsInfinity(a) ||
                        float.IsNaN(r) || float.IsNaN(g) || float.IsNaN(b) || float.IsNaN(a))
                    {
                        Skip(row, name, kind, "the shipped value is not finite (" + value.ToString(
                            Newtonsoft.Json.Formatting.None) + "); writing it would put NaN/Inf into the asset " +
                            "where the shipped object's meaning is a shader-side sentinel, so it was skipped");
                        return;
                    }
                    var color = new Color(r, g, b, a);
                    material.SetColor(name, color);
                    row.Apply(name, kind, "(" + r.ToString("R") + ", " + g.ToString("R") + ", " + b.ToString("R") +
                                          ", " + a.ToString("R") + ")");
                    return;
                }

                default:
                    Skip(row, name, kind, "no applier for property kind '" + kind + "' (the plan's own report " +
                                          "carries the raw value under this row)");
                    return;
            }
        }

        // ------------------------------------------------------------------------------------------------------------
        // Closing the dangling configuration references
        // ------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Assign each created material to the fields that named it, using the configuration importer's own
        /// `needs_binding` records as the field list.
        ///
        /// The three conditions are deliberately strict, because a wrong assignment here would look like a fix:
        ///   * the record's `target_class` must be exactly "Material";
        ///   * `target_name` must equal the created material's name exactly (ordinal, no case folding);
        ///   * the owner asset must exist, and the field path must resolve to an object-reference property whose
        ///     current value is still null (the importer left it null; a non-null value means somebody else has
        ///     already bound it and this pass must not overwrite that).
        /// Nothing is matched by resemblance.
        /// </summary>
        private static void BindRecordedReferences (JObject plan, Dictionary<string, MaterialRow> byName,
                                                    ImportResult result)
        {
            if (byName.Count == 0)
            {
                result.reference_binding_note =
                    "no material was created, so there is nothing to bind; the configuration assets keep the " +
                    "nulls the configuration import recorded, and each `needs_binding` record still carries the " +
                    "exact field path a later pass has to assign.";
                return;
            }

            var owners = new Dictionary<string, SerializedObject>(StringComparer.Ordinal);
            var dirty = new HashSet<string>(StringComparer.Ordinal);

            foreach (JToken token in plan["materials"] as JArray)
            {
                var row = token as JObject;
                if (row == null) continue;
                string name = (string)row["name"];
                if (string.IsNullOrEmpty(name) || !byName.ContainsKey(name)) continue;

                var materialRow = byName[name];
                var material = AssetDatabase.LoadAssetAtPath<Material>(materialRow.asset_path);
                if (material == null)
                {
                    result.warnings.Add("the created material " + materialRow.asset_path +
                                        " could not be reloaded, so its recorded references were not bound");
                    continue;
                }

                var referencedBy = row["referenced_by"] as JObject;
                var records = referencedBy == null ? null : referencedBy["configuration_needs_binding"] as JArray;
                if (records == null) continue;

                foreach (JToken recordToken in records)
                {
                    var record = recordToken as JObject;
                    if (record == null) continue;
                    var binding = new BindingRow {
                        target_class = (string)record["target_class"],
                        target_name = (string)record["target_name"],
                        material_asset = materialRow.asset_path,
                        owner_asset = (string)record["owner_asset"],
                        field = (string)record["field"]
                    };
                    result.bindings.Add(binding);

                    if (!string.Equals(binding.target_class, "Material", StringComparison.Ordinal))
                    {
                        binding.status = "SKIPPED";
                        binding.reason = "the recorded target class is '" + binding.target_class +
                                         "', not Material";
                        result.references_failed++;
                        continue;
                    }
                    if (!string.Equals(binding.target_name, name, StringComparison.Ordinal))
                    {
                        binding.status = "SKIPPED";
                        binding.reason = "the recorded target name '" + binding.target_name +
                                         "' is not the created material's name '" + name + "'";
                        result.references_failed++;
                        continue;
                    }
                    if (string.IsNullOrEmpty(binding.owner_asset) || string.IsNullOrEmpty(binding.field))
                    {
                        binding.status = "SKIPPED";
                        binding.reason = "the record carries no owner asset or no field path";
                        result.references_failed++;
                        continue;
                    }

                    SerializedObject serialized;
                    if (!owners.TryGetValue(binding.owner_asset, out serialized))
                    {
                        var owner = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(binding.owner_asset);
                        if (owner == null)
                        {
                            binding.status = "SKIPPED";
                            binding.reason = "the owning asset does not exist in this project";
                            result.references_failed++;
                            continue;
                        }
                        serialized = new SerializedObject(owner);
                        owners[binding.owner_asset] = serialized;
                    }

                    SerializedProperty property = serialized.FindProperty(binding.field);
                    if (property == null)
                    {
                        binding.status = "SKIPPED";
                        binding.reason = "the asset has no serialized property at this path";
                        result.references_failed++;
                        continue;
                    }
                    if (property.propertyType != SerializedPropertyType.ObjectReference)
                    {
                        binding.status = "SKIPPED";
                        binding.reason = "the property at this path is " + property.propertyType +
                                         ", not an object reference";
                        result.references_failed++;
                        continue;
                    }
                    if (property.objectReferenceValue != null)
                    {
                        binding.status = "SKIPPED";
                        binding.reason = "the field already holds '" + property.objectReferenceValue.name +
                                         "'; this pass does not overwrite a binding somebody else made";
                        result.references_failed++;
                        continue;
                    }

                    property.objectReferenceValue = material;
                    binding.status = "BOUND";
                    binding.reason = "assigned " + materialRow.asset_path + " to " + binding.owner_asset + "." +
                                     binding.field;
                    result.references_bound++;
                    string key = binding.owner_asset + "\u0000" + binding.field;
                    dirty.Add(key);
                }
            }

            foreach (var pair in owners)
            {
                bool touched = false;
                foreach (string key in dirty)
                    if (key.StartsWith(pair.Key + "\u0000", StringComparison.Ordinal)) { touched = true; break; }
                if (!touched) continue;
                pair.Value.ApplyModifiedPropertiesWithoutUndo();
                EditorUtility.SetDirty(pair.Value.targetObject);
            }
            AssetDatabase.SaveAssets();
        }

        // ------------------------------------------------------------------------------------------------------------
        // Shader lookup
        // ------------------------------------------------------------------------------------------------------------

        private static Dictionary<string, Shader> shaderCache;

        /// <summary>
        /// The project's own shader asset whose `Shader.name` is exactly this string, or null.
        ///
        /// `AssetDatabase.FindAssets` matches substrings, so every hit is confirmed by an ordinal comparison -
        /// and a name with more than one exact match is reported instead of picked (see the plan's
        /// `ambiguous_in_project` state, which reaches this method as SHADER_AMBIGUOUS).
        /// </summary>
        private static Shader FindShaderByName (string name)
        {
            Shader cached;
            if (shaderCache.TryGetValue(name, out cached)) return cached;

            Shader found = null;
            try
            {
                string filter = "t:Shader";
                string[] guids = AssetDatabase.FindAssets(filter);
                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    if (string.IsNullOrEmpty(path)) continue;
                    var shader = AssetDatabase.LoadAssetAtPath<Shader>(path);
                    if (shader == null) continue;
                    if (!string.Equals(shader.name, name, StringComparison.Ordinal)) continue;
                    found = shader;
                    break;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning("[MaterialImporter] shader lookup for '" + name + "' failed: " + ex.Message);
            }
            shaderCache[name] = found;
            return found;
        }

        // ------------------------------------------------------------------------------------------------------------
        // Helpers
        // ------------------------------------------------------------------------------------------------------------

        private static void Skip (MaterialRow row, string name, string kind, string reason)
        {
            row.properties_skipped++;
            if (row.properties_skipped_list.Count < MaxMessagesPerRow)
                row.properties_skipped_list.Add(new SkippedProperty { name = name, kind = kind, reason = reason });
            else row.skipped_truncated++;
        }

        private static List<string> ReadStrings (JToken token)
        {
            var list = new List<string>();
            var array = token as JArray;
            if (array == null) return list;
            foreach (JToken item in array)
                if (item != null && item.Type != JTokenType.Null) list.Add(item.ToString());
            return list;
        }

        private static float ReadFloat (JToken token, float fallback = 0f)
        {
            if (token == null || token.Type == JTokenType.Null) return fallback;
            try
            {
                if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
                    return token.Value<float>();
                float parsed;
                return float.TryParse(token.ToString(), System.Globalization.NumberStyles.Float,
                                      System.Globalization.CultureInfo.InvariantCulture, out parsed)
                    ? parsed : fallback;
            }
            catch (Exception) { return fallback; }
        }

        private static long ReadLong (JToken token, long fallback = 0L)
        {
            if (token == null || token.Type == JTokenType.Null) return fallback;
            try { return token.Value<long>(); }
            catch (Exception) { return fallback; }
        }

        private static void EnsureFolder (string assetFolder)
        {
            if (string.IsNullOrEmpty(assetFolder) || AssetDatabase.IsValidFolder(assetFolder)) return;
            int slash = assetFolder.LastIndexOf('/');
            if (slash <= 0) return;
            string parent = assetFolder.Substring(0, slash);
            string leaf = assetFolder.Substring(slash + 1);
            EnsureFolder(parent);
            if (!AssetDatabase.IsValidFolder(assetFolder))
                AssetDatabase.CreateFolder(parent, leaf);
        }

        /// <summary>The absolute path of an asset path, or null when the path cannot be built at all (a caller
        /// must treat null as "unknown", never as "does not exist").</summary>
        private static string AbsoluteAssetPath (string assetPath)
        {
            try
            {
                string projectRoot = Directory.GetParent(Application.dataPath).FullName;
                return Path.GetFullPath(Path.Combine(projectRoot, assetPath.Replace('/', Path.DirectorySeparatorChar)));
            }
            catch (Exception) { return null; }
        }

        private static void Tally (ImportResult result)
        {
            foreach (MaterialRow row in result.rows)
            {
                result.properties_total += row.properties_total;
                result.properties_applied += row.properties_applied;
                result.properties_skipped += row.properties_skipped;
                if (row.status == "PARTIAL") result.partial++;
            }
            result.created_or_updated = result.created + result.updated;
            result.bindings_planned = result.bindings.Count;
        }

        private static List<string> BuildLimitations (ImportResult result)
        {
            var limitations = new List<string>();
            limitations.Add("ASSET CONTENT: " + result.created + " material(s) created and " + result.updated +
                            " updated in place out of " + result.materials_total + " planned. An existing asset at " +
                            "a target path is loaded and re-applied, so re-running this tool neither duplicates " +
                            "nor corrupts anything; an existing asset that is not a Material, or that uses a " +
                            "different shader, is refused rather than overwritten.");
            limitations.Add("SHADERS ARE NOT SUBSTITUTED: " + result.shader_not_found + " material(s) were not " +
                            "created because their shader has no exact-name asset in this project. The plan " +
                            "records the shipped shader's name (read from the extraction's Shader object) and the " +
                            "names that merely resemble it; a resemblance is never turned into a binding, because " +
                            "a material built on a look-alike shader renders differently while appearing fixed. " +
                            "Creating these materials needs the shader's source recovered first - a separate job.");
            limitations.Add("WHERE THE ASSETS GO: " + result.target_folder + " - " + result.target_folder_reason);
            limitations.Add("REFERENCE BINDING: " + result.references_bound + " of " + result.bindings_planned +
                            " recorded configuration reference(s) were assigned, " + result.references_failed +
                            " were not (each with its reason in `bindings`). A record is bound only when its " +
                            "target class is exactly 'Material', its target name equals the created material's " +
                            "name exactly, the owning asset exists, the field path resolves to an object " +
                            "reference, and that field is still null (a field somebody else already filled is " +
                            "left alone). Nothing here matches by resemblance, and the configuration assets " +
                            "themselves are not modified by any other means.");
            limitations.Add("PROPERTIES: " + result.properties_applied + " of " + result.properties_total +
                            " planned property row(s) were applied and " + result.properties_skipped +
                            " skipped, each with a reason in its material's `properties_skipped_list`. A texEnv " +
                            "whose texture the plan could not resolve still gets its scale/offset, because those " +
                            "are known even when the texture is not. A non-finite colour component is skipped " +
                            "rather than written as NaN/Inf.");
            limitations.Add("NOT VERIFIED WITHOUT RUNNING UNITY: this file was type-checked with Unity's own " +
                            "Roslyn against Assembly-CSharp-Editor's reference set but has not been executed, so " +
                            "that `AssetDatabase.FindAssets(\"t:Shader\")` finds the shader, that " +
                            "`SerializedObject.FindProperty` resolves each recorded field path on the real " +
                            "configuration assets, and that the created materials render like the shipped ones " +
                            "are all unverified here; the tool reports what it finds at run time instead of " +
                            "assuming it.");
            return limitations;
        }

        // ------------------------------------------------------------------------------------------------------------
        // Report
        // ------------------------------------------------------------------------------------------------------------

        [Serializable]
        public class ImportResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string input;
            public long input_bytes;
            public string plan_generated;
            public string plan_status;
            public string error;

            public string target_folder;
            public string target_folder_reason;

            public int materials_total;
            public int created;
            public int updated;
            public int created_or_updated;
            public int partial;
            public int failed;
            public int shader_not_found;
            public int skipped_materials;

            public int properties_total;
            public int properties_applied;
            public int properties_skipped;

            public int bindings_planned;
            public int references_bound;
            public int references_failed;
            public string reference_binding_note;

            public List<MaterialRow> rows = new List<MaterialRow>();
            public List<BindingRow> bindings = new List<BindingRow>();
            public List<string> limitations = new List<string>();
            public List<string> warnings = new List<string>();
        }

        [Serializable]
        public class MaterialRow
        {
            public string source_file;
            public string material_name;
            public long shipped_path_id;
            public string shipped_shader_name;
            public string shader_used;
            public string shader_asset_path;
            public string asset_path;
            public string plan_status;
            public string status = "NOT_RUN";
            public string reason;
            public bool created_or_updated;
            public int render_queue;

            public int properties_total;
            public int properties_applied;
            public int properties_skipped;
            public int skipped_truncated;

            public List<SkippedProperty> properties_skipped_list = new List<SkippedProperty>();
            public List<string> keywords_enabled = new List<string>();
            public List<string> notes = new List<string>();

            public void Apply (string name, string kind, string detail)
            {
                properties_applied++;
                if (notes.Count < MaxMessagesPerRow) notes.Add(kind + " " + name + " = " + detail);
            }
        }

        [Serializable]
        public class SkippedProperty
        {
            public string name;
            public string kind;
            public string reason;
        }

        [Serializable]
        public class BindingRow
        {
            public string target_class;
            public string target_name;
            public string material_asset;
            public string owner_asset;
            public string field;
            public string status = "NOT_RUN";
            public string reason;
        }
    }
}
