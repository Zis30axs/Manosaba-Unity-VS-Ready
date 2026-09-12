using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x20000F4")]
public interface IWitchBookUi : IManagedUI
{
	[Token(Token = "0x60003D5")]
	void SetPresentationMode(WitchBookPresentationMode value);

	[Token(Token = "0x60003D6")]
	void SetRestrictionCategories(IReadOnlyList<WitchBookCategory> categories);

	[Token(Token = "0x60003D7")]
	void SetRestrictionMapAreas(IReadOnlyCollection<MapAreaKind> areas);

	[Token(Token = "0x60003D8")]
	IReadOnlyCollection<IdVersionPair> GetActiveEvidences(WitchBookCategory category);

	[Token(Token = "0x60003D9")]
	UniTask<string> ChoiceEvidenceAsync(IReadOnlyList<WitchBookCategory> categories, [Optional] AsyncToken token);

	[Token(Token = "0x60003DA")]
	UniTask<MapAreaKind> ChoiceAreaAsync([Optional] AsyncToken token);

	[Token(Token = "0x60003DB")]
	void UpdateVersion(WitchBookCategory category, string id, int version);

	[Token(Token = "0x60003DC")]
	void ClearState(WitchBookCategory category);
}
