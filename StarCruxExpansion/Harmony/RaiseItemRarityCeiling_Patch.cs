namespace Dawn.DMD.StarCruxExpansion.Harmony;

using Death.Darkness;
using JetBrains.Annotations;

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
            var raisedCeliningMaxPoints = __instance.GetAllChallenges().Where(__instance.IsUnlocked).Select(x => x.PointsPerLevel * x.MaxLevel).Sum();
            if (raisedCeliningMaxPoints == default)
                return true;
            
            __result = raisedCeliningMaxPoints;
            return false;
        }
        catch (Exception e)
        {
            Logger.LogError(e);
            return true;
        }
    }
}