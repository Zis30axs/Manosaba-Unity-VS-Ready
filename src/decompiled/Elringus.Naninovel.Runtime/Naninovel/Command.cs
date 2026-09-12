using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003C9")]
public abstract class Command
{
	[Token(Token = "0x20003CA")]
	public interface ILocalizable
	{
	}

	[Token(Token = "0x20003CB")]
	public interface IPreloadable
	{
		[Token(Token = "0x6001270")]
		UniTask PreloadResources();

		[Token(Token = "0x6001271")]
		void ReleaseResources();
	}

	[Token(Token = "0x20003CC")]
	public interface INestedHost
	{
		[Token(Token = "0x6001272")]
		int GetNextPlaybackIndex(ScriptPlaylist playlist, int playedIndex);
	}

	[Token(Token = "0x20003CD")]
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class CommandAliasAttribute : Attribute
	{
		[Token(Token = "0x1700027C")]
		public string Alias
		{
			[Token(Token = "0x6001273")]
			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x6001274")]
		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
		public CommandAliasAttribute(string alias)
		{
		}
	}

	[Token(Token = "0x20003CE")]
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class RequiredParameterAttribute : Attribute
	{
		[Token(Token = "0x6001275")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public RequiredParameterAttribute()
		{
		}
	}

	[Token(Token = "0x20003CF")]
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class ParameterAliasAttribute : Attribute
	{
		[Token(Token = "0x1700027D")]
		public string Alias
		{
			[Token(Token = "0x6001276")]
			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x6001277")]
		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
		public ParameterAliasAttribute(string alias)
		{
		}
	}

	[Token(Token = "0x20003D0")]
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class ParameterDefaultValueAttribute : Attribute
	{
		[Token(Token = "0x1700027E")]
		public string Value
		{
			[Token(Token = "0x6001278")]
			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x6001279")]
		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
		public ParameterDefaultValueAttribute(string value)
		{
		}
	}

	[Token(Token = "0x20003D3")]
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass33_0
	{
		[Token(Token = "0x4000E46")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public Command _003C_003E4__this;

		[Token(Token = "0x4000E47")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public LocalizableTextParameter[] text;

		[Token(Token = "0x600127F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public _003C_003Ec__DisplayClass33_0()
		{
		}

		[Token(Token = "0x6001280")]
		[Address(RVA = "0x59F1F0", Offset = "0x59DBF0", VA = "0x18059F1F0")]
		internal UniTask _003CLoadDynamicTextResources_003Eb__0(LocalizableTextParameter t)
		{
			return default(UniTask);
		}

		[Token(Token = "0x6001281")]
		[Address(RVA = "0x59F330", Offset = "0x59DD30", VA = "0x18059F330")]
		internal void _003CLoadDynamicTextResources_003Eb__1()
		{
		}
	}

	[Token(Token = "0x20003D4")]
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass34_0
	{
		[Token(Token = "0x4000E48")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public Command _003C_003E4__this;

		[Token(Token = "0x4000E49")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public (LocalizableTextParameter Parameter, LocalizableText ResolvedValue)[] text;

		[Token(Token = "0x6001282")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public _003C_003Ec__DisplayClass34_0()
		{
		}

		[Token(Token = "0x6001283")]
		[Address(RVA = "0x59F490", Offset = "0x59DE90", VA = "0x18059F490")]
		internal UniTask _003CLoadDynamicTextResources_003Eb__0((LocalizableTextParameter Parameter, LocalizableText ResolvedValue) t)
		{
			return default(UniTask);
		}

		[Token(Token = "0x6001284")]
		[Address(RVA = "0x59F5B0", Offset = "0x59DFB0", VA = "0x18059F5B0")]
		internal void _003CLoadDynamicTextResources_003Eb__1()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20003D5")]
	[CompilerGenerated]
	private struct _003CLoadDynamicTextResources_003Ed__33
{
		[Token(Token = "0x4000E4A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000E4B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<IDisposable> _003C_003Et__builder;

		[Token(Token = "0x4000E4C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Command _003C_003E4__this;

		[Token(Token = "0x4000E4D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public LocalizableTextParameter[] text;

		[Token(Token = "0x4000E4E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private _003C_003Ec__DisplayClass33_0 _003C_003E8__1;

		[Token(Token = "0x4000E4F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001285")]
		[Address(RVA = "0x59F700", Offset = "0x59E100", VA = "0x18059F700", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001286")]
		[Address(RVA = "0x59FCE0", Offset = "0x59E6E0", VA = "0x18059FCE0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20003D6")]
	[CompilerGenerated]
	private struct _003CLoadDynamicTextResources_003Ed__34
{
		[Token(Token = "0x4000E50")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000E51")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<IDisposable> _003C_003Et__builder;

		[Token(Token = "0x4000E52")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Command _003C_003E4__this;

		[Token(Token = "0x4000E53")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public (LocalizableTextParameter Parameter, LocalizableText ResolvedValue)[] text;

		[Token(Token = "0x4000E54")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private _003C_003Ec__DisplayClass34_0 _003C_003E8__1;

		[Token(Token = "0x4000E55")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001287")]
		[Address(RVA = "0x59FD10", Offset = "0x59E710", VA = "0x18059FD10", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001288")]
		[Address(RVA = "0x5A02F0", Offset = "0x59ECF0", VA = "0x1805A02F0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20003D7")]
	[CompilerGenerated]
	private struct _003CWaitOrForget_003Ed__35
{
		[Token(Token = "0x4000E56")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000E57")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000E58")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public BooleanParameter wait;

		[Token(Token = "0x4000E59")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Command _003C_003E4__this;

		[Token(Token = "0x4000E5A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x4000E5B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public Func<AsyncToken, UniTask> fn;

		[Token(Token = "0x4000E5C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private IDisposable _003C__003E5__2;

		[Token(Token = "0x4000E5D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001289")]
		[Address(RVA = "0x5A0320", Offset = "0x59ED20", VA = "0x1805A0320", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600128A")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000E3A")]
	public const string DefaultNamespace = "Naninovel.Commands";

	[Token(Token = "0x4000E3B")]
	public const string NamelessParameterAlias = "";

	[Token(Token = "0x4000E3C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	[ParameterAlias("if")]
	[ConditionContext(-1, null)]
	public StringParameter ConditionalExpression;

	[Token(Token = "0x4000E3D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	[SerializeField]
	private PlaybackSpot playbackSpot;

	[Token(Token = "0x4000E3E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private int indent;

	[Token(Token = "0x4000E3F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static LiteralMap<Type> commandTypesCache;

	[Token(Token = "0x17000278")]
	public static LiteralMap<Type> CommandTypes
	{
		[Token(Token = "0x6001258")]
		[Address(RVA = "0x59D320", Offset = "0x59BD20", VA = "0x18059D320")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000279")]
	public PlaybackSpot PlaybackSpot
	{
		[Token(Token = "0x6001259")]
		[Address(RVA = "0x331710", Offset = "0x330110", VA = "0x180331710")]
		get
		{
			return default(PlaybackSpot);
		}
		[Token(Token = "0x600125A")]
		[Address(RVA = "0x59D3D0", Offset = "0x59BDD0", VA = "0x18059D3D0")]
		set
		{
		}
	}

	[Token(Token = "0x1700027A")]
	public int Indent
	{
		[Token(Token = "0x600125B")]
		[Address(RVA = "0x4403C0", Offset = "0x43EDC0", VA = "0x1804403C0")]
		get
		{
			return default(int);
		}
		[Token(Token = "0x600125C")]
		[Address(RVA = "0x4403D0", Offset = "0x43EDD0", VA = "0x1804403D0")]
		set
		{
		}
	}

	[Token(Token = "0x1700027B")]
	public virtual bool ShouldExecute
	{
		[Token(Token = "0x600125D")]
		[Address(RVA = "0x59D430", Offset = "0x59BE30", VA = "0x18059D430", Slot = "4")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x600125E")]
	[Address(RVA = "0x59D600", Offset = "0x59C000", VA = "0x18059D600")]
	public static Type ResolveCommandType(string commandId)
	{
		return null;
	}

	[Token(Token = "0x600125F")]
	public abstract UniTask Execute([Optional] AsyncToken token);

	[Token(Token = "0x6001260")]
	[Address(RVA = "0x59D7C0", Offset = "0x59C1C0", VA = "0x18059D7C0", Slot = "6")]
	public virtual void Log(string message)
	{
	}

	[Token(Token = "0x6001261")]
	[Address(RVA = "0x59D880", Offset = "0x59C280", VA = "0x18059D880", Slot = "7")]
	public virtual void Warn(string message)
	{
	}

	[Token(Token = "0x6001262")]
	[Address(RVA = "0x59D940", Offset = "0x59C340", VA = "0x18059D940", Slot = "8")]
	public virtual void Err(string message)
	{
	}

	[Token(Token = "0x6001263")]
	[Address(RVA = "0x59DA00", Offset = "0x59C400", VA = "0x18059DA00")]
	public static bool Assigned(ICommandParameter parameter)
	{
		return default(bool);
	}

	[Token(Token = "0x6001264")]
	[Address(RVA = "0x59DA50", Offset = "0x59C450", VA = "0x18059DA50")]
	public static bool AssignedDynamic(ICommandParameter parameter)
	{
		return default(bool);
	}

	[Token(Token = "0x6001265")]
	[Address(RVA = "0x59DAE0", Offset = "0x59C4E0", VA = "0x18059DAE0")]
	public static bool AssignedStatic(ICommandParameter parameter)
	{
		return default(bool);
	}

	[Token(Token = "0x6001266")]
	[Address(RVA = "0x59DB70", Offset = "0x59C570", VA = "0x18059DB70", Slot = "9")]
	protected virtual UniTask PreloadStaticTextResources(params LocalizableTextParameter[] text)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001267")]
	[Address(RVA = "0x59DC40", Offset = "0x59C640", VA = "0x18059DC40", Slot = "10")]
	protected virtual void ReleaseStaticTextResources(params LocalizableTextParameter[] text)
	{
	}

	[Token(Token = "0x6001268")]
	[Address(RVA = "0x59DDA0", Offset = "0x59C7A0", VA = "0x18059DDA0", Slot = "11")]
	[AsyncStateMachine(typeof(_003CLoadDynamicTextResources_003Ed__33))]
	protected virtual UniTask<IDisposable> LoadDynamicTextResources(params LocalizableTextParameter[] text)
	{
		return default(UniTask<IDisposable>);
	}

	[Token(Token = "0x6001269")]
	[Address(RVA = "0x59DF90", Offset = "0x59C990", VA = "0x18059DF90", Slot = "12")]
	[AsyncStateMachine(typeof(_003CLoadDynamicTextResources_003Ed__34))]
	protected virtual UniTask<IDisposable> LoadDynamicTextResources(params (LocalizableTextParameter Parameter, LocalizableText ResolvedValue)[] text)
	{
		return default(UniTask<IDisposable>);
	}

	[Token(Token = "0x600126A")]
	[Address(RVA = "0x59E180", Offset = "0x59CB80", VA = "0x18059E180", Slot = "13")]
	[AsyncStateMachine(typeof(_003CWaitOrForget_003Ed__35))]
	protected virtual UniTask WaitOrForget(Func<AsyncToken, UniTask> fn, BooleanParameter wait, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600126B")]
	[Address(RVA = "0x59E3C0", Offset = "0x59CDC0", VA = "0x18059E3C0", Slot = "14")]
	protected virtual IDisposable CompleteOnContinueWhenEnabled(ref AsyncToken token)
	{
		return null;
	}

	[Token(Token = "0x600126C")]
	[Address(RVA = "0x59E490", Offset = "0x59CE90", VA = "0x18059E490", Slot = "15")]
	protected virtual IDisposable CompleteOnContinue(ref AsyncToken token)
	{
		return null;
	}

	[Token(Token = "0x600126D")]
	[Address(RVA = "0x59E7C0", Offset = "0x59D1C0", VA = "0x18059E7C0")]
	private static LiteralMap<Type> GetCommandTypes()
	{
		return null;
	}

	[Token(Token = "0x600126E")]
	[Address(RVA = "0x59EE30", Offset = "0x59D830", VA = "0x18059EE30")]
	protected Command()
	{
	}
}
