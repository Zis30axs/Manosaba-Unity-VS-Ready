using System;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.Dialogs.Models;

[Token(Token = "0x200001A")]
public interface IManualMotionSkipper : IDisposable
{
	[Token(Token = "0x6000051")]
	void StartUpdate(float startTime, bool ignoreTimeScale = false);
}
