namespace Dawn.DMD.StarCruxExpansion;

using System.Collections.Generic;

internal static class StarCruxModifier
{
    internal static void ModifyChallengeData(string Code, scoped ref int MaxLevel, scoped ref int PointsPerLevel)
    {
        if (!StarCruxVerifier.ExpectedChallenges.TryGetValue(Code, out _))
        {
            ModLogger.LogError($"Unknown / Unexpected challenge code: {Code}");
            return;
        }
        
        ModifiedChallenges.TryGetValue(Code, out var modified);

        var priorMaxLevel = MaxLevel;
        var priorPointsPerLevel = PointsPerLevel;
        
        MaxLevel = modified.MaxLevel;
        PointsPerLevel = modified.PointsPerLevel;
        
        
        if (priorMaxLevel != MaxLevel || priorPointsPerLevel != PointsPerLevel)
            ModLogger.LogDebug($"Star Crux '{Code}' :: [Max Level: {priorMaxLevel}, Points Per Level: {priorPointsPerLevel}] -> [Max Level: {MaxLevel}, Points Per Level: {PointsPerLevel}]");
    }
    
    
    internal static readonly Dictionary<string, (int MaxLevel, int PointsPerLevel)> ModifiedChallenges = new()
    {
        ["EliteMoreMs"]     = (MaxLevel: 6, PointsPerLevel: 2),
        ["EliteMoreAre"]    = (MaxLevel: 3, PointsPerLevel: 1),
        ["MinionsMoreDmg"]  = (MaxLevel: 1, PointsPerLevel: 3),
        ["EnemiesMoreProj"] = (MaxLevel: 3, PointsPerLevel: 5),
        ["NoPickUps"]       = (MaxLevel: 2, PointsPerLevel: 3),
        ["BossesMoreDmg"]   = (MaxLevel: 4, PointsPerLevel: 3),
        ["PlayerNoHeal"]    = (MaxLevel: 4, PointsPerLevel: 4),
        ["BossesMoreLife"]  = (MaxLevel: 6, PointsPerLevel: 4),
    };
}