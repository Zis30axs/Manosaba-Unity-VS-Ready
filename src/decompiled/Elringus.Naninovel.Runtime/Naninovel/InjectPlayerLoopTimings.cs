using System;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000133")]
[Flags]
public enum InjectPlayerLoopTimings
{
	[Token(Token = "0x40004CC")]
	All = 0xFFFF,
	[Token(Token = "0x40004CD")]
	Standard = 0x7555,
	[Token(Token = "0x40004CE")]
	Minimum = 0x2110,
	[Token(Token = "0x40004CF")]
	Initialization = 1,
	[Token(Token = "0x40004D0")]
	LastInitialization = 2,
	[Token(Token = "0x40004D1")]
	EarlyUpdate = 4,
	[Token(Token = "0x40004D2")]
	LastEarlyUpdate = 8,
	[Token(Token = "0x40004D3")]
	FixedUpdate = 0x10,
	[Token(Token = "0x40004D4")]
	LastFixedUpdate = 0x20,
	[Token(Token = "0x40004D5")]
	PreUpdate = 0x40,
	[Token(Token = "0x40004D6")]
	LastPreUpdate = 0x80,
	[Token(Token = "0x40004D7")]
	Update = 0x100,
	[Token(Token = "0x40004D8")]
	LastUpdate = 0x200,
	[Token(Token = "0x40004D9")]
	PreLateUpdate = 0x400,
	[Token(Token = "0x40004DA")]
	LastPreLateUpdate = 0x800,
	[Token(Token = "0x40004DB")]
	PostLateUpdate = 0x1000,
	[Token(Token = "0x40004DC")]
	LastPostLateUpdate = 0x2000,
	[Token(Token = "0x40004DD")]
	TimeUpdate = 0x4000,
	[Token(Token = "0x40004DE")]
	LastTimeUpdate = 0x8000
}
