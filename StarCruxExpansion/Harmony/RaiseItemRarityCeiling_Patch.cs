using Death.TimesRealm;
using Death.TimesRealm.UserInterface.Darkness;

namespace Dawn.DMD.StarCruxExpansion.Harmony;

using Death.Darkness;
using JetBrains.Annotations;
using Realms.UI;

/// <summary>
/// Modifies the Star Crux progress bar to display the new modded max value.
/// </summary>

[HarmonyPatch(typeof(DarknessController), nameof(DarknessController.MaxPoints))]
[HarmonyPatch(MethodType.Getter)]
public class RaiseItemRarityCeiling_Patch
{
    [UsedImplicitly]
    private static bool Prefix(DarknessController __instance, ref int __result)
    {
        try
        {
            var vanillaMaxPoints = GetVanillaMaxPoints(__instance);
            var moddedMaxPoints = GetModdedMaxPoints();


            __result = vanillaMaxPoints + moddedMaxPoints;
            return false;
        }
        catch (Exception e)
        {
            ModLogger.LogError(e);
            return true;
        }
    }

    internal static int GetVanillaMaxPoints(IDarknessController controller) => 
        controller.GetAllChallenges()
            .Where(controller.IsUnlocked)
            .Select(x => x.PointsPerLevel * x.MaxLevel)
            .Sum();

    internal static int GetModdedMaxPoints()
    {
        var moddedMaxPoints = ModdedRealmManager._allModdedChallenges.Select(x =>
        {
            var challengeData = x.ChallengeData;
            return challengeData.PointsPerLevel * challengeData.MaxLevel;
        });

        return moddedMaxPoints.Sum();
    }
}