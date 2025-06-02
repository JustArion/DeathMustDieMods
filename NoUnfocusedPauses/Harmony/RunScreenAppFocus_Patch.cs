#nullable enable

using Death.TimesRealm.UserInterface;

namespace Dawn.DMD.NoUnfocusedPauses.Harmony;

using JetBrains.Annotations;

[HarmonyPatch(typeof(RunScreen), "OnApplicationFocus")]
public class RunScreenAppFocus_Patch
{
    [UsedImplicitly]
    private static bool Prefix(RunScreen __instance)
    {
        return false;
    }
}