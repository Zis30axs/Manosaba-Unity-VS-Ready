using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using GigaCreation.NaninovelExtender.Ui;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000099")]
public class WitchTrialsTextPrinterPanel : RevealableTextPrinterPanelModified
{
	[Serializable]
	[Token(Token = "0x200009A")]
	public class ChangeScaleByAppearance
	{
		[Token(Token = "0x4000294")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		[SerializeField]
		private string _appearance;

		[Token(Token = "0x4000295")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		[SerializeField]
		private ChangeScaleTarget[] _targets;

		[Token(Token = "0x1700003F")]
		public string Appearance
		{
			[Token(Token = "0x600021F")]
			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x17000040")]
		public ChangeScaleTarget[] Targets
		{
			[Token(Token = "0x6000220")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x6000221")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public ChangeScaleByAppearance()
		{
		}
	}

	[Serializable]
	[Token(Token = "0x200009B")]
	public class ChangeScaleTarget
	{
		[Token(Token = "0x4000296")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		[SerializeField]
		private Transform _transform;

		[Token(Token = "0x4000297")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		[SerializeField]
		private Vector2 _scale;

		[Token(Token = "0x17000041")]
		public Transform Transform
		{
			[Token(Token = "0x6000222")]
			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x17000042")]
		public Vector2 Scale
		{
			[Token(Token = "0x6000223")]
			[Address(RVA = "0x33FBD0", Offset = "0x33E5D0", VA = "0x18033FBD0")]
			get
			{
				return default(Vector2);
			}
		}

		[Token(Token = "0x6000224")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public ChangeScaleTarget()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200009D")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__9
{
		[Token(Token = "0x400029B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400029C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400029D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public WitchTrialsTextPrinterPanel _003C_003E4__this;

		[Token(Token = "0x400029E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private Naninovel.UniTask _003Ctask_003E5__2;

		[Token(Token = "0x400029F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40002A0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000229")]
		[Address(RVA = "0x33FC90", Offset = "0x33E690", VA = "0x18033FC90", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600022A")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400028F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x210")]
	[Header("Parameters for WitchTrials")]
	[SerializeField]
	private AuthorTextBuilder.Settings _authorTextSettings;

	[Token(Token = "0x4000290")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x218")]
	[SerializeField]
	private ChangeScaleByAppearance[] _changeScaleByAppearances;

	[Token(Token = "0x4000291")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x220")]
	private CanvasGroup _authorPlateCanvasGroup;

	[Token(Token = "0x4000292")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x228")]
	private AuthorTextBuilder _authorTextBuilder;

	[Token(Token = "0x4000293")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x230")]
	private IDictionary<string, ChangeScaleTarget[]> _changeScaleTargets;

	[Token(Token = "0x1700003D")]
	protected override AuthorNamePanel AuthorNamePanel
	{
		[Token(Token = "0x6000218")]
		[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "120")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700003E")]
	private CanvasGroup AuthorPlateCanvasGroup
	{
		[Token(Token = "0x6000219")]
		[Address(RVA = "0x33F250", Offset = "0x33DC50", VA = "0x18033F250")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600021A")]
	[Address(RVA = "0x33F310", Offset = "0x33DD10", VA = "0x18033F310", Slot = "83")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__9))]
	public override Naninovel.UniTask Initialize()
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x600021B")]
	[Address(RVA = "0x33F440", Offset = "0x33DE40", VA = "0x18033F440", Slot = "128")]
	protected override void SetMessageAuthor(MessageAuthor author)
	{
	}

	[Token(Token = "0x600021C")]
	[Address(RVA = "0x33F860", Offset = "0x33E260", VA = "0x18033F860", Slot = "131")]
	protected override void SetActiveAppearance(string appearance)
	{
	}

	[Token(Token = "0x600021D")]
	[Address(RVA = "0x33FA80", Offset = "0x33E480", VA = "0x18033FA80")]
	public WitchTrialsTextPrinterPanel()
	{
	}
}
