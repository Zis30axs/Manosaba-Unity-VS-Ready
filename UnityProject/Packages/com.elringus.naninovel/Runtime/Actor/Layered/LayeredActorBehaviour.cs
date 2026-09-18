using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// When applied to a <see cref="GameObject"/>, containing child objects with <see cref="Renderer"/> components (layers), 
    /// handles the composition (layers enabled state) and rendering to a texture in back to front order based on z-position and sort order.
    /// Will prevent the child renderers from being rendered by the cameras at play mode.
    /// </summary>
    [ExecuteAlways]
    public abstract class LayeredActorBehaviour : MonoBehaviour
    {
        [Serializable]
        public class CompositionMapItem
        {
            public string Key;
            [TextArea(1, 5)]
            public string Composition;
        }

        /// <summary>
        /// Current layer composition of the actor.
        /// </summary>
        public virtual string Composition => string.Join(splitLiteral, Drawer.Layers.Select(l => $"{l.Group}{(l.Enabled ? enableLiteral : disableLiteral)}{l.Name}"));
        public virtual bool Animated => animated;
        public virtual string DefaultAppearance => defaultAppearance;
        public virtual bool RenderOnly => renderOnly;

        protected virtual bool Reversed => reversed;
        protected virtual Material SharedRenderMaterial => renderMaterial;
        protected virtual Camera RenderCamera => renderCamera;
        protected virtual int CameraMask => cameraMask;
        protected virtual LayeredDrawer Drawer { get; private set; }

        private const string groupLiteral = "/";
        private const string selectLiteral = ">";
        private const string enableLiteral = "+";
        private const string disableLiteral = "-";
        private const string splitLiteral = ",";
        private static readonly string[] splitLiterals = { splitLiteral };

        [Tooltip("Whether the actor should be rendered every frame. Enable when animating the layers or implementing other dynamic behaviour.")]
        [SerializeField] private bool animated;
        [Tooltip("Whether to render the layers in a reversed order.")]
        [SerializeField] private bool reversed;
        [Tooltip("Shared material to use when rendering the layers. Will use layer renderer's material when not assigned.")]
        [SerializeField] private Material renderMaterial;
        [Tooltip("When assigned, will render the prefab content with the camera instead of procedural renderer. Less optimized, but supports more features, such as particle systems.")]
        [SerializeField] private Camera renderCamera;
        [Tooltip("Additional layers to include when rendering the actor in camera mode."), LayerMask]
        [SerializeField] private int cameraMask;
        [Tooltip("Allows to map layer composition expressions to keys; the keys can then be used to specify layered actor appearances instead of the full expressions.")]
        [SerializeField] private List<CompositionMapItem> compositionMap = new();
        [Tooltip("Appearance to use by default. Will use layered expression of the initial prefab state when not specified.")]
        [SerializeField] private string defaultAppearance;
        [Tooltip("Whether to disable layer-related behaviour and just render the prefab content. Enable when controlling appearance via external means (eg, with Animator).")]
        [SerializeField] private bool renderOnly;
        [Tooltip("Invoked when appearance of the actor is changed.")]
        [SerializeField] private StringUnityEvent onAppearanceChanged;

        /// <summary>
        /// Returns all the composition expressions mapped to keys via <see cref="compositionMap"/> serialized field.
        /// Records with duplicate keys are ignored.
        /// </summary>
        public virtual IReadOnlyDictionary<string, string> GetCompositionMap ()
        {
            var map = new Dictionary<string, string>();
            foreach (var item in compositionMap)
                map[item.Key] = item.Composition;
            return map;
        }

        /// <summary>
        /// Fires the <see cref="onAppearanceChanged"/> event.
        /// </summary>
        public virtual void NotifyAppearanceChanged (string appearance)
        {
            onAppearanceChanged?.Invoke(appearance);
        }

        /// <summary>
        /// Notifies when the actor becomes visible or completely invisible on the screen.
        /// </summary>
        public virtual void NotifyPerceivedVisibilityChanged (bool visible)
        {
            if (!visible) Drawer.ReleaseCameraLayer();
        }

        /// <summary>
        /// Applies specified layer composition expression to the actor.
        /// </summary>
        /// <remarks>
        /// Value format: path/to/object/parent>SelectedObjName,another/path+EnabledObjName,another/path-DisabledObjName,...
        /// Select operator (>) means that only this object is enabled inside the group, others should be disabled.
        /// When no target objects specified, all the layers inside the group will be affected (recursively, including child groups).
        /// </remarks>
        public virtual void ApplyComposition (string value)
        {
            if (Drawer.Layers is null || Drawer.Layers.Count == 0) return;

            SplitAndApplyExpressions(value);

            void SplitAndApplyExpressions (string composition)
            {
                var items = composition.Trim().Split(splitLiterals, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in items)
                    if (compositionMap.Any(e => e.Key == item))
                        SplitAndApplyExpressions(compositionMap.First(e => e.Key == item).Composition);
                    else ApplyExpression(item);
            }

            void ApplyExpression (string expression)
            {
                if (ParseExpression(expression, selectLiteral, out var group, out var name)) // Select expression (>).
                {
                    if (string.IsNullOrEmpty(name)) // Enable all in the group, disable all in neighbour groups (recursive).
                    {
                        if (!Drawer.Layers.Any(l => l.Group.StartsWithFast(group)))
                            Engine.Warn($"'{gameObject.name}' layered actor group '{group}' not found.");
                        var parentGroup = group.Contains(groupLiteral) ? group.GetBeforeLast(groupLiteral) : string.Empty;
                        ForEachLayer(l => l.Group.StartsWithFast(parentGroup), l => l.Enabled = l.Group.StartsWithFast(group), group);
                    }
                    else
                    {
                        if (!Drawer.Layers.Any(l => l.Group.EqualsFast(group) && l.Name.EqualsFast(name)))
                            Engine.Warn($"'{gameObject.name}' layered actor layer '{name}' inside composition group '{group}' not found.");
                        ForEachLayer(l => l.Group.EqualsFast(group), l => l.Enabled = l.Name.EqualsFast(name), group);
                    }
                }
                else if (ParseExpression(expression, enableLiteral, out group, out name)) // Enable expression (+).
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        var prefix = $"{group}/";
                        ForEachLayer(l => l.Group.EqualsFast(group) || l.Group.StartsWithFast(prefix) || string.IsNullOrEmpty(group), l => l.Enabled = true, group);
                    }
                    else ForLayer(group, name, l => l.Enabled = true);
                }
                else if (ParseExpression(expression, disableLiteral, out group, out name)) // Disable expression (-).
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        var prefix = $"{group}/";
                        ForEachLayer(l => l.Group.EqualsFast(group) || l.Group.StartsWithFast(prefix) || string.IsNullOrEmpty(group), l => l.Enabled = false, group);
                    }
                    else ForLayer(group, name, l => l.Enabled = false);
                }
                else Engine.Warn($"Unrecognized '{gameObject.name}' layered actor composition expression: '{expression}'.");
            }

            bool ParseExpression (string expression, string operationLiteral, out string group, out string name)
            {
                group = expression.GetBefore(operationLiteral);
                if (group is null)
                {
                    name = null;
                    return false;
                }
                name = expression[(group.Length + operationLiteral.Length)..];
                return true;
            }

            void ForEachLayer (Func<ILayeredRendererLayer, bool> selector, Action<ILayeredRendererLayer> action, string group)
            {
                var layers = Drawer.Layers.Where(selector).ToArray();
                if (!layers.Any()) Engine.Warn($"'{gameObject.name}' layered actor composition group '{group}' not found.");
                else
                    foreach (var layer in layers)
                        action.Invoke(layer);
            }

            void ForLayer (string group, string name, Action<ILayeredRendererLayer> action)
            {
                var layer = Drawer.Layers.FirstOrDefault(l => l.Group.EqualsFast(group) && l.Name.EqualsFast(name));
                if (layer is null) Engine.Warn($"'{gameObject.name}' layered actor layer '{name}' inside composition group '{group}' not found.");
                else action.Invoke(layer);
            }
        }

        /// <summary>
        /// Rebuilds the layers and associated rendering parameters.
        /// </summary>
        [ContextMenu("Rebuild Layers")]
        public virtual void RebuildLayers () => Drawer?.BuildLayers();

        /// <summary>
        /// Renders the enabled layers scaled by <paramref name="pixelsPerUnit"/> to the specified or a temporary <see cref="RenderTexture"/>.
        /// Don't forget to release unused render textures.
        /// </summary>
        /// <param name="pixelsPerUnit">PPU to use when rendering.</param>
        /// <param name="renderTexture">Render texture to render the content into; when not specified, will create a temporary one.</param>
        /// <returns>Temporary render texture created when no render texture is specified.</returns>
        public virtual RenderTexture Render (float pixelsPerUnit, RenderTexture renderTexture = default)
        {
            return Drawer.DrawLayers(pixelsPerUnit, renderTexture);
        }

        protected virtual void Awake ()
        {
            Drawer = CreateDrawer();
        }

        protected virtual void OnDestroy ()
        {
            Drawer?.Dispose();
        }

        protected virtual void OnDrawGizmos ()
        {
            Drawer.DrawGizmos();
        }

        protected virtual void OnValidate ()
        {
            // Drawer is null when entering-exiting play mode while
            // a layered prefab is opened in edit mode (Awake is not invoked).
            Drawer ??= CreateDrawer();
        }

        protected virtual LayeredDrawer CreateDrawer ()
        {
            return new(transform, RenderCamera, CameraMask, SharedRenderMaterial, Reversed);
        }
    }
}
