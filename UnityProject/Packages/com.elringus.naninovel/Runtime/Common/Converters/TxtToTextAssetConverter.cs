using System.IO;
using System.Text;
using UnityEngine;

namespace Naninovel
{
    public class TxtToTextAssetConverter : IRawConverter<TextAsset>
    {
        public RawDataRepresentation[] Representations { get; } = {
            new(".txt", "text/plain")
        };

        public TextAsset ConvertBlocking (byte[] obj, string path)
        {
            var textAsset = new TextAsset(Encoding.UTF8.GetString(obj));
            textAsset.name = Path.GetFileNameWithoutExtension(path);
            return textAsset;
        }

        public UniTask<TextAsset> Convert (byte[] obj, string path) => UniTask.FromResult(ConvertBlocking(obj, path));

        public object ConvertBlocking (object obj, string path) => ConvertBlocking(obj as byte[], path);

        public async UniTask<object> Convert (object obj, string path) => await Convert(obj as byte[], path);
    }
}
