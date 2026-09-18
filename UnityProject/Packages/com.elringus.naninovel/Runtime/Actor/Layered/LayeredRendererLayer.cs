using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Layer inside <see cref="LayeredActorBehaviour"/> object.
    /// </summary>
    public interface ILayeredRendererLayer : IDisposable
    {
        string Name { get; }
        string Group { get; }
        bool Enabled { get; set; }
        LayeredActorLayer Layer { get; }
    }

    public class LayeredRendererLayer : ILayeredRendererLayer
    {
        public string Name { get; }
        public string Group { get; }
        public LayeredActorLayer Layer => throw new Error("Layered Actor Layer components are used only with camera mode.");
        public bool Enabled { get => Renderer.enabled; set => Renderer.enabled = value; }
        public readonly Mesh Mesh;
        public readonly Renderer Renderer;
        public readonly Transform Transform;
        public Vector2 Position => Transform.position;
        public Quaternion Rotation => Transform.localRotation;
        public Vector2 Scale => Transform.lossyScale;

        private static readonly int spriteColorId = Shader.PropertyToID("_RendererColor");

        private readonly SpriteRenderer spriteRenderer;

        public LayeredRendererLayer (Renderer renderer, Mesh mesh)
        {
            Name = renderer.gameObject.name;
            Transform = renderer.transform;
            Group = LayeredUtilities.BuildGroupName(renderer.transform);
            Mesh = mesh;
            Renderer = renderer;

            if (Application.isPlaying)
                renderer.forceRenderingOff = true;
        }

        public LayeredRendererLayer (SpriteRenderer spriteRenderer) :
            this(spriteRenderer, BuildSpriteMesh(spriteRenderer))
        {
            this.spriteRenderer = spriteRenderer;
        }

        public void Dispose ()
        {
            if (Mesh && Mesh.hideFlags.HasFlag(HideFlags.HideAndDontSave) && Mesh.name.EqualsFast("Generated Mesh"))
                ObjectUtils.DestroyOrImmediate(Mesh);
        }

        public MaterialPropertyBlock GetPropertyBlock (MaterialPropertyBlock block)
        {
            Renderer.GetPropertyBlock(block);
            if (spriteRenderer)
                block.SetColor(spriteColorId, spriteRenderer.color);
            return block;
        }

        private static Mesh BuildSpriteMesh (SpriteRenderer spriteRenderer)
        {
            var sprite = spriteRenderer.sprite;
            var mesh = new Mesh();
            mesh.hideFlags = HideFlags.HideAndDontSave;
            mesh.name = $"{sprite.name} Generated Mesh";
            mesh.vertices = Array.ConvertAll(sprite.vertices, i => new Vector3(i.x * (spriteRenderer.flipX ? -1 : 1), i.y * (spriteRenderer.flipY ? -1 : 1)));
            mesh.uv = sprite.uv;
            mesh.triangles = Array.ConvertAll(sprite.triangles, i => (int)i);
            return mesh;
        }
    }

    public class LayeredCameraLayer : ILayeredRendererLayer
    {
        public string Name { get; }
        public string Group { get; }
        public bool Enabled { get => go.activeSelf; set => go.SetActive(value); }
        public LayeredActorLayer Layer => layer;

        private readonly GameObject go;
        private readonly LayeredActorLayer layer;

        public LayeredCameraLayer (GameObject go)
        {
            this.go = go;
            go.TryGetComponent<LayeredActorLayer>(out layer);
            Name = go.name;
            Group = LayeredUtilities.BuildGroupName(go.transform);
        }

        public void Dispose () { }
    }
}
