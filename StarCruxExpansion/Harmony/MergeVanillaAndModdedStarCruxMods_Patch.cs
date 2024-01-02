namespace Dawn.DMD.StarCruxExpansion.Harmony;

using Death.App;
using Death.Data;
using Death.Run.Core;
using Death.TimesRealm.UserInterface.Darkness;
using Helpers;
using JetBrains.Annotations;
using UI;

[HarmonyPatch(typeof(Profile), nameof(Profile.GenerateRunOptions))]
public class MergeVanillaAndModdedStarCruxMods_Patch
{
    [UsedImplicitly]
    private static bool Prefix(Profile __instance, ref RunOptions __result)
    {
        try
        {
            __result = GenerateMergedRunOptions(__instance);
            return false;
        }
        catch (Exception e)
        {
            Logger.LogError(e);
            return true;
        }
    }

    private static RunOptions GenerateMergedRunOptions(Profile profile)
    {
        var character = new CharacterSetup(profile.Progression.SelectedCharacterCode, profile.Progression.GetTraitFor(profile.Progression.SelectedCharacterCode));
        var waveData = Database.WaveData;
        var targetDarkness = profile.Darkness;

        foreach (var moddedRealm in ModdedRealmManager._moddedRealms)
            moddedRealm._options.CopyTo(targetDarkness);
        
        
        return new RunOptions(character, waveData, targetDarkness); 
    }
}