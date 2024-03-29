﻿#nullable enable
namespace Dawn.DMD.StarCruxExpansion.Realms.RealmHelpers.GameDurationChangeHandler.Harmony;

using Death.Run.Systems;
using JetBrains.Annotations;

[HarmonyPatch(typeof(Facade_Run), nameof(Facade_Run.Begin))]
public class OnRunStart_Patch
{
    public static event Action<Facade_Run>? OnRunStart;
    
    [UsedImplicitly]
    private static void Postfix(Facade_Run __instance)
    {
        try
        {
            OnRunStart?.Invoke(__instance);
        }
        catch (Exception e)
        {
            ModLogger.LogError(e);
        }
    }
    
}