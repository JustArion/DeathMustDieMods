﻿namespace Dawn.DMD.StarCruxExpansion.Harmony;

using Death.Darkness;
using Death.TimesRealm.UserInterface.Darkness;
using JetBrains.Annotations;

[HarmonyPatch(typeof(GUI_Darkness), nameof(GUI_Darkness.ShowAsync))]
[HarmonyPatch([ typeof(IDarknessController) ])]
public class InterceptStarCruxData_Patch
{
    public static DarknessController VanillaDarknessController { get; private set; }

    [UsedImplicitly]
    private static bool Prefix(IDarknessController darkness)
    {
        try
        {
            if (darkness is not DarknessController controller)
                return true;
            VanillaDarknessController = controller;
            ModLogger.LogDebug(nameof(VanillaDarknessController));

        }
        catch (Exception e)
        {
            ModLogger.LogError(e);
        }

        return true;
    }
}