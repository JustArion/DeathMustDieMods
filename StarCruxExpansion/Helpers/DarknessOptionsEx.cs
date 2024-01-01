namespace Dawn.DMD.StarCruxExpansion.Helpers;

using System.Collections.Generic;
using Death.Darkness;
using Reflection;

public static class DarknessOptionsEx
{
    public static void CopyTo(this DarknessOptions options, DarknessOptions target)
    {
        var originalChallenges = options.Challenges();
        var targetChallenges = target.Challenges();

        targetChallenges.AddRange(originalChallenges);
    }
    
    public static void CopyTo(this IEnumerable<ChallengeData> options, DarknessOptions target)
    {
        var originalChallenges = options.ToDarknessOptions().Challenges();
        var targetChallenges = target.Challenges();

        targetChallenges.AddRange(originalChallenges);
    }
}