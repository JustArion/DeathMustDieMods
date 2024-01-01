namespace Dawn.DMD.StarCruxExpansion.Helpers;

using System.Collections.Generic;
using Death.Darkness;
using Reflection;

public static class ChallengeDataEx
{
    // We'll maybe apply some save-state things if its requested.
    public static DarknessOptions ToDarknessOptions(this IEnumerable<ChallengeData> challengeData)
    {
        var options = new DarknessOptions();
        var challenges = options.Challenges();
        challenges.AddRange(challengeData.Select(data => new DarknessOptions.Challenge(data.Code)));

        return options;
    }
}