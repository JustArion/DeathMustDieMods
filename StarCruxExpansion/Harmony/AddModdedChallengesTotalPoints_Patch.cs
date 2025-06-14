namespace Dawn.DMD.StarCruxExpansion.Harmony;

using Death.Darkness;
using Death.Data;
using Helpers;
using JetBrains.Annotations;
using Realms.UI;
using Reflection;

[HarmonyPatch(typeof(DarknessOptions), nameof(DarknessOptions.GetTotalPoints))]
public class AddModdedChallengesTotalPoints_Patch
{
    
    /* DarknessOptions::GetTotalPoints()
        IL_0018: call         class Death.Data.Tables.DarknessTable Death.Data.Database::get_Darkness()
        IL_001d: ldloc.2      // challenge1
        IL_001e: ldfld        string Death.Darkness.DarknessOptions/Challenge::Code
        IL_0023: ldloca.s     challenge2
        IL_0025: callvirt     instance bool Death.Data.Tables.DarknessTable::TryGet(string, class Death.Darkness.ChallengeData&)
        IL_002a: brfalse.s    IL_003c
     */
    // private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    // {
    //     var instructionArray = instructions.ToArray();
    //     
    //     
    //     
    //     
    // }

    [UsedImplicitly]
    private static bool Prefix(DarknessOptions __instance, ref int __result)
    {
        var totalPoints = 0;
        
        try
        {
            
            var vanillaOptions = InterceptStarCruxData_Patch.VanillaDarknessController?.Options ?? __instance;
            vanillaOptions.Challenges().ForEach(challenge =>
            {
                if (Database.Darkness.TryGet(challenge.Code, out var challengeData))
                    totalPoints += challengeData.PointsPerLevel * challenge.Level;
            });

            foreach (var moddedRealm in ModdedRealmManager._moddedRealms)
            {
                moddedRealm.options.Challenges().ForEach(challenge =>
                {
                    if (ModdedRealmHelper.TryGetModdedChallenge(challenge.Code, out var challengeData))
                        totalPoints += challengeData.PointsPerLevel * challenge.Level;
                });
            }
        }
        catch (Exception e)
        {
            ModLogger.LogError(e);
            return true;
        }


        __result = totalPoints;
        return false;
    }
}