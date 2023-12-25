namespace Dawn.DMD.StarCruxExpansion;

using System.Collections.Generic;

public class StarCruxVerifier
{
    public static bool IsExpectedStarCruxChallenge(string Code, int MaxLevel, int PointsPerLevel)
    {
        if (!ExpectedChallenges.TryGetValue(Code, out var challenge))
        {
            Logger.LogWarning($"Unknown Star Crux Code: '{Code}', Values: [{PointsPerLevel}, {MaxLevel}]");
            return false;
        }

        var retVal =  PointsPerLevel == challenge.PointsPerLevel 
               && MaxLevel == challenge.MaxLevel;

        if (!retVal)
        {
            Logger.LogWarning($"Unexpected Star Crux Values for '{Code}'. Expected [Max: {challenge.MaxLevel}, PPL: {challenge.PointsPerLevel}], " +
                              $"Got: [Max: {MaxLevel}, PPL: {PointsPerLevel}]");
        }

        return retVal;
    }

    internal static readonly Dictionary<string, (int MaxLevel, int PointsPerLevel)> ExpectedChallenges = new()
    {
        ["EliteMoreMs"]     = (MaxLevel: 3, PointsPerLevel: 2),
        ["EliteMoreAre"]    = (MaxLevel: 2, PointsPerLevel: 1),
        ["MinionsMoreDmg"]  = (MaxLevel: 1, PointsPerLevel: 3),
        ["EnemiesMoreProj"] = (MaxLevel: 1, PointsPerLevel: 3),
        ["NoPickUps"]       = (MaxLevel: 1, PointsPerLevel: 2),
        ["BossesMoreDmg"]   = (MaxLevel: 2, PointsPerLevel: 2),
        ["PlayerNoHeal"]    = (MaxLevel: 1, PointsPerLevel: 4),
        ["BossesMoreLife"]  = (MaxLevel: 3, PointsPerLevel: 2),
    };
}