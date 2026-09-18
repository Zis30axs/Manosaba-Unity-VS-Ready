using System.IO;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Converts <see cref="T:byte[]"/> raw data of a .png image to <see cref="Sprite"/>.
    /// </summary>
    public class PngToSpriteConverter : IRawConverter<Sprite>
    {
        public RawDataRepresentation[] Representations { get; } = {
            new(".png", "image/png")
        };

        public Sprite ConvertBlocking (byte[] obj, string path)
        {
            var texture = new Texture2D(2, 2);
            texture.name = Path.GetFileNameWithoutExtension(path);
            texture.LoadImage(obj, true);
            var rect = new Rect(0, 0, texture.width, texture.height);
            var sprite = Sprite.Create(texture, rect, Vector2.one * .5f);
            return sprite;
        }

        public UniTask<Sprite> Convert (byte[] obj, string path) => UniTask.FromResult(ConvertBlocking(obj, path));

        public object ConvertBlocking (object obj, string path) => ConvertBlocking(obj as byte[], path);

        public async UniTask<object> Convert (object obj, string path) => await Convert(obj as byte[], path);
    }
}
