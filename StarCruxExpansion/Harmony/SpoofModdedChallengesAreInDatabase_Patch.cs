namespace Dawn.DMD.StarCruxExpansion.Harmony;

using Death.Darkness;
using Death.Data.Tables;
using Helpers;
using JetBrains.Annotations;

[HarmonyPatch(typeof(DarknessTable), nameof(DarknessTable.TryGet))]
public class SpoofModdedChallengesAreInDatabase_Patch
{
    [UsedImplicitly]
    private static bool Prefix(ref bool __result, string __0, out ChallengeData __1)
    {
        if (!ModdedRealmHelper.TryGetModdedChallenge(__0, out __1)) 
            return true;
                
        __result = true;
        return false;
    }
}