using System.IO;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Converts <see cref="T:byte[]"/> raw data of a .png image to <see cref="Texture2D"/>.
    /// </summary>
    public class PngToTextureConverter : IRawConverter<Texture2D>
    {
        public RawDataRepresentation[] Representations { get; } = {
            new(".png", "image/png")
        };

        public Texture2D ConvertBlocking (byte[] obj, string path)
        {
            var texture = new Texture2D(2, 2);
            texture.name = Path.GetFileNameWithoutExtension(path);
            texture.LoadImage(obj, true);
            return texture;
        }

        public UniTask<Texture2D> Convert (byte[] obj, string path) => UniTask.FromResult(ConvertBlocking(obj, path));

        public object ConvertBlocking (object obj, string path) => ConvertBlocking(obj as byte[], path);

        public async UniTask<object> Convert (object obj, string path) => await Convert(obj as byte[], path);
    }
}
