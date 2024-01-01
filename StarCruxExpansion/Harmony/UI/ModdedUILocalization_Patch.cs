namespace Dawn.DMD.StarCruxExpansion.Harmony;

using System.Collections.Generic;
using Death.Darkness;
using Death.Run.Core;
using Death.TimesRealm.UserInterface.Darkness;
using Reflection;
using UI;
using UnityEngine.Localization.SmartFormat;

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
    
    /* TODO: Fix Description Stat Display Errors
        [Error  : Unity Log] FormattingException: Error parsing format string: Could not evaluate the selector "0" at 25
        Debug Descriptions have {0:stat(per|0.#|%|s|u)} more epicness
        -------------------------^
        Stack trace:
        UnityEngine.Localization.SmartFormat.SmartFormatter.FormatError (UnityEngine.Localization.SmartFormat.Core.Parsing.FormatItem errorItem, System.Exception innerException, System.Int32 startIndex, UnityEngine.Localization.SmartFormat.Core.Formatting.FormattingInfo formattingInfo) (at <03b931cd3e5f46079b9cfc26a30a4330>:0)
        UnityEngine.Localization.SmartFormat.SmartFormatter.Format (UnityEngine.Localization.SmartFormat.Core.Formatting.FormattingInfo formattingInfo) (at <03b931cd3e5f46079b9cfc26a30a4330>:0)
        UnityEngine.Localization.SmartFormat.SmartFormatter.Format (UnityEngine.Localization.SmartFormat.Core.Formatting.FormatDetails formatDetails, UnityEngine.Localization.SmartFormat.Core.Parsing.Format format, System.Object current) (at <03b931cd3e5f46079b9cfc26a30a4330>:0)
        UnityEngine.Localization.SmartFormat.SmartFormatter.Format (System.IFormatProvider provider, System.Collections.Generic.IList`1[T] args, System.String format) (at <03b931cd3e5f46079b9cfc26a30a4330>:0)
        UnityEngine.Localization.SmartFormat.SmartFormatter.Format (System.String format, System.Object[] args) (at <03b931cd3e5f46079b9cfc26a30a4330>:0)
        Dawn.DMD.StarCruxExpansion.Harmony.ModdedUILocalization_Patch.UpdateDescriptionAsyncPrefix (Death.TimesRealm.UserInterface.Darkness.GUI_DarknessChallenge __instance) (at C:/Users/Dawn/RiderProjects/DeathMustDieMods/StarCruxExpansion/Harmony/UI/ModdedUILocalization_Patch.cs:47)
        (wrapper dynamic-method) Death.TimesRealm.UserInterface.Darkness.GUI_DarknessChallenge.DMD<Death.TimesRealm.UserInterface.Darkness.GUI_DarknessChallenge::UpdateDescriptionAsync>(Death.TimesRealm.UserInterface.Darkness.GUI_DarknessChallenge)
        (wrapper dynamic-method) MonoMod.Utils.DynamicMethodDefinition.Glue:ThiscallStructRetPtr<Death.TimesRealm.UserInterface.Darkness.GUI_DarknessChallenge::UpdateDescriptionAsync,DMD<Death.TimesRealm.UserInterface.Darkness.GUI_DarknessChallenge::UpdateDescriptionAsync>>(Death.TimesRealm.UserInterface.Darkness.GUI_DarknessChallenge,Cysharp.Threading.Tasks.UniTask&)
        Death.TimesRealm.UserInterface.Darkness.GUI_DarknessChallenge.UpdateDisplayAsync () (at <086614115fc04a1c96af44e8d575bbaa>:0)
        Death.TimesRealm.UserInterface.Darkness.GUI_DarknessChallenge.InitAsync (Death.TimesRealm.UserInterface.Darkness.DarknessGUIConfig config, Death.Darkness.ChallengeData challenge, System.Int32 level) (at <086614115fc04a1c96af44e8d575bbaa>:0)
        Cysharp.Threading.Tasks.CompilerServices.AsyncUniTask`1[TStateMachine].GetResult (System.Int16 token) (at <496f96d3a6324ed1bd7ae6385e4beed4>:0)
        Cysharp.Threading.Tasks.UniTask+WhenAllPromise.TryInvokeContinuation (Cysharp.Threading.Tasks.UniTask+WhenAllPromise self, Cysharp.Threading.Tasks.UniTask+Awaiter& awaiter) (at <496f96d3a6324ed1bd7ae6385e4beed4>:0)
        --- End of stack trace from previous location where exception was thrown ---

        [Error  :UnityExplorer] [Unity] FormattingException: Error parsing format string: Could not evaluate the selector "0" at 25
        Debug Descriptions have {0:stat(per|0.#|%|s|u)} more epicness
        -------------------------^
     */

    [HarmonyPatch("UpdateDescriptionAsync")]
    [HarmonyPrefix]
    private static bool UpdateDescriptionAsyncPrefix(GUI_DarknessChallenge __instance)
    {
        var moddedChallenges = AllModdedChallenges;
        
        var challenge = __instance.Challenge();

        if (moddedChallenges.All(x => x.ChallengeData != challenge))
            return true; // Have it localize vanilla challenges
        
        // Smart.Default.Format()

        var moddedChallengeData = moddedChallenges.First(x => x.ChallengeData.Code == challenge.Code);
        var challengeDescription = Smart.Default.Format(moddedChallengeData.TextInformation.DescriptionFormat, challenge.GenerateStatsForLevel(__instance.Level()).ToArray());

        __instance.TitleText().text = challengeDescription;
        return false;
    }
}