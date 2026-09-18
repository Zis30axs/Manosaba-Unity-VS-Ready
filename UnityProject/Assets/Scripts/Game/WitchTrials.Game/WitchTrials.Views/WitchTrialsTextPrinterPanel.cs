#nullable enable annotations
﻿using System.Linq;
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

namespace WitchTrials.Views
{
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
    			// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 (shared field getter, +0x10)
    			[Token(Token = "0x600021F")]
    			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    			get
    			{
    				return _appearance;
    			}
    		}

    		[Token(Token = "0x17000040")]
    		public ChangeScaleTarget[] Targets
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (shared field getter, +0x18)
    			[Token(Token = "0x6000220")]
    			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    			get
    			{
    				return _targets;
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
    			// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 (shared field getter, +0x10)
    			[Token(Token = "0x6000222")]
    			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    			get
    			{
    				return _transform;
    			}
    		}

    		[Token(Token = "0x17000042")]
    		public Vector2 Scale
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x33FBD0 (shared Vector2 field getter, +0x18)
    			[Token(Token = "0x6000223")]
    			[Address(RVA = "0x33FBD0", Offset = "0x33E5D0", VA = "0x18033FBD0")]
    			get
    			{
    				return _scale;
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
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33E0C0 (shared `return null` body)
    		// Overridden to null so the base printer never drives the name panel; this class uses
    		// `base.AuthorNamePanel` (RevealableTextPrinterPanel 0x4933A0 = +0x158, inlined) itself.
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
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33F250
    		// Lazy +0x220 ??= `base.AuthorNamePanel` (+0x158) .GetComponent<CanvasGroup>().
    		[Token(Token = "0x6000219")]
    		[Address(RVA = "0x33F250", Offset = "0x33DC50", VA = "0x18033F250")]
    		get
    		{
    			return _authorPlateCanvasGroup ??= base.AuthorNamePanel.GetComponent<CanvasGroup>();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x33F310 (stub), MoveNext 0x33FC90, <>n__0 0x33FA90, lambdas 0x326120 / 0x312640
    	// - `<task>5__2` = base.Initialize() (<>n__0: RevealableTextPrinterPanel.Initialize kickoff inlined).
    	// - `_changeScaleTargets` (+0x230) = `_changeScaleByAppearances` (+0x218).ToDictionary(<>c.b__9_0 Appearance,
    	//   <>c.b__9_1 Targets); both lambdas are cached static delegates (shared field-getter bodies).
    	// - `_authorTextBuilder` (+0x228) = new AuthorTextBuilder(_authorTextSettings +0x210); await InitializeAsync(
    	//   destroyCancellationToken) (Cysharp); then await `task` (Naninovel, state 1).
    	[Token(Token = "0x600021A")]
    	[Address(RVA = "0x33F310", Offset = "0x33DD10", VA = "0x18033F310", Slot = "83")]
    	[AsyncStateMachine(typeof(_003CInitialize_003Ed__9))]
    	public override async Naninovel.UniTask Initialize()
    	{
    		Naninovel.UniTask task = base.Initialize();
    		_changeScaleTargets = _changeScaleByAppearances.ToDictionary((ChangeScaleByAppearance item) => item.Appearance, (ChangeScaleByAppearance item) => item.Targets);
    		_authorTextBuilder = new AuthorTextBuilder(_authorTextSettings);
    		await _authorTextBuilder.InitializeAsync(destroyCancellationToken);
    		await task;
    	}

    	// RECONSTRUCTED | Confidence: A (local and ternary spelling: B) | RVA: 0x33F440, 1041 B
    	// - RevealableTextPrinterPanel.SetMessageAuthor (real call).
    	// - `AuthorMeta` (slot 118) called twice: UseCharacterColor (+0x80) ? new Color?(NameColor +0x84) : null.
    	// - TryBuildAuthorText(author.Id, color, out text) on `_authorTextBuilder` (+0x228). On false: `Author` (slot 117)
    	//   .Label.IsEmpty (inlined: parts null or length 0) ? `CharacterManager` (slot 104) slot 7 GetAuthorName(Author.Id)
    	//   : (string)Author.Label; explicit null when that name is null/whitespace, else BuildTaggedSimpleName(name, null).
    	// - hasText = !IsNullOrWhiteSpace(text); AuthorPlateCanvasGroup (getter inlined) alpha 1f/0f; when hasText,
    	//   `base.AuthorNamePanel` (+0x158) AuthorNamePanel slot 5 `Text = text`.
    	[Token(Token = "0x600021B")]
    	[Address(RVA = "0x33F440", Offset = "0x33DE40", VA = "0x18033F440", Slot = "128")]
    	protected override void SetMessageAuthor(MessageAuthor author)
    	{
    		base.SetMessageAuthor(author);
    		Color? color = AuthorMeta.UseCharacterColor ? new Color?(AuthorMeta.NameColor) : null;
    		if (!_authorTextBuilder.TryBuildAuthorText(author.Id, color, out string text))
    		{
    			string authorName = Author.Label.IsEmpty ? CharacterManager.GetAuthorName(Author.Id) : (string)Author.Label;
    			text = string.IsNullOrWhiteSpace(authorName) ? null : _authorTextBuilder.BuildTaggedSimpleName(authorName, null);
    		}
    		bool hasAuthorText = !string.IsNullOrWhiteSpace(text);
    		AuthorPlateCanvasGroup.alpha = hasAuthorText ? 1f : 0f;
    		if (hasAuthorText)
    		{
    			base.AuthorNamePanel.Text = text;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (Vector2-to-Vector3 spelling: B) | RVA: 0x33F860
    	// RevealableTextPrinterPanelModified.SetActiveAppearance (real call); `_changeScaleTargets` (+0x230) loaded, then
    	// GetActiveAppearance (slot 130), IDictionary slot 7 TryGetValue; for each target: Transform (+0x10) localScale =
    	// (Scale.x, Scale.y, 0) via the `Transform::set_localScale_Injected` icall (name read from the resolver string).
    	[Token(Token = "0x600021C")]
    	[Address(RVA = "0x33F860", Offset = "0x33E260", VA = "0x18033F860", Slot = "131")]
    	protected override void SetActiveAppearance(string appearance)
    	{
    		base.SetActiveAppearance(appearance);
    		if (_changeScaleTargets.TryGetValue(GetActiveAppearance(), out ChangeScaleTarget[] targets))
    		{
    			foreach (ChangeScaleTarget target in targets)
    			{
    				target.Transform.localScale = target.Scale;
    			}
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x33FA80, 7 B
    	// Tail jump to RevealableTextPrinterPanel..ctor; no field initializers.
    	[Token(Token = "0x600021D")]
    	[Address(RVA = "0x33FA80", Offset = "0x33E480", VA = "0x18033FA80")]
    	public WitchTrialsTextPrinterPanel()
    	{
    	}
    }
}
