namespace Dawn.DMD.StarCruxExpansion;

using System.Collections;
using System.Collections.Generic;
using Death.Darkness;
using Helpers;

public class RealmData(string realmName, IEnumerable<ChallengeDataInformation> challenges) : IEnumerable<ChallengeData>
{
    internal DarknessOptions _options = challenges.Select(x => x.ChallengeData).ToDarknessOptions();
    public string RealmName { get; } = realmName;

    public IEnumerable<ChallengeDataInformation> ChallengesInformation => challenges;
    public IEnumerable<ChallengeData> Challenges => challenges.Select(x => x.ChallengeData);

    public IEnumerable<(string Title, string DescriptionFormat)> ChallengeDataInformation => challenges.Select(x => x.TextInformation);
    
    public IEnumerator<ChallengeData> GetEnumerator() => challenges.Select(x => x.ChallengeData).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}