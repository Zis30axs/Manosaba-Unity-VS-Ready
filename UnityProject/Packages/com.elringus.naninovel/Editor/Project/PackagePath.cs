using System.IO;
using JetBrains.Annotations;
using Naninovel.Metadata;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Provides paths to various package-related directories and resources; all paths are relative to project root.
    /// </summary>
    public static class PackagePath
    {
        public static string PackageRootPath => GetPackageRootPath();
        public static string EditorResourcesPath => Path.Combine(PackageRootPath, "Editor/Resources/Naninovel");
        public static string RuntimeResourcesPath => Path.Combine(PackageRootPath, "Resources/Naninovel");
        public static string PrefabsPath => Path.Combine(PackageRootPath, "Prefabs");
        public static string GeneratedDataPath => GetGeneratedDataPath();
        public static string TransientDataPath => cachedTransientDataPath ??= Ensure(Path.Combine(GeneratedDataPath, ".nani/Transient"));
        public static string TransientAssetPath => cachedTransientAssetPath ??= Ensure(Path.Combine(GeneratedDataPath, "Transient"));
        public static string ScriptsRoot => GetScriptsRoot();

        private static string cachedDataPath;
        private static string cachedTransientDataPath;
        private static string cachedTransientAssetPath;
        private static string cachedPackagePath;
        private static string cachedScriptsPath;

        private static string GetPackageRootPath ()
        {
            const string beacon = "Elringus.Naninovel.Editor.asmdef";
            if (string.IsNullOrEmpty(cachedPackagePath) || !Directory.Exists(cachedPackagePath))
                cachedPackagePath = FindInPackages() ?? FindInAssets();
            return cachedPackagePath ?? throw new Error("Failed to locate Naninovel package directory.");

            [CanBeNull]
            static string FindInPackages ()
            {
                // Even when package is installed as immutable (eg, local or git) and only physically
                // exists under Library/PackageCache/…, Unity will still symlink it to Packages/….
                const string dir = "Packages/com.elringus.naninovel";
                return Directory.Exists(dir) ? dir : null;
            }

            [CanBeNull]
            static string FindInAssets ()
            {
                var options = new EnumerationOptions {
                    IgnoreInaccessible = true,
                    RecurseSubdirectories = true,
                    ReturnSpecialDirectories = false,
                    AttributesToSkip = FileAttributes.System
                };
                foreach (var path in Directory.EnumerateFiles(Application.dataPath, beacon, options))
                    return PathUtils.AbsoluteToAssetPath(Path.GetDirectoryName(Path.GetDirectoryName(path)));
                return null;
            }
        }

        private static string GetGeneratedDataPath ()
        {
            const string beacon = ".naninovel.unity.data";
            if (string.IsNullOrEmpty(cachedDataPath) || !Directory.Exists(cachedDataPath))
                cachedDataPath = FindInAssets();
            if (!string.IsNullOrEmpty(cachedDataPath)) return cachedDataPath;
            const string defaultDir = "Assets/NaninovelData";
            const string defaultFile = defaultDir + "/" + beacon;
            Directory.CreateDirectory(defaultDir);
            File.WriteAllText(defaultFile, "");
            return cachedDataPath = defaultDir;

            [CanBeNull]
            static string FindInAssets ()
            {
                var options = new EnumerationOptions {
                    IgnoreInaccessible = true,
                    RecurseSubdirectories = true,
                    ReturnSpecialDirectories = false,
                    AttributesToSkip = FileAttributes.System
                };
                foreach (var path in Directory.EnumerateFiles(Application.dataPath, beacon, options))
                    return PathUtils.AbsoluteToAssetPath(Path.GetDirectoryName(path));
                return null;
            }
        }

        private static string GetScriptsRoot ()
        {
            if (string.IsNullOrEmpty(cachedScriptsPath) || !Directory.Exists(cachedScriptsPath))
                cachedScriptsPath = FindInAssets();
            if (!string.IsNullOrEmpty(cachedScriptsPath)) return cachedScriptsPath;
            const string defaultDir = "Assets/Scenario";
            Directory.CreateDirectory(defaultDir);
            return cachedScriptsPath = defaultDir;

            [CanBeNull]
            static string FindInAssets ()
            {
                var options = new EnumerationOptions {
                    IgnoreInaccessible = true,
                    RecurseSubdirectories = true,
                    ReturnSpecialDirectories = false,
                    AttributesToSkip = FileAttributes.System
                };
                var files = Directory.EnumerateFiles(Application.dataPath, "*.nani", options);
                return ScriptRootResolver.Resolve(files) is { } root ? PathUtils.AbsoluteToAssetPath(root) : null;
            }
        }

        private static string Ensure (string dir)
        {
            Directory.CreateDirectory(dir);
            return dir;
        }
    }
}
