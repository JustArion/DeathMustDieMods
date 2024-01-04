namespace Dawn.DMD.StarCruxExpansion.Harmony;

using Death.TimesRealm.UserInterface.Darkness;
using JetBrains.Annotations;

[HarmonyPatch(typeof(Screen_Darkness), nameof(Screen_Darkness.Init))]
public class InterceptStarCruxUI_Patch
{
    public static event Action<Screen_Darkness> OnDarknessInit;

    [UsedImplicitly]
    private static void Postfix(Screen_Darkness __instance)
    {
        ModLogger.LogDebug(nameof(OnDarknessInit));
        try
        {
            OnDarknessInit?.Invoke(__instance);
        }
        catch (Exception e)
        {
            ModLogger.LogError(e);
        }
    }
}