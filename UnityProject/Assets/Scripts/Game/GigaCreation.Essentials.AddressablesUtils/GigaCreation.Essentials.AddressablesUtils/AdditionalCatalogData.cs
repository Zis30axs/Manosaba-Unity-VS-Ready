using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.AddressablesUtils
{
    [Serializable]
    [Token(Token = "0x2000004")]
    public class AdditionalCatalogData
    {
    	[Token(Token = "0x4000006")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private string _additionalProjectId;

    	[Token(Token = "0x4000007")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private bool _isRemote;

    	[Token(Token = "0x4000008")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private string _serverUrl;

    	[Token(Token = "0x4000009")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private string _additionalAssetsVersion;

    	[Token(Token = "0x400000A")]
    	[FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private DeploymentEnvironmentType _additionalAssetsEnvironment;

    	[Token(Token = "0x17000001")]
    	public string AdditionalProjectId
    	{
    		[Token(Token = "0x6000003")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000002")]
    	public bool IsRemote
    	{
    		[Token(Token = "0x6000004")]
    		[Address(RVA = "0x44BDC0", Offset = "0x44A7C0", VA = "0x18044BDC0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000003")]
    	public string ServerUrl
    	{
    		[Token(Token = "0x6000005")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000004")]
    	public string AdditionalAssetsVersion
    	{
    		[Token(Token = "0x6000006")]
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000005")]
    	public DeploymentEnvironmentType AdditionalAssetsEnvironment
    	{
    		[Token(Token = "0x6000007")]
    		[Address(RVA = "0x36DB50", Offset = "0x36C550", VA = "0x18036DB50")]
    		get
    		{
    			return default(DeploymentEnvironmentType);
    		}
    	}

    	[Token(Token = "0x6000008")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public AdditionalCatalogData()
    	{
    	}
    }
}
