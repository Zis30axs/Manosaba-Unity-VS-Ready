using System;
using System.Threading;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="IActor"/> implementation using <see cref="MonoBehaviour"/> to represent the actor.
    /// </summary>
    public abstract class MonoBehaviourActor<TMeta> : IActor, IDisposable
        where TMeta : ActorMetadata
    {
        public virtual string Id { get; }
        public virtual TMeta ActorMeta { get; }
        public abstract string Appearance { get; set; }
        public abstract bool Visible { get; set; }
        public virtual Vector3 Position
        {
            get => position;
            set
            {
                CompletePositionTween();
                position = value;
                SetBehaviourPosition(value);
            }
        }
        public virtual Quaternion Rotation
        {
            get => rotation;
            set
            {
                CompleteRotationTween();
                rotation = value;
                SetBehaviourRotation(value);
            }
        }
        public virtual Vector3 Scale
        {
            get => scale;
            set
            {
                CompleteScaleTween();
                scale = value;
                SetBehaviourScale(value);
            }
        }
        public virtual Color TintColor
        {
            get => tintColor;
            set
            {
                CompleteTintColorTween();
                tintColor = value;
                SetBehaviourTintColor(value);
            }
        }
        public virtual GameObject GameObject { get; private set; }
        public virtual Transform Transform => GameObject.transform;

        private readonly Tweener<VectorTween> positionTweener = new();
        private readonly Tweener<VectorTween> rotationTweener = new();
        private readonly Tweener<VectorTween> scaleTweener = new();
        private readonly Tweener<ColorTween> tintColorTweener = new();
        private Vector3 position = Vector3.zero;
        private Vector3 scale = Vector3.one;
        private Quaternion rotation = Quaternion.identity;
        private Color tintColor = Color.white;

        protected MonoBehaviourActor (string id, TMeta meta)
        {
            Id = id;
            ActorMeta = meta;
        }

        public virtual UniTask Initialize ()
        {
            GameObject = CreateHostObject();
            return UniTask.CompletedTask;
        }

        public abstract UniTask ChangeAppearance (string appearance, Tween tween,
            Transition? transition = default, AsyncToken token = default);

        public abstract UniTask ChangeVisibility (bool isVisible, Tween tween, AsyncToken token = default);

        public virtual async UniTask ChangePosition (Vector3 position, Tween tween, AsyncToken token = default)
        {
            CompletePositionTween();
            this.position = position;

            var tweenValue = new VectorTween(GetBehaviourPosition(), position, tween, SetBehaviourPosition);
            await positionTweener.RunAwaitable(tweenValue, token, GameObject);
        }

        public virtual async UniTask ChangeRotation (Quaternion rotation, Tween tween, AsyncToken token = default)
        {
            CompleteRotationTween();
            this.rotation = rotation;

            var tweenValue = new VectorTween(GetBehaviourRotation().ClampedEulerAngles(), rotation.ClampedEulerAngles(), tween, SetBehaviourRotation);
            await rotationTweener.RunAwaitable(tweenValue, token, GameObject);
        }

        public virtual async UniTask ChangeScale (Vector3 scale, Tween tween, AsyncToken token = default)
        {
            CompleteScaleTween();
            this.scale = scale;

            var tweenValue = new VectorTween(GetBehaviourScale(), scale, tween, SetBehaviourScale);
            await scaleTweener.RunAwaitable(tweenValue, token, GameObject);
        }

        public virtual async UniTask ChangeTintColor (Color tintColor, Tween tween, AsyncToken token = default)
        {
            CompleteTintColorTween();
            this.tintColor = tintColor;

            var tweenValue = new ColorTween(GetBehaviourTintColor(), tintColor, tween, ColorTweenMode.All, SetBehaviourTintColor);
            await tintColorTweener.RunAwaitable(tweenValue, token, GameObject);
        }

        public virtual void Dispose () => ObjectUtils.DestroyOrImmediate(GameObject);

        public virtual CancellationToken GetDestroyCancellationToken ()
        {
            if (GameObject.TryGetComponent<CancelOnDestroy>(out var component))
                return component.Token;
            return GameObject.AddComponent<CancelOnDestroy>().Token;
        }

        protected virtual Vector3 GetBehaviourPosition () => Transform.position;
        protected virtual void SetBehaviourPosition (Vector3 position) => Transform.position = position;
        protected virtual Quaternion GetBehaviourRotation () => Transform.rotation;
        protected virtual void SetBehaviourRotation (Quaternion rotation) => Transform.rotation = rotation;
        protected virtual void SetBehaviourRotation (Vector3 rotation) => SetBehaviourRotation(Quaternion.Euler(rotation));
        protected virtual Vector3 GetBehaviourScale () => Transform.localScale;
        protected virtual void SetBehaviourScale (Vector3 scale) => Transform.localScale = scale;
        protected abstract Color GetBehaviourTintColor ();
        protected abstract void SetBehaviourTintColor (Color tintColor);

        protected virtual GameObject CreateHostObject ()
        {
            return Engine.CreateObject(Id, parent: GetOrCreateParent());
        }

        protected virtual string BuildActorCategory ()
        {
            return typeof(TMeta).Name.GetBefore("Metadata");
        }

        protected virtual Transform GetOrCreateParent ()
        {
            var name = BuildActorCategory();
            if (string.IsNullOrEmpty(name))
                throw new Error($"Failed to evaluate parent name for {Id} actor.");
            var obj = Engine.FindObject(name);
            return obj ? obj.transform : Engine.CreateObject(name).transform;
        }

        private void CompletePositionTween ()
        {
            if (positionTweener.Running)
                positionTweener.CompleteInstantly();
        }

        private void CompleteRotationTween ()
        {
            if (rotationTweener.Running)
                rotationTweener.CompleteInstantly();
        }

        private void CompleteScaleTween ()
        {
            if (scaleTweener.Running)
                scaleTweener.CompleteInstantly();
        }

        private void CompleteTintColorTween ()
        {
            if (tintColorTweener.Running)
                tintColorTweener.CompleteInstantly();
        }
    }
}
