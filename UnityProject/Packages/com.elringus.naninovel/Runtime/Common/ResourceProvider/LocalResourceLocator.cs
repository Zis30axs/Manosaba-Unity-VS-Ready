using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Naninovel
{
    public class LocalResourceLocator<TResource> : LocateResourcesRunner<TResource>
        where TResource : UnityEngine.Object
    {
        public virtual string RootPath { get; }

        private readonly IEnumerable<IRawConverter<TResource>> converters;

        public LocalResourceLocator (IResourceProvider provider, string rootPath, string resourcesPath,
            IEnumerable<IRawConverter<TResource>> converters) : base(provider, resourcesPath)
        {
            RootPath = rootPath;
            this.converters = converters;
        }

        public override UniTask Run ()
        {
            var locatedResourcePaths = LocateResources();
            SetResult(locatedResourcePaths);
            return UniTask.CompletedTask;
        }

        private IReadOnlyCollection<string> LocateResources ()
        {
            var locatedResources = new List<string>();

            // 1. Resolving parent folder.
            var folderPath = RootPath;
            if (!string.IsNullOrEmpty(Path))
                folderPath += $"/{Path}";
            var parentFolder = new DirectoryInfo(folderPath);
            if (!parentFolder.Exists) return locatedResources;

            // 2. Searching for the files in the folder.
            var results = new Dictionary<RawDataRepresentation, List<FileInfo>>();
            foreach (var converter in converters)
            foreach (var representation in converter.Representations.DistinctBy(r => r.Extension))
            {
                var files = parentFolder.GetFiles($"*{representation.Extension}", SearchOption.AllDirectories).ToList();
                if (files.Count > 0) results.Add(representation, files);
            }

            // 3. Create resources using located files.
            foreach (var result in results)
            foreach (var file in result.Value)
            {
                var fileName = string.IsNullOrEmpty(result.Key.Extension) ? file.Name : file.Name.GetBeforeLast(".");
                var filePath = string.IsNullOrEmpty(Path) ? fileName : $"{Path}{file.FullName.Replace("\\", "/").GetAfterFirst(Path).GetBeforeLast(fileName)}{fileName}";
                locatedResources.Add(filePath);
            }

            return locatedResources;
        }
    }
}
