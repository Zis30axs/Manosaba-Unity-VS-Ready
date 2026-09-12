using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace WitchTrials.Views;

[Token(Token = "0x2000031")]
[UsedImplicitly]
[CommandAlias("audioPlayer")]
public class UpdateAudioPlayerUi : Command
{
	[Token(Token = "0x4000087")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	[ParameterAlias("")]
	public StringParameter TrackName;

	[Token(Token = "0x4000088")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private ISoundTestUi _soundTestUi;

	[Token(Token = "0x1700000A")]
	private ISoundTestUi SoundTestUi
	{
		[Token(Token = "0x60000AC")]
		[Address(RVA = "0x318460", Offset = "0x316E60", VA = "0x180318460")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60000AD")]
	[Address(RVA = "0x318550", Offset = "0x316F50", VA = "0x180318550", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000AE")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public UpdateAudioPlayerUi()
	{
	}
}
