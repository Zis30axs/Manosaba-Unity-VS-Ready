using System.Text;

namespace Naninovel
{
    public class GDocToScriptAssetConverter : IGoogleDriveConverter<Script>
    {
        public RawDataRepresentation[] Representations { get; } = {
            new(null, "application/vnd.google-apps.document")
        };

        public string ExportMimeType => "text/plain";

        public Script ConvertBlocking (byte[] obj, string path)
        {
            var localPath = path.GetAfterFirst(Engine.GetConfiguration<ScriptsConfiguration>().Loader.PathPrefix + "/");
            return Script.FromText(localPath, Encoding.UTF8.GetString(obj), path);
        }

        public UniTask<Script> Convert (byte[] obj, string path) => UniTask.FromResult(ConvertBlocking(obj, path));

        public object ConvertBlocking (object obj, string path) => ConvertBlocking(obj as byte[], path);

        public async UniTask<object> Convert (object obj, string path) => await Convert(obj as byte[], path);
    }
}
