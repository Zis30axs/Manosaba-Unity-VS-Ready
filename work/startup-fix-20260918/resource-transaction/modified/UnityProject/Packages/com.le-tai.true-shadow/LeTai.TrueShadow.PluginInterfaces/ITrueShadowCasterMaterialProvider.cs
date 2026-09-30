using System;
using UnityEngine;

namespace LeTai.TrueShadow.PluginInterfaces
{

public interface ITrueShadowCasterMaterialProvider
{
	event Action materialReplaced;

	event Action materialModified;

	Material GetTrueShadowCasterMaterial();
}
}
