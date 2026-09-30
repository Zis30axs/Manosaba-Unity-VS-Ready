using System;
using UnityEngine;

namespace LeTai.TrueShadow.PluginInterfaces
{

public interface ITrueShadowCasterMeshProvider
{
	event Action<Mesh> trueShadowCasterMeshChanged;
}
}
