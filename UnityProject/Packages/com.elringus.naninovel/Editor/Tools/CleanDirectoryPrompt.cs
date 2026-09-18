using System.IO;
using System.Linq;
using UnityEditor;

namespace Naninovel
{
    public static class CleanDirectoryPrompt
    {
        public static bool PromptIfNotEmpty (DirectoryInfo directory)
        {
            if (directory.Exists && directory.EnumerateFileSystemInfos().Any())
                return EditorUtility.DisplayDialog("Clean directory?",
                    $"The operation requires deleting all the content in \"{directory.FullName}\", which is not empty.", "OK", "Cancel");
            return true;
        }

        public static bool PromptIfNotEmpty (string directory)
        {
            return PromptIfNotEmpty(new DirectoryInfo(directory));
        }
    }
}
