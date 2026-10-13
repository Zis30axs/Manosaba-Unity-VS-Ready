using UnityEngine;

namespace WitchTrials.Views
{
    // Compatibility implementation for the Switch-only serialized component.
    // The original method bodies are unavailable. This supplies normalized
    // sprite-local coordinates to our paired built-in-pipeline shaders.
    [DisallowMultipleComponent]
    public sealed class SpriteLocalCoords : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private bool _usePropertyBlock;

        private static readonly int LocalRect = Shader.PropertyToID("_SpriteLocalRect");
        private MaterialPropertyBlock block;
        private Material originalMaterial;
        private Material ownedMaterial;

        private void OnEnable() => RefreshCoordinates();
        private void LateUpdate() => RefreshCoordinates();

        public void RefreshCoordinates()
        {
            if (!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>();
            if (!_spriteRenderer || !_spriteRenderer.sprite || !_spriteRenderer.sharedMaterial
                || !_spriteRenderer.sharedMaterial.HasProperty(LocalRect)) return;
            Bounds bounds = _spriteRenderer.sprite.bounds;
            if (bounds.size.x <= 0 || bounds.size.y <= 0) return;
            var rect = new Vector4(bounds.min.x, bounds.min.y, 1f / bounds.size.x, 1f / bounds.size.y);
            if (_usePropertyBlock)
            {
                if (block == null) block = new MaterialPropertyBlock();
                _spriteRenderer.GetPropertyBlock(block);
                block.SetVector(LocalRect, rect);
                _spriteRenderer.SetPropertyBlock(block);
            }
            else
            {
                // Never write runtime values into the shared imported material.
                if (!ownedMaterial)
                {
                    originalMaterial = _spriteRenderer.sharedMaterial;
                    ownedMaterial = new Material(originalMaterial) { hideFlags = HideFlags.DontSave };
                    _spriteRenderer.sharedMaterial = ownedMaterial;
                }
                ownedMaterial.SetVector(LocalRect, rect);
            }
        }

        private void OnDisable() => ReleaseCoordinates();

        public void ReleaseCoordinates()
        {
            if (!ownedMaterial) return;
            if (_spriteRenderer && _spriteRenderer.sharedMaterial == ownedMaterial)
                _spriteRenderer.sharedMaterial = originalMaterial;
            if (Application.isPlaying) Destroy(ownedMaterial);
            else DestroyImmediate(ownedMaterial);
            ownedMaterial = null;
            originalMaterial = null;
        }
    }
}
