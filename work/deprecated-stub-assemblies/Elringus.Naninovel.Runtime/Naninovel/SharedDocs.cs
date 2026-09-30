using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20000D5")]
    public static class SharedDocs
    {
    	[Token(Token = "0x4000341")]
    	public const string DurationParameter = "\nDuration of the animation initiated by the command, in seconds.";

    	[Token(Token = "0x4000342")]
    	public const string WaitParameter = "\nWhether to wait for the command to finish before starting executing next command in the scenario script.\nDefault behaviour is controlled by `Wait By Default` option in the script player configuration.";

    	[Token(Token = "0x4000343")]
    	public const string TintParameter = "\nThe tint color to apply.\n\nStrings that begin with `#` will be parsed as hexadecimal in the following way:\n`#RGB` (becomes `RRGGBB`), `#RRGGBB`, `#RGBA` (becomes `RRGGBBAA`), `#RRGGBBAA`; when alpha is not specified will default to `FF`.\n\nStrings that do not begin with `#` will be parsed as literal colors, with the following supported:\nred, cyan, blue, darkblue, lightblue, purple, yellow, lime, fuchsia, white, silver, grey, black, orange, brown, maroon, green, olive, navy, teal, aqua, magenta.";

    	[Token(Token = "0x4000344")]
    	public const string EasingParameter = "\nName of the [easing function](/guide/transition-effects#animation-easing) to apply.\nWhen not specified, will use a default function set in the configuration.";

    	[Token(Token = "0x4000345")]
    	public const string LazyParameter = "\nWhen the animation initiated by the command is already running, enabling `lazy` will continue the animation to the new target from the current state.\nWhen `lazy` is not enabled (default behaviour), currently running animation will instantly complete before starting animating to the new target.";
    }
}
