using System;
using UnityEngine;

namespace LeTai.TrueShadow.PluginInterfaces
{

public interface ITrueShadowRendererMaterialProvider
{
	event Action materialReplaced;

	event Action materialModified;

	Material GetTrueShadowRendererMaterial();
}
}
