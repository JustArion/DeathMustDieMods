namespace Dawn.DMD.StarCruxExpansion.Helpers;

using System.Collections.Generic;
using Death.Darkness;
using Realms.UI;

public static class ModdedRealmHelper
{
    public static bool TryGetModdedChallenge(string code, out ChallengeData data)
    {
        var challenges = GetAllModdedChallenges();

        data = challenges.FirstOrDefault(x => x.Code == code);

        return data != null;
    }

    public static IEnumerable<ChallengeData> GetAllModdedChallenges() => ModdedRealmManager._allModdedChallenges.Select(x => x.ChallengeData);
}