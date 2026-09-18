using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel.Commands
{
    [Doc(
        @"
Modifies the main camera, changing offset, zoom level and rotation over time.
Check [this video](https://youtu.be/zy28jaMss8w) for a quick demonstration of the command effect.",
        null,
        @"
; Offset the camera by -3 units over X-axis and by 1.5 units Y-axis.
@camera offset:-3,1.5",
        @"
; Set camera in perspective mode, zoom-in by 50% and move back by 5 units.
@camera !ortho offset:,,-5 zoom:0.5",
        @"
; Set camera in orthographic mode and roll by 10 degrees clock-wise.
@camera ortho! roll:10",
        @"
; Offset, zoom and roll simultaneously animated over 5 seconds.
@camera offset:-3,1.5 zoom:0.5 roll:10 time:5",
        @"
; Instantly reset camera to the default state.
@camera offset:0,0 zoom:0 rotation:0,0,0 time:0",
        @"
; Toggle 'FancyCameraFilter' and 'Bloom' components attached to the camera.
@camera toggle:FancyCameraFilter,Bloom",
        @"
; Set 'FancyCameraFilter' component enabled and 'Bloom' disabled.
@camera set:FancyCameraFilter.true,Bloom.false",
        @"
; Disable all components attached to the camera object.
@camera set:*.false"
    )]
    [CommandAlias("camera")]
    public class ModifyCamera : Command
    {
        [Doc("Local camera position offset in units by X,Y,Z axes.")]
        [VectorContext("X,Y,Z")]
        public DecimalListParameter Offset;
        [Doc("Local camera rotation by Z-axis in angle degrees (0.0 to 360.0 or -180.0 to 180.0). " +
             "The same as third component of `rotation` parameter; ignored when `rotation` is specified.")]
        public DecimalParameter Roll;
        [Doc("Local camera rotation over X,Y,Z-axes in angle degrees (0.0 to 360.0 or -180.0 to 180.0).")]
        [VectorContext("X,Y,Z")]
        public DecimalListParameter Rotation;
        [Doc("Relative camera zoom (orthographic size or field of view, depending on the render mode), in 0.0 (no zoom) to 1.0 (full zoom) range.")]
        public DecimalParameter Zoom;
        [Doc("Whether the camera should render in orthographic (true) or perspective (false) mode.")]
        [ParameterAlias("ortho")]
        public BooleanParameter Orthographic;
        [Doc("Names of the components to toggle (enable if disabled and vice-versa). The components should be attached to the same game object as the camera. " +
             "This can be used to toggle [custom post-processing effects](/guide/special-effects#camera-effects). " +
             "Use `*` to affect all the components attached to the camera object.")]
        [ParameterAlias("toggle")]
        public StringListParameter ToggleTypeNames;
        [Doc("Names of the components to enable or disable. The components should be attached to the same game object as the camera. " +
             "This can be used to explicitly enable or disable [custom post-processing effects](/guide/special-effects#camera-effects). " +
             "Specified components enabled state will override effect of `toggle` parameter. " +
             "Use `*` to affect all the components attached to the camera object.")]
        [ParameterAlias("set")]
        public NamedBooleanListParameter SetTypeNames;
        [Doc(SharedDocs.EasingParameter)]
        [ParameterAlias("easing"), ConstantContext(typeof(EasingType))]
        public StringParameter EasingTypeName;
        [Doc(SharedDocs.DurationParameter)]
        [ParameterAlias("time"), ParameterDefaultValue("0.35")]
        public DecimalParameter Duration;
        [Doc(SharedDocs.LazyParameter)]
        [ParameterDefaultValue("false")]
        public BooleanParameter Lazy = false;
        [Doc(SharedDocs.WaitParameter)]
        public BooleanParameter Wait;

        private const string selectAllSymbol = "*";

        private readonly List<MonoBehaviour> componentsCache = new();

        public override UniTask Execute (AsyncToken token = default)
        {
            return WaitOrForget(Modify, Wait, token);
        }

        protected virtual async UniTask Modify (AsyncToken token)
        {
            var cameraManager = Engine.GetServiceOrErr<ICameraManager>();

            var duration = Assigned(Duration) ? Duration.Value : cameraManager.Configuration.DefaultDuration;
            var easingType = cameraManager.Configuration.DefaultEasing;
            if (Assigned(EasingTypeName) && !ParseUtils.TryConstantParameter(EasingTypeName, out easingType))
                Warn($"Failed to parse '{EasingTypeName}' easing.");
            var tween = new Tween(duration, easingType, complete: !Lazy);

            if (Assigned(Orthographic))
                cameraManager.Camera.orthographic = Orthographic;

            if (Assigned(ToggleTypeNames))
                foreach (var name in ToggleTypeNames)
                    DoForComponent(name, c => c.enabled = !c.enabled);

            if (Assigned(SetTypeNames))
                foreach (var kv in SetTypeNames)
                    if (kv.HasValue && !string.IsNullOrWhiteSpace(kv.Name) && kv.NamedValue.HasValue)
                        DoForComponent(kv.Name, c => c.enabled = kv.Value?.Value ?? false);

            using var _ = ListPool<UniTask>.Rent(out var tasks);

            if (Assigned(Offset)) tasks.Add(cameraManager.ChangeOffset(ArrayUtils.ToVector3(Offset, Vector3.zero), tween, token));

            if (Assigned(Rotation))
                tasks.Add(cameraManager.ChangeRotation(Quaternion.Euler(
                    Rotation.ElementAtOrDefault(0) ?? cameraManager.Rotation.eulerAngles.x,
                    Rotation.ElementAtOrDefault(1) ?? cameraManager.Rotation.eulerAngles.y,
                    Rotation.ElementAtOrDefault(2) ?? cameraManager.Rotation.eulerAngles.z), tween, token));
            else if (Assigned(Roll))
                tasks.Add(cameraManager.ChangeRotation(Quaternion.Euler(
                    cameraManager.Rotation.eulerAngles.x,
                    cameraManager.Rotation.eulerAngles.y,
                    Roll), tween, token));

            if (Assigned(Zoom))
            {
                var zoom = Zoom.Value;
                if (!zoom.IsWithin(0, 1)) Warn($"Camera zoom should be in 0.0 to 1.0 range, while the assigned value is {zoom}.");
                tasks.Add(cameraManager.ChangeZoom(zoom, tween, token));
            }

            await UniTask.WhenAll(tasks);

            void DoForComponent (string componentName, Action<MonoBehaviour> action)
            {
                cameraManager.Camera.gameObject.GetComponents(componentsCache);

                if (componentName == selectAllSymbol)
                {
                    componentsCache.ForEach(action);
                    return;
                }

                var cmp = componentsCache.FirstOrDefault(c => c.GetType().Name == componentName);
                if (!cmp)
                {
                    Warn($"Failed to toggle '{componentName}' camera component; the component is not found on the camera's game object.");
                    return;
                }
                action.Invoke(cmp);
            }
        }
    }
}
