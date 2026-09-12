using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000343")]
public class EngineTypes
{
	[Token(Token = "0x4000C37")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private string[] commands;

	[Token(Token = "0x4000C38")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private string[] configurations;

	[Token(Token = "0x4000C39")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private string[] resourceProviders;

	[Token(Token = "0x4000C3A")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private string[] actorImplementations;

	[Token(Token = "0x4000C3B")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private string[] customActorMetadata;

	[Token(Token = "0x4000C3C")]
	[FieldOffset(Offset = "0x38")]
	[SerializeField]
	private string[] initializeAtRuntime;

	[Token(Token = "0x4000C3D")]
	[FieldOffset(Offset = "0x40")]
	[SerializeField]
	private string[] dontReset;

	[Token(Token = "0x4000C3E")]
	[FieldOffset(Offset = "0x48")]
	[SerializeField]
	private string[] expressionFunctionHosts;

	[Token(Token = "0x4000C3F")]
	[FieldOffset(Offset = "0x50")]
	[SerializeField]
	private string[] consoleCommandHosts;

	[Token(Token = "0x4000C40")]
	[FieldOffset(Offset = "0x58")]
	[SerializeField]
	private string[] managedTextFieldHosts;

	[Token(Token = "0x4000C41")]
	[FieldOffset(Offset = "0x60")]
	private Dictionary<string, IReadOnlyCollection<Type>> constructed;

	[Token(Token = "0x1700021C")]
	public IReadOnlyCollection<Type> Commands
	{
		[Token(Token = "0x6000FAD")]
		[Address(RVA = "0x5692C0", Offset = "0x567CC0", VA = "0x1805692C0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700021D")]
	public IReadOnlyCollection<Type> Configurations
	{
		[Token(Token = "0x6000FAE")]
		[Address(RVA = "0x569310", Offset = "0x567D10", VA = "0x180569310")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700021E")]
	public IReadOnlyCollection<Type> ResourceProviders
	{
		[Token(Token = "0x6000FAF")]
		[Address(RVA = "0x569360", Offset = "0x567D60", VA = "0x180569360")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700021F")]
	public IReadOnlyCollection<Type> ActorImplementations
	{
		[Token(Token = "0x6000FB0")]
		[Address(RVA = "0x5693B0", Offset = "0x567DB0", VA = "0x1805693B0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000220")]
	public IReadOnlyCollection<Type> CustomActorMetadata
	{
		[Token(Token = "0x6000FB1")]
		[Address(RVA = "0x569400", Offset = "0x567E00", VA = "0x180569400")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000221")]
	public IReadOnlyCollection<Type> InitializeAtRuntime
	{
		[Token(Token = "0x6000FB2")]
		[Address(RVA = "0x569450", Offset = "0x567E50", VA = "0x180569450")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000222")]
	public IReadOnlyCollection<Type> DontReset
	{
		[Token(Token = "0x6000FB3")]
		[Address(RVA = "0x5694A0", Offset = "0x567EA0", VA = "0x1805694A0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000223")]
	public IReadOnlyCollection<Type> ExpressionFunctionHosts
	{
		[Token(Token = "0x6000FB4")]
		[Address(RVA = "0x5694F0", Offset = "0x567EF0", VA = "0x1805694F0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000224")]
	public IReadOnlyCollection<Type> ConsoleCommandHosts
	{
		[Token(Token = "0x6000FB5")]
		[Address(RVA = "0x569540", Offset = "0x567F40", VA = "0x180569540")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000225")]
	public IReadOnlyCollection<Type> ManagedTextFieldHosts
	{
		[Token(Token = "0x6000FB6")]
		[Address(RVA = "0x569590", Offset = "0x567F90", VA = "0x180569590")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000FB7")]
	[Address(RVA = "0x5695E0", Offset = "0x567FE0", VA = "0x1805695E0")]
	private IReadOnlyCollection<Type> GetOrConstruct(string key, IReadOnlyCollection<string> typeNames)
	{
		return null;
	}

	[Token(Token = "0x6000FB8")]
	[Address(RVA = "0x569930", Offset = "0x568330", VA = "0x180569930")]
	public EngineTypes()
	{
	}
}
