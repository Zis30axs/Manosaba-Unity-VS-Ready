using UnityEngine;

namespace WitchTrials.Views
{
    /// <summary>
    /// Player choice for Ema's refutation cut-in.  The shipped game always shows the third of her three portraits;
    /// the other two are deobfuscated art that the game never selects.  Off (the default) keeps the shipped portrait,
    /// on lets every portrait be picked.  Stored in PlayerPrefs so it is independent of save data.
    /// </summary>
    public static class CutInPortraitSetting
    {
        private const string Key = "Manosaba.CutInPortrait.UseAll";

        public static bool UseAll
        {
            get => PlayerPrefs.GetInt(Key, 0) == 1;
            set
            {
                PlayerPrefs.SetInt(Key, value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }
    }
}
