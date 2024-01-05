namespace Dawn.DMD.StarCruxExpansion.Harmony;

using Claw.Core.Utils;
using Death.TimesRealm.UserInterface.Darkness;
using JetBrains.Annotations;
using Reflection;

[HarmonyPatch(typeof(GUI_DarknessBar), nameof(GUI_DarknessBar.UpdateDisplay))]
public class Over3DigitsStarCruxBarTidy_Patch
{
    [UsedImplicitly]
    private static void Postfix(GUI_DarknessBar __instance)
    {
        var totalPoints = __instance.Controller().TotalPoints;

        if (totalPoints >= 100)
        {
            __instance.transform.Find("Icon_Points")
                .SetLocalPosX(405); // This shifts it more to the right, so that the third digit can show properly.
        }
    }
}