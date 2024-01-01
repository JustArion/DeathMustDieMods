namespace Dawn.DMD.StarCruxExpansion;

using System.Collections;
using System.Collections.Generic;
using Death.Darkness;

public class RealmData(string realmName, IEnumerable<ChallengeDataInformation> challenges) : IEnumerable<ChallengeData>
{
    public string RealmName { get; } = realmName;

    public IEnumerable<ChallengeDataInformation> ChallengesInformation => challenges;
    public IEnumerable<ChallengeData> Challenges => challenges.Select(x => x.ChallengeData);

    public IEnumerable<ChallengeDataTextInformation> ChallengeDataInformation => challenges.Select(x => x.TextInformation);
    
    public IEnumerator<ChallengeData> GetEnumerator() => challenges.Select(x => x.ChallengeData).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}