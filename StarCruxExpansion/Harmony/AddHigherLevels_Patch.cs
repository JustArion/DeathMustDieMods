﻿namespace Dawn.DMD.StarCruxExpansion.Harmony;

using System.Collections.Generic;
using Death.Darkness;
using JetBrains.Annotations;

/// <summary>
/// Allows modifying the Star Crux challenge before its created, allowing variations in its max level and points per level.
/// </summary>

[HarmonyPatch(MethodType.Constructor)]
[HarmonyPatch(typeof(ChallengeData))]
[HarmonyPatch([
    typeof(string),
    typeof(int),
    typeof(int),
    typeof(int),
    typeof(string),
    typeof(List<ChallengeData.Effect>)
])]
public class AddHigherLevels_Patch
{
    [UsedImplicitly]
    private static bool Prefix(string code, ref int maxLevel, ref int pointsPerLevel, int winsToUnlock, string iconPath, List<ChallengeData.Effect> effects)
    {
        // Logger.LogDebug($"{nameof(AddHigherLevels_Patch)}: Code: {code}, MaxLevel: {maxLevel}, PointsPerLevel: {pointsPerLevel}, WinsToUnlock: {winsToUnlock}, IconPath: {iconPath}, Effects: {effects.Count}");

        if (!StarCruxVerifier.IsExpectedStarCruxChallenge(code, maxLevel, pointsPerLevel))
            return true;
        
        StarCruxModifier.ModifyChallengeData(code, ref maxLevel, ref pointsPerLevel);
        
        return true;
    }
}