#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore;

namespace Manosaba.EditorTools
{
    /// <summary>Provides temporary multicolour author initials without saving a font or sprite asset.</summary>
    public static class MulticolorAuthorNames
    {
        public const string DefaultGlyphKey = "tricolor-b";
        private const int FontSize = 128;
        private static readonly Dictionary<string, GlyphDefinition> glyphs = new Dictionary<string, GlyphDefinition>(StringComparer.Ordinal);
        private static readonly Dictionary<TMP_Text, TMP_SpriteAsset> originalSpriteAssets = new Dictionary<TMP_Text, TMP_SpriteAsset>();
        private static char[] registeredTokens = Array.Empty<char>();
        private static bool installed;

        private sealed class GlyphDefinition
        {
            public string Key;
            public char Character;
            public char Token;
            public Color[] Bands;
            public TMP_SpriteAsset Asset;
            public Texture2D Texture;
            public Material Material;
        }

        private static TMP_SpriteAsset PrimaryAsset => glyphs[DefaultGlyphKey].Asset;

        static MulticolorAuthorNames()
        {
            AssemblyReloadEvents.beforeAssemblyReload += DestroyOwnedObjects;
            EditorApplication.quitting += DestroyOwnedObjects;
            EditorApplication.playModeStateChanged += state =>
            {
                if (state == PlayModeStateChange.EnteredEditMode) Cleanup();
            };
            RegisterTricolorB();
        }

        /// <summary>Registers a BMP character with horizontal colour bands ordered from top to bottom.</summary>
        public static char RegisterGlyph(string key, char character, Color[] horizontalBands)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("A glyph key is required.", nameof(key));
            if (char.IsSurrogate(character) || char.IsControl(character) || char.IsWhiteSpace(character)) throw new ArgumentException("Use one visible BMP character.", nameof(character));
            if (horizontalBands == null || horizontalBands.Length == 0) throw new ArgumentException("Provide at least one colour band.", nameof(horizontalBands));
            if (!glyphs.TryGetValue(key, out var definition))
            {
                if (glyphs.Count >= 0x1900) throw new InvalidOperationException("The private-use character range is full.");
                definition = new GlyphDefinition { Key = key, Token = (char)(0xE000 + glyphs.Count) };
                glyphs.Add(key, definition);
                registeredTokens = new char[glyphs.Count];
                var index = 0;
                foreach (var item in glyphs.Values) registeredTokens[index++] = item.Token;
            }
            definition.Character = character;
            definition.Bands = (Color[])horizontalBands.Clone();
            if (installed)
            {
                CreateSpriteAsset(definition);
                RefreshFallbacks();
                foreach (var label in originalSpriteAssets.Keys)
                    if (label)
                    {
                        label.spriteAsset = PrimaryAsset;
                        label.havePropertiesChanged = true;
                        label.SetAllDirty();
                    }
            }
            return definition.Token;
        }

        public static char RegisterTricolorB()
        {
            return RegisterGlyph(DefaultGlyphKey, 'B', new[]
            {
                Color.white, new Color(.08f, .28f, .82f), new Color(.84f, .08f, .12f)
            });
        }

