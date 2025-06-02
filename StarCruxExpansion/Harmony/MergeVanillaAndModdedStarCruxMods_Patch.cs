namespace Dawn.DMD.StarCruxExpansion.Harmony;

using Death.App;
using Death.Data;
using Death.Run.Core;
using Helpers;
using JetBrains.Annotations;
using Realms.UI;

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
            ModLogger.LogError(e);
            return true;
        }
    }

    private static RunOptions GenerateMergedRunOptions(Profile profile)
    {
        var character = GetCharacterSetup(profile);
        var waveData = Database.WaveData;
        var targetDarkness = profile.Darkness.Clone();

        foreach (var moddedRealm in ModdedRealmManager._moddedRealms)
        {
            moddedRealm.options.MergeTo(targetDarkness);
        }
        
        
        return new RunOptions(character, waveData, targetDarkness); 
    }
    
    private static CharacterSetup GetCharacterSetup(Profile profile)
    {
        var code = GetProgressionCode(profile.Progression);
        return new CharacterSetup(code, profile.Progression.GetTraitFor(code));
    }

    // Should we humor previous versions of the game?
    // Might replace later with just `profile.Progression.SelectedCharacterId`
    private static string GetProgressionCode(Progression progress)
    {
        // profile.Progression.SelectedCharacterCode
        // Previous versions
        if (typeof(Progression).GetField("SelectedCharacterCode", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public) is { } selectedCharacterCodeField)
            return (string)selectedCharacterCodeField.GetValue(progress);
        
        // profile.Progression.SelectedCharacterId
        // Newer versions
        if (typeof(Progression).GetField("SelectedCharacterId", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public) is { } selectedCharacterIdField)
            return (string)selectedCharacterIdField.GetValue(progress);
        
        throw new InvalidOperationException("Unable to retrieve character code from Progression. Version is incompatible or field names have changed.");
    }
}