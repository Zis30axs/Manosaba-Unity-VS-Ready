using System;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    [CustomEditor(typeof(EngineVersion))]
    public class EngineVersionEditor : Editor
    {
        protected static string GitHubProjectPath => PlayerPrefs.GetString(nameof(GitHubProjectPath), string.Empty);

        private const string packageTextTemplate = @"{
    ""name"": ""com.elringus.naninovel"",
    ""version"": ""{PACKAGE_VERSION}"",
    ""displayName"": ""Naninovel"",
    ""description"": ""Writer-friendly visual novel engine."",
    ""unity"": ""2022.3"",
    ""author"": { ""name"": ""Elringus"", ""url"": ""https://naninovel.com"" },
    ""licensesUrl"": ""https://naninovel.com/eula"",
    ""documentationUrl"": ""https://naninovel.com/guide"",
    ""changelogUrl"": ""https://naninovel.com/releases/{GIT_VERSION}"",
    ""keywords"": [ ""vn"", ""nvl"", ""adv"", ""interactive fiction"", ""text adventure"", ""framework"" ],
    ""dependencies"": {
        ""com.unity.modules.audio"": ""1.0.0"",
        ""com.unity.modules.video"": ""1.0.0"",
        ""com.unity.modules.imgui"": ""1.0.0"",
        ""com.unity.modules.uielements"": ""1.0.0"",
        ""com.unity.modules.particlesystem"": ""1.0.0"",
        ""com.unity.modules.imageconversion"": ""1.0.0"",
        ""com.unity.ugui"": ""1.0.0"",
        ""com.unity.textmeshpro"": ""3.0.9"",
        ""com.unity.nuget.newtonsoft-json"": ""3.2.1""
    }
}
";

        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update", GUIStyles.NavigationButton))
                Update();
        }

        public static void Update ()
        {
            var asset = EngineVersion.LoadFromResources();
            using var serializedObj = new SerializedObject(asset);
            serializedObj.Update();
            var engineVersionProperty = serializedObj.FindProperty("engineVersion");
            var buildDateProperty = serializedObj.FindProperty("buildDate");
            buildDateProperty.stringValue = $"{DateTime.Now:yyyy-MM-dd}";
            serializedObj.ApplyModifiedProperties();

            var gitVersion = engineVersionProperty.stringValue;
            var packageText = packageTextTemplate
                .Replace("{GIT_VERSION}", gitVersion)
                .Replace("{PACKAGE_VERSION}", asset.BuildVersionTag());
            var packagePath = PathUtils.Combine(PackagePath.PackageRootPath, "package.json");
            File.WriteAllText(packagePath, packageText);

            EditorUtility.SetDirty(asset);
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private static DateTime ParseBuildDate (string buildDate)
        {
            var parsed = DateTime.TryParseExact(buildDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result);
            return parsed ? result : DateTime.MinValue;
        }
    }
}
