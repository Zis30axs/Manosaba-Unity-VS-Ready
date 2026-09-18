using System;
using LeTai.TrueShadow.PluginInterfaces;
using UnityEngine;

namespace LeTai.TrueShadow
{

[AddComponentMenu("UI/True Shadow/True Shadow Custom Material")]
[ExecuteAlways]
public class TrueShadowCustomMaterial : MonoBehaviour, ITrueShadowRendererMaterialProvider
{
	public Material material;

	public event Action materialReplaced;

	public event Action materialModified;

	public Material GetTrueShadowRendererMaterial()
	{
		if (!base.isActiveAndEnabled)
		{
			return null;
		}
		return material;
	}

	private void OnEnable()
	{
		TrueShadow component = GetComponent<TrueShadow>();
		if ((bool)component)
		{
			component.RefreshPlugins();
		}
		this.materialReplaced?.Invoke();
	}

	private void OnDisable()
	{
		TrueShadow component = GetComponent<TrueShadow>();
		if ((bool)component)
		{
			component.RefreshPlugins();
		}
		this.materialReplaced?.Invoke();
	}

	private void OnValidate()
	{
		this.materialReplaced?.Invoke();
	}

	public void OnMaterialModified()
	{
		this.materialModified?.Invoke();
	}
}
}
