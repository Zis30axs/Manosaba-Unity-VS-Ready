using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="IActor"/> implementation using <typeparamref name="TBehaviour"/> to represent the actor.
    /// </summary>
    /// <remarks>
    /// Resource prefab should have a <typeparamref name="TBehaviour"/> component attached to the root object.
    /// Appearance and other property changes changes are routed to the events of the <typeparamref name="TBehaviour"/> component.
    /// </remarks>
    public abstract class GenericActor<TBehaviour, TMeta> : MonoBehaviourActor<TMeta>
        where TBehaviour : GenericActorBehaviour
        where TMeta : ActorMetadata
    {
        /// <summary>
        /// Behaviour component of the instantiated generic prefab associated with the actor.
        /// </summary>
        public virtual TBehaviour Behaviour { get; private set; }
        public override string Appearance { get => appearance; set => SetAppearance(value); }
        public override bool Visible { get => visible; set => SetVisibility(value); }

        private readonly EmbeddedAppearanceLoader<GameObject> prefabLoader;
        private string appearance;
        private bool visible;
        private Color tintColor = Color.white;

        protected GenericActor (string id, TMeta meta, EmbeddedAppearanceLoader<GameObject> loader)
            : base(id, meta)
        {
            prefabLoader = loader;
        }

        public override async UniTask Initialize ()
        {
            await base.Initialize();

            var prefabResource = await prefabLoader.LoadOrErr(Id, this);
            Behaviour = (await Engine.Instantiate(prefabResource.Object,
                prefabResource.Object.name, parent: Transform)).GetComponent<TBehaviour>();

            SetVisibility(false);
        }

        public override UniTask ChangeAppearance (string appearance, Tween tween, Transition? transition = default, AsyncToken token = default)
        {
            SetAppearance(appearance);
            return UniTask.CompletedTask;
        }

        public override UniTask ChangeVisibility (bool visible, Tween tween, AsyncToken token = default)
        {
            SetVisibility(visible);
            return UniTask.CompletedTask;
        }

        protected virtual void SetAppearance (string appearance)
        {
            this.appearance = appearance;
            if (string.IsNullOrEmpty(appearance)) return;

            if (appearance.IndexOf(',') >= 0)
                foreach (var part in appearance.Split(','))
                    Behaviour.InvokeAppearanceChangedEvent(part);
            else Behaviour.InvokeAppearanceChangedEvent(appearance);
        }

        protected virtual void SetVisibility (bool visible)
        {
            this.visible = visible;

            Behaviour.InvokeVisibilityChangedEvent(visible);
        }

        protected override Color GetBehaviourTintColor () => tintColor;

        protected override void SetBehaviourTintColor (Color tintColor)
        {
            this.tintColor = tintColor;

            Behaviour.InvokeTintColorChangedEvent(tintColor);
        }

        public override void Dispose ()
        {
            prefabLoader?.ReleaseAll(this);

            base.Dispose();
        }
    }
}