        /// <summary>Returns plain text so AuthorTextBuilder can size the initial without splitting a tag.</summary>
        public static string BuildName(string key, string tail)
        {
            if (!glyphs.TryGetValue(key, out var definition)) throw new ArgumentException("Register the glyph before building its name.", nameof(key));
            return definition.Token + (tail ?? string.Empty);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Install()
        {
            if (!Application.isEditor) return;
            foreach (var definition in glyphs.Values)
                if (!definition.Asset) CreateSpriteAsset(definition);
            installed = true;
            RefreshFallbacks();
            TMP_Text.OnSpriteAssetRequest -= ResolveSpriteAsset;
            TMP_Text.OnSpriteAssetRequest += ResolveSpriteAsset;
            Canvas.preWillRenderCanvases -= BindSceneLabels;
            Canvas.preWillRenderCanvases += BindSceneLabels;
            BindSceneLabels();
        }

        public static void Cleanup()
        {
            TMP_Text.OnSpriteAssetRequest -= ResolveSpriteAsset;
            Canvas.preWillRenderCanvases -= BindSceneLabels;
            foreach (var entry in originalSpriteAssets)
                if (entry.Key) entry.Key.spriteAsset = entry.Value;
            originalSpriteAssets.Clear();
            installed = false;
            // TMP caches named sprite assets without a public remove API. Keep this tiny,
            // nonpersistent asset alive until domain reload so another Play session can reuse it.
        }

        private static TMP_SpriteAsset ResolveSpriteAsset(int hash, string name)
        {
            foreach (var definition in glyphs.Values)
                if (definition.Asset && definition.Asset.name == name) return definition.Asset;
            return null;
        }

        private static void RefreshFallbacks()
        {
            var primary = PrimaryAsset;
            if (!primary) return;
            primary.fallbackSpriteAssets = new List<TMP_SpriteAsset>();
            foreach (var definition in glyphs.Values)
                if (definition.Asset && definition.Asset != primary) primary.fallbackSpriteAssets.Add(definition.Asset);
            foreach (var previous in originalSpriteAssets.Values)
                if (previous && previous != primary && !primary.fallbackSpriteAssets.Contains(previous))
                    primary.fallbackSpriteAssets.Add(previous);
        }

        private static void BindSceneLabels()
        {
            if (!Application.isPlaying || !installed || !PrimaryAsset) return;
            // Bind author labels before TMP rebuilds their meshes, including printers created
            // lazily after engine initialization. Prefab assets are never modified.
            foreach (var panel in Resources.FindObjectsOfTypeAll<Naninovel.UI.AuthorNameTMProPanel>())
                if (panel && panel.gameObject.scene.IsValid() && !EditorUtility.IsPersistent(panel))
                    BindLabel(panel.GetComponentInChildren<TMP_Text>(true));

            // Backlog author labels do not use AuthorNameTMProPanel.
            foreach (var label in Resources.FindObjectsOfTypeAll<TMP_Text>())
                if (label && label.gameObject.scene.IsValid() && !EditorUtility.IsPersistent(label)
                    && label.text != null && label.text.IndexOfAny(registeredTokens) >= 0)
                    BindLabel(label);
        }

        private static void BindLabel(TMP_Text label)
        {
            if (!label || label.spriteAsset == PrimaryAsset) return;
            if (!originalSpriteAssets.ContainsKey(label))
            {
                originalSpriteAssets.Add(label, label.spriteAsset);
                RefreshFallbacks();
            }
            label.spriteAsset = PrimaryAsset;
        }

        private static void CreateSpriteAsset(GlyphDefinition definition)
        {
            try
            {
                var oldTexture = definition.Texture;
                definition.Texture = RenderMulticolorGlyph(definition.Character, definition.Bands);
                if (oldTexture) UnityEngine.Object.DestroyImmediate(oldTexture);
                var assetName = "EditorMulticolorName_" + definition.Key;
                if (!definition.Asset)
                {
                    definition.Asset = ScriptableObject.CreateInstance<TMP_SpriteAsset>();
                    definition.Asset.name = assetName;
                    definition.Asset.hideFlags = HideFlags.HideAndDontSave;
                }
                var spriteAsset = definition.Asset;
                spriteAsset.spriteSheet = definition.Texture;
                spriteAsset.faceInfo = new FaceInfo
                {
                    pointSize = FontSize, scale = 1, ascentLine = FontSize,
                    descentLine = 0, lineHeight = FontSize
                };

                var shader = Shader.Find("TextMeshPro/Sprite");
                if (!shader) throw new InvalidOperationException("TextMeshPro/Sprite shader is unavailable.");
                if (!definition.Material) definition.Material = new Material(shader)
                {
                    name = assetName + " Material", hideFlags = HideFlags.HideAndDontSave
                };
                definition.Material.mainTexture = definition.Texture;
                spriteAsset.material = definition.Material;
                // Initialise the current TMP table format through its public upgrade API.
                if (string.IsNullOrEmpty(spriteAsset.version))
                {
                    spriteAsset.spriteInfoList = new List<TMP_Sprite>();
                    spriteAsset.UpdateLookupTables();
                }
                spriteAsset.spriteGlyphTable.Clear();
                spriteAsset.spriteCharacterTable.Clear();

                var width = definition.Texture.width;
                var height = definition.Texture.height;
                var glyph = new TMP_SpriteGlyph(0,
                    new GlyphMetrics(width, height, 0, height, width + 2),
                    new GlyphRect(0, 0, width, height), 1, 0);
                spriteAsset.spriteGlyphTable.Add(glyph);
                spriteAsset.spriteCharacterTable.Add(new TMP_SpriteCharacter(definition.Token, spriteAsset, glyph)
                {
                    name = definition.Key, scale = 1
                });
                spriteAsset.UpdateLookupTables();
            }
            catch
            {
                DestroyOwnedObjects();
                throw;
            }
        }

        private static Texture2D RenderMulticolorGlyph(char symbol, Color[] bands)
        {
            var font = Font.CreateDynamicFontFromOSFont(new[] { "Arial", "Liberation Sans", "DejaVu Sans" }, FontSize);
            Texture2D atlasCopy = null;
            RenderTexture temporary = null;
            var previous = RenderTexture.active;
            try
            {
                font.hideFlags = HideFlags.HideAndDontSave;
                font.RequestCharactersInTexture(symbol.ToString(), FontSize, FontStyle.Bold);
                if (!font.GetCharacterInfo(symbol, out var character, FontSize, FontStyle.Bold))
                    throw new InvalidOperationException("Could not render author glyph '" + symbol + "'.");

                var atlas = font.material.mainTexture;
                temporary = RenderTexture.GetTemporary(atlas.width, atlas.height, 0, RenderTextureFormat.ARGB32);
                Graphics.Blit(atlas, temporary);
                RenderTexture.active = temporary;
                atlasCopy = new Texture2D(atlas.width, atlas.height, TextureFormat.RGBA32, false);
                atlasCopy.ReadPixels(new Rect(0, 0, atlas.width, atlas.height), 0, 0);
                atlasCopy.Apply();

                var width = Math.Max(1, character.maxX - character.minX);
                var height = Math.Max(1, character.maxY - character.minY);
                var pixels = new Color[width * height];
                for (var y = 0; y < height; y++)
                {
                    var row = (y + .5f) / height;
                    // Texture coordinates start at the bottom; the caller's bands start at the top.
                    var colour = bands[Math.Min(bands.Length - 1, (int)((1 - row) * bands.Length))];
                    var left = Vector2.Lerp(character.uvBottomLeft, character.uvTopLeft, row);
                    var right = Vector2.Lerp(character.uvBottomRight, character.uvTopRight, row);
                    for (var x = 0; x < width; x++)
                    {
                        var uv = Vector2.Lerp(left, right, (x + .5f) / width);
                        var pixel = colour;
                        pixel.a *= atlasCopy.GetPixelBilinear(uv.x, uv.y).a;
                        pixels[y * width + x] = pixel;
                    }
                }

                var result = new Texture2D(width, height, TextureFormat.RGBA32, false)
                {
                    name = "MulticolorAuthorGlyph_" + symbol, hideFlags = HideFlags.HideAndDontSave,
                    filterMode = FilterMode.Bilinear, wrapMode = TextureWrapMode.Clamp
                };
                result.SetPixels(pixels);
                result.Apply(false, true);
                return result;
            }
            finally
            {
                RenderTexture.active = previous;
                if (temporary) RenderTexture.ReleaseTemporary(temporary);
                if (atlasCopy) UnityEngine.Object.DestroyImmediate(atlasCopy);
                if (font) UnityEngine.Object.DestroyImmediate(font);
            }
        }

        private static void DestroyOwnedObjects()
        {
            Cleanup();
            foreach (var definition in glyphs.Values)
            {
                if (definition.Asset) UnityEngine.Object.DestroyImmediate(definition.Asset);
                if (definition.Material) UnityEngine.Object.DestroyImmediate(definition.Material);
                if (definition.Texture) UnityEngine.Object.DestroyImmediate(definition.Texture);
                definition.Asset = null;
                definition.Material = null;
                definition.Texture = null;
            }
        }
    }
}
#endif
