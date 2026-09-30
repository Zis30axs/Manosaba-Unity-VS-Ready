using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x20003C3")]
    public class ResourceLoaderConfiguration
    {
    	[Token(Token = "0x4000E19")]
    	[FieldOffset(Offset = "0x10")]
    	[Tooltip("Path prefix to add for each requested resource.")]
    	public string PathPrefix;

    	[Token(Token = "0x4000E1A")]
    	[FieldOffset(Offset = "0x18")]
    	[Tooltip("Provider types to use, in order.\n\nBuilt-in options:\n • Addressable — For assets managed via the Addressable Asset System.\n • Project — For assets stored in project's `Resources` folders.\n • Local — For assets stored on a local file system.\n • GoogleDrive — For assets stored remotely on a Google Drive account.")]
    	public List<string> ProviderTypes;

    	[Token(Token = "0x6001238")]
    	public ResourceLoader<TResource> CreateFor<TResource>(IResourceProviderManager resources) where TResource : UnityEngine.Object
    	{
    		return null;
    	}

    	[Token(Token = "0x6001239")]
    	public LocalizableResourceLoader<TResource> CreateLocalizableFor<TResource>(IResourceProviderManager resources, ILocalizationManager l10n, bool fallbackToSource = true) where TResource : UnityEngine.Object
    	{
    		return null;
    	}

    	[Token(Token = "0x600123A")]
    	[Address(RVA = "0x59AD30", Offset = "0x599730", VA = "0x18059AD30", Slot = "3")]
    	public override string ToString()
    	{
    		return null;
    	}

    	[Token(Token = "0x600123B")]
    	[Address(RVA = "0x59AF20", Offset = "0x599920", VA = "0x18059AF20")]
    	public ResourceLoaderConfiguration()
    	{
    	}
    }
}
