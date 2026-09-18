using GigaCreation.Essentials.Platforms;
using GigaCreation.Tools.Service;
using System.Text;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Dialogs.Models;
using GigaCreation.Essentials.Dialogs.Views;
using GigaCreation.Essentials.Ui;
using GigaCreation.NaninovelExtender.Common;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using WitchTrials.Debugging;

namespace WitchTrials.Views
{
    [Token(Token = "0x200008B")]
    public class GoogleForm : MonoBehaviour
    {
    	[Token(Token = "0x200008C")]
    	private enum EntryKind
    	{
    		[Token(Token = "0x4000243")]
    		Version,
    		[Token(Token = "0x4000244")]
    		Platform,
    		[Token(Token = "0x4000245")]
    		Resolution,
    		[Token(Token = "0x4000246")]
    		PlaybackSpot,
    		[Token(Token = "0x4000247")]
    		PrintedMessage
    	}

    	[Serializable]
    	[Token(Token = "0x200008D")]
    	public class EntryInfo
    	{
    		[Token(Token = "0x4000248")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A7F0 (`.cctor`, 215 B)
    		// The pattern is a metadata string literal; `r8d = 8` is RegexOptions.Compiled. get_Data below
    		// reads the options field (+0x20) with `test 0x40` — the inlined RightToLeft test of Regex.Replace.
    		private static Regex s_scenarioNamePattern = new Regex("([A-Z])[a-z]+(\\d+)", RegexOptions.Compiled);

    		[Token(Token = "0x4000249")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		[SerializeField]
    		private EntryKind _kind;

    		[Token(Token = "0x400024A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		[SerializeField]
    		private string _id;

    		[Token(Token = "0x17000036")]
    		public string Id
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (shared generic reference-field getter, +0x18 `_id`)
    			[Token(Token = "0x60001E7")]
    			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    			get
    			{
    				return _id;
    			}
    		}

    		[Token(Token = "0x17000037")]
    		public string Data
    		{
    			// RECONSTRUCTED | Confidence: A (values), B (how the last `_` segment was spelled in source) | RVA: 0x33A4F0, 763 B
    			// 
    			// A jump chain over `_kind` (+0x10) in declaration order. `Screen.currentResolution` is icall 0x25F3CA0
    			// (`UnityEngine.Screen::get_currentResolution_Injected`), then Resolution.ToString().
    			// The playback spot is `DebugInfoBuilder.TryGetPlaybackSpot`; its last `_`-separated segment is taken by
    			// inlined index arithmetic (`length - 1` with a bounds check), then the static Regex turns e.g. `Trial14`
    			// into `T14`, and `#` (the spot's line separator) becomes `L`.
    			[Token(Token = "0x60001E8")]
    			[Address(RVA = "0x33A4F0", Offset = "0x338EF0", VA = "0x18033A4F0")]
    			get
    			{
    				switch (_kind)
    				{
    				case EntryKind.Version:
    					return Application.version;
    				case EntryKind.Platform:
    					return Platform.CurrentPlatform.ToName();
    				case EntryKind.Resolution:
    					return Screen.currentResolution.ToString();
    				case EntryKind.PlaybackSpot:
    				{
    					if (!DebugInfoBuilder.TryGetPlaybackSpot(out string spot))
    					{
    						return "No playback spot";
    					}
    					string[] parts = spot.Split('_');
    					return s_scenarioNamePattern.Replace(parts[parts.Length - 1], "$1$2").Replace("#", "L");
    				}
    				case EntryKind.PrintedMessage:
    					return DebugInfoBuilder.TryGetPrintedMessage(out string message) ? message : "No message";
    				default:
    					return string.Empty;
    				}
    			}
    		}

    		[Token(Token = "0x60001E9")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public EntryInfo()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200008E")]
    	[CompilerGenerated]
    	private struct _003COpenGoogleFormAsync_003Ed__9
    {
    		[Token(Token = "0x400024B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400024C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400024D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GoogleForm _003C_003E4__this;

    		[Token(Token = "0x400024E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x400024F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private INaninovelInteractionBlocker _003Cblocker_003E5__2;

    		[Token(Token = "0x4000250")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private ProvidableDialog _003Cdialog_003E5__3;

    		[Token(Token = "0x4000251")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<GigaCreation.Essentials.Input.ResponseKind>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60001EB")]
    		[Address(RVA = "0x33A8D0", Offset = "0x3392D0", VA = "0x18033A8D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60001EC")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400023D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private Button _button;

    	[Token(Token = "0x400023E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[Header("Parameters")]
    	[SerializeField]
    	private string _formUrl;

    	[Token(Token = "0x400023F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private EntryInfo[] _entryInfo;

    	[Token(Token = "0x4000240")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private DialogData _openGoogleFormDialogData;

    	[Token(Token = "0x4000241")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	private IDialogProvider _dialogProvider;

    	[Token(Token = "0x17000035")]
    	private IDialogProvider DialogProvider
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x339E20, 186 B
    		// `cl = 1` is ServiceLocator.Get's `includeDerivedType` parameter; IDialogManager derives from IDialogProvider.
    		// The result is cached in `_dialogProvider` (+0x40) and only looked up when that field is still null.
    		[Token(Token = "0x60001E0")]
    		[Address(RVA = "0x339E20", Offset = "0x338820", VA = "0x180339E20")]
    		get
    		{
    			return _dialogProvider ??= ServiceLocator.Get<IDialogManager>(includeDerivedType: true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x339EE0, 149 B
    	// Unconditional — there is no branch in the body: this build destroys the Google-form object on Awake, so the
    	// bug-report button never exists at runtime. TitleUi guards its `_googleForm` use with a Unity-live check.
    	[Token(Token = "0x60001E1")]
    	[Address(RVA = "0x339EE0", Offset = "0x3388E0", VA = "0x180339EE0")]
    	private void Awake()
    	{
    		Destroy(base.gameObject);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x339F80, 151 B (editor-only reset hook; `_button` is +0x20)
    	[Token(Token = "0x60001E2")]
    	[Address(RVA = "0x339F80", Offset = "0x338980", VA = "0x180339F80")]
    	private void Reset()
    	{
    		_button = GetComponentInChildren<Button>();
    	}

    	[Token(Token = "0x60001E3")]
    	[Address(RVA = "0x33A020", Offset = "0x338A20", VA = "0x18033A020")]
    	[AsyncStateMachine(typeof(_003COpenGoogleFormAsync_003Ed__9))]
    	public UniTask OpenGoogleFormAsync([Optional] CancellationToken ct)
    	{
    		return default(UniTask);
    	}

    	// RECONSTRUCTED | Confidence: A (calls), B (`foreach` vs indexed `for`) | RVA: 0x33A1D0, 721 B
    	// 
    	// `new StringBuilder(_formUrl)` is inlined as the (value, 0, value?.Length ?? 0, 16) constructor. Each entry
    	// appends `&entry.<id>=<data>`; `Id` is inlined to the `_id` field read. The result goes through the inlined
    	// `Uri.EscapeUriString` (UriHelper.EscapeString, with the ArgumentNullException("stringToEscape") path kept).
    	// No URL literal exists in code — `_formUrl` is a [SerializeField] filled in the scene/prefab.
    	[Token(Token = "0x60001E4")]
    	[Address(RVA = "0x33A1D0", Offset = "0x338BD0", VA = "0x18033A1D0")]
    	private string BuildUrl()
    	{
    		StringBuilder builder = new StringBuilder(_formUrl);
    		foreach (EntryInfo entry in _entryInfo)
    		{
    			builder.Append("&entry.").Append(entry.Id).Append("=")
    				.Append(entry.Data);
    		}
    		return Uri.EscapeUriString(builder.ToString());
    	}

    	[Token(Token = "0x60001E5")]
    	[Address(RVA = "0x33A4B0", Offset = "0x338EB0", VA = "0x18033A4B0")]
    	public IObservable<Unit> OnClickAsObservable()
    	{
    		return null;
    	}

    	[Token(Token = "0x60001E6")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public GoogleForm()
    	{
    	}
    }
}
