using System.Collections.Generic;
using Naninovel.Commands;
using System.Linq;
using UnityEngine;

namespace Naninovel.FX
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Rain : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IParameterized, DestroySpawned.IAwaitable
    {
        protected float Intensity { get; private set; }
        protected float FadeInTime { get; private set; }
        protected float FadeOutTime { get; private set; }

        [SerializeField] private float defaultIntensity = 0.5f;
        [SerializeField] private float defaultFadeInTime = 5f;
        [SerializeField] private float defaultFadeOutTime = 5f;
        [SerializeField] private float defaultVelocityX = 1f;
        [SerializeField] private float defaultVelocityY = 1f;

        private static readonly int tintColorId = Shader.PropertyToID("_TintColor");

        private readonly Tweener<FloatTween> intensityTweener = new();
        private ParticleSystem particles;
        private ParticleSystem.EmissionModule emissionModule;
        private ParticleSystem.VelocityOverLifetimeModule velocityModule;
        private Material particlesMaterial;
        private Color tintColor;

        public virtual void SetSpawnParameters (IReadOnlyList<string> parameters, bool asap)
        {
            Intensity = (parameters?.ElementAtOrDefault(0)?.AsInvariantFloat() ?? defaultIntensity) * 1000;
            FadeInTime = asap ? 0 : Mathf.Abs(parameters?.ElementAtOrDefault(1)?.AsInvariantFloat() ?? defaultFadeInTime);
            velocityModule.xMultiplier *= parameters?.ElementAtOrDefault(2)?.AsInvariantFloat() ?? defaultVelocityX;
            velocityModule.yMultiplier *= parameters?.ElementAtOrDefault(3)?.AsInvariantFloat() ?? defaultVelocityY;
        }

        public async UniTask AwaitSpawn (AsyncToken token = default)
        {
            if (intensityTweener.Running)
                intensityTweener.CompleteInstantly();

            var time = token.Completed ? 0 : FadeInTime;
            var tween = new FloatTween(emissionModule.rateOverTimeMultiplier, Intensity, new(time), SetIntensity);
            await intensityTweener.RunAwaitable(tween, token, particles);
        }

        public void SetDestroyParameters (IReadOnlyList<string> parameters)
        {
            FadeOutTime = Mathf.Abs(parameters?.ElementAtOrDefault(0)?.AsInvariantFloat() ?? defaultFadeOutTime);
        }

        public async UniTask AwaitDestroy (AsyncToken token = default)
        {
            if (intensityTweener.Running)
                intensityTweener.CompleteInstantly();

            var time = token.Completed ? 0 : FadeOutTime;
            var tween = new FloatTween(tintColor.a, 0, new(time), SetTintOpacity);
            await intensityTweener.RunAwaitable(tween, token, particles);
        }

        private void Awake ()
        {
            particles = GetComponent<ParticleSystem>();
            emissionModule = particles.emission;
            velocityModule = particles.velocityOverLifetime;
            particlesMaterial = GetComponent<ParticleSystemRenderer>().material;
            tintColor = particlesMaterial.GetColor(tintColorId);

            // Position before the first background.
            transform.position = new(0, 0, Engine.GetConfiguration<BackgroundsConfiguration>().ZOffset - 1);

            SetIntensity(0);
        }

        private void SetIntensity (float value)
        {
            emissionModule.rateOverTimeMultiplier = value;
        }

        private void SetTintOpacity (float value)
        {
            var color = tintColor;
            color.a = value;
            particlesMaterial.SetColor(tintColorId, color);
        }
    }
}
