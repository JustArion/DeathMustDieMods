namespace Dawn.DMD.StarCruxExpansion.Harmony;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Death.TimesRealm.UserInterface.Darkness;
using Helpers;
using Realms;
using Realms.UI;
using Reflection;
using UnityEngine.Localization.Settings;

[HarmonyPatch(typeof(GUI_DarknessChallenge))]
public class ModdedUILocalization_Patch
{
    private static HashSet<ChallengeDataInformation> AllModdedChallenges => ModdedRealmManager._allModdedChallenges;
    
    [HarmonyPatch("UpdateTitleAsync")]
    [HarmonyPrefix] // Allows modded titles to bypass localisation
    private static bool UpdateTitleAsyncPrefix(GUI_DarknessChallenge __instance)
    {
        var moddedChallenges = AllModdedChallenges;
        
        var challenge = __instance.Challenge();

        if (moddedChallenges.All(x => x.ChallengeData != challenge))
            return true; // Have it localize vanilla challenges

        var moddedChallengeData = moddedChallenges.First(x => x.ChallengeData.Code == challenge.Code);
        
        __instance.TitleText().text = moddedChallengeData.TextInformation.Title;
                
        return false;
    }

    [HarmonyPatch("UpdateDescriptionAsync")]
    [HarmonyPrefix]
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")] // Original Method has this
    private static bool UpdateDescriptionAsyncPrefix(GUI_DarknessChallenge __instance)
    {
        var moddedChallenges = AllModdedChallenges;
        
        var challenge = __instance.Challenge();
        
        if (!ModdedRealmHelper.TryGetModdedChallenge(challenge.Code, out _))
            return true;

        var level = __instance.Level();

        var stats = challenge.GenerateStatsForLevel(level).ToArray();
        
        
        var moddedChallengeData = moddedChallenges.First(x => x.ChallengeData.Code == challenge.Code);

        var descriptionFormat = ExceptionWrappers.Wrap(()=> moddedChallengeData.TextInformation.DescriptionFormatBuilder(level), string.Empty, ModLogger.LogErrorMessage);
        var challengeDescription = ExceptionWrappers.Wrap(() => 
                LocalizationSettings.StringDatabase.SmartFormatter.Format(descriptionFormat, stats),
            descriptionFormat, ModLogger.LogErrorMessage);
        
        // ModLogger.LogDebug($"Setting description text to '{challengeDescription}' with format '{descriptionFormat}' to {challenge.Code}");

        __instance.DescriptionText().text = challengeDescription;
        return false;
    }
}