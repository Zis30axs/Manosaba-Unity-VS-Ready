using System;
using LeTai.TrueShadow.PluginInterfaces;
using UnityEngine;
using UnityEngine.UI;

namespace LeTai.TrueShadow
{

[AddComponentMenu("UI/True Shadow/True Shadow Disable Cache")]
[ExecuteAlways]
[RequireComponent(typeof(TrueShadow))]
public class TrueShadowDisableCache : MonoBehaviour, ITrueShadowCustomHashProviderV2
{
	[Tooltip("Force shadow to refresh every frame. Useful for animated effects")]
	public bool everyFrame;

	[Tooltip("Some 3rd party effects doesn't mark the Graphic as dirty when modifying them. Try this if the above doesn't work")]
	public bool markGraphicDirty;

	private Graphic _graphic;

	public event Action<int> trueShadowCustomHashChanged;

	private void OnEnable()
	{
		_graphic = GetComponent<Graphic>();
		Canvas.preWillRenderCanvases += PreWillRenderCanvases;
	}

	private void PreWillRenderCanvases()
	{
		Canvas.preWillRenderCanvases -= PreWillRenderCanvases;
		Dirty();
	}

	private void Update()
	{
		if (everyFrame)
		{
			Dirty();
		}
	}

	private void Dirty()
	{
		if (markGraphicDirty)
		{
			_graphic.SetAllDirty();
		}
		this.trueShadowCustomHashChanged?.Invoke(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
	}

	private void OnDisable()
	{
		this.trueShadowCustomHashChanged?.Invoke(0);
	}
}
}
